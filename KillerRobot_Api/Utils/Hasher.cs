using System.Security.Cryptography;

namespace KillerRobot_Api.Utils
{
    public static class Hasher
    {
        public static string SHA256Hashing(this byte[] data)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                
            }
        }
    }
}
