﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Net.Http;
using DocumentManagementServices;
using MicroClear.EnterpriseSolutions.CryptographyServices;
using MicroClear.EnterpriseSolutions.ServiceFactories;
using System.IO;
using System.Globalization;
using System.Web.Services;
using System.Diagnostics;
namespace WebApplication1.Controllers

{
    public class BrokerRenewalController : MyBaseController
    {
        #region variables
        Dataclass objdataclass = new Dataclass();
        DataSet ds;
        CommonFunctions Cf;
        string sPwd = "";
        public string paymentMode = ConfigurationManager.AppSettings["PaymentMode"].ToString();
        public string PaymentTypeId = ConfigurationManager.AppSettings["PaymentTypeId"].ToString();
        SymmetricEncryption CgServices1 = ServiceFactory.GetSymmetricEncryptionInstance();
        public string paidfor = ConfigurationManager.AppSettings["paidfor"].ToString();
        public string paidby = ConfigurationManager.AppSettings["paidby"].ToString();

        public string urltoredirectforpayments = ConfigurationManager.AppSettings["csPaymentURL"].ToString();

        public string redirecturl = ConfigurationManager.AppSettings["redirecturl"].ToString();
        public string sSLUN = string.Empty;
        public string sSLD = string.Empty;
        public string sSLP = string.Empty;
        public string sSFP = string.Empty;
        public string ShareFolderPath = string.Empty;

        // 27-FEB




        String RenewalRequestReferenceProfile;

        String ServiceId ;
        #region logger
        public void WriteToLogFile(string inputvalue, string sessionDetails)
        {

            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(sessionDetails + "Inpu" +
                    "tValue>" + inputvalue, EventLogEntryType.Information, 101, 1);
            }

        }

        #endregion

        public ActionResult Index()
        {
            CheckSession();
            return View();
        }

        #region BrokerRenwal Service And Doc Upload
        private String checkNullOrEmpty(object obj, int Type)
        {

            // Type 1 : Date
            // Type 2 : String
            // Type 3 : int

            String result = String.Empty;



            switch (Type)
            {
                case 1:

                    if (obj != null)
                    {
                        if (!String.IsNullOrEmpty(obj.ToString()))
                        {
                            result = Convert.ToDateTime(obj.ToString()).ToString("yyyy-MM-dd");

                        }
                        else
                        {
                            result = "2018-01-01";
                        }
                    }

                    else
                    {
                        result = "2018-01-01";
                    }
                    break;
                case 2:
                    result = obj.ToString();
                    break;
                default:
                    break;
            }

            return result;

        }



        [HttpGet]

        public ActionResult BrokerTransfer(string ServiceId)
        {
            Session["ServiceId"] = ServiceId.ToString();
            BrokerUpdateModel ObjbrokerTransferModel = new BrokerUpdateModel();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }

            try
            {
                if (Session["ServiceId"] != null)
                {

             
                HttpCookie langCookie = Request.Cookies["culture"];
                ObjbrokerTransferModel.lang = langCookie.Value == "en" ? "eng" : "ar";//"eng";
                ObjbrokerTransferModel.Userid = Session["UserOrgID"].ToString();
                    ObjbrokerTransferModel.tokenId = Session["TokenId"].ToString();
                 ObjbrokerTransferModel.TypeOfAction = "get";
                    ds = new DataSet();
                ds = objdataclass.FNGetBrokerTypelist(ObjbrokerTransferModel);

                if (ds != null && ds.Tables["GetBrokerTypeList"].Rows.Count > 0)

                {
                    var DropdownBind = from s in ds.Tables["GetBrokerTypeList"].AsEnumerable()
                                       select new SelectListItem
                                       {
                                           //     Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
                                           Text = s["Name"].ToString(),
                                           Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
                                           // Value = s["DeclarationDocumentId"].ToString(),

                                           //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)

                                       };
                    ObjbrokerTransferModel.ddlDocumentTypesitems = DropdownBind.ToList();
                    }
                    //   ViewBag.display = "block";

                }
            }
            catch(Exception ex)
            {
                WriteToLogFile(ex.ToString(), ex.StackTrace.ToString());
            }

