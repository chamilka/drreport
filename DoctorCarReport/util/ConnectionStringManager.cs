using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using System.Data.Entity.Core.EntityClient;

namespace DoctorCarReport.util
{
    class ConnectionStringManager
    {
        private static string HOST = "153.120.61.151";
        private static string USERNAME = "syncuser";
        private static string PASSWORD = "mB7NpS3K";
        public static EntityConnectionStringBuilder ecsb;

        public static void updateConnectionStrings(string orgId)
        {
            try
            {
                string dbName = "drcardrive_" + orgId;
                
                SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                scsb.DataSource = HOST + "\\SQLEXPRESS";
                scsb.InitialCatalog = dbName;
                scsb.MultipleActiveResultSets = true;
                scsb.ApplicationName = "EntityFramework";
                
                if (HOST == "." && USERNAME == "")
                {
                    scsb.IntegratedSecurity = true;
                }
                else
                {
                    scsb.UserID = USERNAME;
                    scsb.Password = PASSWORD;
                }

                ecsb = new EntityConnectionStringBuilder();
                ecsb.Provider = "System.Data.SqlClient";
                ecsb.ProviderConnectionString = scsb.ToString();
                ecsb.Metadata = "res://*/DrCarDriveModel.csdl|res://*/DrCarDriveModel.ssdl|res://*/DrCarDriveModel.msl";

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

                connectionStringsSection.ConnectionStrings["drcardriveEntities"].ConnectionString = ecsb.ToString();

                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
