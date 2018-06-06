using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Simple.Mvc
{
    public class FileResult : ActionResult
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Buffer { get; set; }
        public Stream FileContentStream { get; set; }

        private FileResult(string contentType)
        {
            this.ContentType = contentType;
        }

        public FileResult(string fileName, string contentType) : this(contentType)
        {
            this.FileName = fileName;
            using (FileStream fs = File.OpenRead(this.FileName))
            {
                this.Buffer = new byte[fs.Length];
                fs.Read(this.Buffer, 0, this.Buffer.Length);
            }
        }

        public FileResult(byte[] buffer, string contentType) : this(contentType)
        {
            this.Buffer = buffer;
        }

        public FileResult(Stream fileStream, string contentType) : this(contentType)
        {
            this.FileContentStream = fileStream;
            this.Buffer = new byte[this.FileContentStream.Length];
            this.FileContentStream.Read(this.Buffer, 0, this.Buffer.Length);
        }


        public override void Execute(RequestContext context)
        {
            context.HttpContext.Response.ContentType = this.ContentType;
            context.HttpContext.Response.OutputStream.Write(this.Buffer,0,this.Buffer.Length);
        }
    }
}