using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.DirectoryServices.AccountManagement;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            var ctx = HttpContext.Cache.Get("ctx");
            if(ctx == null)
                CacheId();

            return View();
        } 

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult CacheId()
        {
            var ctx = WindowsIdentity.GetCurrent().Name;
            //PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "HOME");
            HttpContext.Cache.Insert("ctx", ctx, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
            return View(ctx);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}