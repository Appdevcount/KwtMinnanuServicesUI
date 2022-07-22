#region Code Backup 29 Dec
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using System.Web.Mvc;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    public class RequestController : MyBaseController //Controller
//    {
//        Dataclass objdataclass = new Dataclass();
//        public void WriteToLogFile(string inputvalue, string sessionDetails)
//        {
//            using (EventLog eventLog = new EventLog("Application"))
//            {
//                eventLog.Source = "Application";
//                eventLog.WriteEntry(sessionDetails + "Inpu" +
//                    "tValue>" + inputvalue, EventLogEntryType.Information, 101, 1);
//            }
//        }


//        // GET: Request
//        public ActionResult RequestListfortheUser()
//        {
//            try
//            {

//                TempData["data"] = null; //added Siraj - clearing the tempdata which was assigned in last request

//                if (Session["UserId"] == null)
//                {
//                    return RedirectToAction("Start", "registration");
//                }
//                EserviceRequest r = new EserviceRequest { RequesterUserId = int.Parse(Session["UserId"].ToString()), ESERVICEREQUESTNUMBER = "" };


//                String ExamServiceId = ConfigurationManager.AppSettings["ExamService"].ToString();
//                String RenewalServiceId = ConfigurationManager.AppSettings["RenewalService"].ToString();
//                String DeActivateServiceId = ConfigurationManager.AppSettings["DeActivateService"].ToString();
//                String TransferServiceId = ConfigurationManager.AppSettings["TransferService"].ToString();
//                String CancelServiceId = ConfigurationManager.AppSettings["CancelService"].ToString();
//                String IssuanceServiceId = ConfigurationManager.AppSettings["IssuanceService"].ToString();

//                String WhomItConcernsLetterServiceId = ConfigurationManager.AppSettings["WhomItConcernsLetterService"].ToString();
//                String PrintLostIdCardId = ConfigurationManager.AppSettings["PrintLostIdCard"].ToString();

//                String OrganizationRegistrationServiceId = ConfigurationManager.AppSettings["OrganizationRegistrationService"].ToString();
//                String subScriptionServiceId = ConfigurationManager.AppSettings["subScriptionService"].ToString();


//                DataTable dt = objdataclass.GETRequestListfortheUser(r);// ParentID, ChildUser);
//                GroupedRequests GR = new GroupedRequests();

//                var a = from x in dt.AsEnumerable()
//                        where x["RequestServicesId"].ToString() == subScriptionServiceId
//                        select new RequestList
//                        {
//                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["Status"].ToString(),
//                            StatusArabic = x["StatusArabic"].ToString(),
//                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

//                        };
//                GR.MultiSubscription = a.ToList();

//                DataTable dtmyorglist = new DataTable();
//                SecurityParams objmyorglist = new SecurityParams();
//                objmyorglist.mUserid = Session["UserId"].ToString();
//                objmyorglist.tokenId = Session["TokenId"].ToString();
//                dtmyorglist = objdataclass.OrgReqRequest_Status_Data(objmyorglist);

//                var b = from x in dtmyorglist.AsEnumerable()
//                        select new RequestList
//                        {
//                            EServiceRequestNumber = x["RequestNumber"].ToString(),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["StateName"].ToString(),
//                            StateId= x["StateId"].ToString(),
//                            IsMainCompany = x["IsMainCompany"].ToString(),
//                            Name= x["Name"].ToString(),
//                             OrganizationRequestId= x["OrganizationRequestId"].ToString(),
//                              RejectionRemarks= x["RejectionRemarks"].ToString()
//                              ,
//                            RequestForVisitRemarks= x["RequestForVisitRemarks"].ToString()
//                            //, StatusArabic = x["StateId"].ToString()

//                        };

//                //var b = from x in dt.AsEnumerable()
//                //        where x["RequestServicesId"].ToString() == OrganizationRegistrationServiceId
//                //        select new RequestList
//                //        {
//                //            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                //            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                //            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                //            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                //            DateCreated = x["DateCreated"].ToString(),
//                //            Status = x["Status"].ToString(),
//                //            StatusArabic = x["StatusArabic"].ToString(),
//                //            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

//                //        };
//                GR.OrganizationRegistrationRequests = b.ToList();
//                var c = from x in dt.AsEnumerable()
//                        where x["RequestServicesId"].ToString() == ExamServiceId
//                        select new RequestList
//                        {
//                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["Status"].ToString(),
//                            StatusArabic = x["StatusArabic"].ToString(),
//                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

//                        };
//                GR.ExamRequests = c.ToList();



//                var s = from x in dt.AsEnumerable()
//                        where x["RequestServicesId"].ToString() == RenewalServiceId
//                        select new RequestList
//                        {
//                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["Status"].ToString(),
//                            StatusArabic = x["StatusArabic"].ToString(),
//                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

//                        };
//                GR.BrokerRenewalRequests = s.ToList();
// var D = from x in dt.AsEnumerable()
//                        where x["RequestServicesId"].ToString() == DeActivateServiceId
//                        select new RequestList
//                        {
//                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["Status"].ToString(),
//                            StatusArabic = x["StatusArabic"].ToString(),
//                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

//                        };

//            //    if (D.ToList().Count != 0)
//                {
//                    GR.BrokerDeactivateRequests = D.ToList();

//                }
//                    var C = from x in dt.AsEnumerable()
//                        where x["RequestServicesId"].ToString() == CancelServiceId
//                        select new RequestList
//                        {
//                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["Status"].ToString(),
//                            StatusArabic = x["StatusArabic"].ToString(),
//                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

//                        };

//                    GR.BrokerCancelRequests = C.ToList();

//                var T = from x in dt.AsEnumerable()
//                        where x["RequestServicesId"].ToString() == TransferServiceId
//                        select new RequestList
//                        {
//                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["Status"].ToString(),
//                            StatusArabic = x["StatusArabic"].ToString(),
//                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

//                        };

//                    GR.BrokerTransferRequests = T.ToList();



//                var I = from x in dt.AsEnumerable()
//                        where x["RequestServicesId"].ToString() == IssuanceServiceId
//                        select new RequestList
//                        {
//                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["Status"].ToString(),
//                            StatusArabic = x["StatusArabic"].ToString(),
//                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

//                        };


//                    GR.BrokerIssuanceRequests = I.ToList();



//                var W = from x in dt.AsEnumerable()
//                        where x["RequestServicesId"].ToString() == WhomItConcernsLetterServiceId
//                        select new RequestList
//                        {
//                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["Status"].ToString(),
//                            StatusArabic = x["StatusArabic"].ToString(),
//                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

//                        };


//                    GR.BrokerToWhomItConcern = W.ToList();


//                var P = from x in dt.AsEnumerable()
//                        where x["RequestServicesId"].ToString() == PrintLostIdCardId
//                        select new RequestList
//                        {
//                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
//                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
//                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
//                            DateCreated = x["DateCreated"].ToString(),
//                            Status = x["Status"].ToString(),
//                            StatusArabic = x["StatusArabic"].ToString(),
//                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])


//                        };


//                    GR.BrokerPrintLostIdCard = P.ToList();



