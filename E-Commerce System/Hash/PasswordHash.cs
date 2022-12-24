using System.Security.Cryptography;
using System.Text;

namespace E_Commerce_System.Hash
{
    public class PasswordHash : IHashPassword
    {
        public void createHashPassword(string password, out byte[] passswordHash, out byte[] passwordSlot)
        {
            using (var _hash = new HMACSHA512())
            {
                
                passswordHash = _hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordSlot = _hash.Key;
            }
        }

        public bool verifyPassword(string password, byte[] passwordHash, byte[] passwordSlot)
        {
            using (var _hash = new HMACSHA512(passwordSlot))
            {
                var Hashpass = _hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                return Hashpass.SequenceEqual(passwordHash);
            }
        }
    }
}
