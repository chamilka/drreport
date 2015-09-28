using DoctorCarReport.Service;
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
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using DoctorCarReport.util;
using System.Configuration;
using System.Collections.ObjectModel;

namespace DoctorCarReport.wpfPage
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        public ObservableCollection<Organization> Organizations { get; set; }
        public static bool allSelected;

        public Report()
        {
            InitializeComponent();
        }

        private static Report instance;

        public static Report Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Report();
                }
                instance.populateOrganizationList();
                return instance;
            }
        }

        private void populateOrganizationList()
        {
            try
            {
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
                {
                    foreach (Car car in org.Cars)
                    {
                        car.SetValue(ItemHelper.ParentProperty, org);
                    }
                }

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = config.AppSettings.Settings;
                txtSavePath.Text = settings["FileSavePath"].Value;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }


        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (txtSavePath.Text != "")
            {
                folderBrowser.SelectedPath = txtSavePath.Text;
            }
            DialogResult result = folderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtSavePath.Text = folderBrowser.SelectedPath;
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = config.AppSettings.Settings;
                settings["FileSavePath"].Value = folderBrowser.SelectedPath;
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    ExcelGenerator generator = new ExcelGenerator();

            //    DateTime? fromDate = dateFrom.SelectedDate;
            //    DateTime toDate = dateTo.SelectedDate.Value;

            //    string filePath = txtSavePath.Text + "\\Report_" + cmbOrganization.Text.Trim() + "_" + cmbDoctorCar.Text.Trim() + "_" + fromDate.Value.Date.ToString("yyyy-MM-dd") + ".xlsx";

            //    DrCarDriveService service = new DrCarDriveService();
            //    tbl_car selectedDrCar = (tbl_car)cmbDoctorCar.SelectedItem;



            //    if (fromDate == null || toDate == null || selectedDrCar == null || txtSavePath.Text == "" || txtSavePath.Text == null)
            //    {
            //        System.Windows.MessageBox.Show("Please select valid inputs");
            //    }
            //    else
            //    {
            //        List<DriveHistoryView> record = service.viewHistoryRecordByCarIDDateFromTo(selectedDrCar.ID, fromDate.Value, toDate.AddDays(1));
            //        if (record.Count > 0)
            //        {
            //            generator.createExcelReport(filePath, cmbDoctorCar.Text, record);
            //            System.Windows.MessageBox.Show("Report created successfully", "Success");
            //        }
            //        else
            //        {
            //            System.Windows.MessageBox.Show("No records available for the selected inputs");
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.Message, "Failed to create report");
            //}
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void cmbOrganization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    cmbDoctorCar.IsEnabled = true;
            //    tbl_organization organization = (tbl_organization)cmbOrganization.SelectedItem;
            //    string dbName = "drcardrive_" + organization.ID;
            //    ConnectionStringManager.updateConnectionStrings(dbName);

            //    DrCarDriveService service = new DrCarDriveService();
            //    List<tbl_car> listCar = service.getDrCarList();
            //    cmbDoctorCar.Items.Clear();
            //    foreach (tbl_car car in listCar)
            //    {
            //        cmbDoctorCar.Items.Add(car);
            //        cmbDoctorCar.DisplayMemberPath = "REG_NO";
            //        cmbDoctorCar.SelectedIndex = 0;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show("Unable to update database connection", "Error");
            //}

        }

        private void dateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //System.Windows.MessageBox.Show(dateFrom.Text);
            DateTime? fromDate = dateFrom.SelectedDate;
            if (fromDate != null)
            {
                dateTo.SelectedDate = fromDate.Value.AddDays(7);
            }
        }

        private void btnSelectAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (allSelected)
            {
                foreach (Organization org in this.Organizations)
                {
                    ItemHelper.SetIsChecked(org, false);
                }
                btnSelectAll.Content = "Select all";                
            }
            else if (!allSelected)
            {
                foreach (Organization org in this.Organizations)
                {
                    ItemHelper.SetIsChecked(org, true);
                }
                btnSelectAll.Content = "Deselect all";
            }
            allSelected = !allSelected;
        }

        


    }
}
