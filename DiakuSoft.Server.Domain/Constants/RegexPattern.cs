using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiakuSoft.Server.Domain.Constants
{
    public static class RegexPattern
    {
        public static string EmailPattern => "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
        public static string PasswordPattern => "^[a-zA-Z-']*$";
    }
}
