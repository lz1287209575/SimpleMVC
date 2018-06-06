using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Simple.Mvc
{
    /// <summary>
    /// 实现JsonResult
    /// </summary>
    public class JsonResult : ActionResult
    {
        public object Object { get; set; }

        public JsonResult(object obj)
        {
            this.Object = obj;
        }

        public override void Execute(RequestContext context)
        {
            string jsonStr = new JavaScriptSerializer().Serialize(this.Object);
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.Write(jsonStr);
        }
    }
}