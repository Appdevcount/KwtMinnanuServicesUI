using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1
{

    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            var cache = GetCache(filterContext);


            cache.SetExpires(DateTime.UtcNow.AddYears(-1));
            cache.SetValidUntilExpires(false);
            cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            cache.SetCacheability(HttpCacheability.NoCache);
            cache.SetAllowResponseInBrowserHistory(false);
            cache.SetNoServerCaching();

            cache.SetNoStore();


            base.OnResultExecuting(filterContext);
        }

        protected virtual HttpCachePolicyBase GetCache(ResultExecutingContext filterContext)
        {
            return filterContext.HttpContext.Response.Cache;
        }
    }

    [NoCache]
    public class MyBaseController : Controller
    {
        // Here I have created this for execute each time any controller (inherit this) load 
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = SiteLanguages.GetDefaultLanguage();
                }
            }

            new SiteLanguages().SetLanguage(lang);

            return base.BeginExecuteCore(callback, state);
        }

        public void CheckSession()
        {

            if (Session["UserId"] == null)
            {
                RedirectToAction("LogOut", "registration");
                Response.Redirect("~/registration/LogOut", true);
            }
            else
            {
                Dictionary<String, String> UsersDictionaryWithSessionsIds = new Dictionary<String, String>();

                String userIdToCheck = Session["UserId"].ToString();

                if (System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] != null)
                {
                    UsersDictionaryWithSessionsIds = System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] as Dictionary<String, String>;

                    if (UsersDictionaryWithSessionsIds.ContainsKey(userIdToCheck))
                    {
                        if (Session["AuthToken"] != null)
                        {
                            if (!Session["AuthToken"].ToString().Equals(UsersDictionaryWithSessionsIds[userIdToCheck]))
                            {
                                RedirectToAction("LogOut", "registration");
                                Response.Redirect("~/registration/LogOut", true);
                            }
                        }
                    }
                    else
                    {
                        RedirectToAction("LogOut", "registration");
                        Response.Redirect("~/registration/LogOut", true);
                    }

                }

                


                //String userIdToCheck = Session["UserId"].ToString();

                //ArrayList UsersList = new ArrayList();

                //if (System.Web.HttpContext.Current.Application["UsersList"] != null)
                //{
                //    ArrayList TempUsersList = System.Web.HttpContext.Current.Application["UsersList"] as ArrayList;

                //    if (!TempUsersList.Contains(userIdToCheck))
                //    {
                //        RedirectToAction("LogOut", "registration");
                //        Response.Redirect("~/registration/LogOut", true);
                //    }
                //}

                //    //if (Session["additionalId"] != null &&
                //    //    Request.Cookies["toMeetYou"] != null)
                //    //{
                //    //    if (!Session["additionalId"].ToString().
                //    //        Equals(Request.Cookies["toMeetYou"].Value))
                //    //    {
                //    //        RedirectToAction("LogOut", "registration");
                //    //    }
                //    //}
            }
            }


        ////added newly 06-08-2019
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    filterContext.ExceptionHandled = true;

        //    ////Log the error!!
        //    //_Logger.Error(filterContext.Exception);

        //    filterContext.Result = RedirectToAction("servererror", "Error");
        //    base.OnException(filterContext);
        //}
        ////added newly 06-08-2019
        //protected override void OnException(ExecutionContext filterContext)
        //{
        //    filterContext.ExceptionHandled = true;

        //    ////Log the error!!
        //    //_Logger.Error(filterContext.Exception);

        //    //Redirect or return a view, but not both.
        //    filterContext.Result = RedirectToAction("servererror", "Error");
        //    //// OR 
        //    //filterContext.Result = new ViewResult
        //    //{
        //    //    ViewName = "~/Views/ErrorHandler/Index.cshtml"
        //    //};
        //}

    }



}