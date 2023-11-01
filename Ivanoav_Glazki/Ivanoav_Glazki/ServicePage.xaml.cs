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
        public ServicePage()
        {
            InitializeComponent();
            var currentAgent = GlazkiEntities.GetContext().Agent.ToList();
            ServiceListView.ItemsSource = currentAgent;
         
        }

        public void updateserice()
        {
            var currentAgent = GlazkiEntities.GetContext().Agent.ToList();
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
    }
}
