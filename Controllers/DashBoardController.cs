using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DashBoardController : MyBaseController
    {
        Dataclass objdataclass = new Dataclass();
        // GET: DashBoard
        public ActionResult Index()
        {
            CheckSession();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogOut", "registration");
            }
            Dataclass objdataclass = new Dataclass();
            User user = new Models.User();
            user.mUserid = Session["UserId"].ToString();
            DataTable DashBoardDataTable = objdataclass.getUserDashBoard(user);

            ViewData.Model = DashBoardDataTable;

            return View();
        }

        public ActionResult MenuView()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Start", "registration");
            }

            List<AvailableEServices> PreferredServices = null;

            // Added This to Check, if The User Has an Approved Company or Not
            // If User Doesn't Have Approved Company, We will Prevent the User from using Services
            if (Session["LegalEntity"].ToString() == "2" &&
                Convert.ToBoolean(Session["ClearingAgentServices"]))
            {
                SecurityParams objmyorglist = new SecurityParams();
                DataTable dataTableOrg = new DataTable();

                objmyorglist.mUserid = Session["UserId"].ToString();
                objmyorglist.tokenId = Session["TokenId"].ToString();

                dataTableOrg = objdataclass.MyOrganizations_Data(objmyorglist);
                int[] demoArray = {
                         7,
                            14};


                if (dataTableOrg.Rows.Count < 1)
                {
                   /* ReqObj R = new ReqObj { ParentID = Convert.ToInt32(Session["UserId"].ToString()), ChildUser = 0, CommonData = Session["ServicePreference"].ToString() };
                    string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                    bool EnglishCulture = culture.Contains("English");

                    DataTable AvailableServices = objdataclass.GETParentUserActiveServices(R, false);
                    var a = from x in AvailableServices.AsEnumerable()
                            where (x["LegalEntityType"].ToString() == "1")//x["LegalEntityType"].ToString() == "0" ||
                            && Convert.ToInt32(x["ServiceId"]) != 0 && demoArray.Contains(Convert.ToInt32(x["ServiceId"]))  //This is not a actual service so , no need icon for this and not necessary to be part of home screen
                            select new AvailableEServices
                            {
                                SubscriptionId = x["SubscriptionId"].ToString(),
                                ServiceId = Convert.ToInt32(x["ServiceId"]),
                                ServiceNameEng = x["ServiceNameEng"].ToString(),
                                ServiceNameAra = x["ServiceNameAra"].ToString(),
                                LegalEntityType = x["LegalEntityType"].ToString(),
                                ServiceName = EnglishCulture ? x["ServiceNameEng"].ToString() : x["ServiceNameAra"].ToString()

                            };
                    PreferredServices = a.ToList();
                    */
                    return View(PreferredServices);
                }
            }

            // Till Here 


            if (Session["UserId"] != null)
            {
                string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

                bool EnglishCulture = culture.Contains("English");

                ReqObj R = new ReqObj { ParentID = Convert.ToInt32(Session["UserId"].ToString()), ChildUser = 0, CommonData = Session["ServicePreference"].ToString() };

                DataTable AvailableServices = objdataclass.GETParentUserActiveServices(R, false);

                //= new List<AvailableEServices>();

                if (Convert.ToBoolean(Session["ClearingAgentServices"]))
                {

                    var a = from x in AvailableServices.AsEnumerable()
                            where (x["LegalEntityType"].ToString() == "1")//x["LegalEntityType"].ToString() == "0" ||
                            && Convert.ToInt32(x["ServiceId"]) != 0 //This is not a actual service so , no need icon for this and not necessary to be part of home screen
                            select new AvailableEServices
                            {
                                SubscriptionId = x["SubscriptionId"].ToString(),
                                ServiceId = Convert.ToInt32(x["ServiceId"]),
                                ServiceNameEng = x["ServiceNameEng"].ToString(),
                                ServiceNameAra = x["ServiceNameAra"].ToString(),
                                LegalEntityType = x["LegalEntityType"].ToString(),
                                ServiceName = EnglishCulture ? x["ServiceNameEng"].ToString() : x["ServiceNameAra"].ToString()

                            };
                    PreferredServices = a.ToList();

                }
                else if (Convert.ToBoolean(Session["OrganizationServices"]))
                {
                     var a = from x in AvailableServices.AsEnumerable()
                            where ( (x["LegalEntityType"].ToString() == "2")//x["LegalEntityType"].ToString() == "0" ||
                        && Convert.ToInt32(x["ServiceId"]) != 0)  
                        // || ((x["LegalEntityType"].ToString() == "1")//x["LegalEntityType"].ToString() == "0" ||
                        // && Convert.ToInt32(x["ServiceId"]) == 34) //This is not a actual service so , no need icon for this and not necessary to be part of home screen
                            select new AvailableEServices
                            {
                                SubscriptionId = x["SubscriptionId"].ToString(),
                                ServiceId = Convert.ToInt32(x["ServiceId"]),
                                ServiceNameEng = x["ServiceNameEng"].ToString(),
                                ServiceNameAra = x["ServiceNameAra"].ToString(),
                                LegalEntityType = x["LegalEntityType"].ToString(),
                                ServiceName = EnglishCulture ? x["ServiceNameEng"].ToString() : x["ServiceNameAra"].ToString()

                            };
                    PreferredServices = a.ToList();
                }


                return View(PreferredServices);
            }
            else
            {
                return RedirectToAction("Start", "registration");
            }

        }


    }
}