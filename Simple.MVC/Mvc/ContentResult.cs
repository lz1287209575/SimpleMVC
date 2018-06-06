using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Simple.Mvc
{
    public class ContentResult : ActionResult
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
        public Encoding ContentEncoding { get; set; }

        public ContentResult(string content) : this(content, "text/plain")
        {

        }
        public ContentResult(string content, string contentType) : this(content, contentType, Encoding.UTF8)
        {

        }

        public ContentResult(string content, string contentType, Encoding encoding)
        {
            this.Content = content;
            this.ContentType = ContentType;
            this.ContentEncoding = encoding;
        }

        public override void Execute(RequestContext context)
        {
            context.HttpContext.Response.ContentEncoding = this.ContentEncoding;
            context.HttpContext.Response.ContentType = this.ContentType;
            context.HttpContext.Response.Write(this.Content);
        }
    }
}