using System.Security.Cryptography;
using System.Text;

namespace Entidades.Views
{
    public class EncriptarPassword
    {
        public static string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < 20; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }
    }
}