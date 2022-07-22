using DocumentManagementServices;
using MicroClear.EnterpriseSolutions.CryptographyServices;
using MicroClear.EnterpriseSolutions.ServiceFactories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class PrintingServicesController : MyBaseController
    {
        // GET: PrintingServices
        public ActionResult Index()
        {
            CheckSession();
            return View();
        }


        #region service Form

        [HttpGet]
        public ActionResult PrintFormRequest()
        {
            //return View();

            String selectedorg = "";
            String ServiceId;

            BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }


            try
            {
                Dataclass objdataclass = new Dataclass();

                Session["MicroClearUser"] = Session["UserOrgID"].ToString();


                ViewBag.UserId = Session["UserId"].ToString();
                ViewBag.TokenId = Session["TokenId"].ToString();
                ObjbrokerRenwalModel.mobileUserId = int.Parse(Session["UserId"].ToString());


                // by azhar 

                ObjbrokerRenwalModel.tokenId = Session["TokenId"].ToString();

                ObjbrokerRenwalModel.Userid = Session["UserId"].ToString();

                HttpCookie langCookie = Request.Cookies["culture"];
                ObjbrokerRenwalModel.lang = langCookie.Value == "en" ? "eng" : "ar";




                if (Request.QueryString["Orgid"] != null)
                {
                    String Orgid = CommonFunctions.CsUploadDecrypt(Request.QueryString["Orgid"].ToString());


                    if (String.IsNullOrWhiteSpace(Orgid))
                    {

                        String source = Request.RawUrl.ToString();
                        String split = "Orgid=";


                        String result = source.Substring(source.IndexOf(split) + split.Length);


                        Orgid = CommonFunctions.CsUploadDecrypt(result);

                        //              var s = Session["ServiceId"] != null ? CommonFunctions.LogUserActivity(
                        //"NewRequestCreated", "", "", "", Session["ServiceId"].ToString(), "OrganizationID+'" + Orgid + "'") : CommonFunctions.LogUserActivity(
                        //"NewRequestCreated", "", "", "", "", "OrganizationID+'" + Orgid + "'");
                    }

                    Session["Orgid"] = Orgid;
                    ObjbrokerRenwalModel.Organizationid = Convert.ToInt32(Orgid);
                    ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();
                }

                String RequestNumber = "";

                if (Request.QueryString["RequestNumber"] != null && Request.QueryString["Orgid"] == null)
                {
                    RequestNumber = CommonFunctions.CsUploadDecrypt(Request.QueryString["RequestNumber"].ToString());
                    if (RequestNumber == "")
                    {

                        String source = Request.RawUrl.ToString();
                        String split = "RequestNumber=";
                        String result = source.Substring(source.IndexOf(split) + split.Length);
                        RequestNumber = CommonFunctions.CsUploadDecrypt(result);
                    }

                    {
                        ObjbrokerRenwalModel.RequestNumber = RequestNumber;
                        ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();
                        Session["RequestNumber"] = RequestNumber;
                    }

                }
                if (Session["Orgid"] != null)
                {

                    ObjbrokerRenwalModel.Organizationid = Convert.ToInt32(Session["Orgid"]);
                    ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();
                }
                else if (Session["UserOrgID"] != null)
                {
                    ObjbrokerRenwalModel.Organizationid = Convert.ToInt32(Session["UserOrgID"]);
                    ObjbrokerRenwalModel.Userid = Session["MicroClearUser"].ToString();
                }


                AssignServiceIdtoSession(RequestNumber);


                if (Session["ServiceId"] != null)
                {
                    ServiceId = Session["ServiceId"].ToString();
                    ObjbrokerRenwalModel.Referenceprofile = ConfigurationManager.AppSettings[ServiceId].ToString();
                    Session["Referenceprofile"] = ObjbrokerRenwalModel.Referenceprofile;
                    ObjbrokerRenwalModel.Serviceid = int.Parse(Session["ServiceId"].ToString());
                }

                DataSet dataSet = objdataclass.FNGetBrokerDetailsForUpdate(ObjbrokerRenwalModel);
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        ObjbrokerRenwalModel.BrokerType = dataSet.Tables[0].Rows[0]["BrokerType"] != null ? dataSet.Tables[0].Rows[0]["BrokerType"].ToString() : "---";
                        ObjbrokerRenwalModel.BrokerArabicName = dataSet.Tables[0].Rows[0]["BrokerNameAra"] != null ? dataSet.Tables[0].Rows[0]["BrokerNameAra"].ToString() : "---";
                        ObjbrokerRenwalModel.ParentBrokerName = dataSet.Tables[0].Rows[0]["parentname"] != null ? dataSet.Tables[0].Rows[0]["parentname"].ToString() : "---";
                        ObjbrokerRenwalModel.BrokerEnglishName = dataSet.Tables[0].Rows[0]["BrokerEnglishName"] != null ? dataSet.Tables[0].Rows[0]["BrokerEnglishName"].ToString() : "---";
                        ObjbrokerRenwalModel.CivilIdNo = dataSet.Tables[0].Rows[0]["CivilId"] != null ? dataSet.Tables[0].Rows[0]["CivilId"].ToString() : "---";
                        // ObjbrokerRenwalModel.CivilIdExpirydate = checkNullOrEmpty(ds.Tables[0].Rows[0]["CivilIdExpiryDate"], 1);
                        // ObjbrokerRenwalModel.PassportExpirydate = checkNullOrEmpty(ds.Tables[0].Rows[0]["PassportExpiryDate"], 1);
                        // ObjbrokerRenwalModel.TradeLicenseExpiryDate = checkNullOrEmpty(ds.Tables[0].Rows[0]["TLExpiryDate"], 1);
                        ObjbrokerRenwalModel.CivilIdExpirydate = dataSet.Tables[0].Rows[0]["CivilIdExpiryDate"].ToString();
                        ObjbrokerRenwalModel.PassportExpirydate = dataSet.Tables[0].Rows[0]["PassportExpiryDate"].ToString();
                        ObjbrokerRenwalModel.TradeLicenseExpiryDate = dataSet.Tables[0].Rows[0]["TLExpiryDate"].ToString();

                        ObjbrokerRenwalModel.TempCivilIdExpirydate = ObjbrokerRenwalModel.CivilIdExpirydate.ToString();
                        ObjbrokerRenwalModel.TempPassportExpirydate = ObjbrokerRenwalModel.PassportExpirydate.ToString();

                        ObjbrokerRenwalModel.TempTradeLicenseExpiryDate = ObjbrokerRenwalModel.TradeLicenseExpiryDate.ToString();

                        ObjbrokerRenwalModel.passportNo = dataSet.Tables[0].Rows[0]["PassportNo"] != null ? dataSet.Tables[0].Rows[0]["PassportNo"].ToString() : "---";
                        ObjbrokerRenwalModel.MobileNumber = dataSet.Tables[0].Rows[0]["MobileNumber"] != null ? dataSet.Tables[0].Rows[0]["MobileNumber"].ToString() : "---";
                        ObjbrokerRenwalModel.MailAddress = dataSet.Tables[0].Rows[0]["EMail"] != null ? dataSet.Tables[0].Rows[0]["EMail"].ToString() : "---";
                        ObjbrokerRenwalModel.officialAddress = dataSet.Tables[0].Rows[0]["Address"] != null ? dataSet.Tables[0].Rows[0]["Address"].ToString() : "---";


                        ObjbrokerRenwalModel.CommercialLicenseNumber = dataSet.Tables[0].Rows[0]["CommercialLicenseNo"] != null ? dataSet.Tables[0].Rows[0]["CommercialLicenseNo"].ToString() : "---";




                        ObjbrokerRenwalModel.BankGuaranteeNo = dataSet.Tables[0].Rows[0]["BGNo"] != null ?
                            dataSet.Tables[0].Rows[0]["BGNo"].ToString() : "---";
                        ObjbrokerRenwalModel.BankName = dataSet.Tables[0].Rows[0]["BankName"] != null ?
                            dataSet.Tables[0].Rows[0]["BankName"].ToString() : "---";
                        ObjbrokerRenwalModel.BankGuaranteeIssuanceDate = dataSet.Tables[0].Rows[0]["BGIssueDt"] != null ?
                            dataSet.Tables[0].Rows[0]["BGIssueDt"].ToString() : "---";
                        ObjbrokerRenwalModel.BankGuaranteeExpiryDate = dataSet.Tables[0].Rows[0]["BGExpiryDate"] != null ?
                            dataSet.Tables[0].Rows[0]["BGExpiryDate"].ToString() : "---";
                        ObjbrokerRenwalModel.BankGuaranteeStatus = dataSet.Tables[0].Rows[0]["BGStatus"] != null ?
                            dataSet.Tables[0].Rows[0]["BGStatus"].ToString() : "---";


                    }

                }

                if (dataSet != null && dataSet.Tables.Count > 1)
                {
                    if (dataSet.Tables[1].Rows.Count > 0)
                    {
                        if (dataSet.Tables[1].Rows[0]["stateid"].ToString() == "EServiceRequestProceedState" || dataSet.Tables[1].Rows[0]["stateid"].ToString() == "EServiceRequestCreatedState" || dataSet.Tables[1].Rows[0]["stateid"].ToString() == "EServiceRequestRejectedState" || dataSet.Tables[1].Rows[0]["stateid"].ToString() == "EservTranReqCreatedState" || dataSet.Tables[1].Rows[0]["stateid"].ToString() == "EservTranReqInitRejectedState" || dataSet.Tables[1].Rows[0]["stateid"].ToString() == "EservTranReqInitAcceptedState" || dataSet.Tables[1].Rows[0]["stateid"].ToString() == "EservTranReqProceedState")
                        {
                            ObjbrokerRenwalModel.stateid = dataSet.Tables[1].Rows[0]["stateid"].ToString();
                        }
                        else
                        {
                            ObjbrokerRenwalModel.SubmitRequest = "false";
                            ObjbrokerRenwalModel.UploadDiv = "false";

                        }

                        selectedorg = dataSet.Tables[1].Rows[0]["AssociatedOrgIds"].ToString();
                        ObjbrokerRenwalModel.SelectedOrgidForIssuance = dataSet.Tables[1].Rows[0]["AssociatedOrgIds"].ToString();
                        ObjbrokerRenwalModel.RequestNumber = dataSet.Tables[1].Rows[0]["eservicerequestnumber"].ToString();
                        ObjbrokerRenwalModel.RejectionReason = dataSet.Tables[1].Rows[0]["RejectionRemarks"].ToString();
                        Session["RequestNumber"] = dataSet.Tables[1].Rows[0]["eservicerequestnumber"].ToString();
                        Session["Eservicerequestid"] = dataSet.Tables[1].Rows[0]["eservicerequestid"].ToString();

                        ObjbrokerRenwalModel.Eservicerequestid = CommonFunctions.CsUploadEncrypt(dataSet.Tables[1].Rows[0]["eservicerequestid"].ToString());
                    }
                }

                ViewBag.reqid1 = CommonFunctions.CsUploadEncrypt(Session["Eservicerequestid"].ToString());

                if (dataSet.Tables.Contains("EServiceDropdown"))
                {
                    if (dataSet != null && dataSet.Tables["EServiceDropdown"].Rows.Count > 0)

                    {
                        var DropdownBind = from s in dataSet.Tables[2].AsEnumerable()
                                           select new SelectListItem
                                           {

                                               Text = s["Name"].ToString(),
                                               Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())

                                           };
                        ObjbrokerRenwalModel.ddlDocumentTypesitems = DropdownBind.ToList();

                    }

                }
                if (dataSet.Tables.Contains("EServiceUploadedFiles"))
                {
                    var docsUploadedlist = from s in dataSet.Tables["EServiceUploadedFiles"].AsEnumerable()
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

            }

            catch (Exception ex)
            {
                // WriteToLogFile("caught Exception in [HttpGet]public ActionResult BrokerUpdate()" + ex.Message + ex.StackTrace.ToString(), "Serviceid=" + Session["ServiceId"].ToString() + "organizationid=" + Session["UserOrgID"].ToString() + "Tokenid=" + Session["TokenId"].ToString());
            }
            return View(ObjbrokerRenwalModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintFormRequest(BrokerUpdateModel Bobj)

        {
            DataSet ds=null;
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }
            if (Session["eServiceUserEmailId"] != null)
            {
                Bobj.eServiceUserEmailId = Session["eServiceUserEmailId"].ToString();
            }
            if (Bobj.BrokerType == "General Broker" || Bobj.BrokerType == "مخلص عام")
            {
                Bobj.passportNo = Bobj.TemppassportNo;
                Bobj.PassportExpirydate = Bobj.TempPassportExpirydate;
            }

            //Bobj.paidby = paidby;
            //Bobj.PaymentTypeId = PaymentTypeId;
            //Bobj.paidfor = paidfor;
            //Bobj.PaidByType = "B";
            //Bobj.urltoredirectforpayments = urltoredirectforpayments;
            //Bobj.redirecturl = redirecturl;

            try { 
            Dataclass objdataclass = new Dataclass();
              
            Bobj.Serviceid = Convert.ToInt32(Session["Serviceid"]);
            Bobj.Userid = Session["MicroClearUser"].ToString();
            Bobj.Organizationid = Convert.ToInt32(Session["Orgid"]);
            Bobj.RequestNumber = Session["RequestNumber"].ToString();

            //  Bobj.ChangeJobTitleTo = CommonFunctions.CsUploadDecrypt(Request.Form["brokerTypeSelect"].ToString());

            //Bobj.PassportExpirydate = Bobj.TempPassportExpirydate;
            //Bobj.TradeLicenseExpiryDate = Bobj.TempTradeLicenseExpiryDate;
            // Bobj.CivilIdExpirydate = Bobj.TempCivilIdExpirydate;

            Bobj.BrokerArabicName = Session["MicroClearUsername"].ToString();
            Bobj.Eservicerequestid = Session["Eservicerequestid"].ToString();
            Bobj.Referenceprofile = Session["Referenceprofile"].ToString();

            HttpCookie langCookie = Request.Cookies["culture"];
            Bobj.lang = langCookie.Value == "en" ? "eng" : "ar"; //"eng";


            if (!String.IsNullOrWhiteSpace(Bobj.ChangeJobTitleTo))
            {
                Bobj.ChangeJobTitleTo = CommonFunctions.CsUploadDecrypt(Bobj.ChangeJobTitleTo);
            }


             ds = objdataclass.BrokerDetailspost(Bobj);
            }
            catch(Exception ex)
            {
                CommonFunctions.WriteToLogFile("public ActionResult PrintFormRequest(BrokerUpdateModel Bobj)" + ex.StackTrace.ToString() + ex.Message.ToString(), Session["UserId"].ToString());

            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                String redirectionURL = "";
                if (ds.Tables["UpdateUserDetails"].Rows[0]["RedirectUrl"].ToString().Contains("CallPaymentGateWay"))
                {
                    redirectionURL = ds.Tables["UpdateUserDetails"].Rows[0]["RedirectUrl"].ToString();
                }
                else
                {
                    redirectionURL = ds.Tables["UpdateUserDetails"].Rows[0]["RedirectUrl"].ToString() + CommonFunctions.CsUploadEncrypt(Session["RequestNumber"].ToString());
                }

                String url = redirectionURL;

                //var ss = Session["ServiceId"] != null ? CommonFunctions.LogUserActivity(
                //     "SubmittedARequest", "", "", "", Session["ServiceId"].ToString(), "RequestNumber+'" + Session["RequestNumber"].ToString() + "' and The Redirect Url Which he received PostSubmission'" + redirectionURL + "' ") : CommonFunctions.LogUserActivity(
                //       "SubmittedARequest", "", "", "", "", "RequestNumber+'" + Session["RequestNumber"].ToString() + "' and The Redirect Url Which he received PostSubmission'" + redirectionURL + "'");

                return Redirect(redirectionURL);
            }
            else
            {
                return View("PrintFormRequest", Bobj);
            }

        }

        #endregion service Form

        #region GetBrokersDetails  Ajax
        [WebMethod]
        public String GetBrokersDetails(String serviceId = "")
        {
            //CheckSession();

            if (Session["UserId"] == null)
            {
                return "-1";
            }


            String brokers = String.Empty;

            brokers = GetBrokersDetail(serviceId);

            return brokers;
        }

        public String GetBrokersDetail(String serviceId = "")
        {
            String Brokers = String.Empty;

            try
            {
                Dataclass dataClassObject = new Dataclass();

                BrokerRenewalModel databaseModel = new BrokerRenewalModel();

                databaseModel.Orgid = Session["UserOrgID"].ToString();

                databaseModel.Userid = Session["Userid"].ToString();
                databaseModel.Requestorserviceid = serviceId; //Session["ServiceId"].ToString();

                DataSet dataSet = dataClassObject.GetBrokersDetaillist(databaseModel);
                if (dataSet.Tables.Count != 0)
                {
                    DataTable dataTable = dataSet.Tables[0];

                    if (dataTable.Rows.Count > 0)
                    {
                        Brokers = ConvertJsonString(dataTable);

                        Session["ServiceId"] = serviceId;
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

            List<Dictionary<String, object>> rows = new List<Dictionary<String, object>>();
            Dictionary<String, object> row;

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



        [HttpPost]
        public JsonResult DeleteFile(String dataItem)
        {
            if (Session["UserId"] == null)
            {
                return Json(new { message = "-1" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                Dataclass objdataclass = new Dataclass();

                StringBuilder sb = new StringBuilder();
                String[] encrypteddata = dataItem.Split(',');
                ArrayList decryptedvalue = new ArrayList();
                foreach (var item in encrypteddata)
                {

                    dataItem = CommonFunctions.CsUploadDecrypt(item);

                    if (dataItem.All(char.IsDigit))
                    {
                        sb.Append(dataItem);
                    }
                }

                OrgReqResultDocInfoParams objdelete = new OrgReqResultDocInfoParams();

                objdelete.mUserid = Session["UserId"].ToString();
                objdelete.tokenId = Session["TokenId"].ToString();
                objdelete.sOrgReqDocId = sb.ToString();
                objdelete.EserviceRequestid = Session["EserviceRequestid"].ToString();

                String status = objdataclass.DocumentdeleteForEservice(objdelete);

                String s = sb.ToString();

                return Json(new { Message = status, JsonRequestBehavior.AllowGet });
            }

            catch (Exception ex)
            {
                String _Exception = ex.ToString();
                Response.StatusCode = 500;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { message = "error" }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult DownloadFile(String fileName, string referenceprofile = "BRSERenewalDocs")
        {
            OpenDocumentParams objDownloadFile = new OpenDocumentParams();
            CheckSession();
            String id = "";
            String NewFileName = string.Empty;
            String MyfileName = string.Empty; string strpath = string.Empty;
            objDownloadFile.hiderefprofile = referenceprofile;

            String sSLUN = string.Empty;
            String sSLD = string.Empty;
            String sSLP = string.Empty;
            String sSFP = string.Empty;
            String ShareFolderPath = string.Empty;
            String sPwd = "";

            SymmetricEncryption CgServices1 = ServiceFactory.GetSymmetricEncryptionInstance();

            Dataclass objdataclass = new Dataclass();


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

            }
            else
            {
                id = fileName;
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

                    //WriteToLogFile("Error In downloading File" + ex.StackTrace.ToString() + ex.Message.ToString() + "filename" + fileName.ToString(), "new Id" + id.ToString());
                }

            }
            ViewBag.Errormessage = "Error In downloading File";
            return View("Error");



        }

        private void AssignServiceIdtoSession(String RequestNumber)
        {


            if (RequestNumber.Contains("PRCL"))
            {
                Session["ServiceId"] = 20;
            }
            else if (RequestNumber.Contains("PRIL"))
            {
                Session["ServiceId"] = 21;
            }
            else if (RequestNumber.Contains("PRCB"))
            {
                Session["ServiceId"] = 23;
            }
            else if (RequestNumber.Contains("PRCA"))
            {
                Session["ServiceId"] = 22;
            }
            else if (RequestNumber.Contains("PRRG"))
            {
                Session["ServiceId"] = 24;
            }
            else if (RequestNumber.Contains("PRGB"))
            {
                Session["ServiceId"] = 25;
            }
            else if (RequestNumber.Contains("PRRL"))
            {
                Session["ServiceId"] = 26;
            }
            else if (RequestNumber.Contains("PRJR"))
            {
                Session["ServiceId"] = 27;
            }
            else if (RequestNumber.Contains("PRJT"))
            {
                Session["ServiceId"] = 28;
            }
            else if (RequestNumber.Contains("PRRR"))
            {
                Session["ServiceId"] = 29;
            }
            else if (RequestNumber.Contains("PRTR"))
            {
                Session["ServiceId"] = 30;
            }
            else if (RequestNumber.Contains("PRJJ"))
            {
                Session["ServiceId"] = 31;
            }
            else if (RequestNumber.Contains("PRJC"))
            {
                Session["ServiceId"] = 32;
            }
            else if (RequestNumber.Contains("PRDL"))
            {
                Session["ServiceId"] = 33;
            }
        }
        // commented by azhar service id and prefix mismatch in 2 services
        //private void AssignServiceIdtoSession(String RequestNumber)
        //{
        //    if (RequestNumber.Contains("PRCL"))
        //    {
        //        Session["ServiceId"] = 20;
        //    }
        //    else if (RequestNumber.Contains("PRIL"))
        //    {
        //        Session["ServiceId"] = 21;
        //    }
        //    else if (RequestNumber.Contains("PRCD"))
        //    {
        //        Session["ServiceId"] = 22;
        //    }
        //    else if (RequestNumber.Contains("PRCA"))
        //    {
        //        Session["ServiceId"] = 23;
        //    }
        //    else if (RequestNumber.Contains("PRRG"))
        //    {
        //        Session["ServiceId"] = 24;
        //    }
        //    else if (RequestNumber.Contains("PRGB"))
        //    {
        //        Session["ServiceId"] = 25;
        //    }
        //    else if (RequestNumber.Contains("PRRL"))
        //    {
        //        Session["ServiceId"] = 26;
        //    }
        //    else if (RequestNumber.Contains("PRJR"))
        //    {
        //        Session["ServiceId"] = 27;
        //    }
        //    else if (RequestNumber.Contains("PRJT"))
        //    {
        //        Session["ServiceId"] = 28;
        //    }
        //    else if (RequestNumber.Contains("PRRR"))
        //    {
        //        Session["ServiceId"] = 29;
        //    }
        //    else if (RequestNumber.Contains("PRTR"))
        //    {
        //        Session["ServiceId"] = 30;
        //    }
        //    else if (RequestNumber.Contains("PRJJ"))
        //    {
        //        Session["ServiceId"] = 31;
        //    }
        //    else if (RequestNumber.Contains("PRJC"))
        //    {
        //        Session["ServiceId"] = 32;
        //    }
        //    else if (RequestNumber.Contains("PRDL"))
        //    {
        //        Session["ServiceId"] = 33;
        //    }
        //}
    }
}