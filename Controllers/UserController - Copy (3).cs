using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class UserController : MyBaseController
    {
        // GET: User
        Dataclass objdataclass = new Dataclass();
        // Dataclass objdataclass;

        //String a = "";



        List<OrgRequestlist> objOrgRequestdatalist = new List<OrgRequestlist>();
        OrgRequestlist objOrgRequestlist = new OrgRequestlist();
        Data objData = new Data();
        public ActionResult Index()
        {
            return View();
        }






        public void WriteToLogFile(string inputvalue, string sessionDetails)
        {

            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(sessionDetails + "Inpu" +
                    "tValue>" + inputvalue, EventLogEntryType.Information, 101, 1);
            }

        }




        [HttpGet]
        public JsonResult GetNotifications()
        {
            try
            {
                //TempData.Clear();
                List<NotificationsList> objnotifylist = new List<NotificationsList>();
                if (Session["UserId"] == null)
                {
                    return Json(objnotifylist, JsonRequestBehavior.AllowGet);
                }
                Session["Nfcount"] = Constantclass.number;
                SecurityParams objmyorglist = new SecurityParams();
                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();

                if (TempData["Notifylist"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["Notifylist"];
                    TempData["Notifylist"] = objOrgRequestdatalist;
                }
                else
                {
                    objOrgRequestdatalist = objdataclass.Notifylist(objmyorglist);
                }

                NotificationsList ObjOrgGetBasicResult = new NotificationsList();
                if (objOrgRequestdatalist[0].Data != null)
                {
                    if (objOrgRequestdatalist[0].Data.NotificationsList != null)
                    {
                        foreach (var item in objOrgRequestdatalist[0].Data.NotificationsList)
                        {
                            if (item.ReffId != null)
                            {
                                item.ReffIdencry = encrypt.Encode(item.ReffId);
                            }
                            objnotifylist.Add(item);
                        }
                    }
                    else
                    {
                        NotificationsList ObjOrgGetBasicResultemty = new NotificationsList();
                        objnotifylist.Add(ObjOrgGetBasicResultemty);
                    }

                }
                else
                {
                    NotificationsList ObjOrgGetBasicResultemty = new NotificationsList();
                    objnotifylist.Add(ObjOrgGetBasicResultemty);
                }

                return Json(objnotifylist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult ShowNotifications()
        {
            List<NotificationsList> objnotifylist = new List<NotificationsList>();

            try
            {

                if (Session["UserId"] == null)
                {
                    return RedirectToAction("LogOut", "registration");
                }
                SecurityParams objmyorglist = new SecurityParams();
                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();


                objOrgRequestdatalist = objdataclass.Notifylist(objmyorglist);


                NotificationsList ObjOrgGetBasicResult = new NotificationsList();
                if (objOrgRequestdatalist != null && objOrgRequestdatalist.Count > 0)//added 13082019 , if there is no notification, the object is null and cause excepion while accesing
                {
                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.NotificationsList != null)
                        {
                            foreach (var item in objOrgRequestdatalist[0].Data.NotificationsList)
                            {
                                if (item.ReffId != null)
                                {
                                    item.ReffIdencry = encrypt.Encode(item.ReffId);
                                }
                                objnotifylist.Add(item);
                            }
                        }
                        else
                        {
                            NotificationsList ObjOrgGetBasicResultemty = new NotificationsList();
                            objnotifylist.Add(ObjOrgGetBasicResultemty);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(objnotifylist);
        }



        public ActionResult MyOrganizations()
        {
            checksession();

            try
            {
                TempData["data"] = null; //added Siraj - clearing the tempdata which was assigned in last request

                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                if (Session["LegalEntity"] != null)
                {
                    if (Session["LegalEntity"].ToString() != "2")
                    {
                        return RedirectToAction("UnAuthorizedAction", "Registration");
                    }
                }
                Session["returnurl"] = null;
                TempData.Clear();
                DataTable dtmyorglist = new DataTable();
                DataSet dtnotifycount = new DataSet();
                SecurityParams objmyorglist = new SecurityParams();
                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();
                dtnotifycount = objdataclass.NotifyCount(objmyorglist);
                if (dtnotifycount.Tables.Count > 0)
                {
                    if (dtnotifycount.Tables.Count > 1)
                    {
                        Session["Nfcount"] = dtnotifycount.Tables[1].Rows[0]["NotificationsCount"].ToString();
                    }
                    else
                    {
                        Session["Nfcount"] = Constantclass.number;
                    }
                }
                else
                {
                    Session["Nfcount"] = Constantclass.number;
                }
                dtmyorglist = objdataclass.MyOrganizations_Data(objmyorglist);
                checksession();
                CommonFunctions.LogUserActivity("MyOrganizations", "", "", "", "", "");
                return View(dtmyorglist);
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;

                CommonFunctions.LogUserActivity("MyOrganizations", "", "", "", "", e.Message.ToString());

                return View();
            }
        }

        private bool OrgRegistrationAllowed()
        {
            if (Session["LegalEntity"] != null)
            {
                if (Session["LegalEntity"].ToString() == "2")
                {
                    return Convert.ToBoolean(Session["OrgRegistrationAllowed"]);
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


        public ActionResult Org_Registration(string Id, string Requesttype, string purpose, string reqnum, bool NewRequest = false) ///all null first time

        {

            OrgGetBasicResult ObjOrgGetBasicResult = new OrgGetBasicResult();
            Session["ImporterSerialNum"] = 0;
            Session["AuthorizedSignatorySerialNum"] = 0;
            string mypurpose = "";
            string MyrequestType = "";
            string Myid = "";
            try
            {


                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                // handle the update case 
                if (purpose != null)
                {

                    mypurpose = encrypt.Decode(purpose);
                }
                else
                {
                    mypurpose = string.Empty;
                }


                if (mypurpose == "edit" || mypurpose == "")//added Siraj - To get Organization legal address and basic details entered during user registration[""-newrequest,"edit"-editingrequestofneworUpdate]
                {
                    if (NewRequest)//added Siraj - clearing the tempdata and OrgReqId which was assigned in last request
                    {
                        TempData["data"] = null; //added Siraj - clearing the tempdata which was assigned in last request
                        Id = null;
                    }
                    string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                    bool EnglishCulture = culture.Contains("English");

                    ObjOrgGetBasicResult.State = Session["GovernorateAra"].ToString();
                    ObjOrgGetBasicResult.City = Session["RegionAra"].ToString();
                    ObjOrgGetBasicResult.StateBiling = EnglishCulture ? Session["GovernorateEng"].ToString() : Session["GovernorateAra"].ToString();
                    ObjOrgGetBasicResult.CityBiling = EnglishCulture ? Session["RegionEng"].ToString() : Session["RegionAra"].ToString();
                    ObjOrgGetBasicResult.PostalCode = Session["PostalCode"].ToString();
                    ObjOrgGetBasicResult.Address = Session["Address"].ToString();
                    ObjOrgGetBasicResult.Block = Session["Block"].ToString();
                    ObjOrgGetBasicResult.Street = Session["Street"].ToString();
                    ObjOrgGetBasicResult.isIndustrial = Convert.ToBoolean(Session["IsIndustrial"]);
                    ObjOrgGetBasicResult.ResidenceNo = Session["CompanyCivilID"].ToString();
                }


                if (Id != null)
                {
                    double num;
                    if (!(double.TryParse(Id, out num)))
                    {
                        Myid = encrypt.Decode(Id);
                    }

                }


                if (!OrgRegistrationAllowed())
                    return RedirectToAction("UnAuthorizedAction", "Registration");


                int UserId = int.Parse(Session["UserId"].ToString());
                ReqObj r = new ReqObj { ParentID = UserId, ChildUser = 0, Action = mypurpose, OrgID = Myid };
                DataTable dt = objdataclass.CanCreateOrg(r);
                bool CanCreateOrg = Convert.ToBoolean(dt.Rows[0]["CanCreateOrg"].ToString());
                bool MainOrgApproved = Convert.ToBoolean(dt.Rows[0]["MainOrgApproved"].ToString());
                bool OrgExistsForUpdate = Convert.ToBoolean(dt.Rows[0]["OrgRequestExistForUpdate"].ToString());



                if (Id == null && Requesttype == null && purpose == null && reqnum == null && !CanCreateOrg && NewRequest)
                {
                    //ViewBag.NotAllowedToByPassForOrgCreation = "0";
                    return RedirectToAction("UnAuthorizedAction", "Registration");//, new { MsgCode = 1 });//remoed msgcode whic displays - you cannot create  org untill main org is approved.. now the message is generic - you do not have permission to access this feature
                }
                if (Requesttype != null)
                {
                    MyrequestType = encrypt.Decode(Requesttype);
                }


                if (OrgExistsForUpdate && MyrequestType == "created")
                {
                    return RedirectToAction("UnAuthorizedAction", "Registration", new { MsgCode = 2 });

                }
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                    ObjOrgGetBasicResult = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0];//added newly with rewrite
                    if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany)
                    {
                        ViewBag.CompanyType = "Main";
                    }
                    else
                    {
                        ViewBag.CompanyType = "Sub";
                    }
                }


                ViewBag.modelerror = Constantclass.number1;

                //Session["returnurl1"] = null;
                Session["requestnumber"] = null;
                if (reqnum != null)
                {
                    double num1;
                    if (!(double.TryParse(reqnum, out num1)))
                    {
                        reqnum = encrypt.Decode(reqnum);
                        Session["requestnumber"] = reqnum;
                    }
                }

                if (Id != null)
                {
                    double num;
                    if (!(double.TryParse(Id, out num)))
                    {
                        Id = encrypt.Decode(Id);
                    }
                    Session["UploadOrgId"] = Id;
                }
                else
                {
                    Session["purpose"] = null;
                }

                if (Requesttype != null)
                {
                    Requesttype = encrypt.Decode(Requesttype);
                }
                if (purpose != null)
                {
                    Session["purpose"] = null;
                    purpose = encrypt.Decode(purpose);
                }
                else
                {
                    purpose = string.Empty;
                }

                if (Session["purpose"] != null)
                {
                    if (Session["purpose"].ToString().Trim() == Constantclass.number)
                    {
                        ViewBag.dis = Constantclass.number;
                    }
                    else
                    {
                        ViewBag.dis = Constantclass.number1;
                    }
                }
                else
                {
                    if (purpose == "view")
                    {
                        Session["purpose"] = Constantclass.number;
                        ViewBag.dis = Constantclass.number;
                    }
                    else
                    {
                        Session["purpose"] = Constantclass.number1;
                        ViewBag.dis = Constantclass.number1;
                    }
                }


                string OrganizationId = Id;
                //List<OrgGetBasicResult> objlist = new List<OrgGetBasicResult>();
                DeclarationSearchByIdParams objOrgreg = new DeclarationSearchByIdParams();
                //OrgGetBasicResult ObjOrgGetBasicResult = new OrgGetBasicResult();
                if (Id == null && Requesttype == null)
                {
                    TempData["data"] = null;
                    //objlist.Add(ObjOrgGetBasicResult);


                    if (!MainOrgApproved && CanCreateOrg)
                    {
                        ViewBag.CompanyType = "Main"; // all params are null and able to reach this block then its the first request for org creation
                        ObjOrgGetBasicResult.TradeLicNumber = Session["LicenseNumber"].ToString();
                        ObjOrgGetBasicResult.IsmainCompany = true;
                        //objlist[0].IsmainCompany = true;
                    }
                    else
                    {
                        ViewBag.CompanyType = "Sub";
                        ObjOrgGetBasicResult.IsmainCompany = false;
                        //objlist[0].IsmainCompany = false;
                    }
                }
                else
                {
                    objOrgreg.mUserid = Session["UserId"].ToString();
                    objOrgreg.tokenId = Session["TokenId"].ToString();
                    objOrgreg.OrganizationId = OrganizationId;
                    objOrgreg.sOrgReqId = OrganizationId;
                    if (TempData["data"] != null)
                    {
                        //objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];//commented siraj Rewrite
                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId != 0)
                        {

                            ViewBag.reqno = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].RequestNumber.ToString();//added to show req number in first page of org reg
                        }
                    }
                    else
                    {
                        if (purpose == "view")
                        {
                            objOrgreg.ApprovedDetail = true;

                        }
                        else if (purpose == "edit")
                        {
                            objOrgreg.ApprovedDetail = false;
                        }

                        objOrgRequestdatalist = objdataclass.OrgRegistration_Data(objOrgreg, Requesttype);


                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany)
                        {
                            ViewBag.CompanyType = "Main";
                        }
                        else
                        {
                            ViewBag.CompanyType = "Sub";
                        }
                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId != 0)
                        {

                            ViewBag.reqno = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].RequestNumber.ToString();//added to show req number in first page of org reg
                        }

                    }
                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult != null)
                        {
                            ObjOrgGetBasicResult = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0];

                            //========Added 27-10-2019 for displaying state city as bilingual field in eservices request view, but will always save in arabic============

                            string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;
                            bool EnglishCulture = culture.Contains("English");
                            ObjOrgGetBasicResult.State = Session["GovernorateAra"].ToString();
                            ObjOrgGetBasicResult.City = Session["RegionAra"].ToString();
                            ObjOrgGetBasicResult.StateBiling = EnglishCulture ? Session["GovernorateEng"].ToString() : Session["GovernorateAra"].ToString();
                            ObjOrgGetBasicResult.CityBiling = EnglishCulture ? Session["RegionEng"].ToString() : Session["RegionAra"].ToString();
                            objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].State = ObjOrgGetBasicResult.State;
                            objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].City = ObjOrgGetBasicResult.City;
                            objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].StateBiling = ObjOrgGetBasicResult.StateBiling;
                            objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].CityBiling = ObjOrgGetBasicResult.CityBiling;

                            //====================

                            //objlist.Add(ObjOrgGetBasicResult);
                            if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].Editable == Constantclass.number)
                            {
                                Session["UploadOrgId"] = ObjOrgGetBasicResult.OrganizationRequestId;//added 18-07-2019 , when the view screen is navigated back to first page , the Uplload orgid is set to OrgId instead of OrgReqId.. This caused empty upload page when tried to retrive uploaded docs with orgid.. so just added this line to assign uploadorgid value to orgreqid
                                Session["requestnumber"] = ObjOrgGetBasicResult.RequestNumber;//added 18-07-2019 , when the view screen is navigated back to first page , somehow this view bag is not set and so not display for navigating next again.  so reassigning here
                                ViewBag.reqno = Session["requestnumber"].ToString();//added 18-07-2019 , when the view screen is navigated back to first page , somehow this view bag is not set and so not display for navigating next again.  so reassigning here
                                if (Requesttype != null)
                                {
                                    if (Requesttype == "Status")
                                    {
                                        Session["purpose"] = Constantclass.number;
                                        ViewBag.dis = Constantclass.number;
                                    }
                                }

                            }
                        }
                        else
                        {
                            //objlist.Add(ObjOrgGetBasicResult);
                        }
                    }
                    else
                    {
                        //objlist.Add(ObjOrgGetBasicResult);
                    }
                    //TempData["data"] = objOrgRequestdatalist;// moved these 2 lines outside of the block
                    //TempData.Keep("data");
                }
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");

                ObjOrgGetBasicResult.CompanyType = ObjOrgGetBasicResult.IsmainCompany ? "Main" : "Sub";
                //objlist[0].CompanyType = objlist[0].IsmainCompany ? "Main" : "Sub";
                //ViewData["data"] = objlist;

                //if (Session["returnurl"] == null)
                //{
                //    if (Requesttype == null)
                //    {
                //        Requesttype = "";
                //    }
                //    if (Requesttype == "Status")
                //    {
                //        Session["returnurl"] = "Org_RequestStatus";
                //    }
                //    else
                //    {
                //        Session["returnurl"] = "MyOrganizations";
                //    }
                //}

                //checksession();

                if (TempData["data"] != null)//to avoid update of first page details in Org Update Request. This block will overwrite the display option - Siraj
                {
                    List<OrgRequestlist> orgRequestlists3 = new List<OrgRequestlist>();
                    orgRequestlists3 = (List<OrgRequestlist>)TempData["data"];
                    if (orgRequestlists3.Count > 0 && Session["purpose"].ToString().Trim() == Constantclass.number1 && orgRequestlists3[0].Data.OrgGetBasicResult[0].OrganizationId != 0) //to avoid update of first page details in Org Update Request. This block will overwrite the display option - Siraj
                    {
                        //Session["purpose"] = Constantclass.number;
                        ViewBag.dis = Constantclass.number;
                    }
                    if (orgRequestlists3.Count == 0)
                    {
                        TempData["data"] = null;
                    }
                }

                ////adding below block newly in first org registration form page - 15-07-2019 - During first org creation on visiting this page and also when there is no data for the unfilled forms for old requests , just assigning a empty collection object property for Data class - This is to avoid exception in accesing collecting object in subsequent pages - This happened in Importer license details page 
                //if (TempData["data"] != null)//to avoid update of first page details in Org Update Request. This block will overwrite the display option - Siraj
                //{
                //    List<OrgRequestlist> orgRequestlists4 = new List<OrgRequestlist>();
                //    orgRequestlists4 = (List<OrgRequestlist>)TempData["data"];
                //    if (orgRequestlists4.Count > 0 && Session["purpose"].ToString().Trim() == Constantclass.number1) //ensuring this is editable form
                //    {
                //        if (orgRequestlists4[0].Data.EPaymentRequestDetails == null)
                //            orgRequestlists4[0].Data.EPaymentRequestDetails = new List<EPaymentRequestDetails>();
                //        if (orgRequestlists4[0].Data.EPaymentRequestInfo == null)
                //            orgRequestlists4[0].Data.EPaymentRequestInfo = new List<EPaymentRequestInfo>();
                //        if (orgRequestlists4[0].Data.ListOrgRequests == null)
                //            orgRequestlists4[0].Data.ListOrgRequests = new List<ListOrgRequests>();
                //        if (orgRequestlists4[0].Data.NotificationsList == null)
                //            orgRequestlists4[0].Data.NotificationsList = new List<NotificationsList>();
                //        if (orgRequestlists4[0].Data.OrgAuthorizedSignatories == null)
                //            orgRequestlists4[0].Data.OrgAuthorizedSignatories = new List<OrgAuthorizedSignatory>();
                //        if (orgRequestlists4[0].Data.OrgGetBasicResult == null)
                //            orgRequestlists4[0].Data.OrgGetBasicResult = new List<OrgGetBasicResult>();
                //        if (orgRequestlists4[0].Data.OrgGetCommercialLicenseResult == null)
                //            orgRequestlists4[0].Data.OrgGetCommercialLicenseResult = new List<OrgGetCommercialLicenseResult>();
                //        if (orgRequestlists4[0].Data.OrgGetDocumentsResult == null)
                //            orgRequestlists4[0].Data.OrgGetDocumentsResult = new List<OrgGetDocumentsResult>();
                //        if (orgRequestlists4[0].Data.OrgGetImportLicenseResult == null)
                //            orgRequestlists4[0].Data.OrgGetImportLicenseResult = new List<OrgGetImportLicenseResult>();
                //        if (orgRequestlists4[0].Data.OrgGetIndustrialResult == null)
                //            orgRequestlists4[0].Data.OrgGetIndustrialResult = new List<OrgGetIndustrialResult>();
                //        TempData["data"] = orgRequestlists4;
                //        TempData.Keep("data");
                //    }
                //}



                CommonFunctions.LogUserActivity("Org_Registration", "", "", "", "", "");
                return View(ObjOrgGetBasicResult);

            }
            catch (Exception e)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);//+ e.Message);
                ViewBag.modelerror = Constantclass.number;
                CommonFunctions.LogUserActivity("Org_Registration", "", "", "", "", e.Message.ToString());
                return View(ObjOrgGetBasicResult);
                // throw ex;
            }
        }
        [HttpPost]
        public ActionResult Org_Registration(OrgGetBasicResult objorgreg)
        {
            try
            {
                if (TempData["data"] != null)//Added this condition (&& objorgreg.OrganizationId==0) to avoid update of first page details in Org Update Request - Siraj
                {
                    List<OrgRequestlist> objOrgRequestdatalist1 = new List<OrgRequestlist>();
                    objOrgRequestdatalist1 = (List<OrgRequestlist>)TempData["data"];
                    if (Session["purpose"].ToString().Trim() == Constantclass.number1 && objOrgRequestdatalist1[0].Data.OrgGetBasicResult[0].OrganizationId != 0)
                    {
                        objorgreg = objOrgRequestdatalist1[0].Data.OrgGetBasicResult[0];
                    }
                }

                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }

                int UserId = int.Parse(Session["UserId"].ToString());
                ReqObj r = new ReqObj { ParentID = UserId, ChildUser = 0 };
                DataTable dt = objdataclass.CanCreateOrg(r);
                bool CanCreateOrg = Convert.ToBoolean(dt.Rows[0]["CanCreateOrg"].ToString());
                bool MainOrgApproved = Convert.ToBoolean(dt.Rows[0]["MainOrgApproved"].ToString());
                if (!MainOrgApproved && CanCreateOrg)//now there will be one entry in request table
                {
                    ViewBag.CompanyType = "Main";
                    objorgreg.TradeLicNumber = Session["LicenseNumber"].ToString();
                }


                if (Session["purpose"].ToString() != "0" && objorgreg.OrganizationId == 0)//(( objorgreg.OrganizationRequestId == 0 ) && (objorgreg.OrganizationId == 0) && Session["purpose"].ToString() !="0" && TempData["data"] != null)//Check to ensure  that the entered trade license is not used already for any organization(existing,new,orgrequests), chekcing TempData["data"] != null to read from object if its main company or sub company -Siraj ... added this condition to not do any validation for first page during Org update request  objorgreg.OrganizationRequestId == 0
                {
                    ReqObj ro = new ReqObj { CommonData = objorgreg.TradeLicNumber, CommonData1 = objorgreg.OrganizationRequestId.ToString(), CommonData2 = objorgreg.OrganizationId.ToString() };
                    DataTable DT = objdataclass.UniqueTradeLicenseCheck(ro);
                    if (DT != null)
                    {
                        if (DT.Rows.Count > 0)
                        {
                            if (!Convert.ToBoolean(DT.Rows[0]["UniqueLicense"]))
                            {
                                //List<OrgGetBasicResult> objlistTL = new List<OrgGetBasicResult>();
                                //objlistTL.Add(objorgreg);
                                //ViewData["data"] = objlistTL;
                                if (TempData["data"] != null)
                                {
                                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                                    objorgreg.IsmainCompany = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany;
                                    TempData["data"] = TempData["data"];
                                    TempData.Keep("data");
                                }
                                else
                                {
                                    objorgreg.IsmainCompany = ViewBag.CompanyType == "Main" ? true : false;
                                }

                                ModelState.AddModelError("TradeLicNumber", Resources.Resource.AlreadyRegisteredOrgLicense);

                                return View("Org_Registration", objorgreg);
                            }
                        }
                    }
                }



                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;

                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                //List<OrgGetBasicResult> objlist = new List<OrgGetBasicResult>();
                if (!(ModelState.IsValid) && objorgreg.OrganizationId == 0)//to avoid validations in first page details in Org Update Request. This (&& objorgreg.OrganizationRequestId == 0) will avoid model validation for approved orgs - Siraj
                {
                    if (Session["purpose"] != null)
                    {
                        if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                        {
                            //objlist.Add(objorgreg);
                            //ViewData["data"] = objlist;
                            //ViewBag.modelerror = Constantclass.number;

                            return View("Org_Registration", objorgreg);
                        }
                    }
                }
                OrgGetBasicResult objinput = new OrgGetBasicResult();
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                    if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                    {
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgEngName = objorgreg.OrgEngName;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].TradeLicNumber = objorgreg.TradeLicNumber;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].CivilId = objorgreg.CivilId;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].AuthPerson = objorgreg.AuthPerson;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName = objorgreg.OrgAraName;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].POBoxNo = objorgreg.POBoxNo;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].Address = objorgreg.Address;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].City = objorgreg.City;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].State = objorgreg.State;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].PostalCode = objorgreg.PostalCode;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].BusiNo = objorgreg.BusiNo;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].BusiFaxNo = objorgreg.BusiFaxNo;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].MobileNo = objorgreg.MobileNo;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].ResidenceNo = objorgreg.ResidenceNo;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].EmailId = objorgreg.EmailId;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].WebPageAddress = objorgreg.WebPageAddress;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].isIndustrial = objorgreg.isIndustrial;
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany = objorgreg.IsmainCompany;//Company type label issue , added this property assignment, enabled again
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].mUserid = Session["UserId"].ToString();
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].tokenId = Session["TokenId"].ToString();

                        //if (objorgreg.OrganizationRequestId == null || objorgreg.OrganizationRequestId == 0)//OrganizationRequestId coming as null from view ,- siraj 
                        //need to check how its not null in noneditable view
                        if (objorgreg.OrganizationRequestId == 0)
                        {
                            objorgreg.OrganizationRequestId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;
                        }

                        objinput = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0];
                    }
                }
                else
                {
                    objOrgRequestdatalist.Add(objOrgRequestlist);
                    //objData.OrgGetBasicResult = objlist;

                    objOrgRequestdatalist[0].Data = objData;
                    List<OrgGetBasicResult> OGBR = new List<OrgGetBasicResult>();
                    objOrgRequestdatalist[0].Data.OrgGetBasicResult = OGBR;
                    objOrgRequestdatalist[0].Data.OrgGetBasicResult.Add(objorgreg);
                    objorgreg.mUserid = Session["UserId"].ToString();
                    objorgreg.tokenId = Session["TokenId"].ToString();
                    objinput = objorgreg;
                }
                string res = string.Empty;
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {

                    DataSet objOrgReq1Result = objdataclass.OrgRegistration_Data_create(objinput);// objinput);
                    if (objOrgReq1Result.Tables[0].Rows.Count > 0)
                    {
                        if (objOrgReq1Result.Tables[0].Rows[0]["Status"].ToString() == Constantclass.number)
                        {
                            res = Constantclass.number;

                            //Moving below line inside  if statement , so that we consider the details only if the Status =1 and not -1
                            //objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany = objOrgReq1Result.Tables[0].Rows[0]["IsMainCompany"].ToString() == "1" ? true : false;//for company type

                        }
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrganizationRequestId"]);

                        //Moving below line to check ismaincompany to inside  if statement , so that we consider the details only if the Status =1 and not -1
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany = objOrgReq1Result.Tables[0].Rows[0]["IsMainCompany"].ToString() == "1" ? true : false;//for company type

                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].RequestNumber = objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString();//added newly

                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany)
                        {
                            ViewBag.CompanyType = "Main";
                        }
                        else
                        {
                            ViewBag.CompanyType = "Sub";
                        }
                        objorgreg.IsmainCompany = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany;

                        //put here
                        Session["UploadOrgId"] = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrganizationRequestId"]);
                        Session["requestnumber"] = objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString();
                        ViewBag.reqno = Session["requestnumber"].ToString();//added to show req number in first page of org reg
                    }
                }
                else
                {
                    if (Session["requestnumber"] == null)
                    {
                        Session["requestnumber"] = Constantclass.number;
                    }
                    else////added to show req number in first page of org reg
                    {
                        ViewBag.reqno = Session["requestnumber"].ToString();
                    }
                }
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");
                //checksession();
                if (res == Constantclass.number || Session["purpose"].ToString().Trim() != Constantclass.number1)
                {
                    {// added this block newly for additional Auth Sigantories feature --Siraj
                        Session["AuthorizedSignatorySerialNum"] = 0;
                        if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableAdditionalAuthSignatory"].ToString()))
                        {
                            Session["returnurl1"] = "Org_Registration";
                            return RedirectToAction("Org_AuthSignatory", "User");
                        }
                    }
                    if (objorgreg.isIndustrial)
                    {
                        Session["returnurl1"] = "Org_Industrial";
                        return RedirectToAction("Org_Industrial", "User");
                    }
                    else
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult != null)
                        {
                            CommonFunctions.LogUserActivity("Org_Registration", "", "", "", "", "Requestnumber=" + Session["requestnumber"].ToString() + " OrganizationId=" + objorgreg.OrganizationId.ToString());

                            if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0] != null)
                            {
                                if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].isIndustrial)
                                {
                                    Session["returnurl1"] = "Org_Industrial";
                                    return RedirectToAction("Org_Industrial", "User");
                                }
                                else
                                {
                                    Session["ImporterSerialNum"] = 0;
                                    Session["returnurl1"] = "Org_Registration";
                                    if (1 == 1)
                                    {
                                        return RedirectToAction("Org_ImporterDetails", "User");
                                    }
                                    //return RedirectToAction("Org_Commercial", "User");
                                }
                            }
                            else// wont reach here
                            {
                                Session["returnurl1"] = "Org_Industrial";
                                return RedirectToAction("Org_Industrial", "User");
                            }
                        }
                        else// wont reach here
                        {
                            Session["returnurl1"] = "Org_Industrial";
                            return RedirectToAction("Org_Industrial", "User");
                        }
                    }
                }
                else
                {
                    //List<OrgGetBasicResult> objlist1 = new List<OrgGetBasicResult>();
                    //objlist1.Add(objorgreg);
                    //ViewData["data"] = objlist1;
                    CommonFunctions.LogUserActivity("Org_Registration", "", "", "", "", "");
                    return View("Org_Registration", objorgreg);
                }
            }

            catch (Exception e)
            {

                WriteToLogFile(e.Message, "Org_Registration");
                List<OrgGetBasicResult> objlist = new List<OrgGetBasicResult>();
                //objlist.Add(objorgreg);
                //ViewData["data"] = objlist;
                TempData["data"] = TempData["data"];

                //ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;

                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);//+ e.Message);
                ViewBag.modelerror = Constantclass.number;

                ViewBag.GoToRequestStatus = "0";

                CommonFunctions.LogUserActivity("Org_Registration", "", "", "", "", e.Message.ToString());
                return View("Org_Registration", objorgreg);
            }
        }
        public ActionResult Org_AuthSignatory(bool AuthorizedSignatoryBack = false, bool AddMore = false)//,bool NextImporter=false)
        {
            OrgAuthorizedSignatory OrgAuthSignatory = new OrgAuthorizedSignatory();
            try
            {

                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }

                if (!OrgRegistrationAllowed())
                    return RedirectToAction("UnAuthorizedAction", "Registration");

                int AuthorizedSignatorySerialNum = Int32.Parse(Session["AuthorizedSignatorySerialNum"].ToString());

                if (AuthorizedSignatoryBack && AuthorizedSignatorySerialNum > 0)
                {
                    AuthorizedSignatorySerialNum = AuthorizedSignatorySerialNum - 1;
                    Session["AuthorizedSignatorySerialNum"] = AuthorizedSignatorySerialNum;
                }


                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories != null)
                        {

                            OrgAuthSignatory.OrgAraName = objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[0].OrgAraName;

                            if (!AuthorizedSignatoryBack && AddMore)//AddMoreImporter type submit
                            {
                                AuthorizedSignatorySerialNum = AuthorizedSignatorySerialNum + 1;
                                Session["AuthorizedSignatorySerialNum"] = AuthorizedSignatorySerialNum;
                                objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories.Add(OrgAuthSignatory);
                            }

                            int TotalAuthorizedSignatories = objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories.Count();
                            if (AuthorizedSignatorySerialNum + 1 == TotalAuthorizedSignatories)
                            {
                                ViewBag.AddMoreOption = true;
                            }
                            else
                            {
                                ViewBag.AddMoreOption = false;
                            }

                            objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;

                            objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrganizationId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationId;
                            objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrganizationRequestId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;
                            //objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrgAuthorizedSignatoryId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;

                            OrgAuthSignatory = objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum];


                            //objlist.Add(ObjImportLicenseResult);
                            //objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.Add(ObjImportLicenseResult);
                        }
                        else
                        {
                            ViewBag.AddMoreOption = true;

                            OrgAuthSignatory.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;//siraj
                            //objlist.Add(ObjImportLicenseResult);
                            List<OrgAuthorizedSignatory> OAS = new List<OrgAuthorizedSignatory>();
                            objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories = OAS;
                            objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories.Add(OrgAuthSignatory);
                            //objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = objlist;
                        }
                    }
                    else
                    {
                        //OrgAuthSignatory.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;//siraj
                        //objlist.Add(ObjImportLicenseResult);
                        objOrgRequestdatalist[0].Data = objData;
                        List<OrgAuthorizedSignatory> OAS = new List<OrgAuthorizedSignatory>();
                        objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories = OAS;
                        objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories.Add(OrgAuthSignatory);
                        //objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = objlist;
                    }
                }
                else
                {
                    //objlist.Add(ObjImportLicenseResult);

                    objOrgRequestdatalist[0].Data = objData;
                    List<OrgAuthorizedSignatory> OAS = new List<OrgAuthorizedSignatory>();
                    objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories = OAS;

                    objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories.Add(OrgAuthSignatory);
                    //objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = objlist;
                }
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;
                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                ViewBag.reqno = Session["requestnumber"].ToString();
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");

                if (AuthorizedSignatorySerialNum == 0)
                {
                    ViewBag.FirstAuthPerson = true;
                }
                else
                {
                    ViewBag.FirstAuthPerson = false;
                }
                //ViewData["data"] = objlist;
                //checksession();
                CommonFunctions.LogUserActivity("Org_AuthSignatory", "", "", "", "", "");
                return View(OrgAuthSignatory);
            }
            catch (Exception e)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                CommonFunctions.LogUserActivity("Org_AuthSignatory", "", "", "", "", e.Message.ToString());
                return View(OrgAuthSignatory);
            }
        }
        [HttpPost]
        public ActionResult Org_AuthSignatory(OrgAuthorizedSignatory OrgAuthSignatory, string AuthSignatorySubmitType)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }


                int AuthorizedSignatorySerialNum = Int32.Parse(Session["AuthorizedSignatorySerialNum"].ToString());

                if (AuthorizedSignatorySerialNum == 0)
                {
                    ViewBag.FirstAuthPerson = true;
                }
                else
                {
                    ViewBag.FirstAuthPerson = false;
                }

                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;
                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                ViewBag.modelerror = Constantclass.number1;
                string res = string.Empty;
                List<OrgRequestlist> objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                int TotalOrgAuthorizedSignatories = objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories.Count();//This block can be moved after model validation
                if (AuthorizedSignatorySerialNum + 1 == TotalOrgAuthorizedSignatories)
                {
                    ViewBag.AddMoreOption = true;
                }
                else
                {
                    ViewBag.AddMoreOption = false;
                }

                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.reqno = Session["requestnumber"].ToString();
                    OrgAuthSignatory.OrgAraName = objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[0].OrgAraName;
                    if (!(ModelState.IsValid))
                    {
                        //List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                        //objlist.Add(objimporterdetails);
                        //ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        TempData["data"] = objOrgRequestdatalist;
                        TempData.Keep("data");
                        return View("Org_OrgAuthorizedSignatories", OrgAuthSignatory);
                    }


                    //   objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0] = objimporterdetails;
                    objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].mUserid = Session["UserId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].tokenId = Session["TokenId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].AuthPerson = OrgAuthSignatory.AuthPerson;

                    objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].CivilId = OrgAuthSignatory.CivilId;
                    //objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrganizationId = OrgAuthSignatory.OrganizationId;
                    //objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                    objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrganizationRequestId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;
                    //objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrgAuthorizedSignatoryId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;



                    DataSet objOrgReq1Result = objdataclass.OrgAuthPerson_Data_create(objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum]);
                    if (objOrgReq1Result.Tables[0].Rows.Count > 0 && objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString() != "")
                    {
                        if (objOrgReq1Result.Tables[0].Rows[0]["Status"].ToString() == Constantclass.number)
                        {
                            res = Constantclass.number;
                        }
                        objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrganizationRequestId = Convert.ToInt64(objOrgReq1Result.Tables[0].Rows[0]["OrganizationRequestId"]);
                        objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrganizationId = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrganizationId"]);
                        objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories[AuthorizedSignatorySerialNum].OrgAuthorizedSignatoryId = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrgAuthorizedSignatoryId"]);
                        Session["requestnumber"] = objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString();
                    }
                }
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");
                //checksession();
                if (res == Constantclass.number || Session["purpose"].ToString().Trim() != Constantclass.number1)
                {
                    CommonFunctions.LogUserActivity("Org_AuthSignatory", "", "", "", "", "Requestnumber=" + Session["requestnumber"].ToString() + " OrganizationId=" + objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationId.ToString());

                    switch (AuthSignatorySubmitType)
                    {
                        case "AddMoreAuthSignatory":
                            //Session["AuthorizedSignatorySerialNum"] = AuthorizedSignatorySerialNum + 1;
                            return RedirectToAction("Org_AuthSignatory", "User", new { AddMore = true });

                        default:
                            int currentAuthSignatorySeqNum = AuthorizedSignatorySerialNum + 1;//AuthorizedSignatorySerialNum starts from 0 as it is a collection reference
                            if (currentAuthSignatorySeqNum >= TotalOrgAuthorizedSignatories)
                            {
                                if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].isIndustrial)
                                {
                                    Session["returnurl1"] = "Org_Industrial";// "Org_AuthSignatory";//corrected 
                                    return RedirectToAction("Org_Industrial", "User");
                                }
                                else
                                {
                                    Session["ImporterSerialNum"] = 0;
                                    Session["returnurl1"] = "Org_AuthSignatory";
                                    return RedirectToAction("Org_ImporterDetails", "User");
                                }
                            }
                            else
                            {
                                Session["AuthorizedSignatorySerialNum"] = AuthorizedSignatorySerialNum + 1;
                                return RedirectToAction("Org_AuthSignatory", "User");//, new { NextImporter = true });
                            }
                    }
                }
                else
                {
                    //List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                    //objlist.Add(objimporterdetails);
                    //ViewData["data"] = objlist;
                    TempData["data"] = objOrgRequestdatalist;
                    CommonFunctions.LogUserActivity("Org_AuthSignatory", "", "", "", "", "");
                    return View("Org_AuthSignatory", OrgAuthSignatory);
                }
            }

            catch (Exception e)
            {
                WriteToLogFile(e.Message, "Org_AuthSignatory");
                //List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                //objlist.Add(objimporterdetails);
                //ViewData["data"] = objlist;
                TempData["data"] = TempData["data"];
                TempData.Keep("data");

                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;


                ViewBag.GoToRequestStatus = "0";
                CommonFunctions.LogUserActivity("Org_AuthSignatory", "", "", "", "", e.Message.ToString());

                return View(OrgAuthSignatory);
            }
        }

        public ActionResult Org_Industrial()
        {
            OrgGetIndustrialResult ObjIndustrialResult = new OrgGetIndustrialResult();
            try
            {

                Session["ImporterSerialNum"] = 0;
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }

                if (Session["purpose"].ToString() == "1")
                {
                    ObjIndustrialResult.IndustrialLicenseNumber = Session["IndustrialLicenseNumber"].ToString();
                    //CommercialLicenseNumber
                }

                if (!OrgRegistrationAllowed())
                    return RedirectToAction("UnAuthorizedAction", "Registration");

                //List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetIndustrialResult != null)
                        {
                            //ObjIndustrialResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            ObjIndustrialResult = objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0];
                            //objlist.Add(ObjIndustrialResult);
                        }
                        else
                        {
                            ObjIndustrialResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;//siraj
                                                                                                                           //objlist.Add(ObjIndustrialResult);
                                                                                                                           //objOrgRequestdatalist[0].Data.OrgGetIndustrialResult = objlist;

                            List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                            objOrgRequestdatalist[0].Data.OrgGetIndustrialResult = objlist;

                            objOrgRequestdatalist[0].Data.OrgGetIndustrialResult.Add(ObjIndustrialResult);
                        }
                    }
                    else
                    {
                        //objlist.Add(ObjIndustrialResult);
                        //objOrgRequestdatalist[0].Data.OrgGetIndustrialResult = objlist;

                        //added newly 15-7
                        objOrgRequestdatalist[0].Data = objData;
                        List<OrgGetIndustrialResult> OAS = new List<OrgGetIndustrialResult>();
                        objOrgRequestdatalist[0].Data.OrgGetIndustrialResult = OAS;

                        objOrgRequestdatalist[0].Data.OrgGetIndustrialResult.Add(ObjIndustrialResult);
                    }
                }
                else
                {
                    //objlist.Add(ObjIndustrialResult);
                    //objOrgRequestdatalist[0].Data.OrgGetIndustrialResult = objlist;

                    objOrgRequestdatalist[0].Data = objData;
                    List<OrgGetIndustrialResult> OAS = new List<OrgGetIndustrialResult>();
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult = OAS;

                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult.Add(ObjIndustrialResult);
                }
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;
                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");
                //ViewData["data"] = objlist;
                ViewBag.reqno = Session["requestnumber"].ToString();
                //checksession();
                CommonFunctions.LogUserActivity("Org_Industrial", "", "", "", "", "");
                return View(ObjIndustrialResult);
            }
            catch (Exception e)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong + e.Message);
                ViewBag.modelerror = Constantclass.number;
                CommonFunctions.LogUserActivity("Org_Industrial", "", "", "", "", e.Message.ToString());
                return View(ObjIndustrialResult);
            }
        }
        [HttpPost]
        public ActionResult Org_Industrial(OrgGetIndustrialResult objindustrialdetails)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;
                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                string res = string.Empty;
                List<OrgRequestlist> objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    //objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0] = objimporterdetails;
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].mUserid = Session["UserId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].tokenId = Session["TokenId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].IndustrialLicenseNumber = objindustrialdetails.IndustrialLicenseNumber;
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].IssueDate = objindustrialdetails.IssueDate;
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].ExpiryDate = objindustrialdetails.ExpiryDate;
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].IndustrialRegistrationNumber = objindustrialdetails.IndustrialRegistrationNumber;
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].IssuanceDate = objindustrialdetails.IssuanceDate;
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].OrganizationRequestId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;
                    ViewBag.reqno = Session["requestnumber"].ToString();

                    if (!(ModelState.IsValid))
                    {
                        //List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                        objindustrialdetails.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                        //objlist.Add(objindustrialdetails);
                        //ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        TempData["data"] = objOrgRequestdatalist;
                        TempData.Keep("data");
                        return View("Org_Industrial", objindustrialdetails);
                    }
                    //string issueyear1 = Convert.ToDateTime(objindustrialdetails.IssuanceDate).Year.ToString();
                    //string issuemonth1 = Convert.ToDateTime(objindustrialdetails.IssuanceDate).Month.ToString();
                    //string issueday1 = Convert.ToDateTime(objindustrialdetails.IssuanceDate).Day.ToString();
                    //string issueyear = Convert.ToDateTime(objindustrialdetails.IssueDate).Year.ToString();
                    //string issuemonth = Convert.ToDateTime(objindustrialdetails.IssueDate).Month.ToString();
                    //string issueday = Convert.ToDateTime(objindustrialdetails.IssueDate).Day.ToString();
                    //string endyear = Convert.ToDateTime(objindustrialdetails.ExpiryDate).Year.ToString();
                    //string endmonth = Convert.ToDateTime(objindustrialdetails.ExpiryDate).Month.ToString();
                    //string endday = Convert.ToDateTime(objindustrialdetails.ExpiryDate).Day.ToString();

                    string issueyear1 = DateTime.ParseExact(objindustrialdetails.IssuanceDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Year.ToString(); //Convert.ToDateTime(objindustrialdetails.IssuanceDate).Year.ToString();
                    string issuemonth1 = DateTime.ParseExact(objindustrialdetails.IssuanceDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Month.ToString();
                    string issueday1 = DateTime.ParseExact(objindustrialdetails.IssuanceDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Day.ToString();
                    string issueyear = DateTime.ParseExact(objindustrialdetails.IssueDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Year.ToString();
                    string issuemonth = DateTime.ParseExact(objindustrialdetails.IssueDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Month.ToString();
                    string issueday = DateTime.ParseExact(objindustrialdetails.IssueDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Day.ToString();
                    string endyear = DateTime.ParseExact(objindustrialdetails.ExpiryDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Year.ToString();
                    string endmonth = DateTime.ParseExact(objindustrialdetails.ExpiryDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Month.ToString();
                    string endday = DateTime.ParseExact(objindustrialdetails.ExpiryDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Day.ToString();


                    string val = string.Empty;
                    if (Convert.ToInt32(issueyear) > Convert.ToInt32(endyear))
                    {
                        val = "yes";
                    }
                    else if ((Convert.ToInt32(issuemonth) > Convert.ToInt32(endmonth)) && (Convert.ToInt32(issueyear) == Convert.ToInt32(endyear)))
                    {
                        val = "yes";
                    }
                    else if ((Convert.ToInt32(issueday) > Convert.ToInt32(endday)) && (Convert.ToInt32(issuemonth) == Convert.ToInt32(endmonth)) && (Convert.ToInt32(issueyear) == Convert.ToInt32(endyear)))
                    {
                        val = "yes";
                    }
                    else if (Convert.ToInt32(issueyear1) > Convert.ToInt32(endyear))
                    {
                        val = "yes";
                    }
                    else if ((Convert.ToInt32(issuemonth1) > Convert.ToInt32(endmonth)) && (Convert.ToInt32(issueyear1) == Convert.ToInt32(endyear)))
                    {
                        val = "yes";
                    }
                    else if ((Convert.ToInt32(issueday1) > Convert.ToInt32(endday)) && (Convert.ToInt32(issuemonth1) == Convert.ToInt32(endmonth)) && (Convert.ToInt32(issueyear1) == Convert.ToInt32(endyear)))
                    {
                        val = "yes";
                    }
                    if (val == "yes" || val == "yes1")
                    {
                        ModelState.AddModelError(string.Empty, Resources.Resource.IssuancegreaterthanEnddate);
                        //ModelState.AddModelError("IssuanceDate", Resources.Resource.IssuancegreaterthanEnddate);
                        //List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                        //objlist.Add(objindustrialdetails);
                        //ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        TempData["data"] = objOrgRequestdatalist;
                        TempData.Keep("data");
                        return View("Org_Industrial", objindustrialdetails);
                    }

                    DataSet objOrgReq1Result = objdataclass.Orgindustrial_Data_create(objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0]);
                    if (objOrgReq1Result.Tables[0].Rows.Count > 0 && objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString() != "")
                    {
                        if (objOrgReq1Result.Tables[0].Rows[0]["Status"].ToString() == Constantclass.number)
                        {
                            res = Constantclass.number;
                        }
                        objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].OrganizationRequestId = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrganizationRequestId"]);
                        Session["requestnumber"] = objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString();

                    }
                }
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");
                //checksession();
                if (res == Constantclass.number || Session["purpose"].ToString().Trim() != Constantclass.number1)
                {

                    Session["ImporterSerialNum"] = 0;
                    if (1 == 1)
                    {
                        return RedirectToAction("Org_ImporterDetails", "User");
                    }
                    //return RedirectToAction("Org_Commercial", "User");
                }
                else
                {
                    //List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                    //objlist.Add(objindustrialdetails);
                    //ViewData["data"] = objlist;
                    TempData["data"] = objOrgRequestdatalist;
                    TempData.Keep("data");
                    //CommonFunctions.LogUserActivity("Org_Industrial", "", "", "", "", "");
                    CommonFunctions.LogUserActivity("Org_Industrial", "", "", "", "", "Requestnumber=" + Session["requestnumber"].ToString() + " OrganizationId=" + objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationId.ToString());

                    return View("Org_Industrial", objindustrialdetails);
                }

            }

            catch (Exception e)
            {
                WriteToLogFile(e.Message, "Org_Industrial");
                //==
                //List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                //objlist.Add(objindustrialdetails);
                //ViewData["data"] = objlist;
                TempData["data"] = TempData["data"];
                TempData.Keep("data");
                //==

                //ViewBag.modelerror = Constantclass.number5;
                //ViewBag.Msg = Resources.Resource.somethingwentwrong;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);//+ e.Message);
                ViewBag.modelerror = Constantclass.number;

                ViewBag.GoToRequestStatus = "0";
                CommonFunctions.LogUserActivity("Org_Industrial", "", "", "", "", e.Message.ToString());

                return View(objindustrialdetails);
            }
        }
        public ActionResult Org_ImporterDetails(bool ImporterBack = false, bool AddMore = false)//,bool NextImporter=false)
        {
            OrgGetImportLicenseResult ObjImportLicenseResult = new OrgGetImportLicenseResult();
            try
            {

                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }

                if (!OrgRegistrationAllowed())
                    return RedirectToAction("UnAuthorizedAction", "Registration");

                int ImporterSerialNum = Int32.Parse(Session["ImporterSerialNum"].ToString());

                if (ImporterBack && ImporterSerialNum > 0)
                {
                    ImporterSerialNum = ImporterSerialNum - 1;
                    Session["ImporterSerialNum"] = ImporterSerialNum;
                }


                //List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult != null)
                        {

                            ObjImportLicenseResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;

                            if (!ImporterBack && AddMore)//AddMoreImporter type submit
                            {
                                ImporterSerialNum = ImporterSerialNum + 1;
                                Session["ImporterSerialNum"] = ImporterSerialNum;
                                objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.Add(ObjImportLicenseResult);
                            }

                            int TotalImporters = objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.Count();
                            if (ImporterSerialNum + 1 == TotalImporters)
                            {
                                ViewBag.AddMoreOption = true;
                            }
                            else
                            {
                                ViewBag.AddMoreOption = false;
                            }

                            objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            ObjImportLicenseResult = objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum];
                            if (objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].ImpLicNo != null)
                            {
                                string[] liarr = objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].ImpLicNo.ToString().Split('/').ToArray();
                                if (liarr.Length > 1)
                                {
                                    ObjImportLicenseResult.Year = liarr[0];
                                    ObjImportLicenseResult.ImpLicNo = liarr[1];
                                }
                            }

                            //objlist.Add(ObjImportLicenseResult);
                            //objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.Add(ObjImportLicenseResult);
                        }
                        else
                        {
                            ObjImportLicenseResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;//siraj
                            //objlist.Add(ObjImportLicenseResult);

                            //added below 2 lines for null exception on collection property
                            List<OrgGetImportLicenseResult> ListOrgGetImportLicenseResult = new List<OrgGetImportLicenseResult>();
                            objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = ListOrgGetImportLicenseResult;

                            objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.Add(ObjImportLicenseResult);
                            //objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = objlist;
                        }
                    }
                    else
                    {
                        //objlist.Add(ObjImportLicenseResult);


                        objOrgRequestdatalist[0].Data = objData;
                        List<OrgGetImportLicenseResult> OAS = new List<OrgGetImportLicenseResult>();
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = OAS;

                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.Add(ObjImportLicenseResult);
                        //objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = objlist;
                    }
                }
                else
                {
                    //objlist.Add(ObjImportLicenseResult);

                    //added newly 15-07
                    objOrgRequestdatalist[0].Data = objData;
                    List<OrgGetImportLicenseResult> OAS = new List<OrgGetImportLicenseResult>();
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = OAS;

                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.Add(ObjImportLicenseResult);
                    //objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = objlist;
                }
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;
                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                ViewBag.reqno = Session["requestnumber"].ToString();
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");

                if (ImporterSerialNum == 0)
                {
                    ViewBag.FirstImport = true;
                }
                else
                {
                    ViewBag.FirstImport = false;
                }
                //ViewData["data"] = objlist;
                //checksession();
                CommonFunctions.LogUserActivity("Org_ImporterDetails", "", "", "", "", "");
                return View(ObjImportLicenseResult);
            }
            catch (Exception e)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                CommonFunctions.LogUserActivity("Org_ImporterDetails", "", "", "", "", e.Message.ToString());
                return View(ObjImportLicenseResult);
            }
        }
        [HttpPost]
        public ActionResult Org_ImporterDetails(OrgGetImportLicenseResult objimporterdetails, string ImporterDataSubmitType)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }


                int ImporterSerialNum = Int32.Parse(Session["ImporterSerialNum"].ToString());

                if (ImporterSerialNum == 0)
                {
                    ViewBag.FirstImport = true;
                }
                else
                {
                    ViewBag.FirstImport = false;
                }

                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;
                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                ViewBag.modelerror = Constantclass.number1;
                string res = string.Empty;
                List<OrgRequestlist> objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                int TotalImporters = objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.Count();//This block can be moved after model validation
                if (ImporterSerialNum + 1 == TotalImporters)
                {
                    ViewBag.AddMoreOption = true;
                }
                else
                {
                    ViewBag.AddMoreOption = false;
                }

                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.reqno = Session["requestnumber"].ToString();
                    objimporterdetails.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                    if (!(ModelState.IsValid))
                    {
                        //List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                        //objlist.Add(objimporterdetails);
                        //ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        TempData["data"] = objOrgRequestdatalist;
                        TempData.Keep("data");
                        return View("Org_ImporterDetails", objimporterdetails);
                    }
                    //string issueyear = Convert.ToDateTime(objimporterdetails.IssueDate).Year.ToString();
                    //string issuemonth = Convert.ToDateTime(objimporterdetails.IssueDate).Month.ToString();
                    //string issueday = Convert.ToDateTime(objimporterdetails.IssueDate).Day.ToString();
                    //string endyear = Convert.ToDateTime(objimporterdetails.ExpiryDate).Year.ToString();
                    //string endmonth = Convert.ToDateTime(objimporterdetails.ExpiryDate).Month.ToString();
                    //string endday = Convert.ToDateTime(objimporterdetails.ExpiryDate).Day.ToString();

                    string issueyear = DateTime.ParseExact(objimporterdetails.IssueDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Year.ToString();
                    string issuemonth = DateTime.ParseExact(objimporterdetails.IssueDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Month.ToString();
                    string issueday = DateTime.ParseExact(objimporterdetails.IssueDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Day.ToString();
                    string endyear = DateTime.ParseExact(objimporterdetails.ExpiryDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Year.ToString();
                    string endmonth = DateTime.ParseExact(objimporterdetails.ExpiryDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Month.ToString();
                    string endday = DateTime.ParseExact(objimporterdetails.ExpiryDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Day.ToString();


                    string val = string.Empty;
                    string val1 = string.Empty;
                    if (Convert.ToInt32(issueyear) > Convert.ToInt32(endyear))
                    {
                        val = "yes";
                    }
                    else if ((Convert.ToInt32(issuemonth) > Convert.ToInt32(endmonth)) && (Convert.ToInt32(issueyear) == Convert.ToInt32(endyear)))
                    {
                        val = "yes";
                    }
                    else if ((Convert.ToInt32(issueday) > Convert.ToInt32(endday)) && (Convert.ToInt32(issuemonth) == Convert.ToInt32(endmonth)) && (Convert.ToInt32(issueyear) == Convert.ToInt32(endyear)))
                    {
                        val = "yes";
                    }
                    if (objimporterdetails.Year != "" && objimporterdetails.Year != null)
                    {
                        if (issueyear != objimporterdetails.Year)
                        {
                            if (objimporterdetails.ImpLicType == "temporary")
                            {
                                val1 = "yes1";
                            }
                        }
                    }
                    if (val == "yes" || val1 == "yes1")
                    {
                        if (val1 == "yes1")
                        {
                            //ModelState.AddModelError(string.Empty, Resources.Resource.validateYear);
                            ModelState.AddModelError("Year", Resources.Resource.validateYear);
                        }
                        if (val == "yes")
                        {
                            //ModelState.AddModelError(string.Empty, Resources.Resource.IssuancegreaterthanEnddate);
                            ModelState.AddModelError("IssueDate", Resources.Resource.IssuancegreaterthanEnddate);
                        }
                        //List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                        //objlist.Add(objimporterdetails);
                        //ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        TempData["data"] = objOrgRequestdatalist;
                        TempData.Keep("data");
                        return View("Org_ImporterDetails", objimporterdetails);
                    }

                    //   objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0] = objimporterdetails;
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].mUserid = Session["UserId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].tokenId = Session["TokenId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].ImpLicType = objimporterdetails.ImpLicType;
                    if (objimporterdetails.ImpLicType == "permanent")
                    {
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].Year = "";
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].ImpLicNo = objimporterdetails.ImpLicNo;
                    }
                    else
                    {
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].ImpLicNo = objimporterdetails.Year + "/" + objimporterdetails.ImpLicNo;
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].Year = objimporterdetails.Year;//added Siraj - temporary license details(lic and year coming as empty when navigate to next and come back)
                    }
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].IssueDate = objimporterdetails.IssueDate;
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].ExpiryDate = objimporterdetails.ExpiryDate;
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].OrganizationRequestId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;



                    DataSet objOrgReq1Result = objdataclass.Orgimporter_Data_create(objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum]);
                    if (objOrgReq1Result.Tables[0].Rows.Count > 0 && objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString() != "")
                    {
                        if (objOrgReq1Result.Tables[0].Rows[0]["Status"].ToString() == Constantclass.number)
                        {
                            res = Constantclass.number;
                        }
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[ImporterSerialNum].OrganizationRequestId = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrganizationRequestId"]);
                        Session["requestnumber"] = objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString();
                    }
                }
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");
                //checksession();
                if (res == Constantclass.number || Session["purpose"].ToString().Trim() != Constantclass.number1)
                {
                    switch (ImporterDataSubmitType)
                    {
                        case "AddMoreImporterLicense":
                            //Session["ImporterSerialNum"] = ImporterSerialNum + 1;
                            return RedirectToAction("Org_ImporterDetails", "User", new { AddMore = true });

                        default:
                            int currentImporterSeqNum = ImporterSerialNum + 1;//ImporterSerialNum starts from 0 as it is a collection reference
                            if (currentImporterSeqNum >= TotalImporters)
                            {
                                return RedirectToAction("Org_Commercial", "User");
                            }
                            else
                            {
                                Session["ImporterSerialNum"] = ImporterSerialNum + 1;
                                return RedirectToAction("Org_ImporterDetails", "User");//, new { NextImporter = true });
                            }


                    }
                }
                else
                {
                    //List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                    //objlist.Add(objimporterdetails);
                    //ViewData["data"] = objlist;
                    TempData["data"] = objOrgRequestdatalist;
                    //CommonFunctions.LogUserActivity("Org_ImporterDetails", "", "", "", "","");
                    CommonFunctions.LogUserActivity("Org_ImporterDetails", "", "", "", "", "Requestnumber=" + Session["requestnumber"].ToString() + " OrganizationId=" + objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationId.ToString());

                    return View("Org_ImporterDetails", objimporterdetails);
                }
            }

            catch (Exception e)
            {
                WriteToLogFile(e.Message, "Org_ImporterDetails");
                //List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                //objlist.Add(objimporterdetails);
                //ViewData["data"] = objlist;
                TempData["data"] = TempData["data"];
                TempData.Keep("data");

                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;


                ViewBag.GoToRequestStatus = "0";
                CommonFunctions.LogUserActivity("Org_ImporterDetails", "", "", "", "", e.Message.ToString());

                return View(objimporterdetails);
            }
        }


        public ActionResult DeleteThisImporterLicense()
        {
            if (TempData["data"] != null)
            {

                int ImporterSerialNum = Int32.Parse(Session["ImporterSerialNum"].ToString());
                objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.RemoveAt(Convert.ToInt32(ImporterSerialNum));
                Session["ImporterSerialNum"] = ImporterSerialNum - 1;
                //DBCall
            }
            return RedirectToAction("Org_ImporterDetails", "User", new { ImporterBack = true });
        }
        public ActionResult DeleteThisAuthorizedSignatory()
        {
            if (TempData["data"] != null)
            {

                int AuthorizedSignatorySerialNum = Int32.Parse(Session["AuthorizedSignatorySerialNum"].ToString());
                objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                objOrgRequestdatalist[0].Data.OrgAuthorizedSignatories.RemoveAt(Convert.ToInt32(AuthorizedSignatorySerialNum));
                Session["AuthorizedSignatorySerialNum"] = AuthorizedSignatorySerialNum - 1;
                //DBCall
            }
            return RedirectToAction("Org_AuthSignatory", "User", new { AuthorizedSignatoryBack = true });
        }

        public ActionResult Org_Commercial()
        {
            OrgGetCommercialLicenseResult ObjCommercialLicenseResult = new OrgGetCommercialLicenseResult();

            //Session["ImporterSerialNum"] = 0;
            try
            {

                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                //if (Session["purpose"].ToString() == "1")
                //{
                //    ObjCommercialLicenseResult.CommLicNo = Session["CommercialLicenseNumber"].ToString();
                //    ObjCommercialLicenseResult.Year = Session["CommercialLicenseYear"].ToString();

                //}
                if (!OrgRegistrationAllowed())
                    return RedirectToAction("UnAuthorizedAction", "Registration");

                ViewBag.modelerror = Constantclass.number1;
                ViewBag.viewname = "Comm";

                string subtype = string.Empty;
                //List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                    int TotalImporterLicenses = objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult.Count();
                    Session["ImporterSerialNum"] = TotalImporterLicenses - 1;


                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult != null)
                        {
                            ObjCommercialLicenseResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            ObjCommercialLicenseResult = objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0];
                            if (objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].CommLicNo != null)
                            {
                                string[] liarr = objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].CommLicNo.ToString().Split('/').ToArray();
                                if (liarr.Length > 1)
                                {
                                    ObjCommercialLicenseResult.Year = liarr[0];
                                    ObjCommercialLicenseResult.CommLicNo = liarr[1];
                                }
                            }
                            subtype = objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].CommLicSubType;
                            //objlist.Add(ObjCommercialLicenseResult);
                            //objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult.Add(ObjCommercialLicenseResult);
                        }
                        else
                        {
                            ObjCommercialLicenseResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            //objlist.Add(ObjCommercialLicenseResult);
                            //objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult = objlist;


                            List<OrgGetCommercialLicenseResult> ListOrgGetCommercialLicenseResult = new List<OrgGetCommercialLicenseResult>();
                            objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult = ListOrgGetCommercialLicenseResult;

                            objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult.Add(ObjCommercialLicenseResult);
                        }
                    }
                    else
                    {
                        //objlist.Add(ObjCommercialLicenseResult);
                        //objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult = objlist;


                        objOrgRequestdatalist[0].Data = objData;
                        List<OrgGetCommercialLicenseResult> OAS = new List<OrgGetCommercialLicenseResult>();
                        objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult = OAS;

                        objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult.Add(ObjCommercialLicenseResult);
                    }
                }
                else
                {
                    //objlist.Add(ObjCommercialLicenseResult);
                    //objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult = objlist;

                    //added newly 15-07
                    objOrgRequestdatalist[0].Data = objData;
                    List<OrgGetCommercialLicenseResult> OAS = new List<OrgGetCommercialLicenseResult>();
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult = OAS;

                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult.Add(ObjCommercialLicenseResult);
                }

                ViewBag.reqno = Session["requestnumber"].ToString();
                TempData["data"] = objOrgRequestdatalist;
                //ViewData["data"] = objlist;
                TempData.Keep("data");
                CommSubtypes(subtype);
                //checksession();
                CommonFunctions.LogUserActivity("Org_Commercial", "", "", "", "", "");
                return View(ObjCommercialLicenseResult);
            }
            catch (Exception e)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                CommonFunctions.LogUserActivity("Org_Commercial", "", "", "", "", e.Message.ToString());
                return View(ObjCommercialLicenseResult);
            }
        }
        public void CommSubtypes(string subtype)
        {
            if (Session["purpose"].ToString().Trim() == Constantclass.number1)
            {
                ViewBag.dis = Constantclass.number1;
            }
            else
            {
                ViewBag.dis = Constantclass.number;
            }

            langParams objlangParams = new langParams();
            HttpCookie langCookie = Request.Cookies["culture"];
            objlangParams.lang = langCookie.Value;
            List<CommLicSubTypeslist> objCaptchadetails = new List<CommLicSubTypeslist>();
            ////if (subtype != string.Empty && subtype != Constantclass.number)
            //if (subtype != string.Empty)
            //{
            //string Stype = string.Empty;
            //objCaptchadetails = objdataclass.GetCommLicSubTypes(objlangParams);
            //foreach (var item in objCaptchadetails)
            //{
            //    foreach (var item1 in item.CommLicSubTypes)
            //    {
            //        if (item1.TypeId == Convert.ToInt32(subtype))
            //        {
            //            Stype = "S";
            //            ViewBag.subtype = item1.Name;
            //            ViewBag.subtypeid = item1.TypeId;
            //        }
            //    }
            //}
            //    if (Stype == "")
            //    {
            //        ViewBag.subtype = "";
            //        ViewBag.subtypeid = "";
            //    }
            //}
            //else
            //{
            //    ViewBag.subtype = "";
            //    ViewBag.subtypeid = "";
            //}
            string Stype = string.Empty;
            objCaptchadetails = objdataclass.GetCommLicSubTypes(objlangParams);
            foreach (var item in objCaptchadetails)
            {
                foreach (var item1 in item.CommLicSubTypes)
                {
                    if (subtype != string.Empty && subtype != null)
                    {
                        if (item1.TypeId == Convert.ToInt32(subtype))
                        {
                            Stype = "S";
                            ViewBag.subtype = item1.Name;
                            ViewBag.subtypeid = item1.TypeId;
                        }
                    }
                }
            }
            if (Stype == "")
            {
                ViewBag.subtype = "";
                ViewBag.subtypeid = "";
            }
            ViewData["CommLicSubTypes"] = objCaptchadetails;
        }
        [HttpPost]
        public ActionResult Org_Commercial(OrgGetCommercialLicenseResult objcommercialdetails)
        {
            try
            {

                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }

                if (!OrgRegistrationAllowed())
                    RedirectToAction("UnAuthorizedAction", "Registration");

                ViewBag.modelerror = Constantclass.number1;
                ViewBag.viewname = "Comm";
                string res = string.Empty;
                List<OrgRequestlist> objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;

                    ViewBag.reqno = Session["requestnumber"].ToString();
                    objcommercialdetails.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                    if (!(ModelState.IsValid))
                    {
                        //List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                        //objlist.Add(objcommercialdetails);
                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].isIndustrial)
                        {
                            if (objcommercialdetails.CommLicNo != null || objcommercialdetails.Year != null || objcommercialdetails.ExpiryDate != null || objcommercialdetails.IssueDate != null || objcommercialdetails.CommLicType != null)
                            {
                                //ViewData["data"] = objlist;
                                ViewBag.modelerror = Constantclass.number;
                                CommSubtypes(objcommercialdetails.CommLicSubType);
                                TempData["data"] = objOrgRequestdatalist;
                                TempData.Keep("data");//Temp data gets lost during model validation , so added newly
                                return View("Org_Commercial", objcommercialdetails);
                            }
                        }
                        else
                        {
                            //ViewData["data"] = objlist;
                            ViewBag.modelerror = Constantclass.number;
                            CommSubtypes(objcommercialdetails.CommLicSubType);
                            TempData["data"] = objOrgRequestdatalist;
                            TempData.Keep("data");//Temp data gets lost during model validation , so added newly
                            return View("Org_Commercial", objcommercialdetails);
                        }

                    }
                    //string issueyear = Convert.ToDateTime(objcommercialdetails.IssueDate).Year.ToString();
                    //string issuemonth = Convert.ToDateTime(objcommercialdetails.IssueDate).Month.ToString();
                    //string issueday = Convert.ToDateTime(objcommercialdetails.IssueDate).Day.ToString();
                    //string endyear = Convert.ToDateTime(objcommercialdetails.ExpiryDate).Year.ToString();
                    //string endmonth = Convert.ToDateTime(objcommercialdetails.ExpiryDate).Month.ToString();
                    //string endday = Convert.ToDateTime(objcommercialdetails.ExpiryDate).Day.ToString();

                    string issueyear = DateTime.ParseExact(objcommercialdetails.IssueDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Year.ToString();
                    string issuemonth = DateTime.ParseExact(objcommercialdetails.IssueDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Month.ToString();
                    string issueday = DateTime.ParseExact(objcommercialdetails.IssueDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Day.ToString();
                    string endyear = DateTime.ParseExact(objcommercialdetails.ExpiryDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Year.ToString();
                    string endmonth = DateTime.ParseExact(objcommercialdetails.ExpiryDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Month.ToString();
                    string endday = DateTime.ParseExact(objcommercialdetails.ExpiryDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Day.ToString();


                    string val = string.Empty;
                    string val1 = string.Empty;
                    if (Convert.ToInt32(issueyear) > Convert.ToInt32(endyear))
                    {
                        val = "yes";
                    }
                    else if ((Convert.ToInt32(issuemonth) > Convert.ToInt32(endmonth)) && (Convert.ToInt32(issueyear) == Convert.ToInt32(endyear)))
                    {
                        val = "yes";
                    }
                    else if ((Convert.ToInt32(issueday) > Convert.ToInt32(endday)) && (Convert.ToInt32(issuemonth) == Convert.ToInt32(endmonth)) && (Convert.ToInt32(issueyear) == Convert.ToInt32(endyear)))
                    {
                        val = "yes";
                    }
                    if (objcommercialdetails.Year != "" && objcommercialdetails.Year != null)
                    {
                        if (issueyear != objcommercialdetails.Year)
                        {
                            val1 = "yes1";
                        }
                    }
                    if (val == "yes" || val1 == "yes1")
                    {
                        if (val1 == "yes1")
                        {
                            //ModelState.AddModelError(string.Empty, Resources.Resource.validateYear);
                            ModelState.AddModelError("Year", Resources.Resource.validateYear);
                        }
                        if (val == "yes")
                        {
                            //ModelState.AddModelError(string.Empty, Resources.Resource.IssuancegreaterthanEnddate);
                            ModelState.AddModelError("IssueDate", Resources.Resource.IssuancegreaterthanEnddate);
                        }
                        //List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                        //objlist.Add(objcommercialdetails);
                        //ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        CommSubtypes(objcommercialdetails.CommLicSubType);
                        TempData["data"] = objOrgRequestdatalist;
                        TempData.Keep("data");//Temp data gets lost during model validation , so added newly
                        return View("Org_Commercial", objcommercialdetails);
                    }

                    // objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0] = objcommercialdetails;
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].mUserid = Session["UserId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].tokenId = Session["TokenId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].CommLicType = objcommercialdetails.CommLicType;
                    if (objcommercialdetails.CommLicType == "personal") //if (objcommercialdetails.CommLicSubType == "personal")  // siraj
                    {
                        objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].CommLicSubType = "";
                    }
                    else
                    {
                        objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].CommLicSubType = objcommercialdetails.CommLicSubType;
                    }
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].CommLicNo = objcommercialdetails.Year + "/" + objcommercialdetails.CommLicNo;
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].Year = objcommercialdetails.Year;
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].IssueDate = objcommercialdetails.IssueDate;
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].ExpiryDate = objcommercialdetails.ExpiryDate;
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].OrganizationRequestId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;


                    DataSet objOrgReq1Result = objdataclass.Orgcommercial_Data_create(objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0]);
                    if (objOrgReq1Result.Tables[0].Rows.Count > 0 && objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString() != "")
                    {
                        if (objOrgReq1Result.Tables[0].Rows[0]["Status"].ToString() == Constantclass.number)
                        {
                            res = Constantclass.number;
                        }
                        objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult[0].OrganizationRequestId = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrganizationRequestId"]);
                        Session["requestnumber"] = objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString();
                    }
                }
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");
                //checksession();
                if (res == Constantclass.number || Session["purpose"].ToString().Trim() != Constantclass.number1)
                {
                    return RedirectToAction("UploadDocument", "User");
                }
                else
                {
                    //List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                    //objlist.Add(objcommercialdetails);
                    //ViewData["data"] = objlist;
                    CommSubtypes(objcommercialdetails.CommLicSubType);
                    TempData["data"] = objOrgRequestdatalist;
                    TempData.Keep("data");
                    //CommonFunctions.LogUserActivity("Org_Commercial", "", "", "", "", "");
                    CommonFunctions.LogUserActivity("Org_Commercial", "", "", "", "", "Requestnumber=" + Session["requestnumber"].ToString() + " OrganizationId=" + objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationId.ToString());

                    return View("Org_Commercial", objcommercialdetails);
                }
            }
            catch (Exception e)
            {
                WriteToLogFile(e.Message, "Org_Commercial");
                //List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                //objlist.Add(objcommercialdetails);
                //ViewData["data"] = objlist;
                TempData["data"] = TempData["data"];
                TempData.Keep("data");//Temp data gets lost during model validation , so added newly

                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;

                ViewBag.GoToRequestStatus = "0";
                CommonFunctions.LogUserActivity("Org_Commercial", "", "", "", "", e.Message.ToString());

                return View(objcommercialdetails);
            }
        }
        [NonAction]
        public void uploadmethod()
        {
            try
            {
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;
                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                List<OrgGetDocumentsResult> objlist = new List<OrgGetDocumentsResult>();
                OrgGetDocumentsResult objDocumentsResult = new OrgGetDocumentsResult();
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult == null)
                        {
                            objDocumentsResult.DocumentName = "";
                            objlist.Add(objDocumentsResult);
                            objOrgRequestdatalist[0].Data.OrgGetDocumentsResult = objlist;
                        }
                        else
                        {
                            objDocumentsResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.Count < 1)
                            {
                                objDocumentsResult.DocumentName = "";
                                objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.Add(objDocumentsResult);
                            }
                            if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult != null)
                            {
                                objOrgRequestdatalist[0].Data.OrgGetDocumentsResult[0].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;

                                foreach (var item in objOrgRequestdatalist[0].Data.OrgGetDocumentsResult)
                                {
                                    objlist.Add(item);
                                }
                            }
                        }

                    }
                    else
                    {
                        objlist.Add(objDocumentsResult);
                        objOrgRequestdatalist[0].Data.OrgGetDocumentsResult = objlist;
                    }
                }
                else
                {
                    objlist.Add(objDocumentsResult);
                    objOrgRequestdatalist[0].Data.OrgGetDocumentsResult = objlist;
                }

                TempData["data"] = objOrgRequestdatalist;
                ViewData["OrgGetDocumentsResult"] = objlist;

                HttpCookie langCookie = Request.Cookies["culture"];
                langParams objlangParams = new langParams();
                objlangParams.lang = langCookie.Value;
                Doctypesinput ObjDoctypesinput = new Doctypesinput();
                ObjDoctypesinput.lang = langCookie.Value;
                ObjDoctypesinput.sOrgReqId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId.ToString();
                List<DocTypeslist> objDocTypes = new List<DocTypeslist>();
                //if (Session["DocTypeslist"] != null)
                //{
                //    objDocTypes = (List<DocTypeslist>)Session["DocTypeslist"];
                //}
                //else
                //{
                objDocTypes = objdataclass.doctypes(ObjDoctypesinput);
                // }


                Session["DocTypeslist"] = objDocTypes;
                ViewData["Data"] = objDocTypes;
                ViewBag.reqno = Session["requestnumber"].ToString();
                if (objOrgRequestdatalist[0].Data.OrgGetBasicResult != null)
                {
                    ViewBag.cname = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                }
                ViewBag.UserId = Session["UserId"].ToString();
                ViewBag.TokenId = Session["TokenId"].ToString();
                ViewBag.reqid1 = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;
            }
            catch (Exception ex)
            {
                WriteToLogFile(ex.Message, "uploadmethod");
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult CanCreateOrg(string UserId)
        {
            int ParentID = 0;

            if (!string.IsNullOrEmpty(UserId))//Session["UserId"] != null)
            {
                UserId = HttpUtility.UrlDecode(UserId);
                UserId = CommonFunctions.CsUploadDecrypt(UserId);
                ParentID = int.Parse(UserId);// int.Parse(Session["UserId"].ToString());
            }
            else
            {
                return RedirectToAction("Start", "registration");
            }

            ReqObj r = new ReqObj { ParentID = ParentID, ChildUser = 0 };
            DataTable dt = objdataclass.CanCreateOrg(r);

            return Json(new { Status = dt.Rows[0]["CanCreateOrg"].ToString() });

        }
        private void OrgTypeAndPermission(string UserId)
        {
            int ParentID = 0;

            ParentID = int.Parse(UserId);// int.Parse(Session["UserId"].ToString());

            ReqObj r = new ReqObj { ParentID = ParentID, ChildUser = 0 };
            DataTable dt = objdataclass.CanCreateOrg(r);
        }




        public ActionResult UploadDocument()
        {

            if (!OrgRegistrationAllowed())
                RedirectToAction("UnAuthorizedAction", "Registration");
            ViewBag.RedirectToOrgStatus = "Notsubmitted";
            Dataclass objdataclass = new Dataclass();
            List<OrgGetBasicResult> objlist = new List<OrgGetBasicResult>();
            DeclarationSearchByIdParams objOrgreg = new DeclarationSearchByIdParams();
            OrgGetBasicResult ObjOrgGetBasicResult = new OrgGetBasicResult();
            // WriteToLogFile("firstEntry", "test");
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }

                string s = Session["UploadOrgId"].ToString();
                //string xx = "";
                TempData["data"] = null;
                if (TempData["data"] == null)
                {
                    objOrgreg.mUserid = Session["UserId"].ToString();
                    objOrgreg.tokenId = Session["TokenId"].ToString();
                    objOrgreg.OrganizationId = Session["UploadOrgId"].ToString();// Need not be set -Siraj
                    objOrgreg.sOrgReqId = Session["UploadOrgId"].ToString();

                    objOrgRequestdatalist = objdataclass.OrgRegistration_Data(objOrgreg, "Status");//Need to handle for Approved details view -Siraj


                    TempData["data"] = objOrgRequestdatalist;
                    ViewBag.ReqNumber = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].RequestNumber.ToString();
                    ViewBag.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName.ToString();


                }

                //    WriteToLogFile("objOrgRequestdatalist", "BeforeUploadmethod");
                uploadmethod();

                //   WriteToLogFile("objOrgRequestdatalist", "AfetrUpload");
                DocTypes d = new DocTypes();
                BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();
                ObjbrokerRenwalModel.OrgReqId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId.ToString();


                if (TempData["data"] != null)
                {
                    WriteToLogFile("objOrgRequestdatalist", "SecondTempdata");

                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                    if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].Editable != null)
                    {
                        string edit = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].Editable.ToString();
                        if (edit != null)
                        {
                            if (edit == "0")
                            {
                                ObjbrokerRenwalModel.SubmitRequest = "false";
                                ObjbrokerRenwalModel.UploadDiv = "false";
                            }
                        }
                    }

                    //added newly Siraj
                    if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                    {
                        ObjbrokerRenwalModel.SubmitRequest = "true";
                        ObjbrokerRenwalModel.UploadDiv = "true";
                        ViewBag.dis = Constantclass.number1;
                    }
                    else
                    {
                        ObjbrokerRenwalModel.SubmitRequest = "false";
                        ObjbrokerRenwalModel.UploadDiv = "false";
                        ViewBag.dis = Constantclass.number;
                    }
                    TempData["data"] = TempData["data"];
                    TempData.Keep("data");

                    //string edit = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].Editable.ToString();

                    //WriteToLogFile("Editvalue", edit);
                    //if (edit != null)
                    //{
                    //    if (edit == "0")
                    //    {
                    //        ObjbrokerRenwalModel.SubmitRequest = "false";
                    //        ObjbrokerRenwalModel.UploadDiv = "false";
                    //    }
                    //}
                    var doctypes = Session["DocTypeslist"].ToString();

                    List<DocTypeslist> myobject = (List<DocTypeslist>)Session["DocTypeslist"];

                    DocTypeslist xtest = myobject.FirstOrDefault();

                    List<DocTypes> xmydoctype = xtest.DocTypes;

                    ObjbrokerRenwalModel.Referenceprofile = "OrganizationRequests";


                    // DataSet ds = objdataclass.FNGetBrokerDetailsForUpdate(ObjbrokerRenwalModel);



                    var DropdownBind = from x in xmydoctype.AsEnumerable()
                                       select new SelectListItem
                                       {
                                           //Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
                                           //Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
                                           //// Value = s["DeclarationDocumentId"].ToString(),

                                           //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)
                                           Text = x.Name,
                                           Value = CommonFunctions.CsUploadEncrypt(x.TypeId)

                                       };


                    ObjbrokerRenwalModel.ddlDocumentTypesitems = DropdownBind.ToList();

                }
                TempData.Keep("data");
                // TempData["data"] = TempData["data"];

                checksession();
                ViewBag.modelerror = Constantclass.number1;
                ViewBag.Title = Resources.Resource.UploadDocument_Header;




                CommonFunctions.LogUserActivity("UploadDocument", "", "", "", "", "");
                return View("UploadDocument", ObjbrokerRenwalModel);


            }
            catch (Exception ex)
            {
                WriteToLogFile(ex.Message + ex.StackTrace, "UploadDocument");
                ViewBag.modelerror = Constantclass.number5;
                //  ViewBag.Msg = Resources.Resource.somethingwentwrong;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = ex.StackTrace;
                ViewBag.Msg = ex.StackTrace;
                TempData.Keep("data");
                CommonFunctions.LogUserActivity("UploadDocument", "", "", "", "", ex.Message.ToString());
                return View();
            }
        }

        //public ActionResult UploadDocument()
        //{
        //    Dataclass objdataclass = new Dataclass();

        //    try
        //    {
        //        if (Session["UserId"] == null)
        //        {
        //            return RedirectToAction("Start", "registration");
        //        }

        //        string s = Session["UploadOrgId"].ToString();
        //        string xx = "";
        //        uploadmethod();
        //        DocTypes d = new DocTypes();
        //        BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();

        //        if (TempData["data"] != null)
        //        {
        //            objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

        //            string edit = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].Editable.ToString();
        //            if (edit != null)
        //            {
        //                if (edit == "0")
        //                {
        //                    ObjbrokerRenwalModel.SubmitRequest = "false";
        //                    ObjbrokerRenwalModel.UploadDiv = "false";
        //                }
        //            }
        //            var doctypes = Session["DocTypeslist"].ToString();

        //            List<DocTypeslist> myobject=(List<DocTypeslist>)Session["DocTypeslist"];

        //            DocTypeslist xtest = myobject.FirstOrDefault();

        //            List<DocTypes> xmydoctype = xtest.DocTypes ;

        //                ObjbrokerRenwalModel.Referenceprofile = "OrganizationRequests";


        //          // DataSet ds = objdataclass.FNGetBrokerDetailsForUpdate(ObjbrokerRenwalModel);



        //            var DropdownBind = from x in xmydoctype.AsEnumerable()
        //                               select new SelectListItem
        //                               {
        //                                   //Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
        //                                   //Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
        //                                   //// Value = s["DeclarationDocumentId"].ToString(),

        //                                   //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)
        //                                    Text= x.Name,
        //                                    Value= CommonFunctions.CsUploadEncrypt(x.TypeId)

        //                               };


        //            ObjbrokerRenwalModel.ddlDocumentTypesitems = DropdownBind.ToList();

        //        }
        //        TempData.Keep("data");
        //       // TempData["data"] = TempData["data"];

        //        checksession();
        //        ViewBag.modelerror = Constantclass.number1;
        //        ViewBag.Title = Resources.Resource.UploadDocument_Header;



        //        return View("UploadDocument", ObjbrokerRenwalModel);


        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.modelerror = Constantclass.number5;
        //        ViewBag.Msg = Resources.Resource.somethingwentwrong;
        //        return View();
        //    }
        //}
        public ActionResult UploadDocument1(string OrgReqDocId)
        {
            try
            {

                ViewBag.modelerror = Constantclass.number1;
                ViewBag.viewname = "Upload";
                //if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                //{
                //    ViewBag.dis = Constantclass.number1;
                //}
                //else
                //{
                //    ViewBag.dis = Constantclass.number;
                //}
                if (!OrgReqDocId.All(char.IsDigit))
                {
                    if (OrgReqDocId.Contains('%'))
                    {

                        OrgReqDocId = CommonFunctions.CsUploadDecrypt(System.Web.HttpUtility.UrlDecode(OrgReqDocId));
                    }
                    else
                    {
                        OrgReqDocId = CommonFunctions.CsUploadDecrypt(OrgReqDocId);


                    }
                }

                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                OrgReqResultDocInfoParams objdelete = new OrgReqResultDocInfoParams();
                objdelete.mUserid = Session["UserId"].ToString();
                objdelete.tokenId = Session["TokenId"].ToString();
                objdelete.sOrgReqDocId = OrgReqDocId;

                string stratus = objdataclass.Documentdelete(objdelete);

                List<OrgGetDocumentsResult> objlist = new List<OrgGetDocumentsResult>();
                List<OrgRequestlist> objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                int count = objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.Count;
                if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult != null)
                {
                    string deleteindex = string.Empty;
                    for (int i = 0; i <= count - 1; i++)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult[i].OrganizationRequestDocumentId == OrgReqDocId)
                        {
                            deleteindex = i.ToString();
                        }
                        else
                        {
                            objlist.Add(objOrgRequestdatalist[0].Data.OrgGetDocumentsResult[i]);
                        }
                    }
                    if (deleteindex != string.Empty)
                    {
                        objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.RemoveAt(Convert.ToInt32(deleteindex));
                    }
                    if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult == null)
                    {
                        // List<OrgGetDocumentsResult> objemtylist = new List<OrgGetDocumentsResult>();
                        OrgGetDocumentsResult objemty = new OrgGetDocumentsResult();
                        objemty.DocumentName = "";
                        //  objemtylist.Add(objemty);
                        //   objOrgRequestdatalist[0].Data.OrgGetDocumentsResult = objemtylist;
                        objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.Add(objemty);
                        objlist.Add(objemty);

                    }
                    else
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.Count < 1)
                        {
                            OrgGetDocumentsResult objemty = new OrgGetDocumentsResult();
                            objemty.DocumentName = "";
                            objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.Add(objemty);
                            objlist.Add(objemty);
                        }
                    }

                }
                HttpCookie langCookie = Request.Cookies["culture"];
                langParams objlangParams = new langParams();
                objlangParams.lang = langCookie.Value;
                Doctypesinput ObjDoctypesinput = new Doctypesinput();
                ObjDoctypesinput.lang = langCookie.Value;
                ObjDoctypesinput.sOrgReqId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId.ToString();
                List<DocTypeslist> objDocTypes = new List<DocTypeslist>();
                //if (TempData["DocTypeslist"] != null)
                //{
                //    objDocTypes = (List<DocTypeslist>)TempData["DocTypeslist"];
                //}
                //else
                //{
                objDocTypes = objdataclass.doctypes(ObjDoctypesinput);
                //  }
                ViewData["OrgGetDocumentsResult"] = objlist;
                TempData["data"] = objOrgRequestdatalist;
                TempData["DocTypeslist"] = objDocTypes;
                ViewData["Data"] = objDocTypes;
                ViewBag.reqno = Session["requestnumber"].ToString();
                if (objOrgRequestdatalist[0].Data.OrgGetBasicResult != null)
                {
                    ViewBag.cname = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                }
                ViewBag.UserId = Session["UserId"].ToString();
                ViewBag.TokenId = Session["TokenId"].ToString();
                ViewBag.reqid1 = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;
                ViewBag.modelerror = Constantclass.number1;
                checksession();


                //return View("UploadDocument");

                return RedirectToAction("UploadDocument");


            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;

                WriteToLogFile(e.Message + e.StackTrace, "UploadDocument1");

                return View("UploadDocument");
            }
        }

        [HttpPost]
        public ActionResult UploadDocument_Submit(OrgGetDocumentsResult obj)
        {
            try
            {
                //if (TempData["data"] != null)//to avoid update of first page details in Org Update Request. This block will overwrite the display option - Siraj
                //{
                //    List<OrgRequestlist> objOrgRequestdatalist1 = new List<OrgRequestlist>();
                //    objOrgRequestdatalist1 = (List<OrgRequestlist>)TempData["data"];
                //    if (Session["purpose"].ToString().Trim() == Constantclass.number1 && objOrgRequestdatalist1[0].Data.OrgGetBasicResult[0].OrganizationId != 0) //to avoid update of first page details in Org Update Request. This block will overwrite the display option - Siraj
                //    {
                //        //Session["purpose"] = Constantclass.number1;
                //        ViewBag.dis = Constantclass.number1;
                //    }
                //}
                List<OrgGetBasicResult> objlist = new List<OrgGetBasicResult>();
                DeclarationSearchByIdParams objOrgreg = new DeclarationSearchByIdParams();
                OrgGetBasicResult ObjOrgGetBasicResult = new OrgGetBasicResult();
                ViewBag.dis = Constantclass.number1;
                ViewBag.viewname = "Upload";
                ViewBag.modelerror = Constantclass.number1;
                List<OrgRequestlist> objOrgRequestdatalist = new List<OrgRequestlist>();
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                    TempData.Keep("data");// 16-07-2019
                }

                //CheckUploadDocs
                List<DocTypeslist> objDocTypes = new List<DocTypeslist>();
                List<string> objDocTypeserror = new List<string>();

                objOrgreg.mUserid = Session["UserId"].ToString();
                objOrgreg.tokenId = Session["TokenId"].ToString();
                objOrgreg.OrganizationId = Session["UploadOrgId"].ToString();
                objOrgreg.sOrgReqId = Session["UploadOrgId"].ToString();

                objOrgRequestdatalist = objdataclass.OrgRegistration_Data(objOrgreg, "Status");

                //added 16-07-2019 as after submit action , request details are not recollected
                ViewBag.ReqNumber = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].RequestNumber.ToString();
                ViewBag.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName.ToString();
                //Below lines to reassign org req details added on 16-07-2019 .. because after uploading docs thru api, the ajax upload call is not updating the docs collection in mvc side
                TempData["data"] = objOrgRequestdatalist;
                TempData.Keep("data");


                List<OrgRequestlist> objorgreddatalist = new List<OrgRequestlist>();

                if (Session["DocTypeslist"] != null)
                {
                    objDocTypes = (List<DocTypeslist>)Session["DocTypeslist"];
                    for (int i = 0; i < objDocTypes[0].DocTypes.Count; i++)
                    {
                        string docname = string.Empty;
                        if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult != null)
                        {
                            for (int j = 0; j < objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.Count; j++)
                            {
                                if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult[j].DocumentType != null)
                                {
                                    if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult[j].DocumentType.ToString().Trim() == objDocTypes[0].DocTypes[i].TypeId)
                                    {
                                        docname = "yes";
                                    }
                                }

                            }
                        }
                        if (docname == "")
                        {
                            objDocTypeserror.Add(objDocTypes[0].DocTypes[i].Name);
                        }

                    }
                    if (objDocTypeserror.Count > 0)
                    {
                        ViewBag.modelerror = Constantclass.number6;
                        ViewBag.Msg = "Docs";
                        ViewData["Dataerror"] = objDocTypeserror;
                    }

                }
                if (ViewBag.modelerror != "6")
                {
                    //CheckOrgEmailIsVerified
                    OrgReqResultInfoParams objOrgReqResultInfoParams = new OrgReqResultInfoParams();
                    objOrgReqResultInfoParams.mUserid = Session["UserId"].ToString();
                    objOrgReqResultInfoParams.tokenId = Session["TokenId"].ToString();
                    objOrgReqResultInfoParams.sOrgReqId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId.ToString();
                    string IsOrgEmailVarified = string.Empty;
                    DataTable dt = new DataTable();
                    // dt = objdataclass.CheckOrgEmailIsVerified(objOrgReqResultInfoParams);
                    //if (dt.Rows.Count>0)
                    //{
                    //     IsOrgEmailVarified = dt.Rows[0]["IsOrgEmailVarified"].ToString();

                    //}
                    if (IsOrgEmailVarified == Constantclass.number)
                    {
                        TempData["OrgId"] = objOrgReqResultInfoParams.sOrgReqId;
                        ViewBag.modelerror = Constantclass.number6;
                        ViewBag.Msg = "Email";
                    }
                    else
                    {
                        if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                        {
                            dt = objdataclass.SubmitOrgReq(objOrgReqResultInfoParams);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["Status"].ToString().Trim() == Constantclass.number)
                            {
                                ViewBag.modelerror = Constantclass.number5;
                                ViewBag.Msg = Resources.Resource.OrganizationRegCompletedsuccessfully;
                                ViewBag.RedirectToOrgStatus = "RedirectToOrgStatus";
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                WriteToLogFile(e.Message + e.StackTrace, "UploadDocument_submit");
                uploadmethod();
                checksession();
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("UploadDocument", "", "", "", "", e.Message.ToString());
                return View("UploadDocument");
            }



            uploadmethod();
            checksession();

            var doctypes = Session["DocTypeslist"].ToString();

            List<DocTypeslist> myobject = (List<DocTypeslist>)Session["DocTypeslist"];

            DocTypeslist xtest = myobject.FirstOrDefault();

            List<DocTypes> xmydoctype = xtest.DocTypes;
            BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();
            ObjbrokerRenwalModel.Referenceprofile = "OrganizationRequests";


            // DataSet ds = objdataclass.FNGetBrokerDetailsForUpdate(ObjbrokerRenwalModel);



            var DropdownBind = from x in xmydoctype.AsEnumerable()
                               select new SelectListItem
                               {
                                   //Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
                                   //Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
                                   //// Value = s["DeclarationDocumentId"].ToString(),

                                   //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)
                                   Text = x.Name,
                                   Value = CommonFunctions.CsUploadEncrypt(x.TypeId)

                               };


            ObjbrokerRenwalModel.ddlDocumentTypesitems = DropdownBind.ToList();

            ObjbrokerRenwalModel.OrgReqId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId.ToString();//added newly 09-10-2019 , orgreqid is coming null if all the docs are not uploaded

            CommonFunctions.LogUserActivity("UploadDocument", "", "", "", "", "");
            //CommonFunctions.LogUserActivity("UploadDocument", "", "", "", "", "Requestnumber=" + Session["requestnumber"].ToString() + " OrganizationId=" + objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationId.ToString());

            return View("UploadDocument", ObjbrokerRenwalModel);
            //   return RedirectToAction("UploadDocument");
        }
        //[HttpPost]
        //public ActionResult UploadDocument_Submit(OrgGetDocumentsResult obj)
        //{
        //    try
        //    {
        //        ViewBag.dis = Constantclass.number1;
        //        ViewBag.viewname = "Upload";
        //        ViewBag.modelerror = Constantclass.number1;
        //        List<OrgRequestlist> objOrgRequestdatalist = new List<OrgRequestlist>();
        //        if (TempData["data"] != null)
        //        {
        //            objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
        //        }

        //        //CheckUploadDocs
        //        List<DocTypeslist> objDocTypes = new List<DocTypeslist>();
        //        List<string> objDocTypeserror = new List<string>();
        //        if (Session["DocTypeslist"] != null)
        //        {
        //            objDocTypes = (List<DocTypeslist>)Session["DocTypeslist"];
        //            for (int i = 0; i < objDocTypes[0].DocTypes.Count; i++)
        //            {
        //                string docname = string.Empty;
        //                for (int j = 0; j < objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.Count; j++)
        //                {
        //                    if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult[j].DocumentType != null)
        //                    {
        //                        if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult[j].DocumentType.ToString().Trim() == objDocTypes[0].DocTypes[i].TypeId)
        //                        {
        //                            docname = "yes";
        //                        }
        //                    }

        //                }
        //                if (docname == "")
        //                {
        //                    objDocTypeserror.Add(objDocTypes[0].DocTypes[i].Name);
        //                }

        //            }
        //            if (objDocTypeserror.Count > 0)
        //            {
        //                ViewBag.modelerror = Constantclass.number6;
        //                ViewBag.Msg = "Docs";
        //                ViewData["Dataerror"] = objDocTypeserror;
        //            }

        //        }
        //        if (ViewBag.modelerror != "6")
        //        {
        //            //CheckOrgEmailIsVerified
        //            OrgReqResultInfoParams objOrgReqResultInfoParams = new OrgReqResultInfoParams();
        //            objOrgReqResultInfoParams.mUserid = Session["UserId"].ToString();
        //            objOrgReqResultInfoParams.tokenId = Session["TokenId"].ToString();
        //            objOrgReqResultInfoParams.sOrgReqId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId.ToString();
        //            string IsOrgEmailVarified = string.Empty;
        //            DataTable dt = new DataTable();
        //            // dt = objdataclass.CheckOrgEmailIsVerified(objOrgReqResultInfoParams);
        //            //if (dt.Rows.Count>0)
        //            //{
        //            //     IsOrgEmailVarified = dt.Rows[0]["IsOrgEmailVarified"].ToString();

        //            //}
        //            if (IsOrgEmailVarified == Constantclass.number)
        //            {
        //                TempData["OrgId"] = objOrgReqResultInfoParams.sOrgReqId;
        //                ViewBag.modelerror = Constantclass.number6;
        //                ViewBag.Msg = "Email";
        //            }
        //            else
        //            {
        //                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
        //                {
        //                    dt = objdataclass.SubmitOrgReq(objOrgReqResultInfoParams);
        //                }
        //                if (dt.Rows.Count > 0)
        //                {
        //                    if (dt.Rows[0]["Status"].ToString().Trim() == Constantclass.number)
        //                    {
        //                        ViewBag.modelerror = Constantclass.number5;
        //                        ViewBag.Msg = Resources.Resource.OrganizationRegCompletedsuccessfully;
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        uploadmethod();
        //        checksession();
        //        ViewBag.modelerror = Constantclass.number5;
        //        ViewBag.Msg = Resources.Resource.somethingwentwrong;
        //        return View("UploadDocument");
        //    }
        //    uploadmethod();
        //    checksession();

        //    var doctypes = Session["DocTypeslist"].ToString();

        //    List<DocTypeslist> myobject = (List<DocTypeslist>)Session["DocTypeslist"];

        //    DocTypeslist xtest = myobject.FirstOrDefault();

        //    List<DocTypes> xmydoctype = xtest.DocTypes;
        //    BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();
        //    ObjbrokerRenwalModel.Referenceprofile = "OrganizationRequests";


        //    // DataSet ds = objdataclass.FNGetBrokerDetailsForUpdate(ObjbrokerRenwalModel);



        //    var DropdownBind = from x in xmydoctype.AsEnumerable()
        //                       select new SelectListItem
        //                       {
        //                           //Text = s["Name"].ToString() + " (" + s["Cnt"] + ") ",
        //                           //Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())
        //                           //// Value = s["DeclarationDocumentId"].ToString(),

        //                           //  Selected = (s["DeclarationDocumentId"].ToString() == DocumentId ? true : false)
        //                           Text = x.Name,
        //                           Value = CommonFunctions.CsUploadEncrypt(x.TypeId)

        //                       };


        //    ObjbrokerRenwalModel.ddlDocumentTypesitems = DropdownBind.ToList();

        //    return View("UploadDocument",ObjbrokerRenwalModel);
        // //   return RedirectToAction("UploadDocument");
        //}
        [HttpPost]
        public ActionResult UploadDocument(OrgGetDocumentsResult obj)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                ViewBag.viewname = "Upload";
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;
                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                List<OrgGetDocumentsResult> objlist = new List<OrgGetDocumentsResult>();
                List<OrgRequestlist> objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                if (objOrgRequestdatalist[0].Data.OrgGetDocumentsResult != null)
                {
                    objOrgRequestdatalist[0].Data.OrgGetDocumentsResult.Add(obj);
                }
                ViewData["OrgGetDocumentsResult"] = objlist;
                TempData["data"] = objOrgRequestdatalist;

                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var pic = System.Web.HttpContext.Current.Request.Files["Images"];
                    var DocumentName = System.Web.HttpContext.Current.Request.Files["DocumentName"];
                    HttpPostedFileBase filebase = new HttpPostedFileWrapper(pic);
                    var fileName = Path.GetFileName(filebase.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/UploadFile/"), fileName);
                    filebase.SaveAs(path);
                    //  return Json("File Saved Successfully.");
                }
                else { return Json("No File Saved."); }
            }
            catch (Exception e)
            {
                String _Exception = e.ToString();
                return Json("Error While Saving.");
            }
            checksession();
            return View();
        }

        public ActionResult OrganizationsDetail()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Start", "registration");
            }
            DataTable dtmyorglist = new DataTable();
            List<OrganizationDetail> od = new List<OrganizationDetail>();

            SecurityParams objmyorglist = new SecurityParams();
            try
            {
                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();
                dtmyorglist = objdataclass.MyOrganizations_Data(objmyorglist);

                //List<SelectListItem> Organizations = new List<SelectListItem>();

                foreach (DataRow row in dtmyorglist.Rows)
                {
                    od.Add(new OrganizationDetail
                    {
                        OrgId = encrypt.Encode(CommonFunctions.CsUploadEncrypt(row["OrganizationId"].ToString())),
                        OrgReqId = encrypt.Encode(CommonFunctions.CsUploadEncrypt("empty")),
                        OrgName = encrypt.Encode(CommonFunctions.CsUploadEncrypt(row["Name"].ToString())),
                        IsMainCompany = Convert.ToBoolean(row["IsMainCompany"])
                    });

                    //Organizations.Add(new SelectListItem()
                    //{
                    //    Text = row["Name"].ToString(),
                    //    Value = CommonFunctions.CsUploadEncrypt( row["OrganizationId"].ToString())
                    //});
                }

                //OrganizationDetail od = new OrganizationDetail();
                //od.Organizations = Organizations;

                return View(od);
            }
            catch (Exception e)
            {
                String _Exception = e.ToString();
                return View(od);

            }

        }
        public ActionResult OrganizationDocuments(string OrgId, string OrgReqId, string OrgName)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Start", "registration");
            }
            OrganizationDocument Od = new OrganizationDocument();
            try
            {
                Od = GetOrganizationDocumentDetails(OrgId, OrgName, OrgReqId);

                return View(Od);
            }
            catch (Exception e)
            {
                String _Exception = e.ToString();
                return View(Od);
            }
        }
        [HttpPost]
        public ActionResult OrganizationDocuments(OrganizationDocument Document)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Start", "registration");
            }
            OrganizationDocument Od = new OrganizationDocument();
            try
            {
                Od = GetOrganizationDocumentDetails(Document.OrgId, Document.OrgReqId, Document.OrgName);

                return View(Od);
            }
            catch (Exception e)
            {
                String _Exception = e.ToString();
                return View(Od);
            }
        }

        public OrganizationDocument GetOrganizationDocumentDetails(string OrgId, string OrgName, string OrgReqId)// = "empty")
        {
            OrganizationDocument Od = new OrganizationDocument();
            Od.OrgName = CommonFunctions.CsUploadDecrypt(encrypt.Decode(OrgName));
            Od.OrgId = CommonFunctions.CsUploadDecrypt(encrypt.Decode(OrgId));
            Od.OrgReqId = CommonFunctions.CsUploadDecrypt(encrypt.Decode(OrgReqId));// OrgReqId != "empty"? CommonFunctions.CsUploadDecrypt(encrypt.Decode(OrgReqId)): "empty";

            ViewBag.UserId = Session["UserId"].ToString();
            ViewBag.TokenId = Session["TokenId"].ToString();
            ViewBag.reqid1 = Od.OrgReqId == "empty" ? Od.OrgId : Od.OrgReqId;

            Doctypesinput ObjDoctypesinput = new Doctypesinput();
            HttpCookie langCookie = Request.Cookies["culture"];

            ObjDoctypesinput.lang = langCookie.Value;
            ObjDoctypesinput.sOrgReqId = Od.OrgReqId;
            ObjDoctypesinput.sOrgId = Od.OrgId;
            List<DocTypeslist> DocTypesList = new List<DocTypeslist>();

            DocTypesList = objdataclass.OrgDoctypes(ObjDoctypesinput);

            DocTypeslist FirstDocTypesList = DocTypesList.FirstOrDefault();

            List<DocTypes> DocTypes = FirstDocTypesList.DocTypes;

            var DropdownBind = from x in DocTypes.AsEnumerable()
                               select new SelectListItem
                               {
                                   Text = x.Name,
                                   Value = CommonFunctions.CsUploadEncrypt(x.TypeId)
                               };

            Od.DocumentTypeList = DropdownBind.ToList();

            List<SelectListItem> LicenceTypeList = new List<SelectListItem>();
            LicenceTypeList.Add(new SelectListItem
            {
                Text = Resources.Resource.LicensetypeRequired,//"Please select a License Type",//يرجى اختيار نوع الترخيص
                Value = "UnSelected"//"18"
            });
            LicenceTypeList.Add(new SelectListItem
            {
                Text = Resources.Resource.Importer_RB_permanent,// "Permanent",
                Value = Resources.Resource.Importer_RB_permanent//"Permanent"//"18"
            });
            LicenceTypeList.Add(new SelectListItem
            {
                Text = Resources.Resource.Importer_RB_temporary,// "Temporary",
                Value = Resources.Resource.Importer_RB_temporary//"Temporary"//"19"
            });
            Od.LicenceTypeList = LicenceTypeList;

            GetOrganizationDocuments GO = new GetOrganizationDocuments()
            {
                mUserid = Session["UserId"].ToString(),
                OrganizationId = Od.OrgId == "empty" ? "" : Od.OrgId,
                OrganizationRequestId = Od.OrgReqId == "empty" ? "" : Od.OrgReqId,
                tokenId = ""
            };

            List<OrganizationDocumentDetail> OrganizationDocumentDetailList = new List<OrganizationDocumentDetail>();
            DataTable dt = objdataclass.GetOrganizationDocuments(GO);
            if (dt != null)
            {
                var a = from x in dt.AsEnumerable()
                        select new OrganizationDocumentDetail
                        {
                            OrganizationRequestDocumentId = x["OrganizationRequestDocumentId"].ToString(),
                            DocumentId = x["DocumentId"].ToString(),
                            DocumentTypeCode = x["DocumentTypeCode"].ToString(),
                            DocumentName = x["DocumentName"].ToString(),
                            LicenseNumber = x["LicenseNumber"].ToString(),
                            CreatedDate = x["DateCreated"].ToString(),
                            LicenseType = x["LicenseType"].ToString() == "18" ? Resources.Resource.Importer_RB_permanent : (x["LicenseType"].ToString() == "19" ? Resources.Resource.Importer_RB_temporary : ""),
                            IssuanceDate = x["IssuanceDate"].ToString(),
                            ExpiryDate = x["ExpiryDate"].ToString()
                        };

                OrganizationDocumentDetailList = a.ToList();
            }
            Od.OrganizationDocuments = OrganizationDocumentDetailList;

            List<SelectListItem> IssuerList = new List<SelectListItem>();
            IssuerList.Add(new SelectListItem
            {
                Text = Resources.Resource.LicenseIssuerRequired,// "Please select a License Issuer",//يرجى اختيار مصدر الترخيص
                Value = "UnSelected"//"18"
            });
            IssuerList.Add(new SelectListItem
            {
                Text = "MOCI",
                Value = "MOCI"//"18"
            });
            IssuerList.Add(new SelectListItem
            {
                Text = Resources.Resource.SubEpayment_Others,// "Other",
                Value = Resources.Resource.SubEpayment_Others//"Other"//"19"
            });
            Od.IssuerList = IssuerList;

            return Od;
        }

        public ActionResult DeleteOrgDocument(string OrgReqDocId, string OrgId, string OrgReqId, string OrgName)
        {
            string OrgReqDocIdOrig = OrgReqDocId, OrgIdOrig = OrgId, OrgReqIdOrig = OrgReqId, OrgNameOrig = OrgName;
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                ViewBag.modelerror = Constantclass.number1;
                ViewBag.viewname = "Upload";


                OrgReqDocId = CommonFunctions.CsUploadDecrypt(encrypt.Decode(OrgReqDocId));
                OrgId = CommonFunctions.CsUploadDecrypt(encrypt.Decode(OrgId));
                OrgReqId = CommonFunctions.CsUploadDecrypt(encrypt.Decode(OrgReqId));
                OrgName = CommonFunctions.CsUploadDecrypt(encrypt.Decode(OrgName));



                OrgReqResultDocInfoParams objdelete = new OrgReqResultDocInfoParams();
                objdelete.mUserid = Session["UserId"].ToString();
                objdelete.tokenId = Session["TokenId"].ToString();
                objdelete.sOrgReqDocId = OrgReqDocId;

                string stratus = objdataclass.Documentdelete(objdelete);


                return RedirectToAction("OrganizationDocuments", "User", new { OrgId = OrgIdOrig, OrgReqId = OrgReqIdOrig, OrgName = OrgNameOrig });

            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;

                WriteToLogFile(e.Message + e.StackTrace, "UploadDocument1");

                return RedirectToAction("OrganizationDocuments", "User", new { OrgId = OrgIdOrig, OrgReqId = OrgReqIdOrig, OrgName = OrgNameOrig });
            }
        }

        public ActionResult OrganizationProfile()//(List<OrgRequestlist> OrganizationProfile)
        {
            //return View(OrganizationProfile);
            return View();
        }

        public ActionResult Org_RequestStatus()
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                ViewBag.viewname = "Reqstatus";
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                Session["returnurl"] = null;
                TempData.Clear();
                DataTable dtmyorglist = new DataTable();
                SecurityParams objmyorglist = new SecurityParams();
                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();
                dtmyorglist = objdataclass.OrgReqRequest_Status_Data(objmyorglist);
                checksession();
                return View(dtmyorglist);
            }
            catch (Exception e)
            {
                String _Exception = e.ToString();
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        public ActionResult payment()
        {
            try
            {
                ViewBag.viewname = "Paymentsearch";
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                TempData.Clear();
                //payment list
                List<EPaymentRequestDetails> objlist = new List<EPaymentRequestDetails>();
                EPaymentRequestDetails ObjEPaymentRequestDetails = new EPaymentRequestDetails();
                Epaymentlist objmypaylist = new Epaymentlist();
                HttpCookie langCookie = Request.Cookies["culture"];
                objmypaylist.lang = langCookie.Value;
                objmypaylist.mUserid = Session["UserId"].ToString();
                objmypaylist.tokenId = Session["TokenId"].ToString();
                objmypaylist.pagenumber = Constantclass.number1;
                List<OrgRequestlist> objOrgRequestdatalist = objdataclass.Payment_Datalist1(objmypaylist);
                if (objOrgRequestdatalist[0].Data != null)
                {
                    if (objOrgRequestdatalist[0].Data.EPaymentRequestDetails == null)
                    {

                        objlist.Add(ObjEPaymentRequestDetails);

                    }
                    else
                    {
                        if (objOrgRequestdatalist[0].Data.EPaymentRequestDetails.Count < 1)
                        {
                            objlist.Add(ObjEPaymentRequestDetails);
                        }
                    }
                    if (objOrgRequestdatalist[0].Data.EPaymentRequestDetails != null)
                    {
                        foreach (var item in objOrgRequestdatalist[0].Data.EPaymentRequestDetails)
                        {
                            item.RequstNoenyp = encrypt.Encode(item.RequstNo.ToString());
                            objlist.Add(item);
                        }
                    }
                }
                else
                {
                    objlist.Add(ObjEPaymentRequestDetails);
                }
                ViewData["data"] = objlist;

                //dropdown list
                paymentsearchinput objpaymentsearchinput = new paymentsearchinput();
                objpaymentsearchinput.lang = langCookie.Value;
                objpaymentsearchinput.mUserid = Session["UserId"].ToString();
                objpaymentsearchinput.tokenId = Session["TokenId"].ToString();
                DataTable dtpaysearchtypes = new DataTable();
                dtpaysearchtypes = objdataclass.paymentsearchtypes(objpaymentsearchinput);
                List<Epaymentlist> objEpaymentlist = new List<Epaymentlist>();
                List<SelectListItem> items = new List<SelectListItem>();
                if (dtpaysearchtypes.Rows.Count > 0)
                {
                    for (int i = 0; i < dtpaysearchtypes.Rows.Count; i++)
                    {
                        SelectListItem s = new SelectListItem();
                        s.Text = dtpaysearchtypes.Rows[i]["Text"].ToString();
                        s.Value = dtpaysearchtypes.Rows[i]["Value"].ToString();
                        items.Add(s);
                    }
                }
                SelectListItem objs = new SelectListItem();
                objs.Text = Resources.Resource.SelectOption;
                objs.Value = Constantclass.number;
                items.Insert(0, objs);
                ViewBag.searchtype = Resources.Resource.SelectOption;
                ViewBag.searchtypeid = Constantclass.number;

                Epaymentlist objpaymentsearchtypes = new Epaymentlist();
                objpaymentsearchtypes.searchtypes = items;
                objEpaymentlist.Add(objpaymentsearchtypes);
                ViewData["paynentsearchtypes"] = objEpaymentlist;
                TempData["paymentsearchtypes"] = objpaymentsearchtypes;
                checksession();
                CommonFunctions.LogUserActivity("payment", "", "", "", "", "");
                return View(objpaymentsearchtypes);
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("payment", "", "", "", "", e.Message.ToString());
                return View();
            }
        }
        [HttpPost]
        public ActionResult payment(Epaymentlist ObjEpaymentlist)
        {
            try
            {
                ViewBag.viewname = "Paymentsearch";
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                List<EPaymentRequestDetails> objlist = new List<EPaymentRequestDetails>();
                EPaymentRequestDetails ObjEPaymentRequestDetails = new EPaymentRequestDetails();
                HttpCookie langCookie = Request.Cookies["culture"];
                ObjEpaymentlist.lang = langCookie.Value;
                ObjEpaymentlist.mUserid = Session["UserId"].ToString();
                ObjEpaymentlist.tokenId = Session["TokenId"].ToString();
                ObjEpaymentlist.pagenumber = Constantclass.number1;
                List<OrgRequestlist> objOrgRequestdatalist = objdataclass.Payment_Datalist1(ObjEpaymentlist);
                if (objOrgRequestdatalist[0].Data != null)
                {
                    if (objOrgRequestdatalist[0].Data.EPaymentRequestDetails == null)
                    {

                        objlist.Add(ObjEPaymentRequestDetails);

                    }
                    else
                    {
                        if (objOrgRequestdatalist[0].Data.EPaymentRequestDetails.Count < 1)
                        {
                            objlist.Add(ObjEPaymentRequestDetails);
                        }
                    }
                    if (objOrgRequestdatalist[0].Data.EPaymentRequestDetails != null)
                    {
                        foreach (var item in objOrgRequestdatalist[0].Data.EPaymentRequestDetails)
                        {
                            item.RequstNoenyp = encrypt.Encode(item.RequstNo.ToString());
                            objlist.Add(item);
                        }
                    }
                }
                else
                {
                    objlist.Add(ObjEPaymentRequestDetails);
                }
                //dropdown list
                paymentsearchinput objpaymentsearchinput = new paymentsearchinput();
                objpaymentsearchinput.lang = langCookie.Value;
                objpaymentsearchinput.mUserid = Session["UserId"].ToString();
                objpaymentsearchinput.tokenId = Session["TokenId"].ToString();
                DataTable dtpaysearchtypes = new DataTable();
                dtpaysearchtypes = objdataclass.paymentsearchtypes(objpaymentsearchinput);
                List<Epaymentlist> objEpayment = new List<Epaymentlist>();
                List<SelectListItem> items = new List<SelectListItem>();
                string stype = string.Empty;
                if (dtpaysearchtypes.Rows.Count > 0)
                {
                    for (int i = 0; i < dtpaysearchtypes.Rows.Count; i++)
                    {
                        SelectListItem s = new SelectListItem();
                        s.Text = dtpaysearchtypes.Rows[i]["Text"].ToString();
                        s.Value = dtpaysearchtypes.Rows[i]["Value"].ToString();
                        items.Add(s);
                        if (ObjEpaymentlist.searchDropdown == dtpaysearchtypes.Rows[i]["Value"].ToString().Trim())
                        {
                            stype = dtpaysearchtypes.Rows[i]["Text"].ToString();
                        }
                    }
                }
                SelectListItem objs = new SelectListItem();
                objs.Text = Resources.Resource.SelectOption;
                objs.Value = Constantclass.number;
                items.Insert(0, objs);
                ViewBag.searchtype = stype;
                ViewBag.searchtypeid = ObjEpaymentlist.searchDropdown;

                ObjEpaymentlist.searchtypes = items;
                objEpayment.Add(ObjEpaymentlist);
                ViewData["paynentsearchtypes"] = objEpayment;
                TempData["paymentsearchtypes"] = ObjEpaymentlist;
                TempData["searchDropdown"] = ObjEpaymentlist.searchDropdown;
                TempData["searchCriteria"] = ObjEpaymentlist.searchCriteria;
                ViewData["data"] = objlist;
                checksession();
                CommonFunctions.LogUserActivity("payment", "", "", "", "", "");
                return View(ObjEpaymentlist);
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("payment", "", "", "", "", e.Message.ToString());
                return View();
            }
        }
        [HttpPost]
        public JsonResult paymentclient(string pageIndex)
        {
            try
            {
                ViewBag.viewname = "Paymentsearch";
                ViewBag.modelerror = Constantclass.number1;
                //if (Session["pageIndex"] != null)
                //{
                //    pageIndex = (Convert.ToInt32(Session["pageIndex"]) + 1).ToString();
                //    Session["pageIndex"] = Convert.ToInt32(pageIndex);
                //}
                //else
                //{
                //    Session["pageIndex"] = 2;
                //    pageIndex = Session["pageIndex"].ToString();
                //}
                List<EPaymentRequestDetails> objlist = new List<EPaymentRequestDetails>();
                EPaymentRequestDetails ObjEPaymentRequestDetails = new EPaymentRequestDetails();
                Epaymentlist objmypaylist = new Epaymentlist();
                HttpCookie langCookie = Request.Cookies["culture"];
                objmypaylist.lang = langCookie.Value;
                objmypaylist.mUserid = Session["UserId"].ToString();
                objmypaylist.tokenId = Session["TokenId"].ToString();
                if (TempData["searchDropdown"] != null)
                {
                    objmypaylist.searchDropdown = TempData["searchDropdown"].ToString();
                    TempData["searchDropdown"] = objmypaylist.searchDropdown;
                }
                if (TempData["searchCriteria"] != null)
                {
                    objmypaylist.searchDropdown = TempData["searchCriteria"].ToString();
                    TempData["searchCriteria"] = objmypaylist.searchDropdown;
                }
                if (pageIndex == null)
                {
                    pageIndex = Constantclass.number1;
                }
                objmypaylist.pagenumber = pageIndex.ToString();
                List<OrgRequestlist> objOrgRequestdatalist = objdataclass.Payment_Datalist1(objmypaylist);
                if (objOrgRequestdatalist[0].Data != null)
                {
                    if (objOrgRequestdatalist[0].Data.EPaymentRequestDetails == null)
                    {

                        objlist.Add(ObjEPaymentRequestDetails);

                    }
                    else
                    {
                        if (objOrgRequestdatalist[0].Data.EPaymentRequestDetails.Count < 1)
                        {
                            objlist.Add(ObjEPaymentRequestDetails);
                        }
                    }
                    if (objOrgRequestdatalist[0].Data.EPaymentRequestDetails != null)
                    {
                        foreach (var item in objOrgRequestdatalist[0].Data.EPaymentRequestDetails)
                        {
                            item.RequstNoenyp = encrypt.Encode(item.RequstNo.ToString());
                            objlist.Add(item);
                        }
                    }
                }
                else
                {
                    objlist.Add(ObjEPaymentRequestDetails);
                }

                return Json(objlist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult EPayment(string Id)
        {
            try
            {
                ViewBag.viewname = "Paymentsearch";
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                double num;
                if (!(double.TryParse(Id, out num)))
                {
                    Id = encrypt.Decode(Id);
                }

                List<OrgRequestlist> objData = new List<OrgRequestlist>();
                List<EPaymentRequestInfo> objpayinfo = new List<EPaymentRequestInfo>();
                EPaymentRequestInfoParams objmypaylistinfo = new EPaymentRequestInfoParams();
                HttpCookie langCookie = Request.Cookies["culture"];
                objmypaylistinfo.lang = langCookie.Value;
                objmypaylistinfo.mUserid = Session["UserId"].ToString();
                objmypaylistinfo.tokenId = Session["TokenId"].ToString();
                objmypaylistinfo.RequestNo = Id;
                objData = objdataclass.Payment_Datainfo(objmypaylistinfo);

                EPaymentRequestInfo EPaymentRequestInfoemty = new EPaymentRequestInfo();
                if (objData[0].Data != null)
                {
                    if (objData[0].Data.EPaymentRequestInfo != null)
                    {
                        EPaymentRequestInfoemty = objData[0].Data.EPaymentRequestInfo[0];
                        objpayinfo.Add(EPaymentRequestInfoemty);
                    }
                    else
                    {
                        objpayinfo.Add(EPaymentRequestInfoemty);
                    }

                }
                else
                {
                    objpayinfo.Add(EPaymentRequestInfoemty);
                }
                ViewData["data"] = objpayinfo;

                Session["tokenCallCsPayment"] = objpayinfo[0].OPTokenId;

                TempData["EPaymentRequestInfo"] = EPaymentRequestInfoemty;
                checksession();
                CommonFunctions.LogUserActivity("Epayment", "", "", "", "", "EpaymentsRequestNo'" + Id + "'");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("Epayment", "", "", "", "", e.Message.ToString());
                return View();
            }
        }
        public ActionResult DenyPayRequest()
        {
            try
            {
                ViewBag.viewname = "Paymentsearch";
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                List<EPaymentRequestInfo> objpayinfo = new List<EPaymentRequestInfo>();
                EPaymentRequestInfo ObjEPayment = new EPaymentRequestInfo();
                if (TempData["EPaymentRequestInfo"] != null)
                {
                    ObjEPayment = (EPaymentRequestInfo)TempData["EPaymentRequestInfo"];
                    TempData["EPaymentRequestInfo"] = ObjEPayment;
                }

                DataTable dt = new DataTable();
                EPaymentRequestInfoParams objmypaylistinfo = new EPaymentRequestInfoParams();
                HttpCookie langCookie = Request.Cookies["culture"];
                objmypaylistinfo.lang = langCookie.Value;
                objmypaylistinfo.mUserid = Session["UserId"].ToString();
                objmypaylistinfo.tokenId = Session["TokenId"].ToString();
                objmypaylistinfo.RequestNo = ObjEPayment.RequestNo;
                dt = objdataclass.DenyPayRequest(objmypaylistinfo);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString().Trim() == Constantclass.number)
                    {
                        ViewBag.modelerror = Constantclass.number5;
                        ViewBag.Msg = Resources.Resource.Denied;
                        ObjEPayment.STATUS = "Denied";
                        ViewBag.Status = "Denied";
                    }
                    else
                    {
                        ViewBag.modelerror = Constantclass.number5;
                        ViewBag.Msg = "Worng";
                    }
                }
                else
                {
                    ViewBag.modelerror = Constantclass.number5;
                    ViewBag.Msg = "This module is under development";
                }
                objpayinfo.Add(ObjEPayment);
                ViewData["data"] = objpayinfo;
                TempData["EPaymentRequestInfo"] = ObjEPayment;
                checksession();
                CommonFunctions.LogUserActivity("DenyPayRequest", "", "", "", "", "");
                return View("EPayment");
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("DenyPayRequest", "", "", "", "", e.Message.ToString());
                return View("EPayment");
            }
        }
        [HttpPost]
        public ActionResult EPayment(CallbackRedirectInfo ObjCallbackRedirectInfo)
        {

            string urltoredirectforpayments = ConfigurationManager.AppSettings["csPaymentURL"].ToString();

            string redirecturl = ConfigurationManager.AppSettings["ApplicationUrl"].ToString() + "/user/payment";/// Request.Url.ToString();

            //  string previous = Request.UrlReferrer;

            try
            {
                ViewBag.viewname = "Paymentsearch";
                ViewBag.modelerror = Constantclass.number1;
                List<EPaymentRequestInfo> objpayinfo = new List<EPaymentRequestInfo>();
                EPaymentRequestInfo ObjEPayment = new EPaymentRequestInfo();
                if (TempData["EPaymentRequestInfo"] != null)
                {
                    ObjEPayment = (EPaymentRequestInfo)TempData["EPaymentRequestInfo"];
                    TempData["EPaymentRequestInfo"] = ObjEPayment;
                }
                DataTable dt = new DataTable();

                string x = string.Empty;
                EPaymentRequestInfo ObjEPaymentRequestInfo = new EPaymentRequestInfo();
                if (TempData["EPaymentRequestInfo"] != null)
                {
                    ObjEPaymentRequestInfo = (EPaymentRequestInfo)TempData["EPaymentRequestInfo"];
                    ObjCallbackRedirectInfo.mUserid = Session["UserId"].ToString();
                    ObjCallbackRedirectInfo.tokenId = Session["TokenId"].ToString();
                    ObjCallbackRedirectInfo.OPTokenId = ObjEPaymentRequestInfo.OPTokenId;
                    ObjCallbackRedirectInfo.EpayReqNo = ObjEPaymentRequestInfo.RequestNo;
                    ObjCallbackRedirectInfo.RedirectUrl = redirecturl;

                }


                x = objdataclass.EPaymentOnCallbackRedirect(ObjCallbackRedirectInfo);

                if (x == "1")
                {
                    return Redirect(urltoredirectforpayments + ObjEPaymentRequestInfo.OPTokenId);
                }

                else
                {


                }
                //   Response.Redirect()
                //if (dt.Rows.Count > 0)
                //{
                //    if (dt.Rows[0]["Status"].ToString().Trim() == Constantclass.number)
                //    {
                //        ViewBag.modelerror = Constantclass.number5;
                //        ViewBag.Msg = "Success";

                //        return Redirect(urltoredirectforpayments + ObjEPaymentRequestInfo.OPTokenId);
                //    }
                //    else
                //    {
                //        ViewBag.modelerror = Constantclass.number5;
                //        ViewBag.Msg = "Worng";
                //    }
                //}
                //else
                //{
                //    ViewBag.modelerror = Constantclass.number5;
                //    ViewBag.Msg = "This module is under development";
                //}
                objpayinfo.Add(ObjEPayment);
                ViewData["data"] = objpayinfo;
                TempData["EPaymentRequestInfo"] = ObjEPayment;
                CommonFunctions.LogUserActivity("EpaymentPost", "", "", "", "", "TheEpayRequestNumber'" + ObjEPaymentRequestInfo.RequestNo + "'");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("Epayment", "", "", "", "", e.Message.ToString());
                return View();
            }
        }
        public ContentResult paymentprint(string pageIndex)
        {
            try
            {
                ViewBag.viewname = "Paymentsearch";
                ViewBag.modelerror = Constantclass.number1;
                string res = string.Empty;
                paymentprint Objpaymentprint = new paymentprint();
                List<EPaymentRequestInfo> objpayinfo = new List<EPaymentRequestInfo>();
                EPaymentRequestInfo ObjEPaymentRequestInfo = new EPaymentRequestInfo();
                if (TempData["EPaymentRequestInfo"] != null)
                {
                    ObjEPaymentRequestInfo = (EPaymentRequestInfo)TempData["EPaymentRequestInfo"];
                    Objpaymentprint.mUserid = Session["UserId"].ToString();
                    Objpaymentprint.tokenId = Session["TokenId"].ToString();
                    Objpaymentprint.OPTokenId = ObjEPaymentRequestInfo.OPTokenId;
                    Objpaymentprint.PaymentStatus = ObjEPaymentRequestInfo.STATUS;
                    res = objdataclass.Paymentprint(Objpaymentprint);
                }
                TempData["EPaymentRequestInfo"] = ObjEPaymentRequestInfo;
                var jsonser = new JavaScriptSerializer();

                var obj = jsonser.Deserialize<dynamic>(res);
                foreach (var x in obj)
                {

                    res = x.Value;

                }
                //JsonResult result = new JsonResult()
                //{
                //    Data = JsonConvert.SerializeObject(res)
                //};

                return new ContentResult
                {
                    Content = res
                };

                //var htmlLoadOptions = LoadOptions.HtmlDefault;
                //using (var htmlStream = new MemoryStream(htmlLoadOptions.Encoding.GetBytes(strHTML)))
                //    DocumentModel.Load(htmlStream, htmlLoadOptions).Print();

                //   return Content;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ContentResult Requestprint(string RequestNumber)
        {
            RequestNumber = "";
            try
            {

                //     WriteToLogFile("Camehere", Request.QueryString["RequestNumber"].ToString());
                if (Request.QueryString["RequestNumber"] != null && Request.QueryString["Orgid"] == null)
                {
                    RequestNumber = CommonFunctions.CsUploadDecrypt(Request.QueryString["RequestNumber"].ToString());
                    if (RequestNumber == "")
                    {

                        string source = Request.RawUrl.ToString();
                        string split = "RequestNumber=";


                        string result = source.Substring(source.IndexOf(split) + split.Length);


                        RequestNumber = CommonFunctions.CsUploadDecrypt(result);


                        CommonFunctions.LogUserActivity("RequestPrint", "", "", "", "", "RequestNumber'" + RequestNumber + "'");


                    }
                }
                ViewBag.viewname = "Paymentsearch";
                ViewBag.modelerror = Constantclass.number1;
                string res = string.Empty;
                BrokerUpdateModel Bu = new BrokerUpdateModel();

                Bu.RequestNumber = RequestNumber;



                //   if (TempData["EPaymentRequestInfo"] != null)
                {


                    //   WriteToLogFile("2", Request.QueryString["RequestNumber"].ToString());
                    res = objdataclass.Requestprint(Bu);

                    //      WriteToLogFile("3", Request.QueryString["RequestNumber"].ToString());

                }
                //    TempData["EPaymentRequestInfo"] = ObjEPaymentRequestInfo;




                var jsonser = new JavaScriptSerializer();


                var obj = jsonser.Deserialize<dynamic>(res);
                //   WriteToLogFile("4", obj);

                foreach (var x in obj)
                {

                    res = x.Value;

                }
                //JsonResult result = new JsonResult()
                //{
                //    Data = JsonConvert.SerializeObject(res)
                //};

                //   WriteToLogFile("4", res.ToString());
                return new ContentResult
                {
                    Content = res

                };



                //var htmlLoadOptions = LoadOptions.HtmlDefault;
                //using (var htmlStream = new MemoryStream(htmlLoadOptions.Encoding.GetBytes(strHTML)))
                //    DocumentModel.Load(htmlStream, htmlLoadOptions).Print();

                //   return Content;
            }
            catch (Exception ex)
            {
                WriteToLogFile(ex.StackTrace, ex.Message);

                throw ex;
            }
        }
        public ActionResult OrgRequestprint(string OrgReqId)//OrgReqId
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Start", "Registration");
            }
            //RequestNumber = "";

            try
            {
                //WriteToLogFile("Camehere", Request.QueryString["RequestNumber"].ToString());
                //if (Request.QueryString["RequestNumber"] != null && Request.QueryString["Orgid"] == null)
                //{
                //    RequestNumber = CommonFunctions.CsUploadDecrypt(Request.QueryString["RequestNumber"].ToString());
                //    if (RequestNumber == "")
                //    {
                //        string source = Request.RawUrl.ToString();
                //        string split = "RequestNumber=";
                //        string result = source.Substring(source.IndexOf(split) + split.Length);
                //        RequestNumber = CommonFunctions.CsUploadDecrypt(result);
                //    }
                //}
                if (!string.IsNullOrWhiteSpace(OrgReqId))
                {
                    OrgReqId = CommonFunctions.CsUploadDecrypt(OrgReqId);
                }
                else
                {
                    return RedirectToAction("RequestListfortheuser", "Request");
                }
                //ViewBag.viewname = "Paymentsearch";
                //ViewBag.modelerror = Constantclass.number1;
                string res = string.Empty;

                OrgReqResultInfoParams RP = new OrgReqResultInfoParams();
                RP.mUserid = Session["UserId"].ToString();
                RP.sOrgReqId = OrgReqId;

                //WriteToLogFile("2", Request.QueryString["RequestNumber"].ToString());
                res = objdataclass.OrgRequestprint(RP);

                //WriteToLogFile("3", Request.QueryString["RequestNumber"].ToString());

                var jsonser = new JavaScriptSerializer();


                var obj = jsonser.Deserialize<dynamic>(res);

                foreach (var x in obj)
                {
                    res = x.Value;
                }
                return new ContentResult
                {
                    Content = res

                };
            }
            catch (Exception ex)
            {
                WriteToLogFile(ex.StackTrace, ex.Message);
                //return Content("<h3>Some issue has occured. Please click   </h3> <button onclick='window.location.href='http://10.10.26.226/eserviceprod/request/requestlistfortheuser''> Return to Request list</button>", "text/html");

                return RedirectToAction("RequestListfortheuser", "Request");
            }
        }

        //public JsonResult paymentprint(string pageIndex)
        //{
        //    try
        //    {
        //        ViewBag.viewname = "Paymentsearch";
        //        ViewBag.modelerror = Constantclass.number1;
        //        string res = string.Empty;
        //        paymentprint Objpaymentprint = new paymentprint();
        //        List<EPaymentRequestInfo> objpayinfo = new List<EPaymentRequestInfo>();
        //        EPaymentRequestInfo ObjEPaymentRequestInfo = new EPaymentRequestInfo();
        //        if (TempData["EPaymentRequestInfo"] != null)
        //        {
        //            ObjEPaymentRequestInfo = (EPaymentRequestInfo)TempData["EPaymentRequestInfo"];
        //            Objpaymentprint.mUserid = Session["UserId"].ToString();
        //            Objpaymentprint.tokenId = Session["TokenId"].ToString();
        //            Objpaymentprint.OPTokenId = ObjEPaymentRequestInfo.OPTokenId;
        //            Objpaymentprint.PaymentStatus = ObjEPaymentRequestInfo.STATUS;
        //            res = objdataclass.Paymentprint(Objpaymentprint);
        //        }
        //        TempData["EPaymentRequestInfo"] = ObjEPaymentRequestInfo;
        //        var jsonser = new JavaScriptSerializer();

        //        var obj = jsonser.Deserialize<dynamic>(res);
        //        foreach (var x in obj)
        //        {

        //            res = x.Value;

        //        }
        //        JsonResult result = new JsonResult()
        //        {
        //            Data = JsonConvert.SerializeObject(res)
        //        };
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public ActionResult MyAccount()
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                if (Session["login"] != null)
                {
                    ViewBag.login = "sav";
                }
                DataTable dtMyAccountlist = new DataTable();
                SecurityParams objSecurityParams = new SecurityParams();
                objSecurityParams.mUserid = Session["UserId"].ToString();
                objSecurityParams.tokenId = Session["TokenId"].ToString();
                dtMyAccountlist = objdataclass.Myaccount(objSecurityParams);
                TempData["MyAccount"] = dtMyAccountlist;
                checksession();
                return View(dtMyAccountlist);
            }
            catch (Exception e)
            {
                String _Exception = e.ToString();
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }

        [HttpPost]
        public ActionResult MyAccount(Myaccountinput ObjMyaccountinput)
        {
            try
            {
                //string themesddlvalue = Request.Form["ThemesDDl"].ToString();
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                //if (!(ModelState.IsValid))
                //{
                //    DataTable dtMyAccountlist1 = new DataTable();
                //    if (TempData["MyAccount"] != null)
                //    {
                //        dtMyAccountlist1 = (DataTable)TempData["MyAccount"];
                //    }
                //    TempData["MyAccount"] = dtMyAccountlist1;
                //    ViewBag.modelerror = Constantclass.number;
                //    return View(dtMyAccountlist1);
                //}



                Models.User ObjUserDetails = new Models.User();
                DataTable dtMyAccount = new DataTable();
                ObjUserDetails.FirstName = ObjMyaccountinput.FirstName;
                ObjUserDetails.LastName = ObjMyaccountinput.LastName;
                ObjUserDetails.CivilId = ObjMyaccountinput.CivilId;
                ObjUserDetails.LicenceNumber = ObjMyaccountinput.LicenseNumber;
                ObjUserDetails.MobileTelNumber = ObjMyaccountinput.MobileTelNumber;
                ObjUserDetails.EmailId = ObjMyaccountinput.EmailId;
                //ObjUserDetails.Themes = themesddlvalue;

                ObjUserDetails.mUserid = Session["UserId"].ToString();
                ObjUserDetails.tokenId = Session["TokenId"].ToString();
                DataTable dtMyAccountlist = new DataTable();
                if (TempData["MyAccount"] != null)
                {
                    ValidateContacts ObjValidateContacts = new ValidateContacts();

                    ObjValidateContacts.mUserid = Session["UserId"].ToString();
                    ObjValidateContacts.tokenId = Session["TokenId"].ToString();
                    ObjValidateContacts.ReferenceId = "";

                    DataTable dtcheck = new DataTable();

                    TempData["MobileNo"] = ObjUserDetails.MobileTelNumber;
                    dtMyAccountlist = (DataTable)TempData["MyAccount"];
                    if (dtMyAccountlist.Rows.Count > 0)
                    {

                        ObjUserDetails.EmailId = dtMyAccountlist.Rows[0]["EmailId"].ToString();

                        dtMyAccount = objdataclass.Myaccountpost(ObjUserDetails);
                        //Session["Themes"] = themesddlvalue;

                        if (dtMyAccount.Rows.Count > 0)
                        {
                            if (dtMyAccount.Rows[0]["Status"].ToString().Trim() == "-1")
                            {
                                //   ViewBag.modelerror = Constantclass.number2;
                                ViewBag.duplicate = 0;
                            }
                        }

                        ObjUserDetails.CivilId = ObjMyaccountinput.CivilId;

                        ObjUserDetails.LicenceNumber = ObjMyaccountinput.LicenseNumber;

                        if ((String.IsNullOrEmpty(ObjMyaccountinput.LicenseNumber) || String.IsNullOrEmpty(ObjMyaccountinput.CivilId)) || ViewBag.duplicate == 0)
                        {
                            Session["AllowmenuAfterAcountUpdate"] = "False";
                        }
                        else
                        {
                            Session["AllowmenuAfterAcountUpdate"] = "True";

                            //Commented 15-07-2019.. after successfull account update .. its redirected here .. actually it should not be
                            //return RedirectToAction("RequestListfortheUser", "Request");
                        }
                        if (dtMyAccount.Rows.Count > 0)
                        {
                            if (dtMyAccount.Rows[0]["Status"].ToString().Trim() == Constantclass.number)
                            {
                                ViewBag.modelerror = Constantclass.number2;



                                //================================================================
                                //--This sp is called from my account page to update Mobile number for now..This requires OTP verification
                                //--so after changing mobile number, the user account will be deactivated for use and redirected to login page to mandatorily do the mobile OTP verification
                                //--BEACUSE OF THIS BELOW LINE ADDED AND USER LOGGED OUT AND REDIRECTED TO LOGIN PAGE FOR MOBILE VERIFICATION
                                //================================================================
                                return RedirectToAction("Logout", "Registration");
                                //LogOutActions();//Remove this method and clearsession method in this if required, not used if this is commented
                            }
                        }
                        //dtMyAccountlist.Rows[0]["FirstName"] = ObjUserDetails.FirstName;
                        //dtMyAccountlist.Rows[0]["LastName"] = ObjUserDetails.LastName;
                        //dtMyAccountlist.Rows[0]["CivilId"] = ObjUserDetails.CivilId;
                        dtMyAccountlist.Rows[0]["MobileNumber"] = ObjUserDetails.MobileTelNumber;
                        TempData["MyAccount"] = dtMyAccountlist;
                        //if (Sessionlogin != "")
                        //{
                        //    Session["login"] = Sessionlogin;
                        //    if (Session["login"].ToString() == "E")
                        //    {
                        //        TempData["MobileNo"] = null;
                        //    }
                        //    return RedirectToAction("EmailVerification", "registration");
                        //}
                    }
                }
                checksession();
                ViewBag.Showmsg = '0';
                //ViewBag.Msg = Resources.Resource.updatedSuccess;
                ViewBag.Msg = "Your Mobile number has been updated. Please login again by doing the Mobile number verification.";
                ViewBag.DoMobileNumberVerification = "Yes";

                if (ViewBag.duplicate == 0)
                {
                    ViewBag.Showmsg = '0';
                    ViewBag.Msg = Resources.Resource.AlreadyRegisteredCivilID;
                    ViewBag.DoMobileNumberVerification = "No";
                }

                ViewBag.InfoMsg = "No";

                if (dtMyAccount.Rows[0]["Status"].ToString().Trim() == "-1")
                {
                    Session["AllowmenuAfterAcountUpdate"] = "True";
                    if (dtMyAccount.Rows[0]["Errormsg"].ToString() == "DuplicateCivilid")
                    {
                        ViewBag.Showmsg = '0';
                        ViewBag.Msg = Resources.Resource.AlreadyRegisteredCivilID;
                        ViewBag.DoMobileNumberVerification = "Yes";
                        ViewBag.InfoMsg = "Yes";
                    }
                    if (dtMyAccount.Rows[0]["Errormsg"].ToString() == "DuplicateLicenseNumber")
                    {
                        ViewBag.Showmsg = '0';
                        ViewBag.Msg = Resources.Resource.AlreadyRegisteredOrgLicense;
                        ViewBag.DoMobileNumberVerification = "Yes";
                        ViewBag.InfoMsg = "Yes";
                    }
                }

                CommonFunctions.LogUserActivity("MyAccount", "", "", "", "", "");
                return View(dtMyAccountlist);
                //return View();

            }
            catch (Exception e)
            {
                ViewBag.Showmsg = '0';
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("MyAccount", "", "", "", "", e.Message.ToString());
                return View();
            }
        }



        //public ActionResult UserAccount()
        //{
        //    try
        //    {
        //        if (Session["UserId"] == null)
        //        {
        //            return RedirectToAction("Start", "registration");
        //        }
        //        if (Session["login"] != null)
        //        {
        //            ViewBag.login = "sav";
        //        }
        //        DataTable dtMyAccountlist = new DataTable();
        //        SecurityParams objSecurityParams = new SecurityParams();
        //        objSecurityParams.mUserid = Session["UserId"].ToString();
        //        objSecurityParams.tokenId = Session["TokenId"].ToString();
        //        dtMyAccountlist = objdataclass.Myaccount(objSecurityParams);
        //        TempData["MyAccount"] = dtMyAccountlist;
        //        checksession();

        //        if (Session["ClearingAgentServices"] == null || Session["OrganizationServices"] == null)
        //        {
        //            return RedirectToAction("Index");

        //        }
        //        else
        //        {
        //            ViewBag.SelectedEntity = Convert.ToBoolean(Session["ClearingAgentServices"]) ? "1" : "2";
        //        }

        //        Session["SideBarState"] = "active";
        //        Session["Themes"] = "Blue";
        //        List<LegalEntity> uts = new List<LegalEntity>();
        //        uts.Add(new LegalEntity { LegalEntityID = 0, LegalEntityName = Resources.Resource.PleaseselectaLegalEntity });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
        //        uts.Add(new LegalEntity { LegalEntityID = 1, LegalEntityName = Resources.Resource.ClearingAgent });//"Clearing Agent" }); //Resources.Resource.CaptchaCode });// "SubBroker" });
        //        uts.Add(new LegalEntity { LegalEntityID = 2, LegalEntityName = Resources.Resource.Organization });//"Organization" });// Resources.Resource.CaptchaCode });// "Messenger" });

        //        ViewBag.UserType = uts;

        //        List<Gender> G = new List<Gender>();
        //        G.Add(new Gender { GenderTypeID = 0, GenderTypeValue = Resources.Resource.GenderRequired });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
        //        G.Add(new Gender { GenderTypeID = 1, GenderTypeValue = Resources.Resource.male }); //Resources.Resource.CaptchaCode });// "SubBroker" });
        //        G.Add(new Gender { GenderTypeID = 2, GenderTypeValue = Resources.Resource.female });// Resources.Resource.CaptchaCode });// "Messenger" });


        //        ViewBag.GenderType = G;

        //        List<LegalEntity> LEST = new List<LegalEntity>();
        //        LEST.Add(new LegalEntity { LegalEntityID = 0, LegalEntityName = Resources.Resource.PleaseselectaLegalEntity });// "Please select a Legal Entity" });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
        //        LEST.Add(new LegalEntity { LegalEntityID = 1, LegalEntityName = "Trader" });//"Clearing Agent" }); //Resources.Resource.CaptchaCode });// "SubBroker" });
        //        LEST.Add(new LegalEntity { LegalEntityID = 2, LegalEntityName = "Courier" });//"Organization" });// Resources.Resource.CaptchaCode });// "Messenger" });


        //        ViewBag.LegalEntitySubType = LEST;

        //        User u = new User();
        //        u.ParentID = 0;
        //        u.SubUser = false;
        //        u.SelectedServices = "";
        //        u.SelectedOrganizations = "";

        //        return View(u);
        //    }
        //    catch (Exception e)
        //    {
        //        WriteToLogFile(e.Message, "UserRegistrationGET METHOD");
        //        ViewBag.modelerror = Constantclass.number5;
        //        ViewBag.msg = Resources.Resource.somethingwentwrong;
        //        return View();
        //    }
        //    try
        //    {
        //        ViewBag.modelerror = Constantclass.number1;
        //        if (Session["UserId"] == null)
        //        {
        //            return RedirectToAction("Start", "registration");
        //        }
        //        if (Session["login"] != null)
        //        {
        //            ViewBag.login = "sav";
        //        }
        //        DataTable dtMyAccountlist = new DataTable();
        //        SecurityParams objSecurityParams = new SecurityParams();
        //        objSecurityParams.mUserid = Session["UserId"].ToString();
        //        objSecurityParams.tokenId = Session["TokenId"].ToString();
        //        dtMyAccountlist = objdataclass.Myaccount(objSecurityParams);
        //        TempData["MyAccount"] = dtMyAccountlist;
        //        checksession();
        //        return View(dtMyAccountlist);
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.modelerror = Constantclass.number5;
        //        ViewBag.Msg = Resources.Resource.somethingwentwrong;
        //        return View();
        //    }
        //}

        //[HttpPost]
        //public ActionResult UserAccount(Myaccountinput ObjMyaccountinput)
        //{
        //    try
        //    {
        //        //string themesddlvalue = Request.Form["ThemesDDl"].ToString();
        //        ViewBag.modelerror = Constantclass.number1;
        //        if (Session["UserId"] == null)
        //        {
        //            return RedirectToAction("Start", "registration");
        //        }
        //        //if (!(ModelState.IsValid))
        //        //{
        //        //    DataTable dtMyAccountlist1 = new DataTable();
        //        //    if (TempData["MyAccount"] != null)
        //        //    {
        //        //        dtMyAccountlist1 = (DataTable)TempData["MyAccount"];
        //        //    }
        //        //    TempData["MyAccount"] = dtMyAccountlist1;
        //        //    ViewBag.modelerror = Constantclass.number;
        //        //    return View(dtMyAccountlist1);
        //        //}
        //        Models.User ObjUserDetails = new Models.User();
        //        DataTable dtMyAccount = new DataTable();
        //        ObjUserDetails.FirstName = ObjMyaccountinput.FirstName;
        //        ObjUserDetails.LastName = ObjMyaccountinput.LastName;
        //        ObjUserDetails.CivilId = ObjMyaccountinput.CivilId;
        //        ObjUserDetails.LicenceNumber = ObjMyaccountinput.LicenseNumber;
        //        ObjUserDetails.MobileTelNumber = ObjMyaccountinput.MobileTelNumber;
        //        ObjUserDetails.EmailId = ObjMyaccountinput.EmailId;
        //        //ObjUserDetails.Themes = themesddlvalue;

        //        ObjUserDetails.mUserid = Session["UserId"].ToString();
        //        ObjUserDetails.tokenId = Session["TokenId"].ToString();
        //        DataTable dtMyAccountlist = new DataTable();
        //        if (TempData["MyAccount"] != null)
        //        {
        //            ValidateContacts ObjValidateContacts = new ValidateContacts();

        //            ObjValidateContacts.mUserid = Session["UserId"].ToString();
        //            ObjValidateContacts.tokenId = Session["TokenId"].ToString();
        //            ObjValidateContacts.ReferenceId = "";

        //            DataTable dtcheck = new DataTable();

        //            TempData["MobileNo"] = ObjUserDetails.MobileTelNumber;
        //            dtMyAccountlist = (DataTable)TempData["MyAccount"];
        //            if (dtMyAccountlist.Rows.Count > 0)
        //            {

        //                ObjUserDetails.EmailId = dtMyAccountlist.Rows[0]["EmailId"].ToString();

        //                dtMyAccount = objdataclass.Myaccountpost(ObjUserDetails);
        //                //Session["Themes"] = themesddlvalue;

        //                if (dtMyAccount.Rows.Count > 0)
        //                {
        //                    if (dtMyAccount.Rows[0]["Status"].ToString().Trim() == "-1")
        //                    {
        //                        //   ViewBag.modelerror = Constantclass.number2;
        //                        ViewBag.duplicate = 0;
        //                    }
        //                }

        //                ObjUserDetails.CivilId = ObjMyaccountinput.CivilId;

        //                ObjUserDetails.LicenceNumber = ObjMyaccountinput.LicenseNumber;

        //                if ((String.IsNullOrEmpty(ObjMyaccountinput.LicenseNumber) || String.IsNullOrEmpty(ObjMyaccountinput.CivilId)) || ViewBag.duplicate == 0)
        //                {
        //                    Session["AllowmenuAfterAcountUpdate"] = "False";
        //                }
        //                else
        //                {
        //                    Session["AllowmenuAfterAcountUpdate"] = "True";

        //                    //Commented 15-07-2019.. after successfull account update .. its redirected here .. actually it should not be
        //                    //return RedirectToAction("RequestListfortheUser", "Request");
        //                }
        //                if (dtMyAccount.Rows.Count > 0)
        //                {
        //                    if (dtMyAccount.Rows[0]["Status"].ToString().Trim() == Constantclass.number)
        //                    {
        //                        ViewBag.modelerror = Constantclass.number2;



        //                        //================================================================
        //                        //--This sp is called from my account page to update Mobile number for now..This requires OTP verification
        //                        //--so after changing mobile number, the user account will be deactivated for use and redirected to login page to mandatorily do the mobile OTP verification
        //                        //--BEACUSE OF THIS BELOW LINE ADDED AND USER LOGGED OUT AND REDIRECTED TO LOGIN PAGE FOR MOBILE VERIFICATION
        //                        //================================================================
        //                        return RedirectToAction("Logout", "Registration");
        //                        //LogOutActions();//Remove this method and clearsession method in this if required, not used if this is commented
        //                    }
        //                }
        //                //dtMyAccountlist.Rows[0]["FirstName"] = ObjUserDetails.FirstName;
        //                //dtMyAccountlist.Rows[0]["LastName"] = ObjUserDetails.LastName;
        //                //dtMyAccountlist.Rows[0]["CivilId"] = ObjUserDetails.CivilId;
        //                dtMyAccountlist.Rows[0]["MobileNumber"] = ObjUserDetails.MobileTelNumber;
        //                TempData["MyAccount"] = dtMyAccountlist;
        //                //if (Sessionlogin != "")
        //                //{
        //                //    Session["login"] = Sessionlogin;
        //                //    if (Session["login"].ToString() == "E")
        //                //    {
        //                //        TempData["MobileNo"] = null;
        //                //    }
        //                //    return RedirectToAction("EmailVerification", "registration");
        //                //}
        //            }
        //        }
        //        checksession();
        //        ViewBag.Showmsg = '0';
        //        //ViewBag.Msg = Resources.Resource.updatedSuccess;
        //        ViewBag.Msg = "Your Mobile number has been updated. Please login again by doing the Mobile number verification.";
        //        ViewBag.DoMobileNumberVerification = "Yes";
        //        ViewBag.InfoMsg = "No";

        //        if (ViewBag.duplicate == 0)
        //        {
        //            ViewBag.Showmsg = '0';
        //            ViewBag.Msg = Resources.Resource.AlreadyRegisteredCivilID;
        //            ViewBag.DoMobileNumberVerification = "No";
        //        }

        //        if (dtMyAccount.Rows[0]["Status"].ToString().Trim() == "-1")
        //        {

        //            if (dtMyAccount.Rows[0]["Errormsg"].ToString() == "DuplicateCivilid")
        //            {
        //                ViewBag.Showmsg = '0';
        //                ViewBag.Msg = Resources.Resource.AlreadyRegisteredCivilID;
        //                ViewBag.DoMobileNumberVerification = "Yes";
        //                ViewBag.InfoMsg = "Yes";
        //            }
        //            if (dtMyAccount.Rows[0]["Errormsg"].ToString() == "DuplicateLicenseNumber")
        //            {
        //                ViewBag.Showmsg = '0';
        //                ViewBag.Msg = Resources.Resource.AlreadyRegisteredLicense;
        //                ViewBag.DoMobileNumberVerification = "Yes";
        //                ViewBag.InfoMsg = "Yes";
        //            }
        //        }

        //        return View(dtMyAccountlist);

        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Showmsg = '0';
        //        ViewBag.modelerror = Constantclass.number5;
        //        ViewBag.Msg = Resources.Resource.somethingwentwrong;
        //        return View();
        //    }
        //}

        public ActionResult HSCodeSearch()
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                if (TempData["return"] != null)
                {


                    if (TempData["textdata"] != null)
                    {
                        ViewBag.data = TempData["textdata"].ToString();
                    }
                    else
                    {
                        ViewBag.data = "";
                    }
                    if (TempData["paramType"] != null)
                    {
                        if (TempData["paramType"].ToString().Trim() == "HSCode")
                        {
                            ViewBag.HSCode = "selected";
                            TempData["paramType"] = ViewBag.HSCode;
                        }
                        else
                        {
                            ViewBag.Description = "selected";
                            TempData["paramType"] = ViewBag.Description;
                        }
                    }
                    else
                    {
                        ViewBag.Description = "selected";
                        TempData["paramType"] = ViewBag.Description;
                    }
                    TempData["textdata"] = ViewBag.data;

                    DataTable dtHSCodeSearchlist = new DataTable();
                    if (TempData["HSCodeSearchdata"] != null)
                    {
                        dtHSCodeSearchlist = (DataTable)TempData["HSCodeSearchdata"];
                        TempData["HSCodeSearchdata"] = dtHSCodeSearchlist;
                        return View(dtHSCodeSearchlist);
                    }
                    else
                    {
                        ViewBag.Description = "selected";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Description = "selected";
                    return View();
                }

            }
            catch (Exception e)
            {
                String _Exception = e.ToString();
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        [HttpPost]
        public ActionResult HSCodeSearch(HSCodeSearch ObjHSCodeSearch)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                DataTable dtHSCodeSearchlist = new DataTable();
                ObjHSCodeSearch.mUserid = Session["UserId"].ToString();
                ObjHSCodeSearch.tokenId = Session["TokenId"].ToString();
                dtHSCodeSearchlist = objdataclass.HSCodeSearch(ObjHSCodeSearch);
                TempData["HSCodeSearchdata"] = dtHSCodeSearchlist;
                TempData["textdata"] = ObjHSCodeSearch.data;
                TempData["paramType"] = ObjHSCodeSearch.paramType;
                HttpCookie langCookie = Request.Cookies["culture"];
                ObjHSCodeSearch.lang = langCookie.Value;
                ViewBag.data = ObjHSCodeSearch.data;
                if (TempData["paramType"].ToString().Trim() == "HSCode")
                {
                    ViewBag.HSCode = "selected";
                }
                else
                {
                    ViewBag.Description = "selected";
                }
                checksession();
                CommonFunctions.LogUserActivity("HSCodeSearch", "", "", "", "", "");
                return View(dtHSCodeSearchlist);
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("HSCodeSearch", "", "", "", "", e.Message.ToString());
                return View();
            }
        }
        public ActionResult HSCodeSearchid(string id)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                if (id != null)
                {
                    id = encrypt.Decode(id);
                }
                else
                {
                    return View("HSCodeSearch");
                }
                DataTable dtHSCodeSearch = new DataTable();
                if (TempData["textdata"] != null)
                {
                    TempData["textdata"] = TempData["textdata"].ToString();
                }
                if (TempData["paramType"] != null)
                {
                    TempData["paramType"] = TempData["paramType"].ToString();
                }
                if (TempData["HSCodeSearchdata"] != null)
                {
                    dtHSCodeSearch = (DataTable)TempData["HSCodeSearchdata"];
                    TempData["HSCodeSearchdata"] = dtHSCodeSearch;
                }
                HSCodeSearchid ObjHSCodeSearch = new HSCodeSearchid();
                DataSet dtHSCodeSearchlist = new DataSet();
                ObjHSCodeSearch.hsCodeId = id;
                ObjHSCodeSearch.mUserid = Session["UserId"].ToString();
                ObjHSCodeSearch.tokenId = Session["TokenId"].ToString();
                dtHSCodeSearchlist = objdataclass.HSCodeSearchid(ObjHSCodeSearch);
                TempData["return"] = "id";
                checksession();
                CommonFunctions.LogUserActivity("HSCodeSearchid", "", "", "", "", "");
                return View(dtHSCodeSearchlist);
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("HSCodeSearchid", "", "", "", "", e.Message.ToString());
                return View();
            }
        }
        public ActionResult HouseBillSearch()
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                ViewBag.search = "";
                return View();
            }
            catch (Exception e)
            {
                String _Exception = e.ToString();
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        [HttpPost]
        public ActionResult HouseBillSearch(HousebillSearch ObjHousebillSearch)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                ViewBag.search = ObjHousebillSearch.hbNumber;
                DataTable dtHousebillSearchlist = new DataTable();
                ObjHousebillSearch.mUserid = Session["UserId"].ToString();
                ObjHousebillSearch.tokenId = Session["TokenId"].ToString();
                dtHousebillSearchlist = objdataclass.HousebillSearch(ObjHousebillSearch);
                checksession();
                CommonFunctions.LogUserActivity("HouseBillSearch", "", "", "", "", "");
                return View(dtHousebillSearchlist);
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("HouseBillSearch", "", "", "", "", e.Message.ToString());
                return View();
            }
        }
        public ActionResult DeclarationSearch()
        {
            try
            {
                // ViewBag.modelerror = Constantclass.number1;
                //if (Session["UserId"] == null)
                //{
                //   return RedirectToAction("Start", "registration");
                //}
                ViewBag.search = "";
                return View();
            }
            catch (Exception e)
            {
                String _Exception = e.ToString();
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        [HttpPost]
        public ActionResult DeclarationSearch(DeclarationSearch ObjDeclarationSearch)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                HttpCookie langCookie = Request.Cookies["culture"];

                ViewBag.search = ObjDeclarationSearch.tempDeclNumber;
                DataTable dtDeclarationSearchlist = new DataTable();
                ObjDeclarationSearch.mUserid = Session["UserId"].ToString();
                ObjDeclarationSearch.tokenId = Session["TokenId"].ToString();
                ObjDeclarationSearch.lang = langCookie.Value;


                //   langCookie.Value;
                dtDeclarationSearchlist = objdataclass.Declarationsearch(ObjDeclarationSearch);
                checksession();
                CommonFunctions.LogUserActivity("DeclarationSearch", "", "", "", "", "");
                return View(dtDeclarationSearchlist);
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                CommonFunctions.LogUserActivity("DeclarationSearch", "", "", "", "", e.Message.ToString());
                return View();
            }
        }
        public JsonResult Orgprint(string OrgReqId)
        {
            try
            {
                string res = string.Empty;
                OrgReqResultInfoParams objOrgReqResultInfoParams = new OrgReqResultInfoParams();
                objOrgReqResultInfoParams.mUserid = Session["UserId"].ToString();
                objOrgReqResultInfoParams.tokenId = Session["TokenId"].ToString();
                objOrgReqResultInfoParams.sOrgReqId = OrgReqId;
                res = objdataclass.SubmitOrgReqHtml(objOrgReqResultInfoParams);
                var jsonser = new JavaScriptSerializer();

                var obj = jsonser.Deserialize<dynamic>(res);
                foreach (var x in obj)
                {

                    res = x.Value;

                }
                JsonResult result = new JsonResult()
                {
                    Data = JsonConvert.SerializeObject(res)
                };
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult OrgDelete(string OrgReqId)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Start", "registration");
                }
                SecurityParams objmyorglist = new SecurityParams();
                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();

                OrgReqdeleteParams objOrgReqdeleteParams = new OrgReqdeleteParams();
                objOrgReqdeleteParams.mUserid = Session["UserId"].ToString();
                objOrgReqdeleteParams.tokenId = Session["TokenId"].ToString();
                objOrgReqdeleteParams.OrganizationRequestId = WebApplication1.Models.encrypt.Decode(OrgReqId);

                DataTable dt = objdataclass.DeleteOrgReq(objOrgReqdeleteParams);
                DataTable dtmyorglist = new DataTable();
                //commented below block as the below tempdata concept is not requered for this page-- Siraj 
                //if (dt.Rows.Count > 0)
                //{
                //    if (dt.Rows[0]["status"].ToString().Trim() == Constantclass.number1)
                //    {
                //        if (TempData["MyOrgReqData"] != null)
                //        {
                //            dtmyorglist = (DataTable)TempData["MyOrgReqData"];
                //        }
                //        else
                //        {
                //            dtmyorglist = objdataclass.OrgReqRequest_Status_Data(objmyorglist);

                //        }
                //        DataView dv = dtmyorglist.DefaultView;
                //        dv.RowFilter = "NOT(OrganizationRequestId='" + OrgReqId + "')";
                //        dtmyorglist = dv.ToTable();
                //        TempData["MyOrgReqData"] = dtmyorglist;
                //    }
                //}
                checksession();
                //return View("Org_RequestStatus", dtmyorglist);
                CommonFunctions.LogUserActivity("OrgDelete", "", "", "", "", "");
                return RedirectToAction("RequestListFortheUser", "Request");
            }
            catch (Exception e)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                //return View("Org_RequestStatus");
                CommonFunctions.LogUserActivity("OrgDelete", "", "", "", "", e.Message.ToString());
                return RedirectToAction("RequestListFortheUser", "Request");
            }
        }
        private void checksession()
        {
            //if (Session["DataStatus"] != null)
            //{
            //    Session["DataStatus"] = "0";
            //    if (Session["DataStatus"].ToString() == "-1")
            //    {
            //        ViewBag.modelerror = Constantclass.number9;
            //        ViewBag.Msg = Resources.Resource.Sessionexpired;
            //    }
            //}
        }

        private void LogOutActions()
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


            TempData.Clear();
            Response.Cookies.Clear();
            Request.Cookies.Clear();

            Session.Abandon();
            Session.Clear();

            clearSession();
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


    }
}