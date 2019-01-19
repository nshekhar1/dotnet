using ReceptionProcam.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReceptionProcam.Areas.Area.Models
{
    public class EmployeeMasters
    {
        public int ID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Employee Designation")]
        public Nullable<int> EmpDesignationID { get; set; }

        [DisplayName("Employee Department")]
        public string EmpDept { get; set; }

        [DisplayName("Mobile No")]
        [Required(ErrorMessage = "* Please enter contact No")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "* Please enter max 10 Digit mobile No")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "* Please enter Valid Digit mobile No")]
        public string PhoneNo { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [DisplayName("Is Employee Active")]
        public bool IsActive { get; set; }
        public virtual tblEmpDesignationMaster tblEmpDesignationMaster { get; set; }


    }
}