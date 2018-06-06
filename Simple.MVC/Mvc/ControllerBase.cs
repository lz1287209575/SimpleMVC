using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace Simple.Mvc
{
    public abstract class ControllerBase : IController
    {
        /// <summary>
        /// 请求有关的数据
        /// </summary>
        public HttpContext Context { get; set; }

        /// <summary>
        /// 路由数据
        /// </summary>
        public IDictionary<string, object> RouteData { get; set; }

        public virtual ActionResult Execute(RequestContext context)
        {
            this.Context = context.HttpContext;
            this.RouteData = context.RouteData;

            //通过路由数据获取请求的ActionName   由于在注册路由的时候已经进行了非空判断，所以可以直接ToString()
            string actionName = RouteData["Action"].ToString();

            //通过ActionName来获取相应的控制器函数
            //利用BindingFlag来进行条件过滤  BindFlags.Public 公共方法  BindingFlags.Instance 实例  BindingFlags.DeclaredOnly  只考虑自身声明，不考虑继承成员
            MethodInfo[] methods = this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance |BindingFlags.DeclaredOnly);
            MethodInfo method = null;
            //遍历获取到的函数信息，然后进行Action的匹配
            foreach(MethodInfo item in methods)
            {
                //进行忽略大小写的比较
                if (item.Name.Equals(actionName, StringComparison.OrdinalIgnoreCase))
                {
                    method = item;
                }
            }

            //进行空值判断
            if(method == null)
            {
                //没找到对应的Action，直接报404错误
                throw new HttpException(404, "Not Found");
            }

            //参数列表
            List<object> values = new List<object>();
            ParameterInfo[] parameters = method.GetParameters(); //获取所有的参数
            foreach(ParameterInfo parameter in parameters)
            {
                // 参数名称
                string name = parameter.Name;
                // 参数类型
                Type type = parameter.ParameterType;
                // 从QueryString、Form、RouteData当中获取value的值
                string value = Context.Request[name];
                if (string.IsNullOrEmpty(value))
                {
                    //如果值为空，证明value没在QueryString 或者Form当中
                    value = RouteData.ContainsKey(name) ? RouteData[name].ToString() : null;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    //证明值非空
                    values.Add(Convert.ChangeType(value, type));  //利用Convert.ChangeType()来转换类型
                }
                else
                {
                    values.Add(null);  //用null值把参数列表撑开
                }
            }


            //执行Action  后面附带参数
            return method.Invoke(this,values.ToArray()) as ActionResult;
        }
    }
}