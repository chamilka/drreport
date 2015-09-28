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

namespace DoctorCarReport
{
    /// <summary>
    /// Interaction logic for ApplicationStarter.xaml
    /// </summary>
    public partial class ApplicationStarter : Window
    {
        public ApplicationStarter()
        {
            InitializeComponent();
            MainWindow.Instance.Show();
            //Tree.Instance.Show();
            this.Close();
        }
    }
}
