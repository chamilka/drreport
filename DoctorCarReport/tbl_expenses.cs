//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorCarReport
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_expenses
    {
        public string ID { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public Nullable<System.DateTime> ADD_DATE { get; set; }
        public string REMARK { get; set; }
        public Nullable<System.DateTime> INSERT_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string FKY_DRIVE { get; set; }
        public string FKY_EXPENSE { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }
}