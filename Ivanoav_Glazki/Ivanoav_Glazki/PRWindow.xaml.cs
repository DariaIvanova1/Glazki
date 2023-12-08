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
using System.Windows.Shapes;

namespace Ivanoav_Glazki
{
    /// <summary>
    /// Логика взаимодействия для PRWindow.xaml
    /// </summary>
    public partial class PRWindow : Window
    {
        private Agent _currentAgent = new Agent();
        public PRWindow(int max)
        {
            InitializeComponent();
            PriorityBox.Text=max.ToString();
        }
    
    
        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PriorityBox.Text))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Введите новый приоритет агентов!");
            }
        }

        private void CloseBT_Click(object sender, RoutedEventArgs e)
        {
            PriorityBox.Text = "";
            Close();
        }
    }
}
