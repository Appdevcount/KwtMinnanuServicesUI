using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MvcHandler.DisableMvcResponseHeader = true;
        }


        protected void Session_End(object sender, EventArgs e)
        {


            if (Session["UserId"] != null)
            {
                String userIdToCheck = Session["UserId"].ToString();

                Dictionary<String, String> UsersDictionaryWithSessionsIds = new Dictionary<String, String>();

                if (Application["UsersDictionaryWithSessionsIds"] != null)
                {
                    UsersDictionaryWithSessionsIds = Application["UsersDictionaryWithSessionsIds"] as Dictionary<String, String>;

                    if (UsersDictionaryWithSessionsIds.ContainsKey(userIdToCheck))
                    {
                        if (Session["AuthToken"] != null)
                        {
                            if (Session["AuthToken"].ToString().Equals(UsersDictionaryWithSessionsIds[userIdToCheck]))
                            {
                                UsersDictionaryWithSessionsIds.Remove(userIdToCheck);

                                Application.Lock();
                                Application.Contents.Remove("UsersDictionaryWithSessionsIds");
                                Application.UnLock();

                                Application.Lock();
                                Application.Contents.Add("UsersDictionaryWithSessionsIds", UsersDictionaryWithSessionsIds);
                                Application.UnLock();
                            }
                        }
                    }
                }
            }

            //if (Session["UserId"] != null)
            //{
            //    String userIdToCheck = Session["UserId"].ToString();

            //    ArrayList UsersList = new ArrayList();

            //    if (Application["UsersList"] != null)
            //    {
            //        ArrayList TempUsersList = Application["UsersList"] as ArrayList;

            //        if (TempUsersList.Contains(userIdToCheck))
            //        {

            //            TempUsersList.Remove(userIdToCheck);

            //            UsersList = (ArrayList)TempUsersList.Clone();

            //            Application.Lock();
            //            Application.Contents.Remove("UsersList");
            //            Application.UnLock();


            //            Application.Lock();
            //            Application.Contents.Add("UsersList", UsersList);
            //            Application.UnLock();
            //        }
            //    }
            //}




            //if (Application["currentLoggedInUserId"] != null && Session["UserId"] != null)
            //{
            //    if (Application["currentLoggedInUserId"].ToString() == Session["UserId"].ToString())
            //    {
            //        Application.Lock();
            //        Application.Contents.Remove("currentLoggedInUserId");
            //        Application.UnLock();
            //    }
            //}


            if (Session["AuthToken"] != null)
            {
                Session["AuthToken"] = null;
            }

            if (Session["additionalId"] != null)
            {
                Session["additionalId"] = null;
            }


            Session.Abandon();
            Session.Clear();

            //for (int count = 0; count < Response.Cookies.Count; count++)
            //{
            //    HttpCookie cookie = Response.Cookies[count];

            //    Response.Cookies.Remove(cookie.Name);
            //    cookie.Expires = DateTime.Now.AddYears(-5);
            //    cookie.Value = null;

            //    Response.SetCookie(cookie);
            //}



            //Response.Cookies.Clear();
            //Request.Cookies.Clear();

            //Session.Abandon();
            //Session.Clear();

        }


        protected void Application_End(object sender, EventArgs e)
        {

            //if (Application["currentLoggedInUserId"] != null && Session["UserId"] != null)
            //{
            //    if (Application["currentLoggedInUserId"].ToString() == Session["UserId"].ToString())
            //    {
            //        Application.Lock();
            //        Application.Contents.Remove("currentLoggedInUserId");
            //        Application.UnLock();
            //    }
            //}

        }


        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
        }
    }
}
