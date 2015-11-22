using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Library_System.Models;

namespace Library_System.DAL
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class LibraryContext : DbContext
    {

        // User Models
        public virtual DbSet<UserBase> UserBases { get; set; }

        // Containts checkout date
        public virtual DbSet<CheckOut> CheckOuts { get; set; }

        // Item Models
        public virtual DbSet<ItemBase> ItemBases { get; set; }

        public LibraryContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<UserBase>().HasMany(u => u.CheckOuts).WithRequired().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ItemBase>().HasMany(i => i.CheckOuts).WithRequired().HasForeignKey(c => c.ItemId);
        }
    }
}