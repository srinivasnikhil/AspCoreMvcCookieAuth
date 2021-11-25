using AspCoreMvcCookieAuth.Models;
using AspCoreMvcCookieAuth.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvcCookieAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly ILoginRepository _loginRepository;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, ILoginRepository loginRepository)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _loginRepository = loginRepository;
        }
        public async Task<IActionResult> Index()
        {
            string cookie = Request.Cookies["UserLoginCookie"];
            EmpLoginTransaction objempLoginTransaction = new EmpLoginTransaction();
            objempLoginTransaction.EmpName = _session.GetString("empname");
            objempLoginTransaction.EmpId = Convert.ToInt64(_session.GetInt32("empid"));
            objempLoginTransaction.LoginTime = DateTime.UtcNow;
            objempLoginTransaction.UserLoginCookie = cookie;
            await _loginRepository.UpdateLoginTransaction(objempLoginTransaction); 
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
