using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MatHackWebApiTemplate.Models
{
    public class Like
    {
        
        public int MessageId { get; set; }

        public bool Liked { get; set; }

        public bool Disliked { get; set; }

        [Key]
        public int UserId { get; set; }
    }
}