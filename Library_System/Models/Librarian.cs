using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class Librarian : UserBaseModel
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }
    }
}