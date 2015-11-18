using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_System.Models
{
    public class Periodical : ItemBase
    {
        public string Version { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}