using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class LibrarianModel : UserBaseModel
    {
        [Required]
        [Display(Name = "Employee Number")]
        [RegularExpression("([0-9]{9})", ErrorMessage = "Please enter your 9 digit employee number.")]
        public string EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}