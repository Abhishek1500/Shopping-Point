using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace UserMicroService.Helper
{
    public static class HashingHelper
    {
        public static string hashPassword(this string password, byte[] hashkey)
        {
            //128 bit salt and telling the bytes
                return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: hashkey,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100000,
                //thismuchbyte hashed will be provaided
                numBytesRequested: 256 / 8));

        }

        public static bool isHashOf(this string hashString,string newString, byte[] hashkey)
        {
            //128 bit salt and telling the bytes
            return hashString== Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: newString,
            salt: hashkey,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100000,
            //thismuchbyte hashed will be provaided
            numBytesRequested: 256 / 8));

        }
    }
}
