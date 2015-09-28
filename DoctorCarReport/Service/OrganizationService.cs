using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCarReport.Service
{
    class OrganizationService
    {
        public List<tbl_organization> getOrganizationList()
        {
            syncdbEntities db = new syncdbEntities();
            return db.tbl_organization.Where(o => o.STATUS == 0).OrderBy(o => o.ORGANIZATION_NAME).ToList();
        }

    }
}
