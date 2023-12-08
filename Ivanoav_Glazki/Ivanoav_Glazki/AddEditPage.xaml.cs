using Microsoft.Win32;
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
   
    public partial class AddEditPage : Page
    {
        private Agent _currentAgent = new Agent();
        List<AgentType> AgentTypes = GlazkiEntities1.GetContext().AgentType.ToList();

        
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();

            if (SelectedAgent != null)
                _currentAgent = SelectedAgent;
            DataContext = _currentAgent;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(_currentAgent.Title))
                    errors.AppendLine("Укажите наименование агента");
                if (string.IsNullOrWhiteSpace(_currentAgent.Address))
                    errors.AppendLine("Укажите адрес агента");
                if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
                    errors.AppendLine("Укажите ФИО директора");
                if (ComboType.SelectedItem == null)
                    errors.AppendLine("Укажите тип агента");
                if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
                    errors.AppendLine("Укажите приоритет агента");
                if (_currentAgent.Priority <= 0)
                    errors.AppendLine("Укажите положительный приоритет агента");
                if (string.IsNullOrWhiteSpace(_currentAgent.INN))
                    errors.AppendLine("Укажите ИНН агента");
                if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
                    errors.AppendLine("Укажите КПП агента");
                if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
                    errors.AppendLine("Укажите телефон агента");
                else
                {
                    string ph = _currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "").Replace(")", "").Replace(" ", "");
                    if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11) || (ph[1] == '3' && ph.Length != 12))
                        errors.AppendLine("Укажите правильно телефон агента");
                }
                if (string.IsNullOrWhiteSpace(_currentAgent.Email))
                    errors.AppendLine("Укажите почту агента");
                var currentType = (TextBlock)ComboType.SelectedItem;
                string currentTypeContent = currentType.Text;
                foreach (AgentType type in AgentTypes)
                {
                    if (type.Title.ToString() == currentTypeContent)
                    {
                        _currentAgent.AgentType = type;
                        _currentAgent.AgentTypeID = type.ID;
                        break;
                    }
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                if (_currentAgent.ID == 0)
                    GlazkiEntities1.GetContext().Agent.Add(_currentAgent);
                try
                { GlazkiEntities1.GetContext().SaveChanges();
                    MessageBox.Show("информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            {
                var currentAgent = (sender as Button).DataContext as Agent;
                var currentAgent1 = GlazkiEntities1.GetContext().Agent.ToList();
                currentAgent1 = currentAgent1.Where(p => p.ID == currentAgent.ID).ToList();
                if (currentAgent1.Count != 0)
                    MessageBox.Show("Невозможно выполнить удаление, т.к. существуют записи на эту услугу");
                else
                {
                    if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            GlazkiEntities1.GetContext().Agent.Remove(currentAgent);
                            GlazkiEntities1.GetContext().SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
            }
        }
        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if( myOpenFileDialog.ShowDialog()== true )
            {
                _currentAgent.Logo = myOpenFileDialog.FileName;
                LogoImage.Source=new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void SalePage_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SalePage(_currentAgent));
        }
    }
}
