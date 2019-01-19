using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ReceptionProcam.Areas.Area.Models
{
    public class IdentityMasters
    {
        public int Id { get; set; }

        [DisplayName("Govt Id Name")]
        public string ProofName { get; set; }

        [DisplayName("Govt Id Code")]
        public string ProofCode { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}