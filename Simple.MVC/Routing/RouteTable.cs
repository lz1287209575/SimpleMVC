using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Routing
{
    /// <summary>
    /// 全局路由表
    /// </summary>
    public static class RouteTable
    {
        public static RouteCollection Routes { get; private set; }

        //要用静态代码块
        static RouteTable()
        {
            Routes = RouteCollection.GetInstance();
        }

        /// <summary>
        /// 根据请求的Path Info解析路由数据  
        /// 可以在外面得到Route对象 所以路由字典还是要用out参数带出
        /// 可以定制HttpHandler
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public static Route MatchRoute(string requestUrl, out IDictionary<string, object> dict)
        {
            dict = null;
            //遍历全局路由表中的路由规则
            foreach (Route route in Routes)
            {
                //让当前遍历到的路由规则去匹配当前请求的地址
                if(route.MatchRoute(requestUrl, out dict))
                {
                    return route;
                }
            }
            return null;
        }
    }
}