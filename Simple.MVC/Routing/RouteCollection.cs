using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Simple.Routing
{
    /// <summary>
    /// 路由集合
    /// </summary>
    public class RouteCollection : Collection<Route>
    {
        public static RouteCollection Instance;

        public static RouteCollection GetInstance()
        {
            if (Instance == null)
            {
                lock (_locker)
                {
                    if (Instance == null)
                    {
                        Instance = new RouteCollection();
                    }
                }
            }
            return Instance;
        }
        private static object _locker = new object();

        private RouteCollection()
        {

        }
    }
}