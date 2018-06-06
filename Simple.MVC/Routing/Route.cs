using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


namespace Simple.Routing
{
    /// <summary>
    /// 路由对象
    /// 约定路由规则
    /// </summary>
    public class Route
    {
        /// <summary>
        /// 路由对象
        /// </summary>
        /// <param name="url">URL格式</param>
        /// <param name="defaults">默认值</param>
        /// <param name="getHandler">获取当前请求的处理对象</param>
        public Route(string url, object defaults, Func<IDictionary<string, object>, IHttpHandler> getHandler)
        {
            //匿名类型转换对象
            this.UrlTemplate = url;
            Defaults = new Dictionary<string, object>();
            this.GetRouteHandler = getHandler;
            PropertyInfo[] props = defaults.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Defaults.Add(prop.Name, prop.GetValue(defaults));
            }
        }

        public Route(string name, string url, object defaults, Func<IDictionary<string, object>, IHttpHandler> getHandler) : this(url, defaults, getHandler)
        {
            this.Name = name;
        }

        /// <summary>
        /// 路由名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL路由模板
        /// </summary>
        public string UrlTemplate { get; set; }

        /// <summary>
        /// 默认路由规则 {controller = "Home" , action = "Index"}
        /// </summary>
        public IDictionary<string, object> Defaults { get; set; }

        /// <summary>
        /// 当前请求有人处理了，获取当前处理请求的Handler对象
        /// </summary>
        //public IHttpHandler RouteHandler { get; set; }
        public Func<IDictionary<string, object>, IHttpHandler> GetRouteHandler { get; set; }




        /// <summary>
        /// 主动匹配一个Url
        /// </summary>
        /// <param name="requestUrl">请求路径</param>
        /// <param name="routeData"></param>
        public bool MatchRoute(string requestUrl, out IDictionary<string, object> routeData)
        {
            //为路由字典赋值一个空集合用于存放匹配完成后的数据
            routeData = new Dictionary<string, object>();
            //给字典里面添加Defaults元素  不要直接赋值，会有引用类型的问题
            foreach (KeyValuePair<string, object> kvp in Defaults)
            {
                routeData.Add(kvp.Key, kvp.Value);
            }

            //进行字符串处理 获取请求的每一部分和template的每一部分
            string[] requestUrlItems = requestUrl.Split('/');   //{"home","index"}
            string[] urlTemplateItems = this.UrlTemplate.Split('/');  //{"{controller}","{index}"}

            //判断是否匹配成功
            if (requestUrlItems.Length != urlTemplateItems.Length)
            {
                //格式不匹配
                return false;
            }

            //格式匹配   接下来开始匹配每个元素
            for (int i = 0; i < requestUrlItems.Length; i++)
            {
                //获取每一个部分
                string requestUrlItem = requestUrlItems[i];
                string urlTemplateItem = urlTemplateItems[i];
                //匹配变量
                if (urlTemplateItem.StartsWith("{") && urlTemplateItem.EndsWith("}"))
                {
                    //证明这里匹配完成之后是类似于 {controller}的变量
                    //路由字典中添加变量
                    //添加的时候先要判断键是否存在
                    string key = urlTemplateItem.Trim("{}".ToArray());   //一个地方多次使用变量的时候先进行变量本地化，否则影响性能
                    if (routeData.ContainsKey(key))
                    {
                        //如果存在，则进行修改
                        routeData[key] = requestUrlItem;
                    }
                    else
                    {
                        //不存在的话进行添加
                        routeData.Add(key, requestUrlItem);
                    }

                }
                else
                {
                    //匹配完成之后是强匹配，则下面进行的强匹配的判断
                    if (!urlTemplateItem.Equals(requestUrlItem, StringComparison.OrdinalIgnoreCase))
                    {
                        //防止外面拿到假数据,在匹配失败的时候先Clear掉字典里面的元素
                        routeData.Clear();
                        return false;
                    }
                }
            }

            //如果执行到循环结束，证明所有的都匹配成功了，则return true
            return true;

        }

    }
}