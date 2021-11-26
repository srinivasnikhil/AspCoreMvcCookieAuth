using AspCoreMvcCookieAuth.Models;
using AspCoreMvcCookieAuth.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspCoreMvcCookieAuth.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string Username, string Password)
        {
            try
            {
                EmpLogin objempLogin = new EmpLogin();
                if (ModelState.IsValid)
                {
                    objempLogin = await _loginRepository.CheckAuthenticationAsync(Username, Password);
                    if (objempLogin != null)
                    {
                        var userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name,objempLogin.EmpName)
                        };

                        var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                        await HttpContext.SignInAsync(userPrincipal);
                        HttpContext.Session.SetString("empname",objempLogin.EmpName);
                        HttpContext.Session.SetInt32("empid", Convert.ToInt32(objempLogin.Id));

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                    
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<IActionResult> LogOut()
        {
            try
            {
                string cookie = Request.Cookies["UserLoginCookie"];
                Response.Cookies.Delete(".AspNetCore.UserLoginCookie");
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                EmpLoginTransaction objempLoginTransaction = new EmpLoginTransaction();
                objempLoginTransaction.LogOutTime = DateTime.UtcNow;
                await _loginRepository.UpdateLogOutTime(objempLoginTransaction.LogOutTime,cookie);
                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
