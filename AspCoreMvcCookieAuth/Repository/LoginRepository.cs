using AspCoreMvcCookieAuth.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvcCookieAuth.Repository
{
    public class LoginRepository : ILoginRepository
    {
        public async Task<EmpLogin> CheckAuthenticationAsync(string Username, string Password)
        {
            using (var con=new SqlConnection(ShareConnectionString.Value))
            {
                EmpLogin objempLogin = await con.QueryFirstOrDefaultAsync<EmpLogin>("select * from EmpLogin where EmpName='" + Username + "' and Password='" + Password + "'");
                return objempLogin;
            }
        }

        public async Task UpdateLoginTransaction(EmpLoginTransaction objempLoginTransaction)
        {
            using (var con = new SqlConnection(ShareConnectionString.Value))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(objempLoginTransaction);
                await con.ExecuteAsync("insert into EmpLoginTransaction(EmpId,EmpName,LoginTime,UserLoginCookie) values(@EmpId,@EmpName,@LoginTime,@UserLoginCookie)", parameters, commandType: CommandType.Text);
            }
        }

        public async Task UpdateLogOutTime(DateTime logouttime, string cookie)
        {
            using (var con = new SqlConnection(ShareConnectionString.Value))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@logouttime", logouttime);
                parameters.Add("@UserLoginCookie", cookie);
                await con.ExecuteAsync("update EmpLoginTransaction set LogOutTime=@logouttime where UserLoginCookie=@UserLoginCookie", parameters, commandType: CommandType.Text);
            }
        }
    }
}
