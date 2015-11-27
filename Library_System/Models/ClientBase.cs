using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class ClientBase : UserBase
    {
        [Required]
        [RegularExpression("([0-9]){9}", ErrorMessage = "A student ID has 9 digits.")]
        [Display(Name = "Client ID")]
        public string ClientId { get; set; }
    }
}