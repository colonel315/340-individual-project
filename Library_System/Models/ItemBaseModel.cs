using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class ItemBaseModel
    {
        public int ID { get; set; }
        public string Year { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }

        public virtual ICollection<CheckOut> CheckOuts { get; set; }
    }
}