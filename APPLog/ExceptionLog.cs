using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using NLog;

namespace WebApplication1.APPLog
{
    public class ExceptionLog:IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Log("OnException", filterContext.RouteData, filterContext);
        }
        private void Log(string methodName, RouteData routeData, ExceptionContext filterContext)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];

           
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("methodName:" + methodName);
            sb.AppendLine("controller:" + controllerName);
            sb.AppendLine("action:" + actionName);
            sb.AppendLine("Exception:" + filterContext.Exception);
            var logger = NLog.LogManager.GetCurrentClassLogger();
            // logger.Info(sb.ToString());


           // var eventInfo = new LogEventInfo(LogLevel.Error, logger.Name, sb.ToString());
            var eventInfo = new LogEventInfo(LogLevel.Error, "ETrade", sb.ToString());
            eventInfo.Properties["Controller"] = controllerName;
            eventInfo.Properties["Action"] = actionName;
            eventInfo.Properties["browserIp"] = filterContext.HttpContext.Request.UserHostAddress;
            eventInfo.Properties["url"] = filterContext.HttpContext.Request.Url;
            if (HttpContext.Current.Session["UserId"] != null)
            {
                eventInfo.Properties["UserId"] = HttpContext.Current.Session["UserId"].ToString();
            }
            if (HttpContext.Current.Session["TokenId"] != null)
            {
                eventInfo.Properties["TokenId"] = HttpContext.Current.Session["TokenId"].ToString();
            }
            if (HttpContext.Current.Session["Geolocation"] != null)
            {
                eventInfo.Properties["geoLocation"] = HttpContext.Current.Session["Geolocation"].ToString();
            }
            if (filterContext.HttpContext.Request.Browser.IsMobileDevice)
            {
                eventInfo.Properties["deviceType"] = "Android";
            }
            else
            {
                eventInfo.Properties["deviceType"] = "Browser";
            }
            eventInfo.Properties["requestDatetime"] = DateTime.Now.ToString();
            //  eventInfo.Properties["LoggerName"] = filterContext.HttpContext.Request.ApplicationPath.ToString().Replace("/","");
            logger.Log(eventInfo);
        }
    }
}