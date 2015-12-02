using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class Cd : ItemBase
    {
        [Required]
        [Display(Name = "Artist")]
        public string Artist { get; set; }

        public string Director { get; set; }
    }
}