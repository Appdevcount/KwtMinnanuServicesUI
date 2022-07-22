using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class ShipmentReceivingAuthorizationController : MyBaseController // Controller
    {
        // GET: ShipmentReceivingAuthorization
        DataSet ShipmentRequestDS = new DataSet();
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "registration");
            }
            Dataclass dc = new Dataclass();
            HttpCookie langCookie = Request.Cookies["culture"];
            string lang;
            if (langCookie != null)
                lang = langCookie.Value == "en" ? "eng" : "ar";
            else
                lang = "eng";
            try
            {


                ShipmentAuthorization ShipmentAuthorizationModel = new ShipmentAuthorization();
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
                    ShipmentAuthorizationModel.OrganizationId = Convert.ToInt32(Orgid);
                    ShipmentAuthorizationModel.RequesterUserId = int.Parse(Session["UserId"].ToString());//Session["Userid"].ToString();
                                                                                                         //ObjbrokerRenwalModel.requestForMobileUserId = Convert.ToInt32(Orgid);
                }

                if (Request.QueryString["RequestNumber"] != null &&
                    Request.QueryString["Orgid"] == null )
                {
                    string RequestNumber = CommonFunctions.CsUploadDecrypt(Request.QueryString["RequestNumber"].ToString());
                    if (RequestNumber == "")
                    {
                        string source = Request.RawUrl.ToString();
                        string split = "RequestNumber=";

                        string result = source.Substring(source.IndexOf(split) + split.Length);

                        RequestNumber = CommonFunctions.CsUploadDecrypt(result);
                    }


                    {
                        ShipmentAuthorizationModel.RequestNumber = RequestNumber;
                        ShipmentAuthorizationModel.RequesterUserId = int.Parse(Session["UserId"].ToString());//Session["Userid"].ToString();
                        Session["RequestNumber"] = RequestNumber;
                    }

                }
                if (Request.QueryString["Renewal"] != null)
                {
                    string Renewal = CommonFunctions.CsUploadDecrypt(Request.QueryString["Renewal"].ToString());
                    if (Renewal != "")
                    {
                        ShipmentAuthorizationModel.Renewal = Renewal;
                    }
                    else
                    {
                        ShipmentAuthorizationModel.Renewal = "0";
                    }
                   
                }
                else
                {
                    ShipmentAuthorizationModel.Renewal = "0";
                }
                if (Request.QueryString["Edit"] != null)
                {
                    string Edit = CommonFunctions.CsUploadDecrypt(Request.QueryString["Edit"].ToString());
                    if (Edit != "")
                    {
                        ShipmentAuthorizationModel.Edit = Edit;
                    }
                    else
                    {
                        ShipmentAuthorizationModel.Edit = "0";
                    }
                }
                else
                {
                    ShipmentAuthorizationModel.Edit = "0";
                }

                //ShipmentRequestDS = CommonFunctions.GetShipmentRequest(ShipmentAuthorizationModel);
                ShipmentRequestDS = dc.GetShipmentRequest(ShipmentAuthorizationModel);

                if (ShipmentRequestDS.Tables.Contains("shipmentRequest"))
                {
                    if (ShipmentRequestDS != null && ShipmentRequestDS.Tables["shipmentRequest"].Rows.Count > 0)

                    {
                        ShipmentAuthorizationModel.RequestNumber = ShipmentRequestDS.Tables["shipmentRequest"].Rows[0]["RequestNumber"].ToString();
                        ShipmentAuthorizationModel.CompanyName = ShipmentRequestDS.Tables["shipmentRequest"].Rows[0]["CompanyName"].ToString();
                        ShipmentAuthorizationModel.CommercialLicenseNo = ShipmentRequestDS.Tables["shipmentRequest"].Rows[0]["CommercialLicenseNo"].ToString();
                    }

                }
                List<SelectListItem> NT = new List<SelectListItem>();
                if (lang == "eng")
                {
                    NT = NationalityEng.NationalityType();
                }
                else
                {
                    NT = NationalityAra.NationalityType();
                }
                if (ShipmentRequestDS.Tables.Contains("shipmentRequestdetails"))
                {
                   

                    if (ShipmentRequestDS != null && ShipmentRequestDS.Tables["shipmentRequestdetails"].Rows.Count > 0)

                    {

                        var x = from Z in ShipmentRequestDS.Tables["shipmentRequestdetails"].AsEnumerable()
                                select new ShipmentAuthorizationDetails
                                {
                                    NameOfDelegate = Z["NameOfDelegate"].ToString(),
                                    CivilId = Z["CivilId"].ToString(),
                                    Delegationvalidity = Z["Delegationvalidity"].ToString(),
                                    Gender = Z["Gender"].ToString(),
                                    Nationality = int.Parse(Z["Nationality"].ToString()),

                                    NationalityType = NT,
                                    ShipmentAuthorizationDetailsSeqId= int.Parse(Z["seq"].ToString())
                                };

                        ShipmentAuthorizationModel.shipmentAuthorizationDetails = x.ToList();

                    }
                    else
                    {

                        //List<ShipmentAuthorizationDetails> shipmentAuthorizationDetailst = List<ShipmentAuthorizationDetails>();
                        //ShipmentAuthorizationDetails ShipmentAuthorizationDetailstt = new ShipmentAuthorizationDetails() { NameOfDelegate = "",NationalityType = NT ,ShipmentAuthorizationDetailsSeqId=0};
                        //shipmentAuthorizationDetailst.Add(ShipmentAuthorizationDetailstt);


                        //List<ShipmentAuthorizationDetails> shipmentAuthorizationDetailst = List<ShipmentAuthorizationDetails>{
                        //    new ShipmentAuthorizationDetails() { NameOfDelegate = "" ,NationalityType = NT}
                        //};

                        ShipmentAuthorizationModel.shipmentAuthorizationDetails = defaultshipmentAuthorizationDetails.getdefaultvalue(NT);
                    }
                }
                else
                {

                    //List<ShipmentAuthorizationDetails> shipmentAuthorizationDetailst = List<ShipmentAuthorizationDetails>();
                    //ShipmentAuthorizationDetails ShipmentAuthorizationDetailstt = new ShipmentAuthorizationDetails() { NameOfDelegate = "",NationalityType = NT ,ShipmentAuthorizationDetailsSeqId=0};
                    //shipmentAuthorizationDetailst.Add(ShipmentAuthorizationDetailstt);


                    //List<ShipmentAuthorizationDetails> shipmentAuthorizationDetailst = List<ShipmentAuthorizationDetails>{
                    //    new ShipmentAuthorizationDetails() { NameOfDelegate = "" ,NationalityType = NT}
                    //};

                    ShipmentAuthorizationModel.shipmentAuthorizationDetails = defaultshipmentAuthorizationDetails.getdefaultvalue(NT);
                }



                if (ShipmentRequestDS.Tables.Contains("EServiceDropdown"))
                {
                    if (ShipmentRequestDS != null && ShipmentRequestDS.Tables["EServiceDropdown"].Rows.Count > 0)

                    {
                        var DropdownBind = from s in ShipmentRequestDS.Tables["EServiceDropdown"].AsEnumerable()
                                           select new SelectListItem
                                           {
                                               Text = s["Name"].ToString(),
                                               Value = CommonFunctions.CsUploadEncrypt(s["DeclarationDocumentId"].ToString())

                                           };
                        ShipmentAuthorizationModel.ddlDocumentTypesitems = DropdownBind.ToList();

                    }

                }
                if (ShipmentRequestDS.Tables.Contains("EServiceUploadedFiles"))
                {
                    var docsUploadedlist = from s in ShipmentRequestDS.Tables["EServiceUploadedFiles"].AsEnumerable()
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

                    ShipmentAuthorizationModel.ListOfUploadedFiles = docsUploadedlist.ToList();
                }

                List<GenderTypes> G = new List<GenderTypes>();
                G.Add(new GenderTypes { GenderTypeID = 0, GenderTypeValue = Resources.Resource.GenderRequired });// Resources.Resource.CaptchaCode });// "Please select a UserType" });
                G.Add(new GenderTypes { GenderTypeID = 1, GenderTypeValue = Resources.Resource.male }); //Resources.Resource.CaptchaCode });// "SubBroker" });
                G.Add(new GenderTypes { GenderTypeID = 2, GenderTypeValue = Resources.Resource.female });// Resources.Resource.CaptchaCode });// "Messenger" });


                ViewBag.GenderType = G;

                return View(ShipmentAuthorizationModel);

            }
            catch(Exception e)
            {
                CommonFunctions.LogUserActivity("ShipmentReceivingAuthorizationGet", "", "", "", "", e.Message.ToString());
                return RedirectToAction("error", "error");
            }


        
        }
        [HttpPost]
        public ActionResult Index(ShipmentAuthorization ShipmentAuthorizationModel)
        {
            //return Json(new { message = ShipmentAuthorizationModel });
            Dataclass dc = new Dataclass();
            try//if (true)
            {
                DataSet ds = dc.ShipmentReceivingAuthorization(ShipmentAuthorizationModel);
                string Status = ds.Tables.Count>0? ds.Tables[0].Rows[0]["Status"].ToString():"2else";

                if (Status == "0")
                {
                    return Json(new { StatusCode = Status, message = Resources.Resource.AppointmentUpdatesuccess });
                }
                else if (Status == "1")
                    //upload the misssing documents
                {
                    return Json(new { StatusCode = Status, message = Resources.Resource.CannotprocessPleasecontactadministrator });
                }
                else
                {
                    return Json(new { StatusCode = Status, message = Resources.Resource.somethingwentwrong });
                }
            }
            catch (Exception e)//else
            {
                CommonFunctions.LogUserActivity("ShipmentReceivingAuthorizationPost", "", "", "", "", e.Message.ToString());
                return Json(new { StatusCode = -11, message = Resources.Resource.somethingwentwrong });
            }


        }

        public ActionResult test()
        {
            //ShipmentAuthorization sa = new ShipmentAuthorization();
            return View();
        }
        [HttpPost]
        public ActionResult test(ShipmentAuthorization ShipmentAuthorizationModel)
        {
            return View(ShipmentAuthorizationModel);
        }
    }
}
