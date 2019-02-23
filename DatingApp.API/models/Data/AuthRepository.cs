using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace DatingApp.API.models.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string userName, string password)
        {
            var user =  _context.Users.FirstOrDefault(x=> x.UserName == userName);

            if(user == null)
              return await Task.FromResult(new User());

            if(!VerifyPassowrd(password,user.PasswordHash,user.PasswordSalt))
              return await Task.FromResult(new User());;

              return await Task.FromResult(user);
        }

        private bool VerifyPassowrd(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                
                for(var i=0;i < computedHash.Length; i++){

                    if(computedHash[i] != passwordHash[i]) {
                        return false;
                    }
                    
                }

            }

            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash,passwordSalt;

            CreatePasswordHash(password,out passwordHash,out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string userName)
        {
           if( _context.Users.Any(x=> x.UserName == userName))
                return await Task.FromResult(true);

               return await Task.FromResult(false); 
        }
    }
}