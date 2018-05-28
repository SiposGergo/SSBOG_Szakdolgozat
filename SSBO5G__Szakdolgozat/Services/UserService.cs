using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Services
{
    public interface IUserService
    {
        Hiker Authenticate(string username, string password);
        Task<IEnumerable<Hiker>> GetAll();
        Task<Hiker> GetById(int id);
        Hiker Create(Hiker user, string password);
        void Update(Hiker user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private ApplicationContext context;

        public UserService(ApplicationContext context)
        {
            this.context = context;
        }

        public Hiker Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = context
                .Hikers
                .Include(x => x.Registrations)
                .SingleOrDefault(x => x.UserName == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public async Task <IEnumerable<Hiker>> GetAll()
        {
            var hikers = await context.Hikers.ToListAsync();
            return hikers;
        }

        public async Task <Hiker> GetById(int id)
        {
            var user = await context
                .Hikers
                .Include(x => x.Registrations)
                .ThenInclude(y => y.HikeCourse)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new ApplicationException("Nem található túrázó ezzel az azonosítóval.");
            }
            return user;
        }

        public Hiker Create(Hiker user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Jelszó megadása szükséges!");

            if (context.Hikers.Any(x => x.UserName == user.UserName))
                throw new ApplicationException("A '" + user.UserName + "' felhasználónév már foglalt.");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.Hikers.Add(user);
            context.SaveChanges();

            return user;
        }

        public void Update(Hiker userParam, string password = null)
        {
            var user = context.Hikers.Find(userParam.Id);

            if (user == null)
                throw new ApplicationException("Felhasználó nem található");

            if (userParam.UserName != user.UserName)
            {
                // username has changed so check if the new username is already taken
                if (context.Hikers.Any(x => x.UserName == userParam.UserName))
                    throw new ApplicationException("A " + userParam.UserName + " felhasználónév már foglalt");
            }

            // update user properties
            user.Email = userParam.Email;
            user.Name = userParam.Name;
            user.PhoneNumber = userParam.PhoneNumber;
            user.Town = userParam.Town;
            user.UserName = userParam.UserName;
            user.DateOfBirth = userParam.DateOfBirth;
            user.Gender = userParam.Gender;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            context.Hikers.Update(user);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = context.Hikers.Find(id);
            if (user != null)
            {
                context.Hikers.Remove(user);
                context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
