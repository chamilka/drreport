using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCarReport.Service
{
    class LoginService
    {
        public tbl_user validateUser(string username, string password, string role)
        {
            syncdbEntities db = new syncdbEntities();
            return db.tbl_user.Where(u => u.USERNAME.Equals(username) && u.PASSWORD.Equals(password) && u.ROLE.Equals(role) && u.STATUS == 0).SingleOrDefault();
        }
    }
}
