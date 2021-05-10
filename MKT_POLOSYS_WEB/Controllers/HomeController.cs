using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MKT_POLOSYS_WEB.Models;
using MKT_POLOSYS_WEB.Providers;
using MKT_POLOSYS_WEB.Views;
using Newtonsoft.Json;

namespace MKT_POLOSYS_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index(string emp_no, string id)
        {
            MappingProductMtrkuProvider mappingProductMtrku = new MappingProductMtrkuProvider();
            //untuk sementara
            //if (id == null)
            //{
            //    id = "MTRKUMP";
            //}
            if (emp_no != null && id != null)
            {
                var base64EncodedBytes = System.Convert.FromBase64String(emp_no);
                var emp_nox = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                var emp_name = mappingProductMtrku.getUser(emp_nox);
                var sessionLogin = new SessionLogin() { empName = emp_name, empNo = emp_nox, idMenu = id };
                HttpContext.Session.SetString("SessionLogin", JsonConvert.SerializeObject(sessionLogin));
                HttpContext.Session.SetString("getusername", sessionLogin.empName);
                HttpContext.Session.SetString("getuserid", sessionLogin.empNo);
                HttpContext.Session.SetString("getmenuid", sessionLogin.idMenu);
            }
            if (HttpContext.Session.GetString("getusername") != null)
            {
                var sessionLogin = JsonConvert.DeserializeObject<SessionLogin>(HttpContext.Session.GetString("SessionLogin"));
                ViewBag.session = sessionLogin;
            }
            else
            {
                return View("~/Views/Shared/Redir.cshtml");
            }

            if (HttpContext.Session.GetString("getusername") == null || HttpContext.Session.GetString("getmenuid") == null)
            {
                return View("~/Views/Shared/Redir.cshtml");
            }
            else
            {
                return View();
            }
                
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

        //public IActionResult Menu()
        //{
        //    MenuViewModel model = new MenuViewModel();
        //    return PartialView("~/Shared/_LayoutUpload.cshtml",model);
        //}

        public void hello()
        {
            Console.WriteLine("hello");
        }


        public string Getdata {get;set;}
    }
}
