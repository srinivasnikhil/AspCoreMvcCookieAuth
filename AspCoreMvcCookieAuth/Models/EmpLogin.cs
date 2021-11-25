using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvcCookieAuth.Models
{
    public class EmpLogin
    {
        public long Id { get; set; }
        public string EmpName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Auth { get; set; }
    }
}
