using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCarReport.util
{
    class ExpenseView
    {
        private string item;
        private Decimal? amount;
        private string  remark;

        public string  Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        

        public Decimal? Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        

        public string Item
        {
            get { return item; }
            set { item = value; }
        }
        
    }
}
