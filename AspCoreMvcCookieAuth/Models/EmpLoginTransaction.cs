using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvcCookieAuth.Models
{
    public class EmpLoginTransaction
    {
        public long EmpTransId { get; set; }
        public long EmpId { get; set; }
        public string EmpName { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogOutTime { get; set; }
        public string UserLoginCookie { get; set; }
    }
}
