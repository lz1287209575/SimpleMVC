using Simple.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Mvc
{
    /// <summary>
    /// 找到一个控制器，用来处理请求
    /// </summary>
    public class MvcHandler : IHttpHandler
    {
        private IDictionary<string, object> routes;

        public MvcHandler(IDictionary<string, object> routes)
        {
            this.routes = routes;
        }

        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {

            //从路由数据中获取到控制器名称
            var controllerName = routes["Controller"].ToString();

            // 根据你的需求找一个人
            //IController controller = null;
            //switch (controllerName.ToString().ToLower())
            //{
            //case "home":
            //// 找一个处理member请求的对象
            //controller = new HomeController();
            //break;
            //case "main":
            //// 找一个处理product请求的对象
            //// 找一个处理member请求的对象
            //controller = new MainController();
            //break;
            //default:
            //break;
            //}
            // 确保有找到一个同志处理你的请求

            IController controller = DefaultControllerFactory.CreateController(controllerName);

            if (controller == null)
            {
                // 利用HTTP状态码 告诉你 你要找的人不存在
                throw new HttpException(404, "Not Found");
            }

            //创建和请求有关的上下文
            RequestContext reqContext = new RequestContext {
                HttpContext = context,
                RouteData = routes
            };



            ActionResult result = controller.Execute(reqContext);
            result.Context = reqContext;
            result.Execute(reqContext);

            // 已经找到
            //controller.Execute(context);
            // 前台职责结束        
        }
    }
}