using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MatHackWebApiTemplate.Models
{
    public class WishList
    {
        [Key]
        public int UserId { get; set; }

        public int MessageId { get; set; }


    }
}