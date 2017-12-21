using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;


namespace LCWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoRegeist.RegisterDependencies(); //注册调用
            //登录验证过滤器
            //GlobalFilters.Filters.Add(new CheckLoginFilter());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
