using DoctorCarReport.wpfPage;
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

namespace DoctorCarReport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static MainWindow instance;

        public static MainWindow Instance
        {
            get {
                if (instance == null)
                {
                    instance = new MainWindow();
                }
                return instance;
            }
        }
        

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = Login.Instance;
            //ContentFrame.Content = OrganizationListPage.Instance;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = Login.Instance;
            btnLogout.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
