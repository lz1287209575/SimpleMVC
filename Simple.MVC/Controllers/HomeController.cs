using Simple.Mvc;
using Simple.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Controllers
{
    /// <summary>
    /// 控制器的主体，用来处理分发向Action的请求
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region 测试
            //Context.Response.Write("差不多了");
            //return Json(new { Name = "Hahah", Age = 16 });
            //return File("D:\\1.txt", "text/plain"); 
            #endregion


            return Content("Hahaha");
        }
    }
}