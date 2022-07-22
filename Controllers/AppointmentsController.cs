using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AppointmentsController : MyBaseController //Controller
    {
        // GET: Appointments
        public ActionResult InspectionAppointment()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogOut", "registration");
            }
            InspectionAppointment IA = new InspectionAppointment();

            List<InspectionZone> InsZone = new List<InspectionZone>{
                               new InspectionZone{  ZoneId= "0" , ZoneName = Resources.Resource.selectInspectionZone   }
                            };
            List<InspectionTerminal> InsTerminal = new List<InspectionTerminal>{
                               new InspectionTerminal{  TerminalId= "0" , TerminalName = Resources.Resource.selectInspectionTerminal  }
                            };
            List<InspectionRounds> InsRound = new List<InspectionRounds>{
                               new InspectionRounds{  RoundId= "0" , RoundName = Resources.Resource.selectInspectionRound  }
                            };

            IA.InsZone = InsZone;
            IA.InsTerminal = InsTerminal;
            IA.InsRound = InsRound;


            return View(IA);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InspectionAppointment(InspectionAppointment IA)
        {
            //ViewBag.AlreadyScheduledVehicle = "0";
            IA.AppointmentId = CommonFunctions.CsUploadDecrypt(HttpUtility.UrlDecode(IA.AppointmentId));
            if (Session["UserId"] == null)
            {
                return Json(new { StatusCode = -3, message = Resources.Resource.Sessionexpired });
            }
            try
            {
                Dataclass objdataclass = new Dataclass();

                IA.UserId = Session["UserId"].ToString();
                IA.OrgId = Session["UserOrgId"].ToString();

                DataTable IAD;
                if (IA.UpdateRequest && IA.EditableRequest)
                {
                    IAD = objdataclass.UpdateInspectionAppointment(IA);
                }
                else
                {
                    IAD = objdataclass.CreateInspectionAppointment(IA);
                }

                if (IAD.Rows.Count > 0)
                {
                    CommonFunctions.LogUserActivity("InspectionAppointment", "", "", "", "", "Status " + IAD.Rows[0]["Status"].ToString());
                    if (IAD.Rows[0]["Status"].ToString() == "1")
                    {
                        return Json(new { StatusCode = 1, message = Resources.Resource.AppointmentCreationsuccess });
                    }
                    else if (IAD.Rows[0]["Status"].ToString() == "11")
                    {
                        return Json(new { StatusCode = 11, message = Resources.Resource.AppointmentUpdatesuccess });
                    }
                    else if (IAD.Rows[0]["Status"].ToString() == "-1")
                    {
                        return Json(new { StatusCode = -1, message = Resources.Resource.Duplicatebookingforthevehicle });
                    }
                    else if (IAD.Rows[0]["Status"].ToString() == "-5")
                    {
                        return Json(new { StatusCode = -5, message = Resources.Resource.Vehiclesrequired });
                    }
                    else if (IAD.Rows[0]["Status"].ToString() == "2")
                    {
                        String _message = Resources.Resource.Roundreachedmaximumcapacity1;

                        if (IAD.Rows[0]["availabletobook"] != null)
                        {

                            if (!String.IsNullOrWhiteSpace(IAD.Rows[0]["availabletobook"].ToString()))
                            {
                                Int64 availabletobook = Int64.Parse(IAD.Rows[0]["availabletobook"].ToString());

                                if (availabletobook > 0)
                                {
                                    _message = String.Format(Resources.Resource.Roundreachedmaximumcapacity + " {0} " + Resources.Resource.RoundreachedmaximumcapacityPart2,
                       IAD.Rows[0]["availabletobook"].ToString());
                                }
                            }
                        }

                        return Json(new { StatusCode = 2, message = _message });
                        //return Json(new { StatusCode = 2, message = Resources.Resource.Roundreachedmaximumcapacity + " " + IAD.Rows[0]["availabletobook"].ToString() });
                    }
                    else
                    {
                        return Json(new { StatusCode = 2, message = Resources.Resource.CannotprocessPleasecontactadministrator + " 7" });
                    }
                }
                else
                {
                    CommonFunctions.LogUserActivity("InspectionAppointment", "", "", "", "", "No response returned for creation");
                    return Json(new { StatusCode = -2, message = Resources.Resource.somethingwentwrong });
                }
            }
            catch (Exception e)
            {
                CommonFunctions.LogUserActivity("InspectionAppointment", "", "", "", "", e.Message.ToString());
                return Json(new { StatusCode = -4, message = Resources.Resource.somethingwentwrong });
            }

        }
        //public ActionResult AppointmentList()
        //{
        //    if (Session["UserId"] == null)
        //    {
        //        return RedirectToAction("LogOut", "registration");
        //    }
        //    //List< InspectionAppointment> Al = new List<InspectionAppointment>();

        //    //InspectionAppointment IA1 = new InspectionAppointment { RoundName = "df",EditableRequest=true };
        //    //InspectionAppointment IA2 = new InspectionAppointment { RoundName = "dasdf", EditableRequest = false };
        //    //Al.Add(IA1);
        //    //Al.Add(IA2);

        //    //List<InspectionRounds> irs = new List<InspectionRounds>();
        //    //irs.Add(new InspectionRounds { InspectionRoundId = 0, InspectionRoundName = "Please select the Round" });
        //    //irs.Add(new InspectionRounds { InspectionRoundId = 1, InspectionRoundName = "Round 1 [1PM-2PM]" });
        //    //irs.Add(new InspectionRounds { InspectionRoundId = 2, InspectionRoundName = "Round 2 [4PM-6PM]" });

        //    //ViewBag.InspectionRounds = irs;

        //    Dataclass objdataclass = new Dataclass();

        //    ReqObj ro = new ReqObj { Action = Session["LegalEntity"].ToString(), CommonData = Session["UserId"].ToString(), OrgID = Session["UserOrgID"].ToString() };

        //    DataTable GetInspectionAppointments = objdataclass.GetInspectionAppointments(ro);

        //    List<InspectionAppointment> InspectionAppointments = new List<InspectionAppointment>();

        //    var a = from x in GetInspectionAppointments.AsEnumerable()
        //            select new InspectionAppointment
        //            {
        //                AppointmentId = x["EInspectionAppointmentRequestId"].ToString(),
        //                RequestNumber = x["RequestNumber"].ToString(),
        //                DeclarationId = x["DeclarationId"].ToString(),
        //                RoundName = x["RoundName"].ToString(),
        //                InspectionDate = x["InspectionDate"].ToString(),
        //                EditableRequest = Convert.ToBoolean(x["EditableRequest"].ToString())
        //            };
        //    InspectionAppointments = a.ToList();

        //    return View(InspectionAppointments);
        //}
        public ActionResult UpdateAppointment(string AppointmentId)
        {
            AppointmentId = CommonFunctions.CsUploadDecrypt(HttpUtility.UrlDecode(AppointmentId));
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogOut", "registration");
            }
            try
            {
                Dataclass objdataclass = new Dataclass();

                ReqObj ro = new ReqObj { Action = Session["LegalEntity"].ToString(), CommonData = AppointmentId.ToString(), CommonData2 = Session["UserId"].ToString(), OrgID = Session["UserOrgID"].ToString() };

                DataSet GetInspectionAppointmentDetails = objdataclass.GetInspectionAppointmentDetails(ro);

                if (GetInspectionAppointmentDetails.Tables["InspectionAppointment"].Rows.Count == 0)
                {
                    //return RedirectToAction("error", "error");
                    CommonFunctions.LogUserActivity("UpdateAppointment", "", "", "", "", "No data for Appointmenid -" + AppointmentId);
                    return RedirectToAction("RequestListfortheuser", "Request");
                }

                InspectionAppointment IA = new InspectionAppointment();

                //string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                //bool EnglishCulture = culture.Contains("English");

                //string AppointmentBookedStatus = "Booked";
                //string AppointmentCompletedStatus = "Booked";
                HttpCookie langCookie = Request.Cookies["culture"];
                string lang = langCookie.Value == "en" ? "eng" : "ar";

                var ial = from x in GetInspectionAppointmentDetails.Tables["InspectionAppointment"].AsEnumerable()
                          select new InspectionAppointment
                          {
                              AppointmentId = x["EInspectionAppointmentRequestId"].ToString(),
                              RequestNumber = x["RequestNumber"].ToString(),
                              DateCreated = x["DateCreated"].ToString(),
                              DeclarationId = x["DeclarationId"].ToString(),
                              DeclarationNumberToPrint = x["decnumber"].ToString(),
                              //   DeclarationIduid = x["DeclarationIduid"].ToString(),
                              TempDeclarationId = x["TempDeclarationId"].ToString(),
                              DeclarationType = lang == "eng" ? x["DeclarationType"].ToString() : x["DeclarationTypeAra"].ToString(),
                              PortId = x["PortId"].ToString(),
                              PortName = x["PortId"].ToString(),
                              InspectionZone = x["InspectionZone"].ToString(),
                              InspectionTerminal = x["InspectionTerminal"].ToString(),
                              InspectionRound = x["InspectionRoundId"].ToString(),
                              InspectionDate = x["InspectionDate"].ToString(),
                              //SelectedVehicleList = x["SelectedVehicleList"].ToString(),
                              Status = x["Status"].ToString(), // == "1" ? AppointmentBookedStatus : AppointmentCompletedStatus,
                              StatusNumber = x["Status"].ToString(),
                              EditableRequest = Convert.ToBoolean(Convert.ToInt16(x["EditableRequest"].ToString())),
                              UpdateRequest = true
                          };
                IA = ial.ToList().FirstOrDefault();

                List<VehicleList> VehicleList = new List<VehicleList>();

                String lefttoright = ((Char)0x200E).ToString();

                var a = from x in GetInspectionAppointmentDetails.Tables["SelectedVehicleList"].AsEnumerable()
                        select new VehicleList
                        {

                            VehicleID = x["DeclarationVehicleId"].ToString(),
                            VehiclePlateNumber = lefttoright + x["VehiclePlateNumber"].ToString() + lefttoright,
                            ContainerNumber = x["ContainerNumber"].ToString(),
                            DriverName = x["DriverName"].ToString(),
                            Weight = x["Weight"].ToString(),
                            MobileNumber = x["MobileNumber"].ToString()

                        };
                VehicleList = a.ToList();

                if (GetInspectionAppointmentDetails.Tables[2].Rows.Count == 0 || GetInspectionAppointmentDetails.Tables[3].Rows.Count == 0 || GetInspectionAppointmentDetails.Tables[4].Rows.Count == 0)
                {
                    CommonFunctions.LogUserActivity("UpdateAppointment", "", "", "", "", "No InspectionAppointmentDetails -id " + AppointmentId);
                    return RedirectToAction("RequestListfortheuser", "Request");
                }
                List<InspectionRounds> InspectionRoundss = new List<InspectionRounds>();


                var b = from x in GetInspectionAppointmentDetails.Tables["EInspectionsRound"].AsEnumerable()
                        select new InspectionRounds
                        {

                            RoundId = x["InspectionRoundId"].ToString(),
                            RoundName = x["InspectionRoundName"].ToString()

                        };
                InspectionRoundss = b.ToList();
                InspectionRoundss.Insert(0, new InspectionRounds { RoundId = "0", RoundName = Resources.Resource.selectInspectionRound });
                IA.InsRound = InspectionRoundss;


                List<InspectionZone> InspectionZones = new List<InspectionZone>();



                var c = from x in GetInspectionAppointmentDetails.Tables["EInspectionZones"].AsEnumerable()
                        select new InspectionZone
                        {

                            ZoneId = x["ZoneId"].ToString(),
                            ZoneName = x["ZoneName"].ToString()

                        };
                InspectionZones = c.ToList();
                InspectionZones.Insert(0, new InspectionZone { ZoneId = "0", ZoneName = Resources.Resource.selectInspectionZone });
                IA.InsZone = InspectionZones;

                List<InspectionTerminal> InspectionTerminals = new List<InspectionTerminal>();


                var d = from x in GetInspectionAppointmentDetails.Tables["Eterminals"].AsEnumerable()
                        select new InspectionTerminal
                        {

                            TerminalId = x["TerminalId"].ToString(),
                            TerminalName = x["TerminalName"].ToString()

                        };
                InspectionTerminals = d.ToList();
                InspectionTerminals.Insert(0, new InspectionTerminal { TerminalId = "0", TerminalName = Resources.Resource.selectInspectionTerminal });
                IA.InsTerminal = InspectionTerminals;

                IA.SelectedVehicleDetails = VehicleList;
                IA.SelectedVehicleList = String.Join(",", VehicleList.Select(i => i.VehicleID).ToArray());

                //if (IA == null)
                //{
                //    //return RedirectToAction("error", "error");
                //    return RedirectToAction("RequestListfortheuser", "Request");
                //}

                return View("InspectionAppointment", IA);
            }
            catch (Exception e)
            {
                CommonFunctions.LogUserActivity("UpdateAppointment", "", "", "", "", e.Message.ToString());
                return RedirectToAction("RequestListfortheuser", "Request");
            }
        }
        public ActionResult CancelInspectionAppointment(string AppointmentId)
        {

            AppointmentId = CommonFunctions.CsUploadDecrypt(HttpUtility.UrlDecode(AppointmentId));
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogOut", "registration");
            }
            try
            {
                Dataclass objdataclass = new Dataclass();

                ReqObj ro = new ReqObj { Action = Session["LegalEntity"].ToString(), CommonData = AppointmentId.ToString(), CommonData2 = Session["UserId"].ToString(), OrgID = Session["UserOrgID"].ToString() };

                DataTable GetInspectionAppointmentDetails = objdataclass.CancelInspectionAppointment(ro);

                if (GetInspectionAppointmentDetails.Rows.Count == 0)
                {
                    //return RedirectToAction("error", "error");
                    CommonFunctions.LogUserActivity("CancelInspectionAppointment", "", "", "", "", "No response returned for cancellation");
                }

                return RedirectToAction("RequestListForTheUser", "Request");
            }
            catch (Exception e)
            {
                CommonFunctions.LogUserActivity("CancelInspectionAppointment", "", "", "", "", e.Message.ToString());
                return RedirectToAction("RequestListfortheuser", "Request");
            }
        }
        [HttpPost]
        public JsonResult GetVehicleList(string DeclarationId, string TempDeclarationId)// string DONumber, string SecurityCode)
        {
            if (Session["UserId"] == null)
            {
                return Json(new { StatusCode = -1, message = Resources.Resource.Sessionexpired });
            }
            try
            {
                Dataclass objdataclass = new Dataclass();

                if (Session["UserOrgID"] == null)
                {
                    return Json(new { StatusCode = -3, message = Resources.Resource.CannotprocessPleasecontactadministrator + " 5" });
                }

                if (string.IsNullOrEmpty(Session["UserOrgID"].ToString()))
                {
                    return Json(new { StatusCode = -3, message = Resources.Resource.CannotprocessPleasecontactadministrator + " 5" });
                }

                ReqObj ro = new ReqObj { Action = Session["LegalEntity"].ToString(), CommonData = DeclarationId, CommonData1 = TempDeclarationId, CommonData2 = Session["UserId"].ToString(), OrgID = Session["UserOrgID"].ToString() };

                DataSet GetDeclarationDetails = objdataclass.GetVehicleList(ro);

                List<VehicleList> VehicleLists = new List<VehicleList>();

                //if (GetDeclarationDetails.Tables[0].Rows.Count == 0)


                string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                bool EnglishCulture = culture.Contains("English");

                String errorMsgWrongDeclaration = "الرجاء كتابة رقم بيان جمركي صحيح";

                if (EnglishCulture)
                {
                    errorMsgWrongDeclaration = "Please Write a Valid Declaration Number";
                }

                if (GetDeclarationDetails.Tables.Count > 0)
                {
                    if (GetDeclarationDetails.Tables.Contains("vehiclelist"))
                    {
                        if (GetDeclarationDetails.Tables["vehiclelist"].Columns.Contains("Status"))
                        {
                            String status = GetDeclarationDetails.Tables["vehiclelist"].Rows[0]["Status"].ToString();

                            if (status == "2") // Wrong Declaration number
                            {
                                return Json(new { StatusCode = -3, message = errorMsgWrongDeclaration });
                            }
                            else if (status == "3") // All Vehicles Booked for This Declaration
                            {
                                return Json(new { StatusCode = -3, message = Resources.Resource.NoVehiclesforgivendeclaration });
                            }
                            else if (status == "4") //  No Vehicle in Proper State  or  Declaration Not in Proper State
                            {
                                return Json(new { StatusCode = -3, message = Resources.Resource.declartionStatusNotProper });
                            }

                            else if (status == "5") // Organizarion is Null or Empty
                            {
                                return Json(new { StatusCode = -3, message = Resources.Resource.CannotprocessPleasecontactadministrator + " 5" });
                            }
                        }
                    }

                }


                HttpCookie langCookie = Request.Cookies["culture"];
                string lang = langCookie.Value == "en" ? "eng" : "ar";

                var lefttoright = ((Char)0x200E).ToString();

                var a = from x in GetDeclarationDetails.Tables["vehiclelist"].AsEnumerable()
                        select new VehicleList
                        {

                            VehicleID = x["VehicleID"].ToString(),
                            VehiclePlateNumber = (lefttoright + x["VehiclePlateNumber"].ToString()),
                            ContainerNumber = x["ContainerNumber"].ToString(),
                            DriverName = x["DriverName"].ToString(),
                            DeclarationId = x["DeclarationId"].ToString(),
                            PortId = x["PortId"].ToString(),
                            PortName = x["portname"].ToString(),
                            // DeclarationType= x["DeclarationType"].ToString(),
                            DeclarationType = lang == "eng" ? x["DeclarationType"].ToString() : x["DeclarationTypeAra"].ToString(),
                            Weight = x["Weight"].ToString(),
                            MobileNumber = x["MobileNumber"].ToString()

                        };
                VehicleLists = a.ToList();

                List<InspectionRounds> InspectionRoundss = new List<InspectionRounds>();


                var b = from x in GetDeclarationDetails.Tables["EInspectionsRound"].AsEnumerable()
                        select new InspectionRounds
                        {

                            RoundId = x["InspectionRoundId"].ToString(),
                            RoundName = x["InspectionRoundName"].ToString()

                        };
                InspectionRoundss = b.ToList();

                List<InspectionZone> InspectionZones = new List<InspectionZone>();



                var c = from x in GetDeclarationDetails.Tables["EInspectionZones"].AsEnumerable()
                        select new InspectionZone
                        {

                            ZoneId = x["ZoneId"].ToString(),
                            ZoneName = x["ZoneName"].ToString()

                        };
                InspectionZones = c.ToList();

                List<InspectionTerminal> InspectionTerminals = new List<InspectionTerminal>();


                var d = from x in GetDeclarationDetails.Tables["Eterminals"].AsEnumerable()
                        select new InspectionTerminal
                        {

                            TerminalId = x["TerminalId"].ToString(),
                            TerminalName = x["TerminalName"].ToString()

                        };
                InspectionTerminals = d.ToList();
                var data = new { StatusCode = 1, Vehicles = VehicleLists, InspectionZone = InspectionZones, InspectionTerminal = InspectionTerminals, InspectionRound = InspectionRoundss };



                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                CommonFunctions.LogUserActivity("GetVehicleList", "", "", "", "", e.Message.ToString());
                return Json(new { StatusCode = -2, message = Resources.Resource.somethingwentwrong });
            }
        }

        public ActionResult CustomsVisit()
        {
            HttpCookie langCookie = Request.Cookies["culture"];
            string lang;
            if (langCookie != null)
                lang = langCookie.Value == "en" ? "eng" : "ar";
            else
                lang = "eng";
            try
            {
                CustomsVisit CVA = new CustomsVisit();


                DepartmentDetails DD1 = new DepartmentDetails()
                {
                    DepartmentId = 1,
                    ScheduleId = 1,
                    DepartmentNameEng = "test1",
                    DepartmentNameAra = "test1",
                    DepartmentName = "test1",
                    DeptAddressEng = "test1",
                    DeptAddressAra = "test1",
                    DeptAddress = "test1",
                    //VisitPurposeEng = "test1",
                    //VisitPurposeAra = "test1",
                    //VisitPurposeName = "test1",
                    //GuidelineEng = "test1",
                    //GuidelineAra = "test1",
                    //Guideline = "test1",
                    AppointmentStartDate = "test1",
                    AppointmentEndDate = "test1",
                    WorkingDays = "1,4"
                    //,
                    //AppointmentTimeSlots = AppointmentTimeSlots1
                };

                DepartmentDetails DD2 = new DepartmentDetails()
                {
                    DepartmentId = 2,
                    ScheduleId = 2,
                    DepartmentNameEng = "test2",
                    DepartmentNameAra = "test2",
                    DepartmentName = "test2",
                    DeptAddressEng = "test2",
                    DeptAddressAra = "test2",
                    DeptAddress = "test2",
                    //VisitPurposeEng = "test2",
                    //VisitPurposeAra = "test2",
                    //VisitPurposeName = "test2",
                    //GuidelineEng = "test2",
                    //GuidelineAra = "test2",
                    //Guideline = "test2",
                    AppointmentStartDate = "test2",
                    AppointmentEndDate = "test2",
                    WorkingDays="0,2"
                    //,
                    //AppointmentTimeSlots = AppointmentTimeSlots2
                };
                //DD1.DepartmentName = lang == "eng" ? DD1.DepartmentNameEng : DD1.DepartmentNameAra;
                //DD1.DeptAddress = lang == "eng" ? DD1.DeptAddressEng : DD1.DeptAddressAra;
                //DD2.DepartmentName = lang == "eng" ? DD2.DepartmentNameEng : DD2.DepartmentNameAra;
                //DD2.DeptAddress = lang == "eng" ? DD2.DeptAddressEng : DD2.DeptAddressAra;

                List<DepartmentDetails> DepartmentDetailsFull = new List<DepartmentDetails>();
                DepartmentDetailsFull.Add(DD1);
                DepartmentDetailsFull.Add(DD1);

                List<VisitPurpose> VisitPurposeFull = new List<VisitPurpose>()
                {
                    new VisitPurpose{ VisitPurposeId=1, DepartmentId=1, VisitPurposeEng="VSP1",VisitPurposeAra="VSP1", GuidelineEng="VSG1",GuidelineAra="VSG1", VisitPurposeName="VSPN1" },
                    new VisitPurpose{ VisitPurposeId=2, DepartmentId=2, VisitPurposeEng="VSP2",VisitPurposeAra="VSP2", GuidelineEng="VSG2",GuidelineAra="VSG2", VisitPurposeName="VSPN2" }
                };


                List<AppointmentTimeSlot> AppointmentTimeSlotsFull = new List<AppointmentTimeSlot>
       {
           new AppointmentTimeSlot{ ScheduleId=1,DepartmentId=1, VisitPurposeId=1, AppountmentHour="9", Capacity="3"},
           new AppointmentTimeSlot{ ScheduleId=1,DepartmentId=1, VisitPurposeId=1, AppountmentHour="10", Capacity="2"},
           new AppointmentTimeSlot{ ScheduleId=1,DepartmentId=1, VisitPurposeId=1, AppountmentHour="11", Capacity="3"},
           new AppointmentTimeSlot{ ScheduleId=2,DepartmentId=2, VisitPurposeId=2, AppountmentHour="9", Capacity="3"},
           new AppointmentTimeSlot{ ScheduleId=2,DepartmentId=2, VisitPurposeId=2, AppountmentHour="10", Capacity="2"},
           new AppointmentTimeSlot{ ScheduleId=2,DepartmentId=2, VisitPurposeId=2, AppountmentHour="11", Capacity="3"}
       };

                //List<DepartmentDetails> DepartmentDetailsFullMapped = DepartmentDetailsFull.ForEach(i => i.AppointmentTimeSlots = AppointmentTimeSlotsFull.Where(inn => inn.ScheduleId == i.ScheduleId).ToList());

                List<DepartmentDetails> DepartmentDetailsFullMapped = new List<DepartmentDetails>();
                List<VisitPurpose> VisitPurposeMapped = new List<VisitPurpose>();
                //List<AppointmentTimeSlot> AppointmentTimeSlotsMapped = null;
                foreach (DepartmentDetails dd in DepartmentDetailsFull)
                {
                    DepartmentDetails ddm = dd;
                    List<VisitPurpose> VisitPurposetemp = new List<VisitPurpose>();
                    ddm.DepartmentName = lang == "eng" ? ddm.DepartmentNameEng : ddm.DepartmentNameAra;
                    ddm.DeptAddress = lang == "eng" ? ddm.DeptAddressEng : ddm.DeptAddressAra;
                    VisitPurposetemp = VisitPurposeFull.Where(i => i.DepartmentId == ddm.DepartmentId).ToList();
                    ddm.VisitPurpose = VisitPurposetemp;
                    int[] VisitPurposeIds = VisitPurposetemp.Select(i => i.VisitPurposeId).ToArray();
                    VisitPurposetemp = null;
                    ddm.AppointmentTimeSlots = AppointmentTimeSlotsFull.Where(i => i.DepartmentId == ddm.DepartmentId).ToList();//(inn => VisitPurposeIds.Contains(inn.VisitPurposeId)).ToList();
                    //DepartmentDetailsFull.Remove(dd);
                    DepartmentDetailsFullMapped.Add(ddm);
                    ddm = null;
                }
                //DepartmentDetailsFull = null;
                foreach (VisitPurpose dd in VisitPurposeFull)
                {
                    VisitPurpose ddm = dd;
                    ddm.VisitPurposeName = lang == "eng" ? ddm.VisitPurposeEng : ddm.VisitPurposeAra;
                    ddm.Guideline = lang == "eng" ? ddm.GuidelineEng : ddm.GuidelineAra;

                    VisitPurposeMapped.Add(ddm);
                    ddm = null;
                }
                VisitPurposeFull = null;
                //foreach (AppointmentTimeSlot dd in AppointmentTimeSlotsFull)
                //{
                //    AppointmentTimeSlot ddm = dd;
                //    ddm.DepartmentName = lang == "eng" ? ddm.DepartmentNameEng : ddm.DepartmentNameAra;
                //    ddm.DeptAddress = lang == "eng" ? ddm.DeptAddressEng : ddm.DeptAddressAra;
                //    VisitPurposetemp = VisitPurposeFull.Where(i => i.DepartmentId == ddm.DepartmentId).ToList();
                //    ddm.VisitPurpose = VisitPurposetemp;
                //    int[] VisitPurposeIds = VisitPurposetemp.Select(i => i.VisitPurposeId).ToArray();
                //    VisitPurposetemp = null;
                //    ddm.AppointmentTimeSlots = AppointmentTimeSlotsFull.Where(i => i.DepartmentId == ddm.DepartmentId).ToList();//(inn => VisitPurposeIds.Contains(inn.VisitPurposeId)).ToList();
                //    //DepartmentDetailsFull.Remove(dd);
                //    DepartmentDetailsFullMapped.Add(ddm);
                //    ddm = null;
                //}
                //AppointmentTimeSlotsFull = null;



                CVA.DepartmentDetails = DepartmentDetailsFullMapped;
                CVA.DepartmentDetailsstring = JsonConvert.SerializeObject(DepartmentDetailsFullMapped);
                CVA.VisitPurpose = JsonConvert.SerializeObject(VisitPurposeMapped);
                CVA.AppointmentTimeSlotsstring = JsonConvert.SerializeObject(AppointmentTimeSlotsFull);

               
                CVA.VisitorTypeData = new List<VisitorTypeData>()
            {
                new VisitorTypeData{ VisitorTypeId=0, VisitorTypeName="Please select VisitorTypeData"},
                new VisitorTypeData{ VisitorTypeId=1, VisitorTypeName="VST1"},
                new VisitorTypeData{ VisitorTypeId=2, VisitorTypeName="VST2"}
            };

                return View(CVA);
            }
            catch (Exception e)
            {
                CommonFunctions.LogUserActivity("CustomsVisitAppointment", "", "", "", "", e.Message.ToString());
                return RedirectToAction("error", "error");
            }
        }
        [HttpPost]
        public ActionResult CustomsVisit(CustomsVisit CVD)
        {
            DataAccess DA = new DataAccess();
            DataSet ds = DA.CreateCustomsVisitAppointment(CVD);
            try//if (true)
            {

                return RedirectToAction("CustomsVisitAppointmentPrint", new { ReservationNumber="" });
            }
            catch(Exception e)//else
            {
                CommonFunctions.LogUserActivity("CustomsVisitPost", "", "", "", "", e.Message.ToString());
                return View(CVD);
            }

        }
        public ActionResult CustomsVisitAppointmentPrint(string ReservationNumber)
        {
            try//if (true)
            {

                return RedirectToAction("CustomsVisitAppointmentPrint", new { ReservationNumber = "" });
            }
            catch (Exception e)//else
            {
                CommonFunctions.LogUserActivity("CustomsVisitPost", "", "", "", "", e.Message.ToString());
                return RedirectToAction("error", "error");
            }

        }

    }
}