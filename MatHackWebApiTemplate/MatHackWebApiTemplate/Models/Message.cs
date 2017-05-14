using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MatHackWebApiTemplate.Models
{

    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        
        public String MessageText { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public String messageCategory { get; set; }

        public int LikesNum { get; set; }

        public int DislikeNum { get; set; }

        public DateTime CreateTime { get; set; }
    }
}