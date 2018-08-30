using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Dtos;
using System.Text;

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
        Task ChangePassword(int userId, ChangePasswordDto dto);
        Task ForgottenPassword(ForgottenPasswordDto dto);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationContext context;
        private readonly IEmailSender emailSender;

        public UserService(ApplicationContext context, IEmailSender emailSender)
        {
            this.context = context;
            this.emailSender = emailSender;
        }

        public Hiker Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = context
                .Hikers
                .Include(x => x.Registrations)
                .SingleOrDefault(x => x.UserName == username);
            
            if (user == null)
                return null;
            
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

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
                .Include(x=> x.OrganizedHikes)
                .ThenInclude(y=>y.Courses)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new NotFoundException("felhasználó");
            }
            return user;
        }

        public Hiker Create(Hiker user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Jelszó megadása szükséges!");

            if (context.Hikers.Any(x => x.UserName == user.UserName))
                throw new ApplicationException("A '" + user.UserName + "' felhasználónév már foglalt.");

            CheckPasswordLength(password, 6);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

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
                throw new NotFoundException("felhasználó");

            if (userParam.UserName != user.UserName)
            {
                if (context.Hikers.Any(x => x.UserName == userParam.UserName))
                    throw new ApplicationException("A " + userParam.UserName + " felhasználónév már foglalt");
            }
            
            user.Email = userParam.Email;
            user.Name = userParam.Name;
            user.PhoneNumber = userParam.PhoneNumber;
            user.Town = userParam.Town;
            user.UserName = userParam.UserName;
            user.DateOfBirth = new DateTime(userParam.DateOfBirth.Year, userParam.DateOfBirth.Month, userParam.DateOfBirth.Day);
            user.Gender = userParam.Gender;
            
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new ApplicationException("Nem megfelelő jelszó!");
            }

            context.Hikers.Update(user);
            context.SaveChanges();
        }

        public async Task ChangePassword(int userId, ChangePasswordDto dto)
        {
            Hiker hiker = await context.Hikers.FindAsync(userId);
            if (hiker == null)
            {
                throw new NotFoundException("túrázó");
            }
            if (dto == null || String.IsNullOrWhiteSpace(dto.CurrentPassword) || String.IsNullOrWhiteSpace(dto.NewPassword))
            {
                throw new ApplicationException("Helytelenül megadott adatok");
            }
            if (!VerifyPasswordHash(dto.CurrentPassword, hiker.PasswordHash, hiker.PasswordSalt))
            {
                throw new ApplicationException("Nem megfelő jelszó!");
            }
            CheckPasswordLength(dto.NewPassword, 6);
            CreatePasswordHash(dto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            hiker.PasswordHash = passwordHash;
            hiker.PasswordSalt = passwordSalt;
            hiker.mustChangePassword = false;
            await context.SaveChangesAsync();
        }

        public async Task ForgottenPassword(ForgottenPasswordDto dto)
        {
            Hiker hiker = await context.Hikers.SingleOrDefaultAsync(x => x.UserName == dto.UserName);
            if (String.IsNullOrWhiteSpace(dto.Email)|| String.IsNullOrWhiteSpace(dto.UserName))
            {
                throw new ApplicationException("Nem megfelelően megadott adatok");
            }
            if (hiker == null || hiker.Email != dto.Email)
            {
                throw new ApplicationException("Nem található ilyen adatokkal túrázó!");
            }
            string password = CreteRandomPassword(10);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            hiker.PasswordHash = passwordHash;
            hiker.PasswordSalt = passwordSalt;
            hiker.mustChangePassword = true;

            string emailText = $"Kedves {hiker.Name}, a HikeX rendszerben a jelszavad megváltoztatásást kérted.\n" +
                $"Az új jelszavad: {password}.\n Biztonsági okokból az első belépést követően meg kell változattnod ezt a jelszót!";

            await emailSender.SendEmail(hiker.Email,emailText,"Elfelejtett jelszó!");
            await context.SaveChangesAsync();
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

        private static string CreteRandomPassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Az érték nem lehet üres vagy egyetlen szóköz.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private void CheckPasswordLength(string passWord, int length)
        {
            if (passWord.Length < length)
            {
                throw new ApplicationException($"A minmum jelszó hossz {length} karakter!");
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Az érték nem lehet üres vagy egyetlen szóköz.", "password");
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
