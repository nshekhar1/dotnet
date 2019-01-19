using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReceptionProcam.Areas.Area.Models
{
    public class clsAssetIssueDetails
    {
        public int ID { get; set; }
        public Nullable<int> EmpId { get; set; }
        public int[] AssetId { get; set; }
        public Nullable<System.DateTime> AssetIssueDateTime { get; set; }
        public Nullable<System.DateTime> AssetSubmitDateTime { get; set; }
        public Nullable<bool> IsSubmited { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}