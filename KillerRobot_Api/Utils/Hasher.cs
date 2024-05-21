using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace KillerRobot_Api.Utils
{
    public static class Hasher
    {
        public static byte[] SHA256Hashing(byte[] data)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                return hasher.ComputeHash(data);
            }
        }
        public static string SHA256Hashing(string dataString)
        {
            byte[] hashedData = SHA256Hashing(System.Text.Encoding.Unicode.GetBytes(dataString));
            return ByteToHex(hashedData);
        }
        public static string ByteToHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder();
            foreach(byte b in bytes)
            {
                hex.Append(b.ToString("x2"));
            }
            return hex.ToString();
        }
    }
}
