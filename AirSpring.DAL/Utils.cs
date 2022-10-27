using System.Text;
namespace AirSpring.DAL
{
    public static class Utils
    {
        // Encrypt password
        public static string Encrypt(this string _stringToEncrypt)
        {
            string result = string.Empty;
            byte[] encrypted = Encoding.Unicode.GetBytes(_stringToEncrypt);
            result = Convert.ToBase64String(encrypted);
            return result;
        }
    }
}
