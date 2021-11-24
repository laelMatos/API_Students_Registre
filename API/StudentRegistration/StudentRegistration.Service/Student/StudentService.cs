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
                    return new Student(EHttpResponseCode.NotFound,
                        "Aluno existente",
                        "O RA informado não foi cadastrado",
                        "RA");
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
                    return new Student(EHttpResponseCode.NotFound, 
                        "Aluno não encontrado", 
                        "O RA informado não foi cadastrado", 
                        "RA");
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

        public Notification Delete(string ra)
        {
            try
            {
                Student studentDB = Repository.GetByRA(ra);
                if (studentDB == null)
                {
                    return new Notification
                    {
                        HttpStatusCode = EHttpResponseCode.NotFound,
                        Title = "Aluno não encontrado",
                        Messages = new List<Messages> { new Messages {
                            Message = "O RA informado não foi cadastrado",
                            ErrorField = "RA", }}
                    };
                }

                Repository.Delete(ra);

                return new Notification
                {
                    HttpStatusCode = EHttpResponseCode.OK,
                    Title = "Remover aluno",
                    Messages = new List<Messages> { new Messages {
                            Message = "Aluno removido com sucesso",
                            ErrorField = "RA", }}
                };
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
                var studentDb =  Repository.GetByRA(ra);

                if (studentDb == null)
                {
                    return new Student(EHttpResponseCode.NotFound,
                        "Aluno não encontrado",
                        "O RA informado não foi cadastrado",
                        "RA");
                }

                return studentDb;
            }
            catch (Exception)
            {
                throw new Exception($"Falha ao buscar aluno com o código : {ra}");
            }
        }
    }
}
