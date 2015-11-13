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
            var librarian = new List<Librarian>
            {
                new Librarian
                {
                    FirstName = "Fred",
                    LastName = "Burr",
                    EmployeeId = "901506977",
                    Password = "foo12"
                }
            };

            librarian.ForEach(l => libraryContext.Librarians.Add(l));
            libraryContext.SaveChanges();

            var student = new List<Student>
            {
                new Student
                {
                    FirstName = "Billy",
                    LastName = "Joel",
                    StudentId = "901111111"
                },
                new Student {FirstName = "Thomas", LastName = "Brand", StudentId = "901526289" },
                new Student {FirstName = "Tony", LastName = "Stark", StudentId = "901038592" },
                new Student
                {
                    FirstName = "Thor",
                    LastName = "Odinson",
                    StudentId = "901837692"
                }
            };

            student.ForEach(s => libraryContext.Students.Add(s));
            libraryContext.SaveChanges();

            var faculty = new List<Faculty>
            {
                new Faculty
                {
                    FirstName = "Kuangnan",
                    LastName = "Chang",
                    FacultyId = "901428584"
                },
                new Faculty
                {
                    FirstName = "Kirk",
                    LastName = "Jones",
                    FacultyId = "901582795"
                },
                new Faculty
                {
                    FirstName = "John",
                    LastName = "Gaffney",
                    FacultyId = "901582957"
                }
            };

            faculty.ForEach(f => libraryContext.Faculties.Add(f));
            libraryContext.SaveChanges();
        }
    }
}