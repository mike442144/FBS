using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace FBS.Utils
{
    public class AuthenticationHelper
    {
        //获取票据
        public static FormsAuthenticationTicket CreateAuthenticationTicket(string userName, string userData,bool isPersist)
        {
            HttpContext context = HttpContext.Current;

            AuthenticationSection config = (AuthenticationSection)context.GetSection("system.web/authentication");
            int timeout = (int)config.Forms.Timeout.TotalMinutes;
            if (null==userData)
                userData = string.Empty;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(timeout), isPersist, userData, FormsAuthentication.FormsCookiePath);
            
            return ticket;
        }

        //设置cookie
        public static void SetAuthenticalCookie(FormsAuthenticationTicket ticket)
        {
            HttpContext context = HttpContext.Current;

            string authcookie = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);

            cookie.Value = authcookie;
            string name = cookie.Name;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Expires = DateTime.Now.AddDays(7);
            cookie.HttpOnly = true;

            //if (!context.Request.IsSecureConnection && FormsAuthentication.RequireSSL)
            //throw new HttpException("Ticket requires SSL");

            context.Response.Cookies.Add(cookie);

        }

        public static void SetQueryStringRedirect(FormsAuthenticationTicket ticket, string url)
        {
            //获取当前请求上下文
            HttpContext context = HttpContext.Current;
            //if (!context.Request.IsSecureConnection && FormsAuthentication.RequireSSL)
            //throw new HttpException("Ticket requires SSL");

            string encTicket = FormsAuthentication.Encrypt(ticket);
            context.Response.Redirect(String.Format("{0}?{1}={2}", url, FormsAuthentication.FormsCookieName, encTicket));
        }
    }

    public class HashHelper
    {
        private const int iterations = 2;
        private const int salt_size = 32;
        private const int hash_size = 32;


        public static bool ValidatePassword(string password, byte[] storedSalt, byte[] storedHash)
        {
            Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(password, storedSalt, iterations);

            byte[] hash = rdb.GetBytes(hash_size);
            return isSameByteArray(storedHash, hash);
        }

        private static bool isSameByteArray(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }

        public static void SaltAndHashPassword(string password, out byte[] salt, out byte[] hash)
        {
            Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(password, salt_size, iterations);
            salt = rdb.Salt;
            hash = rdb.GetBytes(hash_size);
        }


        public static string StringToMD5Hash(string inputString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }

        public static byte[] Serialize(object o)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, o);
            return ms.ToArray();
        }
        public static T Deserialize<T>(byte[] bytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(bytes);
            return (T)bf.Deserialize(ms);
        }
    }
}
