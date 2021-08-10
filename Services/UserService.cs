using GameStore.Contexts;
using GameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services
{
    public class UserService
    {
        private readonly GameStoreDBContext context;
        public UserService(GameStoreDBContext context)
        {
            this.context = context;
        }
        #region Register And Login Section
        public User CreateUser(User user)
        {
            if (user.Password != user.RepeatPassword)
            {
                throw new Exception("Password mismatch");
            }

            if (!IsEmailUnique(user))
            {
                throw new Exception("This email is already taken");
            }

            if (!IsUsernameUnique(user))
            {
                throw new Exception("This username is already taken");
            }

            string salt = "";
            string newPassword = HashService.GenerateHashString(user.Password, ref salt);

            user.Password = newPassword;
            user.Salt = salt;

            
            context.Users.Add(user);
            context.SaveChanges();
            
            return user;
        }

        public User LogIn(string username, string password)
        {
            User user = context.Users.SingleOrDefault(x => x.Username == username);
            var hashPassword = HashService.GenerateHashString(password, user.Salt);

            return user.Password == hashPassword ? user : throw new UnauthorizedAccessException();
        }

        private bool IsEmailUnique(User User)
        {
            
            var users = context.Users.ToList();
            foreach(var user in users)
            {
                if (user.Email == User.Email)
                    return false;
            }
            
            return true;
        }

        private bool IsUsernameUnique(User User)
        {
            
            var users = context.Users.ToList();
            foreach (var user in users)
            {
                if (user.Username == User.Username)
                    return false;
            }
            
            return true;
        }
        #endregion
        #region GetMethods
        public List<User> GetUsers() => context.Users.ToList();

        public User GetUserById(int id) => context.Users.SingleOrDefault(x => x.Id == id);
       
        #endregion
        #region UpdateMethods
        public void UpdateUser(int id, User newUser)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == id);
            user = newUser;

            context.SaveChanges();
        }

        public void UpdateUsersPassword(int id, string password)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == id);
            var newPassword = HashService.GenerateHashString(password, user.Salt);
            user.Password = newPassword;

            context.SaveChanges();
        }
        #endregion
        #region RemoveMethods
        public void RemoveUserById(int id)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == id);

            context.Users.Remove(user);
            context.SaveChanges();
        }
        #endregion
    }
}
