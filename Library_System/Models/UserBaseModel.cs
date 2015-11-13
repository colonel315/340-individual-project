using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class UserBaseModel
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<CheckOut> CheckOuts { get; set; }
    }
}