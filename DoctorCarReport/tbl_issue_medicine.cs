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
    
    public partial class tbl_issue_medicine
    {
        public string ID { get; set; }
        public Nullable<double> DOSAGE { get; set; }
        public string UNIT { get; set; }
        public string REMARK { get; set; }
        public Nullable<System.DateTime> INSERT_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string FKY_PLACE { get; set; }
        public string FKY_MEDICINE { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }
}
