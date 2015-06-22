using BardicTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BardicTree.Profiles
{
    public class Gravatar
    {
        public static string GetURL(ApplicationUser user)
        {
            return "http://www.gravatar.com/avatar/" + GetHash(user) + "?d=identicon&s=150&f=y&r=pg";
        }

        private static string GetHash(ApplicationUser user)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return System.BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(user.Email.Trim().ToLower()))).Replace("-", "").ToLower();

        }
    }
}