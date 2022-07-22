using KnetPayment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApplication1.Models;

namespace KnetPayment
{
    public partial class GCSPayment : System.Web.UI.Page //: BasePage
    {


        //static string DomainSite = "/esoneppia";// "/esONEnewTSP";//Prod//Localhost ""
        //                                          //var DomainSite = "/test";//Test environment -- /esonepp
        //static HttpRequest request = HttpContext.Current.Request;
        //string address = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority) + DomainSite;

        string address = ConfigurationManager.AppSettings["ApplicationUrl"].ToString();

        string lang = "en";
        bool IsGCSKnetAnonymousUser = false;

        protected override void InitializeCulture()
        {

            try
            {
                if (Session["lng"] == null)
                {
                    //commented below in version 2
                    //Session["lng"] = "en";

                    //added below in version 2
                    //string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                    //bool EnglishCulture = culture.Contains("English");
                    //Session["lng"] = EnglishCulture ? "en" : "ar";
                    Session["lng"] = Session["lng"] == null ? "en" : Session["lng"].ToString();

                }

                lang = Session["lng"].ToString();

                new SiteLanguages().SetLanguage(lang);

                HttpCookie langCookie = Request.Cookies["culture"];
                langCookie.Value = lang;


                if (Session["UserId"] != null)// non Anonymous User - version 2
                {
                    Dataclass objdataclass = new Dataclass();

                    objdataclass.UpdateUserSession(new UserSession { Userid = Convert.ToInt32(Session["UserId"]), lang = lang });
                    objdataclass = null;
                }
            }
            catch (Exception)
            {
                new SiteLanguages().SetLanguage(lang);

                HttpCookie langCookie = Request.Cookies["culture"];
                langCookie.Value = lang;

                Session["lng"] = lang;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null && Session["AnonymousUser"] != null)
            {
                //if (!string.IsNullOrEmpty(Request.QueryString["AnonymousUser"]))
                //{
                //    if (CommonFunctions.CsUploadDecrypt(Request.QueryString["AnonymousUser"].ToString()) == "AnonymousUser")
                //    {
                TIRCNumber.Disabled = true;
                //SecurityCode.Disabled = true;//2nd change from shahn
                //Session["AnonymousUser"] = CommonFunctions.CsUploadEncrypt("AnonymousUser");
                IsGCSKnetAnonymousUser = true;


                //    }
                //    else
                //    {
                //        Response.Redirect(DomainSite + "/Registration/Start");
                //    }
                //}
                //else
                //{
                //    Response.Redirect(DomainSite + "/Registration/Start");
                //}
            }
            else
            {// first version block 


                if (Session["UserId"] == null)
                {
                    Response.Redirect(address + "/Registration/Index");
                }
                if (Session["LegalEntity"].ToString() != "2")
                {
                    Response.Redirect(address + "/Registration/Index");
                }
                UserId.Value = HttpUtility.UrlEncode(CommonFunctions.CsUploadEncrypt(Session["UserId"].ToString()));
                Mobile.Value = Session["mobileNumberOfLoggedInUser"].ToString();
                Email.Value = Session["emailOfLoggedInUser"].ToString();//"test@test.com";//

                //added below in 2nd version
                Mobile.Disabled = true;
                Email.Disabled = true;
                ReferenceNumber.Disabled = true;
            }

            GCSKnetAnonymousUser.Value = IsGCSKnetAnonymousUser.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("Cache-Control", "no-cache");
            Response.CacheControl = "no-cache";
            Response.Expires = -1;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.Cache.SetNoStore();
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }

        protected void LanguageSwitch_Click(object sender, ImageClickEventArgs e)
        {
            //string lang = "";
            try
            {
                if (Session["lng"] == null)
                {

                    //commented below in version 2
                    //Session["lng"] = "en";

                    //added below in version 2
                    //string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                    //bool EnglishCulture = culture.Contains("English");
                    //Session["lng"] = EnglishCulture ? "en" : "ar";
                    Session["lng"] = Session["lng"] == null ? "en" : Session["lng"].ToString();
                    //=============

                    lang = Session["lng"].ToString();
                }
                else
                {

                    lang = Session["lng"].ToString();

                    lang = lang.Contains("ar") ? "en" : "ar";//switch language
                }

                new SiteLanguages().SetLanguage(lang);

                HttpCookie langCookie = Request.Cookies["culture"];
                langCookie.Value = lang;// lang.Contains("ar") ? "ar" : "en";


                Session["lng"] = lang;

                if (Session["UserId"] != null)
                {
                    Dataclass objdataclass = new Dataclass();

                    objdataclass.UpdateUserSession(new UserSession { Userid = Convert.ToInt32(Session["UserId"]), lang = lang });
                    objdataclass = null;

                }
                else if (Session["UserId"] == null && Session["AnonymousUser"] == null)// else if condion() alone is added in version 2.. earlier only else
                {
                    Response.Redirect(address + "/Registration/Index");
                }

            }
            catch (Exception)// e)
            {
                new SiteLanguages().SetLanguage(lang);

                HttpCookie langCookie = Request.Cookies["culture"];
                langCookie.Value = lang;

                Session["lng"] = lang;
            }

        }
    }
}

