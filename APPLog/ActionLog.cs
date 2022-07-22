using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1.APPLog
{
    public class ActionLog: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData, filterContext);
            base.OnActionExecuting(filterContext);
        }
        private void Log(string methodName, RouteData routeData, ActionExecutingContext filterContext)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            
            var stream = filterContext.HttpContext.Request.InputStream;
            var data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("methodName:" + methodName);
            sb.AppendLine("controller:"+ controllerName);
            sb.AppendLine("action:"+ actionName);
            sb.AppendLine("Parameters:"+ Encoding.UTF8.GetString(data));
            var logger = NLog.LogManager.GetCurrentClassLogger();
           // logger.Info(sb.ToString());

            //var eventInfo = new LogEventInfo(LogLevel.Info, logger.Name, sb.ToString());
            var eventInfo = new LogEventInfo(LogLevel.Info, "ETrade", sb.ToString());
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