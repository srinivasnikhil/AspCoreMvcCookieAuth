using AspCoreMvcCookieAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvcCookieAuth.Repository
{
    public interface ILoginRepository
    {
        Task<EmpLogin> CheckAuthenticationAsync(string Username,string Password);
        Task UpdateLoginTransaction(EmpLoginTransaction objempLoginTransaction);
        Task UpdateLogOutTime(DateTime logouttime, string cookie);
    }
}
