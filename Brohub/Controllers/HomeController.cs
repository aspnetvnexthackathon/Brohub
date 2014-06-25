using Microsoft.AspNet.Mvc;
using System;

namespace Brohub
{
    /// <summary>
    /// Summary description for HomeController
    /// </summary>
    public class HomeController : Controller
    {
	    public HomeController()
	    {
	    }

        public ActionResult Index()
        {
            return View();
        }
    }
}