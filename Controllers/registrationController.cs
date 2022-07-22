﻿#region New Code 17-FEB
using CaptchaMvc.HtmlHelpers;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class registrationController : MyBaseController
    {
        // GET: registration
        Dataclass objdataclass = new Dataclass();


        public void WriteToLogFile(string inputvalue, string sessionDetails)
        {

            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(sessionDetails + "Inpu" +
                    "tValue>" + inputvalue, EventLogEntryType.Information, 101, 1);
            }

        }
        //azhar




        public ActionResult logIn(String TokenId, String ePayReqNo)
        {
            CheckSession();
            String requestNumberWithBackSlaSh = "";
            String EnCryptedRequestNumber = "";
            String BrokerRenewalPrefix = ConfigurationManager.AppSettings["BrokerRenewalPrefix"].ToString();
            try
            {
                if (!String.IsNullOrEmpty(TokenId) &&
                    !String.IsNullOrWhiteSpace(TokenId) &&
                    !String.IsNullOrEmpty(ePayReqNo) &&
                    !String.IsNullOrWhiteSpace(ePayReqNo))
                {

                    var DeCryptedRequestNumber = CommonFunctions.CsUploadDecrypt(ePayReqNo);
                    var DeCodedRequestNumber = HttpUtility.UrlDecode(DeCryptedRequestNumber);
                    requestNumberWithBackSlaSh = DeCodedRequestNumber.Replace('_', '/');



                    WriteToLogFile(requestNumberWithBackSlaSh, "check");



                    if (requestNumberWithBackSlaSh.StartsWith("LR") || requestNumberWithBackSlaSh.StartsWith("LD") || requestNumberWithBackSlaSh.StartsWith("LC") || requestNumberWithBackSlaSh.StartsWith("LT") || requestNumberWithBackSlaSh.StartsWith("LW") || requestNumberWithBackSlaSh.StartsWith("LP") || requestNumberWithBackSlaSh.StartsWith("LI") || requestNumberWithBackSlaSh.StartsWith(BrokerRenewalPrefix))
                    {
                        EnCryptedRequestNumber = CommonFunctions.CsUploadEncrypt(requestNumberWithBackSlaSh);

                        WriteToLogFile(EnCryptedRequestNumber, "EnCryptedRequestNumber");
                        return RedirectToAction("RequestDetailsfortheRequest", "Request", new { EServiceRequestNumber = EnCryptedRequestNumber });


                    }

                    else
                    {

                        // var deCodedToken = HttpUtility.UrlDecode(TokenId);
                        //String DecryptedTokenId = AES.DecryptToken(deCodedToken);


                        var ReqNo = CommonFunctions.CsUploadDecrypt(ePayReqNo);

                        //ReqNo = HttpUtility.UrlEncode(ReqNo);

                        return RedirectToAction("EPayment", "User", new { Id = ReqNo });
                        //return RedirectToAction("UserRegistration");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToLogFile(ex.Message + "  " + EnCryptedRequestNumber, ex.StackTrace + " public ActionResult logIn(String TokenId, String ePayReqNo)");
            }
            return RedirectToAction("LogOut");
        }

        public ActionResult Home()
        {

            return View();
        }
        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            //string alphabets = "012345679ABCEFGHKLMNPRSWXYZabcdefghijkhlmnopqrstuvwxyz";
            string alphabets = "12345679ABCDEFGHKLMNPQRSXYZ";
            //string alphabets = "012345679abcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString().ToUpper();
        }



        public System.Web.Mvc.FileResult GetCaptchaImage()
        {
            // Added By AbdulKarim   27 10
            //HatchBrush hatchBrush = new HatchBrush(
            //     HatchStyle.ZigZag,
            //     Color.DarkBlue,
            //     Color.LightSeaGreen);

            HatchBrush hatchBrush = new HatchBrush(
                 HatchStyle.ZigZag,
                 Color.Gray,
                 Color.DarkGreen);

            //HatchBrush hatchBrush = new HatchBrush(
            //     HatchStyle.Percent60,
            //     Color.Empty,
            //     Color.DarkBlue); 



            string text = GetRandomText();// Session["CAPTCHA"].ToString();
            Session["CaptchaCode"] = text;
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            // Commented  By AbdulKarim   27 10  
            //Font font = new Font("Arial", 15);

            // Added By AbdulKarim   27 10
            Font font = new Font("Arial", 17);


            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);

            //img = new Bitmap((int)textSize.Width + 80, (int)textSize.Height + 40);

            drawing = Graphics.FromImage(img);

            Color backColor = Color.WhiteSmoke; //SeaShell;
            Color textColor = Color.Black;

            drawing.Clear(backColor);


            //drawing.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            //drawing.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //drawing.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            // Commented  27 10
            //drawing.DrawString(text, font, textBrush, 20, 10);


            // Added By AbdulKarim   27 10
            drawing.DrawString(text, font, hatchBrush, 20, 10);




            drawing.Save();

            font.Dispose();
            textBrush.Dispose();

            // Added 27 10
            hatchBrush.Dispose();

            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }

        public ActionResult LogOut()
        {


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


            if (Session["UserId"] != null)
            {
                String userIdToCheck = Session["UserId"].ToString();

                Dictionary<String, String> UsersDictionaryWithSessionsIds = new Dictionary<String, String>();

                if (System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] != null)
                {
                    UsersDictionaryWithSessionsIds = System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] as Dictionary<String, String>;

                    if (UsersDictionaryWithSessionsIds.ContainsKey(userIdToCheck))
                    {

                        if (Session["AuthToken"] != null)
                        {
                            if (Session["AuthToken"].ToString().Equals(UsersDictionaryWithSessionsIds[userIdToCheck]))
                            {
                                UsersDictionaryWithSessionsIds.Remove(userIdToCheck);

                                System.Web.HttpContext.Current.Application.Lock();
                                System.Web.HttpContext.Current.Application.Contents.Remove("UsersDictionaryWithSessionsIds");
                                System.Web.HttpContext.Current.Application.UnLock();


                                System.Web.HttpContext.Current.Application.Lock();
                                System.Web.HttpContext.Current.Application.Contents.Add("UsersDictionaryWithSessionsIds", UsersDictionaryWithSessionsIds);
                                System.Web.HttpContext.Current.Application.UnLock();
                            }
                        }


                    }
                }
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

            CommonFunctions.LogUserActivity(

           "UserLoggedOUT", "UserLoggedOUT", "", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "", "");


            TempData.Clear();
            Response.Cookies.Clear();
            Request.Cookies.Clear();

            Session.Abandon();
            Session.Clear();

            clearSession();

            //return RedirectToAction("Index");
            return RedirectToAction("Start");
        }
        public ActionResult Start()
        {

            return View();
            //return RedirectToAction("BrokerServices", "registration");
        }


        #region Abdul Karim 26 NOV 2019
        public ActionResult Start2()
        {

            return View();
        }
        public ActionResult Index2(int? ServiceType)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    return RedirectToAction("LogOut");
                }

                if (Session["ClearingAgentServices"] == null || Session["OrganizationServices"] == null)//Using session will cause data/User preference loss issue for a extended peiod of stay in same page
                {
                    return RedirectToAction("Start");

                }


                if (ServiceType == 1)
                {
                    ViewBag.ServiceType = "1";
                }
                else if (ServiceType == 2)
                {

                    ViewBag.ServiceType = "2";
                }
                else
                {
                    return RedirectToAction("Start2");

                }
                ViewBag.modelerror = Constantclass.number1;
                return View();
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        public ActionResult BrokerServices1()
        {
            Session["ClearingAgentServices"] = true;
            Session["OrganizationServices"] = false;
            return RedirectToAction("Index2", new { ServiceType = 1 });
        }
        public ActionResult StakeholderServices1()
        {

            //return RedirectToAction("BrokerServices", "registration");

            Session["ClearingAgentServices"] = false;
            Session["OrganizationServices"] = true;
            return RedirectToAction("Index2", new { ServiceType = 2 });

        }
        #endregion Abdul Karim 26 NOV 2019



        //// Created at 07 October  by Abdul Karim
        //public ActionResult StartNew()
        //{
        //    return View();
        //}



        public ActionResult BrokerServices()
        {
            Session["ClearingAgentServices"] = true;
            Session["OrganizationServices"] = false;
            return RedirectToAction("Index", new { ServiceType = 1 });
        }
        public ActionResult StakeholderServices()
        {

            //return RedirectToAction("BrokerServices", "registration");

            Session["ClearingAgentServices"] = false;
            Session["OrganizationServices"] = true;
            return RedirectToAction("Index", new { ServiceType = 2 });

        }

        /*code for tracking client ip by azhar
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        */
        public ActionResult LoginForBrokerServices()
        {
            
            Session["ClearingAgentServices"] = true;
            Session["OrganizationServices"] = false;
            return RedirectToAction("Index");
        }
        public ActionResult LoginForStakeholderServices()
        {

            //return RedirectToAction("BrokerServices", "registration");

            Session["ClearingAgentServices"] = false;
            Session["OrganizationServices"] = true;
            return RedirectToAction("Index");

        }
        public ActionResult RegisterForBrokerServices()
        {
            Session["ClearingAgentServices"] = true;
            Session["OrganizationServices"] = false;
            return RedirectToAction("UserRegistration");

        }
        public ActionResult RegisterForStakeholderServices()
        {
            Session["ClearingAgentServices"] = false;
            Session["OrganizationServices"] = true;
            return RedirectToAction("UserRegistration");

        }

        public ActionResult Index(int? ServiceType)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    return RedirectToAction("LogOut");
                }

                if (Session["ClearingAgentServices"] == null || Session["OrganizationServices"] == null)//Using session will cause data/User preference loss issue for a extended peiod of stay in same page
                {
                    return RedirectToAction("Start");
                    //return RedirectToAction("BrokerServices", "registration");
                }

                //if (ViewBag.ServiceType !="1" && ViewBag.ServiceType!="2")//Using session will cause data/User preference loss issue for a extended peiod of stay in same page
                //{
                //    return RedirectToAction("Start");
                //}

                if (ServiceType == 1)
                {
                    ViewBag.ServiceType = "1";
                }
                else if (ServiceType == 2)
                {

                    ViewBag.ServiceType = "2";
                }
                else
                {
                    return RedirectToAction("Start");
                    //return RedirectToAction("BrokerServices", "registration");
                }

                //if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                //{
                //    ViewBag.UserName = Request.Cookies["UserName"].Value;
                //    ViewBag.Pwd = Request.Cookies["Password"].Value;
                //}
                //TempData.Clear();
                //Session.Clear();
                ViewBag.modelerror = Constantclass.number1;
                return View();
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;

                return View();

            }
        }





        [ValidateAntiForgeryToken]
        [HttpPost]                                   // Abdul Karim
        public ActionResult Index(FormCollection frm, string LoginForStakeholderServices, string LoginForBrokerServices, FormCollection fc)//(LogOnRequest objlogon)
        {
            try
            {
               // string x = GetIPAddress();
              //  WriteToLogFile(x, "mytest");

                if (frm["ServiceType"] != null)
                {
                    ViewBag.ServiceType = frm["ServiceType"].ToString();
                }
                else
                {
                    return RedirectToAction("Start");
                    //return RedirectToAction("BrokerServices", "registration");
                }

                ViewBag.TempEmail = frm["Username"].ToString();


                if (Session["CaptchaCode"] != null)
                {
                    if (frm["CaptchaValue"] != null)
                    {
                        if (Session["CaptchaCode"].ToString() != frm["CaptchaValue"].ToString().ToUpper())// (this.IsCaptchaValid("Validate your captcha"))
                        {
                            ViewBag.Msg = Resources.Resource.EntercorrectCaptcha;
                            ViewBag.showMessageStyle = "block";
                            ViewBag.modelerror = Constantclass.number5;
                            //Session.Remove("CaptchaCode");
                            //  CommonFunctions.LogUserActivity("User Is trying To acessSSystemwith IncorrectCaptcha", "", "", "", "", Session["CaptchaCode"].ToString());
                            return View("Index");
                            //return RedirectToAction("Index", new { ServiceType = ViewBag.ServiceType });
                        }
                        else
                        {
                            Session.Remove("CaptchaCode");
                        }
                    }
                }
                else
                {
                    if (Session["CaptchaCode"] == null)
                    {
                        Session["CaptchaCode"] = frm["CaptchaValue"].ToString().ToUpper();
                    }

                    //                    return RedirectToAction("Start");
                    //return RedirectToAction("BrokerServices", "registration");
                }


                //if (Session["ClearingAgentServices"] == null || Session["OrganizationServices"] == null)//Using session will cause data/User preference loss issue for a extended peiod of stay in same page
                //{
                //    return RedirectToAction("Start");

                //}

                LogOnRequest objlogon = new LogOnRequest(); // Abdul Karim
                ViewBag.showMessageStyle = "none";
                ViewBag.Msg = "";


                if (frm["Username"] == null || frm["password"] == null)
                {
                    return View("Index");
                    //return RedirectToAction("Index");
                }


                if (!frm["Username"].ToString().Contains("@") ||
                    String.IsNullOrWhiteSpace(frm["Username"].ToString()))//||
                                                                          //String.IsNullOrWhiteSpace(frm["password"].ToString()))
                {
                    ViewBag.Msg = Resources.Resource.validemail;
                    ViewBag.showMessageStyle = "block";
                    ViewBag.modelerror = Constantclass.number5;

                    return View("Index");
                    //return RedirectToAction("Index");
                }
                // //if (!frm["Username"].ToString().Contains("@") ||
                // //   String.IsNullOrWhiteSpace(frm["Username"].ToString()) ||
                //if(    String.IsNullOrWhiteSpace(frm["password"].ToString()))
                // {
                //     ViewBag.Msg = Resources.Resource.Pleaseenterpassword;
                //     ViewBag.showMessageStyle = "block";
                //     ViewBag.modelerror = Constantclass.number5;

                //     return View("Index");
                //     //return RedirectToAction("Index");
                // }


                String email = frm["Username"].ToString();
                String password = frm["password"].ToString();

                Session["tempEmail"] = frm["Username"].ToString();
                ViewBag.TempEmail = frm["Username"].ToString();
                TempData.Clear();
                //Session.Clear();


                ViewBag.modelerror = Constantclass.number1;
                TempData["MyOrgData"] = null;
                TempData["data"] = null;
                TempData["MyOrgReqData"] = null;
                TempData["Paymentdata"] = null;
                Session["notifycount"] = null;
                TempData["Notifylist"] = null;

                // Abdul Karim
                HttpCookie langCookie = Request.Cookies["culture"];
                objlogon.Lang = langCookie.Value;
                objlogon.email = email;
                objlogon.pwd = password;
                objlogon.RememberMe = false;
                //objlogon.Lang = Session["lng"] != null ? Session["lng"].ToString() : "";



                if (objlogon.gln != null)
                {
                    Session["Geolocation"] = objlogon.gln;
                }
                if (!(ModelState.IsValid))
                {
                    ViewBag.modelerror = Constantclass.number;
                    ViewBag.UserName = objlogon.email;
                    return View("Index");
                }

                Session["AnonymousUser"] = null;

                DataTable dtlogondetails = objdataclass.Logonmethod(objlogon);

                if (objlogon.RememberMe)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["UserName"].Value = null;//objlogon.email;
                    Response.Cookies["Password"].Value = null;//objlogon.pwd;
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddYears(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddYears(-1);
                }
                ViewBag.ok = "0";
                if (dtlogondetails.Rows.Count > 0)
                {
                    if (String.IsNullOrWhiteSpace(dtlogondetails.Rows[0]["UserId"].ToString()))
                    {

                        ViewBag.Msg = Resources.Resource.Userdoesnotexist;
                        ViewBag.showMessageStyle = "block";

                        return View("Index");
                    }


                    // This Mean Email Not Verified 
                    if (!String.IsNullOrWhiteSpace(dtlogondetails.Rows[0]["UserId"].ToString()) &&
                        !String.IsNullOrWhiteSpace(dtlogondetails.Rows[0]["IsEmailVerified"].ToString()) &&
                       //( dtlogondetails.Rows[0]["IsEmailVerified"].ToString() != "1" || dtlogondetails.Rows[0]["IsMobileVerified"].ToString() != "1") &&
                       (dtlogondetails.Rows[0]["IsEmailVerified"].ToString() != "0" || dtlogondetails.Rows[0]["IsMobileVerified"].ToString() != "0") &&
                        dtlogondetails.Rows[0]["Status"].ToString() != "66")//-66 to indicate that the email is not verified and also user didnt enter the right password to proceed for email verification//!= "-1")
                    {
                        TempData["EmailKeyValTemporary"] = 1;
                        TempData["TokenId"] = dtlogondetails.Rows[0]["TokenId"].ToString();
                        TempData["UserId"] = dtlogondetails.Rows[0]["UserId"].ToString();

                        bool EmailVerificRequired = dtlogondetails.Rows[0]["IsEmailVerified"].ToString() != "0" ? true : false;
                        bool MobileVerificRequired = dtlogondetails.Rows[0]["IsMobileVerified"].ToString() != "0" ? true : false;


                        TempData.Keep();

                        //return RedirectToAction("EmailVerification");
                        //if (dtlogondetails.Rows[0]["Status"].ToString() == "0")//USER MUST ENTER CORRECT CREDENTIALS EVEN FOR THE FIRST TIME TO PROCEED FOR CONTACT VERIFICATION
                        {
                            return RedirectToAction("ContactVerification", new { EmailVerific = EmailVerificRequired, MobileVerific = MobileVerificRequired });
                        }
                    }

                    if (dtlogondetails.Rows[0]["TokenId"].ToString() != "" &&
                        dtlogondetails.Rows[0]["Status"].ToString() == "0")
                    {


                        if (dtlogondetails.Rows[0]["LegalEntity"].ToString()
                         //  != frm["ServiceType"].ToString()) //fc.AllKeys.Contains("LoginForStakeholderServices"))//Display msg that Broker services user dont have Org services
                         != frm["ServiceType"].ToString() && dtlogondetails.Rows[0]["LegalEntity"].ToString() == "1") //fc.AllKeys.Contains("LoginForStakeholderServices"))//Display msg that Broker services user dont have Org services

                        {
                            //ViewBag.Msg = Resources.Resource.OrgServicesNotAllowed;
                            //ViewBag.modelerror = Constantclass.number5;
                            ////ViewBag.CrossAccess = "0";
                            //ViewBag.Showmsg = Constantclass.number;



                            ViewBag.Msg = Resources.Resource.OrgServicesNotAllowed;
                            ViewBag.showMessageStyle = "block";
                            ViewBag.modelerror = Constantclass.number5;

                            return View("Index");
                        }


                        String userIdforMulipleSessions = dtlogondetails.Rows[0]["UserId"].ToString();

                        Dictionary<String, String> UsersDictionaryWithSessionsIds = new Dictionary<String, String>();

                        if (System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] != null)
                        {
                            UsersDictionaryWithSessionsIds = System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] as Dictionary<String, String>;

                            if (UsersDictionaryWithSessionsIds.ContainsKey(userIdforMulipleSessions))
                            {
                                ViewBag.Msg = Resources.Resource.userSessionIsActive;
                                ViewBag.showMessageStyle = "block";
                                ViewBag.modelerror = Constantclass.number99;

                                UsersDictionaryWithSessionsIds.Remove(userIdforMulipleSessions);

                                System.Web.HttpContext.Current.Application.Lock();
                                System.Web.HttpContext.Current.Application.Contents.Remove("UsersDictionaryWithSessionsIds");
                                System.Web.HttpContext.Current.Application.UnLock();


                                System.Web.HttpContext.Current.Application.Lock();
                                System.Web.HttpContext.Current.Application.Contents.Add("UsersDictionaryWithSessionsIds", UsersDictionaryWithSessionsIds);
                                System.Web.HttpContext.Current.Application.UnLock();

                                return View("Index");
                            }

                            else
                            {
                                UsersDictionaryWithSessionsIds.Add(userIdforMulipleSessions, "start");

                                System.Web.HttpContext.Current.Application.Lock();
                                System.Web.HttpContext.Current.Application.Contents.Remove("UsersDictionaryWithSessionsIds");
                                System.Web.HttpContext.Current.Application.UnLock();


                                System.Web.HttpContext.Current.Application.Lock();
                                System.Web.HttpContext.Current.Application.Contents.Add("UsersDictionaryWithSessionsIds", UsersDictionaryWithSessionsIds);
                                System.Web.HttpContext.Current.Application.UnLock();

                            }
                        }





                        #region Last Working

                        //if (System.Web.HttpContext.Current.Application["UsersList"] != null)
                        //{
                        //    ArrayList TempUsersList = System.Web.HttpContext.Current.Application["UsersList"] as ArrayList;

                        //    if (TempUsersList.Contains(userIdforMulipleSessions))
                        //    {
                        //        ViewBag.Msg = Resources.Resource.userSessionIsActive;
                        //        ViewBag.showMessageStyle = "block";
                        //        ViewBag.modelerror = Constantclass.number99;

                        //        TempUsersList.Remove(userIdforMulipleSessions);

                        //        UsersList = (ArrayList)TempUsersList.Clone();

                        //        System.Web.HttpContext.Current.Application.Lock();
                        //        System.Web.HttpContext.Current.Application.Contents.Remove("UsersList");
                        //        System.Web.HttpContext.Current.Application.UnLock();


                        //        System.Web.HttpContext.Current.Application.Lock();
                        //        System.Web.HttpContext.Current.Application.Contents.Add("UsersList", UsersList);
                        //        System.Web.HttpContext.Current.Application.UnLock();

                        //        return View("Index");
                        //    }

                        //    else
                        //    {
                        //        TempUsersList.Add(userIdforMulipleSessions);

                        //        UsersList = (ArrayList)TempUsersList.Clone();

                        //        System.Web.HttpContext.Current.Application.Lock();
                        //        System.Web.HttpContext.Current.Application.Contents.Remove("UsersList");
                        //        System.Web.HttpContext.Current.Application.UnLock();


                        //        System.Web.HttpContext.Current.Application.Lock();
                        //        System.Web.HttpContext.Current.Application.Contents.Add("UsersList", UsersList);
                        //        System.Web.HttpContext.Current.Application.UnLock();

                        //    }
                        //}
                        #endregion Last Working



                        regenerateId();

                        //if(fc.AllKeys.Contains("LoginForStakeholderServices.x"))

                        if (fc.AllKeys.Contains("LoginForStakeholderServices.x"))//!string.IsNullOrEmpty(LoginForStakeholderServices))
                        {
                            Session["ClearingAgentServices"] = false;
                            Session["OrganizationServices"] = true;
                        }
                        if (fc.AllKeys.Contains("LoginForBrokerServices.x"))//!string.IsNullOrEmpty(LoginForBrokerServices))
                        {
                            Session["ClearingAgentServices"] = true;
                            Session["OrganizationServices"] = false;
                        }
                        Session["ServicePreference"] = Convert.ToBoolean(Session["ClearingAgentServices"]) ? "CA" : "ORG";

                        Session["UserName"] = dtlogondetails.Rows[0]["FirstName"].ToString().ToUpper() + " " + dtlogondetails.Rows[0]["LastName"].ToString().ToUpper();
                        Session["UserId"] = dtlogondetails.Rows[0]["UserId"].ToString();
                        Session["TokenId"] = dtlogondetails.Rows[0]["TokenId"].ToString();
                        Session["AllowedMenus"] = dtlogondetails.Rows[0]["AllowedMenus"].ToString();
                        Session["UserOrgID"] = dtlogondetails.Rows[0]["OrgID"].ToString();// Siraj
                        Session["isAdmin"] = dtlogondetails.Rows[0]["isAdmin"].ToString();// Siraj
                        Session["isSubAdmin"] = dtlogondetails.Rows[0]["isSubAdmin"].ToString();// Siraj
                        Session["LegalEntity"] = dtlogondetails.Rows[0]["LegalEntity"].ToString();// Siraj
                        Session["LicenseNumber"] = dtlogondetails.Rows[0]["LicenseNumber"].ToString();// Siraj
                        Session["MicroClearUsername"] = dtlogondetails.Rows[0]["MC_UserId"].ToString();// Siraj
                        Session["eServiceUserEmailId"] = dtlogondetails.Rows[0]["EmailId"].ToString();// Siraj

                        Session["Themes"] = dtlogondetails.Rows[0]["UserThemes"].ToString();// Siraj


                        Session["FirstName"] = dtlogondetails.Rows[0]["FirstName"].ToString();// Abdul Karim
                        Session["LastName"] = dtlogondetails.Rows[0]["LastName"].ToString();// Abdul Karim
                        Session["genderOfLoggedInUser"] = dtlogondetails.Rows[0]["Gender"].ToString();


                        Session["Governorate"] = dtlogondetails.Rows[0]["Governorate"].ToString();// Siraj
                        Session["Region"] = dtlogondetails.Rows[0]["Region"].ToString();// Siraj
                        Session["GovernorateEng"] = dtlogondetails.Rows[0]["GovernorateEng"].ToString();// Siraj
                        Session["RegionEng"] = dtlogondetails.Rows[0]["RegionEng"].ToString();// Siraj
                        Session["GovernorateAra"] = dtlogondetails.Rows[0]["GovernorateAra"].ToString();// Siraj
                        Session["RegionAra"] = dtlogondetails.Rows[0]["RegionAra"].ToString();// Siraj
                        Session["PostalCode"] = dtlogondetails.Rows[0]["PostalCode"].ToString();// Siraj
                        Session["Address"] = dtlogondetails.Rows[0]["Address"].ToString();// Siraj
                        Session["Block"] = dtlogondetails.Rows[0]["Block"].ToString();// Siraj
                        Session["Street"] = dtlogondetails.Rows[0]["Street"].ToString();// Siraj
                        Session["IndustrialLicenseNumber"] = dtlogondetails.Rows[0]["IndustrialLicenseNumber"].ToString();// Siraj
                        Session["CommercialLicenseNumber"] = dtlogondetails.Rows[0]["CommercialLicenseNumber"].ToString();// Siraj
                        Session["IsIndustrial"] = Convert.ToBoolean(dtlogondetails.Rows[0]["IsIndustrial"]);// Siraj
                        Session["CivilId"] = dtlogondetails.Rows[0]["CivilId"].ToString();// Siraj
                        Session["CommercialLicenseYear"] = dtlogondetails.Rows[0]["CommercialLicenseYear"].ToString();// Siraj
                        Session["CompanyCivilID"] = dtlogondetails.Rows[0]["CompanyCivilID"].ToString();// Siraj
                        Session["CompanyName"] = dtlogondetails.Rows[0]["MociCompanyName"].ToString();// Siraj

                        Session["SideBarState"] = "active";
                        CommonFunctions.LogUserActivity(

                      "UserLoggedIn", "UserSignIN", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "", "", "");


                        if (dtlogondetails.Rows[0]["LegalEntity"] != null)
                        {
                            Session["LegalEntity"] = dtlogondetails.Rows[0]["LegalEntity"].ToString();
                            Session["LicenseNumberOfLoggedInUser"] = dtlogondetails.Rows[0]["LicenseNumber"].ToString();
                            Session["CivilIdOfLoggedInUser"] = dtlogondetails.Rows[0]["CivilId"].ToString();
                            Session["emailOfLoggedInUser"] = email;
                            Session["mobileNumberOfLoggedInUser"] = dtlogondetails.Rows[0]["MobileNumber"].ToString();

                            int UserIdtoGetServices = int.Parse(Session["UserId"].ToString());
                            ReqObj serviceSubscription = new ReqObj { ParentID = UserIdtoGetServices, ChildUser = 0, CommonData = Session["ServicePreference"].ToString() };

                            //DataTable dataTable = objdataclass.GETParentUserActiveServices(serviceSubscription);
                            DataTable dataTable = objdataclass.GETParentUserActiveServices(serviceSubscription, false);

                            Session.Add("serviceSubscriptionDataTable", dataTable);
                            if (Session["LegalEntity"].ToString() == "2")
                            {
                                if (dataTable != null)
                                {
                                    if (dataTable.Rows.Count > 0)
                                    {
                                        String OrgRegServiceId = System.Configuration.ConfigurationManager.AppSettings["OrganizationRegistrationService"].ToString();
                                        DataRow OrgCreationAllowed = dataTable.AsEnumerable().Where
                         (row => row.Field<Int64>("ServiceID") == Int64.Parse(OrgRegServiceId)).FirstOrDefault();
                                        Session["OrgRegistrationAllowed"] = OrgCreationAllowed != null ? true : false;

                                    }
                                }
                            }
                            if (Session["LegalEntity"].ToString() == "1" || Session["LegalEntity"].ToString() == "2")
                            {
                                if (dataTable != null)
                                {
                                    if (dataTable.Rows.Count > 0)
                                    {
                                        String RenewalServiceId = System.Configuration.ConfigurationManager.AppSettings["RenewalService"].ToString();
                                        DataRow RenewalServiceAllowed = dataTable.AsEnumerable().Where
                         (row => row.Field<Int64>("ServiceID") == Int64.Parse(RenewalServiceId)).FirstOrDefault();
                                        Session["RenewalServiceAllowed"] = RenewalServiceAllowed != null ? true : false;

                                    }
                                }
                            }

                      
                            SessionIDManager manger = new SessionIDManager();
                            var guId = Guid.NewGuid();

                            String newId = manger.CreateSessionID(System.Web.HttpContext.Current);

                            String additionalId = guId + GenerateRandomWord(5);



                            Session["AuthToken"] = newId;
                            Session["additionalId"] = additionalId;


                            HttpCookie AuthenticationCookie = new HttpCookie("Nice", newId);
                            AuthenticationCookie.HttpOnly = true;
                            Response.Cookies.Add(AuthenticationCookie);

                            HttpCookie AuthenticationCookie1 = new HttpCookie("toMeetYou", additionalId);
                            AuthenticationCookie1.HttpOnly = true;
                            Response.Cookies.Add(AuthenticationCookie1);


                            String random = GenerateRandomWord(10) + GetRandomText();
                            HttpCookie AuthenticationCookie2 = new HttpCookie("kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd", random);
                            AuthenticationCookie2.HttpOnly = true;
                            Response.Cookies.Add(AuthenticationCookie2);


                            HttpCookie AuthenticationCookie3 = new HttpCookie("catchMeIfYouCan", Guid.NewGuid().ToString());
                            AuthenticationCookie3.HttpOnly = true;
                            Response.Cookies.Add(AuthenticationCookie3);

                            if (System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] != null)
                            {
                                if (UsersDictionaryWithSessionsIds.ContainsKey(userIdforMulipleSessions))
                                {
                                    if (UsersDictionaryWithSessionsIds[userIdforMulipleSessions] == "start")
                                    {
                                        UsersDictionaryWithSessionsIds[userIdforMulipleSessions] = newId;
                                    }
                                }
                            }
                            else
                            {
                                UsersDictionaryWithSessionsIds.Add(userIdforMulipleSessions, newId);

                                System.Web.HttpContext.Current.Application.Lock();
                                System.Web.HttpContext.Current.Application.Contents.Add("UsersDictionaryWithSessionsIds", UsersDictionaryWithSessionsIds);
                                System.Web.HttpContext.Current.Application.UnLock();
                            }


                            if (dtlogondetails.Rows[0]["LegalEntity"].ToString() == "1") // Clearning Agent
                            {
                                ViewBag.showMessageStyle = "none";
                                Session["AllowmenuAfterAcountUpdate"] = "True";
                                ViewBag.Msg = "";
                                //return RedirectToAction("BrokerRenewal", "BrokerRenewal");
                                return RedirectToAction("MenuView", "Dashboard");
                                //return RedirectToAction("BrokerServiceRequestUpdates", "BrokerRenewal");
                            }
                            else if (dtlogondetails.Rows[0]["LegalEntity"].ToString() == "2") // Organization
                            {
                                ViewBag.showMessageStyle = "none";
                                ViewBag.Msg = "";
                                if (dtlogondetails.Columns.Contains("CivilId") && dtlogondetails.Columns.Contains("LicenseNumber"))
                                {

                                    if (dtlogondetails.Rows[0]["CivilId"].ToString() == "" || dtlogondetails.Rows[0]["LicenseNumber"].ToString() == "" || dtlogondetails.Rows[0]["CivilId"].ToString() == "NULL" || dtlogondetails.Rows[0]["LicenseNumber"].ToString() == "NULL")
                                    {

                                        Session["AllowmenuAfterAcountUpdate"] = "False";

                                        return RedirectToAction("MyAccount", "User");

                                    }

                                    else
                                    {
                                        Session["AllowmenuAfterAcountUpdate"] = "True";
                                    }
                                }
                                //return RedirectToAction("MyOrganizations", "User");
                                return RedirectToAction("MenuView", "Dashboard");
                            }
                        }
                    }

                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-2")
                    //  Account Locked  
                    {
                        ViewBag.Msg = Resources.Resource.ActioncannotbeperformedPleasecontactadministrator;
                        ViewBag.showMessageStyle = "block";
                        ViewBag.modelerror = Constantclass.number5;
                    }
                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-3")
                    //  Password Mismatch  
                    {
                        ViewBag.Msg = Resources.Resource.wrongcredentials;
                        ViewBag.showMessageStyle = "block";
                        ViewBag.modelerror = Constantclass.number5;
                    }
                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-5")
                    //Request User To reset Pwd as account has more Invalid Attempts
                    {
                        ViewBag.ok = "1";
                        ViewBag.Msg = Resources.Resource.Multipleinvalidpassword;
                        ViewBag.showMessageStyle = "block";
                        ViewBag.modelerror = Constantclass.number5;
                    }
                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-1" || dtlogondetails.Rows[0]["Status"].ToString() == "66")
                    {
                        ViewBag.Msg = Resources.Resource.wrongcredentials;
                        ViewBag.showMessageStyle = "block";
                        ViewBag.modelerror = Constantclass.number5;
                    }
                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "00")
                    // "Account confirmation is under processing";
                    {
                        ViewBag.Msg = Resources.Resource.AccountConfirmationisunderprocessing;
                        ViewBag.showMessageStyle = "block";
                        ViewBag.modelerror = Constantclass.number5;
                    }
                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-133")

                    {
                        ViewBag.Msg = Resources.Resource.AlreadyRegisteredOrgLicense;
                        ViewBag.showMessageStyle = "block";
                        ViewBag.modelerror = Constantclass.number5;
                    }
                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-134")

                    {
                        ViewBag.Msg = Resources.Resource.AlreadyRegisteredOrgLicense;
                        ViewBag.showMessageStyle = "block";
                        ViewBag.modelerror = Constantclass.number5;
                    }
                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-51")

                    {
                        ViewBag.Msg = Resources.Resource.IndustrialLicenseAlreadyRegistered;
                        ViewBag.showMessageStyle = "block";
                        ViewBag.modelerror = Constantclass.number5;
                    }
                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-52")

                    {
                        ViewBag.Msg = Resources.Resource.CommercialLicenseAlreadyRegistered;
                        ViewBag.showMessageStyle = "block";
                        ViewBag.modelerror = Constantclass.number5;
                    }
                    return View("Index");
                }
                else
                {
                    // ModelState.AddModelError(string.Empty, Resources.Resource.Servernotresponding);
                    ViewBag.Msg = Resources.Resource.Servernotresponding;
                    ViewBag.modelerror = Constantclass.number5;
                    return View("Index");
                }

            }
            catch (Exception ex)
            {
                String _Exception = ex.ToString();
                //StringBuilder sb = new StringBuilder();
                //for (Exception eCurrent = ex; eCurrent != null; eCurrent = eCurrent.InnerException)
                //{
                //    sb.AppendLine(eCurrent.Message + "\n at " + eCurrent.Source + " , \n Type : " + eCurrent.GetType().Name + " , \n Trace: " + eCurrent.StackTrace);
                //}
                CommonFunctions.LogUserActivity("LogonCall Exception", "", "", "", "", _Exception + ex.StackTrace.ToString());
                ViewBag.modelerror = Constantclass.number5;
                //ViewBag.Msg = sb.ToString();// Resources.Resource.somethingwentwrong;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                ViewBag.showMessageStyle = "block";

                return View("Index");
            }
        }
        private Captcha getcaptcha(int captchaid)
        {
            try
            {
                Captcha objcaptcha = new Captcha();
                objcaptcha.CaptchaId = captchaid;
                Captcha objCaptchadetails = objdataclass.GetCaptcha(objcaptcha);
                if (objCaptchadetails != null)
                {
                    ViewData["CaptchaImage"] = objCaptchadetails.CaptchaImage;
                    TempData["CaptchaImage"] = objCaptchadetails.CaptchaImage;
                    TempData["CaptchaId"] = objCaptchadetails.CaptchaId;
                    ViewData["CaptchaId"] = objCaptchadetails.CaptchaId;

                }
                else
                {
                    ViewData["CaptchaImage"] = "";
                    TempData["CaptchaImage"] = "";
                    TempData["CaptchaId"] = "0";
                    ViewData["CaptchaId"] = "0";
                }
                return objCaptchadetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UserRegistration()
        {
            try
            {
                if (Session["ClearingAgentServices"] == null || Session["OrganizationServices"] == null)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.SelectedEntity = Convert.ToBoolean(Session["ClearingAgentServices"]) ? "1" : "2";
                }

                Session["SideBarState"] = "active";
                Session["Themes"] = "Blue";
                List<LegalEntity> uts = new List<LegalEntity>();
                uts.Add(new LegalEntity { LegalEntityID = 0, LegalEntityName = Resources.Resource.PleaseselectaLegalEntity });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                uts.Add(new LegalEntity { LegalEntityID = 1, LegalEntityName = Resources.Resource.ClearingAgent });//"Clearing Agent" }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                uts.Add(new LegalEntity { LegalEntityID = 2, LegalEntityName = Resources.Resource.Organization });//"Organization" });// Resources.Resource.CaptchaCode });// "Messenger" });

                ViewBag.UserType = uts;

                List<Gender> G = new List<Gender>();
                G.Add(new Gender { GenderTypeID = 0, GenderTypeValue = Resources.Resource.GenderRequired });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                G.Add(new Gender { GenderTypeID = 1, GenderTypeValue = Resources.Resource.male }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                G.Add(new Gender { GenderTypeID = 2, GenderTypeValue = Resources.Resource.female });// Resources.Resource.CaptchaCode });// "Messenger" });


                ViewBag.GenderType = G;

                List<Nationality> N = new List<Nationality>();
                N.Add(new Nationality { NationalityID = 0, NationalityValue = Resources.Resource.nationailityRequired });// Resources.Resource.CaptchaCode });// "Please select a UserType" });

                N.Add(new Nationality { NationalityID = 5150, NationalityValue = Resources.Resource.kuwaiti });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                N.Add(new Nationality { NationalityID = 5027, NationalityValue = Resources.Resource.bahaini }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                N.Add(new Nationality { NationalityID = 5203, NationalityValue = Resources.Resource.omani });// Resources.Resource.CaptchaCode });// "Messenger" });

                N.Add(new Nationality { NationalityID = 5274, NationalityValue = Resources.Resource.emirati });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                N.Add(new Nationality { NationalityID = 5220, NationalityValue = Resources.Resource.qatari }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                N.Add(new Nationality { NationalityID = 5228, NationalityValue = Resources.Resource.saudi });// Resources.Resource.CaptchaCode });// "Messenger" });

                ViewBag.NationalityType = N;
                List<LegalEntity> LEST = new List<LegalEntity>();
                LEST.Add(new LegalEntity { LegalEntityID = 0, LegalEntityName = Resources.Resource.PleaseselectaLegalEntity });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                LEST.Add(new LegalEntity { LegalEntityID = 1, LegalEntityName = "Trader" });//"Clearing Agent" }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                LEST.Add(new LegalEntity { LegalEntityID = 2, LegalEntityName = "Courier" });//"Organization" });// Resources.Resource.CaptchaCode });// "Messenger" });


                ViewBag.LegalEntitySubType = LEST;


                //List<GovernorateRegions> Governorate = new List<GovernorateRegions>();
                ////Governorate.Add(new GovernorateRegions { GovernorateID = 0, GovernorateName = Resources.Resource.Governorate });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                //Governorate.Add(new GovernorateRegions { GovernorateID = 0, GovernorateName = "* " + Resources.Resource.GovernorateRequired });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                //Governorate.Add(new GovernorateRegions { GovernorateID = 1, GovernorateName = "Hawalli" });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                //Governorate.Add(new GovernorateRegions { GovernorateID = 2, GovernorateName = "Jleeb" });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                //Governorate.Add(new GovernorateRegions { GovernorateID = 3, GovernorateName = "City" });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });

                //ViewBag.Governorate = Governorate;

                List<GovernorateRegions> GovernRegions = new List<GovernorateRegions>();
                //GovernRegions.Add(new GovernorateRegions { GovernorateID = 0, GovernorateName = Resources.Resource.Governorate });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                GovernRegions.Add(new GovernorateRegions { RegionID = 0, RegionName = "* " + Resources.Resource.RegionRequired });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });

                ViewBag.GovernRegions = GovernRegions;


                DataTable GovernorateDetails = objdataclass.GetGovernorates();

                List<GovernorateRegions> Governorates = new List<GovernorateRegions>();

                string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                bool EnglishCulture = culture.Contains("English");

                var a = from x in GovernorateDetails.AsEnumerable()
                        select new GovernorateRegions
                        {
                            GovernorateID = Convert.ToInt32(x["GovernorateID"]),
                            GovernorateName = EnglishCulture ? x["GovernorateEng"].ToString() : x["GovernorateAra"].ToString()
                        };
                Governorates = a.ToList();
                GovernorateRegions Gvrnrt = new GovernorateRegions { GovernorateID = 0, GovernorateName = "* " + Resources.Resource.GovernorateRequired };
                Governorates.Insert(0, Gvrnrt);
                ViewBag.Governorate = Governorates;



                User u = new User();
                u.ParentID = 0;
                u.SubUser = false;
                u.SelectedServices = "";
                u.SelectedOrganizations = "";

                //CommonFunctions.LogUserActivity("UserRegistration", "", "", "", "", "");
                return View(u);
            }
            catch (Exception e)
            {
                WriteToLogFile(e.Message, "UserRegistrationGET METHOD");
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;

                CommonFunctions.LogUserActivity("UserRegistration", "", "", "", "", e.Message.ToString());
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserRegistration(User objUser, FormCollection form)
        {

            try
            {
                if (Session["ClearingAgentServices"] == null || Session["OrganizationServices"] == null)
                {

                    ViewBag.Status = Resources.Resource.somethingwentwrong;
                    return Json(new { StatusCode = 88, message = Resources.Resource.somethingwentwrong });
                    //return RedirectToAction("Index");

                }
                if (objUser.LegalEntity == "1")
                {
                    if (objUser.GeneralBrokerLicenseNumber != null)
                    {
                        objUser.ExistingUser = true;
                    }

                    if (!Convert.ToBoolean(Session["ClearingAgentServices"]))
                    {
                        ViewBag.Status = Resources.Resource.somethingwentwrong;
                        return Json(new { StatusCode = 88, message = Resources.Resource.somethingwentwrong });
                        //return RedirectToAction("Index");
                    }
                }
                if (objUser.LegalEntity == "2")
                {

                    if (!Convert.ToBoolean(Session["OrganizationServices"]))
                    {
                        ViewBag.Status = Resources.Resource.somethingwentwrong;
                        return Json(new { StatusCode = 88, message = Resources.Resource.somethingwentwrong });
                        //return RedirectToAction("Index");
                    }
                }
                //else if (!(objUser.LegalEntity == "1" && Convert.ToBoolean(Session["ClearingAgentServices"]) )|| !(objUser.LegalEntity == "2" && Convert.ToBoolean(Session["OrganizationServices"])))//Session["ClearingAgentServices"] == null || Session["OrganizationServices"] == null)                  
                //{
                //    return RedirectToAction("Index");
                //}
                //string S = form["SelectedServices"].ToString();

                List<LegalEntity> uts = new List<LegalEntity>();
                uts.Add(new LegalEntity { LegalEntityID = 0, LegalEntityName = Resources.Resource.PleaseselectaLegalEntity });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                uts.Add(new LegalEntity { LegalEntityID = 1, LegalEntityName = Resources.Resource.ClearingAgent });//"Clearing Agent" }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                uts.Add(new LegalEntity { LegalEntityID = 2, LegalEntityName = Resources.Resource.Organization });//"Organization" });// Resources.Resource.CaptchaCode });// "Messenger" });

                ViewBag.UserType = uts;

                List<Gender> G = new List<Gender>();
                G.Add(new Gender { GenderTypeID = 0, GenderTypeValue = Resources.Resource.GenderRequired });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                G.Add(new Gender { GenderTypeID = 1, GenderTypeValue = Resources.Resource.male }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                G.Add(new Gender { GenderTypeID = 2, GenderTypeValue = Resources.Resource.female });// Resources.Resource.CaptchaCode });// "Messenger" });


                ViewBag.GenderType = G;

                List<Nationality> N = new List<Nationality>();
                N.Add(new Nationality { NationalityID = 5150, NationalityValue = Resources.Resource.kuwaiti });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                N.Add(new Nationality { NationalityID = 5027, NationalityValue = Resources.Resource.bahaini }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                N.Add(new Nationality { NationalityID = 5203, NationalityValue = Resources.Resource.omani });// Resources.Resource.CaptchaCode });// "Messenger" });

                N.Add(new Nationality { NationalityID = 5274, NationalityValue = Resources.Resource.emirati });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                N.Add(new Nationality { NationalityID = 5220, NationalityValue = Resources.Resource.qatari }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                N.Add(new Nationality { NationalityID = 5228, NationalityValue = Resources.Resource.saudi });// Resources.Resource.CaptchaCode });// "Messenger" });

                ViewBag.NationalityType = N;



                if (objUser.SubUser)
                {
                    ReqObj r = new ReqObj { ParentID = objUser.ParentID, ChildUser = 0, CommonData = Session["ServicePreference"].ToString() };

                    DataTable dt = objdataclass.GETParentUserActiveServices(r, true);

                    var s = from x in dt.AsEnumerable()
                            select new EService
                            {
                                SubscriptionID = Convert.ToInt32(x["ServiceID"]),
                                ServiceID = Convert.ToInt32(x["SubscriptionID"]),
                                ServiceNameEng = x["ServiceNameEng"].ToString(),
                                ServiceNameAra = x["ServiceNameAra"].ToString()
                            };
                    objUser.AvailableEServices = s.ToList();

                    if (Session["TokenId"] != null)
                    {
                        objUser.Token = Session["TokenId"].ToString();
                    }

                }
                ViewBag.SubUser = false;

                if (Session["LicenseNumber"] != null)
                {
                    objUser.LicenceNumber = Session["LicenseNumber"].ToString();
                }

                if (Session["CaptchaCode"].ToString() == objUser.CaptchaValue.ToUpper())// (this.IsCaptchaValid("Validate your captcha"))
                {
                    DataTable objCaptchadetails = objdataclass.SignUp(objUser);
                    Session.Remove("CaptchaCode");
                    if (objCaptchadetails.Rows.Count > 0)
                    {
                        CommonFunctions.LogUserActivity("UserRegistrationStatusCode", "", "", "", "", "Status - " + objCaptchadetails.Rows[0]["Status"].ToString());

                        if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "0")
                        {
                            TempData["EmailKeyValTemporary"] = objCaptchadetails.Rows[0]["EmailKeyValTemporary"].ToString();
                            TempData["TokenId"] = objCaptchadetails.Rows[0]["TokenId"].ToString();
                            TempData["UserId"] = objCaptchadetails.Rows[0]["UserId"].ToString();

                            ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;


                            if (objUser.SubUser)
                            {
                                TempData["ParentUserID"] = objUser.ParentID;
                            }
                            else

                            {
                                TempData["ParentUserID"] = 0;
                            }

                            //return  RedirectToAction("EmailVerification");
                            if (objUser.SubUser)
                                return Json(new { StatusCode = 0, message = Resources.Resource.AdditionalUserRegistrationSuccessfull, UserID = objCaptchadetails.Rows[0]["UserId"].ToString(), URL = "GETChildUsers" });/// "Accountverification?UserID=" + objCaptchadetails.Rows[0]["UserId"].ToString() });
                            else
                                return Json(new { StatusCode = 0, message = "Registration Successfull", UserID = objCaptchadetails.Rows[0]["UserId"].ToString(), URL = "ContactVerification" });/// "Accountverification?UserID=" + objCaptchadetails.Rows[0]["UserId"].ToString() });
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-1")
                        {

                            //ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;
                            ViewBag.Status = Resources.Resource.emailExist;
                            return Json(new { StatusCode = -1, message = Resources.Resource.emailExist });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-9")
                        {

                            //ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;
                            ViewBag.Status = Resources.Resource.validpassword;
                            return Json(new { StatusCode = -9, message = Resources.Resource.validpassword });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-11")
                        {

                            //ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;
                            ViewBag.Status = Resources.Resource.InvalidMCDeatils;
                            return Json(new { StatusCode = -11, message = Resources.Resource.InvalidMCDeatils });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-133")
                        {

                            //ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;
                            ViewBag.Status = Resources.Resource.AlreadyRegisteredLicense;
                            return Json(new { StatusCode = -11, message = Resources.Resource.AlreadyRegisteredLicense });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-134")
                        {

                            //ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;
                            ViewBag.Status = Resources.Resource.AlreadyRegisteredOrgLicense;
                            return Json(new { StatusCode = -11, message = Resources.Resource.AlreadyRegisteredOrgLicense });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-136")
                        {

                            //ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;
                            ViewBag.Status = Resources.Resource.AlreadyRegisteredCivilID;
                            return Json(new { StatusCode = -11, message = Resources.Resource.AlreadyRegisteredCivilID });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-137")
                        {

                            //ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;
                            ViewBag.Status = Resources.Resource.AlreadyRegisteredCompanyCivilID;
                            return Json(new { StatusCode = -11, message = Resources.Resource.AlreadyRegisteredCompanyCivilID });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-801")//Validation failed with MOCI Service
                        {

                            //ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;
                            ViewBag.Status = Resources.Resource.brokercorrectInfo;
                            return Json(new { StatusCode = -11, message = Resources.Resource.brokercorrectInfo });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-802")//Issue in MOCI Service, see user activity table
                        {

                            //ViewBag.Status = Resources.Resource.VerificationsenttoyourEmail;
                            ViewBag.Status = Resources.Resource.somethingwentwrong;
                            CommonFunctions.LogUserActivity("UserRegistration", "", "", "", "", "Issue in MOCI Service  Status =" + objCaptchadetails.Rows[0]["Status"].ToString().Trim());
                            return Json(new { StatusCode = -11, message = Resources.Resource.somethingwentwrong });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-77")
                        {
                            ViewBag.Status = Resources.Resource.somethingwentwrong;
                            CommonFunctions.LogUserActivity("UserRegistration", "", "", "", "", "Additional User Security check Status =" + objCaptchadetails.Rows[0]["Status"].ToString().Trim());
                            return Json(new { StatusCode = -13, message = Resources.Resource.somethingwentwrong });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-51")//provided  @IndustrialLicenseNumber is already registered and approved or is already under process of registration by another user 
                        {
                            ViewBag.Status = Resources.Resource.IndustrialLicenseAlreadyRegistered;
                            return Json(new { StatusCode = -13, message = Resources.Resource.IndustrialLicenseAlreadyRegistered });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-52")//provided  @@CommercialLicenseNumber is already registered and approved or is already under process of registration by another user 
                        {
                            ViewBag.Status = Resources.Resource.CommercialLicenseAlreadyRegistered;
                            return Json(new { StatusCode = -13, message = Resources.Resource.CommercialLicenseAlreadyRegistered });
                            //return View(objUser);
                        }
                        else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-53")//provided  @@CommercialLicenseNumber is already registered and approved or is already under process of registration by another user 
                        {
                            ViewBag.Status = Resources.Resource.ImporterLicenseAlreadyRegistered;
                            return Json(new { StatusCode = -13, message = Resources.Resource.ImporterLicenseAlreadyRegistered });
                            //return View(objUser);
                        }
                        else
                        {
                            ViewBag.Status = Resources.Resource.UnknownStatusinRegistration;
                            return Json(new { StatusCode = -13, message = Resources.Resource.UnknownStatusinRegistration });//UnknownStatusinRegistration
                            //return View(objUser);
                        }
                    }
                    else
                    {

                        //ViewBag.Status = "Processing in complete";
                        CommonFunctions.LogUserActivity("EmptyDataset UserRegistrationStatusCode", "", "", "", "", "Status emptydataset " );

                        ViewBag.Status = Resources.Resource.ProcessincompleteinRegistration;
                        return Json(new { StatusCode = -14, message = Resources.Resource.ProcessincompleteinRegistration });
                        //return View(objUser);
                    }
                    //return View();

                }
                else
                {

                    ViewBag.Status = Resources.Resource.PleaseEntertheCorrectCaptcha;
                    return Json(new { StatusCode = -15, message = Resources.Resource.PleaseEntertheCorrectCaptcha });
                }

            }
            catch (Exception EX)
            {

                WriteToLogFile(EX.Message, "UserRegistration");
                ViewBag.Status = Resources.Resource.Someissuehasoccured;
                CommonFunctions.LogUserActivity("Exception UserRegistrationStatusCode", "", "", "", "", EX.Message.ToString());
                return Json(new { StatusCode = -16, message = Resources.Resource.Someissuehasoccured });
            }


        }

        public ActionResult AdditionalUserRegistration() //(int ParentID)
        {
            try

            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                if (Session["isAdmin"] != null && Session["isSubAdmin"] != null && Session["LegalEntity"].ToString() == "1")//&& Session["LicenseNumber"]!=null)
                                                                                                                            //added newly  && Session["LegalEntity"].ToString() == "1" , user management module is not available for Org user, so hiding the link
                {
                    bool isAdmin = Convert.ToBoolean(Session["isAdmin"].ToString() == "" ? "False" : "True");
                    bool isSubAdmin = Convert.ToBoolean(Session["isSubAdmin"].ToString() == "" ? "False" : "True");//db has null value for old users which comes as ""

                    if ((!isAdmin || !isSubAdmin) && Session["LicenseNumber"].ToString() == "")
                    {
                        return RedirectToAction("UnAuthorizedAction");
                    }
                }

                List<LegalEntity> uts = new List<LegalEntity>();
                uts.Add(new LegalEntity { LegalEntityID = 0, LegalEntityName = Resources.Resource.PleaseselectaLegalEntity });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                uts.Add(new LegalEntity { LegalEntityID = 1, LegalEntityName = Resources.Resource.ClearingAgent });//"Clearing Agent" }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                uts.Add(new LegalEntity { LegalEntityID = 2, LegalEntityName = Resources.Resource.Organization });//"Organization" });// Resources.Resource.CaptchaCode });// "Messenger" });

                ViewBag.UserType = uts;

                List<Gender> G = new List<Gender>();
                G.Add(new Gender { GenderTypeID = 0, GenderTypeValue = Resources.Resource.GenderRequired });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                G.Add(new Gender { GenderTypeID = 1, GenderTypeValue = Resources.Resource.male }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                G.Add(new Gender { GenderTypeID = 2, GenderTypeValue = Resources.Resource.female });// Resources.Resource.CaptchaCode });// "Messenger" });


                ViewBag.GenderType = G;


                List<LegalEntity> LEST = new List<LegalEntity>();
                LEST.Add(new LegalEntity { LegalEntityID = 0, LegalEntityName = Resources.Resource.PleaseselectaLegalEntity });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                LEST.Add(new LegalEntity { LegalEntityID = 1, LegalEntityName = "Trader" });//"Clearing Agent" }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                LEST.Add(new LegalEntity { LegalEntityID = 2, LegalEntityName = "Courier" });//"Organization" });// Resources.Resource.CaptchaCode });// "Messenger" });


                ViewBag.LegalEntitySubType = LEST;


                //List<GovernorateRegions> Governorate = new List<GovernorateRegions>();
                ////Governorate.Add(new GovernorateRegions { GovernorateID = 0, GovernorateName = Resources.Resource.Governorate });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                //Governorate.Add(new GovernorateRegions { GovernorateID = 0, GovernorateName = "* " + Resources.Resource.GovernorateRequired });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                //Governorate.Add(new GovernorateRegions { GovernorateID = 1, GovernorateName = "Hawalli" });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                //Governorate.Add(new GovernorateRegions { GovernorateID = 2, GovernorateName = "Jleeb" });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                //Governorate.Add(new GovernorateRegions { GovernorateID = 3, GovernorateName = "City" });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });

                //ViewBag.Governorate = Governorate;

                List<GovernorateRegions> GovernRegions = new List<GovernorateRegions>();
                //GovernRegions.Add(new GovernorateRegions { GovernorateID = 0, GovernorateName = Resources.Resource.Governorate });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                GovernRegions.Add(new GovernorateRegions { RegionID = 0, RegionName = "* " + Resources.Resource.RegionRequired });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });

                ViewBag.GovernRegions = GovernRegions;


                DataTable GovernorateDetails = objdataclass.GetGovernorates();

                List<GovernorateRegions> Governorates = new List<GovernorateRegions>();

                string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                bool EnglishCulture = culture.Contains("English");

                var a = from x in GovernorateDetails.AsEnumerable()
                        select new GovernorateRegions
                        {
                            GovernorateID = Convert.ToInt32(x["GovernorateID"]),
                            GovernorateName = EnglishCulture ? x["GovernorateEng"].ToString() : x["GovernorateAra"].ToString()
                        };
                Governorates = a.ToList();
                GovernorateRegions Gvrnrt = new GovernorateRegions { GovernorateID = 0, GovernorateName = "* " + Resources.Resource.GovernorateRequired };
                Governorates.Insert(0, Gvrnrt);
                ViewBag.Governorate = Governorates;

                User u = new User();
                int ParentID = 0;

                if (Session["LegalEntity"] != null && Session["LegalEntity"].ToString() == "2")
                {
                    ViewBag.OrgAdmin = "True";
                }

                if (Session["UserId"] != null)
                {
                    ParentID = int.Parse(Session["UserId"].ToString());
                    u.ParentID = ParentID;
                    u.SubUser = true;
                    u.SelectedServices = "";
                    u.SelectedOrganizations = "";
                }

                else
                {
                    return RedirectToAction("Start", "registration");
                }

                ReqObj r = new ReqObj { ParentID = ParentID, ChildUser = 0 };

                DataTable dt = objdataclass.GETParentUserActiveServices(r, true);

                var s = from x in dt.AsEnumerable()
                        where Convert.ToInt32(x["ServiceId"]) != 0
                        && x["LegalEntityType"].ToString() == (Convert.ToBoolean(Session["ClearingAgentServices"]) ? "1" : "2")
                        select new EService
                        {
                            SubscriptionID = Convert.ToInt32(x["ServiceID"]),
                            ServiceID = Convert.ToInt32(x["SubscriptionID"]),
                            ServiceNameEng = x["ServiceNameEng"].ToString(),
                            ServiceNameAra = x["ServiceNameAra"].ToString()

                        };


                DataTable dt1 = objdataclass.GETAdminUserOrganization(r);

                var s1 = from x in dt1.AsEnumerable()
                         select new Organization
                         {
                             OrganizationId = Convert.ToInt32(x["OrganizationId"]),
                             OrgNameEng = x["OrgNameEng"].ToString(),
                             OrgNameAra = x["OrgNameAra"].ToString()
                         };

                u.AvailableEServices = s.ToList();
                u.AvailableOrganizations = s1.ToList();

                return View("UserRegistration", u);
            }
            catch (Exception EX)
            {
                WriteToLogFile(EX.Message, "AdditionalUserRegistration");
                String error = EX.ToString();
                CommonFunctions.LogUserActivity("AdditionalUserRegistration", "", "", "", "", EX.Message.ToString());
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }

        [HttpPost]
        public JsonResult GetCitiesofGovernorate(int GovernorateId)
        {
            ReqObj ReqObj = new ReqObj { CommonData = GovernorateId.ToString() };

            DataTable GovernorateDetails = objdataclass.GovernorateDetails(ReqObj);

            List<GovernorateRegions> GovernorateRegions = new List<GovernorateRegions>();

            string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

            bool EnglishCulture = culture.Contains("English");

            var a = from x in GovernorateDetails.AsEnumerable()
                    select new GovernorateRegions
                    {
                        GovernorateID = Convert.ToInt32(x["GovernorateID"]),
                        GovernorateName = EnglishCulture ? x["GovernorateEng"].ToString() : x["GovernorateAra"].ToString(),
                        RegionID = Convert.ToInt32(x["RegionID"]),
                        RegionName = EnglishCulture ? x["RegionEng"].ToString() : x["RegionAra"].ToString(),
                        PostalCode = x["PostalCode"].ToString()
                    };
            GovernorateRegions = a.ToList();

            //GovernorateRegions.Add(new GovernorateRegions { GovernorateID=1, RegionID = 1, RegionName = "adfdf",PostalCode="234234" });
            //GovernorateRegions.Add(new GovernorateRegions { GovernorateID = 1, RegionID = 2, RegionName = "fghh", PostalCode = "67467" });
            return Json(GovernorateRegions, JsonRequestBehavior.AllowGet);
        }



        // added by azhar for autopoulate MociCodes
        [HttpPost]
        public JsonResult GetCivilIDDetailsFromMoci(string CompanyCivilOrImporterId, string reftype)
        {
            //   ReqObj ReqObj = new ReqObj { CommonData = CompanyCivilId.ToString() };



            User UData;

            if (reftype.Contains("civilid"))
            {
                UData = new User { CompanyCivilId = CompanyCivilOrImporterId.ToString(), ImporterLicenseNumber = null };
            }
            else
            {
                UData = new User { ImporterLicenseNumber = CompanyCivilOrImporterId.ToString(), CompanyCivilId = null };
            }

            DataTable GovernorateDetails = objdataclass.GetCivilIDDetailsFromMoci(UData);

            //string result;
            //using (StringWriter sw = new StringWriter())
            //{
            //    GovernorateDetails.WriteXml(sw);
            //    result = sw.ToString();
            //}

            //  string json = JsonConvert.SerializeObject(GovernorateDetails);


            //  DataTable ds = JsonConvert.DeserializeObject<DataTable>(json);

            List<User> MociDetails = new List<User>();

            string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

            bool EnglishCulture = culture.Contains("English");

            var a = from x in GovernorateDetails.AsEnumerable()
                    select new User
                    {
                        //GovernorateID = Convert.ToInt32(x["GovernorateID"]),
                        //GovernorateName = EnglishCulture ? x["GovernorateEng"].ToString() : x["GovernorateAra"].ToString(),
                        //RegionID = Convert.ToInt32(x["RegionID"]),
                        //RegionName = EnglishCulture ? x["RegionEng"].ToString() : x["RegionAra"].ToString(),
                        //PostalCode = x["PostalCode"].ToString()
                        CommercialLicenseNumber = x["commLicData"].ToString(),
                        TradeLicenseNumber = x["TradeLicData"].ToString(),
                        ImporterLicenseNumber = x["importLicData"].ToString()
                        //CompanyName=x["CompanyName"].ToString()
                    };
            MociDetails = a.ToList();

            //GovernorateRegions.Add(new GovernorateRegions { GovernorateID=1, RegionID = 1, RegionName = "adfdf",PostalCode="234234" });
            //GovernorateRegions.Add(new GovernorateRegions { GovernorateID = 1, RegionID = 2, RegionName = "fghh", PostalCode = "67467" });

            return Json(MociDetails, JsonRequestBehavior.AllowGet);

        }



        public ActionResult GETChildUsers()
        {
            if (Session["isAdmin"] != null && Session["isSubAdmin"] != null)
            {
                bool isAdmin = Convert.ToBoolean(Session["isAdmin"]);
                bool isSubAdmin = Convert.ToBoolean(Session["isSubAdmin"]);

                if (!isAdmin && !isSubAdmin)
                {
                    return RedirectToAction("UnAuthorizedAction");
                }
            }

            int ParentID = 0;

            if (Session["UserId"] != null)
            {
                ParentID = int.Parse(Session["UserId"].ToString());
            }

            else
            {
                return RedirectToAction("Start", "registration");
            }

            ReqObj r = new ReqObj { ParentID = ParentID, ChildUser = 0 };
            DataTable dt = objdataclass.GETChildUsers(r);

            var s = from p in dt.AsEnumerable()
                    select new ChildUsers
                    {
                        UserID = Convert.ToInt32(p["UserID"]),
                        FirstName = p["FirstName"].ToString(),// p.Field<string>("FirstName"),
                        LastName = p["LastName"].ToString(),//                        p.Field<string>("LastName"),
                        AccountStatus = p["Account Status"].ToString(),// p.Field<string>("AccountStatus")
                        ServiceNameEng = System.Convert.IsDBNull(p["ServiceNameEng"]) ? "" : p["ServiceNameEng"].ToString(),// p.Field<string>("AccountStatus")
                        OrgNameEng = System.Convert.IsDBNull(p["OrgNameEng"]) ? "" : p["OrgNameEng"].ToString()// p.Field<string>("AccountStatus")
                    };
            List<ChildUsers> CU = s.ToList();

            ChildUsersModel C = new ChildUsersModel { ChildUsers = CU, ParentID = ParentID };

            return View(C);

        }

        public ActionResult UnAuthorizedAction(int? MsgCode)//(int ParentID)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Start", "registration");
            }
            string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;
            bool EnglishCulture = culture.Contains("English");

            if (MsgCode == 1)
            {
                ViewBag.PageTitle = Resources.Resource.Menu2;
                ViewBag.UnAuthorizedPageMsg = Resources.Resource.CanCreateOrg;

            }
            else if (MsgCode == 2)
            {
                ViewBag.PageTitle = Resources.Resource.Menu2;
                ViewBag.UnAuthorizedPageMsg = Resources.Resource.updaterequestPending;

            }
            else if (MsgCode == 3)
            {
                ViewBag.PageTitle = EnglishCulture ? "Information" : "معلومات";
                ViewBag.FromOrgReqInitPage = true;
                ViewBag.ExistingRequest = "EserviceRequest";
                ViewBag.UnAuthorizedPageMsg = EnglishCulture ? "Please check My Request page to proceed or to check your request" : "يرجى مراجعة صفحة طلبي للمتابعة أو للتحقق من طلبك";

            }
            else if (MsgCode == 4)
            {
                ViewBag.PageTitle = EnglishCulture ? "Information" : "معلومات";
                ViewBag.FromOrgReqInitPage = true;
                ViewBag.ExistingRequest = "EtradeRequest";
                ViewBag.UnAuthorizedPageMsg = EnglishCulture ? "Our record shows that you might have old request placed earlier in Customs system. Please contact the support team for assistance " : "يوضح سجلنا أنه قد يكون لديك طلب قديم تم تقديمه مسبقًا في نظام الجمارك. يرجى الاتصال بفريق الدعم للحصول على المساعدة";

            }
            else //if (MsgCode != 1 && MsgCode != 2)
            {
                ViewBag.PageTitle = Resources.Resource.unAuthorizedAction;
                ViewBag.UnAuthorizedPageMsg = Resources.Resource.Permissiondenied;
            }
            return View();

        }
        //[HttpPost]
        [HttpGet]
        public ActionResult SideBarStateChange(ReqObj R)
        {
            if (Session["UserId"] != null && Session["TokenId"] != null)
            {
                if (R.CommonData == "active")
                {
                    Session["SideBarState"] = "active";
                }
                else
                {
                    Session["SideBarState"] = "";

                }
            }
            return Json(new { StatusCode = 0 });
        }
        public ActionResult ManageUser(string UserID, int? StatusCode)
        {
            DataSet ds = new DataSet();
            try
            {


                UserID = HttpUtility.UrlDecode(UserID);
                UserID = CommonFunctions.CsUploadDecrypt(UserID);
                if (string.IsNullOrEmpty(UserID))
                {
                    return View(ds);
                }

                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }


                ReqObj r = new ReqObj { ParentID = int.Parse(UserID), ChildUser = 0 };
                ds = objdataclass.GETUserDetail(r);

                DataTable GETUserAssociatedServices = new DataTable("GETUserAssociatedServices");
                GETUserAssociatedServices = ds.Tables.Contains("GETUserAssociatedServices") ? ds.Tables["GETUserAssociatedServices"] : null;
                //DataTable GETUserAssociatedOrganizations = ds.Tables.Contains("GETUserAssociatedOrganizations") ? ds.Tables["GETUserAssociatedOrganizations"] : null;
                if (GETUserAssociatedServices != null)
                {
                    ds.Tables.Remove("GETUserAssociatedServices");
                    GETUserAssociatedServices = GETUserAssociatedServices.AsEnumerable().Where
                            (row => row.Field<string>("LegalEntityType") == (Convert.ToBoolean(Session["ClearingAgentServices"]) ? "1" : "2"))
                            .AsDataView().ToTable();
                }
                else
                {
                    GETUserAssociatedServices = new DataTable("GETUserAssociatedServices");//assign some dummy dataset
                }
                ds.Tables.Add(GETUserAssociatedServices);


                //var s = from p in dt.AsEnumerable()
                //        select new ChildUsers
                //        {
                //            UserID = Convert.ToInt32(p["UserID"]),
                //            FirstName = p["FirstName"].ToString(),// p.Field<string>("FirstName"),
                //            LastName = p["LastName"].ToString(),//                        p.Field<string>("LastName"),
                //            AccountStatus = p["Account Status"].ToString(),// p.Field<string>("AccountStatus")
                //            ServiceNameEng = System.Convert.IsDBNull(p["ServiceNameEng"]) ? "" : p["ServiceNameEng"].ToString(),// p.Field<string>("AccountStatus")
                //            OrgNameEng = System.Convert.IsDBNull(p["OrgNameEng"]) ? "" : p["OrgNameEng"].ToString()// p.Field<string>("AccountStatus")
                //        };
                //List<ChildUsers> CU = s.ToList();

                //ChildUsersModel C = new ChildUsersModel { ChildUsers = CU, ParentID = ParentID };



                short StatusCodeResult;
                if (StatusCode != null)
                {
                    if (Int16.TryParse(StatusCode.ToString(), out StatusCodeResult))
                    {
                        if (StatusCodeResult == 0)
                        {

                            ViewBag.Showmsg = Constantclass.number;
                            ViewBag.Msg = Resources.Resource.updatedSuccess;
                        }
                        else if (StatusCodeResult == 11)
                        {
                            ViewBag.Showmsg = Constantclass.number;
                            if (ds.Tables[0].Rows[0]["Admin"].ToString() == "No")
                            {
                                if (ds.Tables[0].Rows[0]["LegalEntityName"].ToString() == "Organization")
                                {
                                    ViewBag.Msg = Resources.Resource.OrgAdditonalUserCriteria;

                                }
                                else if (ds.Tables[0].Rows[0]["LegalEntityName"].ToString() == "Clearing Agent")
                                {
                                    ViewBag.Msg = Resources.Resource.ClearingAgentAdditonalUserCriteria;
                                }
                            }
                        }

                    }
                    else if (StatusCodeResult == -1)
                    {
                        ViewBag.Showmsg = Constantclass.number;
                        ViewBag.Msg = Resources.Resource.somethingwentwrong;

                    }
                }


                return View(ds);
            }
            catch (Exception e)
            {
                WriteToLogFile(e.Message, "ManageUser");
                ViewBag.Showmsg = Constantclass.number;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View(ds);
            }
        }
        [HttpPost]
        public ActionResult ManageUser(Manageuser MU)
        {
            return View();
        }

        public ActionResult DeActivateChildUser(string UserID, string Act)
        {
            string UrlParamUserID = UserID;
            try
            {
                UserID = HttpUtility.UrlDecode(UserID);
                UserID = CommonFunctions.CsUploadDecrypt(UserID);
                if (string.IsNullOrEmpty(UserID))
                {
                    return RedirectToAction("ManageUser", "Registration", new { UserID = UrlParamUserID });
                }
                Act = HttpUtility.UrlDecode(Act);
                Act = CommonFunctions.CsUploadDecrypt(Act);

                ReqObj r = new ReqObj { ParentID = 0, ChildUser = int.Parse(UserID), Action = Act };
                DataTable dt = objdataclass.DeActivateChildUser(r);// ParentID, ChildUser);

                ResObj Res = new ResObj { Status = dt.Rows[0][0].ToString() };

                return RedirectToAction("ManageUser", "Registration", new { UserID = UrlParamUserID, StatusCode = Int16.Parse(dt.Rows[0][0].ToString()) });
            }
            catch (Exception e)
            {
                WriteToLogFile(e.Message, "DeActivateChildUser");
                return RedirectToAction("ManageUser", "Registration", new { UserID = UrlParamUserID, StatusCode = Int16.Parse("-1") });
            }

        }
        public ActionResult UserStatusAction(string Act,
            string UserID, string ServiceID, string SubscriptionID,
             string OrganizationID)
        {
            string UrlParamUserID = UserID;
            try
            {
                UserID = HttpUtility.UrlDecode(UserID);
                UserID = CommonFunctions.CsUploadDecrypt(UserID);
                if (string.IsNullOrEmpty(UserID))
                {
                    return RedirectToAction("ManageUser", "Registration", new { UserID = UrlParamUserID });
                }
                Act = HttpUtility.UrlDecode(Act);
                Act = CommonFunctions.CsUploadDecrypt(Act);
                ServiceID = HttpUtility.UrlDecode(ServiceID);
                ServiceID = CommonFunctions.CsUploadDecrypt(ServiceID);
                SubscriptionID = HttpUtility.UrlDecode(SubscriptionID);
                SubscriptionID = CommonFunctions.CsUploadDecrypt(SubscriptionID);
                OrganizationID = HttpUtility.UrlDecode(OrganizationID);
                OrganizationID = CommonFunctions.CsUploadDecrypt(OrganizationID);

                bool isLinked = Act.Contains("DisAssociate") ? false : true;
                string ActionType = Act.Contains("Org") ? "OrganizationAction" : "ServiceAction";

                ServicesAndOrgManagementFortheUser ro = new ServicesAndOrgManagementFortheUser
                {
                    UserID = int.Parse(UserID),
                    ParentUserID = 0,
                    isLinked = isLinked,
                    ActionType = ActionType,
                    ServiceID = ServiceID,
                    SubscriptionID = SubscriptionID,
                    OrganizationID = OrganizationID
                };

                DataTable dt = objdataclass.ServicesAndOrgManagementFortheUser(ro);

                return RedirectToAction("ManageUser", "Registration", new { UserID = UrlParamUserID, StatusCode = Int16.Parse(dt.Rows[0][0].ToString()) });
            }
            catch (Exception e)
            {
                WriteToLogFile(e.Message, "UserStatusActionUserStatusAction");
                return RedirectToAction("ManageUser", "Registration", new { UserID = UrlParamUserID, StatusCode = Int16.Parse("-1") });
            }

        }

        public ActionResult ChangeLanguage(string lang, String pageName, String ContName, string urlredirect)
        {
            try
            {
                Session["lng"] = lang;

                //if(urlredirect== "/BrokerRenewal/BrokerTransferPost")
                //{
                //    urlredirect = "/BrokerRenewal/BrokerTransfer?ServiceId=1PVm4XXA8vcpccI5B3%2Banw%3D%3D";
                //}

                urlredirect = urlredirect.Replace("/BrokerTransferPost", "/BrokerTransfer?ServiceId=1PVm4XXA8vcpccI5B3%2Banw%3D%3D");
                HttpCookie langCookie = Request.Cookies["culture"];
                langCookie.Value = lang.Contains("ar") ? "ar" : "en";

                new SiteLanguages().SetLanguage(lang);

                if (Session["UserId"] != null)
                {

                    objdataclass.UpdateUserSession(new UserSession { Userid = Convert.ToInt32(Session["UserId"]), lang = lang });
                }
                //else//Commented this part , as user can change language in User registration screen also
                //{
                //    return RedirectToAction("Start", "registration");

                //}
                //return RedirectToAction(ContName, pageName);
                //return RedirectToAction(pageName, "registration");

                urlredirect = HttpUtility.HtmlDecode(urlredirect);

                return Redirect(urlredirect);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return RedirectToAction("Start", "registration");
                //return Redirect(urlredirect);
            }
        }
        public ActionResult Forgot_Password()
        {
            try
            {
                ViewBag.disabled = Constantclass.disnone;
                getcaptcha(0);
                ViewBag.modelerror = Constantclass.number1;
                List<ForgotPwdOTPinput> objlist = new List<ForgotPwdOTPinput>();
                ForgotPwdOTPinput objforgot = new ForgotPwdOTPinput();
                objlist.Add(objforgot);
                ViewData["data"] = objlist;
                return View();
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Forgot_Password(ForgotPwdOTPinput objinput)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                ViewBag.disabled = Constantclass.disnone;
                if (!(ModelState.IsValid))
                {
                    //getcaptcha(Convert.ToInt32(TempData["CaptchaId"]));
                    //objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
                    //TempData["CaptchaId"] = objinput.CaptchaId;
                    List<ForgotPwdOTPinput> objlist = new List<ForgotPwdOTPinput>();
                    objlist.Add(objinput);
                    ViewData["data"] = objlist;
                    ViewBag.modelerror = Constantclass.number;
                    return View();
                }
                string input = objinput.emailId.ToString().Trim();
                double num;
                if (double.TryParse(input, out num))
                {
                    objinput.emailId = "";
                    objinput.mobileNo = input;
                }
                else
                {
                    objinput.emailId = input;
                    objinput.mobileNo = "";
                }

                string status = string.Empty;
                if (Session["CaptchaCode"].ToString() == objinput.CaptchaValue.ToUpper())// (this.IsCaptchaValid("Validate your captcha"))
                {
                    //objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
                    DataTable objForgotPwdOTPResult = objdataclass.ForgotPwdOTPResult(objinput);
                    Session.Remove("CaptchaCode");
                    TempData["dt"] = objForgotPwdOTPResult;
                    //string status = string.Empty;
                    if (objForgotPwdOTPResult.Rows.Count > 0)
                    {
                        if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "-8")
                        {
                            status = Resources.Resource.EntercorrectCaptcha;
                            ViewBag.modelerror = Constantclass.number;
                            ModelState.AddModelError(string.Empty, status);
                        }

                        else if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "0" && objForgotPwdOTPResult.Rows[0]["OTPKeyId"].ToString().Trim() != "")
                        {
                            ViewBag.modelerror = Constantclass.number2;
                            if (objinput.mobileNo != "")
                            {
                                ViewBag.msg = Resources.Resource.Verificationsenttoyourmobile + ":" + objinput.mobileNo;
                            }
                            else
                            {
                                ViewBag.msg = Resources.Resource.VerificationsenttoyourEmail + ": " + objinput.emailId;
                            }
                            ViewBag.disabled = Constantclass.disblock;
                        }
                    }
                }
                else //if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "-1")
                {
                    status = "Enter correct Captcha";
                    ViewBag.modelerror = Constantclass.number;
                    //ModelState.AddModelError(string.Empty, "Status : " + status);
                    ModelState.AddModelError(string.Empty, status);
                }

                //TempData["dt"] = objForgotPwdOTPResult;
                //getcaptcha(Convert.ToInt32(TempData["CaptchaId"]));
                //objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
                //TempData["CaptchaId"] = objinput.CaptchaId;
                List<ForgotPwdOTPinput> objlist1 = new List<ForgotPwdOTPinput>();
                objlist1.Add(objinput);
                ViewData["data"] = objlist1;

                return View();
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        public ActionResult Forgot_ResetPassword(ForgotPwdOTPinput objForgotPwdOTPinput)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                ResetPwdByOTPParams objinput = new ResetPwdByOTPParams();
                DataTable dt = new DataTable();
                if (TempData["dt"] != null)
                {
                    dt = (DataTable)TempData["dt"];
                    if (dt.Rows.Count > 0)
                    {
                        objinput.mUserid = dt.Rows[0]["mUserId"].ToString().Trim();
                        objinput.otpId = dt.Rows[0]["OTPKeyId"].ToString().Trim();
                        //objinput.otpValue = dt.Rows[0]["OTPKeyVal"].ToString().Trim();
                        objinput.otpValue = objForgotPwdOTPinput.otpValue;
                        objinput.newPwd = objForgotPwdOTPinput.newPwd;
                    }
                }
                DataTable obresetOTPResult = objdataclass.ResetPwdOTPResult(objinput);
                if (obresetOTPResult.Rows.Count > 0)
                {
                    if (obresetOTPResult.Rows[0]["Status"].ToString().Trim() == "0")
                    {
                        ViewBag.modelerror = Constantclass.number3;
                        ViewBag.msg = Resources.Resource.loginwithyourcredentials;
                    }

                }
                List<ForgotPwdOTPinput> objlist1 = new List<ForgotPwdOTPinput>();
                objlist1.Add(objForgotPwdOTPinput);
                ViewData["data"] = objlist1;
                return View("Forgot_Password");
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return View("Forgot_Password");
            }
        }
        public ActionResult ResetPassword()
        {
            try
            {
                ViewBag.disabled = Constantclass.disnone;
                //getcaptcha(0);
                ViewBag.modelerror = Constantclass.number1;
                List<ForgotPwdOTPinput> objlist = new List<ForgotPwdOTPinput>();
                ForgotPwdOTPinput objforgot = new ForgotPwdOTPinput();
                objlist.Add(objforgot);
                ViewData["data"] = objlist;
                return View();
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ResetPassword(ForgotPwdOTPinput objinput)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                ViewBag.disabled = Constantclass.disnone;
                ViewBag.disableCaptchaFormelements = "";
                if (!(ModelState.IsValid))
                {
                    //getcaptcha(Convert.ToInt32(TempData["CaptchaId"]));
                    //objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
                    //TempData["CaptchaId"] = objinput.CaptchaId;
                    List<ForgotPwdOTPinput> objlist = new List<ForgotPwdOTPinput>();
                    objlist.Add(objinput);
                    ViewData["data"] = objlist;
                    ViewBag.modelerror = Constantclass.number;

                    return View();
                }
                string input = objinput.emailId.ToString().Trim();
                double num;
                if (double.TryParse(input, out num))
                {
                    objinput.emailId = "";
                    objinput.mobileNo = input;
                }
                else
                {
                    objinput.emailId = input;
                    objinput.mobileNo = "";
                }


                if (Session["CaptchaCode"].ToString() == objinput.CaptchaValue.ToUpper())// (this.IsCaptchaValid("Validate your captcha"))
                {
                    //objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
                    DataTable objForgotPwdOTPResult = objdataclass.ForgotPwdOTPResult(objinput);
                    Session.Remove("CaptchaCode");
                    TempData["dt"] = objForgotPwdOTPResult;
                    //string status = string.Empty;
                    if (objForgotPwdOTPResult.Rows.Count > 0)
                    {

                        if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "0" && objForgotPwdOTPResult.Rows[0]["OTPKeyId"].ToString().Trim() != "")
                        {
                            ViewBag.modelerror = Constantclass.number2;
                            if (objinput.mobileNo != "")
                            {
                                ViewBag.msg = Resources.Resource.Verificationsenttoyourmobile + ":" + objinput.mobileNo;
                            }
                            else
                            {
                                ViewBag.msg = Resources.Resource.VerificationsenttoyourEmail + ": " + objinput.emailId;
                            }
                            ViewBag.disabled = Constantclass.disblock;

                            //adding below line to hide CaptchaFormelements- Siraj
                            ViewBag.disableCaptchaFormelements = Constantclass.disnone;

                        }
                        else
                        {
                            ViewBag.status = Resources.Resource.Userdoesnotexist;
                        }
                    }

                    else
                    {
                        ViewBag.status = Resources.Resource.Userdoesnotexist;
                    }
                }
                else
                {
                    ViewBag.status = Resources.Resource.EntercorrectCaptcha;
                    ViewBag.modelerror = Constantclass.number;
                }
                List<ForgotPwdOTPinput> objlist1 = new List<ForgotPwdOTPinput>();
                objlist1.Add(objinput);
                ViewData["data"] = objlist1;

                return View();
            }
            catch (Exception)
            {

                TempData["dt"] = TempData["dt"];

                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        public ActionResult Reset_Password(ForgotPwdOTPinput objForgotPwdOTPinput)
        {
            try
            {

                //adding below line to hide CaptchaFormelements- Siraj
                ViewBag.disableCaptchaFormelements = Constantclass.disnone; // This is of no use now as we show captcha even for verification opage

                if (Session["CaptchaCode"].ToString() != objForgotPwdOTPinput.CaptchaValue.ToUpper())// (this.IsCaptchaValid("Validate your captcha"))
                {
                    ViewBag.modelerror = "-1334";
                    ViewBag.msg = Resources.Resource.EntercorrectCaptcha;
                    List<ForgotPwdOTPinput> objlist11 = new List<ForgotPwdOTPinput>();
                    objlist11.Add(objForgotPwdOTPinput);
                    ViewData["data"] = objlist11;
                    return View("ResetPassword");
                }

                ViewBag.modelerror = Constantclass.number1;
                ResetPwdByOTPParams objinput = new ResetPwdByOTPParams();
                DataTable dt = new DataTable();
                if (TempData["dt"] != null)
                {
                    dt = (DataTable)TempData["dt"];
                    if (dt.Rows.Count > 0)
                    {
                        objinput.mUserid = dt.Rows[0]["mUserId"].ToString().Trim();
                        objinput.otpId = dt.Rows[0]["OTPKeyId"].ToString().Trim();
                        //objinput.otpValue = dt.Rows[0]["OTPKeyVal"].ToString().Trim();
                        objinput.otpValue = objForgotPwdOTPinput.otpValue;
                        objinput.newPwd = objForgotPwdOTPinput.newPwd;
                    }

                    TempData["dt"] = TempData["dt"];
                }
                DataTable obresetOTPResult = objdataclass.ResetPwdOTPResult(objinput);
                if (obresetOTPResult.Rows.Count > 0)
                {
                    if (obresetOTPResult.Rows[0]["Status"].ToString().Trim() == "0")
                    {
                        ViewBag.modelerror = Constantclass.number3;
                        ViewBag.msg = Resources.Resource.ResetPasswordSuccess;// loginwithyourcredentials;
                    }
                    else if (obresetOTPResult.Rows[0]["Status"].ToString().Trim() == "-11")// Siraj
                    {
                        ViewBag.modelerror = "-11";
                        ViewBag.msg = Resources.Resource.NewPasswordSameasOld;// loginwithyourcredentials;
                                                                              //NewPasswordSameasOld -  Please do not enter the new password same as your last password
                    }
                    else// Siraj
                    {
                        ViewBag.modelerror = "-1";
                        ViewBag.msg = Resources.Resource.PasswordCriteriadoesntmet;// loginwithyourcredentials;
                    }


                }
                List<ForgotPwdOTPinput> objlist1 = new List<ForgotPwdOTPinput>();
                objlist1.Add(objForgotPwdOTPinput);
                ViewData["data"] = objlist1;
                return View("ResetPassword");
            }
            catch (Exception)
            {
                TempData["dt"] = TempData["dt"];

                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return View("ResetPassword");
            }
        }


        //public ActionResult EmailVerification()
        //{
        //    try
        //    {
        //        ViewBag.ParentID = TempData["ParentUserID"];
        //        ViewBag.modelstatusid = "1";
        //        if (TempData["MobileNo"] != null)
        //        {
        //            ViewBag.mobile = TempData["MobileNo"].ToString();
        //            TempData["MobileNo"] = ViewBag.mobile;
        //        }
        //        TempData.Keep();
        //        return View();
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.modelerror = Constantclass.number5;
        //        ViewBag.msg = Resources.Resource.somethingwentwrong;
        //        return View();
        //    }
        //}
        //[HttpPost]
        //public ActionResult EmailVerification(VerifyOTPParams objVerifyOTPParams, FormCollection FC)
        //{
        //    try
        //    {

        //        if (FC["ParentID"] != null && FC["ParentID"] != "")
        //        {
        //            objVerifyOTPParams.ParentID = Convert.ToInt32(FC["ParentID"]);
        //        }

        //        //ViewBag.modelstatusid = "1";
        //        //if (!(ModelState.IsValid))
        //        //{
        //        //    TempData.Keep();
        //        //    ViewBag.modelstatusid = "0";
        //        //    ViewBag.modelstatus = "Please Enter Code";
        //        //    return View("EmailVerification");
        //        //}
        //        DataTable objres = new DataTable();
        //        if (TempData["EmailKeyValTemporary"] != null &&
        //            TempData["TokenId"] != null &&
        //            TempData["UserId"] != null)
        //        {
        //            objVerifyOTPParams.tokenId = TempData["TokenId"].ToString();
        //            objVerifyOTPParams.mUserid = TempData["UserId"].ToString();
        //            String EmailKeyValTemporary = TempData["EmailKeyValTemporary"].ToString();
        //            objres = objdataclass.EmailVerificationdata(objVerifyOTPParams);
        //            TempData["TokenId"] = objVerifyOTPParams.tokenId;
        //            TempData["UserId"] = objVerifyOTPParams.mUserid;
        //            TempData.Keep();
        //        }
        //        //if (Session["login"] != null && Session["TokenId"] != null && Session["UserId"] != null)
        //        //{
        //        //    if (Session["login"].ToString() == "M")
        //        //    {
        //        //        objVerifyOTPParams.Mobile = objVerifyOTPParams.Email;
        //        //        objVerifyOTPParams.Email = "";
        //        //    }
        //        //    objVerifyOTPParams.tokenId = Session["TokenId"].ToString();
        //        //    objVerifyOTPParams.mUserid = Session["UserId"].ToString();
        //        //    objres = objdataclass.EmailVerificationdata(objVerifyOTPParams);
        //        //}
        //        //if (Session["UserId"] != null && Session["TokenId"] != null)
        //        //{
        //        //objVerifyOTPParams.mUserid = Session["UserId"].ToString();
        //        //objVerifyOTPParams.tokenId = Session["TokenId"].ToString();
        //        //if (TempData["OrgId"] != null)
        //        //{
        //        //    objVerifyOTPParams.ReferenceId = TempData["OrgId"].ToString();
        //        //    objVerifyOTPParams.Mobile = "";
        //        //    objres = objdataclass.EmailVerificationdata(objVerifyOTPParams);
        //        //    TempData["OrgId"] = objVerifyOTPParams.ReferenceId;
        //        //}
        //        //if (TempData["MobileNo"] != null && TempData["refId"] != null)
        //        //{
        //        //    ViewBag.mobile = TempData["MobileNo"].ToString();

        //        //    ValidateContacts ObjValidateContacts = new ValidateContacts();
        //        //    ObjValidateContacts.Reference = objVerifyOTPParams.Email;
        //        //    ObjValidateContacts.mUserid = Session["UserId"].ToString();
        //        //    ObjValidateContacts.tokenId = Session["TokenId"].ToString();
        //        //    ObjValidateContacts.ReferenceType = "M";
        //        //    ObjValidateContacts.ReferenceId = TempData["refId"].ToString();
        //        //    objres = objdataclass.ValidateContactsWithOTP(ObjValidateContacts);
        //        //    TempData["refId"] = ObjValidateContacts.ReferenceId;
        //        //    TempData["MobileNo"] = ViewBag.mobile;
        //        //}
        //        //}

        //        if (objres.Rows.Count > 0)
        //        {
        //            if (objres.Columns.Contains("VerifyStatus"))
        //            {
        //                if (objres.Rows[0]["VerifyStatus"].ToString().Trim() == "0")
        //                {
        //                    ViewBag.msg = Resources.Resource.loginwithyourcredentials;
        //                    ViewBag.success = "true";
        //                }

        //                else
        //                {
        //                    ViewBag.msg = Resources.Resource.WrongOTPPleasetryagain;
        //                    ViewBag.success = "false";
        //                }
        //            }



        //            //string res = string.Empty;
        //            //if (objres.Columns.Contains("VerifyStatus") && TempData["OrgId"] != null)
        //            //{
        //            //    if (objres.Rows[0]["VerifyStatus"].ToString().Trim() == "0")
        //            //    {
        //            //        res = "Verified";
        //            //    }
        //            //}
        //            //if (objres.Columns.Contains("VerifyStatus") && TempData["UserId"] != null)
        //            //{
        //            //    if (objres.Rows[0]["VerifyStatus"].ToString().Trim() == "0")
        //            //    {
        //            //        res = "Verified";
        //            //    }
        //            //}
        //            //if (objres.Columns.Contains("MobileVerifyStatus") && TempData["MobileNo"] != null && TempData["refId"] != null)
        //            //{
        //            //    if (objres.Rows[0]["MobileVerifyStatus"].ToString().Trim() == "0")
        //            //    {
        //            //        res = "Verified";
        //            //    }
        //            //}
        //            //if (objres.Columns.Contains("VerifyStatus") && Session["login"] != null)
        //            //{
        //            //    if (objres.Rows[0]["VerifyStatus"].ToString().Trim() == "0")
        //            //    {
        //            //        res = "Verified";
        //            //    }
        //            //}
        //            //if (res == "Verified")
        //            //{
        //            //    if (objVerifyOTPParams.ReferenceId != null && Session["login"] == null)
        //            //    {
        //            //        return RedirectToAction("UploadDocument", "User");
        //            //    }
        //            //    else
        //            //    {
        //            //        if (TempData["MobileNo"] != null || Session["login"] != null)
        //            //        {
        //            //            Session["login"] = null;
        //            //            return RedirectToAction("MyAccount", "User");
        //            //        }
        //            //        else
        //            //        {
        //            //            if (objVerifyOTPParams.ParentID > 0)
        //            //            {
        //            //                ViewBag.ok = "PUser";
        //            //            }
        //            //            else
        //            //            {
        //            //                ViewBag.ok = "3";
        //            //            }
        //            //        }

        //            //    }
        //            //    ViewBag.modelstatusid = "0";
        //            //    ViewBag.modelstatus = Resources.Resource.loginwithyourcredentials;
        //            //}
        //            //else
        //            //{
        //            //    ViewBag.modelstatusid = "0";
        //            //    ViewBag.modelstatus = Resources.Resource.VerificationFail;
        //            //}
        //        }

        //        else
        //        {
        //            ViewBag.msg = Resources.Resource.WrongOTPPleasetryagain;
        //            ViewBag.success = "false";
        //        }
        //        //if (TempData["MobileNo"] != null)
        //        //{
        //        //    TempData["MobileNo"] = TempData["MobileNo"].ToString();
        //        //}
        //        //if (TempData["refId"] != null)
        //        //{
        //        //    TempData["refId"] = TempData["refId"].ToString();
        //        //}
        //        //if (TempData["OrgId"] != null)
        //        //{
        //        //    TempData["OrgId"] = TempData["OrgId"].ToString();
        //        //}
        //        if (TempData["EmailKeyValTemporary"] != null &&
        //            TempData["TokenId"] != null &&
        //            TempData["UserId"] != null)
        //        {
        //            TempData["EmailKeyValTemporary"] = TempData["EmailKeyValTemporary"].ToString();
        //            TempData["TokenId"] = TempData["TokenId"].ToString();
        //            TempData["UserId"] = TempData["UserId"].ToString();
        //        }
        //        TempData.Keep();
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        String _Exception = ex.ToString();
        //        ViewBag.modelerror = Constantclass.number5;
        //        ViewBag.success = "false";
        //        ViewBag.msg = Resources.Resource.somethingwentwrong;
        //        return View();
        //    }
        //}
        public ActionResult ContactVerification(bool EmailVerific = true, bool MobileVerific = true)
        {
            try
            {
                if (TempData["ParentUserID"] != null)
                    ViewBag.ParentID = TempData["ParentUserID"];

                ViewBag.modelstatusid = "1";
                //if (TempData["MobileNo"] != null)
                //{
                //    ViewBag.mobile = TempData["MobileNo"].ToString();
                //    TempData["MobileNo"] = ViewBag.mobile;
                //}
                TempData.Keep();
                ViewBag.ShowEmailBox = EmailVerific ? "true" : "false";
                ViewBag.ShowMobileBox = MobileVerific ? "true" : "false";

                return View();
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ContactVerification(VerifyOTPParams objVerifyOTPParams, FormCollection FC)
        {
            try
            {

                if (FC["ParentID"] != null && FC["ParentID"] != "")
                {
                    objVerifyOTPParams.ParentID = Convert.ToInt32(FC["ParentID"]);
                }

                DataTable objres = new DataTable();
                if (TempData["EmailKeyValTemporary"] != null &&
                    TempData["TokenId"] != null &&
                    TempData["UserId"] != null)
                {
                    objVerifyOTPParams.tokenId = TempData["TokenId"].ToString();
                    objVerifyOTPParams.mUserid = TempData["UserId"].ToString();
                    String EmailKeyValTemporary = TempData["EmailKeyValTemporary"].ToString();

                    //newly added Siraj 26-12-2019 
                    if (Session["lng"] != null)
                    {
                        objVerifyOTPParams.Lang = Session["lng"].ToString();
                    }
                    else
                    {
                        objVerifyOTPParams.Lang = "ar";
                    }

                    objres = objdataclass.EmailVerificationdata(objVerifyOTPParams);
                    TempData["TokenId"] = objVerifyOTPParams.tokenId;


                    Session["TokenId"] = objVerifyOTPParams.tokenId; //newly added Siraj 26-12-2019 .. as its used in AutoAuth method

                    TempData["UserId"] = objVerifyOTPParams.mUserid;
                    TempData.Keep();
                }

                ViewBag.ShowEmailBox = "true";
                ViewBag.ShowMobileBox = "true";

                if (objres.Rows.Count > 0)
                {
                    if (objres.Columns.Contains("VerifyStatus"))
                    {
                        if (objres.Rows[0]["VerifyStatus"].ToString().Trim() == "0")
                        {
                            ViewBag.msg = Resources.Resource.loginwithyourcredentials;
                            ViewBag.success = "true";
                            //string redirectionHomePage= AutoAuth(objres); //redirection to menu view page is not happening there as its a method call and proceed further in this method, so added redirection below
                            ////return RedirectToAction("MenuView", "Dashboard");
                            //return Redirect(redirectionHomePage);

                            //ViewBag.ShowEmailBox = "true";
                            //ViewBag.ShowMobileBox = "true";

                            return AutoAuth(objres);

                        }

                        else
                        {
                            ViewBag.msg = Resources.Resource.WrongOTPPleasetryagain;

                            if (objres.Rows[0]["EmailVerifyStatus"].ToString().Trim() == "0")
                            {
                                ViewBag.ShowEmailBox = "false";
                                ViewBag.msg = Resources.Resource.WrongSMSOTPPleasetryagain;
                            }
                            if (objres.Rows[0]["MobileVerifyStatus"].ToString().Trim() == "0")
                            {
                                ViewBag.ShowMobileBox = "false";
                                ViewBag.msg = Resources.Resource.WrongEmailOTPPleasetryagain2;
                            }
                            if(objres.Rows[0]["EmailVerifyStatus"].ToString().Trim() != "0" && objres.Rows[0]["MobileVerifyStatus"].ToString().Trim() != "0")//added siraj 08-03-2020
                            {
                                ViewBag.msg = Resources.Resource.WrongOTPPleasetryagain;
                            }
                            ViewBag.success = "false";
                        }
                    }
                }

                else
                {
                    ViewBag.msg = Resources.Resource.WrongOTPPleasetryagain;
                    ViewBag.success = "false";
                }

                if (TempData["EmailKeyValTemporary"] != null &&
                    TempData["TokenId"] != null &&
                    TempData["UserId"] != null)
                {
                    TempData["EmailKeyValTemporary"] = TempData["EmailKeyValTemporary"].ToString();
                    TempData["TokenId"] = TempData["TokenId"].ToString();
                    TempData["UserId"] = TempData["UserId"].ToString();
                }
                TempData.Keep();
                return View();
            }
            catch (Exception ex)
            {
                String _Exception = ex.ToString();
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.success = "false";
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        public ActionResult AutoAuth(DataTable dtlogondetails)
        {

            string redirectionHomePage = "";

            String userIdforMulipleSessions = dtlogondetails.Rows[0]["UserId"].ToString();

            Dictionary<String, String> UsersDictionaryWithSessionsIds = new Dictionary<String, String>();

            if (System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] != null)
            {
                UsersDictionaryWithSessionsIds = System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] as Dictionary<String, String>;

                if (UsersDictionaryWithSessionsIds.ContainsKey(userIdforMulipleSessions))
                {
                    ViewBag.Msg = Resources.Resource.userSessionIsActive;
                    ViewBag.showMessageStyle = "block";
                    ViewBag.modelerror = Constantclass.number99;

                    UsersDictionaryWithSessionsIds.Remove(userIdforMulipleSessions);

                    System.Web.HttpContext.Current.Application.Lock();
                    System.Web.HttpContext.Current.Application.Contents.Remove("UsersDictionaryWithSessionsIds");
                    System.Web.HttpContext.Current.Application.UnLock();


                    System.Web.HttpContext.Current.Application.Lock();
                    System.Web.HttpContext.Current.Application.Contents.Add("UsersDictionaryWithSessionsIds", UsersDictionaryWithSessionsIds);
                    System.Web.HttpContext.Current.Application.UnLock();

                    //return View("Index");---------------------------------------------
                }

                else
                {
                    UsersDictionaryWithSessionsIds.Add(userIdforMulipleSessions, "start");

                    System.Web.HttpContext.Current.Application.Lock();
                    System.Web.HttpContext.Current.Application.Contents.Remove("UsersDictionaryWithSessionsIds");
                    System.Web.HttpContext.Current.Application.UnLock();


                    System.Web.HttpContext.Current.Application.Lock();
                    System.Web.HttpContext.Current.Application.Contents.Add("UsersDictionaryWithSessionsIds", UsersDictionaryWithSessionsIds);
                    System.Web.HttpContext.Current.Application.UnLock();

                }
            }



            regenerateId();

            //if(fc.AllKeys.Contains("LoginForStakeholderServices.x"))

            if (dtlogondetails.Rows[0]["LegalEntity"].ToString() == "2")//fc.AllKeys.Contains("LoginForStakeholderServices.x"))//!string.IsNullOrEmpty(LoginForStakeholderServices))
            {
                Session["ClearingAgentServices"] = false;
                Session["OrganizationServices"] = true;
            }
            if (dtlogondetails.Rows[0]["LegalEntity"].ToString() == "1")//fc.AllKeys.Contains("LoginForBrokerServices.x"))//!string.IsNullOrEmpty(LoginForBrokerServices))
            {
                Session["ClearingAgentServices"] = true;
                Session["OrganizationServices"] = false;
            }
            Session["ServicePreference"] = Convert.ToBoolean(Session["ClearingAgentServices"]) ? "CA" : "ORG";

            Session["UserName"] = dtlogondetails.Rows[0]["FirstName"].ToString().ToUpper() + " " + dtlogondetails.Rows[0]["LastName"].ToString().ToUpper();
            Session["UserId"] = dtlogondetails.Rows[0]["UserId"].ToString();
            Session["TokenId"] = dtlogondetails.Rows[0]["TokenId"].ToString();// Session["TokenId"];// dtlogondetails.Rows[0]["TokenId"].ToString();--------------------
            Session["AllowedMenus"] = dtlogondetails.Rows[0]["AllowedMenus"].ToString();
            Session["UserOrgID"] = dtlogondetails.Rows[0]["OrgID"].ToString();// Siraj
            Session["isAdmin"] = dtlogondetails.Rows[0]["isAdmin"].ToString();// Siraj
            Session["isSubAdmin"] = dtlogondetails.Rows[0]["isSubAdmin"].ToString();// Siraj
            Session["LegalEntity"] = dtlogondetails.Rows[0]["LegalEntity"].ToString();// Siraj
            Session["LicenseNumber"] = dtlogondetails.Rows[0]["LicenseNumber"].ToString();// Siraj
            Session["MicroClearUsername"] = dtlogondetails.Rows[0]["MC_UserId"].ToString();// Siraj
            Session["eServiceUserEmailId"] = dtlogondetails.Rows[0]["EmailId"].ToString();// Siraj
                                                                                          //Session["CompanyName"] = dtlogondetails.Rows[0]["CompanyName"].ToString();// Siraj

            Session["Themes"] = dtlogondetails.Rows[0]["UserThemes"].ToString();// Siraj


            Session["FirstName"] = dtlogondetails.Rows[0]["FirstName"].ToString();// Abdul Karim
            Session["LastName"] = dtlogondetails.Rows[0]["LastName"].ToString();// Abdul Karim
            Session["genderOfLoggedInUser"] = dtlogondetails.Rows[0]["Gender"].ToString();


            Session["Governorate"] = dtlogondetails.Rows[0]["Governorate"].ToString();// Siraj
            Session["Region"] = dtlogondetails.Rows[0]["Region"].ToString();// Siraj
            Session["GovernorateEng"] = dtlogondetails.Rows[0]["GovernorateEng"].ToString();// Siraj
            Session["RegionEng"] = dtlogondetails.Rows[0]["RegionEng"].ToString();// Siraj
            Session["GovernorateAra"] = dtlogondetails.Rows[0]["GovernorateAra"].ToString();// Siraj
            Session["RegionAra"] = dtlogondetails.Rows[0]["RegionAra"].ToString();// Siraj
            Session["PostalCode"] = dtlogondetails.Rows[0]["PostalCode"].ToString();// Siraj
            Session["Address"] = dtlogondetails.Rows[0]["Address"].ToString();// Siraj
            Session["Block"] = dtlogondetails.Rows[0]["Block"].ToString();// Siraj
            Session["Street"] = dtlogondetails.Rows[0]["Street"].ToString();// Siraj
            Session["IndustrialLicenseNumber"] = dtlogondetails.Rows[0]["IndustrialLicenseNumber"].ToString();// Siraj
            Session["CommercialLicenseNumber"] = dtlogondetails.Rows[0]["CommercialLicenseNumber"].ToString();// Siraj
            Session["IsIndustrial"] = Convert.ToBoolean(dtlogondetails.Rows[0]["IsIndustrial"]);// Siraj
            Session["CivilId"] = dtlogondetails.Rows[0]["CivilId"].ToString();// Siraj
            Session["CommercialLicenseYear"] = dtlogondetails.Rows[0]["CommercialLicenseYear"].ToString();// Siraj
            Session["CompanyCivilID"] = dtlogondetails.Rows[0]["CompanyCivilID"].ToString();// Siraj
            Session["CompanyName"] = dtlogondetails.Rows[0]["MociCompanyName"].ToString();// Siraj

            Session["SideBarState"] = "active";
            CommonFunctions.LogUserActivity(

          "UserLoggedInAutoAuthenticatedAfterVerfc", "UserSignIN", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "", "", "");




            if (dtlogondetails.Rows[0]["LegalEntity"] != null)
            {
                Session["LegalEntity"] = dtlogondetails.Rows[0]["LegalEntity"].ToString();
                Session["LicenseNumberOfLoggedInUser"] = dtlogondetails.Rows[0]["LicenseNumber"].ToString();
                Session["CivilIdOfLoggedInUser"] = dtlogondetails.Rows[0]["CivilId"].ToString();
                Session["emailOfLoggedInUser"] = dtlogondetails.Rows[0]["EmailId"].ToString(); //-----------------------email;
                Session["mobileNumberOfLoggedInUser"] = dtlogondetails.Rows[0]["MobileNumber"].ToString();

                int UserIdtoGetServices = int.Parse(Session["UserId"].ToString());
                ReqObj serviceSubscription = new ReqObj { ParentID = UserIdtoGetServices, ChildUser = 0, CommonData = Session["ServicePreference"].ToString() };

                //DataTable dataTable = objdataclass.GETParentUserActiveServices(serviceSubscription);
                DataTable dataTable = objdataclass.GETParentUserActiveServices(serviceSubscription, false);


                Session.Add("serviceSubscriptionDataTable", dataTable);
                if (Session["LegalEntity"].ToString() == "2")
                {
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            String OrgRegServiceId = System.Configuration.ConfigurationManager.AppSettings["OrganizationRegistrationService"].ToString();
                            DataRow OrgCreationAllowed = dataTable.AsEnumerable().Where
             (row => row.Field<Int64>("ServiceID") == Int64.Parse(OrgRegServiceId)).FirstOrDefault();
                            Session["OrgRegistrationAllowed"] = OrgCreationAllowed != null ? true : false;

                        }
                    }

                }
                if (Session["LegalEntity"].ToString() == "1" || Session["LegalEntity"].ToString() == "2")
                {
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            String RenewalServiceId = System.Configuration.ConfigurationManager.AppSettings["RenewalService"].ToString();
                            DataRow RenewalServiceAllowed = dataTable.AsEnumerable().Where
             (row => row.Field<Int64>("ServiceID") == Int64.Parse(RenewalServiceId)).FirstOrDefault();
                            Session["RenewalServiceAllowed"] = RenewalServiceAllowed != null ? true : false;

                        }
                    }
                }

                SessionIDManager manger = new SessionIDManager();
                var guId = Guid.NewGuid();

                String newId = manger.CreateSessionID(System.Web.HttpContext.Current);

                String additionalId = guId + GenerateRandomWord(5);



                Session["AuthToken"] = newId;
                Session["additionalId"] = additionalId;


                HttpCookie AuthenticationCookie = new HttpCookie("Nice", newId);
                AuthenticationCookie.HttpOnly = true;
                Response.Cookies.Add(AuthenticationCookie);

                HttpCookie AuthenticationCookie1 = new HttpCookie("toMeetYou", additionalId);
                AuthenticationCookie1.HttpOnly = true;
                Response.Cookies.Add(AuthenticationCookie1);


                String random = GenerateRandomWord(10) + GetRandomText();
                HttpCookie AuthenticationCookie2 = new HttpCookie("kUwAiTCuSt0MspoajhdnqAcNadlAoepzfwwydBdgd", random);
                AuthenticationCookie2.HttpOnly = true;
                Response.Cookies.Add(AuthenticationCookie2);


                HttpCookie AuthenticationCookie3 = new HttpCookie("catchMeIfYouCan", Guid.NewGuid().ToString());
                AuthenticationCookie3.HttpOnly = true;
                Response.Cookies.Add(AuthenticationCookie3);

                if (System.Web.HttpContext.Current.Application["UsersDictionaryWithSessionsIds"] != null)
                {
                    if (UsersDictionaryWithSessionsIds.ContainsKey(userIdforMulipleSessions))
                    {
                        if (UsersDictionaryWithSessionsIds[userIdforMulipleSessions] == "start")
                        {
                            UsersDictionaryWithSessionsIds[userIdforMulipleSessions] = newId;
                        }
                    }
                }
                else
                {
                    UsersDictionaryWithSessionsIds.Add(userIdforMulipleSessions, newId);

                    System.Web.HttpContext.Current.Application.Lock();
                    System.Web.HttpContext.Current.Application.Contents.Add("UsersDictionaryWithSessionsIds", UsersDictionaryWithSessionsIds);
                    System.Web.HttpContext.Current.Application.UnLock();
                }



                if (dtlogondetails.Rows[0]["LegalEntity"].ToString() == "1") // Clearning Agent
                {
                    ViewBag.showMessageStyle = "none";
                    Session["AllowmenuAfterAcountUpdate"] = "True";
                    ViewBag.Msg = "";
                    //return RedirectToAction("BrokerRenewal", "BrokerRenewal");
                    return RedirectToAction("MenuView", "Dashboard"); //------------------------------------------
                    //redirectionHomePage = Url.Action("MenuView", "Dashboard");
                    //return RedirectToAction("BrokerServiceRequestUpdates", "BrokerRenewal");
                }
                else if (dtlogondetails.Rows[0]["LegalEntity"].ToString() == "2") // Organization
                {
                    ViewBag.showMessageStyle = "none";
                    ViewBag.Msg = "";
                    if (dtlogondetails.Columns.Contains("CivilId") && dtlogondetails.Columns.Contains("LicenseNumber"))
                    {

                        if (dtlogondetails.Rows[0]["CivilId"].ToString() == "" || dtlogondetails.Rows[0]["LicenseNumber"].ToString() == "" || dtlogondetails.Rows[0]["CivilId"].ToString() == "NULL" || dtlogondetails.Rows[0]["LicenseNumber"].ToString() == "NULL")
                        {

                            Session["AllowmenuAfterAcountUpdate"] = "False";

                            return RedirectToAction("MyAccount", "User"); //----------------------------------------------
                            //redirectionHomePage = Url.Action("MyAccount", "User");

                        }

                        else
                        {
                            Session["AllowmenuAfterAcountUpdate"] = "True";
                        }
                    }
                    //return RedirectToAction("MyOrganizations", "User");
                    return RedirectToAction("MenuView", "Dashboard");// --------------------------------------
                    //redirectionHomePage = Url.Action("MenuView", "Dashboard");
                }
                return View("Index");
                //redirectionHomePage = Url.Action("Index", "Registration");
            }
            else
            {
                return View("Index");
                //redirectionHomePage = Url.Action("Index", "Registration");
            }
            //return redirectionHomePage;
        }

        public ActionResult MobileVerificationResend()
        {
            try
            {
                //ViewBag.modelstatusid = "1";
                //if (TempData["MobileNo"] != null && Session["login"] == null)
                //{
                //    ValidateContacts ObjValidateContacts = new ValidateContacts();
                //    ObjValidateContacts.Reference = TempData["MobileNo"].ToString();
                //    ObjValidateContacts.mUserid = Session["UserId"].ToString();
                //    ObjValidateContacts.tokenId = Session["TokenId"].ToString();
                //    ObjValidateContacts.ReferenceId = "";
                //    ObjValidateContacts.ReferenceType = "M";
                //    DataTable dtcheck = new DataTable();
                //    dtcheck = objdataclass.ValidateContactsWithOTP(ObjValidateContacts);
                //    if (dtcheck.Columns.Contains("MobileVerifyStatus"))
                //    {
                //        if (dtcheck.Rows[0]["MobileVerifyStatus"].ToString().Trim() == "-1")
                //        {
                //            TempData["refId"] = dtcheck.Rows[0]["MOTPreqId"].ToString().Trim();
                //            if (dtcheck.Rows[0]["MOTPreqId"].ToString().Trim() != "")
                //            {
                //                ViewBag.modelstatusid = "0";
                //                ViewBag.modelstatus = Resources.Resource.Verificationsenttoyourmobile;
                //            }
                //            else
                //            {
                //                ViewBag.modelstatusid = "0";
                //                ViewBag.modelstatus = Resources.Resource.error;
                //            }
                //        }
                //    }
                //    ViewBag.mobile = ObjValidateContacts.Reference;
                //    TempData["MobileNo"] = ObjValidateContacts.Reference;
                //}
                //if (Session["login"] != null)
                //{
                //    if (Session["login"].ToString() == "M")
                //    {
                //        resendotp("M", Resources.Resource.Verificationsenttoyourmobile);
                //    }
                //}
                if (TempData["TokenId"] != null && TempData["UserId"] != null)
                {
                    resendotp("M", Resources.Resource.Verificationsenttoyourmobile);
                }

                else
                {
                    //ViewBag.msg = "sdsadsadasd";
                }

                TempData.Keep();
                //return View("EmailVerification");
                return View("ContactVerification");
            }
            catch (Exception)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                //return View("EmailVerification");
                return View("ContactVerification");
            }
        }


        public void resendotp(string rtype, string msg)
        {
            ReSendOTPParams ObjReSendOTPParams = new ReSendOTPParams();
            ObjReSendOTPParams.mUserid = TempData["UserId"].ToString();//Session["UserId"].ToString();
            ObjReSendOTPParams.tokenId = TempData["TokenId"].ToString();//Session["TokenId"].ToString();
            TempData["TokenId"] = ObjReSendOTPParams.tokenId;
            TempData["UserId"] = ObjReSendOTPParams.mUserid;
            ObjReSendOTPParams.rType = rtype;
            DataTable dt = objdataclass.ReSendOTP(ObjReSendOTPParams);
            if (dt.Rows[0]["Status"].ToString() == "0")
            {
                //ViewBag.modelstatusid = "0";
                ViewBag.msg = msg;// Resources.Resource.VerificationsenttoyourEmail;//msg;
            }
            //else
            //{
            //    //ViewBag.modelstatusid = "0";
            //    ViewBag.msg = Resources.Resource.WrongOTPPleasetryagain;
            //}
        }
        public ActionResult EmailVerificationResend()
        {
            try
            {
                //if (Session["login"] != null)
                //{
                //    if (Session["login"].ToString() == "E")
                //    {
                //        resendotp("E", Resources.Resource.VerificationsenttoyourEmail);
                //    }
                //}
                //if (TempData["OrgId"] != null && Session["UserId"] != null && Session["TokenId"] != null)
                //{
                //    //CheckOrgEmailIsVerified
                //    OrgReqResultInfoParams objOrgReqResultInfoParams = new OrgReqResultInfoParams();
                //    objOrgReqResultInfoParams.mUserid = Session["UserId"].ToString();
                //    objOrgReqResultInfoParams.tokenId = Session["TokenId"].ToString();
                //    objOrgReqResultInfoParams.sOrgReqId = TempData["OrgId"].ToString();
                //    TempData["OrgId"] = objOrgReqResultInfoParams.sOrgReqId;
                //    DataTable dt = objdataclass.CheckOrgEmailIsVerified(objOrgReqResultInfoParams);
                //    if (dt.Rows[0]["IsOrgEmailVarified"].ToString() == "0")
                //    {
                //        ViewBag.modelstatusid = "0";
                //        ViewBag.modelstatus = Resources.Resource.VerificationsenttoyourEmail;
                //    }
                //    else
                //    {
                //        ViewBag.modelstatusid = "0";
                //        ViewBag.modelstatus = Resources.Resource.error;
                //    }
                //}
                if (TempData["TokenId"] != null && TempData["UserId"] != null)
                {
                    resendotp("E", Resources.Resource.VerificationsenttoyourEmail);
                }

                else
                {
                    //ViewBag.msg = "sdsadsadasd";
                }

                TempData.Keep();
                //return View("EmailVerification");
                return View("ContactVerification");

            }
            catch (Exception)
            {
                TempData.Keep();
                //ViewBag.modelerror = Constantclass.number5;
                ViewBag.msg = Resources.Resource.somethingwentwrong;
                //return View("EmailVerification");
                return View("ContactVerification");
            }
        }



        private string GenerateRandomWord(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890&$#@*_!";
            StringBuilder res = new StringBuilder();

            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
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

            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
        }



        void regenerateId()
        {
            System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
            string oldId = manager.GetSessionID(System.Web.HttpContext.Current);
            string newId = manager.CreateSessionID(System.Web.HttpContext.Current);
            bool isAdd = false, isRedir = false;
            manager.SaveSessionID(System.Web.HttpContext.Current, newId, out isRedir, out isAdd);
            HttpApplication ctx = (HttpApplication)System.Web.HttpContext.Current.ApplicationInstance;
            HttpModuleCollection mods = ctx.Modules;
            System.Web.SessionState.SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
            System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            SessionStateStoreProviderBase store = null;
            System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;
            foreach (System.Reflection.FieldInfo field in fields)
            {
                if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
                if (field.Name.Equals("_rqId")) rqIdField = field;
                if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
                if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
            }
            object lockId = rqLockIdField.GetValue(ssm);
            if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(System.Web.HttpContext.Current, oldId, lockId);
            rqStateNotFoundField.SetValue(ssm, true);
            rqIdField.SetValue(ssm, newId);
        }


    }



}
#endregion New Code 17-FEB




#region OLD Code 12-FEB
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Web.Mvc;
//using System.Web;
//using WebApplication1.Models;
//namespace WebApplication1.Controllers
//{
//    public class registrationController : MyBaseController
//    {
//        // GET: registration
//        Dataclass objdataclass = new Dataclass();



//        public ActionResult Index()
//        {
//            try
//            {
//                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
//                {
//                    ViewBag.UserName = Request.Cookies["UserName"].Value;
//                    ViewBag.Pwd = Request.Cookies["Password"].Value;
//                }
//                TempData.Clear();
//                Session.Clear();
//                ViewBag.modelerror = Constantclass.number1;
//                return View();
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.Msg = Resources.Resource.somethingwentwrong;
//                return View();
//            }
//        }
//        [HttpPost]
//        public ActionResult Index(LogOnRequest objlogon)
//        {
//            try
//            {
//                TempData.Clear();
//                Session.Clear();
//                ViewBag.modelerror = Constantclass.number1;
//                TempData["MyOrgData"] = null;
//                TempData["data"] = null;
//                TempData["MyOrgReqData"] = null;
//                TempData["Paymentdata"] = null;
//                Session["notifycount"] = null;
//                TempData["Notifylist"] = null;
//                HttpCookie langCookie = Request.Cookies["culture"];
//                objlogon.Lang = langCookie.Value;
//                if (objlogon.gln!=null)
//                {
//                    Session["Geolocation"] = objlogon.gln;
//                }
//                if (!(ModelState.IsValid))
//                {
//                    ViewBag.modelerror = Constantclass.number;
//                    ViewBag.UserName = objlogon.email;
//                    return View("Index");
//                }
//                DataTable dtlogondetails = objdataclass.Logonmethod(objlogon);

//                if (objlogon.RememberMe)
//                {
//                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
//                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
//                    Response.Cookies["UserName"].Value = objlogon.email;
//                    Response.Cookies["Password"].Value = objlogon.pwd;
//                }
//                else
//                {
//                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
//                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
//                }
//                ViewBag.ok = "0";
//                if (dtlogondetails.Rows.Count > 0)
//                {
//                    if (dtlogondetails.Rows[0]["TokenId"].ToString() != "" && dtlogondetails.Rows[0]["Status"].ToString() == "0")
//                    {
//                        Session["UserName"] = dtlogondetails.Rows[0]["FirstName"].ToString().ToUpper() + " " + dtlogondetails.Rows[0]["LastName"].ToString().ToUpper();
//                        Session["UserId"] = dtlogondetails.Rows[0]["UserId"].ToString();
//                        Session["TokenId"] = dtlogondetails.Rows[0]["TokenId"].ToString();
//                        Session["AllowedMenus"] = dtlogondetails.Rows[0]["AllowedMenus"].ToString();
//                        if (dtlogondetails.Rows[0]["EtradeAppVerificationMode"].ToString() == "E")
//                        {
//                            if (dtlogondetails.Rows[0]["IsEmailVerified"].ToString() == "-1")
//                            {
//                                Session["login"] = dtlogondetails.Rows[0]["EtradeAppVerificationMode"].ToString();
//                                return RedirectToAction("MyAccount", "User");
//                            }
//                        }
//                        else if (dtlogondetails.Rows[0]["EtradeAppVerificationMode"].ToString() == "M")
//                        {
//                            if (dtlogondetails.Rows[0]["IsMobileVerified"].ToString() == "-1")
//                            {
//                                return RedirectToAction("MyAccount", "User");
//                            }
//                        }
//                        else if (dtlogondetails.Rows[0]["EtradeAppVerificationMode"].ToString() == "EM")
//                        {
//                            if (dtlogondetails.Rows[0]["IsMobileVerified"].ToString() == "-1" || dtlogondetails.Rows[0]["IsMobileVerified"].ToString() == "-1")
//                            {
//                                return RedirectToAction("MyAccount", "User");
//                            }
//                        }
//                        return RedirectToAction("MyOrganizations", "User");
//                    }
//                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-2")
//                    {
//                        ViewBag.Msg = Resources.Resource.ActioncannotbeperformedPleasecontactadministrator;
//                        ViewBag.modelerror = Constantclass.number5;
//                    }
//                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-3")
//                    {
//                        ViewBag.Msg = Resources.Resource.Passwordsarenotmatching;
//                        ViewBag.modelerror = Constantclass.number5;
//                    }
//                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-5")
//                    {
//                        ViewBag.ok = "1";
//                        ViewBag.Msg = Resources.Resource.Multipleinvalidpassword;
//                        ViewBag.modelerror = Constantclass.number5;
//                    }
//                    else if (dtlogondetails.Rows[0]["Status"].ToString() == "-1")
//                    {
//                        ViewBag.Msg = Resources.Resource.wrongcredentials;
//                        ViewBag.modelerror = Constantclass.number5;
//                    }
//                    return View("Index");
//                }
//                else
//                {
//                    // ModelState.AddModelError(string.Empty, Resources.Resource.Servernotresponding);
//                    ViewBag.Msg = Resources.Resource.Servernotresponding;
//                    ViewBag.modelerror = Constantclass.number5;
//                    return View("Index");
//                }

//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.Msg = Resources.Resource.somethingwentwrong;
//                return View("Index");
//            }
//        }
//        private Captcha getcaptcha(int captchaid)
//        {
//            try
//            {
//                Captcha objcaptcha = new Captcha();
//                objcaptcha.CaptchaId = captchaid;
//                Captcha objCaptchadetails = objdataclass.GetCaptcha(objcaptcha);
//                if (objCaptchadetails != null)
//                {
//                    ViewData["CaptchaImage"] = objCaptchadetails.CaptchaImage;
//                    TempData["CaptchaImage"] = objCaptchadetails.CaptchaImage;
//                    TempData["CaptchaId"] = objCaptchadetails.CaptchaId;
//                    ViewData["CaptchaId"] = objCaptchadetails.CaptchaId;

//                }
//                else
//                {
//                    ViewData["CaptchaImage"] = "";
//                    TempData["CaptchaImage"] = "";
//                    TempData["CaptchaId"] = "0";
//                    ViewData["CaptchaId"] = "0";
//                }
//                return objCaptchadetails;
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        public ActionResult Registration()
//        {
//            try
//            {
//                ViewBag.modelstatusid = "1";
//                ViewBag.modelstatus = "1";
//                ViewBag.modelerror = Constantclass.number1;
//                getcaptcha(0);
//                List<User> objlist = new List<User>();
//                User objuser = new User();
//                objlist.Add(objuser);
//                ViewData["data"] = objlist;
//                return View("Registration");
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View("Registration");
//            }
//        }
//        public JsonResult Registration1(string captchaid)
//        {
//            try
//            {
//                ViewBag.modelstatusid = "1";
//                ViewBag.modelstatus = "1";
//                ViewBag.modelerror = Constantclass.number1;
//                Captcha result = getcaptcha(Convert.ToInt32(captchaid));

//                return Json(result, JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        [HttpPost]
//        public ActionResult Registration(User objUser)
//        {
//            try
//            {
//                ViewBag.modelstatusid = "1";
//                ViewBag.modelstatus = "1";
//                ViewBag.modelerror = Constantclass.number1;
//                if (!(ModelState.IsValid))
//                {
//                    getcaptcha(Convert.ToInt32(TempData["CaptchaId"]));
//                    objUser.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
//                    TempData["CaptchaId"] = objUser.CaptchaId;
//                    List<User> objlist = new List<User>();
//                    objlist.Add(objUser);
//                    ViewData["data"] = objlist;
//                    ViewBag.modelerror = Constantclass.number;
//                    return View("Registration");
//                }
//                //        if (ModelState.IsValid)
//                //        {

//                //            //check whether name is already exists in the database or not
//                //            bool nameAlreadyExists = *check database *


//                //if (nameAlreadyExists)
//                //            {
//                //                ModelState.AddModelError(string.Empty, "Student Name already exists.");

//                //                return View(std);
//                //            }
//                //        }

//                HttpCookie langCookie = Request.Cookies["culture"];
//                objUser.Language = langCookie.Value;
//                objUser.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
//                DataTable objCaptchadetails = objdataclass.SignUp(objUser);
//                if (objCaptchadetails.Rows.Count > 0)
//                {
//                    if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "0")
//                    {
//                        TempData["EmailKeyValTemporary"] = objCaptchadetails.Rows[0]["EmailKeyValTemporary"].ToString();
//                        TempData["TokenId"] = objCaptchadetails.Rows[0]["TokenId"].ToString();
//                        TempData["UserId"] = objCaptchadetails.Rows[0]["UserId"].ToString();
//                        ViewBag.modelstatusid = "0";
//                        ViewBag.ok = "1";
//                        ViewBag.modelstatus = Resources.Resource.VerificationsenttoyourEmail;
//                        TempData["Email"] = objUser.EmailId;
//                    }
//                    else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-1")
//                    {
//                        ViewBag.modelstatusid = "0";
//                        ViewBag.modelstatus = Resources.Resource.emailExist;
//                    }
//                    else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-9")
//                    {
//                        ViewBag.modelstatusid = "0";
//                        ViewBag.modelstatus = Resources.Resource.validpassword;
//                    }
//                    else if (objCaptchadetails.Rows[0]["Status"].ToString().Trim() == "-8")
//                    {
//                        ViewBag.modelstatusid = "0";
//                        ViewBag.modelstatus = Resources.Resource.EntercorrectCaptcha;
//                    }
//                }
//                getcaptcha(Convert.ToInt32(TempData["CaptchaId"]));
//                objUser.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
//                TempData["CaptchaId"] = objUser.CaptchaId;
//                List<User> objlistuser = new List<User>();
//                objlistuser.Add(objUser);
//                ViewData["data"] = objlistuser;
//                return View("Registration");
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View("Registration");
//            }
//        }
//        public ActionResult ChangeLanguage(string lang)
//        {
//            try
//            {
//                Session["lng"] = lang;
//                new SiteLanguages().SetLanguage(lang);
//                return RedirectToAction("Start", "registration");
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return RedirectToAction("Start", "registration");
//            }
//        }
//        public ActionResult Forgot_Password()
//        {
//            try
//            {
//                ViewBag.disabled = Constantclass.disnone;
//                getcaptcha(0);
//                ViewBag.modelerror = Constantclass.number1;
//                List<ForgotPwdOTPinput> objlist = new List<ForgotPwdOTPinput>();
//                ForgotPwdOTPinput objforgot = new ForgotPwdOTPinput();
//                objlist.Add(objforgot);
//                ViewData["data"] = objlist;
//                return View();
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View();
//            }
//        }
//        [HttpPost]
//        public ActionResult Forgot_Password(ForgotPwdOTPinput objinput)
//        {
//            try
//            {
//                ViewBag.modelerror = Constantclass.number1;
//                ViewBag.disabled = Constantclass.disnone;
//                if (!(ModelState.IsValid))
//                {
//                    getcaptcha(Convert.ToInt32(TempData["CaptchaId"]));
//                    objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
//                    TempData["CaptchaId"] = objinput.CaptchaId;
//                    List<ForgotPwdOTPinput> objlist = new List<ForgotPwdOTPinput>();
//                    objlist.Add(objinput);
//                    ViewData["data"] = objlist;
//                    ViewBag.modelerror = Constantclass.number;
//                    return View();
//                }
//                string input = objinput.emailId.ToString().Trim();
//                double num;
//                if (double.TryParse(input, out num))
//                {
//                    objinput.emailId = "";
//                    objinput.mobileNo = input;
//                }
//                else
//                {
//                    objinput.emailId = input;
//                    objinput.mobileNo = "";
//                }

//                objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
//                DataTable objForgotPwdOTPResult = objdataclass.ForgotPwdOTPResult(objinput);
//                string status = string.Empty;
//                if (objForgotPwdOTPResult.Rows.Count > 0)
//                {
//                    if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "-8")
//                    {
//                        status = Resources.Resource.EntercorrectCaptcha;
//                        ViewBag.modelerror = Constantclass.number;
//                        ModelState.AddModelError(string.Empty, status);
//                    }
//                    else if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "-1")
//                    {
//                        status = "Enter correct Captcha";
//                        ViewBag.modelerror = Constantclass.number;
//                        //ModelState.AddModelError(string.Empty, "Status : " + status);
//                        ModelState.AddModelError(string.Empty, status);
//                    }
//                    else if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "0" && objForgotPwdOTPResult.Rows[0]["OTPKeyId"].ToString().Trim() != "")
//                    {
//                        ViewBag.modelerror = Constantclass.number2;
//                        if (objinput.mobileNo != "")
//                        {
//                            ViewBag.msg = Resources.Resource.Verificationsenttoyourmobile + ":" + objinput.mobileNo;
//                        }
//                        else
//                        {
//                            ViewBag.msg = Resources.Resource.VerificationsenttoyourEmail + ": " + objinput.emailId;
//                        }
//                        ViewBag.disabled = Constantclass.disblock;
//                    }
//                }

//                TempData["dt"] = objForgotPwdOTPResult;
//                getcaptcha(Convert.ToInt32(TempData["CaptchaId"]));
//                objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
//                TempData["CaptchaId"] = objinput.CaptchaId;
//                List<ForgotPwdOTPinput> objlist1 = new List<ForgotPwdOTPinput>();
//                objlist1.Add(objinput);
//                ViewData["data"] = objlist1;

//                return View();
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View();
//            }
//        }
//        public ActionResult Forgot_ResetPassword(ForgotPwdOTPinput objForgotPwdOTPinput)
//        {
//            try
//            {
//                ViewBag.modelerror = Constantclass.number1;
//                ResetPwdByOTPParams objinput = new ResetPwdByOTPParams();
//                DataTable dt = new DataTable();
//                if (TempData["dt"] != null)
//                {
//                    dt = (DataTable)TempData["dt"];
//                    if (dt.Rows.Count > 0)
//                    {
//                        objinput.mUserid = dt.Rows[0]["mUserId"].ToString().Trim();
//                        objinput.otpId = dt.Rows[0]["OTPKeyId"].ToString().Trim();
//                        //objinput.otpValue = dt.Rows[0]["OTPKeyVal"].ToString().Trim();
//                        objinput.otpValue = objForgotPwdOTPinput.otpValue;
//                        objinput.newPwd = objForgotPwdOTPinput.newPwd;
//                    }
//                }
//                DataTable obresetOTPResult = objdataclass.ResetPwdOTPResult(objinput);
//                if (obresetOTPResult.Rows.Count > 0)
//                {
//                    if (obresetOTPResult.Rows[0]["Status"].ToString().Trim() == "0")
//                    {
//                        ViewBag.modelerror = Constantclass.number3;
//                        ViewBag.msg = Resources.Resource.loginwithyourcredentials;
//                    }

//                }
//                List<ForgotPwdOTPinput> objlist1 = new List<ForgotPwdOTPinput>();
//                objlist1.Add(objForgotPwdOTPinput);
//                ViewData["data"] = objlist1;
//                return View("Forgot_Password");
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View("Forgot_Password");
//            }
//        }
//        public ActionResult ResetPassword()
//        {
//            try
//            {
//                ViewBag.disabled = Constantclass.disnone;
//                getcaptcha(0);
//                ViewBag.modelerror = Constantclass.number1;
//                List<ForgotPwdOTPinput> objlist = new List<ForgotPwdOTPinput>();
//                ForgotPwdOTPinput objforgot = new ForgotPwdOTPinput();
//                objlist.Add(objforgot);
//                ViewData["data"] = objlist;
//                return View();
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View();
//            }
//        }
//        [HttpPost]
//        public ActionResult ResetPassword(ForgotPwdOTPinput objinput)
//        {
//            try
//            {
//                ViewBag.modelerror = Constantclass.number1;
//                ViewBag.disabled = Constantclass.disnone;
//                if (!(ModelState.IsValid))
//                {
//                    getcaptcha(Convert.ToInt32(TempData["CaptchaId"]));
//                    objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
//                    TempData["CaptchaId"] = objinput.CaptchaId;
//                    List<ForgotPwdOTPinput> objlist = new List<ForgotPwdOTPinput>();
//                    objlist.Add(objinput);
//                    ViewData["data"] = objlist;
//                    ViewBag.modelerror = Constantclass.number;
//                    return View();
//                }
//                string input = objinput.emailId.ToString().Trim();
//                double num;
//                if (double.TryParse(input, out num))
//                {
//                    objinput.emailId = "";
//                    objinput.mobileNo = input;
//                }
//                else
//                {
//                    objinput.emailId = input;
//                    objinput.mobileNo = "";
//                }

//                objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
//                DataTable objForgotPwdOTPResult = objdataclass.ForgotPwdOTPResult(objinput);
//                string status = string.Empty;
//                if (objForgotPwdOTPResult.Rows.Count > 0)
//                {
//                    if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "-8")
//                    {
//                        status = Resources.Resource.EntercorrectCaptcha;
//                        ViewBag.modelerror = Constantclass.number;
//                        ModelState.AddModelError(string.Empty, status);
//                    }
//                    else if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "-1")
//                    {
//                        status = "Enter correct Captcha";
//                        ViewBag.modelerror = Constantclass.number;
//                        //ModelState.AddModelError(string.Empty, "Status : " + status);
//                        ModelState.AddModelError(string.Empty, status);
//                    }
//                    else if (objForgotPwdOTPResult.Rows[0]["Status"].ToString().Trim() == "0" && objForgotPwdOTPResult.Rows[0]["OTPKeyId"].ToString().Trim() != "")
//                    {
//                        ViewBag.modelerror = Constantclass.number2;
//                        if (objinput.mobileNo != "")
//                        {
//                            ViewBag.msg = Resources.Resource.Verificationsenttoyourmobile + ":" + objinput.mobileNo;
//                        }
//                        else
//                        {
//                            ViewBag.msg = Resources.Resource.VerificationsenttoyourEmail + ": " + objinput.emailId;
//                        }
//                        ViewBag.disabled = Constantclass.disblock;
//                    }
//                }

//                TempData["dt"] = objForgotPwdOTPResult;
//                getcaptcha(Convert.ToInt32(TempData["CaptchaId"]));
//                objinput.CaptchaId = Convert.ToInt32(TempData["CaptchaId"]);
//                TempData["CaptchaId"] = objinput.CaptchaId;
//                List<ForgotPwdOTPinput> objlist1 = new List<ForgotPwdOTPinput>();
//                objlist1.Add(objinput);
//                ViewData["data"] = objlist1;

//                return View();
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View();
//            }
//        }
//        public ActionResult Reset_Password(ForgotPwdOTPinput objForgotPwdOTPinput)
//        {
//            try
//            {
//                ViewBag.modelerror = Constantclass.number1;
//                ResetPwdByOTPParams objinput = new ResetPwdByOTPParams();
//                DataTable dt = new DataTable();
//                if (TempData["dt"] != null)
//                {
//                    dt = (DataTable)TempData["dt"];
//                    if (dt.Rows.Count > 0)
//                    {
//                        objinput.mUserid = dt.Rows[0]["mUserId"].ToString().Trim();
//                        objinput.otpId = dt.Rows[0]["OTPKeyId"].ToString().Trim();
//                        //objinput.otpValue = dt.Rows[0]["OTPKeyVal"].ToString().Trim();
//                        objinput.otpValue = objForgotPwdOTPinput.otpValue;
//                        objinput.newPwd = objForgotPwdOTPinput.newPwd;
//                    }
//                }
//                DataTable obresetOTPResult = objdataclass.ResetPwdOTPResult(objinput);
//                if (obresetOTPResult.Rows.Count > 0)
//                {
//                    if (obresetOTPResult.Rows[0]["Status"].ToString().Trim() == "0")
//                    {
//                        ViewBag.modelerror = Constantclass.number3;
//                        ViewBag.msg = Resources.Resource.loginwithyourcredentials;
//                    }

//                }
//                List<ForgotPwdOTPinput> objlist1 = new List<ForgotPwdOTPinput>();
//                objlist1.Add(objForgotPwdOTPinput);
//                ViewData["data"] = objlist1;
//                return View("ResetPassword");
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View("ResetPassword");
//            }
//        }
//        public ActionResult EmailVerification()
//        {
//            try
//            {
//                ViewBag.modelstatusid = "1";
//                if (TempData["MobileNo"] != null)
//                {
//                    ViewBag.mobile = TempData["MobileNo"].ToString();
//                    TempData["MobileNo"] = ViewBag.mobile;
//                }
//                TempData.Keep();
//                return View();
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View();
//            }
//        }
//        [HttpPost]
//public ActionResult EmailVerification(VerifyOTPParams objVerifyOTPParams)
//{
//    try
//    {
//        ViewBag.modelstatusid = "1";
//        if (!(ModelState.IsValid))
//        {
//            TempData.Keep();
//            ViewBag.modelstatusid = "0";
//            ViewBag.modelstatus = "Please Enter Code";
//            return View("EmailVerification");
//        }
//        DataTable objres = new DataTable();
//        if (TempData["EmailKeyValTemporary"] != null && TempData["TokenId"] != null && TempData["UserId"] != null)
//        {
//            objVerifyOTPParams.tokenId = TempData["TokenId"].ToString();
//            objVerifyOTPParams.mUserid = TempData["UserId"].ToString();
//            string EmailKeyValTemporary = TempData["EmailKeyValTemporary"].ToString();
//            objres = objdataclass.EmailVerificationdata(objVerifyOTPParams);
//            TempData["TokenId"] = objVerifyOTPParams.tokenId;
//            TempData["UserId"] = objVerifyOTPParams.mUserid;
//        }
//        if (Session["login"] != null && Session["TokenId"] != null && Session["UserId"] != null)
//        {
//            if (Session["login"].ToString() == "M")
//            {
//                objVerifyOTPParams.Mobile = objVerifyOTPParams.Email;
//                objVerifyOTPParams.Email = "";
//            }
//            objVerifyOTPParams.tokenId = Session["TokenId"].ToString();
//            objVerifyOTPParams.mUserid = Session["UserId"].ToString();
//            objres = objdataclass.EmailVerificationdata(objVerifyOTPParams);
//        }
//        if (Session["UserId"] != null && Session["TokenId"] != null)
//        {
//            objVerifyOTPParams.mUserid = Session["UserId"].ToString();
//            objVerifyOTPParams.tokenId = Session["TokenId"].ToString();
//            if (TempData["OrgId"] != null)
//            {
//                objVerifyOTPParams.ReferenceId = TempData["OrgId"].ToString();
//                objVerifyOTPParams.Mobile = "";
//                objres = objdataclass.EmailVerificationdata(objVerifyOTPParams);
//                TempData["OrgId"] = objVerifyOTPParams.ReferenceId;
//            }
//            if (TempData["MobileNo"] != null && TempData["refId"] != null)
//            {
//                ViewBag.mobile = TempData["MobileNo"].ToString();

//                ValidateContacts ObjValidateContacts = new ValidateContacts();
//                ObjValidateContacts.Reference = objVerifyOTPParams.Email;
//                ObjValidateContacts.mUserid = Session["UserId"].ToString();
//                ObjValidateContacts.tokenId = Session["TokenId"].ToString();
//                ObjValidateContacts.ReferenceType = "M";
//                ObjValidateContacts.ReferenceId = TempData["refId"].ToString();
//                objres = objdataclass.ValidateContactsWithOTP(ObjValidateContacts);
//                TempData["refId"] = ObjValidateContacts.ReferenceId;
//                TempData["MobileNo"] = ViewBag.mobile;
//            }
//        }

//        if (objres.Rows.Count > 0)
//        {
//            string res = string.Empty;
//            if (objres.Columns.Contains("VerifyStatus") && TempData["OrgId"] != null)
//            {
//                if (objres.Rows[0]["VerifyStatus"].ToString().Trim() == "0")
//                {
//                    res = "Verified";
//                }
//            }
//            if (objres.Columns.Contains("VerifyStatus") && TempData["UserId"] != null)
//            {
//                if (objres.Rows[0]["VerifyStatus"].ToString().Trim() == "0")
//                {
//                    res = "Verified";
//                }
//            }
//            if (objres.Columns.Contains("MobileVerifyStatus") && TempData["MobileNo"] != null && TempData["refId"] != null)
//            {
//                if (objres.Rows[0]["MobileVerifyStatus"].ToString().Trim() == "0")
//                {
//                    res = "Verified";
//                }
//            }
//            if (objres.Columns.Contains("VerifyStatus") && Session["login"] != null)
//            {
//                if (objres.Rows[0]["VerifyStatus"].ToString().Trim() == "0")
//                {
//                    res = "Verified";
//                }
//            }
//            if (res == "Verified")
//            {
//                if (objVerifyOTPParams.ReferenceId != null && Session["login"] == null)
//                {
//                    return RedirectToAction("UploadDocument", "User");
//                }
//                else
//                {
//                    if (TempData["MobileNo"] != null || Session["login"] != null)
//                    {
//                        Session["login"] = null;
//                        return RedirectToAction("MyAccount", "User");
//                    }
//                    else
//                    {
//                        ViewBag.ok = "3";
//                    }

//                }
//                ViewBag.modelstatusid = "0";
//                ViewBag.modelstatus = Resources.Resource.loginwithyourcredentials;
//            }
//            else
//            {
//                ViewBag.modelstatusid = "0";
//                ViewBag.modelstatus = Resources.Resource.VerificationFail;
//            }
//        }
//        if (TempData["MobileNo"] != null)
//        {
//            TempData["MobileNo"] = TempData["MobileNo"].ToString();
//        }
//        if (TempData["refId"] != null)
//        {
//            TempData["refId"] = TempData["refId"].ToString();
//        }
//        if (TempData["OrgId"] != null)
//        {
//            TempData["OrgId"] = TempData["OrgId"].ToString();
//        }
//        if (TempData["EmailKeyValTemporary"] != null && TempData["TokenId"] != null && TempData["UserId"] != null)
//        {
//            TempData["EmailKeyValTemporary"] = TempData["EmailKeyValTemporary"].ToString();
//            TempData["TokenId"] = TempData["TokenId"].ToString();
//            TempData["UserId"] = TempData["UserId"].ToString();
//        }
//        TempData.Keep();
//        return View();
//    }
//    catch (Exception)
//    {
//        ViewBag.modelerror = Constantclass.number5;
//        ViewBag.msg = Resources.Resource.somethingwentwrong;
//        return View();
//    }
//}
//        public ActionResult MobileVerificationResend()
//        {
//            try
//            {
//                ViewBag.modelstatusid = "1";
//                if (TempData["MobileNo"] != null && Session["login"] == null)
//                {
//                    ValidateContacts ObjValidateContacts = new ValidateContacts();
//                    ObjValidateContacts.Reference = TempData["MobileNo"].ToString();
//                    ObjValidateContacts.mUserid = Session["UserId"].ToString();
//                    ObjValidateContacts.tokenId = Session["TokenId"].ToString();
//                    ObjValidateContacts.ReferenceId = "";
//                    ObjValidateContacts.ReferenceType = "M";
//                    DataTable dtcheck = new DataTable();
//                    dtcheck = objdataclass.ValidateContactsWithOTP(ObjValidateContacts);
//                    if (dtcheck.Columns.Contains("MobileVerifyStatus"))
//                    {
//                        if (dtcheck.Rows[0]["MobileVerifyStatus"].ToString().Trim() == "-1")
//                        {
//                            TempData["refId"] = dtcheck.Rows[0]["MOTPreqId"].ToString().Trim();
//                            if (dtcheck.Rows[0]["MOTPreqId"].ToString().Trim() != "")
//                            {
//                                ViewBag.modelstatusid = "0";
//                                ViewBag.modelstatus = Resources.Resource.Verificationsenttoyourmobile;
//                            }
//                            else
//                            {
//                                ViewBag.modelstatusid = "0";
//                                ViewBag.modelstatus = Resources.Resource.error;
//                            }
//                        }
//                    }
//                    ViewBag.mobile = ObjValidateContacts.Reference;
//                    TempData["MobileNo"] = ObjValidateContacts.Reference;
//                }
//                if (Session["login"] != null)
//                {
//                    if (Session["login"].ToString() == "M")
//                    {
//                        resendotp("M", Resources.Resource.Verificationsenttoyourmobile);
//                    }
//                }
//                TempData.Keep();
//                return View("EmailVerification");
//            }
//            catch (Exception)
//            {
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View("EmailVerification");
//            }
//        }
//        public void resendotp(string rtype, string msg)
//        {
//            ReSendOTPParams ObjReSendOTPParams = new ReSendOTPParams();
//            ObjReSendOTPParams.mUserid = Session["UserId"].ToString();
//            ObjReSendOTPParams.tokenId = Session["TokenId"].ToString();
//            TempData["TokenId"] = ObjReSendOTPParams.tokenId;
//            TempData["UserId"] = ObjReSendOTPParams.mUserid;
//            ObjReSendOTPParams.rType = rtype;
//            DataTable dt = objdataclass.ReSendOTP(ObjReSendOTPParams);
//            if (dt.Rows[0]["Status"].ToString() == "0")
//            {
//                ViewBag.modelstatusid = "0";
//                ViewBag.modelstatus = msg;
//            }
//            else
//            {
//                ViewBag.modelstatusid = "0";
//                ViewBag.modelstatus = Resources.Resource.error;
//            }
//        }
//        public ActionResult EmailVerificationResend()
//        {
//            try
//            {
//                ViewBag.modelstatusid = "1";
//                if (Session["login"] != null)
//                {
//                    if (Session["login"].ToString() == "E")
//                    {
//                        resendotp("E", Resources.Resource.VerificationsenttoyourEmail);
//                    }
//                }
//                if (TempData["OrgId"] != null && Session["UserId"] != null && Session["TokenId"] != null)
//                {
//                    //CheckOrgEmailIsVerified
//                    OrgReqResultInfoParams objOrgReqResultInfoParams = new OrgReqResultInfoParams();
//                    objOrgReqResultInfoParams.mUserid = Session["UserId"].ToString();
//                    objOrgReqResultInfoParams.tokenId = Session["TokenId"].ToString();
//                    objOrgReqResultInfoParams.sOrgReqId = TempData["OrgId"].ToString();
//                    TempData["OrgId"] = objOrgReqResultInfoParams.sOrgReqId;
//                    DataTable dt = objdataclass.CheckOrgEmailIsVerified(objOrgReqResultInfoParams);
//                    if (dt.Rows[0]["IsOrgEmailVarified"].ToString() == "0")
//                    {
//                        ViewBag.modelstatusid = "0";
//                        ViewBag.modelstatus = Resources.Resource.VerificationsenttoyourEmail;
//                    }
//                    else
//                    {
//                        ViewBag.modelstatusid = "0";
//                        ViewBag.modelstatus = Resources.Resource.error;
//                    }
//                }
//                else if (TempData["TokenId"] != null && TempData["UserId"] != null)
//                {
//                    resendotp("E", Resources.Resource.VerificationsenttoyourEmail);
//                }
//                TempData.Keep();
//                return View("EmailVerification");

//            }
//            catch (Exception)
//            {
//                TempData.Keep();
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.msg = Resources.Resource.somethingwentwrong;
//                return View("EmailVerification");
//            }
//        }
//    }
//}
#endregion OLD Code 12-FEB