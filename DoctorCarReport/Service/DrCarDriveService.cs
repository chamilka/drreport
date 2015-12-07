using DoctorCarReport.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCarReport.Service
{
    public class DrCarDriveService
    {
        private static drcardriveEntities db;

        public DrCarDriveService()
        {
            db = new drcardriveEntities(ConnectionStringManager.ecsb.ToString());
            //db = new drcardriveEntities();
        }

        public List<Car> getDrCarList()
        {
            return db.tbl_car.Where(c => c.STATUS == true).Select(c => new Car{Id=c.ID,RegNo=c.REG_NO}).ToList();
        }

        internal List<DriveHistoryView> viewHistoryRecordByCarID(string carID)
        {

            var query = (from d in db.tbl_drive
                         where d.FKY_CAR == carID
                         join p in db.tbl_places on d.ID equals p.FKY_DRIVE into pl
                         join f in db.tbl_fuel on d.ID equals f.FKY_DRIVE into fd
                         join e in db.tbl_expenses on d.ID equals e.FKY_DRIVE into ex
                         select new DriveHistoryView()
                         {
                             DriveID = d.ID,
                             DriverName = d.DRIVER_NAME,
                             StartDate = d.START_DATE,
                             StartOdometer = d.START_ODOMETER,
                             EndDate = d.END_DATE,
                             EndOdometer = d.END_ODOMETER,
                             From = d.FROM,
                             To = d.TO,
                             Places = pl,
                             FuelTopup = fd,
                             NumPatients = pl.Sum(pls => pls.NUMBER_PATIENT),
                             NumDoctors = pl.Sum(pls => pls.NUMBER_DOCTORS),
                             NumParamedics = pl.Sum(pls => pls.NUMBER_PARAMEDICS),
                             InsertDateTime = d.INSERT_DATETIME
                         }).OrderBy(d=>d.StartDate).ToList();

            return query;

        }

        internal List<tbl_issue_medicine> getIssueMedicineByPlaceId(string placeId)
        {
            return db.tbl_issue_medicine.Where(im => im.FKY_PLACE.Equals(placeId)).ToList();
        }

        internal List<ExpenseView> getOtherExpensesByDriveId(string driveId)
        {
            var query = (from e in db.tbl_expenses
                         where e.FKY_DRIVE == driveId
                         join ed in db.tbl_expense_desc on e.FKY_EXPENSE equals ed.ID
                         select new ExpenseView
                         {
                             Item = ed.DESCRIPTION,
                             Amount = e.AMOUNT,
                             Remark = e.REMARK,
                             AddDate = e.ADD_DATE
                         }).ToList();

            return query;
        }

        internal tbl_drive_medicine getMedicineById(string medicineId)
        {
            return db.tbl_drive_medicine.Where(m => m.ID.Equals(medicineId)).FirstOrDefault();
        }

        internal List<DriveHistoryView> viewHistoryRecordByCarIDDateFromTo(string carID, DateTime fromDate, DateTime toDate)
        {
            var query = (from d in db.tbl_drive
                         where d.FKY_CAR == carID 
                         && d.START_DATE>=fromDate
                         && d.START_DATE<toDate
                         && d.STATUS == false
                         join p in db.tbl_places on d.ID equals p.FKY_DRIVE into pl
                         join f in db.tbl_fuel on d.ID equals f.FKY_DRIVE into fd
                         select new DriveHistoryView()
                         {
                             DriveID = d.ID,
                             DriverName = d.DRIVER_NAME,
                             StartDate = d.START_DATE,
                             StartOdometer = d.START_ODOMETER,
                             EndDate = d.END_DATE,
                             EndOdometer = d.END_ODOMETER,
                             From = d.FROM,
                             To = d.TO,
                             Places = pl,
                             FuelTopup = fd,
                             NumPatients = pl.Sum(pls => pls.NUMBER_PATIENT),
                             NumDoctors = pl.Sum(pls => pls.NUMBER_DOCTORS),
                             NumParamedics = pl.Sum(pls => pls.NUMBER_PARAMEDICS),
                             InsertDateTime = d.INSERT_DATETIME
                         }).ToList();

            return query;
        }
    }
}
