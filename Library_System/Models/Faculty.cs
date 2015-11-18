using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class Faculty : UserBase
    {
        [Required]
        [RegularExpression("([0-9]){9}", ErrorMessage = "A student ID has 9 digits.")]
        [Display(Name = "Faculty ID")]
        public string FacultyId { get; set; }
    }
}