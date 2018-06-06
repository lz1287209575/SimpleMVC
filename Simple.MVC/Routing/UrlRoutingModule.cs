using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Routing
{
    public class UrlRoutingModule : IHttpModule
    {

        public void Init(HttpApplication application)
        {
            //Asp.Net 中第七个事件 需要在这个事件当中进行路由处理，如果不处理路由的话，就晚了
            application.PostResolveRequestCache += Context_PostResolveRequestCache;
        }

        private void Context_PostResolveRequestCache(object sender, EventArgs e)
        {
            #region 获取请求相对于网站根目录的路径  pathinfo

            //得到HttpPApplication对象
            HttpApplication application = sender as HttpApplication;
            //得到当前请求的上下文
            HttpContext context = application.Context;

            //根据路由表解析当前请求的路径
            //context.Request.AppRelativeCurrentExecutionFilePath 得到的是从网站根目录的虚拟路径  类似~/portal.ashx
            //需要的是portal.ashx 所以要Substring()
            string requestUrl = context.Request.AppRelativeCurrentExecutionFilePath.Substring(2);

            #endregion

            #region 获取当前请求的路由对象
            //解析数据
            //匹配路由表放在RouteTable类里面最合适
            //解析数据获取路由对象
            Route route = RouteTable.MatchRoute(requestUrl, out IDictionary<string, object> routeData);
            if (route is null)
            {
                // 404 没找到
                throw new HttpException(404, "Not Found");
            }

            //确保路由数据中存在Controller和Action
            if (!routeData.ContainsKey("Controller") || !routeData.ContainsKey("Action"))
            {
                throw new HttpException(404, "Not Found");
            }


            //为当前路由数据指定HttpHandler
            //指定的参数是当前的路由字典
            IHttpHandler handler = route.GetRouteHandler(routeData);
            #endregion


            //给当前请求分配处理程序
            context.RemapHandler(handler);
        }



        public void Dispose()
        {

        }
    }
}