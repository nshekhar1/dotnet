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
    
    public partial class tblAssetType
    {
        public tblAssetType()
        {
            this.tblAssetDetails = new HashSet<tblAssetDetail>();
        }
    
        public int ID { get; set; }
        public string AssetTypeCode { get; set; }
        public string AssetTypeName { get; set; }
    
        public virtual ICollection<tblAssetDetail> tblAssetDetails { get; set; }
    }
}
