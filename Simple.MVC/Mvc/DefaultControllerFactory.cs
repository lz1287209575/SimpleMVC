using Simple.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;

namespace Simple.Mvc
{
    public static class DefaultControllerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public static IList<Type> AllControllerType { get; set; }

        static DefaultControllerFactory()
        {
            AllControllerType = new List<Type>();
            //获取项目中所有引用的程序集  利用BuildManager.GetReferenceAssemblies();
            var assemblies = BuildManager.GetReferencedAssemblies();
            //遍历所有的程序集
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                //遍历程序集中所有的类型
                foreach (Type type in types)
                {
                    //判断这个类是不是控制器  
                    //实现了IContoller接口
                    if (type.IsClass && !type.IsAbstract
                        && !type.IsInterface && typeof(IController).IsAssignableFrom(type))
                    {
                        AllControllerType.Add(type);
                    }
                }
            }
        }


        public static IController CreateController(string controllerName)
        {
            #region 简单工厂
            //IController controller = null;
            //switch (controllerName.ToLower())
            //{
            //    case "home":
            //        // 找一个处理member请求的对象
            //        controller = new HomeController();
            //        break;
            //    case "main":
            //        // 找一个处理product请求的对象
            //        // 找一个处理member请求的对象
            //        controller = new MainController();
            //        break;
            //    default:
            //        break;
            //}
            //return controller;
            #endregion

            #region 抽象工厂V1

            //string controllerTypeName = string.Format("Simple.Controllers.{0}Contoller", controllerName);
            //IController controller = Assembly.GetExecutingAssembly().CreateInstance(controllerTypeName) as IController;
            //return controller;

            #endregion

            #region 抽象工厂V2

            foreach (Type type in AllControllerType)
            {
                if (type.Name.Equals(controllerName + "Controller", StringComparison.OrdinalIgnoreCase))
                {
                    return Activator.CreateInstance(type) as IController;
                }
            }
            return null;
            #endregion
        }
    }
}