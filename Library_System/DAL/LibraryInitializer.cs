using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library_System.Models;

namespace Library_System.DAL
{
    public class LibraryInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        protected override void Seed(LibraryContext libraryContext)
        {
            var librarian = new List<LibrarianModel>
            {
                new LibrarianModel
                {
                    FirstName = "Fred",
                    LastName = "Burr",
                    EmployeeId = "901506977",
                    Password = "foo12"
                }
            };

            librarian.ForEach(l => libraryContext.UserBaseModels.Add(l));
            libraryContext.SaveChanges();

            var student = new List<StudentModel>
            {
                new StudentModel
                {
                    FirstName = "Billy",
                    LastName = "Joel",
                    StudentId = "901111111"
                },
                new StudentModel
                {
                    FirstName = "Thomas",
                    LastName = "Brand",
                    StudentId = "901526289"
                },
                new StudentModel
                {
                    FirstName = "Tony",
                    LastName = "Stark",
                    StudentId = "901038592"
                },
                new StudentModel
                {
                    FirstName = "Thor",
                    LastName = "Odinson",
                    StudentId = "901837692"
                }
            };

            student.ForEach(s => libraryContext.UserBaseModels.Add(s));
            libraryContext.SaveChanges();

            var faculty = new List<FacultyModel>
            {
                new FacultyModel
                {
                    FirstName = "Kuangnan",
                    LastName = "Chang",
                    FacultyId = "901428584"
                },
                new FacultyModel
                {
                    FirstName = "Kirk",
                    LastName = "Jones",
                    FacultyId = "901582795"
                },
                new FacultyModel
                {
                    FirstName = "John",
                    LastName = "Gaffney",
                    FacultyId = "901582957"
                }
            };

            faculty.ForEach(f => libraryContext.UserBaseModels.Add(f));
            libraryContext.SaveChanges();
        }
    }
}