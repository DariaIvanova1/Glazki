using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ivanoav_Glazki
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;
        public List<Agent> CurrentPageList = new List<Agent>();
        public List<Agent> TableList;

        public ServicePage()
        {
            InitializeComponent();
            var currentAgent = GlazkiEntities1.GetContext().Agent.ToList();
            ServiceListView.ItemsSource = currentAgent;
            updateserice();
        }

        public void updateserice()
        {
            var currentAgent = GlazkiEntities1.GetContext().Agent.ToList();
            currentAgent = currentAgent.Where(p => p.Title.ToLower().Contains(TBSeartch.Text.ToLower()) || p.Phone.Replace("+7", "8").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Contains(TBSeartch.Text.Replace("+7", "8").
                 Replace("(", "").Replace("-", "").Replace(" ", "")) || p.Email.ToLower().Contains(TBSeartch.Text.ToLower())).ToList();
            if (ComboBox.SelectedIndex == 0)
                currentAgent = currentAgent.OrderBy(p => p.Title).ToList();
            if (ComboBox.SelectedIndex == 1)
                currentAgent = currentAgent.OrderByDescending(p => p.Title).ToList();
            if (ComboBox.SelectedIndex == 2)
                currentAgent = currentAgent.OrderBy(p => p.Priority).ToList();
            if (ComboBox.SelectedIndex == 3)
                currentAgent = currentAgent.OrderByDescending(p => p.Priority).ToList();
            if (ComboBox.SelectedIndex == 4)
                currentAgent = currentAgent.OrderBy(p => p.Title).ToList();
            if (ComboBox.SelectedIndex == 5)
                currentAgent = currentAgent.OrderByDescending(p => p.Title).ToList();

            if (ComboBoxType.SelectedIndex == 0)
                currentAgent = currentAgent.Where(p => (p.AgentTypeID >= 1 && p.AgentTypeID<=6)).ToList();
            if (ComboBoxType.SelectedIndex == 1)
                currentAgent = currentAgent.Where(p => (p.AgentTypeID ==1)).ToList();
            if (ComboBoxType.SelectedIndex == 2)
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 2)).ToList();
            if (ComboBoxType.SelectedIndex == 3)
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 3)).ToList();
            if (ComboBoxType.SelectedIndex == 4)
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 4)).ToList();
            if (ComboBoxType.SelectedIndex == 5)
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 5)).ToList();
            if (ComboBoxType.SelectedIndex == 6)
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 6)).ToList();


            ServiceListView.ItemsSource = currentAgent;
            ServiceListView.ItemsSource = currentAgent;
            TableList = currentAgent;
            ChangePage(0, 0);

        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateserice();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateserice();
        }

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateserice();
        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void RigthDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }
        private void ChangePage(int direction, int? selectedPage)
        {


            CurrentPageList.Clear();
            CountRecords = TableList.Count;
            if (CountRecords % 10 > 0) CountPage = CountRecords / 10 + 1;
            else CountPage = CountRecords / 10;

            Boolean Ifupdate = true;

            int min;
            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
           

            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }

                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                    case 2:
                        if (CurrentPage > CurrentPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                               CurrentPageList.Add(TableList[i]);
                                ;
                            }

                        }
                        else
                        {
                            Ifupdate = false;
                        }

                        break;
                }
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();

                for (int i = 1; i <= CountPage; i++) PageListBox.Items.Add(i);
                PageListBox.SelectedIndex = CurrentPage;
                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBALLRecords.Text = "из" + CountRecords.ToString();
                ServiceListView.ItemsSource = CurrentPageList;
                ServiceListView.Items.Refresh();

            }

            
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
           Manager.MainFrame.Navigate(new AddEditPage((sender as Button ).DataContext as Agent));
            updateserice();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void ChangePriory_Click(object sender, RoutedEventArgs e)
        {
            int max = 0;
            foreach(Agent agent in ServiceListView.SelectedItems)

            {
                if(max<agent.Priority) max = agent.Priority;
             
            }
           
            PRWindow window = new PRWindow(max);
            window.ShowDialog();
            if (string.IsNullOrEmpty(window.PriorityBox.Text))
            {
                return;
            }
            foreach (Agent AgentLV in ServiceListView.SelectedItems)
            {
                AgentLV.Priority = Convert.ToInt32(window.PriorityBox.Text);
            }
            try
            {
                GlazkiEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            updateserice();
        }
        private void ServiceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceListView.SelectedItems.Count > 1)
            {
                ChangePriory.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePriory.Visibility = Visibility.Hidden;
                
                
            }
        }

     
    }
}
