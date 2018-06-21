using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Mvc
{
    /// <summary>
    /// ControllerBase执行完成后的得到的结果
    /// </summary>
    public abstract class ActionResult
    {
        public RequestContext Context { get; set; }

        /// <summary>
        /// ActionResult的执行方法
        /// </summary>
        /// <param name="context"></param>
        public abstract void Execute(RequestContext context);
    }
}