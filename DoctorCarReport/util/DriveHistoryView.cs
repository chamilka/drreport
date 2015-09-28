using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCarReport.util
{
    class DriveHistoryView
    {
        private string driveID;
        private string driverName;
        private Nullable<System.DateTime> startDate;
        private Nullable<double> startOdometer;
        private Nullable<System.DateTime> endDate;
        private Nullable<double> endOdometer;
        private string from;
        private string to;
        private IEnumerable<tbl_places> places;
        private IEnumerable<tbl_expenses> expenses;
        private IEnumerable<tbl_fuel> fuelTopup;
        private int? numPatients;
        private string placesList;
        private int? numDoctors;
        private int? numParamedics;
        private Nullable<System.DateTime> insertDateTime;

        public int? NumParamedics
        {
            get { return numParamedics; }
            set { numParamedics = value; }
        }


        public int? NumDoctors
        {
            get { return numDoctors; }
            set { numDoctors = value; }
        }

        public string PlacesList
        {
            get { return placesList; }
            set { placesList = value; }
        }

        public int? NumPatients
        {
            get { return numPatients; }
            set { numPatients = value; }
        }

        public string To
        {
            get { return to; }
            set { to = value; }
        }


        public string From
        {
            get { return from; }
            set { from = value; }
        }

        public string DriveID
        {
            get { return driveID; }
            set { driveID = value; }
        }


        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }


        public Nullable<System.DateTime> StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }


        public Nullable<double> StartOdometer
        {
            get { return startOdometer; }
            set { startOdometer = value; }
        }


        public Nullable<System.DateTime> EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }


        public Nullable<double> EndOdometer
        {
            get { return endOdometer; }
            set { endOdometer = value; }
        }

        public IEnumerable<tbl_places> Places
        {
            get { return places; }
            set { places = value; }
        }


        public IEnumerable<tbl_expenses> Expenses
        {
            get { return expenses; }
            set { expenses = value; }
        }


        public IEnumerable<tbl_fuel> FuelTopup
        {
            get { return fuelTopup; }
            set { fuelTopup = value; }
        }

        public Nullable<System.DateTime> InsertDateTime
        {
            get { return insertDateTime; }
            set { insertDateTime = value; }
        }

    }
}
