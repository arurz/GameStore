using GameStore.Contexts;
using GameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GameStore.Services
{
    public class UserService
    {
        #region HashGeneration
        public string GenerateHashString(string password, ref string Salt)
        {
            Random random = new Random();
            int MinSaltSize = 4;
            int MaxSaltSize = 8;
            
            int SaltSize = random.Next(MinSaltSize, MaxSaltSize);

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);
            Salt = Convert.ToBase64String(salt);
            
            var hash = Encoding.UTF8.GetBytes(password);
            var saltedHash = new byte[hash.Length + salt.Length];

            for (int i = 0; i < hash.Length; i++)
            {
                saltedHash[i] = hash[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                saltedHash[hash.Length + i] = salt[i];
            }

            HashAlgorithm algorithm = new SHA512Managed();
            byte[] hashPassword = algorithm.ComputeHash(saltedHash);
            var base64Hash = Convert.ToBase64String(hashPassword);

            return base64Hash;
        }

        public string GenerateHashString(string password, string Salt)
        {
            var salt = Encoding.UTF8.GetBytes(Salt);
            var hash = Encoding.UTF8.GetBytes(password);
            var saltedHash = new byte[hash.Length + salt.Length];

            for (int i = 0; i < hash.Length; i++)
            {
                saltedHash[i] = hash[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                saltedHash[hash.Length + i] = salt[i];
            }

            HashAlgorithm algorithm = new SHA512Managed();
            byte[] hashPassword = algorithm.ComputeHash(saltedHash);
            var base64Hash = Convert.ToBase64String(hashPassword);

            return base64Hash;
        }
        #endregion
        #region Register And Login Section
        public User CreateUser(int id,
                               string firstName,
                               string lastName,
                               string username,
                               string email,
                               string password,
                               string telephoneNumber,
                               int roleId)
        {
            string salt = "";
            var user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = email,
                Password = GenerateHashString(password, ref salt),
                Salt = salt,
                TelephoneNumber = telephoneNumber,
                RoleId = roleId
            };
            using (var context = new GameStoreDBContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            return user;
        }

        public User LogIn(string username, string password)
        {
            using (var context = new GameStoreDBContext())
            {
                User user = context.Users.SingleOrDefault(x => x.Username == username);
                var hashPassword = GenerateHashString(password, user.Salt);

                return user.Password == hashPassword ? user : throw new UnauthorizedAccessException();
            }
        }
        #endregion
        #region GetMethods
        public List<User> GetUsers()
        {
            using(var context = new GameStoreDBContext())
            {
                return context.Users.ToList();
            }
        }

        public User GetUserById(int id)
        {
            using(var context = new GameStoreDBContext())
            {
                return context.Users.SingleOrDefault(x => x.Id == id);
            }
        }
        #endregion
        #region UpdateMethods
        public void UpdateUser(int id, User newUser)
        {
            using(var context = new GameStoreDBContext())
            {
                var user = context.Users.SingleOrDefault(x => x.Id == id);
                user = newUser;

                context.SaveChanges();
            }
        }

        public void UpdateUsersPassword(int id, string password)
        {
            if (id < 1)
                return;
            using(var context = new GameStoreDBContext())
            {
                var user = context.Users.SingleOrDefault(x => x.Id == id);
                var newPassword = GenerateHashString(password, user.Salt);
                user.Password = newPassword;

                context.SaveChanges();
            }
        }
        #endregion
        #region RemoveMethods
        public void RemoveUserById(int id)
        {
            using(var context = new GameStoreDBContext())
            {
                var user = context.Users.SingleOrDefault(x => x.Id == id);

                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
