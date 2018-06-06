using Simple.Mvc;
using Simple.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple
{
    /// <summary>
    /// 配置路由
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// 利用静态方法进行路由注册
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{Controller}/{Action}",
                defaults: new{
                    Controller = "home",
                    Action = "index"
            });
        }
    }
}