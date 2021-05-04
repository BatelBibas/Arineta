using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using PopulationRegistry.ViewModels;

namespace PopulationRegistry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Initializa population registry data
            try
            {
                string path = "../../persons.json";
                String myStream = File.ReadAllText(path);
                DataContext = new MainViewModel(myStream);
            }
            catch(Exception ex)
            {
                // TBD: add error 
                MessageBox.Show("Error parsing persons data file. Please chack file and try again.");
            }

            InitializeComponent();
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NavCtrl_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
