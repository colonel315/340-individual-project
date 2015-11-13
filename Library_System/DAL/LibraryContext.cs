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
        public LibraryContext() : base("DefaultConnection"){}

        // User Models
        public DbSet<UserBaseModel> UserBaseModels { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Student> Students { get; set; }

        // Many to Many Relation
        public DbSet<CheckOut> CheckOuts { get; set; }

        // Item Models
        public DbSet<ItemBaseModel> ItemBaseModels { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Periodical> Periodicals { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<CD> Cds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<UserBaseModel>().HasMany(u => u.CheckOuts);
            modelBuilder.Entity<ItemBaseModel>().HasMany(i => i.CheckOuts);
        }
    }
}