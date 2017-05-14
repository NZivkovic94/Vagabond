using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MatHackWebApiTemplate.Models
{
    public class Korisnik
    {
        [Key]
        public int UserId { get; set; }

        public String Username { get; set; }

        public String Interest1 { get; set; }

        public String Interest2 { get; set; }

        public String Interest3 { get; set; }

        public String Interest4 { get; set; }

        public String Interest5 { get; set; }

    }
}