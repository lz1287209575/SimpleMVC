using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Simple.Mvc
{
    public abstract class Controller : ControllerBase
    {
        #region 返回ContentResult


        /// <summary>
        /// 返回一个ContentResult
        /// </summary>
        /// <param name="content">输出内容</param>
        /// <returns></returns>
        public ContentResult Content(string content)
        {
            return Content(content, "text/palin");
        }

        /// <summary>
        /// 返回一个ContentResult
        /// </summary>
        /// <param name="content">输出内容</param>
        /// <param name="contentType">Content-Type</param>
        /// <returns></returns>
        public ContentResult Content(string content, string contentType)
        {
            return Content(content, contentType, Encoding.UTF8);
        }

        /// <summary>
        /// 返回一个ContentResult
        /// </summary>
        /// <param name="content">输出内容</param>
        /// <param name="contentType">Content-Type</param>
        /// <param name="contentEncoding">编码字符集</param>
        /// <returns></returns>
        public ContentResult Content(string content, string contentType, Encoding contentEncoding)
        {
            return new ContentResult(content, contentType, contentEncoding);
        } 

        #endregion


        #region 返回JsonResult

        /// <summary>
        /// 返回一个JsonResult
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public JsonResult Json(object obj)
        {
            return new JsonResult(obj);
        }

        #endregion


        #region 返回FileResult

        public FileResult File(string fileName, string contentType)
        {
            return new FileResult(fileName, contentType);
        }

        public FileResult File(byte[] buffer, string contentType)
        {
            return new FileResult(buffer, contentType);
        }

        public FileResult File(Stream fileStream, string contentType)
        {
            return new FileResult(fileStream, contentType);
        } 

        #endregion
    }
}