//                return View(GR);// s.ToList());
//            }
//            catch (Exception e)
//            {
//                WriteToLogFile(e.Message, "RequestListfortheUser");
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.Msg = Resources.Resource.somethingwentwrong;
//                return View();
//            }
//        }
//        public ActionResult RequestDetailsfortheRequest(String EServiceRequestNumber)
//        {
//            try
//            {
//                EServiceRequestNumber = CommonFunctions.CsUploadDecrypt(EServiceRequestNumber);

//                if (EServiceRequestNumber == "")
//                {

//                    string source = Request.RawUrl.ToString();
//                    string split = "RequestNumber=";


//                    string result = source.Substring(source.IndexOf(split) + split.Length);


//                    EServiceRequestNumber = CommonFunctions.CsUploadDecrypt(result);




//                }
//                if (Session["UserId"] == null)
//                {
//                    return RedirectToAction("Start", "registration");
//                }
//                EserviceRequest r = new EserviceRequest { RequesterUserId = int.Parse(Session["UserId"].ToString()), ESERVICEREQUESTNUMBER = EServiceRequestNumber };

//                DataTable dt = objdataclass.GETRequestDetailsfortheRequest(r);

//                RequestDetailMain rm;

//                if (!EServiceRequestNumber.Contains("ORG"))
//                {
//                    var s = from x in dt.AsEnumerable()
//                            select new RequestDetail
//                            {
//                                EServiceRequestDetailsID = Convert.ToInt32(x["EServiceRequestDetailsID"]),
//                                RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
//                                //RequestForUserID = System.Convert.IsDBNull(x["RequestForUserID"])?0: Convert.ToInt32(x["RequestForUserID"]),
//                                //DateModified = Convert.ToDateTime(System.Convert.IsDBNull(x["DateModified"]) ? null : x["DateModified"]),
//                                Status = System.Convert.IsDBNull(x["Status"]) ? "" : x["Status"].ToString(),

//                                StatusArabic = System.Convert.IsDBNull(x["StatusArabic"]) ? "" : x["StatusArabic"].ToString(),

//                                ServiceNameEng = System.Convert.IsDBNull(x["ServiceNameEng"]) ? "" : x["ServiceNameEng"].ToString(),
//                                ServiceNameAra = System.Convert.IsDBNull(x["ServiceNameAra"]) ? "" : x["ServiceNameAra"].ToString(),

//                                RequestForUserType = System.Convert.IsDBNull(x["RequestForUserType"]) ? "" : x["RequestForUserType"].ToString(),
//                                RequestServicesId = System.Convert.IsDBNull(x["RequestServicesId"]) ? "" : x["RequestServicesId"].ToString(),
//                                OrganizationId = System.Convert.IsDBNull(x["OrganizationId"]) ? "" : x["OrganizationId"].ToString(),
//                                RequesterLicenseNumber = System.Convert.IsDBNull(x["RequesterLicenseNumber"]) ? "" : x["RequesterLicenseNumber"].ToString(),
//                                RequesterArabicName = System.Convert.IsDBNull(x["RequesterArabicName"]) ? "" : x["RequesterArabicName"].ToString(),
//                                RequesterEnglishName = System.Convert.IsDBNull(x["RequesterEnglishName"]) ? "" : x["RequesterEnglishName"].ToString(),
//                                Remarks = System.Convert.IsDBNull(x["Remarks"]) ? "" : x["Remarks"].ToString(),
//                                RejectionRemarks = System.Convert.IsDBNull(x["RejectionRemarks"]) ? "" : x["RejectionRemarks"].ToString(),
//                                RequestForVisit = System.Convert.IsDBNull(x["RequestForVisit"]) ? "" : x["RequestForVisit"].ToString(),
//                                RequestForVisitRemarks = System.Convert.IsDBNull(x["RequestForVisitRemarks"]) ? "" : x["RequestForVisitRemarks"].ToString(),
//                                ExamAddmissionId = System.Convert.IsDBNull(x["ExamAddmissionId"]) ? "" : x["ExamAddmissionId"].ToString(),
//                                ExamDetailsId = System.Convert.IsDBNull(x["ExamDetailsId"]) ? "" : x["ExamDetailsId"].ToString(),
//                                OwnerOrgId = System.Convert.IsDBNull(x["OwnerOrgId"]) ? "" : x["OwnerOrgId"].ToString(),
//                                OwnerLocId = System.Convert.IsDBNull(x["OwnerLocId"]) ? "" : x["OwnerLocId"].ToString(),
//                                RequestForUserId = System.Convert.IsDBNull(x["RequestForUserId"]) ? "" : x["RequestForUserId"].ToString(),
//                                RequestForName = System.Convert.IsDBNull(x["RequestForName"]) ? "" : x["RequestForName"].ToString(),
//                                RequestForEmail = System.Convert.IsDBNull(x["RequestForEmail"]) ? "" : x["RequestForEmail"].ToString().ToString(),
//                                EserviceRequestStatusArabic = System.Convert.IsDBNull(x["EserviceRequestStatusArabic"]) ? "" : x["EserviceRequestStatusArabic"].ToString().ToString(),
//                                EserviceRequestStatusEnglish = System.Convert.IsDBNull(x["EserviceRequestStatusEnglish"]) ? "" : x["EserviceRequestStatusEnglish"].ToString().ToString()
//                            };


//                    List<RequestDetail> RD = new List<RequestDetail>();
//                    RD = s.ToList();

