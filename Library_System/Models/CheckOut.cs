using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_System.Models
{
    [Table("CheckOut")]
    public class CheckOut
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserBase Users { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public ItemBase ItemBase { get; set; }

        DateTime CheckoutDate { get; set; }
    }
}