using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MatHackWebApiTemplate.Models
{
    public class MatHackWebApiTemplateContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MatHackWebApiTemplateContext() : base("name=DefaultConnection")
        {
             
        }

        public System.Data.Entity.DbSet<MatHackWebApiTemplate.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<MatHackWebApiTemplate.Models.Like> Likes { get; set; }

        public System.Data.Entity.DbSet<MatHackWebApiTemplate.Models.Korisnik> Korisniks { get; set; }

        public System.Data.Entity.DbSet<MatHackWebApiTemplate.Models.WishList> WishLists { get; set; }
    }
}
