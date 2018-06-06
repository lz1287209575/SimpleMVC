using Simple.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Simple.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Content()
        {
            return Content("终于实现了");
        }
    }
}