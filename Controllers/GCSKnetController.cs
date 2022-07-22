using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Data;



using WebApplication1.Models;
using System.Configuration;

namespace WebApplication1.Controllers
{
    public class GCSKnetController : Controller
    {

        [HttpGet]
        public ActionResult AnonymousUserPayment()
        {

            //Logout existing user and making session for Anonymous user

            // if (Request.Cookies["catchMeIfYouCan"] != null)
            // {
            //     Response.Cookies["catchMeIfYouCan"].Value = String.Empty;
            //     Response.Cookies["catchMeIfYouCan"].Value = null;
            //     Response.Cookies["catchMeIfYouCan"].Expires = DateTime.Now.AddMonths(-50);


            //     Response.Cookies.Remove("catchMeIfYouCan");

            //     Response.Cookies.Add(new HttpCookie("catchMeIfYouCan", ""));
            // }

            // if (Request.Cookies["Nice"] != null)
            // {
            //     Response.Cookies["Nice"].Value = String.Empty;
            //     Response.Cookies["Nice"].Value = null;
            //     Response.Cookies["Nice"].Expires = DateTime.Now.AddMonths(-50);

            //     Response.Cookies.Remove("Nice");

            //     Response.Cookies.Add(new HttpCookie("AuthToken", ""));
            // }

            // if (Request.Cookies["kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd"] != null)
            // {
            //     Response.Cookies["kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd"].Value = String.Empty;
            //     Response.Cookies["kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd"].Value = null;
            //     Response.Cookies["kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd"].Expires = DateTime.Now.AddMonths(-50);

            //     Response.Cookies.Remove("kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd");

            //     Response.Cookies.Add(new HttpCookie("kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd", ""));
            // }


            // if (Request.Cookies["toMeetYou"] != null)
            // {
            //     Response.Cookies["toMeetYou"].Value = String.Empty;
            //     Response.Cookies["toMeetYou"].Value = null;
            //     Response.Cookies["toMeetYou"].Expires = DateTime.Now.AddMonths(-50);

            //     Response.Cookies.Add(new HttpCookie("toMeetYou", ""));
            // }


            // if (Session["UserId"] != null)
            // {
            //     String userIdToCheck = Session["UserId"].ToString();

            //     Dictionary<String, String> UsersDictionaryWithSessionsIds = new Dictionary<String, String>();

            //     if (System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] != null)
            //     {
            //         UsersDictionaryWithSessionsIds = System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] as Dictionary<String, String>;

            //         if (UsersDictionaryWithSessionsIds.ContainsKey(userIdToCheck))
            //         {

            //             if (Session["AuthToken"] != null)
            //             {
            //                 if (Session["AuthToken"].ToString().Equals(UsersDictionaryWithSessionsIds[userIdToCheck]))
            //                 {
            //                     UsersDictionaryWithSessionsIds.Remove(userIdToCheck);

            //                     System.Web.HttpContext.Current.Application.Lock();
            //                     System.Web.HttpContext.Current.Application.Contents.Remove("UsersDictionaryWithSessionsIds");
            //                     System.Web.HttpContext.Current.Application.UnLock();


            //                     System.Web.HttpContext.Current.Application.Lock();
            //                     System.Web.HttpContext.Current.Application.Contents.Add("UsersDictionaryWithSessionsIds", UsersDictionaryWithSessionsIds);
            //                     System.Web.HttpContext.Current.Application.UnLock();
            //                 }
            //             }


            //         }
            //     }
            // }


            // if (Session["additionalId"] != null)
            // {
            //     Session["additionalId"] = null;
            // }


            // for (int count = 0; count < Response.Cookies.Count; count++)
            // {
            //     HttpCookie cookie = Response.Cookies[count];

            //     Response.Cookies.Remove(cookie.Name);
            //     cookie.Expires = DateTime.Now.AddYears(-5);
            //     cookie.Value = null;

            //     Response.SetCookie(cookie);
            // }

            // CommonFunctions.LogUserActivity(

            //"UserLoggedOUT", "UserLoggedOUT", "", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "", "");


            // TempData.Clear();
            // Response.Cookies.Clear();
            // Request.Cookies.Clear();

            // //Session.Abandon();
            // Session.Clear();

            // clearSession();


            //Setting up Anonymoususer
            //string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

            //bool EnglishCulture = culture.Contains("English");
            //Session["lng"] = EnglishCulture ? "en" : "ar";

            Session["lng"] = Session["lng"] == null ? "en" : Session["lng"].ToString();

            Session["AnonymousUser"] = CommonFunctions.CsUploadEncrypt("AnonymousUser");



            // string DomainSite = "esoneppia";// "/esONEnewTSP";//Prod//Localhost ""
            //                                       //var DomainSite = "/test";//Test environment -- /esonepp
            // HttpRequest request = System.Web.HttpContext.Current.Request;
            //string address = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority) + DomainSite;

            string address = ConfigurationManager.AppSettings["ApplicationUrl"].ToString();

            //return Redirect(address+"/SelfService/Payment.aspx");
            return Redirect(address + "/SelfService/Payment.aspx");

        }
        [HttpPost]
        public ActionResult IsReceiptValid(GCSReqObj GCSReqObj)
        {
            ReceiptAction ReceiptAction = new ReceiptAction();

            //string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

            //bool EnglishCulture = culture.Contains("English");

            //Session["lng"]= EnglishCulture ? "en" : "ar";
            String lang = Session["lng"].ToString() == "en" ? "eng" : "ara";

            GCSReqObj.CommonData5 = lang;// EnglishCulture ? "eng" : "ara";

            if (Session["UserId"] == null)
            {
                if (Session["AnonymousUser"] != null && CommonFunctions.CsUploadDecrypt(Session["AnonymousUser"].ToString()) == "AnonymousUser")

                {
                    if (!GCSReqObj.CommonData1.ToLower().StartsWith("tdr"))
                    {
                        VerifyReceiptDetailsforGCSSite VerifyReceiptDetailsforGCSSite = new VerifyReceiptDetailsforGCSSite() { Proceed = false, Message = "22" };//Please enter correct details message" };

                        ReceiptAction.VerifyReceiptDetailsforGCSSite = VerifyReceiptDetailsforGCSSite;
                        return Json(ReceiptAction);
                    }
                }

                else//below line existing condition
                {
                    return Json(new { StatusCode = -3, message = "Session expired" });
                }
            }
            else//added in version 2
            {
                if (!string.IsNullOrEmpty(GCSReqObj.CommonData1))// some refeerence number value passed, which shoukld not be by logged in user
                {
                    VerifyReceiptDetailsforGCSSite VerifyReceiptDetailsforGCSSite = new VerifyReceiptDetailsforGCSSite() { Proceed = false, Message = "1" };//Please enter correct details message" };

                    ReceiptAction.VerifyReceiptDetailsforGCSSite = VerifyReceiptDetailsforGCSSite;
                    return Json(ReceiptAction);
                }
            }

            try
            {
                ReceiptAction = GCSKnetDataclass.IsReceiptValid(GCSReqObj);
                return Json(ReceiptAction);
            }
            catch (Exception e)
            {
                CommonFunctions.LogUserActivity("IsReceiptValid", "", "", "", "", e.Message.ToString());

                VerifyReceiptDetailsforGCSSite VerifyReceiptDetailsforGCSSite = new VerifyReceiptDetailsforGCSSite() { Proceed = false, Message = "-1" };// Resources.Resource.somethingwentwrong };//"Some error has occured . Please Contact IT Team" };

                ReceiptAction.VerifyReceiptDetailsforGCSSite = VerifyReceiptDetailsforGCSSite;
                return Json(ReceiptAction);
            }

        }
        [HttpPost]
        public ActionResult SessionChecker(string UserId)
        {

            if (Session["UserId"] == null)
            {
                if (Session["AnonymousUser"] != null && CommonFunctions.CsUploadDecrypt(Session["AnonymousUser"].ToString()) == "AnonymousUser")
                {
                    return Json(new { Active = true });
                }
                //version 1 condition
                return Json(new { Active = false });
            }
            UserId = CommonFunctions.CsUploadDecrypt(HttpUtility.UrlDecode(UserId));
            if (Session["UserId"].ToString() == UserId && Session["LegalEntity"].ToString() == "2")
            {
                return Json(new { Active = true });
            }
            else
            {
                return Json(new { Active = false });
            }

        }


