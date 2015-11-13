using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_System.Models
{
    [Table("CheckOut")]
    public class CheckOutModel
    {
        public int Id { get; set; }

        [ForeignKey("UserBaseModel")]
        public int UserId;

        [ForeignKey("ItemBaseModel")]
        public int ItemId;

        DateTime CheckoutDate { get; set; }

        /*public virtual UserBaseModel UserBaseModel { get; set; }
        public virtual ItemBaseModel Items { get; set; }*/
    }
}