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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Forms;
using DoctorCarReport.util;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Threading;

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
            if (validInputs())
            {
                try
                {
                   // ExcelGenerator generator = new ExcelGenerator();
                    ExcelGeneratorByCar generator = new ExcelGeneratorByCar();

                    DateTime? fromDate = dateFrom.SelectedDate;
                    DateTime toDate = dateTo.SelectedDate.Value;
                    int recordCount = 0;
                    foreach (Organization org in this.Organizations)
                    {
                        if (ItemHelper.GetIsChecked(org) == true)
                        {
                            foreach (Car car in org.Cars)
                            {
                                if (ItemHelper.GetIsChecked(car) == true)
                                {

                                    //string filePath = txtSavePath.Text + "\\Report_" + org.Name.Trim() + "_" + car.RegNo.Trim() + "_" + fromDate.Value.Date.ToString("yyyy-MM-dd") + ".xlsx";
                                    string filePath = txtSavePath.Text + "\\Report_" + org.Name.Trim() + "_" + car.RegNo.Trim() + ".xlsx";
                                    
                                    if (File.Exists(filePath))
                                    {
                                        var result = System.Windows.MessageBox.Show("Report file for your selected inputs already exists. Do you want to replace this file?", "File already exists", System.Windows.MessageBoxButton.YesNoCancel);

                                        if (result == System.Windows.MessageBoxResult.Cancel)
                                        {
                                            return;
                                        }
                                        else if (result == System.Windows.MessageBoxResult.Yes)
                                        {
                                            File.Delete(filePath);
                                        }
                                        else if (result == System.Windows.MessageBoxResult.No)
                                        {
                                            filePath = generateNewFilePath(filePath);
                                        }
                                    }


                                    ConnectionStringManager.updateConnectionStrings(org.Id.ToString());

                                    DrCarDriveService service = new DrCarDriveService();

                                    List<DriveHistoryView> record = service.viewHistoryRecordByCarIDDateFromTo(car.Id, fromDate.Value, toDate.AddDays(1));
                                    if (record.Count > 0)
                                    {
                                        progReport.Visibility = System.Windows.Visibility.Hidden;
                                        generator.createExcelReport(filePath, car.RegNo, org.Name, record);
                                        //Thread t = new Thread(() => runCreateReport(generator, filePath, car.RegNo, record, (double)(recordCount+1/record.Count)));
                                        //t.SetApartmentState(ApartmentState.STA);
                                        //t.Start();
                                        recordCount++;
                                    }
                                }

                            }
                        }
                    }
                    if (recordCount > 0)
                    {
                        System.Windows.MessageBox.Show("Report created successfully", "Success");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("No records available for the selected inputs");
                    }

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Failed to create report");
                }
            }
        }

        //private void runCreateReport(ExcelGenerator generator, string filePath, string regNo, List<DriveHistoryView> record, double count)
        //{
        //    System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
        //    {
        //        generator.createExcelReport(filePath, regNo, record);
        //        progReport.Value = progReport.Value + count*100;
                
        //    }));
            
        //}

        private string generateNewFilePath(string filePath)
        {
            int num = 2;
            string newName = "";
            string extension = Path.GetExtension(filePath);
            string path = filePath.Replace(Path.GetFileName(filePath), "");
            do
            {
                newName = Path.GetFileNameWithoutExtension(filePath) + "(" + num + ")";
                num++;
            } while (File.Exists(path + newName + extension));

            return path + newName + extension;
        }

        private bool validInputs()
        {
            DateTime? fromDate = dateFrom.SelectedDate;
            DateTime? toDate = dateTo.SelectedDate;

            if (txtSavePath.Text == "" || txtSavePath.Text == null)
            {
                System.Windows.MessageBox.Show("Please select report save directory");
                return false;
            }
            if (fromDate == null)
            {
                System.Windows.MessageBox.Show("Please select report start date");
                return false;
            }
            if (toDate == null)
            {
                System.Windows.MessageBox.Show("Please select report end date");
                return false;
            }

            int numCars = 0;
            foreach (Organization org in this.Organizations)
            {
                foreach (Car car in org.Cars)
                {
                    if (ItemHelper.GetIsChecked(car) == true)
                    {
                        numCars++;
                    }
                }
            }
            if (numCars == 0)
            {
                System.Windows.MessageBox.Show("Please select organization and cars");
                return false;
            }

            return true;

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
                foreach (var item in treeView.Items)
                {
                    DependencyObject dObject = treeView.ItemContainerGenerator.ContainerFromItem(item);
                    TreeViewItem itm = (TreeViewItem)dObject;
                    itm.IsExpanded = false;
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
