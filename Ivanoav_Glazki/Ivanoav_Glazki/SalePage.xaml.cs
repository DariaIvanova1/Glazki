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
    /// Логика взаимодействия для SalePage.xaml
    /// </summary>
    public partial class SalePage : Page
    {
       Agent currentAgent;
        public SalePage(Agent SelectedAgent)
        {
            InitializeComponent();

            currentAgent = SelectedAgent;

            var currentSales = GlazkiEntities1.GetContext().ProductSale.ToList();

            if (SelectedAgent != null)
            {
                if (SelectedAgent.ID != 0)
                {
                    currentSales = currentSales.Where(p => p.AgentID == SelectedAgent.ID).ToList();
                }
                SalesListView.ItemsSource = currentSales;
                DeleteSale.Visibility = Visibility.Collapsed;
            }
            else
            {
                SalesListView.ItemsSource = new List<ProductSale>();
            }
        }
            private void UpdateSale()
        {
            var currentSales = GlazkiEntities1.GetContext().ProductSale.ToList();

            if (currentAgent.ID != 0)
            {
                currentSales = currentSales.Where(p => p.Agent.ID == currentAgent.ID).ToList();
            }
            SalesListView.ItemsSource = currentSales;
            SalesListView.Items.Refresh();
        }

        private void SalesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SalesListView.SelectedItems.Count == 0)
                DeleteSale.Visibility = Visibility.Collapsed;
            if (SalesListView.SelectedItems.Count > 0)
                DeleteSale.Visibility = Visibility.Visible;
            UpdateSale();
            SalesListView.Items.Refresh();
        }

        private void AddSale_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddSalePage(currentAgent));
            UpdateSale();
            SalesListView.Items.Refresh();
        }

        private void DeleteSale_Click(object sender, RoutedEventArgs e)
        {
            List<ProductSale> SelectedSales = SalesListView.SelectedItems.Cast<ProductSale>().ToList();

            foreach(ProductSale currentSales in SelectedSales)
            {
               GlazkiEntities1.GetContext().ProductSale.Remove(currentSales);
            }
            GlazkiEntities1.GetContext().SaveChanges();
            UpdateSale();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateSale();
        }
    }
    
}
