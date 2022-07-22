using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication1.Models;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Diagnostics;
namespace WebApplication1.Controllers
{
    public class UserController : MyBaseController
    {
        // GET: User
        Dataclass objdataclass = new Dataclass();
        // Dataclass objdataclass;



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




        [HttpPost]
        public JsonResult GetNotifications()
        {
            try
            {
                TempData.Clear();
                List<NotificationsList> objnotifylist = new List<NotificationsList>();
                if (Session["UserId"] == null)
                {
                    return Json(objnotifylist, JsonRequestBehavior.AllowGet); ;
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
        public ActionResult MyOrganizations()
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
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
                return View(dtmyorglist);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
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

        public ActionResult Org_Registration(string Id, string Requesttype, string purpose, string reqnum) ///all null first time

        {
            try
            {

                //== CHECKING IF MAIN ORG IS APPROVED

                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
                }
                
                if(!OrgRegistrationAllowed())
                    RedirectToAction("UnAuthorizedAction", "Registration");


                int UserId = int.Parse(Session["UserId"].ToString());
                ReqObj r = new ReqObj { ParentID = UserId, ChildUser = 0 };
                DataTable dt = objdataclass.CanCreateOrg(r);
                bool CanCreateOrg = Convert.ToBoolean(dt.Rows[0]["CanCreateOrg"].ToString());
                bool MainOrgApproved = Convert.ToBoolean(dt.Rows[0]["MainOrgApproved"].ToString());
                if (Id == null && Requesttype == null && purpose == null && reqnum == null && !CanCreateOrg)
                {
                    ViewBag.NotAllowedToByPassForOrgCreation = "0";
                    return RedirectToAction("MyOrganizations");
                }
                if (TempData["data"] != null)
                {
                    List<OrgRequestlist> TD = (List<OrgRequestlist>)TempData["data"];
                    if (TD[0].Data.OrgGetBasicResult[0].IsmainCompany)
                    {
                        ViewBag.CompanyType = "Main";
                    }
                }

                //==


                ViewBag.modelerror = Constantclass.number1;
                //if (Session["UserId"] == null)
                //{
                //    return RedirectToAction("Index", "registration");
                //}
                Session["returnurl1"] = null;
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
                List<OrgGetBasicResult> objlist = new List<OrgGetBasicResult>();
                DeclarationSearchByIdParams objOrgreg = new DeclarationSearchByIdParams();
                OrgGetBasicResult ObjOrgGetBasicResult = new OrgGetBasicResult();
                if (Id == null && Requesttype == null)
                {
                    TempData["data"] = null;
                    objlist.Add(ObjOrgGetBasicResult);


                    if (!MainOrgApproved && CanCreateOrg)
                    {
                        ViewBag.CompanyType = "Main"; // all params are null and able to reach this block then its the first request for org creation
                        ObjOrgGetBasicResult.TradeLicNumber = Session["LicenseNumber"].ToString();
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
                        objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];
                    }
                    else
                    {
                        objOrgRequestdatalist = objdataclass.OrgRegistration_Data(objOrgreg, Requesttype);
                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany)
                        {
                            ViewBag.CompanyType = "Main";
                        }
                        else
                        {
                            ViewBag.CompanyType = "Sub";
                        }

                    }
                    TempData.Keep("data");
                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult != null)
                        {
                            ObjOrgGetBasicResult = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0];
                            objlist.Add(ObjOrgGetBasicResult);
                            if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].Editable == Constantclass.number)
                            {
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
                            objlist.Add(ObjOrgGetBasicResult);
                        }
                    }
                    else
                    {
                        objlist.Add(ObjOrgGetBasicResult);
                    }
                    TempData["data"] = objOrgRequestdatalist;
                }
                ViewData["data"] = objlist;
                if (Session["returnurl"] == null)
                {
                    if (Requesttype == null)
                    {
                        Requesttype = "";
                    }
                    if (Requesttype == "Status")
                    {
                        Session["returnurl"] = "Org_RequestStatus";
                    }
                    else
                    {
                        Session["returnurl"] = "MyOrganizations";
                    }
                }
                checksession();
                return View();
                //string com = Request.QueryString["com"];
                //  string page = Request.QueryString["page"];
                // int id = int.Parse(this.RouteData.Values["Id"].ToString());
            }
            catch (Exception ex)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                return View();
                // throw ex;
            }
        }
        [HttpPost]
        public ActionResult Org_Registration(OrgGetBasicResult objorgreg)
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
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

                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.dis = Constantclass.number1;
                }
                else
                {
                    ViewBag.dis = Constantclass.number;
                }
                List<OrgGetBasicResult> objlist = new List<OrgGetBasicResult>();
                if (!(ModelState.IsValid))
                {
                    if (Session["purpose"] != null)
                    {
                        if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                        {
                            objlist.Add(objorgreg);
                            ViewData["data"] = objlist;
                            ViewBag.modelerror = Constantclass.number;

                            return View("Org_Registration");
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
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany = objorgreg.IsmainCompany;//Company type label issue , added this property assignment
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].mUserid = Session["UserId"].ToString();
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].tokenId = Session["TokenId"].ToString();
                        objinput = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0];
                    }
                }
                else
                {
                    objOrgRequestdatalist.Add(objOrgRequestlist);
                    objData.OrgGetBasicResult = objlist;
                    objOrgRequestdatalist[0].Data = objData;
                    objOrgRequestdatalist[0].Data.OrgGetBasicResult.Add(objorgreg);
                    objorgreg.mUserid = Session["UserId"].ToString();
                    objorgreg.tokenId = Session["TokenId"].ToString();
                    objinput = objorgreg;
                }
                string res = string.Empty;
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    DataSet objOrgReq1Result = objdataclass.OrgRegistration_Data_create(objinput);
                    if (objOrgReq1Result.Tables[0].Rows.Count > 0)
                    {
                        if (objOrgReq1Result.Tables[0].Rows[0]["Status"].ToString() == Constantclass.number)
                        {
                            res = Constantclass.number;
                        }
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrganizationRequestId"]);
                        objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany = objOrgReq1Result.Tables[0].Rows[0]["IsMainCompany"].ToString() == "1" ? true : false;//for company type

                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].IsmainCompany)
                        {
                            ViewBag.CompanyType = "Main";
                        }
                        else
                        {
                            ViewBag.CompanyType = "Sub";
                        }

                        //put here
                        Session["UploadOrgId"] = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrganizationRequestId"]);
                        Session["requestnumber"] = objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString();
                    }
                }
                else
                {
                    if (Session["requestnumber"] == null)
                    {
                        Session["requestnumber"] = Constantclass.number;
                    }
                }
                TempData["data"] = objOrgRequestdatalist;
                checksession();
                if (res == Constantclass.number || Session["purpose"].ToString().Trim() != Constantclass.number1)
                {
                    if (objorgreg.isIndustrial)
                    {
                        Session["returnurl1"] = "Org_Industrial";
                        return RedirectToAction("Org_Industrial", "User");
                    }
                    else
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult != null)
                        {
                            if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0] != null)
                            {
                                if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].isIndustrial)
                                {
                                    Session["returnurl1"] = "Org_Industrial";
                                    return RedirectToAction("Org_Industrial", "User");
                                }
                                else
                                {
                                    Session["returnurl1"] = "Org_Registration";
                                    return RedirectToAction("Org_ImporterDetails", "User");
                                }
                            }
                            else
                            {
                                Session["returnurl1"] = "Org_Industrial";
                                return RedirectToAction("Org_Industrial", "User");
                            }
                        }
                        else
                        {
                            Session["returnurl1"] = "Org_Industrial";
                            return RedirectToAction("Org_Industrial", "User");
                        }
                    }
                }
                else
                {
                    List<OrgGetBasicResult> objlist1 = new List<OrgGetBasicResult>();
                    objlist1.Add(objorgreg);
                    ViewData["data"] = objlist1;
                    return View("Org_Registration");
                }
            }

            catch (Exception e)
            {

                List<OrgGetBasicResult> objlist = new List<OrgGetBasicResult>();
                objlist.Add(objorgreg);
                ViewData["data"] = objlist;
                TempData["data"] = TempData["data"];

                //ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;

                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;

                return View("Org_Registration");
            }
        }
        public ActionResult Org_Industrial()
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
                }

                if (!OrgRegistrationAllowed())
                    RedirectToAction("UnAuthorizedAction", "Registration");

                List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                OrgGetIndustrialResult ObjIndustrialResult = new OrgGetIndustrialResult();
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetIndustrialResult != null)
                        {
                            ObjIndustrialResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            ObjIndustrialResult = objOrgRequestdatalist[0].Data.OrgGetIndustrialResult[0];
                            objlist.Add(ObjIndustrialResult);
                        }
                        else
                        {
                            ObjIndustrialResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;//siraj
                            objlist.Add(ObjIndustrialResult);
                            objOrgRequestdatalist[0].Data.OrgGetIndustrialResult = objlist;
                        }
                    }
                    else
                    {
                        objlist.Add(ObjIndustrialResult);
                        objOrgRequestdatalist[0].Data.OrgGetIndustrialResult = objlist;
                    }
                }
                else
                {
                    objlist.Add(ObjIndustrialResult);
                    objOrgRequestdatalist[0].Data.OrgGetIndustrialResult = objlist;
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
                ViewData["data"] = objlist;
                ViewBag.reqno = Session["requestnumber"].ToString();
                checksession();
                return View();
            }
            catch (Exception)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                return View();
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
                    return RedirectToAction("Index", "registration");
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
                        List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                        objindustrialdetails.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                        objlist.Add(objindustrialdetails);
                        ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        TempData["data"] = objOrgRequestdatalist;
                        return View("Org_Industrial");
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
                        List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                        objlist.Add(objindustrialdetails);
                        ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        TempData["data"] = objOrgRequestdatalist;
                        return View("Org_Industrial");
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
                checksession();
                if (res == Constantclass.number || Session["purpose"].ToString().Trim() != Constantclass.number1)
                {
                    return RedirectToAction("Org_ImporterDetails", "User");
                }
                else
                {
                    List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                    objlist.Add(objindustrialdetails);
                    ViewData["data"] = objlist;
                    TempData["data"] = objOrgRequestdatalist;
                    return View("Org_Industrial");
                }

            }

            catch (Exception e)
            {
                //==
                List<OrgGetIndustrialResult> objlist = new List<OrgGetIndustrialResult>();
                objlist.Add(objindustrialdetails);
                ViewData["data"] = objlist;
                TempData["data"] = TempData["data"];
                //==

                //ViewBag.modelerror = Constantclass.number5;
                //ViewBag.Msg = Resources.Resource.somethingwentwrong;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                return View();
            }
        }
        public ActionResult Org_ImporterDetails()
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
                }

                if (!OrgRegistrationAllowed())
                    RedirectToAction("UnAuthorizedAction", "Registration");

                List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                OrgGetImportLicenseResult ObjImportLicenseResult = new OrgGetImportLicenseResult();
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

                    if (objOrgRequestdatalist[0].Data != null)
                    {
                        if (objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult != null)
                        {
                            ObjImportLicenseResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            ObjImportLicenseResult = objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0];
                            if (objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].ImpLicNo != null)
                            {
                                string[] liarr = objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].ImpLicNo.ToString().Split('/').ToArray();
                                if (liarr.Length > 1)
                                {
                                    ObjImportLicenseResult.Year = liarr[0];
                                    ObjImportLicenseResult.ImpLicNo = liarr[1];
                                }
                            }

                            objlist.Add(ObjImportLicenseResult);
                        }
                        else
                        {
                            ObjImportLicenseResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;//siraj
                            objlist.Add(ObjImportLicenseResult);
                            objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = objlist;
                        }
                    }
                    else
                    {
                        objlist.Add(ObjImportLicenseResult);
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = objlist;
                    }
                }
                else
                {
                    objlist.Add(ObjImportLicenseResult);
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult = objlist;
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
                ViewData["data"] = objlist;
                checksession();
                return View();
            }
            catch (Exception)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Org_ImporterDetails(OrgGetImportLicenseResult objimporterdetails)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
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
                if (Session["purpose"].ToString().Trim() == Constantclass.number1)
                {
                    ViewBag.reqno = Session["requestnumber"].ToString();
                    objimporterdetails.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                    if (!(ModelState.IsValid))
                    {
                        List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                        objlist.Add(objimporterdetails);
                        ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        TempData["data"] = objOrgRequestdatalist;
                        return View("Org_ImporterDetails");
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
                            ModelState.AddModelError(string.Empty, Resources.Resource.validateYear);
                        }
                        if (val == "yes")
                        {
                            ModelState.AddModelError(string.Empty, Resources.Resource.IssuancegreaterthanEnddate);
                        }
                        List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                        objlist.Add(objimporterdetails);
                        ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        TempData["data"] = objOrgRequestdatalist;
                        return View("Org_ImporterDetails");
                    }

                    //   objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0] = objimporterdetails;
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].mUserid = Session["UserId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].tokenId = Session["TokenId"].ToString();
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].ImpLicType = objimporterdetails.ImpLicType;
                    if (objimporterdetails.ImpLicType == "permanent")
                    {
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].Year = "";
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].ImpLicNo = objimporterdetails.ImpLicNo;
                    }
                    else
                    {
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].ImpLicNo = objimporterdetails.Year + "/" + objimporterdetails.ImpLicNo;
                    }
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].IssueDate = objimporterdetails.IssueDate;
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].ExpiryDate = objimporterdetails.ExpiryDate;
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                    objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].OrganizationRequestId = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrganizationRequestId;



                    DataSet objOrgReq1Result = objdataclass.Orgimporter_Data_create(objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0]);
                    if (objOrgReq1Result.Tables[0].Rows.Count > 0 && objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString() != "")
                    {
                        if (objOrgReq1Result.Tables[0].Rows[0]["Status"].ToString() == Constantclass.number)
                        {
                            res = Constantclass.number;
                        }
                        objOrgRequestdatalist[0].Data.OrgGetImportLicenseResult[0].OrganizationRequestId = Convert.ToInt32(objOrgReq1Result.Tables[0].Rows[0]["OrganizationRequestId"]);
                        Session["requestnumber"] = objOrgReq1Result.Tables[0].Rows[0]["RequestNumber"].ToString();
                    }
                }
                TempData["data"] = objOrgRequestdatalist;
                checksession();
                if (res == Constantclass.number || Session["purpose"].ToString().Trim() != Constantclass.number1)
                {
                    return RedirectToAction("Org_Commercial", "User");
                }
                else
                {
                    List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                    objlist.Add(objimporterdetails);
                    ViewData["data"] = objlist;
                    TempData["data"] = objOrgRequestdatalist;
                    return View("Org_ImporterDetails");
                }
            }

            catch (Exception e)
            {
                List<OrgGetImportLicenseResult> objlist = new List<OrgGetImportLicenseResult>();
                objlist.Add(objimporterdetails);
                ViewData["data"] = objlist;
                TempData["data"] = TempData["data"];

                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                return View();
            }
        }
        public ActionResult Org_Commercial()
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
                }

                if (!OrgRegistrationAllowed())
                    RedirectToAction("UnAuthorizedAction", "Registration");

                ViewBag.modelerror = Constantclass.number1;
                ViewBag.viewname = "Comm";

                string subtype = string.Empty;
                List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                OrgGetCommercialLicenseResult ObjCommercialLicenseResult = new OrgGetCommercialLicenseResult();
                if (TempData["data"] != null)
                {
                    objOrgRequestdatalist = (List<OrgRequestlist>)TempData["data"];

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
                            objlist.Add(ObjCommercialLicenseResult);
                        }
                        else
                        {
                            ObjCommercialLicenseResult.OrgAraName = objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].OrgAraName;
                            objlist.Add(ObjCommercialLicenseResult);
                            objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult = objlist;
                        }
                    }
                    else
                    {
                        objlist.Add(ObjCommercialLicenseResult);
                        objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult = objlist;
                    }
                }
                else
                {
                    objlist.Add(ObjCommercialLicenseResult);
                    objOrgRequestdatalist[0].Data.OrgGetCommercialLicenseResult = objlist;
                }

                ViewBag.reqno = Session["requestnumber"].ToString();
                TempData["data"] = objOrgRequestdatalist;
                ViewData["data"] = objlist;
                TempData.Keep("data");
                CommSubtypes(subtype);
                checksession();
                return View();
            }
            catch (Exception e)
            {
                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                return View();
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
                    return RedirectToAction("Index", "registration");
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
                        List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                        objlist.Add(objcommercialdetails);
                        if (objOrgRequestdatalist[0].Data.OrgGetBasicResult[0].isIndustrial)
                        {
                            if (objcommercialdetails.CommLicNo != null || objcommercialdetails.Year != null || objcommercialdetails.ExpiryDate != null || objcommercialdetails.IssueDate != null || objcommercialdetails.CommLicType != null)
                            {
                                ViewData["data"] = objlist;
                                ViewBag.modelerror = Constantclass.number;
                                CommSubtypes(objcommercialdetails.CommLicSubType);
                                TempData["data"] = objOrgRequestdatalist;
                                return View("Org_Commercial");
                            }
                        }
                        else
                        {
                            ViewData["data"] = objlist;
                            ViewBag.modelerror = Constantclass.number;
                            CommSubtypes(objcommercialdetails.CommLicSubType);
                            TempData["data"] = objOrgRequestdatalist;
                            return View("Org_Commercial");
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
                            ModelState.AddModelError(string.Empty, Resources.Resource.validateYear);
                        }
                        if (val == "yes")
                        {
                            ModelState.AddModelError(string.Empty, Resources.Resource.IssuancegreaterthanEnddate);
                        }
                        List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                        objlist.Add(objcommercialdetails);
                        ViewData["data"] = objlist;
                        ViewBag.modelerror = Constantclass.number;
                        CommSubtypes(objcommercialdetails.CommLicSubType);
                        TempData["data"] = objOrgRequestdatalist;
                        return View("Org_Commercial");
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
                checksession();
                if (res == Constantclass.number || Session["purpose"].ToString().Trim() != Constantclass.number1)
                {
                    return RedirectToAction("UploadDocument", "User");
                }
                else
                {
                    List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                    objlist.Add(objcommercialdetails);
                    ViewData["data"] = objlist;
                    CommSubtypes(objcommercialdetails.CommLicSubType);
                    TempData["data"] = objOrgRequestdatalist;
                    return View("Org_Commercial");
                }
            }
            catch (Exception e)
            {
                List<OrgGetCommercialLicenseResult> objlist = new List<OrgGetCommercialLicenseResult>();
                objlist.Add(objcommercialdetails);
                ViewData["data"] = objlist;
                TempData["data"] = TempData["data"];

                //ViewBag.modelerror = Constantclass.number5;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = Constantclass.number;
                return View();
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
                return RedirectToAction("Index", "registration");
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

            Dataclass objdataclass = new Dataclass();
            List<OrgGetBasicResult> objlist = new List<OrgGetBasicResult>();
            DeclarationSearchByIdParams objOrgreg = new DeclarationSearchByIdParams();
            OrgGetBasicResult ObjOrgGetBasicResult = new OrgGetBasicResult();
            // WriteToLogFile("firstEntry", "test");
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
                }

                string s = Session["UploadOrgId"].ToString();
                //string xx = "";

                if (TempData["data"] == null)
                {
                    objOrgreg.mUserid = Session["UserId"].ToString();
                    objOrgreg.tokenId = Session["TokenId"].ToString();
                    objOrgreg.OrganizationId = Session["UploadOrgId"].ToString();
                    objOrgreg.sOrgReqId = Session["UploadOrgId"].ToString();

                    objOrgRequestdatalist = objdataclass.OrgRegistration_Data(objOrgreg, "Status");


                    TempData["data"] = objOrgRequestdatalist;


                }

                //    WriteToLogFile("objOrgRequestdatalist", "BeforeUploadmethod");
                uploadmethod();

                //   WriteToLogFile("objOrgRequestdatalist", "AfetrUpload");
                DocTypes d = new DocTypes();
                BrokerUpdateModel ObjbrokerRenwalModel = new BrokerUpdateModel();



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




                return View("UploadDocument", ObjbrokerRenwalModel);


            }
            catch (Exception ex)
            {
                ViewBag.modelerror = Constantclass.number5;
                //  ViewBag.Msg = Resources.Resource.somethingwentwrong;
                ModelState.AddModelError(string.Empty, Resources.Resource.somethingwentwrong);
                ViewBag.modelerror = ex.StackTrace;
                ViewBag.Msg = ex.StackTrace;
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
        //            return RedirectToAction("Index", "registration");
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
        //    catch (Exception)
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
                    return RedirectToAction("Index", "registration");
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
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;


                return View("UploadDocument");
            }
        }

        [HttpPost]
        public ActionResult UploadDocument_Submit(OrgGetDocumentsResult obj)
        {
            try
            {
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
                }

                //CheckUploadDocs
                List<DocTypeslist> objDocTypes = new List<DocTypeslist>();
                List<string> objDocTypeserror = new List<string>();

                objOrgreg.mUserid = Session["UserId"].ToString();
                objOrgreg.tokenId = Session["TokenId"].ToString();
                objOrgreg.OrganizationId = Session["UploadOrgId"].ToString();
                objOrgreg.sOrgReqId = Session["UploadOrgId"].ToString();

                objOrgRequestdatalist = objdataclass.OrgRegistration_Data(objOrgreg, "Status");


                List<OrgRequestlist> objorgreddatalist = new List<OrgRequestlist>();

                if (Session["DocTypeslist"] != null)
                {
                    objDocTypes = (List<DocTypeslist>)Session["DocTypeslist"];
                    for (int i = 0; i < objDocTypes[0].DocTypes.Count; i++)
                    {
                        string docname = string.Empty;
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
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                uploadmethod();
                checksession();
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
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
        //    catch (Exception)
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
                    return RedirectToAction("Index", "registration");
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
            catch (Exception) { return Json("Error While Saving."); }
            checksession();
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
                    return RedirectToAction("Index", "registration");
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
            catch (Exception)
            {
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
                    return RedirectToAction("Index", "registration");
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
                return View(objpaymentsearchtypes);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
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
                    return RedirectToAction("Index", "registration");
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
                return View(ObjEpaymentlist);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
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
                    return RedirectToAction("Index", "registration");
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
                return View();
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
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
                    return RedirectToAction("Index", "registration");
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
                //dt = objdataclass.DenyPayRequest(objmypaylistinfo);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString().Trim() == Constantclass.number)
                    {
                        ViewBag.modelerror = Constantclass.number5;
                        ViewBag.Msg = Resources.Resource.Denied;
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
                return View("EPayment");
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View("EPayment");
            }
        }
        [HttpPost]
        public ActionResult EPayment(CallbackRedirectInfo ObjCallbackRedirectInfo)
        {
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
                EPaymentRequestInfo ObjEPaymentRequestInfo = new EPaymentRequestInfo();
                if (TempData["EPaymentRequestInfo"] != null)
                {
                    ObjEPaymentRequestInfo = (EPaymentRequestInfo)TempData["EPaymentRequestInfo"];
                    ObjCallbackRedirectInfo.mUserid = Session["UserId"].ToString();
                    ObjCallbackRedirectInfo.tokenId = Session["TokenId"].ToString();
                    ObjCallbackRedirectInfo.OPTokenId = ObjEPaymentRequestInfo.OPTokenId;
                    ObjCallbackRedirectInfo.EpayReqNo = ObjEPaymentRequestInfo.RequestNo;
                    ObjCallbackRedirectInfo.RedirectUrl = "";

                }
                //    dt = objdataclass.EPaymentOnCallbackRedirect(ObjCallbackRedirectInfo);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString().Trim() == Constantclass.number)
                    {
                        ViewBag.modelerror = Constantclass.number5;
                        ViewBag.Msg = "Success";
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
                return View();
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        public JsonResult paymentprint(string pageIndex)
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
        public ActionResult MyAccount()
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
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
            catch (Exception)
            {
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
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
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
                ObjUserDetails.MobileTelNumber = ObjMyaccountinput.MobileTelNumber;
                ObjUserDetails.EmailId = ObjMyaccountinput.EmailId;
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
                        //if (dtMyAccountlist.Rows[0]["MobileNumber"].ToString().Trim() != "")
                        //{

                        //    if (dtMyAccountlist.Rows[0]["MobileNumber"].ToString().Trim() != ObjUserDetails.MobileTelNumber)
                        //    {
                        //        ObjValidateContacts.Reference = ObjUserDetails.MobileTelNumber;
                        //        ObjValidateContacts.ReferenceType = "M";
                        //        dtcheck = objdataclass.ValidateContactsWithOTP(ObjValidateContacts);
                        //    }
                        //}
                        //else
                        //{
                        //    ObjValidateContacts.Reference = ObjUserDetails.MobileTelNumber;
                        //    ObjValidateContacts.ReferenceType = "M";
                        //    dtcheck = objdataclass.ValidateContactsWithOTP(ObjValidateContacts);
                        //}
                        //string Sessionlogin = string.Empty;
                        //if (Session["login"] != null)
                        //{
                        //    Sessionlogin = Session["login"].ToString();
                        //}

                        //if (dtcheck.Columns.Contains("MobileVerifyStatus"))
                        //{
                        //    if (dtcheck.Rows[0]["MobileVerifyStatus"].ToString().Trim() == "-1")
                        //    {
                        //        Session["login"] = null;
                        //        TempData["refId"] = dtcheck.Rows[0]["MOTPreqId"].ToString().Trim();
                        //        ViewBag.modelerror = Constantclass.number3;
                        //        return View(dtMyAccountlist);
                        //    }
                        //}

                        ObjUserDetails.EmailId = dtMyAccountlist.Rows[0]["EmailId"].ToString();
                        ObjUserDetails.CivilId = dtMyAccountlist.Rows[0]["CivilId"].ToString();
                        dtMyAccount = objdataclass.Myaccountpost(ObjUserDetails);
                        if (dtMyAccount.Rows.Count > 0)
                        {
                            if (dtMyAccount.Rows[0]["Status"].ToString().Trim() == Constantclass.number)
                            {
                                ViewBag.modelerror = Constantclass.number2;
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
                return View(dtMyAccountlist);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        public ActionResult HSCodeSearch()
        {
            try
            {
                ViewBag.modelerror = Constantclass.number1;
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
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
            catch (Exception)
            {
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
                    return RedirectToAction("Index", "registration");
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
                return View(dtHSCodeSearchlist);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View();
            }
        }
        public ActionResult HSCodeSearchid(string id)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "registration");
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
                return View(dtHSCodeSearchlist);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
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
                    return RedirectToAction("Index", "registration");
                }
                ViewBag.search = "";
                return View();
            }
            catch (Exception)
            {
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
                    return RedirectToAction("Index", "registration");
                }
                ViewBag.search = ObjHousebillSearch.hbNumber;
                DataTable dtHousebillSearchlist = new DataTable();
                ObjHousebillSearch.mUserid = Session["UserId"].ToString();
                ObjHousebillSearch.tokenId = Session["TokenId"].ToString();
                dtHousebillSearchlist = objdataclass.HousebillSearch(ObjHousebillSearch);
                checksession();
                return View(dtHousebillSearchlist);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
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
                //   return RedirectToAction("Index", "registration");
                //}
                ViewBag.search = "";
                return View();
            }
            catch (Exception)
            {
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
                    return RedirectToAction("Index", "registration");
                }
                ViewBag.search = ObjDeclarationSearch.tempDeclNumber;
                DataTable dtDeclarationSearchlist = new DataTable();
                ObjDeclarationSearch.mUserid = Session["UserId"].ToString();
                ObjDeclarationSearch.tokenId = Session["TokenId"].ToString();
                dtDeclarationSearchlist = objdataclass.Declarationsearch(ObjDeclarationSearch);
                checksession();
                return View(dtDeclarationSearchlist);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
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
                    return RedirectToAction("Index", "registration");
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
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["status"].ToString().Trim() == Constantclass.number1)
                    {
                        if (TempData["MyOrgReqData"] != null)
                        {
                            dtmyorglist = (DataTable)TempData["MyOrgReqData"];
                        }
                        else
                        {
                            dtmyorglist = objdataclass.OrgReqRequest_Status_Data(objmyorglist);

                        }
                        DataView dv = dtmyorglist.DefaultView;
                        dv.RowFilter = "NOT(OrganizationRequestId='" + OrgReqId + "')";
                        dtmyorglist = dv.ToTable();
                        TempData["MyOrgReqData"] = dtmyorglist;
                    }
                }
                checksession();
                return View("Org_RequestStatus", dtmyorglist);
            }
            catch (Exception)
            {
                ViewBag.modelerror = Constantclass.number5;
                ViewBag.Msg = Resources.Resource.somethingwentwrong;
                return View("Org_RequestStatus");
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
    }
}