                return View("BrokerTransfer", ObjbrokerTransferModel);
        }

        [HttpPost]

        public ActionResult BrokerTransferPost(BrokerUpdateModel ObjbrokerTransferModel)
        {

          //  BrokerUpdateModel ObjbrokerTransferModel = new BrokerUpdateModel();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }

            try
            {
                if (Session["ServiceId"] != null)
                {


                    HttpCookie langCookie = Request.Cookies["culture"];
                    ObjbrokerTransferModel.lang = langCookie.Value == "en" ? "eng" : "ar";//"eng";
                    ObjbrokerTransferModel.Userid = Session["UserOrgID"].ToString();
                    ObjbrokerTransferModel.tokenId = Session["TokenId"].ToString();
                    ObjbrokerTransferModel.Serviceid = Convert.ToInt32(Session["ServiceId"]);
                    ObjbrokerTransferModel.BrokerType = CommonFunctions.CsUploadDecrypt(ObjbrokerTransferModel.docsid.ToString());
                    ObjbrokerTransferModel.TypeOfAction = "Post";
                    ds = new DataSet();
                    ds = objdataclass.FNGetBrokerTypelist(ObjbrokerTransferModel);

                    if (ds.Tables.Count != 0) 

                    {
                        if (ds.Tables.Contains("GetBrokerOrgId"))
                        { 
                            if (ds.Tables["GetBrokerOrgId"]!=null && ds.Tables["GetBrokerOrgId"].Rows.Count > 0)

                        { 
                        ViewBag.display = "none";
                        string Orgid = CommonFunctions.CsUploadEncrypt(ds.Tables["GetBrokerOrgId"].Rows[0]["OrganizationId"].ToString());
                            if(ds.Tables["GetBrokerOrgId"].Rows[0]["BGCHECK"].ToString()== "BGNOTALLOWED")
                                {
                                    ViewBag.display = "block";
                                    ViewBag.message = Resources.Resource.BankGuranteeStatusForTransfer;
                                    if (ds.Tables["GetBrokerTypeList"] != null && ds.Tables["GetBrokerTypeList"].Rows.Count > 0)
                                    {
                                        var DropdownBind = from s in ds.Tables["GetBrokerTypeList"].AsEnumerable()
                                                           select new SelectListItem
                                                           {
                                                               //     Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
                                                               Text = s["Name"].ToString(),
                                                               Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
                                                               // Value = s["DeclarationDocumentId"].ToString(),

                                                               //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)

                                                           };
                                        ObjbrokerTransferModel.ddlDocumentTypesitems = DropdownBind.ToList();
                                    }
                                }
                            else
                                { 
                            if (ds.Tables.Contains("GetTransferRequestStatus"))
                            {
                                    if (ds.Tables["GetTransferRequestStatus"].Rows[0]["StateId"].ToString() == "EServiceRequestSubmittedState"|| ds.Tables["GetTransferRequestStatus"].Rows[0]["StateId"].ToString() == "EServiceRequestAcceptedState" || ds.Tables["GetTransferRequestStatus"].Rows[0]["StateId"].ToString() == "EServiceRequestProceedProgressState")
                                    {
                                        ViewBag.display = "block";
                                        //   ViewBag.message = "Please Enter Correct Info";
                                        //    ViewBag.message = Resources.Resource.plea
                                        ViewBag.message = Resources.Resource.BrokerTransferRequestExists;
                                        if (ds.Tables.Contains("GetBrokerTypeList"))
                                        { 
                                            if (ds.Tables["GetBrokerTypeList"] != null && ds.Tables["GetBrokerTypeList"].Rows.Count > 0)
                                            {
                                                var DropdownBind = from s in ds.Tables["GetBrokerTypeList"].AsEnumerable()
                                                                   select new SelectListItem
                                                                   {
                                                                       //     Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
                                                                       Text = s["Name"].ToString(),
                                                                       Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
                                                                       // Value = s["DeclarationDocumentId"].ToString(),

                                                                       //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)

                                                                   };
                                                ObjbrokerTransferModel.ddlDocumentTypesitems = DropdownBind.ToList();
                                            }

                                        }
                    
                            }
                                    else
                                    {
                                        var url = ConfigurationManager.AppSettings["ApplicationUrl"].ToString() + "/BrokerRenewal/BrokerUpdate?Orgid=" + Orgid;
                                             return Redirect(url);


                                    }
                                }
                            else
                                {
                                    var url = ConfigurationManager.AppSettings["ApplicationUrl"].ToString() + "/BrokerRenewal/BrokerUpdate?Orgid=" + Orgid;
                                        return Redirect(url);

                                    //var url = ConfigurationManager.AppSettings["ApplicationUrl"].ToString()+"/BrokerRenewal/BrokerUpdate?Orgid=" + Orgid;
                                    //Response.Redirect(url);
                                }
                        }
                        }
                        }
                        else
                        {
                            if (ds.Tables["GetBrokerTypeList"] != null &&  ds.Tables["GetBrokerTypeList"].Rows.Count > 0)
                            {
                                var DropdownBind = from s in ds.Tables["GetBrokerTypeList"].AsEnumerable()
                                                   select new SelectListItem
                                                   {
                                                       //     Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
                                                       Text = s["Name"].ToString(),
                                                       Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
                                                       // Value = s["DeclarationDocumentId"].ToString(),
                                                   
                                                       //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)

                                                   };
                                ObjbrokerTransferModel.ddlDocumentTypesitems = DropdownBind.ToList();
                            }

                            ViewBag.display = "block";
                            //   ViewBag.message = "Please Enter Correct Info";
                            //    ViewBag.message = Resources.Resource.plea
                            ViewBag.message= Resources.Resource.brokercorrectInfo;


                                }
                        }

                    }
                    else
                    {
                        ViewBag.display = "block";
                        ViewBag.message = Resources.Resource.brokercorrectInfo;
                    //    ds.Tables["GetBrokerTypeList"].Rows[0]["OrganizationId"] != null ? ds.Tables["GetBrokerTypeList"].Rows[0]["OrganizationId"].ToString() : "---";
                }
                    //   ViewBag.display = "block";

                }
            
            catch (Exception ex)
            {

            }

            return View("BrokerTransfer", ObjbrokerTransferModel);
        }


        [HttpGet]
        public ActionResult BrokerUpdate()
        {
            string selectedorg = "";
      

            BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }

            try
            {
                Session["MicroClearUser"] = Session["UserOrgID"].ToString();//  MicroClearUser;


                ViewBag.UserId = Session["UserId"].ToString();

                ViewBag.TokenId = Session["TokenId"].ToString();
         

                //   ;//"BRSERenewalDocs";

                ObjbrokerRenwalModel.mobileUserId = int.Parse(Session["UserId"].ToString());//"BRSERenewalDocs";

                HttpCookie langCookie = Request.Cookies["culture"];
                ObjbrokerRenwalModel.lang = langCookie.Value == "en" ? "eng" : "ar";//"eng";

                //will give service  type
               

              
                int mobileUserId = int.Parse(Session["UserId"].ToString());

                //ObjbrokerRenwalModel.Referenceprofile=""
                if (Request.QueryString["Orgid"] != null)
                {
                    string Orgid = CommonFunctions.CsUploadDecrypt(Request.QueryString["Orgid"].ToString());


                    if (Orgid == "")
                    {

                        string source = Request.RawUrl.ToString();
                        string split = "Orgid=";


                        string result = source.Substring(source.IndexOf(split) + split.Length);


                        Orgid = CommonFunctions.CsUploadDecrypt(result);




                    }

                    Session["Orgid"] = Orgid;
                    ObjbrokerRenwalModel.Organizationid = Convert.ToInt32(Orgid);
                    ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();//Session["Userid"].ToString();
                                                                                       //ObjbrokerRenwalModel.requestForMobileUserId = Convert.ToInt32(Orgid);

                }
                string RequestNumber="";
                if (Request.QueryString["RequestNumber"] != null && Request.QueryString["Orgid"] == null)
                {
                     RequestNumber = CommonFunctions.CsUploadDecrypt(Request.QueryString["RequestNumber"].ToString());
                    if (RequestNumber == "")
                    {
                        
                        string source = Request.RawUrl.ToString();
                        string split = "RequestNumber=";


                        string result = source.Substring(source.IndexOf(split) + split.Length);


                        RequestNumber = CommonFunctions.CsUploadDecrypt(result);




                    }
                    //  if (RequestNumber.Contains("LR")|| RequestNumber.Contains("LD"))
                 //   if (RequestNumber.Contains("LR") || RequestNumber.Contains("LD"))

                    {
                        ObjbrokerRenwalModel.RequestNumber = RequestNumber;
                        ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();//Session["Userid"].ToString();
                        Session["RequestNumber"] = RequestNumber;
                    }

                }
                if (Session["Orgid"] != null)
                {
                    ObjbrokerRenwalModel.Organizationid = Convert.ToInt32(Session["Orgid"]);
                    ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();
                }
               //  if (Session["ServiceId"] == null)
                {
                    if(RequestNumber.Contains("LR"))
                    {
                        Session["ServiceId"] = "2";
                    }

                    if (RequestNumber.Contains("LD"))
                    {
                        Session["ServiceId"] = "12";
                    }
                    if (RequestNumber.Contains("LC"))
                    {
                        Session["ServiceId"] = "13";
                    }

                    if (RequestNumber.Contains("LT"))
                    {
                        Session["ServiceId"] = "14";
                    }
                    if (RequestNumber.Contains("LW"))
                    {
                        Session["ServiceId"] = "15";
                    }

                    if (RequestNumber.Contains("LP"))
                    {
                        Session["ServiceId"] = "16";
                    }
                    if (RequestNumber.Contains("LI"))
                    {
                        Session["ServiceId"] = "17";
                    }


                }

                if (Session["ServiceId"] != null)
                {
                    ServiceId = Session["ServiceId"].ToString();
                    ObjbrokerRenwalModel.Referenceprofile = ConfigurationManager.AppSettings[ServiceId].ToString();
                    Session["Referenceprofile"] = ObjbrokerRenwalModel.Referenceprofile;
                    ObjbrokerRenwalModel.Serviceid = int.Parse(Session["ServiceId"].ToString());
                }

                DataSet ds = objdataclass.FNGetBrokerDetailsForUpdate(ObjbrokerRenwalModel);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ObjbrokerRenwalModel.BrokerType = ds.Tables[0].Rows[0]["BrokerType"] != null ? ds.Tables[0].Rows[0]["BrokerType"].ToString() : "---";
                    ObjbrokerRenwalModel.BrokerArabicName = ds.Tables[0].Rows[0]["BrokerNameAra"] != null ? ds.Tables[0].Rows[0]["BrokerNameAra"].ToString() : "---";
                    ObjbrokerRenwalModel.ParentBrokerName = ds.Tables[0].Rows[0]["parentname"] != null ? ds.Tables[0].Rows[0]["parentname"].ToString() : "---";
                    ObjbrokerRenwalModel.BrokerEnglishName = ds.Tables[0].Rows[0]["BrokerEnglishName"] != null ? ds.Tables[0].Rows[0]["BrokerEnglishName"].ToString() : "---";
                    ObjbrokerRenwalModel.CivilIdNo = ds.Tables[0].Rows[0]["CivilId"] != null ? ds.Tables[0].Rows[0]["CivilId"].ToString() : "---";
                    // ObjbrokerRenwalModel.CivilIdExpirydate = checkNullOrEmpty(ds.Tables[0].Rows[0]["CivilIdExpiryDate"], 1);
                    // ObjbrokerRenwalModel.PassportExpirydate = checkNullOrEmpty(ds.Tables[0].Rows[0]["PassportExpiryDate"], 1);
                    // ObjbrokerRenwalModel.TradeLicenseExpiryDate = checkNullOrEmpty(ds.Tables[0].Rows[0]["TLExpiryDate"], 1);
                    ObjbrokerRenwalModel.CivilIdExpirydate = ds.Tables[0].Rows[0]["CivilIdExpiryDate"].ToString();
                    ObjbrokerRenwalModel.PassportExpirydate = ds.Tables[0].Rows[0]["PassportExpiryDate"].ToString();
                    ObjbrokerRenwalModel.TradeLicenseExpiryDate = ds.Tables[0].Rows[0]["TLExpiryDate"].ToString();
       

                    if (Convert.ToInt32(Session["Serviceid"]) != 7 || Convert.ToInt32(Session["Serviceid"]) != 2)
                    {
                        ObjbrokerRenwalModel.TempCivilIdExpirydate = ObjbrokerRenwalModel.CivilIdExpirydate.ToString();


                        ObjbrokerRenwalModel.TempPassportExpirydate = ObjbrokerRenwalModel.PassportExpirydate.ToString();

                        ObjbrokerRenwalModel.TempTradeLicenseExpiryDate = ObjbrokerRenwalModel.TradeLicenseExpiryDate.ToString();
                    }
                    ObjbrokerRenwalModel.passportNo = ds.Tables[0].Rows[0]["PassportNo"] != null ? ds.Tables[0].Rows[0]["PassportNo"].ToString() : "---";
                    ObjbrokerRenwalModel.MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"] != null ? ds.Tables[0].Rows[0]["MobileNumber"].ToString() : "---";
                    ObjbrokerRenwalModel.MailAddress = ds.Tables[0].Rows[0]["EMail"] != null ? ds.Tables[0].Rows[0]["EMail"].ToString() : "---";
                    ObjbrokerRenwalModel.officialAddress = ds.Tables[0].Rows[0]["Address"] != null ? ds.Tables[0].Rows[0]["Address"].ToString() : "---";

                    if(ServiceId=="17")
                    {
                        ObjbrokerRenwalModel.Nationality = ds.Tables[0].Rows[0]["nationality"] != null ? ds.Tables[0].Rows[0]["nationality"].ToString() : "---";
                    }

                }
               
                if (ds != null && ds.Tables[1].Rows.Count > 0)
                {
                    //  eservicerequestnumber EServiceRequestPaidState
                    if (ds.Tables[1].Rows[0]["stateid"].ToString() == "EServiceRequestProceedState" || ds.Tables[1].Rows[0]["stateid"].ToString() == "EServiceRequestCreatedState" || ds.Tables[1].Rows[0]["stateid"].ToString() == "EServiceRequestRejectedState")
                    {

                    }
                    else
                    {
                        ObjbrokerRenwalModel.SubmitRequest = "false";
                        ObjbrokerRenwalModel.UploadDiv = "false";

                    }

                    selectedorg = ds.Tables[1].Rows[0]["AssociatedOrgIds"].ToString();
                ObjbrokerRenwalModel.SelectedOrgidForIssuance= ds.Tables[1].Rows[0]["AssociatedOrgIds"].ToString();
                    // ObjbrokerRenwalModel.NewFileName
                    ObjbrokerRenwalModel.RequestNumber = ds.Tables[1].Rows[0]["eservicerequestnumber"].ToString();
                    ObjbrokerRenwalModel.RejectionReason = ds.Tables[1].Rows[0]["RejectionRemarks"].ToString();
                    Session["RequestNumber"] = ds.Tables[1].Rows[0]["eservicerequestnumber"].ToString();
                    Session["Eservicerequestid"] = ds.Tables[1].Rows[0]["eservicerequestid"].ToString();

                    ObjbrokerRenwalModel.Eservicerequestid = CommonFunctions.CsUploadEncrypt(ds.Tables[1].Rows[0]["eservicerequestid"].ToString());


                }

                ViewBag.reqid1 = CommonFunctions.CsUploadEncrypt(Session["Eservicerequestid"].ToString());

                if(ds.Tables.Contains("EServiceDropdown"))
                {
                    if (ds != null && ds.Tables["EServiceDropdown"].Rows.Count > 0)

                    {
                        var DropdownBind = from s in ds.Tables[2].AsEnumerable()
                                           select new SelectListItem
                                           {
                                               //     Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
                                               Text = s["Name"].ToString(),
                                               Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
                                               // Value = s["DeclarationDocumentId"].ToString(),

                                               //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)

                                           };
                        ObjbrokerRenwalModel.ddlDocumentTypesitems = DropdownBind.ToList();

                    }
                    else
                    {
                    }
                }
                if (ds.Tables.Contains("EServiceUploadedFiles"))
                {
                    var docsUploadedlist = from s in ds.Tables["EServiceUploadedFiles"].AsEnumerable()
                                           select new BrFileResult
                                           {

                                               //  }),
                                               DocumentId = HttpUtility.UrlEncode(CommonFunctions.CsUploadEncrypt(s["Documentid"].ToString())),
                                               NewFileName = s["documentname"].ToString(),
                                               //Filename = s["documentname"].ToString(),
                                               //createdBy = s["createdby"].ToString(),
                                               //description = s["description"].ToString(),
                                               DocumentType = s["name"].ToString(),
                                               Createddate = s["DateCreated"].ToString()
                                               //,UploadFilePath = s["NewFileName"].ToString()
                                               ,
                                               CreatedBy = s["createdby"].ToString()
                                           };

                    ObjbrokerRenwalModel.ListOfUploadedFiles = docsUploadedlist.ToList();
                }
                if (ds.Tables.Contains("OrgDetails"))
                {
                    if (selectedorg != "")
                    {
                        foreach (var item in selectedorg.Split(','))
                        {

                            foreach (DataRow dr in ds.Tables["OrgDetails"].Rows) // search whole table
                            {
                                if (dr["organizationid"].ToString() == item.ToString()) // if id==2
                                {
                                    dr["isselected"] = true; //change the name
                                                                //break; break or not depending on you
                                }
                            }
                        }
                    }
                    var OrgDetails = from s in ds.Tables["OrgDetails"].AsEnumerable()
                                           select new AssoicatedOrgList
                                           {

                                               //  }),  
                                               AssoicatedOrgId= HttpUtility.UrlEncode(CommonFunctions.CsUploadEncrypt(s["organizationid"].ToString())),
                                               AssoicatedOrgName = ObjbrokerRenwalModel.lang=="eng"?s["OrgEnglishname"].ToString(): s["organizationArabicName"].ToString(),
                                                isselected=Convert.ToString(s["isselected"]),
                                                 MainOrgId= HttpUtility.UrlEncode(CommonFunctions.CsUploadEncrypt(s["mainorganizationid"].ToString()))


                                           };
                            ObjbrokerRenwalModel.ListofAssocaitedORg = OrgDetails.ToList();
                    //    }
                  
              //  }
                }
                // return View();

            }

            catch (Exception ex)
            {
                WriteToLogFile("caught Exception in [HttpGet]public ActionResult BrokerUpdate()" +ex.Message+ ex.StackTrace.ToString(), "Serviceid="+Session["ServiceId"].ToString()+ "organizationid=" +Session["UserOrgID"].ToString() + "Tokenid="+Session["TokenId"].ToString());
            }
            return View("BrokerUpdate", ObjbrokerRenwalModel);
        }




        [HttpPost]
        public ActionResult BrokerUpdate(BrokerUpdateModel Bobj)

        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }

         
            Bobj.paidby = paidby;
            Bobj.PaymentTypeId = PaymentTypeId;
            Bobj.paidfor = paidfor;
            Bobj.PaidByType = "B";
            Bobj.urltoredirectforpayments = urltoredirectforpayments;
            Bobj.redirecturl = redirecturl;
            Bobj.Serviceid= Convert.ToInt32(Session["Serviceid"]);

            if((Convert.ToInt32(Session["Serviceid"])!=7)&&(Convert.ToInt32(Session["Serviceid"]) != 2) && (Convert.ToInt32(Session["Serviceid"]) != 17))
            { 
            Bobj.PassportExpirydate = Bobj.TempPassportExpirydate;
            Bobj.TradeLicenseExpiryDate = Bobj.TempTradeLicenseExpiryDate;
            Bobj.CivilIdExpirydate = Bobj.TempCivilIdExpirydate;
            }

            if(Bobj.SelectedOrgidForIssuance!=null)
            {
                string[] encrypteddata = Bobj.SelectedOrgidForIssuance.Split(',');
                string DecryptedId="";
                StringBuilder sb = new StringBuilder();
               foreach(var item in encrypteddata)
                {
                    DecryptedId = CommonFunctions.CsUploadDecrypt(HttpUtility.UrlDecode(item));

                
                    if (DecryptedId.All(char.IsDigit))
                    {
                        sb.Append(',' + DecryptedId);
                    }
                }

                Bobj.SelectedOrgidForIssuance = sb.ToString().TrimStart(',');
            }

            // 27-FEB
            //Bobj.Userid = Session["Userid"]

            Bobj.Userid = Session["MicroClearUser"].ToString();
            Bobj.Organizationid = Convert.ToInt32(Session["Orgid"]);
            Bobj.RequestNumber = Session["RequestNumber"].ToString();


            Bobj.BrokerArabicName = Session["MicroClearUsername"].ToString();

            Bobj.Eservicerequestid = Session["Eservicerequestid"].ToString();
            Bobj.Referenceprofile = Session["Referenceprofile"].ToString();

            HttpCookie langCookie = Request.Cookies["culture"];
            Bobj.lang = langCookie.Value == "en" ? "eng" : "ar"; //"eng";

            DataSet ds = objdataclass.BrokerDetailspost(Bobj);
            // sendfiles(ds.Tables[0]);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string s = "";
                if (ds.Tables["UpdateUserDetails"].Rows[0]["RedirectUrl"].ToString().Contains("CallPaymentGateWay"))

                        {

                    s = ds.Tables["UpdateUserDetails"].Rows[0]["RedirectUrl"].ToString();
                }
                else
                { 
                 s = ds.Tables["UpdateUserDetails"].Rows[0]["RedirectUrl"].ToString()+CommonFunctions.CsUploadEncrypt(Session["RequestNumber"].ToString());
                }

                string url = s;

          //      return JavaScript("<script>window.open = '" + s + "', '_blank';</script>");

                //     return Content("window.open('"+s+"')");
                //   return JavaScript(string.Format("window.open('{0}', '_blank', 'left=100,top=100,width=500,height=500,toolbar=no,resizable=no,scrollable=yes');", url));
                 return Redirect(s);
                //return View("BrokerUpdate", Bobj);
            }
            else
            {
                return View("BrokerUpdate", Bobj);
            }

        }
     





        private bool RenewalServiceAllowed()
        {
            if (Session["LegalEntity"] != null)
            {
                if (Session["LegalEntity"].ToString() == "1" || Session["LegalEntity"].ToString() == "2")
                {
                    return Convert.ToBoolean(Session["RenewalServiceAllowed"]);
                }
                else
                {
                    return false;// RedirectToAction("UnAuthorizedAction", "Registration");
                }
            }
            else
            {
                return false;
            }
        }


        [HttpGet]
        public ActionResult BrokerRenewal(string ServiceId = "2")
        {
            Session["ServiceId"] = ServiceId;


            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }
            if (!RenewalServiceAllowed() && (Session["isAdmin"].ToString() == "False" && Session["isSubAdmin"].ToString() == "False"))
                return RedirectToAction("ExamRequest", "ClearingAgentExamRequest");//("UnAuthorizedAction", "Registration");
            else if (!RenewalServiceAllowed())//&& Session["isAdmin"].ToString() != "True"
            {
                return RedirectToAction("BrokerServiceRequestUpdates");//, "BrokerRenewal");
            }

            if (Request.QueryString["TokenID"] != null)
            {
                Cf = new CommonFunctions();
                //   Userid = Request.QueryString["Userid"].ToString();
                //  Session["Userid"] = Userid.Replace("'", "");
                // BrokerNameLBl.Text=Userid.Replace("'", "");

                string returntokenid = Cf.Decrypt(System.Web.HttpUtility.UrlDecode(Request.QueryString["TokenID"].ToString()));

                //    FNCheckAuthetication(returntokenid);
            }

            // 27-FEB
            //Session["Userid"]= "ABDULLAH.BOARKI";
            Session["MicroClearUser"] = Session["UserOrgID"].ToString();// "ABDULLAH.BOARKI";
            return View("BrokerRenewal");


        }


        private void FNCheckAuthetication(string TokenID)
        {

            BrokerRenewalModel ObjbrokerRenwalModel = new BrokerRenewalModel();

            ObjbrokerRenwalModel.tokenid = TokenID;

            DataSet dataSet = objdataclass.FNCheckAutheticationForBrokerRenwal(ObjbrokerRenwalModel);

            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                // 27-FEB
                //Session["Userid"] = dataSet.Tables[0].Rows[0]["genbrokerUserid"].ToString();
                Session["MicroClearUser"] = dataSet.Tables[0].Rows[0]["genbrokerUserid"].ToString();


                //  BrokerNameLBl.Text = " " + Bd.brokername + " <a href='" + Bd.brokername + "'> </a> ";
            }


            else
            {
           
            }



        }

        public ActionResult DeleteFile(string dataItem)
        {
            //dataItem = HttpUtility.UrlDecode(dataItem);

           
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }

            //  Elog = new ErrorLogger();
            //   int InsertResult = 0;
            try
            {
                StringBuilder sb = new StringBuilder();
                string[] encrypteddata = dataItem.Split(',');
                ArrayList decryptedvalue = new ArrayList();
                foreach (var item in encrypteddata)
                {
                    //dataItem = Decrypt(item);
                    if (item.Contains('%'))
                    {

                        dataItem = CommonFunctions.CsUploadDecrypt(System.Web.HttpUtility.UrlDecode(item));
                    }
                    else
                    {
                        dataItem = CommonFunctions.CsUploadDecrypt(item);


                    }
                    if (dataItem.All(char.IsDigit))
                    {
                        sb.Append(dataItem);
                        // sb.Append(',' + dataItem);
                    }
                }
                //  dropdownvalue = CommonFunctions.CsUploadDecrypt(dropdownvalue);

                OrgReqResultDocInfoParams objdelete = new OrgReqResultDocInfoParams();

                // 27- FEB
                objdelete.mUserid = Session["MicroClearUser"].ToString();//Session["UserId"].ToString();
                objdelete.tokenId = Session["TokenId"].ToString();
                objdelete.sOrgReqDocId = sb.ToString();
                objdelete.EserviceRequestid = Session["EserviceRequestid"].ToString();

                string stratus = objdataclass.DocumentdeleteForEservice(objdelete);


                string s = sb.ToString();

            }
            catch (Exception ex)
            {
                WriteToLogFile(dataItem.ToString() + Session["MicroClearUser"].ToString() + Session["EserviceRequestid"].ToString(), " public ActionResult DeleteFile(string dataItem)");
            }

            //return new EmptyResult();
            return RedirectToAction("BrokerUpdate", "BrokerRenewal");
            //   return Json(new { success = "0", responseText = "Deleted Sucessfully!" }, JsonRequestBehavior.AllowGet);




        }
        


        public ActionResult DownloadFile(string fileName, string referenceprofile = "BRSERenewalDocs")
        {
            OpenDocumentParams objDownloadFile = new OpenDocumentParams();
            CheckSession();
            string id = "";
            string NewFileName = string.Empty;
            string MyfileName = string.Empty; string strpath = string.Empty;
            objDownloadFile.hiderefprofile = referenceprofile;//"BRSERenewalDocs";


            if (!fileName.All(char.IsDigit))
            {
                if (fileName.Contains('%'))
                {
                    id = CommonFunctions.CsUploadDecrypt(System.Web.HttpUtility.UrlDecode(fileName));
                }
                else
                {
                    id = CommonFunctions.CsUploadDecrypt(fileName);
                }
                //  objDownloadFile.hiderefprofile = RenewalRequestReferenceProfile;//"BRSERenewalDocs";

            }
            else
            {
                id = fileName;
                //   objDownloadFile.hiderefprofile = referenceprofile;
            }

            if (id != "")
            {
                try
                {


                    objDownloadFile.DocumentId = id;
                    if (Session["Eservicerequestid"] != null)
                    {
                        objDownloadFile.EserviceRequestid = Session["Eservicerequestid"].ToString();
                    }
                    DataSet ds = objdataclass.EserviceFileDownload(objDownloadFile);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        NewFileName = ds.Tables[0].Rows[0]["NewFileName"].ToString();
                        MyfileName = ds.Tables[0].Rows[0]["DocumentName"].ToString();
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        ShareFolderPath = ds.Tables[1].Rows[0]["SRDocumentsShareFolderPath"].ToString();
                        sSLUN = ds.Tables[1].Rows[0]["ShareLocationUserName"].ToString();
                        sSLD = ds.Tables[1].Rows[0]["ShareLocationDomain"].ToString();
                        sSLP = ds.Tables[1].Rows[0]["ShareLocationPassword"].ToString();
                        sSFP = ds.Tables[1].Rows[0]["SRDocumentsShareFolderPath"].ToString();
                    }


                    sPwd = CgServices1.DecryptData(sSLP);
                    ImpersonateUser iU = new ImpersonateUser();
                    SymmetricEncryption CgServices = ServiceFactory.GetSymmetricEncryptionInstance(); ;
                    // setConfigValues();
                    iU.Impersonate(sSLD, sSLUN, sPwd);
                    strpath = Path.Combine(ShareFolderPath, NewFileName);

                    // fro arabic handlers 
                    //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlPathEncode(MyfileName));

                    bool s = System.IO.File.Exists(strpath);


                    string filename = MyfileName;
                    string filepath = strpath;
                    byte[] filedata = System.IO.File.ReadAllBytes(filepath);
                    string contentType = MimeMapping.GetMimeMapping(filepath);

                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = filename,
                        Inline = false,
                    };

                    Response.AppendHeader("Content-Disposition", cd.ToString());

                    return File(filedata, contentType);


                }

                catch (Exception ex)
                {

                    String error = ex.ToString();
                    //  WriteToLogFile(ex);
                    WriteToLogFile("Error In downloading File"+ex.StackTrace.ToString() + ex.Message.ToString() + "filename" + fileName.ToString(), "new Id" + id.ToString());
                }

            }
            ViewBag.Errormessage = "Error In downloading File";
            return View("Error");



        }
        
        #region GetRequestDetails Ajax
        [WebMethod]
        public String GetRequestDetails(String nothing = "")
        {
            CheckSession();
            String requests = String.Empty;
            requests = GetRequestDetail(nothing);
            return requests;
        }

        ///[WebMethod]
        private String GetRequestDetail(String nothing = "")
        {
            // EServiceBrokerRenewal eServiceBrokerRenew = new EServiceBrokerRenewal();
            String requests = String.Empty;

            try
            {
                BrokerRenewalModel objbrm = new BrokerRenewalModel();
                // 27-FEB
                objbrm.Userid = Session["UserOrgID"].ToString(); //Session["MicroClearUser"].ToString();//Session["Userid"].ToString();

                DataSet dataSet = objdataclass.GetRequestDetaillist(objbrm);
                if (dataSet.Tables.Count != 0)
                {
                    DataTable dataTable = dataSet.Tables[0];

                    if (dataTable.Rows.Count > 0)
                    {
                        requests = ConvertJsonString(dataTable);
                    }
                }
                else
                {
                    requests = "{}";
                }
            }
            catch (Exception ex)
            {
                String error = ex.ToString();
                WriteToLogFile("private String GetRequestDetail" + ex.StackTrace.ToString() + ex.Message.ToString(), Session["UserOrgID"].ToString());
            }
            return requests;
        }

        #endregion GetRequestDetails Ajax

         
        #region GetBrokersDetails  Ajax
        //   [WebMethod]
        public String GetBrokersDetails(String nothing = "")
        {
            CheckSession();
            String brokers = String.Empty;

            brokers = GetBrokersDetail(nothing);

            return brokers;
        }

        public String GetBrokersDetail(String nothing = "")
        {
            String Brokers = String.Empty;
       //     String Brokers = String.Empty;

            try
            {
                BrokerRenewalModel objbrm = new BrokerRenewalModel();
                // 27-FEB
                objbrm.Orgid = Session["UserOrgID"].ToString();//Session["Userid"].ToString(); //Session["MicroClearUser"].ToString();//Session["Userid"].ToString();
                
                objbrm.Userid = Session["Userid"].ToString();
                objbrm.Requestorserviceid = Session["ServiceId"].ToString();

                DataSet dataSet = objdataclass.GetBrokersDetaillist(objbrm);
                if (dataSet.Tables.Count != 0)
                {
                    DataTable dataTable = dataSet.Tables[0];
                    if (dataTable.Rows.Count > 0)
                    {
                        Brokers = ConvertJsonString(dataTable);
                    }
                }
                else
                {
                    Brokers = "{}";
                }
            }
            catch (Exception ex)
            {
                WriteToLogFile("private String Get Broker details" + ex.StackTrace.ToString() + ex.Message.ToString(), Session["UserOrgID"].ToString());

                // String error = ex.ToString();
            }
            return Brokers;
        }
        #endregion GetBrokersDetails  Ajax



        #region Convert Json String
        private static String ConvertJsonString(DataTable dataTable)
        {


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            serializer.MaxJsonLength = Int32.MaxValue;

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;


         
            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    //    row.Add(col.ColumnName, MCEncrypt(dr[col].ToString()));
                    if ((col.ColumnName == "OrganizationId") || (col.ColumnName == "encryptedEServiceRequestNumber") || (col.ColumnName == "Serviceid"))
                    {
                        var Encryptedid = CommonFunctions.CsUploadEncrypt(dr[col].ToString());
                        var Encodedid = HttpUtility.UrlEncode(Encryptedid);

                        row.Add(col.ColumnName, Encodedid);
                    }
                    //else if ((col.ColumnName == "RequestsubmissionDateTime") || (col.ColumnName == "RequestCompletionDateTime"))
                    //{
                    //    var columnDate = DateTime.ParseExact(dr[col].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture); //null);

                    //    row.Add(col.ColumnName, columnDate.ToString());
                    //}

                    else
                    {

                        row.Add(col.ColumnName, dr[col].ToString());

                    }


                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        #endregion Convert Json String
        #endregion

     //   #region BrokerSubmissionRequestScreen

        [WebMethod]
        public String GetBrokerSubmissionRequests(String nothing = "")
        {
            CheckSession();
            String requests = String.Empty;
            requests = GetBrokerSubmissionRequest(nothing);
            return requests;

        }

        private String GetBrokerSubmissionRequest(String nothing = "")
        {
            // EServiceBrokerRenewal eServiceBrokerRenew = new EServiceBrokerRenewal();
            String requests = String.Empty;

            try
            {
                DataSet dataSet = new DataSet();
                //Session["Userid"] = "ABDULLAH.BOARKI";
                BrokerServiceRequestModel ObjBrokerServiceRequestModel = new BrokerServiceRequestModel();

                // 27-FEB
                ObjBrokerServiceRequestModel.userid = Session["Userid"].ToString(); //Session["MicroClearUser"].ToString();//Session["Userid"].ToString();
                ObjBrokerServiceRequestModel.CheckAvailableServicesforRequest = false;

                dataSet = objdataclass.FNGetEntityServiceList(ObjBrokerServiceRequestModel);

                //BrokerRenewalModel objbrm = new BrokerRenewalModel();
                //objbrm.Userid = Session["Userid"].ToString();

                //DataSet dataSet = objdataclass.GetRequestDetaillist(objbrm);
                if (dataSet.Tables.Count != 0)
                {
                    DataTable dataTable = dataSet.Tables[0];

                    if (dataTable.Rows.Count > 0)
                    {
                        requests = ConvertJsonString(dataTable);
                    }
                }
                else
                {
                    requests = "{}";
                }
            }
            catch (Exception ex)
            {
                WriteToLogFile("private String Get Broker Submission Request" + ex.StackTrace.ToString() + ex.Message.ToString(), Session["UserOrgID"].ToString());

            }
            return requests;
        }


        [WebMethod]
        public String GetListOfAvailableEservices(String nothing = "")
        {
            CheckSession();
            String requests = String.Empty;
            requests = GetListOfAvailableEservice(nothing);
            return requests;
        }

        private String GetListOfAvailableEservice(String nothing = "")
        {
            // EServiceBrokerRenewal eServiceBrokerRenew = new EServiceBrokerRenewal();
            String requests = String.Empty;

            try
            {
                DataSet dataSet = new DataSet();
                //Session["Userid"] = "ABDULLAH.BOARKI";
                BrokerServiceRequestModel ObjBrokerServiceRequestModel = new BrokerServiceRequestModel();

                // 27-FEB
                ObjBrokerServiceRequestModel.userid = Session["UserId"].ToString(); //"ABDULLAH.BOARKI";//Session["MicroClearUser"].ToString();//Session["Userid"].ToString();

                ObjBrokerServiceRequestModel.CheckAvailableServicesforRequest = true;
                dataSet = objdataclass.FNGetEntityServiceList(ObjBrokerServiceRequestModel);

                //BrokerRenewalModel objbrm = new BrokerRenewalModel();
                //objbrm.Userid = Session["Userid"].ToString();

                //DataSet dataSet = objdataclass.GetRequestDetaillist(objbrm);
                if (dataSet.Tables.Count != 0)
                {
                    DataTable dataTable = dataSet.Tables[0];

                    if (dataTable.Rows.Count > 0)
                    {
                        requests = ConvertJsonString(dataTable);
                    }
                }
                else
                {
                    requests = "{}";
                }
            }
            catch (Exception ex)
            {
                WriteToLogFile("private String Get GetListOfAvailableEservice" + ex.StackTrace.ToString() + ex.Message.ToString(), Session["UserOrgID"].ToString());

            }
            return requests;
        }
        

        [HttpGet]
        public ActionResult BrokerServiceRequestUpdates()
        {

            CheckSession();

            return View("BrokerServiceRequestUpdates");

        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult BrokerServiceRequestUpdatesPost(string SelectedFileId, string UnSelectedFileId)

  
        {
            StringBuilder sb;
            string[] encrypteddata;
            string[] encrypteddataForUnselectedId;
            string SelectedServiceids="";
            string UnSelectedServiceids="";
            BrokerServiceRequestModel ObjBrokerServiceRequestModel = new BrokerServiceRequestModel();

            try
            {
                if ((SelectedFileId != "" && UnSelectedFileId != "") || (SelectedFileId != null && UnSelectedFileId != null))

                {

                    sb = new StringBuilder();
                    encrypteddata = SelectedFileId.Split(',');
                    ArrayList decryptedvalue = new ArrayList();
                    string dataItem;
                    foreach (var item in encrypteddata)
                    {
                        //dataItem = Decrypt(item);

                        string s = System.Web.HttpUtility.UrlDecode(item);
                        dataItem = CommonFunctions.CsUploadDecrypt(s);
                        if (dataItem.All(char.IsDigit))
                        {
                            //  sb.Append(dataItem);
                            sb.Append(',' + dataItem);
                        }
                    }
                     SelectedServiceids = sb.ToString().TrimStart(',');

                    sb = new StringBuilder();
                    encrypteddataForUnselectedId = UnSelectedFileId.Split(',');
                    string dataItem1;
                    foreach (var item in encrypteddataForUnselectedId)
                    {
                        //dataItem = Decrypt(item);
                        string s = System.Web.HttpUtility.UrlDecode(item);
                        dataItem1 = CommonFunctions.CsUploadDecrypt(s);
                        if (dataItem1.All(char.IsDigit))
                        {
                            //  sb.Append(dataItem);
                            sb.Append(',' + dataItem1);
                        }
                    }
                     UnSelectedServiceids = sb.ToString().TrimStart(',');





                    ds = new DataSet();


                    ObjBrokerServiceRequestModel.userid = Session["UserId"].ToString();
                    ObjBrokerServiceRequestModel.MobileUserid = Convert.ToInt32(Session["UserId"].ToString());
                    ObjBrokerServiceRequestModel.RequestedForMobileUserid = Convert.ToInt32(Session["UserId"].ToString());
                    ObjBrokerServiceRequestModel.SelectedServiceids = SelectedServiceids;
                    ObjBrokerServiceRequestModel.UnSelectedServiceids = UnSelectedServiceids;
                    ds = objdataclass.FNPostEntityServiceList(ObjBrokerServiceRequestModel);



                }
           
        
            else
            {
           
                return RedirectToAction("BrokerServiceRequestUpdates");
            }

            }

            catch (Exception ex)
            {
                WriteToLogFile("private String brokerServiceRequestUpdatesPost" + ex.StackTrace.ToString() + ex.Message.ToString(), Session["UserId"].ToString()+"selectedServiceid"+SelectedServiceids+ "UnSelectedServiceids"+UnSelectedServiceids);

            }

            return Json(new { success = true, responseText = "posted" }, JsonRequestBehavior.AllowGet);

        }



    }

}
#endregion