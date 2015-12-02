using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_System.Models
{
    [Table("CheckOut")]
    public class CheckOut
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserBase Users { get; set; }

        [Required]
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public ItemBase ItemBase { get; set; }

        [Display(Name = "Reserved?")]
        public bool IsReserve { get; set; }

        public DateTime CheckoutDate { get; set; }

        public int lateFee()
        {
            TimeSpan checkOutTime = DateTime.Today.Subtract(this.CheckoutDate);

            if (checkOutTime.Days > 0)
            {
                return checkOutTime.Days;
            }
            else
            {
                return 0;
            }
        }

        public bool canCheckout(UserBase user, ItemBase item)
        {
            if (item.GetType().IsEquivalentTo(typeof (Magazine)))
            {
                return false;
            }
            else if (item.GetType().IsEquivalentTo(typeof (Periodical)) &&
                     user.GetType().IsEquivalentTo(typeof (Student)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string getCheckoutDate()
        {
            return this.CheckoutDate.ToString("MM/dd/yy");
        }
    }
}