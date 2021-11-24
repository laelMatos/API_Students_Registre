using StudentRegistration.Domain;
using System;
using System.Collections.Generic;


namespace StudentRegistration.Service
{

    public class UserService : IUserService
    {
        private readonly IUserRepository USER_REPOSITORY;
        public UserService(IUserRepository userRepository)
        {
            USER_REPOSITORY = userRepository;
        }

        
        public bool ExistEmail(string email)
        {
            return  USER_REPOSITORY.ExistEmail(email);
        }

        public User GetByEmail(string email)
        {
            return USER_REPOSITORY.GetByEmail(email);
        }

        public User GetById(int id)
        {
            return USER_REPOSITORY.GetById(id);
        }

        public User Insert(User user)
        {
            var exist = ExistEmail(user.Email);
            user.ExistEmail(exist);

            if (!user.NOTIFICATION.Success)
            {
                user.NOTIFICATION.HttpStatusCode = EHttpResponseCode.BadRequest;
                return user;
            }

            return USER_REPOSITORY.Insert(user);
        }

        public User Update(User user)
        {
            user.Validate();

            if (!user.NOTIFICATION.Success)
            {
                user.NOTIFICATION.HttpStatusCode = EHttpResponseCode.BadRequest;
                return user;
            }

            user.SetUpdate();

            return USER_REPOSITORY.Update(user);
        }

    }
}