//                    rm = new RequestDetailMain()
//                    {
//                        ReqDetail = RD,//s.ToList() ,
//                        EServiceRequestID = Convert.ToInt32(dt.Rows[0]["EServiceRequestID"].ToString()),
//                        EServiceRequestNumber = dt.Rows[0]["EServiceRequestNumber"].ToString(),
//                        //DateCreated = Convert.ToDateTime(System.Convert.IsDBNull(dt.Rows[0]["DateCreated"]) ? null : dt.Rows[0]["DateCreated"]),
//                        DateCreated = System.Convert.IsDBNull(dt.Rows[0]["DateCreated"]) ? null : dt.Rows[0]["DateCreated"].ToString(),
//                        ArabicFirstName = System.Convert.IsDBNull(dt.Rows[0]["ArabicFirstName"]) ? "" : dt.Rows[0]["ArabicFirstName"].ToString(),
//                        ArabicSecondName = System.Convert.IsDBNull(dt.Rows[0]["ArabicSecondName"]) ? "" : dt.Rows[0]["ArabicSecondName"].ToString(),
//                        ArabicThirdName = System.Convert.IsDBNull(dt.Rows[0]["ArabicThirdName"]) ? "" : dt.Rows[0]["ArabicThirdName"].ToString(),
//                        ArabicLastName = System.Convert.IsDBNull(dt.Rows[0]["ArabicLastName"]) ? "" : dt.Rows[0]["ArabicLastName"].ToString(),
//                        EnglishFirstName = System.Convert.IsDBNull(dt.Rows[0]["EnglishFirstName"]) ? "" : dt.Rows[0]["EnglishFirstName"].ToString(),
//                        EnglishSecondName = System.Convert.IsDBNull(dt.Rows[0]["EnglishSecondName"]) ? "" : dt.Rows[0]["EnglishSecondName"].ToString(),
//                        EnglishThirdName = System.Convert.IsDBNull(dt.Rows[0]["EnglishThirdName"]) ? "" : dt.Rows[0]["EnglishThirdName"].ToString(),
//                        EnglishLastName = System.Convert.IsDBNull(dt.Rows[0]["EnglishLastName"]) ? "" : dt.Rows[0]["EnglishLastName"].ToString(),
//                        Nationality = System.Convert.IsDBNull(dt.Rows[0]["Nationality"]) ? "" : dt.Rows[0]["Nationality"].ToString(),
//                        CivilID = System.Convert.IsDBNull(dt.Rows[0]["CivilID"]) ? "" : dt.Rows[0]["CivilID"].ToString(),
//                        CivilIDExpiryDate = System.Convert.IsDBNull(dt.Rows[0]["CivilIDExpiryDate"]) ? "" : dt.Rows[0]["CivilIDExpiryDate"].ToString(),
//                        MobileNumber = System.Convert.IsDBNull(dt.Rows[0]["MobileNumber"]) ? "" : dt.Rows[0]["MobileNumber"].ToString(),
//                        PassportNo = System.Convert.IsDBNull(dt.Rows[0]["PassportNo"]) ? "" : dt.Rows[0]["PassportNo"].ToString(),
//                        PassportExpiryDate = System.Convert.IsDBNull(dt.Rows[0]["PassportExpiryDate"]) ? "" : dt.Rows[0]["PassportExpiryDate"].ToString(),
//                        Address = System.Convert.IsDBNull(dt.Rows[0]["Address"]) ? "" : dt.Rows[0]["Address"].ToString(),
//                        Email = System.Convert.IsDBNull(dt.Rows[0]["Email"]) ? "" : dt.Rows[0]["Email"].ToString(),
//                        LicenseNumber = System.Convert.IsDBNull(dt.Rows[0]["LicenseNumber"]) ? "" : dt.Rows[0]["LicenseNumber"].ToString(),
//                        LicenseNumberExpiryDate = System.Convert.IsDBNull(dt.Rows[0]["LicenseNumberExpiryDate"]) ? "" : dt.Rows[0]["LicenseNumberExpiryDate"].ToString(),
//                        CreatedBy = System.Convert.IsDBNull(dt.Rows[0]["CreatedBy"]) ? "" : dt.Rows[0]["CreatedBy"].ToString(),
//                        BrokerName = System.Convert.IsDBNull(dt.Rows[0]["BrokerNameAra"]) ? "" : dt.Rows[0]["BrokerNameAra"].ToString(),
//                        BrokerType = System.Convert.IsDBNull(dt.Rows[0]["BrokerType"]) ? "" : dt.Rows[0]["BrokerType"].ToString(),
//                        RequestedOrganizations = System.Convert.IsDBNull(dt.Rows[0]["AssociatedOrgs"]) ? "" : dt.Rows[0]["AssociatedOrgs"].ToString(),

//                         //added by azhar 
//                        RejectionRemarks = System.Convert.IsDBNull(dt.Rows[0]["RejectionRemarks"]) ? "" : dt.Rows[0]["RejectionRemarks"].ToString()
//                    };
//                }
//                else
//                {
//                    rm = new RequestDetailMain();
//                }

//                return View(rm);
//            }
//            catch (Exception e)
//            {
//                WriteToLogFile(e.Message, "RequestDetailsfortheRequest");
//                ViewBag.modelerror = Constantclass.number5;
//                ViewBag.Msg = Resources.Resource.somethingwentwrong;
//                return View();
//            }
//        }


//        public ActionResult ConfirmAttendance(String eServiceRequestId)
//        {
//            String confirmationState = ConfigurationManager.AppSettings["ExamAttendanceConfirmedState"].ToString();

//            String RequestId = CommonFunctions.CsUploadDecrypt(eServiceRequestId);

//            Dataclass objdataclass = new Dataclass();

//            ExamCandidateInfo ExamCandidateInfo = new ExamCandidateInfo();

//            ExamCandidateInfo.EserviceRequestId = RequestId;
//            ExamCandidateInfo.stateId = confirmationState;

//            DataTable dataTable =  objdataclass.ConfirmExamAttendance(ExamCandidateInfo);

//            return RedirectToAction("RequestListfortheUser");
//        }
//    }
//}
#endregion Code Backup 29 Dec



