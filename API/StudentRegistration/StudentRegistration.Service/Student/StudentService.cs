using StudentRegistration.Domain;
using System;
using System.Collections.Generic;

namespace StudentRegistration.Service
{
    public class StudentService : IStudentService
    {
        IStudentRepository Repository;
        public StudentService(IStudentRepository studentRepository)
        {
            Repository = studentRepository;
        }

        public Student Add(Student student)
        {
            try
            {
                if (!student.NOTIFICATION.Success)
                {
                    student.NOTIFICATION.HttpStatusCode = EHttpResponseCode.BadRequest;
                    return student;
                }

                var studentDb = Repository.GetByRA(student.RA);

                if(studentDb == null)
                    return Repository.Add(student);
                else
                {
                    studentDb = new Student("", "", "", "");

                    studentDb.NOTIFICATION = new Notification()
                    {
                        Title = "Aluno existente",
                        HttpStatusCode = EHttpResponseCode.BadRequest
                    };
                    studentDb.NOTIFICATION.Messages = new List<Messages>() { new Messages {
                        Message="O RA informado ja foi cadastrado",
                        ErrorField="RA"
                    } };

                    return studentDb;
                }
            }
            catch (Exception)
            {
                throw new Exception("Falha ao adicionar");
            }
        }

        public IEnumerable<Student> GetAll()
        {
            try
            {
                return Repository.GetAll();
            }
            catch (Exception)
            {
                throw new Exception("Falha ao buscar alunos");
            }
        }

        public Student Update(string ra, string email, string name)
        {
            try
            {
                Student studentDB = Repository.GetByRA(ra);
                if (studentDB == null)
                {
                    studentDB = new Student("", "", "", "");

                    studentDB.NOTIFICATION = new Notification() { 
                        Title = "Aluno não encontrado", 
                        HttpStatusCode= EHttpResponseCode.NotFound};
                    studentDB.NOTIFICATION.Messages = new List<Messages>() { new Messages { 
                        Message="O RA informado não foi cadastrado",
                        ErrorField="RA"
                    } };

                    return studentDB;
                }

                studentDB.Update(name, email);

                if (!studentDB.NOTIFICATION.Success)
                    return studentDB;

                return Repository.Update(studentDB);
            }
            catch (Exception)
            {
                throw new Exception($"Falha ao atualizar aluno com o código : {ra}");
            }
           
        }

        public bool Delete(string ra)
        {
            try
            {
                Student studentDB = Repository.GetByRA(ra);
                if (studentDB == null)
                {
                    return false;
                }

                    return Repository.Delete(studentDB);
            }
            catch (Exception)
            {
                throw new Exception($"Falha ao remover aluno com o código : {ra}");
            }
        }

        public Student GetByRA(string ra)
        {
            try
            {
                return Repository.GetByRA(ra);
            }
            catch (Exception)
            {
                throw new Exception($"Falha ao buscar aluno com o código : {ra}");
            }
        }
    }
}
