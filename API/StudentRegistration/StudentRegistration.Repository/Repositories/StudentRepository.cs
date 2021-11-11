using StudentRegistration.Domain;
using System.Collections.Generic;
using System.Linq;

namespace StudentRegistration.Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private ConnectionEf _Db;

        public StudentRepository(ConnectionEf connectionEf)
        {
            _Db = connectionEf;
        }

        public bool Delete(Student student)
        {
            if (student == null)
                throw new System.ArgumentNullException();

            _Db.Students.Remove(student);
            _Db.SaveChanges();
            return true;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _Db.Students.AsQueryable().ToArray();
        }

        public Student InsertStudent(Student student)
        {
            if (student == null)
                throw new System.ArgumentNullException();

            _Db.Students.Add(student);
            _Db.SaveChanges();

            return student;
        }

        public Student Update(Student student)
        {
            if (student == null)
                throw new System.ArgumentNullException();

            _Db.Students.Update(student);
            _Db.SaveChanges();

            return student;
        }
    }
}
