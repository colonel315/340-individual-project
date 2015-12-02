using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class ItemBase
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Year")]
        public string Year { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public int Amount { get; set; }

        public ICollection<CheckOut> CheckOuts { get; set; }
    }
}