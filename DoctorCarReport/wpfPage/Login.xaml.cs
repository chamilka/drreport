using DoctorCarReport.Service;
using DoctorCarReport.util;
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

namespace DoctorCarReport.wpfPage
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        public Login()
        {
            InitializeComponent();
        }

        private static Login instance;

        public static Login Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Login();
                }
                return instance;
            }
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text="admin";
            string password = txtPassword.Password="axi0helix";
            


            if (!username.Equals("") && !password.Equals(""))
            {
                try
                {
                    lblStatus.Content = "";
                    LoginService service = new LoginService();
                    tbl_user user = service.validateUser(username, password, "report");
                    if (user != null)
                    {
                        MainWindow.Instance.ContentFrame.Content = Report.Instance;
                        MainWindow.Instance.btnLogout.Visibility = System.Windows.Visibility.Visible;
                    }

                    else
                    {
                        lblStatus.Content = "Invalid username or password";
                    }
                }
                catch (Exception ex)
                {
                   // MessageBox.Show("Error", "Unable to connect database, Please check your Internet connection");
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
