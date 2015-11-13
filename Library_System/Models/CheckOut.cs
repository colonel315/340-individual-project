using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_System.Models
{
    public class CheckOut
    {
        DateTime CheckoutDate { get; set; }

        public virtual UserBaseModel UserBaseModel { get; set; }
        public virtual ItemBaseModel Items { get; set; }
    }
}