using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class LiteratureBase : PaperBase
    {
        public string Version { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }
    }
}