using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Simple.Mvc
{
    /// <summary>
    /// 控制器实现的接口 
    /// </summary>
    public interface IController
    {
        //void Execute(RequestContext context);
        ActionResult Execute(RequestContext context);
    }
}
