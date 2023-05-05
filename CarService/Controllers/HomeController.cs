using CarService.Models;
using CarService.Models.Constants;
using CarService.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Diagnostics;

namespace CarService.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarServiceContext _context;
        private readonly IConfiguration _config;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CarServiceContext context,IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _config = configuration;
        }

        public IActionResult Index()
        {
            string? userid =  HttpContext.Session.GetString(SessionKeys.UserId);
            if (userid == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Index", "JobMasters");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login LoginData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!string.IsNullOrEmpty(LoginData.Password))
            {
                LoginData.DecryptAndModifyPassword();

            }
            TblSystemUser user = (from u in _context.TblSystemUsers where u.FldEmailId == LoginData.UserName select u).FirstOrDefault();
            if (user == null)
            {
                ViewBag.msg = "User not found";
                return View();
            }
            else if (user.FldIsActive == false)
            {
                ViewBag.msg = "User is deactivated. Please contact admin";
                return View();
            }
            else if (user.FldPassword != LoginData.Password)
            {
                ViewBag.msg = "Incorrect Password";
                return View();
            }

            TblOrganizationMaster org = _context.TblOrganizationMasters.Where(o=>o.FldOrgId == user.FldOrgId).FirstOrDefault();
            if(org != null && org.FldIsFullSubscription == false)
            {
                if(org.FldActiveUntil.HasValue && org.FldActiveUntil.Value < DateTime.Now)
                {
                    ViewBag.msg = "Your company's subscription is expired.";
                    return View();
                }

            }
            if (user != null && user.FldPassword == LoginData.Password)
            {
                HttpContext.Session.SetString(SessionKeys.UserId, user.FldUserId + "");
                HttpContext.Session.SetString(SessionKeys.UserName, user.FldFullName + "");
                HttpContext.Session.SetString(SessionKeys.UserRole, user.FldRole + "");
                HttpContext.Session.SetString(SessionKeys.OrganizationId, user.FldOrgId + "");
                HttpContext.Session.SetString(SessionKeys.OrgLogo, "");
                HttpContext.Session.SetString(SessionKeys.OrganizationName, org.FldOrgName);

                string LogoPath = _config.GetValue<string>(ConfigKeys.StorageBasePath) + org.FldLogo;
                if (System.IO.File.Exists(LogoPath))
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(LogoPath);
                    string base64ImageRepresentation = "data:image/png;base64,"+Convert.ToBase64String(imageArray);
                    HttpContext.Session.SetString(SessionKeys.OrgLogo, base64ImageRepresentation);

                }


                if (user.FldRole == UserRoles.Admin)
                {
                    return RedirectToAction("Index", "OrganizationMasters");
                }
                else
                {
                    return RedirectToAction("Index", "JobMasters");
                }
            }

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

        //--------Remove all sessions
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}