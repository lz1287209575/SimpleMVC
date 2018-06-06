using Simple.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Mvc
{
    /// <summary>
    /// 路由集合的扩展方法
    /// </summary>
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// 映射路由
        /// </summary>
        /// <param name="routes">路由集合</param>
        /// <param name="name">路由名称</param>
        /// <param name="url">路由格式</param>
        /// <param name="defaults">默认值</param>
        public static void MapRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            //注册路由的过程就是向路由集合中添加Route对象
            //MapRoute(routes,name,url,new MvcHandler());   这里是可以进行优化的点，给Route一个创建MvcHandler的资格，这样得到的IHttpHandler是不同的Handler
            MapRoute(routes, name, url, defaults, (routeData) => { return new MvcHandler(routeData); });
        }


        /// <summary>
        /// 映射路由
        /// </summary>
        /// <param name="routes">路由集合</param>
        /// <param name="name">路由名称</param>
        /// <param name="url">路由格式</param>
        /// <param name="defaults">默认值</param>
        /// <param name="handler">处理程序</param>
        //  public static void MapRoute(this RouteCollection routes, string name, string url, object defaults, IHttpHandler handler)
        public static void MapRoute(this RouteCollection routes, string name, string url, object defaults, Func<IDictionary<string, object>, IHttpHandler> handler)
        {
            //注册路由的过程就是向路由集合中添加Route对象
            routes.Add(new Route(
                    name: name,
                    url: url,
                    defaults: defaults,
                    getHandler: handler
            ));
        }
    }
}