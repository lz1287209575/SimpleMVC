using Simple.Mvc;
using Simple.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Simple
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 应用程序开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.Add(new Route(
            //   url: "home/{action}/{controller}",
            //   defaults: new { Action = "Home", Controller = "Index" },
            //   getHandler: () => new MvcHandler()
            //));


            //进行路由注册
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}