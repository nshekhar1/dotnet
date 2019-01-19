using ReceptionProcam.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ReceptionProcam.Areas.Area.Models
{
    public class AssetMasters
    {
        public int ID { get; set; }
        [DisplayName("Asset Name")]
        public string AssetModelName { get; set; }
        [DisplayName("Asset Type")]
        public int AssetTypeID { get; set; }

        [DisplayName("Asset Company")]
        public int AssetCompanyID { get; set; }

        [DisplayName("Asset Manufacturing Date")]
        public DateTime? ManufacturingDate { get; set; }

        [DisplayName("Software Expiry Date")]
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        [DisplayName("Is Asset issued?")]
        public bool IsIssued { get; set; }
        [DisplayName("Software Licesence No / Product Key")]
        public string LicesenceNo { get; set; }

        public virtual tblAssetType tblAssetType { get; set; }
        public virtual tblAssetCompany tblAssetCompany { get; set; }

    }
}