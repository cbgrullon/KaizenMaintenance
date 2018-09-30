using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaizen_Maintenance
{
    public static class Helpers
    {
        public static string HidePassword(string Password)
        {
            string HiddenPassword="";
            for (var index = 0; index < Password.Length; index++)
            {
                HiddenPassword += "*";
            }
            return HiddenPassword;
        }
    }
}