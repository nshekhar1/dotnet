using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ReceptionProcam.Models
{
    public class clsVisitor
    {
        public clsVisitor()
        {
            TimeIn = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm");
        }


        public int Id { get; set; }

        [DisplayName("Visitor Number")]
        public string VisitorId { get; set; }

        [DisplayName("Employee Id")]
        public string  EmpId { get; set; }

        [Required(ErrorMessage = "* Please enter Name")]
        [DisplayName("Name")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "* Use letters only please")]
        public string Name { get; set; }

        [DisplayName("Date of Birth")]
        //[Required(ErrorMessage = "Please enter Date of Birth")]
        public string DOB { get; set; }

        [DisplayName("Identification Proof")]
       // [Required(ErrorMessage = "* Please select any identity proof")]
        public string GovId { get; set; }

        [DisplayName("Identification Proof No")]
        [Required(ErrorMessage = "* Please insert any identity proof No")]
        public string GovIdNo { get; set; }

        [DisplayName("Contact No")]
        [Required(ErrorMessage = "* Please enter contact No")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "* Please enter max 10 Digit mobile No")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "* Please enter Valid Digit mobile No")]
        public string MobileNo { get; set; }


        //[DataType(DataType.EmailAddress, ErrorMessage = "* Please write valid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "* Please enter valid Email Address")]
        public string Email { get; set; }

        [DisplayName("Asset ID")]
        public string AssetId { get; set; }

        [DisplayName("Location")]
        [Required(ErrorMessage = "* Please from details")]
        public string Location { get; set; }

        [Required(ErrorMessage = "* Please enter whom to meet")]
        [DisplayName("Meet")]
        public string ToMeet { get; set; }


        [DisplayName("Office Sub Location")]
        public string SubLocation { get; set; }


        [DisplayName("NCS Office Location ")]
        public string OfficeLocation { get; set; }

        [Required(ErrorMessage = "* Please enter Gate No")]
        public string Gate { get; set; }

        [Required(ErrorMessage = "* Please enter porpose to visit")]
        [DisplayName("Purpose")]
        public string Purpose { get; set; }

        [Required(ErrorMessage = "* Please enter In Time")]
        [DisplayName("Time In")]
        public string TimeIn { get; set; }


        [DisplayName("Valid Upto")]
        [Required(ErrorMessage = "* Please enter out time")] 
        public string ValidUpto { get; set; }

        public string PurposeText { get; set; } 

        [DisplayName("Remarks")]
        public string Remark { get; set; }
        public string ImagePath { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public bool IsPassReturned { get; set; }

    }

    public class MailModel
    {
        public string From
        {
            get;
            set;
        }
        public string To
        {
            get;
            set;
        }
        public string Subject
        {
            get;
            set;
        }
        public string Body
        {
            get;
            set;
        }
    }
}