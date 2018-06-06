using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Mvc
{
    /// <summary>
    /// 路由以及请求相关的数据进行打包
    /// </summary>
    public class RequestContext
    {
        /// <summary>
        /// 这次请求有关的上下文
        /// </summary>
        public HttpContext HttpContext { get; set; }
        
        
        /// <summary>
        /// 路由数据
        /// </summary>
        public IDictionary<string,object> RouteData { get; set; }
    }
}