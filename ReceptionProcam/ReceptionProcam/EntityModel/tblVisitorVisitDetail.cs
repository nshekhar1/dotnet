//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReceptionProcam.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblVisitorVisitDetail
    {
        public int Id { get; set; }
        public string VisitorId { get; set; }
        public string GovIdNo { get; set; }
        public string AssetId { get; set; }
        public string Location { get; set; }
        public string ToMeet { get; set; }
        public string SubLocation { get; set; }
        public string OfficeLocation { get; set; }
        public string Gate { get; set; }
        public string Purpose { get; set; }
        public string TimeIn { get; set; }
        public string ValidUpto { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsPassReturned { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
