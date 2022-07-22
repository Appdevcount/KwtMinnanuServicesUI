using DocumentManagementServices;
using MicroClear.EnterpriseSolutions.CryptographyServices;
using MicroClear.EnterpriseSolutions.ServiceFactories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClearingAgentExamRequestController : MyBaseController //Controller
    {

        String sSLUN = String.Empty;
        String sSLD = String.Empty;
        String sSLP = String.Empty;
        String sSFP = String.Empty;
        String ShareFolderPath = String.Empty;

        String examRequestReferenceProfile = ConfigurationManager.AppSettings["7"].ToString();
        String ServiceId = ConfigurationManager.AppSettings["ExamService"].ToString();
        String EServiceRequestDetailsSubmittedState = ConfigurationManager.AppSettings["EServiceRequestDetailsSubmittedState"].ToString();

        String EServiceRequestSubmittedState = ConfigurationManager.AppSettings["EServiceRequestSubmittedState"].ToString();

        String EServiceRequestCreatedState = ConfigurationManager.AppSettings["EServiceRequestCreatedState"].ToString();
        String EServicesRequestDetailsCreatedState = ConfigurationManager.AppSettings["EServiceRequestDetailsCreatedState"].ToString();


        Dataclass objdataclass = new Dataclass();
        DataSet dataSet = new DataSet();

        examRequestViewModel examRequestViewMd;
        EServiceRequestsDetails requestDetails;

        SymmetricEncryption CgServices1 = ServiceFactory.GetSymmetricEncryptionInstance();

        // GET: ClearingAgentExamRequest

        public ActionResult Index(String EServiceRequestNumber = "")
        {

            CheckSession();

            if (Session["LegalEntity"].ToString() == "2")
            {
                SecurityParams objmyorglist = new SecurityParams();
                DataTable dataTableOrg = new DataTable();

                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();

                dataTableOrg = objdataclass.MyOrganizations_Data(objmyorglist);


                
                if (dataTableOrg.Rows.Count >  0)
                {
                    if (dataTableOrg.Rows[0]["Name"] != null)
                    {
                        Session["orgName"] = dataTableOrg.Rows[0]["Name"].ToString();
                    }
                    
                }

            }


            String requestNumber = String.Empty;
            Dictionary<String, String> brokerTypes = new Dictionary<String, String>();

            brokerTypes.Add(ConfigurationManager.AppSettings["generalBroker"].ToString(), Resources.Resource.generalBroker);
            brokerTypes.Add(ConfigurationManager.AppSettings["subBroker"].ToString(), Resources.Resource.subBroker);
            brokerTypes.Add(ConfigurationManager.AppSettings["subMessanger"].ToString(), Resources.Resource.subMessanger);

            brokerTypes.Add(ConfigurationManager.AppSettings["specialBroker"].ToString(), Resources.Resource.specialBroker);
            brokerTypes.Add(ConfigurationManager.AppSettings["specialMessanger"].ToString(), Resources.Resource.specialMessanger);
            brokerTypes.Add(ConfigurationManager.AppSettings["directReleaseBroker"].ToString(), Resources.Resource.directReleaseBroker);


            if (!String.IsNullOrWhiteSpace(EServiceRequestNumber))
            {
                Session["examRequestViewMd"] = null;

                examRequestViewMd = new examRequestViewModel();
                String decodedRequestNumber = HttpUtility.HtmlDecode(EServiceRequestNumber);//Server.UrlDecode(Request.QueryString["RequestNumber"].ToString());
                requestNumber = CommonFunctions.CsUploadDecrypt(decodedRequestNumber);//Session["RequestNumberToEdit"].ToString();//
                requestNumber = HttpUtility.HtmlDecode(requestNumber);

                BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();
                ObjbrokerRenwalModel.Userid = "";
                ObjbrokerRenwalModel.Organizationid = 0;
                ObjbrokerRenwalModel.RequestNumber = requestNumber;
                ObjbrokerRenwalModel.Referenceprofile = examRequestReferenceProfile;

                HttpCookie lang = Request.Cookies["culture"];
                ObjbrokerRenwalModel.lang = lang.Value;


                ObjbrokerRenwalModel.Serviceid = int.Parse(ServiceId);
                ObjbrokerRenwalModel.mobileUserId = int.Parse(Session["UserId"].ToString());


                DataSet ds = objdataclass.FNGetBrokerDetailsForUpdate(ObjbrokerRenwalModel);

                var sss = Session["ServiceId"] != null ? CommonFunctions.LogUserActivity(
                "OpeningAExistingExamRequest", "", "", "", Session["ServiceId"].ToString(), "RequestNumber+'" + requestNumber.ToString() + "'") : CommonFunctions.LogUserActivity(
                "OpeningAExistingExamRequest", "", "", "", "", "RequestNumber+'" + requestNumber.ToString() + "'");

                if (ds.Tables["EServiceRequests"] != null)
                {
                    requestDetails = new EServiceRequestsDetails();

                    examRequestViewMd.RequestsDetails = new List<EServiceRequestsDetails>();
                    examRequestViewMd.RequesterUserId = Int64.Parse(Session["UserId"].ToString());
                    examRequestViewMd.Referenceprofile = examRequestReferenceProfile;
                    examRequestViewMd.Serviceid = int.Parse(ServiceId); //7;
                    examRequestViewMd.ServiceId = int.Parse(ServiceId);
                    examRequestViewMd.mobileUserId = int.Parse(Session["UserId"].ToString());
                    examRequestViewMd.BrokerType = ds.Tables["EServiceRequests"].Rows[0]["RequestForUserType"].ToString();

                    Session["EserviceRequestId"] = ds.Tables["EServiceRequests"].Rows[0]["EserviceRequestId"].ToString();


                    ViewBag.brokerTy = ds.Tables["EServiceRequests"].Rows[0]["RequestForUserType"].ToString();
                    Session["RequestForUserType"] = ds.Tables["EServiceRequests"].Rows[0]["RequestForUserType"].ToString();

                    ViewBag.Referenceprofile = examRequestReferenceProfile;
                    ViewBag.EserviceRequestId = ds.Tables["EServiceRequests"].Rows[0]["EserviceRequestId"].ToString();

                    ViewBag.TokenId = Session["TokenId"].ToString();
                    ViewBag.reqid1 = CommonFunctions.CsUploadEncrypt(ds.Tables["EServiceRequests"].Rows[0]["EserviceRequestId"].ToString());
                    ViewBag.EserviceRequestId = CommonFunctions.CsUploadEncrypt(ds.Tables["EServiceRequests"].Rows[0]["EserviceRequestId"].ToString());


                    requestDetails.RequestForUserType = Int64.Parse(Session["RequestForUserType"].ToString());
                    requestDetails.CivilID = Session["CivilIdOfLoggedInUser"] != null ? Session["CivilIdOfLoggedInUser"].ToString() : "";
                    requestDetails.Email = Session["emailOfLoggedInUser"] != null ? Session["emailOfLoggedInUser"].ToString() : "";
                    requestDetails.Gender = Session["genderOfLoggedInUser"] != null ? Session["genderOfLoggedInUser"].ToString() : "";

                    examRequestViewMd.brokerTypeString = brokerTypes[ds.Tables["EServiceRequests"].Rows[0]["RequestForUserType"].ToString()];


                    Session["requesterName"] = Session["UserName"]; //"Company or General Broker Name";
                    Session["requesterLicenceNo"] = Session["LicenseNumberOfLoggedInUser"] != null ? Session["LicenseNumberOfLoggedInUser"].ToString() : "";//"Company Licence No or general broker Licence No";
                    Session["requesterMobileNo"] = Session["mobileNumberOfLoggedInUser"] != null ? Session["mobileNumberOfLoggedInUser"].ToString() : ""; //"general broker Mobile No";


                    examRequestViewMd.requesterName = Session["requesterName"] != null ? Session["requesterName"].ToString() : "";
                    examRequestViewMd.requesterLicenceNo = Session["requesterLicenceNo"] != null ? Session["requesterLicenceNo"].ToString() : "";
                    examRequestViewMd.requesterMobileNo = Session["requesterMobileNo"] != null ? Session["requesterMobileNo"].ToString() : "";
                    examRequestViewMd.EserviceRequestNumber = requestNumber;


                    requestDetails.ArabicFirstName = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["ArabicFirstName"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["ArabicFirstName"].ToString() : "";
                    requestDetails.ArabicSecondName = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["ArabicSecondName"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["ArabicSecondName"].ToString() : "";
                    requestDetails.ArabicThirdName = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["ArabicThirdName"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["ArabicThirdName"].ToString() : "";
                    requestDetails.ArabicLastName = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["ArabicLastName"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["ArabicLastName"].ToString() : "";
                    requestDetails.EnglishFirstName = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["EnglishFirstName"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["EnglishFirstName"].ToString() : "";
                    requestDetails.EnglishSecondName = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["EnglishSecondName"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["EnglishSecondName"].ToString() : "";
                    requestDetails.EnglishThirdName = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["EnglishThirdName"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["EnglishThirdName"].ToString() : "";
                    requestDetails.EnglishLastName = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["EnglishLastName"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["EnglishLastName"].ToString() : "";
                    requestDetails.Nationality = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["Nationality"].ToString()) ? int.Parse(ds.Tables["EServiceRequests"].Rows[0]["Nationality"].ToString()) : 0;

                    requestDetails.CivilID = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["CivilID"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["CivilID"].ToString() : "";


                    if (!String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["CivilIDExpiryDateFormatted"].ToString()))
                    {
                        //requestDetails.CivilIDExpiryDate = DateTime.ParseExact(ds.Tables["EServiceRequests"].Rows[0]["CivilIDExpiryDateFormatted"].ToString(), "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                        requestDetails.CivilIDExpiryDateFormatted = ds.Tables["EServiceRequests"].Rows[0]["CivilIDExpiryDateFormatted"].ToString();
                    }

                    requestDetails.PassportNo = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["PassportNo"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["PassportNo"].ToString() : "";

                    if (!String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["PassportExpiryDateFormatted"].ToString()))
                    {
                        //requestDetails.PassportExpiryDate = DateTime.ParseExact(ds.Tables["EServiceRequests"].Rows[0]["PassportExpiryDateFormatted"].ToString(), "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        requestDetails.PassportExpiryDateFormatted = ds.Tables["EServiceRequests"].Rows[0]["PassportExpiryDateFormatted"].ToString();
                    }

                    requestDetails.Address = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["Address"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["Address"].ToString() : "";
                    requestDetails.Email = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["Email"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["Email"].ToString() : "";

                    requestDetails.Remarks = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["Remarks"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["Remarks"].ToString() : "";
                    requestDetails.MobileNumber = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["MobileNumber"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["MobileNumber"].ToString() : "";

                    requestDetails.Gender = !String.IsNullOrWhiteSpace(ds.Tables["EServiceRequests"].Rows[0]["gender"].ToString()) ? ds.Tables["EServiceRequests"].Rows[0]["gender"].ToString() : "";



                    examRequestViewMd.RequestsDetails.Add(requestDetails);

                    
                    string mylang = lang.Value;

                    if (ds.Tables["EServiceDropdown"].Rows.Count > 0)

                    {
                        if (Session["EServiceDropdownForExam"] != null)
                        {
                            Session.Remove("EServiceDropdownForExam");

                            Session.Add("EServiceDropdownForExam", ds.Tables["EServiceDropdown"]);
                        }
                        else
                        {
                            Session.Add("EServiceDropdownForExam", ds.Tables["EServiceDropdown"]);
                        }



                        var DropdownBind = from str in ds.Tables["EServiceDropdown"].AsEnumerable()
                                           select new SelectListItem
                                           {
                                               // Text = str["Name"].ToString() + " (" + str["Cnt"] + ") ",
                                               Text = mylang == "en"?str["engname"].ToString(): str["arabicname"].ToString(),
                                               Value = CommonFunctions.CsUploadEncrypt(str["DeclarationDocumentId"].ToString())
                                           };
                        examRequestViewMd.ddlDocumentTypesitems = DropdownBind.ToList();

                    }


                    if (ds.Tables.Contains("EServiceUploadedFiles"))
                    {
                        var docsUploadedlist = from s in ds.Tables["EServiceUploadedFiles"].AsEnumerable()
                                               select new BrFileResult
                                               {

                                                   //  }),
                                                   DocumentId = CommonFunctions.CsUploadEncrypt(s["Documentid"].ToString()),
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

                        examRequestViewMd.ListOfUploadedFiles = docsUploadedlist.ToList();
                    }


                    return View(examRequestViewMd);
                }


            }




            //if (ViewBag.requesterName == null || String.IsNullOrWhiteSpace(ViewBag.requesterName))
            //{
            //    return RedirectToAction("ExamRequest", "ClearingAgentExamRequest");
            //}

            else
            {
                if (Session["UserName"] == null ||
               Session["LicenseNumberOfLoggedInUser"] == null ||
               Session["mobileNumberOfLoggedInUser"] == null)
                {
                    return RedirectToAction("ExamRequest", "ClearingAgentExamRequest");
                }

                if (Session["RequestForUserType"] == null)
                {
                    return RedirectToAction("ExamRequest", "ClearingAgentExamRequest");
                }

                if (Session["examRequestViewMd"] != null)
                {
                    ViewBag.brokerTy = Session["RequestForUserType"].ToString();

                    ViewBag.Referenceprofile = examRequestReferenceProfile;
                    Session["Referenceprofile"] = examRequestReferenceProfile;
                    ViewBag.EserviceRequestId = Session["EserviceRequestId"].ToString();

                    ViewBag.TokenId = Session["TokenId"].ToString();
                    ViewBag.reqid1 = CommonFunctions.CsUploadEncrypt(Session["Eservicerequestid"].ToString());
                    ViewBag.EserviceRequestId = CommonFunctions.CsUploadEncrypt(Session["Eservicerequestid"].ToString());

                    examRequestViewModel examRequestViewModel = Session["examRequestViewMd"] as examRequestViewModel;

                    examRequestViewModel.brokerTypeString = brokerTypes[Session["RequestForUserType"].ToString()];


                    return View(examRequestViewModel);
                }
            }





            //if (Session["EserviceRequestId"] == null)
            //{
            examRequestViewMd = new examRequestViewModel();
            requestDetails = new EServiceRequestsDetails();

            examRequestViewMd.RequestsDetails = new List<EServiceRequestsDetails>();

            examRequestViewMd.RequesterUserId = Int64.Parse(Session["UserId"].ToString());
            //examRequestViewMd.organizationid = String.Empty;
            examRequestViewMd.Referenceprofile = examRequestReferenceProfile;//"BRSExamDOCS";


            HttpCookie langCookie = Request.Cookies["culture"];
            examRequestViewMd.lang = langCookie.Value;


            examRequestViewMd.Serviceid = int.Parse(ServiceId); //7;
            examRequestViewMd.ServiceId = int.Parse(ServiceId);

            examRequestViewMd.EXAMREQ = true;

            // 27-FEB
            //examRequestViewMd.requestForMobileUserId = Int64.Parse(Session["UserId"].ToString());
            examRequestViewMd.mobileUserId = int.Parse(Session["UserId"].ToString());


            examRequestViewMd.BrokerType = Session["RequestForUserType"].ToString();

            String generalBrokerTypeValue = ConfigurationManager.AppSettings["generalBroker"].ToString();



            requestDetails.CivilID = Session["CivilIdOfLoggedInUser"] != null ? Session["CivilIdOfLoggedInUser"].ToString() : "";
            requestDetails.Email = Session["emailOfLoggedInUser"] != null ? Session["emailOfLoggedInUser"].ToString() : "";
            requestDetails.Gender = Session["genderOfLoggedInUser"] != null ? Session["genderOfLoggedInUser"].ToString() : "";

            dataSet = objdataclass.InitiateExamRequest(examRequestViewMd);

            examRequestViewMd.RequestsDetails.Add(requestDetails);
            //}



            Session["requesterName"] = Session["UserName"]; //"Company or General Broker Name";
            Session["requesterLicenceNo"] = Session["LicenseNumberOfLoggedInUser"] != null ? Session["LicenseNumberOfLoggedInUser"].ToString() : "32234234";//"Company Licence No or general broker Licence No";
            Session["requesterMobileNo"] = Session["mobileNumberOfLoggedInUser"] != null ? Session["mobileNumberOfLoggedInUser"].ToString() : "98995673"; //"general broker Mobile No";

            //Session["RequestForUserType"] = 33224591;// ConfigurationManager.AppSettings["generalBroker"].ToString();
            //33224591        General Broker
            //33224593        Sub Broker
            //33237149        Messanger 
            //33224592        Special Broker
            //33237150        Special Messanger
            //33237246        Direct Release Broker

            if (Session["RequestForUserType"] != null)
            {
                if (examRequestViewMd != null)
                {
                    examRequestViewMd.RequestsDetails[0].RequestForUserType = Int64.Parse(Session["RequestForUserType"].ToString());
                }

                if (examRequestViewMd.RequestsDetails[0].RequestForUserType.ToString() != generalBrokerTypeValue)
                {
                    requestDetails.CivilID = String.Empty;
                    requestDetails.Email = String.Empty;

                }


                examRequestViewMd.brokerTypeString = brokerTypes[Session["RequestForUserType"].ToString()];

                ViewBag.brokerTy = Session["RequestForUserType"].ToString();

                examRequestViewMd.requesterName = Session["requesterName"] != null ? Session["requesterName"].ToString() : "";
                examRequestViewMd.requesterLicenceNo = Session["requesterLicenceNo"] != null ? Session["requesterLicenceNo"].ToString() : "";
                examRequestViewMd.requesterMobileNo = Session["requesterMobileNo"] != null ? Session["requesterMobileNo"].ToString() : "";
            }


            if (dataSet.Tables.Count > 0)
            {
                //if (dataSet.Tables["EServiceRequests"].Rows[0]["stateid"].ToString()
                //    == "EServiceRequestProceedState" ||
                //   dataSet.Tables["EServiceRequests"].Rows[0]["stateid"].ToString()
                //   == "EServiceRequestCreatedState" ||
                //   dataSet.Tables["EServiceRequests"].Rows[0]["stateid"].ToString()
                //   == "EServiceRequestRejectedState")
                //{

                //}
                //else
                //{
                //    examRequestViewMd.SubmitRequest = "false";
                //    examRequestViewMd.UploadDiv = "false";
                //}

                HttpCookie lang = Request.Cookies["culture"];
                string mylang = lang.Value;
                if (dataSet.Tables["EServiceDropdown"].Rows.Count > 0)

                {

                    if (Session["EServiceDropdownForExam"] != null)
                    {
                        Session.Remove("EServiceDropdownForExam");

                        Session.Add("EServiceDropdownForExam", dataSet.Tables["EServiceDropdown"]);
                    }
                    else
                    {
                        Session.Add("EServiceDropdownForExam", dataSet.Tables["EServiceDropdown"]);
                    }


                    var DropdownBind = from str in dataSet.Tables["EServiceDropdown"].AsEnumerable()
                                       select new SelectListItem
                                       {
                                           // Text = str["Name"].ToString() + " (" + str["Cnt"] + ") ",
                                           //   Text = str["Name"].ToString(),
                                           Text = mylang == "en" ? str["engname"].ToString() : str["arabicname"].ToString(),

                                           Value = CommonFunctions.CsUploadEncrypt(str["DeclarationDocumentId"].ToString())
                                       };
                    examRequestViewMd.ddlDocumentTypesitems = DropdownBind.ToList();

                }


                if (dataSet.Tables.Contains("EServiceUploadedFiles"))
                {
                    var docsUploadedlist = from s in dataSet.Tables["EServiceUploadedFiles"].AsEnumerable()
                                           select new BrFileResult
                                           {

                                               //  }),
                                               DocumentId = CommonFunctions.CsUploadEncrypt(s["Documentid"].ToString()),
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

                    examRequestViewMd.ListOfUploadedFiles = docsUploadedlist.ToList();
                }

                examRequestViewMd.EserviceRequestNumber = dataSet.Tables["EServiceRequests"].Rows[0]["EserviceRequestNumber"].ToString();

                Session["EserviceRequestId"] = dataSet.Tables["EServiceRequests"].Rows[0]["EserviceRequestId"].ToString();
            }

            ViewBag.Referenceprofile = examRequestReferenceProfile;
            ViewBag.EserviceRequestId = Session["EserviceRequestId"].ToString();
            Session["Referenceprofile"] = examRequestReferenceProfile;
            // by azhar on 03132019
            ViewBag.TokenId = Session["TokenId"].ToString();
            ViewBag.reqid1 = CommonFunctions.CsUploadEncrypt(Session["Eservicerequestid"].ToString());
            ViewBag.EserviceRequestId = CommonFunctions.CsUploadEncrypt(Session["Eservicerequestid"].ToString());

            //end 

            var ss = Session["ServiceId"] != null ? CommonFunctions.LogUserActivity(
"CreatingaNewExamRequest", "", "", "", Session["ServiceId"].ToString(), "RequestNumber+'" + examRequestViewMd.EserviceRequestNumber + "'") : CommonFunctions.LogUserActivity(
"CreatingaNewExamRequest", "", "", "", "", "RequestNumber+'" + examRequestViewMd.EserviceRequestNumber + "'");
            if (Session["examRequestViewMd"] != null)
            {
                Session.Remove("examRequestViewMd");
            }

            Session["examRequestViewMd"] = examRequestViewMd;


            return View(examRequestViewMd);

        }






        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SumbitRequest(FormCollection form)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogOut", "registration");
            }


            if (form.Keys.Count < 1)
            {
                return RedirectToAction("LogOut", "registration");
            }

            Dataclass objdataclass = new Dataclass();
            DataTable dataTable = new DataTable();

            EservicesRequests ServicesRequest = new EservicesRequests();

            EServiceRequestsDetails ServiceRequestDetails = new EServiceRequestsDetails();

            RequestExistsByCivilId RequestExistByCivilId = new RequestExistsByCivilId();


            if (form["sumbitExamRequest"] != null &&
                form["saveExamRequestDraft"] == null)
            {

                RequestExistByCivilId.ServiceId = ServiceId;
                RequestExistByCivilId.CivilId = form["brokerCivilId"].ToString();
                RequestExistByCivilId.StateId = EServiceRequestSubmittedState; //EServiceRequestCreatedState;

                dataTable = objdataclass.GetRequestIfExistsByCivilId(RequestExistByCivilId);

                String passedExam = ConfigurationManager.AppSettings["passedExam"].ToString();


                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        String examResult = !String.IsNullOrWhiteSpace(row["ExamResult"].ToString()) ? row["ExamResult"].ToString() : "";

                        if (examResult == passedExam
                         || String.IsNullOrWhiteSpace(examResult)
                         || String.IsNullOrEmpty(examResult))
                        {
                            ViewBag.requestSubmitted = "Duplicated";
                            return View("Index");
                        }
                    }
                }

                ServicesRequest.EserviceRequestId = Int64.Parse(Session["EserviceRequestId"].ToString());

                ServicesRequest.EserviceRequestNumber = form["requestNo"].ToString();
                ServicesRequest.RequesterUserId = Int64.Parse(Session["UserId"].ToString());  //Session[];
                ServicesRequest.ServiceId = int.Parse(ServiceId);// 7; 

                ServicesRequest.DateCreated = DateTime.Now;
                ServicesRequest.CreatedBy = Session["UserId"].ToString();
                ServicesRequest.ModifiedBy = "";
                ServicesRequest.DateModified = null;
                ServicesRequest.OwnerOrgId = 0;
                ServicesRequest.OwnerLocId = 0;

                ServiceRequestDetails.RequestForUserType = Int64.Parse(Session["RequestForUserType"].ToString());
                ServiceRequestDetails.RequestServicesId = ServiceId;
                //ServiceRequestDetails.OrganizationId = ;

                String generalBrokerType = ConfigurationManager.AppSettings["generalBroker"].ToString();

                String subBrokerType = ConfigurationManager.AppSettings["subBroker"].ToString();
                String subMessangerType = ConfigurationManager.AppSettings["subMessanger"].ToString();

                String specialBrokerType = ConfigurationManager.AppSettings["specialBroker"].ToString();
                String specialMessangerType = ConfigurationManager.AppSettings["specialMessanger"].ToString();
                String directReleaseBrokerType = ConfigurationManager.AppSettings["directReleaseBroker"].ToString();


                ViewBag.brokerTy = Session["RequestForUserType"].ToString();//ServiceRequestDetails.RequestForUserType.ToString();

                if (Session["LegalEntity"].ToString() == "1" // Clearning Agent
                    && ServiceRequestDetails.RequestForUserType.ToString() != generalBrokerType)
                {
                    ServiceRequestDetails.RequesterLicenseNumber = form["generalbrokerLicenceNo"].ToString();
                    ServiceRequestDetails.RequesterArabicName = form["generalbrokerName"].ToString();
                    ServiceRequestDetails.MobileNumber = form["generalbrokerMobileNo"].ToString();
                    ServiceRequestDetails.RequesterEnglishName = form["generalbrokerName"].ToString();
                }
                else if (Session["LegalEntity"].ToString() == "2"
                         && ServiceRequestDetails.RequestForUserType.ToString() != generalBrokerType
                         && ServiceRequestDetails.RequestForUserType.ToString() != subBrokerType
                         && ServiceRequestDetails.RequestForUserType.ToString() != subMessangerType)
                {
                    ServiceRequestDetails.RequesterLicenseNumber = form["commercialRegistrationNo"].ToString();
                    ServiceRequestDetails.RequesterArabicName = form["companyName"].ToString();
                    ServiceRequestDetails.RequesterEnglishName = form["companyName"].ToString();
                }


                ServiceRequestDetails.ArabicFirstName = form["brokerNameArabicFirst"].ToString();
                ServiceRequestDetails.ArabicSecondName = form["brokerNameArabicSecond"].ToString();
                ServiceRequestDetails.ArabicThirdName = form["brokerNameArabicThird"].ToString();
                ServiceRequestDetails.ArabicLastName = form["brokerNameArabicLast"].ToString();

                ServiceRequestDetails.EnglishFirstName = form["brokerNameEnglishFirst"].ToString();
                ServiceRequestDetails.EnglishSecondName = !String.IsNullOrWhiteSpace(form["brokerNameEnglishSecond"].ToString()) ? form["brokerNameEnglishSecond"].ToString() : "";
                ServiceRequestDetails.EnglishThirdName = !String.IsNullOrWhiteSpace(form["brokerNameEnglishThird"].ToString()) ? form["brokerNameEnglishThird"].ToString() : "";
                ServiceRequestDetails.EnglishLastName = form["brokerNameEnglishLast"].ToString();
                // get the selected Nationality
                ServiceRequestDetails.Nationality = !String.IsNullOrWhiteSpace(form["selectedCountryVal"].ToString()) ? int.Parse(form["selectedCountryVal"].ToString()) : 5051;

                ServiceRequestDetails.CivilID = form["brokerCivilId"].ToString();

                if (form["brokerCivilIdexpiryDate"] != null)
                {
                    ServiceRequestDetails.CivilIDExpiryDate = DateTime.ParseExact(form["brokerCivilIdexpiryDate"].ToString(), "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                }
                else
                {
                    ServiceRequestDetails.CivilIDExpiryDate = null;
                }


                ServiceRequestDetails.MobileNumber = form["brokerMobileNumber"].ToString();

                if (ServiceRequestDetails.RequestForUserType.ToString() != generalBrokerType)
                {
                    ServiceRequestDetails.PassportNo = form["brokerPassportNo"].ToString();
                }
                else
                {
                    ServiceRequestDetails.PassportNo = "";
                }

                if (ServiceRequestDetails.RequestForUserType.ToString() != generalBrokerType)
                {
                    ServiceRequestDetails.Gender = form["genderSelect"].ToString();

                    if (form["brokerPassportExpiryDate"] != null)
                    {
                        ServiceRequestDetails.PassportExpiryDate = DateTime.ParseExact(form["brokerPassportExpiryDate"].ToString(), "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")); //null);
                    }
                }

                else
                {
                    ServiceRequestDetails.Gender = Session["genderOfLoggedInUser"] != null ? Session["genderOfLoggedInUser"].ToString() : "M";

                    ServiceRequestDetails.PassportExpiryDate = null;
                }


                ServiceRequestDetails.Address = !String.IsNullOrWhiteSpace(form["brokerAddress"].ToString()) ? form["brokerAddress"].ToString() : "";
                ServiceRequestDetails.Email = !String.IsNullOrWhiteSpace(form["brokerEmailID"].ToString()) ? form["brokerEmailID"].ToString() : "";

                ServiceRequestDetails.LicenseNumber = String.Empty;
                ServiceRequestDetails.LicenseNumberExpiryDate = null;

                ServiceRequestDetails.Remarks = !String.IsNullOrWhiteSpace(form["brokerRemarks"].ToString()) ? form["brokerRemarks"].ToString() : "";


                ServicesRequest.StateId = EServiceRequestSubmittedState;
                ServiceRequestDetails.status = EServiceRequestSubmittedState;
                ServiceRequestDetails.StateId = EServiceRequestDetailsSubmittedState;


                List<EServiceRequestsDetails> ServiceRequestDetailsList = new List<EServiceRequestsDetails>();

                ServiceRequestDetailsList.Add(ServiceRequestDetails);

                ServicesRequest.RequestsDetails = ServiceRequestDetailsList;


                dataTable = objdataclass.SubmitEservicesRequest(ServicesRequest);


                ViewBag.requestSubmitted = "Submit";
            }

            else if (form["saveExamRequestDraft"] != null &&
                     form["sumbitExamRequest"] == null)
            {

                if (String.IsNullOrWhiteSpace(form["brokerCivilId"]) &&
                    !String.IsNullOrWhiteSpace(form["brokerCivilId"]))
                {

                    RequestExistByCivilId.ServiceId = ServiceId;
                    RequestExistByCivilId.CivilId = form["brokerCivilId"].ToString();
                    RequestExistByCivilId.StateId = EServiceRequestCreatedState;

                    dataTable = objdataclass.GetRequestIfExistsByCivilId(RequestExistByCivilId);
                    String requestNumberToCheck = form["requestNo"].ToString();

                    if (dataTable.Rows.Count > 0 &&
                        requestNumberToCheck != dataTable.Rows[0]["EserviceRequestNumber"].ToString())
                    {
                        ViewBag.requestSubmitted = "Duplicated";

                        //return RedirectToAction("Index");
                        return View("Index");
                    }
                }

                


                ServicesRequest.EserviceRequestId = Int64.Parse(Session["EserviceRequestId"].ToString());

                ServicesRequest.EserviceRequestNumber = form["requestNo"].ToString();
                ServicesRequest.RequesterUserId = Int64.Parse(Session["UserId"].ToString());  //Session[];
                ServicesRequest.ServiceId = int.Parse(ServiceId);// 7; 

                ServicesRequest.DateCreated = DateTime.Now;
                ServicesRequest.CreatedBy = Session["UserId"].ToString();
                ServicesRequest.ModifiedBy = "";
                ServicesRequest.DateModified = null;
                ServicesRequest.OwnerOrgId = 0;
                ServicesRequest.OwnerLocId = 0;

                ServiceRequestDetails.RequestForUserType = Int64.Parse(Session["RequestForUserType"].ToString());
                ServiceRequestDetails.RequestServicesId = ServiceId;
                //ServiceRequestDetails.OrganizationId = ;

                String generalBrokerType = ConfigurationManager.AppSettings["generalBroker"].ToString();

                String subBrokerType = ConfigurationManager.AppSettings["subBroker"].ToString();
                String subMessangerType = ConfigurationManager.AppSettings["subMessanger"].ToString();

                String specialBrokerType = ConfigurationManager.AppSettings["specialBroker"].ToString();
                String specialMessangerType = ConfigurationManager.AppSettings["specialMessanger"].ToString();
                String directReleaseBrokerType = ConfigurationManager.AppSettings["directReleaseBroker"].ToString();


                ViewBag.brokerTy = Session["RequestForUserType"].ToString();//ServiceRequestDetails.RequestForUserType.ToString();

                if (Session["LegalEntity"].ToString() == "1" // Clearning Agent
                    && ServiceRequestDetails.RequestForUserType.ToString() != generalBrokerType)
                {
                    ServiceRequestDetails.RequesterLicenseNumber = form["generalbrokerLicenceNo"].ToString();
                    ServiceRequestDetails.RequesterArabicName = form["generalbrokerName"].ToString();
                    ServiceRequestDetails.MobileNumber = form["generalbrokerMobileNo"].ToString();
                    ServiceRequestDetails.RequesterEnglishName = form["generalbrokerName"].ToString();
                }
                else if (Session["LegalEntity"].ToString() == "2"
                         && ServiceRequestDetails.RequestForUserType.ToString() != generalBrokerType
                         && ServiceRequestDetails.RequestForUserType.ToString() != subBrokerType
                         && ServiceRequestDetails.RequestForUserType.ToString() != subMessangerType)
                {
                    ServiceRequestDetails.RequesterLicenseNumber = form["commercialRegistrationNo"].ToString();
                    ServiceRequestDetails.RequesterArabicName = form["companyName"].ToString();
                    ServiceRequestDetails.RequesterEnglishName = form["companyName"].ToString();
                }


                ServiceRequestDetails.ArabicFirstName = form["brokerNameArabicFirst"].ToString();
                ServiceRequestDetails.ArabicSecondName = form["brokerNameArabicSecond"].ToString();
                ServiceRequestDetails.ArabicThirdName = form["brokerNameArabicThird"].ToString();
                ServiceRequestDetails.ArabicLastName = form["brokerNameArabicLast"].ToString();

                ServiceRequestDetails.EnglishFirstName = form["brokerNameEnglishFirst"].ToString();
                ServiceRequestDetails.EnglishSecondName = !String.IsNullOrWhiteSpace(form["brokerNameEnglishSecond"].ToString()) ? form["brokerNameEnglishSecond"].ToString() : "";
                ServiceRequestDetails.EnglishThirdName = !String.IsNullOrWhiteSpace(form["brokerNameEnglishThird"].ToString()) ? form["brokerNameEnglishThird"].ToString() : "";
                ServiceRequestDetails.EnglishLastName = form["brokerNameEnglishLast"].ToString();
                // get the selected Nationality
                ServiceRequestDetails.Nationality = !String.IsNullOrWhiteSpace(form["selectedCountryVal"].ToString()) ? int.Parse(form["selectedCountryVal"].ToString()) : 5051;

                ServiceRequestDetails.CivilID = form["brokerCivilId"].ToString();

                if (form["brokerCivilIdexpiryDate"] != null)
                {
                    if (!String.IsNullOrWhiteSpace(form["brokerCivilIdexpiryDate"]))
                    {
                        ServiceRequestDetails.CivilIDExpiryDate = DateTime.ParseExact(form["brokerCivilIdexpiryDate"].ToString(), "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                    }
                }
                else
                {
                    ServiceRequestDetails.CivilIDExpiryDate = null;
                }


                ServiceRequestDetails.MobileNumber = form["brokerMobileNumber"].ToString();

                if (ServiceRequestDetails.RequestForUserType.ToString() != generalBrokerType)
                {
                    ServiceRequestDetails.PassportNo = form["brokerPassportNo"].ToString();
                }
                else
                {
                    ServiceRequestDetails.PassportNo = "";
                }

                if (ServiceRequestDetails.RequestForUserType.ToString() != generalBrokerType)
                {
                    ServiceRequestDetails.Gender = form["genderSelect"].ToString();

                    if (form["brokerPassportExpiryDate"] != null)
                    {
                        if (!String.IsNullOrWhiteSpace(form["brokerPassportExpiryDate"]))
                        {
                            ServiceRequestDetails.PassportExpiryDate = DateTime.ParseExact(form["brokerPassportExpiryDate"].ToString(), "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")); //null);
                        }
                    }
                }

                else
                {
                    ServiceRequestDetails.Gender = Session["genderOfLoggedInUser"] != null ? Session["genderOfLoggedInUser"].ToString() : "M";

                    ServiceRequestDetails.PassportExpiryDate = null;
                }


                ServiceRequestDetails.Address = !String.IsNullOrWhiteSpace(form["brokerAddress"].ToString()) ? form["brokerAddress"].ToString() : "";
                ServiceRequestDetails.Email = !String.IsNullOrWhiteSpace(form["brokerEmailID"].ToString()) ? form["brokerEmailID"].ToString() : "";

                ServiceRequestDetails.LicenseNumber = String.Empty;
                ServiceRequestDetails.LicenseNumberExpiryDate = null;

                ServiceRequestDetails.Remarks = !String.IsNullOrWhiteSpace(form["brokerRemarks"].ToString()) ? form["brokerRemarks"].ToString() : "";


                ServicesRequest.StateId = EServiceRequestCreatedState;
                ServiceRequestDetails.status = EServiceRequestCreatedState;
                ServiceRequestDetails.StateId = EServicesRequestDetailsCreatedState;


                List<EServiceRequestsDetails> ServiceRequestDetailsList = new List<EServiceRequestsDetails>();

                ServiceRequestDetailsList.Add(ServiceRequestDetails);

                ServicesRequest.RequestsDetails = ServiceRequestDetailsList;

                dataTable = objdataclass.SubmitEservicesRequest(ServicesRequest);
                var s = Session["ServiceId"] != null ? CommonFunctions.LogUserActivity(
"SubmitAExamRequest", "", "", "", Session["ServiceId"].ToString(), "RequestNumber+'" + form["requestNo"].ToString() + "'") : CommonFunctions.LogUserActivity(
"SubmitAExamRequest", "", "", "", "", "RequestNumber+'" + form["requestNo"].ToString() + "'");

                ViewBag.requestSubmitted = "Draft";

            }







            //return RedirectToAction("Index");
            return View("Index");


            //ServiceRequestsDetails.RejectionRemarks
            //ServiceRequestsDetails.RequestForVisit
            //ServiceRequestsDetails.RequestForVisitRemarks
            //ServiceRequestsDetails.ExamAddmissionId
            //ServiceRequestsDetails.ExamDetailsId

            //ServiceRequestsDetails.status
            //ServiceRequestsDetails.StateId
            //ServiceRequestsDetails.DateCreated
            //ServiceRequestsDetails.CreatedBy
            //ServiceRequestsDetails.ModifiedBy
            //ServiceRequestsDetails.DateModified
            //ServiceRequestsDetails.OwnerOrgId
            //ServiceRequestsDetails.OwnerLocId

        }





        //public ActionResult DeleteFile(String dataItem)
        //{
        //    if (Session["UserId"] == null)
        //    {
        //        return RedirectToAction("Start", "registration");
        //    }

        //    Dataclass objdataclass = new Dataclass();

        //    StringBuilder sb = new StringBuilder();
        //    String[] encrypteddata = dataItem.Split(',');
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
        //    objdelete.mUserid = Session["UserId"].ToString();
        //    objdelete.tokenId = Session["TokenId"].ToString();
        //    objdelete.sOrgReqDocId = sb.ToString();
        //    objdelete.EserviceRequestid = Session["EserviceRequestid"].ToString();

        //    String stratus = objdataclass.DocumentdeleteForEservice(objdelete);


        //    String s = sb.ToString();


        //    return new EmptyResult();

        //    // return Json(new { success = true, responseText = "Deleted Sucessfully!" }, JsonRequestBehavior.AllowGet);

        //}



        [HttpPost]
        public JsonResult DeleteFile(String dataItem)
        {
            //if (Session["UserId"] == null)
            //{
            //    return RedirectToAction("Start", "registration");
            //}

            try
            { 
            Dataclass objdataclass = new Dataclass();

            StringBuilder sb = new StringBuilder();
            String[] encrypteddata = dataItem.Split(',');
            ArrayList decryptedvalue = new ArrayList();
            foreach (var item in encrypteddata)
            {
                //dataItem = Decrypt(item);

                dataItem = CommonFunctions.CsUploadDecrypt(item);
                if (dataItem.All(char.IsDigit))
                {
                    sb.Append(dataItem);
                    // sb.Append(',' + dataItem);
                }
            }
            //  dropdownvalue = CommonFunctions.CsUploadDecrypt(dropdownvalue);

            OrgReqResultDocInfoParams objdelete = new OrgReqResultDocInfoParams();
            objdelete.mUserid = Session["UserId"].ToString();
            objdelete.tokenId = Session["TokenId"].ToString();
            objdelete.sOrgReqDocId = sb.ToString();
            objdelete.EserviceRequestid = Session["EserviceRequestid"].ToString();

            String stratus = objdataclass.DocumentdeleteForEservice(objdelete);



            String s = sb.ToString();
            //return Json(new { success = true, responseText = "posted" }, JsonRequestBehavior.AllowGet);
            string message = "SUCCESS";
//            return Json(new { Message = message, JsonRequestBehavior.AllowGet });

        return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }

            catch(Exception ex)
            {
                String _Exception = ex.ToString();
                Response.StatusCode = 500;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { message = "error" }, JsonRequestBehavior.AllowGet);

            }

            //    return Json(message, JsonRequestBehavior.AllowGet);
            // return json
            // return RedirectToAction("Index", "ClearingAgentExamRequest");
            //  return new EmptyResult();

            // return Json(new { success = true, responseText = "Deleted Sucessfully!" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DownloadFile(String fileName)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Start", "registration");
            }

            Dataclass objdataclass = new Dataclass();

            String NewFileName = String.Empty;
            String MyfileName = String.Empty;
            String strpath = String.Empty;
            String sPwd = "";


            String id = CommonFunctions.CsUploadDecrypt(fileName);
            if (id != "")
            {
                try
                {

                    OpenDocumentParams objDownloadFile = new OpenDocumentParams();
                    objDownloadFile.DocumentId = id;
                    objDownloadFile.EserviceRequestid = Session["Eservicerequestid"].ToString();
                    objDownloadFile.hiderefprofile = examRequestReferenceProfile;//"BRSExamDOCS";
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


                    String filename = MyfileName;
                    String filepath = strpath;
                    byte[] filedata = System.IO.File.ReadAllBytes(filepath);
                    String contentType = MimeMapping.GetMimeMapping(filepath);

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
                    //  WriteToLogFile(ex);
                    String _Exception = ex.ToString();
                }

            }
            ViewBag.Errormessage = "Error In downloading File";
            return View("Error");



        }


        public ActionResult ExamRequest()
        {
           // CheckSession();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Start", "registration");
            }

            // Added This to Check, if The User Has an Approved Company or Not
            // If User Doesn't Have Approved Company, We will Prevent the User from Request Exam
            if (Session["LegalEntity"].ToString() == "2")
            {
                SecurityParams objmyorglist = new SecurityParams();
                DataTable dataTableOrg = new DataTable();

                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();

                dataTableOrg = objdataclass.MyOrganizations_Data(objmyorglist);


                if (dataTableOrg.Rows.Count < 1)
                {
                    ViewBag.display = "block";
                    ViewBag.message = Resources.Resource.noApprovedOrganization;

                    return View();
                }
            }






            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExamRequest(String brokerTypeSelected)
        {
            CheckSession();


            // Added This to Check, if The User Has an Approved Company or Not
            // If User Doesn't Have Approved Company, We will Prevent the User from Request Exam
            if (Session["LegalEntity"].ToString() == "2")
            {
                SecurityParams objmyorglist = new SecurityParams();
                DataTable dataTableOrg = new DataTable();

                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();

                dataTableOrg = objdataclass.MyOrganizations_Data(objmyorglist);


                if (dataTableOrg.Rows.Count < 1)
                {
                    ViewBag.display = "block";
                    ViewBag.message = Resources.Resource.noApprovedOrganization;

                    return View();
                }

            }





            if (Session["examRequestViewMd"] != null)
            {
                Session.Remove("examRequestViewMd");
            }

            String generalBrokerTypeValue = ConfigurationManager.AppSettings["generalBroker"].ToString();
            DataTable dataTable = new DataTable();

            if (Session["UserId"] == null ||
                Session["LegalEntity"] == null)
            {
                return RedirectToAction("LogOut", "registration");
            }

            if (brokerTypeSelected != null)
            {
                String DecrptedSelectedBrokerType = CheckBrokerType(brokerTypeSelected);

                if (!String.IsNullOrEmpty(DecrptedSelectedBrokerType))
                {
                    if (DecrptedSelectedBrokerType == generalBrokerTypeValue)
                    {
                        RequestExists RequestExist = new RequestExists();

                        RequestExist.RequesterUserId = Session["UserId"].ToString();
                        RequestExist.ServiceId = ServiceId;
                        RequestExist.RequestForUserType = DecrptedSelectedBrokerType;

                        dataTable = objdataclass.GetRequestIfExists(RequestExist);

                        if (dataTable.Rows.Count > 0)
                        {
                            ViewBag.display = "block";
                            ViewBag.message = Resources.Resource.ExamRequestDuplicated;

                            return View();
                        }
                    }

                    else
                    {
                        if (Session["UserName"] == null ||
                            Session["LicenseNumberOfLoggedInUser"] == null ||
                            Session["mobileNumberOfLoggedInUser"] == null)
                        {
                            ViewBag.display = "block";
                            ViewBag.message = Resources.Resource.noLicenceNumberError + " " +
                                              Resources.Resource.MobileNumber + " " +
                                              Resources.Resource.UserName;

                            return View();
                        }


                        if (Session["LegalEntity"].ToString() == "2") // This is Organization
                        {
                            if (Session["LicenseNumberOfLoggedInUser"] == null ||
                                String.IsNullOrWhiteSpace(Session["LicenseNumberOfLoggedInUser"].ToString()))
                            {
                                ViewBag.display = "block";
                                ViewBag.message = Resources.Resource.noLicenceNumberError;

                                return View();
                            }
                        }
                    }



                    Session["RequestForUserType"] = DecrptedSelectedBrokerType;
                    return RedirectToAction("Index");
                }
            }

            return View();
        }



        private String CheckBrokerType(String brokerType)
        {
            String result = String.Empty;

            String generalBrokerTypeValue = ConfigurationManager.AppSettings["generalBroker"].ToString();
            String subBrokerTypeValue = ConfigurationManager.AppSettings["subBroker"].ToString();
            String subMessangerTypeValue = ConfigurationManager.AppSettings["subMessanger"].ToString();

            String specialBrokerTypeValue = ConfigurationManager.AppSettings["specialBroker"].ToString();
            String specialMessangerTypeValue = ConfigurationManager.AppSettings["specialMessanger"].ToString();
            String directReleaseBrokerTypeValue = ConfigurationManager.AppSettings["directReleaseBroker"].ToString();

            brokerType = CommonFunctions.CsUploadDecrypt(brokerType);

            if (brokerType == generalBrokerTypeValue ||
                brokerType == subBrokerTypeValue ||
                brokerType == subMessangerTypeValue ||
                brokerType == specialBrokerTypeValue ||
                brokerType == specialMessangerTypeValue ||
                brokerType == directReleaseBrokerTypeValue
                )
            {
                result = brokerType;
            }

            return result;
        }
    }
}