using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RequestController : MyBaseController //Controller
    {
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


        // GET: Request
        public ActionResult RequestListfortheUser()
        {
            try
            {

                TempData["data"] = null; //added Siraj - clearing the tempdata which was assigned in last request

                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }



                String ExamServiceId = ConfigurationManager.AppSettings["ExamService"].ToString();
                String RenewalServiceId = ConfigurationManager.AppSettings["RenewalService"].ToString();
                String DeActivateServiceId = ConfigurationManager.AppSettings["DeActivateService"].ToString();
                String TransferServiceId = ConfigurationManager.AppSettings["TransferService"].ToString();
                String CancelServiceId = ConfigurationManager.AppSettings["CancelService"].ToString();
                String IssuanceServiceId = ConfigurationManager.AppSettings["IssuanceService"].ToString();

                String WhomItConcernsLetterServiceId = ConfigurationManager.AppSettings["WhomItConcernsLetterService"].ToString();
                String PrintLostIdCardId = ConfigurationManager.AppSettings["PrintLostIdCard"].ToString();

                String OrganizationRegistrationServiceId = ConfigurationManager.AppSettings["OrganizationRegistrationService"].ToString();
                String subScriptionServiceId = ConfigurationManager.AppSettings["subScriptionService"].ToString();




                String BrsPrintingCancelLicense = ConfigurationManager.AppSettings["BrsPrintingCancelLicense"].ToString();
                String BrsPrintingIssueLicense = ConfigurationManager.AppSettings["BrsPrintingIssueLicense"].ToString();
                String BrsPrintingChangeLicenseAddress = ConfigurationManager.AppSettings["BrsPrintingChangeLicenseAddress"].ToString();
                String BrsPrintingChangeLicenseActivity = ConfigurationManager.AppSettings["BrsPrintingChangeLicenseActivity"].ToString();
                String BrsPrintingReleaseBankGuarantee = ConfigurationManager.AppSettings["BrsPrintingReleaseBankGuarantee"].ToString();
                String BrsPrintingGoodBehave = ConfigurationManager.AppSettings["BrsPrintingGoodBehave"].ToString();
                String BrsPrintingRenewLicense = ConfigurationManager.AppSettings["BrsPrintingRenewLicense"].ToString();
                String BrsPrintingChangeJobTitleRenewResidency = ConfigurationManager.AppSettings["BrsPrintingChangeJobTitleRenewResidency"].ToString();
                String BrsPrintingChangeJobTitleTransferResidency = ConfigurationManager.AppSettings["BrsPrintingChangeJobTitleTransferResidency"].ToString();
                String BrsPrintingRenewResidency = ConfigurationManager.AppSettings["BrsPrintingRenewResidency"].ToString();
                String BrsPrintingTransferResidency = ConfigurationManager.AppSettings["BrsPrintingTransferResidency"].ToString();
                String BrsPrintingChangeJobTitle = ConfigurationManager.AppSettings["BrsPrintingChangeJobTitle"].ToString();
                String BrsPrintingChangeJobTitleCivil = ConfigurationManager.AppSettings["BrsPrintingChangeJobTitleCivil"].ToString();
                String BrsPrintingDeActivateLicenseDeath = ConfigurationManager.AppSettings["BrsPrintingDeActivateLicenseDeath"].ToString();



                EserviceRequest r = new EserviceRequest { RequesterUserId = int.Parse(Session["UserId"].ToString()), ESERVICEREQUESTNUMBER = "" };
                WriteToLogFile("got data ", "RequestListfortheUser");

                DataTable dt = objdataclass.GETRequestListfortheUser(r);// ParentID, ChildUser);
                GroupedRequests GR = new GroupedRequests();


                #region Service Subscription
                var a = from x in dt.AsEnumerable()
                        where x["RequestServicesId"].ToString() == subScriptionServiceId
                        select new RequestList
                        {
                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["Status"].ToString(),
                            StatusArabic = x["StatusArabic"].ToString(),
                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

                        };
                GR.MultiSubscription = a.ToList();
                #endregion Service Subscription

                ///  WriteToLogFile("1 ", "RequestListfortheUser");
                #region Organization Requests
                DataTable dtmyorglist = new DataTable();
                SecurityParams objmyorglist = new SecurityParams();
                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();
                dtmyorglist = objdataclass.OrgReqRequest_Status_Data(objmyorglist);

                var b = from x in dtmyorglist.AsEnumerable()
                        select new RequestList
                        {
                            EServiceRequestNumber = x["RequestNumber"].ToString(),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["StateName"].ToString(),
                            StateId = x["StateId"].ToString(),
                            IsMainCompany = x["IsMainCompany"].ToString(),
                            Name = x["Name"].ToString(),
                            OrganizationRequestId = x["OrganizationRequestId"].ToString(),
                            RejectionRemarks = x["RejectionRemarks"].ToString()
                              ,
                            RequestForVisitRemarks = x["RequestForVisitRemarks"].ToString()
                            //, StatusArabic = x["StateId"].ToString()

                        };

                GR.OrganizationRegistrationRequests = b.ToList();

                #endregion  Organization Requests


                //  WriteToLogFile("2 ", "RequestListfortheUser");
             /*
                
                #region Inspection Appointment Requests
             


                 if (Session["LegalEntity"] != null && Session["UserId"] != null &&
                     Session["UserId"] != null && Session["UserOrgID"] != null)
                 {
                     ReqObj ro = new ReqObj { Action = Session["LegalEntity"].ToString(), CommonData = Session["UserId"].ToString(), CommonData2 = Session["UserId"].ToString(), OrgID = Session["UserOrgID"].ToString() };

                     DataTable GetInspectionAppointments = objdataclass.GetInspectionAppointments(ro);

                     List<InspectionAppointment> InspectionAppointments = new List<InspectionAppointment>();

                     bool EnglishCulturei = false;

                     if (Request.Cookies["culture"] != null)
                     {
                         EnglishCulturei = Request.Cookies["culture"].Value.Contains("en");
                     }

                     //string culturei = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                     //bool EnglishCulturei = culturei.Contains("English");

                     var ia = from x in GetInspectionAppointments.AsEnumerable()
                              select new InspectionAppointment
                              {
                                  AppointmentId = x["EInspectionAppointmentRequestId"].ToString(),
                                  RequestNumber = x["RequestNumber"].ToString(),
                                  DateCreated = x["DateCreated"].ToString(),
                                  DeclarationId = x["DeclarationId"].ToString(),
                                  TempDeclarationId = x["TempDeclarationId"].ToString(),
                                  DeclarationType = EnglishCulturei ? x["DeclarationType"].ToString() : x["DeclarationTypeAra"].ToString(),
                                  PortId = EnglishCulturei ? x["PortId"].ToString() : x["PortIdArabic"].ToString(),
                                  InspectionZone = x["InspectionZone"].ToString(),
                                  InspectionTerminal = x["InspectionTerminal"].ToString(),
                                  InspectionRound = x["InspectionRoundId"].ToString(),
                                  InspectionDate = x["InspectionDate"].ToString(),
                                  SelectedVehicleList = x["SelectedVehicleList"].ToString(),
                                  Status = x["Status"].ToString(),//=="1"? AppointmentBookedStatus:AppointmentCompletedStatus,
                                  StatusNumber = x["Status"].ToString(),
                                  VehiclePlateNumber = x["vehicleplatenumber"].ToString(),

                                  //Added To Print in Request Printout: 28 March 
                                  DriverNameToPrint = x["DriverName"].ToString(),
                                  DriverMobileNumberToPrint = x["MobileNumber"].ToString(),
                                  ContainerNumberToPrint = x["ContainerNumber"].ToString(),
                                  WeightToPrint = x["Weight"].ToString(),
                                  PortName = EnglishCulturei ? x["PortId"].ToString() : x["PortIdArabic"].ToString(),
                                  DeclarationNumberToPrint = x["decnumber"].ToString(),
                                  //Till Here

                                  EditableRequest = Convert.ToBoolean(Convert.ToInt16(x["EditableRequest"].ToString()))
                              };




                     InspectionAppointments = ia.ToList();


                     var groupeddeclresult = (from appnts in InspectionAppointments
                                              group appnts by appnts.DeclarationId into groupeddecl
                                              orderby groupeddecl.Key

                                              select new declgroup
                                              {
                                                  DeclarationId = groupeddecl.Key,
                                                  VehiclesAppointment = groupeddecl.Select(x => new InspectionAppointment
                                                  {
                                                      AppointmentId = x.AppointmentId,
                                                      RequestNumber = x.RequestNumber,
                                                      DateCreated = x.DateCreated,
                                                      DeclarationId = x.DeclarationId,
                                                      TempDeclarationId = x.TempDeclarationId,
                                                      DeclarationType = x.DeclarationType,
                                                      PortId = x.PortId,
                                                      InspectionZone = x.InspectionZone,
                                                      InspectionTerminal = x.InspectionTerminal,
                                                      InspectionRound = x.InspectionRound,
                                                      InspectionDate = x.InspectionDate,
                                                      SelectedVehicleList = x.SelectedVehicleList,
                                                      Status = x.Status, //== "1" ? AppointmentBookedStatus : AppointmentCompletedStatus,
                                                      StatusNumber = x.Status,
                                                      EditableRequest = x.EditableRequest,
                                                      VehiclePlateNumber = x.VehiclePlateNumber,

                                                      //Added To Print in Request Printout: 28 March 
                                                      DriverNameToPrint = x.DriverNameToPrint,
                                                      DriverMobileNumberToPrint = x.DriverMobileNumberToPrint,
                                                      ContainerNumberToPrint = x.ContainerNumberToPrint,
                                                      WeightToPrint = x.WeightToPrint,
                                                      PortName = x.PortId,
                                                      DeclarationNumberToPrint = x.DeclarationNumberToPrint
                                                  }).ToList()
                                              }).ToList();

                     List<declgroup> groupeddeclresulttyped = new List<declgroup>();
                     foreach (declgroup d in groupeddeclresult)
                     {
                         groupeddeclresulttyped.Add(d);
                     }

                     GR.InspectionAppointments = groupeddeclresulttyped;
                 }





               
    
                #endregion  Inspection Appointment Requests
    */
                #region Exam Requests
                var c = from x in dt.AsEnumerable()
                        where x["RequestServicesId"].ToString() == ExamServiceId
                        select new RequestList
                        {
                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["Status"].ToString(),
                            StatusArabic = x["StatusArabic"].ToString(),
                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"]),
                            RejectionRemarks = x["RejectionRemarks"].ToString()
                        };
                GR.ExamRequests = c.ToList();
                #endregion Exam Requests



                #region Renewal Requests
                var s = from x in dt.AsEnumerable()
                        where x["RequestServicesId"].ToString() == RenewalServiceId
                        select new RequestList
                        {
                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["Status"].ToString(),
                            StatusArabic = x["StatusArabic"].ToString(),
                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                            ,
                            RejectionRemarks = x["RejectionRemarks"].ToString()

                        };
                GR.BrokerRenewalRequests = s.ToList();

                #endregion Renewal Requests


                #region DeActivation Requests
                var D = from x in dt.AsEnumerable()
                        where x["RequestServicesId"].ToString() == DeActivateServiceId
                        select new RequestList
                        {
                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["Status"].ToString(),
                            StatusArabic = x["StatusArabic"].ToString(),
                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                            ,
                            RejectionRemarks = x["RejectionRemarks"].ToString()
                        };

                GR.BrokerDeactivateRequests = D.ToList();
                #endregion DeActivation Requests


                #region Cancelation Requests
                var C = from x in dt.AsEnumerable()
                        where x["RequestServicesId"].ToString() == CancelServiceId
                        select new RequestList
                        {
                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["Status"].ToString(),
                            StatusArabic = x["StatusArabic"].ToString(),
                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                            ,
                            RejectionRemarks = x["RejectionRemarks"].ToString()
                        };

                GR.BrokerCancelRequests = C.ToList();
                #endregion  Cancelation Requests

                #region Transfer Requests
                var T = from x in dt.AsEnumerable()
                        where x["RequestServicesId"].ToString() == TransferServiceId
                        select new RequestList
                        {
                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["Status"].ToString(),
                            StatusArabic = x["StatusArabic"].ToString(),
                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                            ,
                            RejectionRemarks = x["RejectionRemarks"].ToString()
                        };

                GR.BrokerTransferRequests = T.ToList();
                #endregion Transfer Requests


                //   WriteToLogFile("3 ", "RequestListfortheUser");
                #region Issuance Request
                var I = from x in dt.AsEnumerable()
                        where x["RequestServicesId"].ToString() == IssuanceServiceId
                        select new RequestList
                        {
                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["Status"].ToString(),
                            StatusArabic = x["StatusArabic"].ToString(),
                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                            ,
                            RejectionRemarks = x["RejectionRemarks"].ToString()
                        };


                GR.BrokerIssuanceRequests = I.ToList();
                #endregion Issuance Request



                #region To Whom it May Concern Requests

                var W = from x in dt.AsEnumerable()
                        where x["RequestServicesId"].ToString() == WhomItConcernsLetterServiceId
                        select new RequestList
                        {
                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["Status"].ToString(),
                            StatusArabic = x["StatusArabic"].ToString(),
                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                            ,
                            RejectionRemarks = x["RejectionRemarks"].ToString()
      				 //added newly by azhar for print
                            , BrokerName=x["BrokerNameAra"].ToString()
                            , GenBrokerName=x["GenBrokerName"].ToString()
                            , BrokerType=x["BrokerType"].ToString()
                            ,LicenseNumber=x["BrokerLicenseNumber"].ToString()
                        };


                GR.BrokerToWhomItConcern = W.ToList();

                #endregion To Whom it May Concern Requests


                #region Print Lost Card Request
                var P = from x in dt.AsEnumerable()
                        where x["RequestServicesId"].ToString() == PrintLostIdCardId
                        select new RequestList
                        {
                            EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                            EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                            RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                            RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                            DateCreated = x["DateCreated"].ToString(),
                            Status = x["Status"].ToString(),
                            StatusArabic = x["StatusArabic"].ToString(),
                            RequestServicesId = Convert.ToInt32(x["RequestServicesId"])

                            ,
                            RejectionRemarks = x["RejectionRemarks"].ToString()
                        };


                GR.BrokerPrintLostIdCard = P.ToList();
                #endregion  Print Lost Card Request




                //  WriteToLogFile("4 ", "RequestListfortheUser");


                // 05/DECEMBER/2019

                #region New Broker Printing  


                #region BrsPrintingCancelLicense
                var PrintingCancelLicense = from x in dt.AsEnumerable()
                                            where x["RequestServicesId"].ToString() == BrsPrintingCancelLicense
                                            select new RequestList
                                            {
                                                EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                DateCreated = x["DateCreated"].ToString(),
                                                Status = x["Status"].ToString(),
                                                StatusArabic = x["StatusArabic"].ToString(),
                                                RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                ,
                                                RejectionRemarks = x["RejectionRemarks"].ToString()
                                            };

                GR.BrsPrintingCancelLicense = PrintingCancelLicense.ToList();
                #endregion BrsPrintingCancelLicense

                #region BrsPrintingIssueLicense
                var PrintingIssueLicense = from x in dt.AsEnumerable()
                                           where x["RequestServicesId"].ToString() == BrsPrintingIssueLicense
                                           select new RequestList
                                           {
                                               EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                               EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                               RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                               RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                               DateCreated = x["DateCreated"].ToString(),
                                               Status = x["Status"].ToString(),
                                               StatusArabic = x["StatusArabic"].ToString(),
                                               RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                               ,
                                               RejectionRemarks = x["RejectionRemarks"].ToString()
                                           };

                GR.BrsPrintingIssueLicense = PrintingIssueLicense.ToList();
                #endregion BrsPrintingIssueLicense

                #region BrsPrintingChangeLicenseAddress
                var PrintingChangeLicenseAddress = from x in dt.AsEnumerable()
                                                   where x["RequestServicesId"].ToString() == BrsPrintingChangeLicenseAddress
                                                   select new RequestList
                                                   {
                                                       EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                       EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                       RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                       RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                       DateCreated = x["DateCreated"].ToString(),
                                                       Status = x["Status"].ToString(),
                                                       StatusArabic = x["StatusArabic"].ToString(),
                                                       RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                       ,
                                                       RejectionRemarks = x["RejectionRemarks"].ToString()
                                                   };

                GR.BrsPrintingChangeLicenseAddress = PrintingChangeLicenseAddress.ToList();
                #endregion BrsPrintingChangeLicenseAddress

                #region BrsPrintingChangeLicenseActivity
                var PrintingChangeLicenseActivity = from x in dt.AsEnumerable()
                                                    where x["RequestServicesId"].ToString() == BrsPrintingChangeLicenseActivity
                                                    select new RequestList
                                                    {
                                                        EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                        EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                        RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                        RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                        DateCreated = x["DateCreated"].ToString(),
                                                        Status = x["Status"].ToString(),
                                                        StatusArabic = x["StatusArabic"].ToString(),
                                                        RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                        ,
                                                        RejectionRemarks = x["RejectionRemarks"].ToString()
                                                    };

                GR.BrsPrintingChangeLicenseActivity = PrintingChangeLicenseActivity.ToList();
                #endregion BrsPrintingChangeLicenseActivity

                #region BrsPrintingReleaseBankGuarantee
                var PrintingReleaseBankGuarantee = from x in dt.AsEnumerable()
                                                   where x["RequestServicesId"].ToString() == BrsPrintingReleaseBankGuarantee
                                                   select new RequestList
                                                   {
                                                       EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                       EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                       RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                       RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                       DateCreated = x["DateCreated"].ToString(),
                                                       Status = x["Status"].ToString(),
                                                       StatusArabic = x["StatusArabic"].ToString(),
                                                       RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                       ,
                                                       RejectionRemarks = x["RejectionRemarks"].ToString()
                                                   };

                GR.BrsPrintingReleaseBankGuarantee = PrintingReleaseBankGuarantee.ToList();
                #endregion BrsPrintingReleaseBankGuarantee

                #region BrsPrintingGoodBehave
                var PrintingGoodBehave = from x in dt.AsEnumerable()
                                         where x["RequestServicesId"].ToString() == BrsPrintingGoodBehave
                                         select new RequestList
                                         {
                                             EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                             EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                             RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                             RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                             DateCreated = x["DateCreated"].ToString(),
                                             Status = x["Status"].ToString(),
                                             StatusArabic = x["StatusArabic"].ToString(),
                                             RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                             ,
                                             RejectionRemarks = x["RejectionRemarks"].ToString()
                                         };

                GR.BrsPrintingGoodBehave = PrintingGoodBehave.ToList();
                #endregion BrsPrintingGoodBehave

                #region BrsPrintingRenewLicense
                var PrintingRenewLicense = from x in dt.AsEnumerable()
                                           where x["RequestServicesId"].ToString() == BrsPrintingRenewLicense
                                           select new RequestList
                                           {
                                               EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                               EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                               RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                               RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                               DateCreated = x["DateCreated"].ToString(),
                                               Status = x["Status"].ToString(),
                                               StatusArabic = x["StatusArabic"].ToString(),
                                               RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                               ,
                                               RejectionRemarks = x["RejectionRemarks"].ToString()
                                           };

                GR.BrsPrintingRenewLicense = PrintingRenewLicense.ToList();
                #endregion BrsPrintingRenewLicense

                #region BrsPrintingChangeJobTitleRenewResidency
                var PrintingChangeJobTitleRenewResidency = from x in dt.AsEnumerable()
                                                           where x["RequestServicesId"].ToString() == BrsPrintingChangeJobTitleRenewResidency
                                                           select new RequestList
                                                           {
                                                               EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                               EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                               RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                               RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                               DateCreated = x["DateCreated"].ToString(),
                                                               Status = x["Status"].ToString(),
                                                               StatusArabic = x["StatusArabic"].ToString(),
                                                               RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                               ,
                                                               RejectionRemarks = x["RejectionRemarks"].ToString()
                                                           };

                GR.BrsPrintingChangeJobTitleRenewResidency = PrintingChangeJobTitleRenewResidency.ToList();
                #endregion BrsPrintingChangeJobTitleRenewResidency

                #region BrsPrintingChangeJobTitleTransferResidency
                var PrintingChangeJobTitleTransferResidency = from x in dt.AsEnumerable()
                                                              where x["RequestServicesId"].ToString() == BrsPrintingChangeJobTitleTransferResidency
                                                              select new RequestList
                                                              {
                                                                  EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                                  EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                                  RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                                  RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                                  DateCreated = x["DateCreated"].ToString(),
                                                                  Status = x["Status"].ToString(),
                                                                  StatusArabic = x["StatusArabic"].ToString(),
                                                                  RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                                  ,
                                                                  RejectionRemarks = x["RejectionRemarks"].ToString()
                                                              };

                GR.BrsPrintingChangeJobTitleTransferResidency = PrintingChangeJobTitleTransferResidency.ToList();
                #endregion BrsPrintingChangeJobTitleTransferResidency

                #region BrsPrintingRenewResidency
                var PrintingRenewResidency = from x in dt.AsEnumerable()
                                             where x["RequestServicesId"].ToString() == BrsPrintingRenewResidency
                                             select new RequestList
                                             {
                                                 EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                 EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                 RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                 RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                 DateCreated = x["DateCreated"].ToString(),
                                                 Status = x["Status"].ToString(),
                                                 StatusArabic = x["StatusArabic"].ToString(),
                                                 RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                 ,
                                                 RejectionRemarks = x["RejectionRemarks"].ToString()
                                             };

                GR.BrsPrintingRenewResidency = PrintingRenewResidency.ToList();
                #endregion BrsPrintingRenewResidency

                #region BrsPrintingTransferResidency
                var PrintingTransferResidency = from x in dt.AsEnumerable()
                                                where x["RequestServicesId"].ToString() == BrsPrintingTransferResidency
                                                select new RequestList
                                                {
                                                    EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                    EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                    RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                    RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                    DateCreated = x["DateCreated"].ToString(),
                                                    Status = x["Status"].ToString(),
                                                    StatusArabic = x["StatusArabic"].ToString(),
                                                    RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                    ,
                                                    RejectionRemarks = x["RejectionRemarks"].ToString()
                                                };

                GR.BrsPrintingTransferResidency = PrintingTransferResidency.ToList();
                #endregion BrsPrintingTransferResidency

                #region BrsPrintingChangeJobTitle
                var PrintingChangeJobTitle = from x in dt.AsEnumerable()
                                             where x["RequestServicesId"].ToString() == BrsPrintingChangeJobTitle
                                             select new RequestList
                                             {
                                                 EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                 EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                 RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                 RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                 DateCreated = x["DateCreated"].ToString(),
                                                 Status = x["Status"].ToString(),
                                                 StatusArabic = x["StatusArabic"].ToString(),
                                                 RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                 ,
                                                 RejectionRemarks = x["RejectionRemarks"].ToString()
                                             };

                GR.BrsPrintingChangeJobTitle = PrintingChangeJobTitle.ToList();
                #endregion BrsPrintingChangeJobTitle

                #region BrsPrintingChangeJobTitleCivil
                var PrintingChangeJobTitleCivil = from x in dt.AsEnumerable()
                                                  where x["RequestServicesId"].ToString() == BrsPrintingChangeJobTitleCivil
                                                  select new RequestList
                                                  {
                                                      EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                      EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                      RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                      RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                      DateCreated = x["DateCreated"].ToString(),
                                                      Status = x["Status"].ToString(),
                                                      StatusArabic = x["StatusArabic"].ToString(),
                                                      RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                      ,
                                                      RejectionRemarks = x["RejectionRemarks"].ToString()
                                                  };

                GR.BrsPrintingChangeJobTitleCivil = PrintingChangeJobTitleCivil.ToList();
                #endregion BrsPrintingChangeJobTitleCivil


                #region BrsPrintingDeActivateLicenseDeath
                var PrintingDeActivateLicenseDeath = from x in dt.AsEnumerable()
                                                     where x["RequestServicesId"].ToString() == BrsPrintingDeActivateLicenseDeath
                                                     select new RequestList
                                                     {
                                                         EServiceRequestID = Convert.ToInt32(x["EServiceRequestID"]),
                                                         EServiceRequestNumber = x["EServiceRequestNumber"].ToString(),
                                                         RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                                         RequestForUserID = Convert.ToInt32(System.Convert.IsDBNull(x["RequestForUserID"]) ? 0 : x["RequestForUserID"]),
                                                         DateCreated = x["DateCreated"].ToString(),
                                                         Status = x["Status"].ToString(),
                                                         StatusArabic = x["StatusArabic"].ToString(),
                                                         RequestServicesId = Convert.ToInt32(x["RequestServicesId"])
                                                         ,
                                                         RejectionRemarks = x["RejectionRemarks"].ToString()
                                                     };

                GR.BrsPrintingDeActivateLicenseDeath = PrintingDeActivateLicenseDeath.ToList();
                #endregion BrsPrintingDeActivateLicenseDeath



                //  WriteToLogFile("5 ", "RequestListfortheUser");

                #endregion New Broker Printing 


                return View(GR);// s.ToList());
            }
            catch (Exception e)
            {
                //CommonFunctions.LogUserActivity("RequestList", "", "", "", "", e.Message.ToString()+e.StackTrace.ToString());
                WriteToLogFile(e.Message.ToString(), "RequestListfortheUser");
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        public ActionResult RequestDetailsfortheRequest(String EServiceRequestNumber)
        {
            try
            {
                EServiceRequestNumber = CommonFunctions.CsUploadDecrypt(EServiceRequestNumber);

                if (EServiceRequestNumber == "")
                {

                    string source = Request.RawUrl.ToString();
                    string split = "RequestNumber=";


                    string result = source.Substring(source.IndexOf(split) + split.Length);


                    EServiceRequestNumber = CommonFunctions.CsUploadDecrypt(result);




                }
                EserviceRequest r;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                if (Session["UserId"] != null)
                {
                    r = new EserviceRequest { RequesterUserId = int.Parse(Session["UserId"].ToString()), ESERVICEREQUESTNUMBER = EServiceRequestNumber };
                }
                else
                {
                    r = new EserviceRequest { ESERVICEREQUESTNUMBER = EServiceRequestNumber };

                }
                DataTable dt = objdataclass.GETRequestDetailsfortheRequest(r);

                RequestDetailMain rm;

                if (!EServiceRequestNumber.Contains("ORG"))
                {
                    var s = from x in dt.AsEnumerable()
                            select new RequestDetail
                            {
                                EServiceRequestDetailsID = Convert.ToInt32(x["EServiceRequestDetailsID"]),
                                RequesterUserID = Convert.ToInt32(x["RequesterUserID"]),
                                //RequestForUserID = System.Convert.IsDBNull(x["RequestForUserID"])?0: Convert.ToInt32(x["RequestForUserID"]),
                                //DateModified = Convert.ToDateTime(System.Convert.IsDBNull(x["DateModified"]) ? null : x["DateModified"]),
                                Status = System.Convert.IsDBNull(x["Status"]) ? "" : x["Status"].ToString(),

                                StatusArabic = System.Convert.IsDBNull(x["StatusArabic"]) ? "" : x["StatusArabic"].ToString(),

                                ServiceNameEng = System.Convert.IsDBNull(x["ServiceNameEng"]) ? "" : x["ServiceNameEng"].ToString(),
                                ServiceNameAra = System.Convert.IsDBNull(x["ServiceNameAra"]) ? "" : x["ServiceNameAra"].ToString(),

                                RequestForUserType = System.Convert.IsDBNull(x["RequestForUserType"]) ? "" : x["RequestForUserType"].ToString(),
                                RequestServicesId = System.Convert.IsDBNull(x["RequestServicesId"]) ? "" : x["RequestServicesId"].ToString(),
                                OrganizationId = System.Convert.IsDBNull(x["OrganizationId"]) ? "" : x["OrganizationId"].ToString(),
                                RequesterLicenseNumber = System.Convert.IsDBNull(x["RequesterLicenseNumber"]) ? "" : x["RequesterLicenseNumber"].ToString(),
                                RequesterArabicName = System.Convert.IsDBNull(x["RequesterArabicName"]) ? "" : x["RequesterArabicName"].ToString(),
                                RequesterEnglishName = System.Convert.IsDBNull(x["RequesterEnglishName"]) ? "" : x["RequesterEnglishName"].ToString(),
                                Remarks = System.Convert.IsDBNull(x["Remarks"]) ? "" : x["Remarks"].ToString(),
                                RejectionRemarks = System.Convert.IsDBNull(x["RejectionRemarks"]) ? "" : x["RejectionRemarks"].ToString(),
                                RequestForVisit = System.Convert.IsDBNull(x["RequestForVisit"]) ? "" : x["RequestForVisit"].ToString(),
                                RequestForVisitRemarks = System.Convert.IsDBNull(x["RequestForVisitRemarks"]) ? "" : x["RequestForVisitRemarks"].ToString(),
                                ExamAddmissionId = System.Convert.IsDBNull(x["ExamAddmissionId"]) ? "" : x["ExamAddmissionId"].ToString(),
                                ExamDetailsId = System.Convert.IsDBNull(x["ExamDetailsId"]) ? "" : x["ExamDetailsId"].ToString(),
                                OwnerOrgId = System.Convert.IsDBNull(x["OwnerOrgId"]) ? "" : x["OwnerOrgId"].ToString(),
                                OwnerLocId = System.Convert.IsDBNull(x["OwnerLocId"]) ? "" : x["OwnerLocId"].ToString(),
                                RequestForUserId = System.Convert.IsDBNull(x["RequestForUserId"]) ? "" : x["RequestForUserId"].ToString(),
                                RequestForName = System.Convert.IsDBNull(x["RequestForName"]) ? "" : x["RequestForName"].ToString(),
                                RequestForEmail = System.Convert.IsDBNull(x["RequestForEmail"]) ? "" : x["RequestForEmail"].ToString().ToString(),
                                EserviceRequestStatusArabic = System.Convert.IsDBNull(x["EserviceRequestStatusArabic"]) ? "" : x["EserviceRequestStatusArabic"].ToString().ToString(),
                                EserviceRequestStatusEnglish = System.Convert.IsDBNull(x["EserviceRequestStatusEnglish"]) ? "" : x["EserviceRequestStatusEnglish"].ToString().ToString()
                            };


                    List<RequestDetail> RD = new List<RequestDetail>();
                    RD = s.ToList();

                    rm = new RequestDetailMain()
                    {
                        ReqDetail = RD,//s.ToList() ,
                        EServiceRequestID = Convert.ToInt32(dt.Rows[0]["EServiceRequestID"].ToString()),
                        EServiceRequestNumber = dt.Rows[0]["EServiceRequestNumber"].ToString(),
                        //DateCreated = Convert.ToDateTime(System.Convert.IsDBNull(dt.Rows[0]["DateCreated"]) ? null : dt.Rows[0]["DateCreated"]),
                        DateCreated = System.Convert.IsDBNull(dt.Rows[0]["DateCreated"]) ? null : dt.Rows[0]["DateCreated"].ToString(),
                        ArabicFirstName = System.Convert.IsDBNull(dt.Rows[0]["ArabicFirstName"]) ? "" : dt.Rows[0]["ArabicFirstName"].ToString(),
                        ArabicSecondName = System.Convert.IsDBNull(dt.Rows[0]["ArabicSecondName"]) ? "" : dt.Rows[0]["ArabicSecondName"].ToString(),
                        ArabicThirdName = System.Convert.IsDBNull(dt.Rows[0]["ArabicThirdName"]) ? "" : dt.Rows[0]["ArabicThirdName"].ToString(),
                        ArabicLastName = System.Convert.IsDBNull(dt.Rows[0]["ArabicLastName"]) ? "" : dt.Rows[0]["ArabicLastName"].ToString(),
                        EnglishFirstName = System.Convert.IsDBNull(dt.Rows[0]["EnglishFirstName"]) ? "" : dt.Rows[0]["EnglishFirstName"].ToString(),
                        EnglishSecondName = System.Convert.IsDBNull(dt.Rows[0]["EnglishSecondName"]) ? "" : dt.Rows[0]["EnglishSecondName"].ToString(),
                        EnglishThirdName = System.Convert.IsDBNull(dt.Rows[0]["EnglishThirdName"]) ? "" : dt.Rows[0]["EnglishThirdName"].ToString(),
                        EnglishLastName = System.Convert.IsDBNull(dt.Rows[0]["EnglishLastName"]) ? "" : dt.Rows[0]["EnglishLastName"].ToString(),
                        Nationality = System.Convert.IsDBNull(dt.Rows[0]["Nationality"]) ? "" : dt.Rows[0]["Nationality"].ToString(),
                        CivilID = System.Convert.IsDBNull(dt.Rows[0]["CivilID"]) ? "" : dt.Rows[0]["CivilID"].ToString(),
                        CivilIDExpiryDate = System.Convert.IsDBNull(dt.Rows[0]["CivilIDExpiryDate"]) ? "" : dt.Rows[0]["CivilIDExpiryDate"].ToString(),
                        MobileNumber = System.Convert.IsDBNull(dt.Rows[0]["MobileNumber"]) ? "" : dt.Rows[0]["MobileNumber"].ToString(),
                        PassportNo = System.Convert.IsDBNull(dt.Rows[0]["PassportNo"]) ? "" : dt.Rows[0]["PassportNo"].ToString(),
                        PassportExpiryDate = System.Convert.IsDBNull(dt.Rows[0]["PassportExpiryDate"]) ? "" : dt.Rows[0]["PassportExpiryDate"].ToString(),
                        Address = System.Convert.IsDBNull(dt.Rows[0]["Address"]) ? "" : dt.Rows[0]["Address"].ToString(),
                        Email = System.Convert.IsDBNull(dt.Rows[0]["Email"]) ? "" : dt.Rows[0]["Email"].ToString(),
                        LicenseNumber = System.Convert.IsDBNull(dt.Rows[0]["LicenseNumber"]) ? "" : dt.Rows[0]["LicenseNumber"].ToString(),
                        LicenseNumberExpiryDate = System.Convert.IsDBNull(dt.Rows[0]["LicenseNumberExpiryDate"]) ? "" : dt.Rows[0]["LicenseNumberExpiryDate"].ToString(),
                        CreatedBy = System.Convert.IsDBNull(dt.Rows[0]["CreatedBy"]) ? "" : dt.Rows[0]["CreatedBy"].ToString(),
                        BrokerName = System.Convert.IsDBNull(dt.Rows[0]["BrokerNameAra"]) ? "" : dt.Rows[0]["BrokerNameAra"].ToString(),
                        BrokerType = System.Convert.IsDBNull(dt.Rows[0]["BrokerType"]) ? "" : dt.Rows[0]["BrokerType"].ToString(),
                        RequestedOrganizations = System.Convert.IsDBNull(dt.Rows[0]["AssociatedOrgs"]) ? "" : dt.Rows[0]["AssociatedOrgs"].ToString(),

                        //added by azhar 
                        RejectionRemarks = System.Convert.IsDBNull(dt.Rows[0]["RejectionRemarks"]) ? "" : dt.Rows[0]["RejectionRemarks"].ToString()
                    };
                }
                else
                {
                    rm = new RequestDetailMain();
                }

                return View(rm);
            }
            catch (Exception e)
            {
                WriteToLogFile(e.Message, "RequestDetailsfortheRequest");
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }


        public ActionResult ConfirmAttendance(String eServiceRequestId)
        {
            String confirmationState = ConfigurationManager.AppSettings["ExamAttendanceConfirmedState"].ToString();

            String RequestId = CommonFunctions.CsUploadDecrypt(eServiceRequestId);

            Dataclass objdataclass = new Dataclass();

            ExamCandidateInfo ExamCandidateInfo = new ExamCandidateInfo();

            ExamCandidateInfo.EserviceRequestId = RequestId;
            ExamCandidateInfo.stateId = confirmationState;

            DataTable dataTable = objdataclass.ConfirmExamAttendance(ExamCandidateInfo);

            return RedirectToAction("RequestListfortheUser");
        }





        [HttpPost]
        public JsonResult DeleteRequest(String requestNumber)
        {

            String status = "-1";

            if (Session["UserId"] == null)
            {
                return Json(new { message = status }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                Dictionary<String, String> requestToUpdate = new Dictionary<String, String>();
                String deletedRequestState = ConfigurationManager.AppSettings["EServiceRequestDeletedState"].ToString();
                String deletedRequestDetailsSate = ConfigurationManager.AppSettings["EServiceRequestDetailsDeletedState"].ToString();


                if (!String.IsNullOrWhiteSpace(requestNumber))
                {
                    requestNumber = requestNumber.Replace("request", "");
                    requestNumber = CommonFunctions.CsUploadDecrypt(requestNumber);

                    requestToUpdate.Add("serviceRequestNumber", requestNumber);
                    requestToUpdate.Add("EServiceRequestStateid", deletedRequestState);
                    requestToUpdate.Add("EServiceRequestDetailsStateid", deletedRequestDetailsSate);


                    Dataclass objdataclass = new Dataclass();

                    DataSet dataSet = objdataclass.updateRequest(requestToUpdate);

                    String requestState = String.Empty;
                    String requestDetailsState = String.Empty;

                    if (dataSet.Tables.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            requestState = dataSet.Tables[0].Rows[0]["EServiceRequests_StateID"].ToString();
                            requestDetailsState = dataSet.Tables[0].Rows[0]["EServiceRequestsDetails_StateID"].ToString();

                            if (requestState == deletedRequestState &&
                                requestDetailsState == deletedRequestDetailsSate)
                            {
                                status = "1";
                            }
                        }
                    }
                }

                return Json(new { message = status, JsonRequestBehavior.AllowGet });
            }

            catch (Exception ex)
            {
                String _Exception = ex.ToString();
                Response.StatusCode = 500;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { message = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}