        private void clearSession()
        {
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = String.Empty;
                Response.Cookies["ASP.NET_SessionId"].Value = null;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-50);


                Response.Cookies.Remove("ASP.NET_SessionId");

                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            }


            if (Request.Cookies["catchMeIfYouCan"] != null)
            {
                Response.Cookies["catchMeIfYouCan"].Value = String.Empty;
                Response.Cookies["catchMeIfYouCan"].Value = null;
                Response.Cookies["catchMeIfYouCan"].Expires = DateTime.Now.AddMonths(-50);


                Response.Cookies.Remove("catchMeIfYouCan");

                Response.Cookies.Add(new HttpCookie("catchMeIfYouCan", ""));
            }



            if (Request.Cookies["Nice"] != null)
            {
                Response.Cookies["Nice"].Value = String.Empty;
                Response.Cookies["Nice"].Value = null;
                Response.Cookies["Nice"].Expires = DateTime.Now.AddMonths(-50);

                Response.Cookies.Remove("Nice");

                Response.Cookies.Add(new HttpCookie("AuthToken", ""));
            }

            if (Request.Cookies["kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd"] != null)
            {
                Response.Cookies["kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd"].Value = String.Empty;
                Response.Cookies["kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd"].Value = null;
                Response.Cookies["kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd"].Expires = DateTime.Now.AddMonths(-50);

                Response.Cookies.Remove("kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd");

                Response.Cookies.Add(new HttpCookie("kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd", ""));
            }


            if (Request.Cookies["toMeetYou"] != null)
            {
                Response.Cookies["toMeetYou"].Value = String.Empty;
                Response.Cookies["toMeetYou"].Value = null;
                Response.Cookies["toMeetYou"].Expires = DateTime.Now.AddMonths(-50);

                Response.Cookies.Add(new HttpCookie("toMeetYou", ""));
            }


            //if (System.Web.HttpContext.Current.Application["currentLoggedInUserId"] != null && Session["UserId"] != null)
            //{
            //    if (System.Web.HttpContext.Current.Application["currentLoggedInUserId"].ToString() == Session["UserId"].ToString())
            //    {
            //        System.Web.HttpContext.Current.Application.Contents.Remove("currentLoggedInUserId");
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


            for (int count = 0; count < Response.Cookies.Count; count++)
            {
                HttpCookie cookie = Response.Cookies[count];

                Response.Cookies.Remove(cookie.Name);
                cookie.Expires = DateTime.Now.AddYears(-5);
                cookie.Value = null;

                Response.SetCookie(cookie);
            }


            TempData.Clear();
            Response.Cookies.Clear();
            Request.Cookies.Clear();

            //Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
        }


    }
}