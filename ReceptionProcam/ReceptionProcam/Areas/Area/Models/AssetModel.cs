using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReceptionProcam.Areas.Area.Models
{
    public class AssetModel
    {

        public int Id { get; set; }

        public string AssetId { get; set; }

        public string AssetName { get; set; }

        public string EmployeeId { get; set; }
        
        public string EmployeeName { get; set; }

        public string AssetIssueDateTime { get; set; }

        public string AssetSubmitDateTime { get; set; }

        public string ExpiryDate { get; set; }

        public string LicesenceNo { get; set; }

        public string AssetType { get; set; }

        public bool IsSubmitted { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }
    }
}