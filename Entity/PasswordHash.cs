using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Entity
{
    /// <summary>
    /// class used to hash passwords
    /// </summary>
    public static class PasswordHash
    {
        static Random rnd = new Random();

        /// <summary>
        /// contains the hash and the salt
        /// </summary>
        public struct PassAndSalt
        {
            internal PassAndSalt(byte[] salt, byte[] hash)
            {
                Salt = Convert.ToBase64String(salt);
                Hash = Convert.ToBase64String(hash);
            }
            public string Salt { get; set; }
            public string Hash { get; set; }
        }

        /// <summary>
        /// create hash and salt from password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PassAndSalt Create(string password)
        {
            
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password); //turn the password into byte array
            byte[] salt = new byte[rnd.Next(10, 20)]; //create random size byte array
            new RNGCryptoServiceProvider().GetBytes(salt); //fill it with secure random numbers

            byte[] hash = HashPassword(salt, passwordBytes); // create hash from salt and password bytes
            return new PassAndSalt(salt, hash);
        }

        //combine the random salt and the password hash
        static byte[] HashPassword(byte[] salt, byte[] password)
        {
            byte[] hash = new byte[salt.Length + password.Length];
            for (int i = 0; i < salt.Length; i++)
            {
                hash[i] = salt[i];
            }
            for (int i = 0; i < password.Length; i++)
            {
                hash[i + salt.Length] = password[i];
            }
            SHA256Managed sha256 = new SHA256Managed();
            return sha256.ComputeHash(hash);
        }

        //take string password and string salt and return hash
        static byte[] HashPassword(string salt, string password)
        {
            byte[] saltB = Convert.FromBase64String(salt);
            byte[] passwordB = Encoding.UTF8.GetBytes(password);
            return HashPassword(saltB, passwordB);
        }

        /// <summary>
        /// take the salt the hash and the password
        /// and compare if they match
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool Verify(string password, string salt,string hash)
        {
            byte[] inputHash = HashPassword(salt, password);
            byte[] _hash = Convert.FromBase64String(hash);
            return ComareHash(_hash, inputHash);
        }

        //compare 2 hashes
        static bool ComareHash(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
                return false;
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                    return false;
            }
            return true;
        }

    }
}