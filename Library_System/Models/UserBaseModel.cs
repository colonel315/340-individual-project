using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class UserBaseModel
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public ICollection<CheckOutModel> CheckOuts { get; set; }
    }
}