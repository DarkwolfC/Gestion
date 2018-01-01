using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnicaesGestion.Models;
using System.Security.Cryptography;
using System.Text;

namespace UnicaesGestion.Class
{
    public class Security
    {
        public bool Authenticate(LoginModel login) {



            return false;
           
        }


        private String getHash(String txt) {
            StringBuilder sb = new StringBuilder();
            using (SHA256 has = SHA256Managed.Create()) {
                Encoding enc = Encoding.UTF8;
                Byte[] result = has.ComputeHash(enc.GetBytes(txt));
                foreach (Byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

    }
}