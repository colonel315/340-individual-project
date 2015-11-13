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
        public LibraryContext() : base("LibraryContext") {}

        // User Models
        public DbSet<UserBaseModel> UserBaseModels { get; set; }
        /*public DbSet<LibrarianModel> Librarians { get; set; }
        public DbSet<FacultyModel> Faculties { get; set; }
        public DbSet<StudentModel> Students { get; set; }*/

        // Containts checkout date
        public DbSet<CheckOutModel> CheckOuts { get; set; }

        // Item Models
        public DbSet<ItemBaseModel> ItemBaseModels { get; set; }
        /*public DbSet<BookModel> Books { get; set; }
        public DbSet<PeriodicalModel> Periodicals { get; set; }
        public DbSet<MagazineModel> Magazines { get; set; }
        public DbSet<CdModel> Cds { get; set; }*/

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<UserBaseModel>().HasMany(u => u.CheckOuts);
            modelBuilder.Entity<ItemBaseModel>().HasMany(i => i.CheckOuts);
        }
    }
}