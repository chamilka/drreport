using DoctorCarReport.Service;
using DoctorCarReport.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Tree.xaml
    /// </summary>
    public partial class Tree : Window
    {
        public ObservableCollection<Organization> Organizations { get; set; }

        public Tree()
        {
            InitializeComponent();

            OrganizationService service = new OrganizationService();
            List<tbl_organization> listOrganization = service.getOrganizationList();
            this.Organizations = new ObservableCollection<Organization>();
            foreach (tbl_organization organization in listOrganization)
            {
                
                ConnectionStringManager.updateConnectionStrings(organization.ID.ToString());

                DrCarDriveService drCarService = new DrCarDriveService();
                List<Car> listCar = drCarService.getDrCarList();

                this.Organizations.Add(new Organization() { Id = organization.ID, Name = organization.ORGANIZATION_NAME, Cars = listCar });

            }


            foreach (Organization org in this.Organizations)
                foreach (Car car in org.Cars)
                    car.SetValue(ItemHelper.ParentProperty, org);
        }

        private static Tree instance;

        public static Tree Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Tree();
                }
                return instance;
            }
        }

        private void Button_PrintCrew_Click(object sender, RoutedEventArgs e)
        {
            string crew = "";
            foreach (Organization org in this.Organizations)
                if (ItemHelper.GetIsChecked(org) == true)
                    crew += org.Name + ", ";
                //foreach (Car car in org.Cars)
                    //if (ItemHelper.GetIsChecked(car) == true)
                    //    crew += car.RegNo + ", ";
            crew = crew.TrimEnd(new char[] { ',', ' ' });
            this.textBoxCrew.Text = "Your crew: " + crew;
        }
    }
}
