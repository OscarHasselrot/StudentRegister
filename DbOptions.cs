using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegister
{
    public class DbOptions
    {
        StudentDbContext dbCtx;
        public DbOptions(StudentDbContext db)
        {
            this.dbCtx = db;
        }

        public void AddStudent(string firstName, string lastName, string city)
        {
            var student = new Student(firstName, lastName, city);
            dbCtx.Add(student);
            dbCtx.SaveChanges();
        }
        public Student ChangeStudent(int id)
        {
            var student = dbCtx.Students.Where(s => s.StudentId == id).FirstOrDefault<Student>();
            return student;
        }
        public void ChangeFirstName(Student student, string firstName)
        {
            student.FirstName = firstName;
            dbCtx.SaveChanges();
        }
        public void ChangeLastName(Student student, string lastName)
        {
            student.LastName = lastName;
            dbCtx.SaveChanges();
        }
        public void ChangeCity(Student student, string city)
        {
            student.City = city;
            dbCtx.SaveChanges();
        }
    }
}
