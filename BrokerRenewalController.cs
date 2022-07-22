using System;
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

        public string urltoredirectforpayments = ConfigurationManager.AppSettings["siteUrl"].ToString();

        public string redirecturl = ConfigurationManager.AppSettings["redirecturl"].ToString();
        public string sSLUN = string.Empty;
        public string sSLD = string.Empty;
        public string sSLP = string.Empty;
        public string sSFP = string.Empty;
        public string ShareFolderPath = string.Empty;

        // 27-FEB
        String RenewalRequestReferenceProfile = ConfigurationManager.AppSettings["RenewalRequestReference"].ToString();
        String ServiceId = ConfigurationManager.AppSettings["RenewalService"].ToString();

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
        public ActionResult BrokerUpdate()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }

            // 27-FEB
            //Session["Userid"] = "ABDULLAH.BOARKI";
            //String MicroClearUser = "ABDULLAH.BOARKI";
            Session["MicroClearUser"] = Session["UserOrgID"].ToString();//  MicroClearUser;

            Session["Tokenid"] = "BR2Q78TMLH88FX0T4GHBYS5ZDPDT3CG";



            BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();
            ObjbrokerRenwalModel.Referenceprofile = RenewalRequestReferenceProfile;//"BRSERenewalDocs";
            ObjbrokerRenwalModel.mobileUserId = int.Parse(Session["UserId"].ToString());//"BRSERenewalDocs";

            HttpCookie langCookie = Request.Cookies["culture"];
            ObjbrokerRenwalModel.lang = langCookie.Value == "en" ? "eng" : "ar";//"eng";

            //will give service  type
            ObjbrokerRenwalModel.Serviceid = int.Parse(ServiceId); //2;
            int mobileUserId = int.Parse(Session["UserId"].ToString());

            //ObjbrokerRenwalModel.Referenceprofile=""
            if (Request.QueryString["Orgid"] != null)
            {
                string Orgid = CommonFunctions.CsUploadDecrypt(Request.QueryString["Orgid"].ToString());

                //   Session["Orgid"] = "346617";

                // 27-FEB
                //Session["Userid"] = "ABDULLAH.BOARKI";

                ObjbrokerRenwalModel.Organizationid = Convert.ToInt32(Orgid);
                ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();//Session["Userid"].ToString();
                //ObjbrokerRenwalModel.requestForMobileUserId = Convert.ToInt32(Orgid);

            }
            if (Request.QueryString["RequestNumber"] != null && Request.QueryString["Orgid"] == null)
            {
                string RequestNumber = CommonFunctions.CsUploadDecrypt(Request.QueryString["RequestNumber"].ToString());
                //   string RequestNumber = "RSR/2/19";
                if (RequestNumber.Contains("RSR"))
                {
                    ObjbrokerRenwalModel.RequestNumber = RequestNumber;
                    ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();//Session["Userid"].ToString();
                    Session["RequestNumber"] = RequestNumber;
                }

            }

            DataSet ds = objdataclass.FNGetBrokerDetailsForUpdate(ObjbrokerRenwalModel);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ObjbrokerRenwalModel.BrokerType = ds.Tables[0].Rows[0]["BrokerType"] != null ? ds.Tables[0].Rows[0]["BrokerType"].ToString() : "---";
                ObjbrokerRenwalModel.BrokerArabicName = ds.Tables[0].Rows[0]["BrokerNameAra"] != null ? ds.Tables[0].Rows[0]["BrokerNameAra"].ToString() : "---";
                ObjbrokerRenwalModel.ParentBrokerName = ds.Tables[0].Rows[0]["parentname"] != null ? ds.Tables[0].Rows[0]["parentname"].ToString() : "---";
                ObjbrokerRenwalModel.BrokerEnglishName = ds.Tables[0].Rows[0]["BrokerEnglishName"] != null ? ds.Tables[0].Rows[0]["BrokerEnglishName"].ToString() : "---";
                ObjbrokerRenwalModel.CivilIdNo = ds.Tables[0].Rows[0]["CivilId"] != null ? ds.Tables[0].Rows[0]["CivilId"].ToString() : "---";
                ObjbrokerRenwalModel.CivilIdExpirydate = checkNullOrEmpty(ds.Tables[0].Rows[0]["CivilIdExpiryDate"], 1);
                ObjbrokerRenwalModel.PassportExpirydate = checkNullOrEmpty(ds.Tables[0].Rows[0]["PassportExpiryDate"], 1);
                ObjbrokerRenwalModel.TradeLicenseExpiryDate = checkNullOrEmpty(ds.Tables[0].Rows[0]["TLExpiryDate"], 1);
                ObjbrokerRenwalModel.passportNo = ds.Tables[0].Rows[0]["PassportNo"] != null ? ds.Tables[0].Rows[0]["PassportNo"].ToString() : "---";
                ObjbrokerRenwalModel.MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"] != null ? ds.Tables[0].Rows[0]["MobileNumber"].ToString() : "---";
                ObjbrokerRenwalModel.MailAddress = ds.Tables[0].Rows[0]["EMail"] != null ? ds.Tables[0].Rows[0]["EMail"].ToString() : "---";
                ObjbrokerRenwalModel.officialAddress = ds.Tables[0].Rows[0]["Address"] != null ? ds.Tables[0].Rows[0]["Address"].ToString() : "---";

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
                // ObjbrokerRenwalModel.NewFileName
                ObjbrokerRenwalModel.RequestNumber = ds.Tables[1].Rows[0]["eservicerequestnumber"].ToString();
                Session["RequestNumber"] = ds.Tables[1].Rows[0]["eservicerequestnumber"].ToString();
                Session["Eservicerequestid"] = ds.Tables[1].Rows[0]["eservicerequestid"].ToString();
                ObjbrokerRenwalModel.Eservicerequestid = ds.Tables[1].Rows[0]["eservicerequestid"].ToString();

            }
            if (ds != null && ds.Tables[2].Rows.Count > 0)

            {
                var DropdownBind = from s in ds.Tables[2].AsEnumerable()
                                   select new SelectListItem
                                   {
                                       Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
                                       Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
                                       // Value = s["DeclarationDocumentId"].ToString(),

                                       //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)

                                   };
                ObjbrokerRenwalModel.ddlDocumentTypesitems = DropdownBind.ToList();

            }
            else
            {
            }
            if (ds.Tables.Contains("EServiceUploadedFiles"))
            {
                var docsUploadedlist = from s in ds.Tables[3].AsEnumerable()
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
            // return View();
            return View("BrokerUpdate", ObjbrokerRenwalModel);
        }


        //public ActionResult BrokerUpdate()
        //{

        //    CheckSession();

        //    // 27-FEB
        //    //Session["Userid"] = "ABDULLAH.BOARKI";
        //    String MicroClearUser  = "ABDULLAH.BOARKI";
        //    Session["MicroClearUser"] = MicroClearUser;

        //    Session["Tokenid"] = "BR2Q78TMLH88FX0T4GHBYS5ZDPDT3CG";



        //    BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();
        //    ObjbrokerRenwalModel.Referenceprofile = RenewalRequestReferenceProfile;//"BRSERenewalDocs";

        //    HttpCookie langCookie = Request.Cookies["culture"];
        //    ObjbrokerRenwalModel.lang = langCookie.Value == "en" ? "eng" : "ar" ;//"eng";

        //    //will give service  type
        //    ObjbrokerRenwalModel.Serviceid = int.Parse(ServiceId); //2;
        //    int mobileUserId = int.Parse(Session["UserId"].ToString());

        //    //ObjbrokerRenwalModel.Referenceprofile=""
        //    if (Request.QueryString["Orgid"] != null)
        //    {
        //        string Orgid = CommonFunctions.CsUploadDecrypt(Request.QueryString["Orgid"].ToString());

        //        //   Session["Orgid"] = "346617";

        //         // 27-FEB
        //        //Session["Userid"] = "ABDULLAH.BOARKI";

        //        ObjbrokerRenwalModel.Organizationid = Convert.ToInt32(Orgid);
        //        ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();//Session["Userid"].ToString();
        //        ObjbrokerRenwalModel.requestForMobileUserId = Convert.ToInt32(Orgid);

        //    }
        //    if (Request.QueryString["RequestNumber"] != null && Request.QueryString["Orgid"] == null)
        //    {
        //        string RequestNumber = CommonFunctions.CsUploadDecrypt(Request.QueryString["RequestNumber"].ToString());
        //        //   string RequestNumber = "RSR/2/19";
        //        if (RequestNumber.Contains("RSR"))
        //        {
        //            ObjbrokerRenwalModel.RequestNumber = RequestNumber;
        //            ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();//Session["Userid"].ToString();
        //            Session["RequestNumber"] = RequestNumber;
        //        }

        //    }

        //    DataSet ds = objdataclass.FNGetBrokerDetailsForUpdate(ObjbrokerRenwalModel);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        ObjbrokerRenwalModel.BrokerType = ds.Tables[0].Rows[0]["BrokerType"] != null ? ds.Tables[0].Rows[0]["BrokerType"].ToString() : "---";
        //        ObjbrokerRenwalModel.BrokerArabicName = ds.Tables[0].Rows[0]["BrokerNameAra"] != null ? ds.Tables[0].Rows[0]["BrokerNameAra"].ToString() : "---";
        //        ObjbrokerRenwalModel.ParentBrokerName = ds.Tables[0].Rows[0]["parentname"] != null ? ds.Tables[0].Rows[0]["parentname"].ToString() : "---";
        //        ObjbrokerRenwalModel.BrokerEnglishName = ds.Tables[0].Rows[0]["BrokerEnglishName"] != null ? ds.Tables[0].Rows[0]["BrokerEnglishName"].ToString() : "---";
        //        ObjbrokerRenwalModel.CivilIdNo = ds.Tables[0].Rows[0]["CivilId"] != null ? ds.Tables[0].Rows[0]["CivilId"].ToString() : "---";
        //        ObjbrokerRenwalModel.CivilIdExpirydate = checkNullOrEmpty(ds.Tables[0].Rows[0]["CivilIdExpiryDate"], 1);
        //        ObjbrokerRenwalModel.PassportExpirydate = checkNullOrEmpty(ds.Tables[0].Rows[0]["PassportExpiryDate"], 1);
        //        ObjbrokerRenwalModel.TradeLicenseExpiryDate = checkNullOrEmpty(ds.Tables[0].Rows[0]["TLExpiryDate"], 1);
        //        ObjbrokerRenwalModel.passportNo = ds.Tables[0].Rows[0]["PassportNo"] != null ? ds.Tables[0].Rows[0]["PassportNo"].ToString() : "---";
        //        ObjbrokerRenwalModel.MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"] != null ? ds.Tables[0].Rows[0]["MobileNumber"].ToString() : "---";
        //        ObjbrokerRenwalModel.MailAddress = ds.Tables[0].Rows[0]["EMail"] != null ? ds.Tables[0].Rows[0]["EMail"].ToString() : "---";
        //        ObjbrokerRenwalModel.officialAddress = ds.Tables[0].Rows[0]["Address"] != null ? ds.Tables[0].Rows[0]["Address"].ToString() : "---";

        //    }
        //    if (ds != null && ds.Tables[1].Rows.Count > 0)
        //    {
        //        //  eservicerequestnumber EServiceRequestPaidState
        //        if (ds.Tables[1].Rows[0]["stateid"].ToString() == "EServiceRequestProceedState" || ds.Tables[1].Rows[0]["stateid"].ToString() == "EServiceRequestCreatedState" || ds.Tables[1].Rows[0]["stateid"].ToString() == "EServiceRequestRejectedState")
        //        {

        //        }
        //        else
        //        {
        //            ObjbrokerRenwalModel.SubmitRequest = "false";
        //            ObjbrokerRenwalModel.UploadDiv = "false";

        //        }
        //        // ObjbrokerRenwalModel.NewFileName
        //        ObjbrokerRenwalModel.RequestNumber = ds.Tables[1].Rows[0]["eservicerequestnumber"].ToString();
        //        Session["RequestNumber"] = ds.Tables[1].Rows[0]["eservicerequestnumber"].ToString();
        //        Session["Eservicerequestid"] = ds.Tables[1].Rows[0]["eservicerequestid"].ToString();
        //        ObjbrokerRenwalModel.Eservicerequestid = ds.Tables[1].Rows[0]["eservicerequestid"].ToString();

        //    }
        //    if (ds != null && ds.Tables[2].Rows.Count > 0)

        //    {
        //        var DropdownBind = from s in ds.Tables[2].AsEnumerable()
        //                           select new SelectListItem
        //                           {
        //                               Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
        //                               Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
        //                               // Value = s["DeclarationDocumentId"].ToString(),

        //                               //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)

        //                           };
        //        ObjbrokerRenwalModel.ddlDocumentTypesitems = DropdownBind.ToList();

        //    }
        //    else
        //    {
        //    }
        //    if (ds.Tables.Contains("EServiceUploadedFiles"))
        //    {
        //        var docsUploadedlist = from s in ds.Tables[3].AsEnumerable()
        //                               select new BrFileResult
        //                               {

        //                                   //  }),
        //                                   DocumentId = CommonFunctions.CsUploadEncrypt(s["Documentid"].ToString()),
        //                                   NewFileName = s["documentname"].ToString(),
        //                                   //Filename = s["documentname"].ToString(),
        //                                   //createdBy = s["createdby"].ToString(),
        //                                   //description = s["description"].ToString(),
        //                                   DocumentType = s["name"].ToString(),
        //                                   Createddate = s["DateCreated"].ToString()
        //                                   //,UploadFilePath = s["NewFileName"].ToString()
        //                                   ,
        //                                   CreatedBy = s["createdby"].ToString()
        //                               };

        //        ObjbrokerRenwalModel.ListOfUploadedFiles = docsUploadedlist.ToList();
        //    }
        //    // return View();
        //    return View("BrokerUpdate", ObjbrokerRenwalModel);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
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

            // 27-FEB
            //Bobj.Userid = Session["Userid"]
            Bobj.Userid = Session["MicroClearUser"].ToString();
            Bobj.Organizationid = Convert.ToInt32(Session["Orgid"]);
            Bobj.RequestNumber = Session["RequestNumber"].ToString();
            Bobj.Eservicerequestid = Session["Eservicerequestid"].ToString();

            HttpCookie langCookie = Request.Cookies["culture"];
            Bobj.lang = langCookie.Value == "en" ? "eng" : "ar"; //"eng";

            DataSet ds = objdataclass.BrokerDetailspost(Bobj);
            // sendfiles(ds.Tables[0]);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string s = ds.Tables["UpdateUserDetails"].Rows[0]["RedirectUrl"].ToString();
                return Redirect(s);
                //return View("BrokerUpdate", Bobj);
            }
            else
            {
                return View("BrokerUpdate", Bobj);
            }
        }

        //public ActionResult BrokerUpdate(BrokerUpdateModel Bobj)
        //{
        //    CheckSession();

        //    Bobj.paidby = paidby;
        //    Bobj.PaymentTypeId = PaymentTypeId;
        //    Bobj.paidfor = paidfor;
        //    Bobj.PaidByType = "B";
        //    Bobj.urltoredirectforpayments = urltoredirectforpayments;
        //    Bobj.redirecturl = redirecturl;

        //    // 27-FEB
        //    //Bobj.Userid = Session["Userid"]
        //    Bobj.Userid = Session["MicroClearUser"].ToString();
        //    Bobj.Organizationid = Convert.ToInt32(Session["Orgid"]);
        //    Bobj.RequestNumber = Session["RequestNumber"].ToString();
        //    Bobj.Eservicerequestid = Session["Eservicerequestid"].ToString();

        //    HttpCookie langCookie = Request.Cookies["culture"];
        //    Bobj.lang = langCookie.Value == "en" ? "eng" : "ar"; //"eng";

        //    DataSet ds = objdataclass.BrokerDetailspost(Bobj);
        //    // sendfiles(ds.Tables[0]);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        string s = ds.Tables["UpdateUserDetails"].Rows[0]["RedirectUrl"].ToString();
        //        //return Redirect(s);
        //        return View("BrokerUpdate", Bobj);
        //    }
        //    else
        //    {
        //        return View("BrokerUpdate", Bobj);
        //    }
        //}

        [HttpGet]
        public ActionResult BrokerRenewal()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
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
        //public ActionResult BrokerRenewal()
        //{
        //    CheckSession();

        //    if (Request.QueryString["TokenID"] != null)
        //    {
        //        Cf = new CommonFunctions();
        //        //   Userid = Request.QueryString["Userid"].ToString();
        //        //  Session["Userid"] = Userid.Replace("'", "");
        //        // BrokerNameLBl.Text=Userid.Replace("'", "");

        //        string returntokenid = Cf.Decrypt(System.Web.HttpUtility.UrlDecode(Request.QueryString["TokenID"].ToString()));

        //        //    FNCheckAuthetication(returntokenid);
        //    }

        //    // 27-FEB
        //    //Session["Userid"]= "ABDULLAH.BOARKI";
        //    Session["MicroClearUser"] = "ABDULLAH.BOARKI";
        //    return View("BrokerRenewal");

        //}

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
                //    Elog = new ErrorLogger();
                //    Elog.WriteToLogFile("TokenID=" + TokenID.ToString(), "public void FNCheckAuthetication(string TokenID)");

                //    // WriteToLogFile("No data Found For usp_GetUploadedDocumentsInfo Parametere used '" + Language + "''" + TablePrimaryKey + "' '" + ProfileName + "''" + ReferenceType + "'documentid>>'" + D + "'maqassaDocumnettype>>'" + m + "','" + sProfileReferenceId + "''" + UploadedFrom + "'");
                //    //          Elog.WriteToLogFile(" From Csupload No data Found For usp_GetUploadedDocumentsInfo Parametere used '" + Language + "''" + TablePrimaryKey + "' '" + ProfileName + "''" + ReferenceType + "'documentid>>'" + D + "'maqassaDocumnettype>>'" + m + "','" + sProfileReferenceId + "''" + UploadedFrom + "'", " and   Param Information => (referredUrl = '" + Session["referredUrl"].ToString() + "')    and tokenValue = '" + Session["mytokenvalue"].ToString() + "' and sessionID = '" + Session["mysessionId"].ToString() + "'and tokensalt = '" + Tokensalt + "'");
                //    Elog = null;

                //    //   WriteToLogFile("No data Found For usp_GetUploadedDocumentsInfo Parametere used ");
                //}

                // TempData["Fmodel"] = F;
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
            StringBuilder sb = new StringBuilder();
            string[] encrypteddata = dataItem.Split(',');
            ArrayList decryptedvalue = new ArrayList();
            foreach (var item in encrypteddata)
            {
                //dataItem = Decrypt(item);

                dataItem = CommonFunctions.CsUploadDecrypt(System.Web.HttpUtility.UrlDecode(item));
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



            //return new EmptyResult();
            //return RedirectToAction("BrokerUpdate", "BrokerRenewal");
            return Json(new { success = "0", responseText = "Deleted Sucessfully!" }, JsonRequestBehavior.AllowGet);




        }

        //public ActionResult DeleteFile(string dataItem)
        //{
        //    CheckSession();

        //    //  Elog = new ErrorLogger();
        //    //   int InsertResult = 0;
        //    StringBuilder sb = new StringBuilder();
        //    string[] encrypteddata = dataItem.Split(',');
        //    ArrayList decryptedvalue = new ArrayList();
        //    foreach (var item in encrypteddata)
        //    {
        //        //dataItem = Decrypt(item);

        //        dataItem = CommonFunctions.CsUploadDecrypt(item);
        //        if (dataItem.All(char.IsDigit))
        //        {
        //            sb.Append(dataItem);
        //            // sb.Append(',' + dataItem);
        //        }
        //    }
        //    //  dropdownvalue = CommonFunctions.CsUploadDecrypt(dropdownvalue);

        //    OrgReqResultDocInfoParams objdelete = new OrgReqResultDocInfoParams();

        //    // 27- FEB
        //    objdelete.mUserid = Session["MicroClearUser"].ToString();//Session["UserId"].ToString();
        //    objdelete.tokenId = Session["TokenId"].ToString();
        //    objdelete.sOrgReqDocId = sb.ToString();
        //    objdelete.EserviceRequestid = Session["EserviceRequestid"].ToString();

        //    string stratus = objdataclass.DocumentdeleteForEservice(objdelete);


        //    string s = sb.ToString();


        //    return new EmptyResult();
        //    //return Json(new { success = true, responseText = "Deleted Sucessfully!" }, JsonRequestBehavior.AllowGet);




        //}


        public ActionResult DownloadFile(string fileName)
        {

            CheckSession();

            string NewFileName = string.Empty;
            string MyfileName = string.Empty; string strpath = string.Empty;
            // string id = CommonFunctions.CsUploadDecrypt(fileName);
            string id = CommonFunctions.CsUploadDecrypt(System.Web.HttpUtility.UrlDecode(fileName));
            if (id != "")
            {
                try
                {

                    OpenDocumentParams objDownloadFile = new OpenDocumentParams();
                    objDownloadFile.DocumentId = id;
                    objDownloadFile.EserviceRequestid = Session["Eservicerequestid"].ToString();
                    objDownloadFile.hiderefprofile = RenewalRequestReferenceProfile;//"BRSERenewalDocs";
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

                }

            }
            ViewBag.Errormessage = "Error In downloading File";
            return View("Error");



        }

        #region documentsArchieve

        //public JsonResult MoveToArchive(string dataItem, string dropdownvalue)
        //{
        //    //  setSessionRealtedValues();
        //    //     setConfigValues();
        //    ds = new DataSet();
        //   // DataSet ds = objdataclass.EserviceFileDownload(objDownloadFile);


        //    if (ds.Tables[1].Rows.Count > 0)
        //    {
        //        ShareFolderPath = ds.Tables[1].Rows[0]["SRDocumentsShareFolderPath"].ToString();
        //        sSLUN = ds.Tables[1].Rows[0]["ShareLocationUserName"].ToString();
        //        sSLD = ds.Tables[1].Rows[0]["ShareLocationDomain"].ToString();
        //        sSLP = ds.Tables[1].Rows[0]["ShareLocationPassword"].ToString();
        //        sSFP = ds.Tables[1].Rows[0]["SRDocumentsShareFolderPath"].ToString();
        //    }
        //    sPwd = CgServices1.DecryptData(sSLP);
        //    ImpersonateUser iU = new ImpersonateUser();
        //    SymmetricEncryption CgServices = ServiceFactory.GetSymmetricEncryptionInstance(); ;
        //    // setConfigValues();
        //    iU.Impersonate(sSLD, sSLUN, sPwd);
        //   // int InsertResult = 0;
        //    StringBuilder sb = new StringBuilder();
        //    string[] encrypteddata = dataItem.Split(',');
        //    ArrayList decryptedvalue = new ArrayList();
        //    foreach (var item in encrypteddata)
        //    {
        //        dataItem = CommonFunctions.CsUploadDecrypt(item);
        //        if (dataItem.All(char.IsDigit))
        //        {
        //            sb.Append(',' + dataItem);
        //        }
        //    }


        //    //setSessionRealtedValues();

        //    try
        //    {
        //        dataItem = sb.ToString().Remove(0, 1);

        //        SqlConnection cn = new SqlConnection(strConnString);
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cn;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cn.Open();

        //        SqlParameter[] SqlParameters = new SqlParameter[2];

        //        SqlParameters[0] = new SqlParameter();
        //        SqlParameters[0].ParameterName = "@DocumentId";
        //        SqlParameters[0].Value = dataItem;


        //        string MovedDocumentIds = "";// dataItem;
        //        string MovingFailedIds = "";


        //        //SqlParameters[0].Value = MovedDocumentIds;
        //        SqlParameters[1] = new SqlParameter();
        //        SqlParameters[1].ParameterName = "@Result";
        //        SqlParameters[1].Direction = ParameterDirection.Output;
        //        SqlParameters[1].Value = false;


        //        cmd.CommandText = "sp_ConsigneeArcDocs";
        //        cmd.Parameters.Add(SqlParameters[0]);
        //        cmd.Parameters.Add(SqlParameters[1]);
        //        SqlDataAdapter sdaNewPath = new SqlDataAdapter(cmd);

        //        DataSet dsFP = new DataSet();
        //        sdaNewPath.Fill(dsFP);

        //        //InsertResult = cmd.ExecuteNonQuery();
        //        bool OkToMove = Convert.ToBoolean(cmd.Parameters["@Result"].Value.ToString());

        //        cn.Close();

        //        if (OkToMove)
        //        {
        //            MovedDocumentIds = "";
        //            foreach (DataRow dr in dsFP.Tables[0].Rows)
        //            {
        //                string UploadedFile = ShareFolderPath + @"\" + dr[2].ToString();
        //                FileInfo file = new FileInfo(UploadedFile);
        //                //string ArchivePath = @"ConsigneeDoc\" + dr[3].ToString() + @"\" + dr[0].ToString();
        //                string ArchiveFilePath = dr[3].ToString();
        //                string FullDestFilePath = ShareFolderPath + @"\" + ArchiveFilePath;
        //                if (MoveFile(file, FullDestFilePath))
        //                {
        //                    //MovedDocumentIds = MovedDocumentIds + dr[0].ToString() + ",";
        //                    MovedDocumentIds = dr[1].ToString();
        //                    //UpdateNewArchiveFilePath(dr[0].ToString(), ArchivePath + @"\" + file.Name);
        //                    UpdateNewArchiveFilePath(MovedDocumentIds, ArchiveFilePath);
        //                }
        //                else
        //                {
        //                    //Log error incase of file system issue , and path not updated in main table , doc viewable in main table
        //                    //But future link is affected - resolved - Doc remains not active in archive table if moving failed
        //                    MovingFailedIds = MovingFailedIds + dr[0].ToString() + ",";
        //                }
        //            }
        //        }


        //        var ArchiveStatus = new { Status = OkToMove ? "Success" : "Failed" };

        //        return Json(ArchiveStatus);

        //    }
        //    catch (Exception ex)
        //    {
        //        Elog.WriteToLogFile(ex, dataItem, "from Csupload MoveToArchive Param Information => (referredUrl = '" + Session["referredUrl"].ToString() + "')    and tokenValue = '" + Session["mytokenvalue"].ToString() + "' and sessionID = '" + Session["mysessionId"].ToString() + "'and tokensalt = '" + Tokensalt + "'");
        //        // WriteToLogFile(ex, "public ActionResult DeleteFile");

        //        var ArchiveStatus = new { Status = "Error" };

        //        return Json(ArchiveStatus);
        //    }



        //}


        #endregion


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
                objbrm.Userid = Session["Userid"].ToString(); //Session["MicroClearUser"].ToString();//Session["Userid"].ToString();

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
            try
            {
                BrokerRenewalModel objbrm = new BrokerRenewalModel();
                // 27-FEB
                objbrm.Userid = Session["UserOrgID"].ToString();//Session["Userid"].ToString(); //Session["MicroClearUser"].ToString();//Session["Userid"].ToString();

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

                String error = ex.ToString();
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

        #region BrokerSubmissionRequestScreen

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
                String error = ex.ToString();
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
                String error = ex.ToString();
            }
            return requests;
        }


        



        [HttpGet]
        public ActionResult BrokerServiceRequestUpdates()
        {

            CheckSession();
            //if (Request.QueryString["TokenID"] != null)
            //{
            //    Cf = new CommonFunctions();
            //    //   Userid = Request.QueryString["Userid"].ToString();
            //    //  Session["Userid"] = Userid.Replace("'", "");
            //    // BrokerNameLBl.Text=Userid.Replace("'", "");

            //    string returntokenid = Cf.Decrypt(System.Web.HttpUtility.UrlDecode(Request.QueryString["TokenID"].ToString()));

            //    //    FNCheckAuthetication(returntokenid);
            //}
            //Session["Userid"] = "ABDULLAH.BOARKI";
            return View("BrokerServiceRequestUpdates");

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult BrokerServiceRequestUpdatesPost(string SelectedFileId, string UnSelectedFileId)

        //      public ActionResult test(FormCollection form)
        {
            StringBuilder sb;
            string[] encrypteddata;
            string[] encrypteddataForUnselectedId;
            BrokerServiceRequestModel ObjBrokerServiceRequestModel = new BrokerServiceRequestModel();
            //string SelectedFileId = dataItem;
            //string UnSelectedFileId = dropdownvalue;
            //  if ((SelectedFileId != "" && UnSelectedFileId != "") || (SelectedFileId!=null && UnSelectedFileId != null))
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
                string SelectedServiceids = sb.ToString().TrimStart(',');

                sb = new StringBuilder();
                encrypteddataForUnselectedId = UnSelectedFileId.Split(',');
                string dataItem1;
                foreach (var item in encrypteddataForUnselectedId)
                {
                    //dataItem = Decrypt(item);

                    dataItem1 = CommonFunctions.CsUploadDecrypt(item);
                    if (dataItem1.All(char.IsDigit))
                    {
                        //  sb.Append(dataItem);
                        sb.Append(',' + dataItem1);
                    }
                }
                string UnSelectedServiceids = sb.ToString().TrimStart(',');

                //string ss = form["ServiceCollection"];

               

                ds = new DataSet();
               // Session["Userid"] = "ABDULLAH.BOARKI";

                ObjBrokerServiceRequestModel.userid = Session["UserId"].ToString();
                ObjBrokerServiceRequestModel.MobileUserid = Convert.ToInt32(Session["UserId"].ToString());
                ObjBrokerServiceRequestModel.RequestedForMobileUserid = Convert.ToInt32(Session["UserId"].ToString());
                ObjBrokerServiceRequestModel.SelectedServiceids = SelectedServiceids;
                ObjBrokerServiceRequestModel.UnSelectedServiceids = UnSelectedServiceids;
                ds = objdataclass.FNPostEntityServiceList(ObjBrokerServiceRequestModel);

                //int UserIdtoGetServices = int.Parse(Session["UserId"].ToString());
                //ReqObj serviceSubscription = new ReqObj { ParentID = UserIdtoGetServices, ChildUser = 0 };

                //DataTable dataTable = objdataclass.GETParentUserActiveServices(serviceSubscription);

                //if (Session["serviceSubscriptionDataTable"] != null)
                //{
                //    Session.Remove("serviceSubscriptionDataTable");
                //}

                //Session.Add("serviceSubscriptionDataTable", dataTable);

            }

            else
            {
                // RedirectToAction("BrokerServiceRequest");
                return RedirectToAction("BrokerServiceRequestUpdates");
            }
            //  return View("BrokerServiceRequestUpdates", ObjBrokerServiceRequestModel);
            //  return RedirectToAction("BrokerServiceRequestUpdates");
            

            return Json(new { success = true, responseText = "posted" }, JsonRequestBehavior.AllowGet);

        }




          #endregion

       
        

    }
}



