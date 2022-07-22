using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApplication1.Models
{
    public class Dataclass
    {

        #region BrokerEserviceRenewal
        public DataSet GetRequestDetaillist(BrokerRenewalModel objBrokerRenwalModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetRequestDetaillist"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerRenewalModel>("GetRequestDetaillist", objBrokerRenwalModel);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }

        public DataSet GetBrokersDetaillist(BrokerRenewalModel objBrokerRenwalModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetBrokerDetailslist"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerRenewalModel>("GetBrokerDetailslist", objBrokerRenwalModel);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }


        // Get Request Number
        public DataSet FNGetBrokerDetailsForUpdate(BrokerUpdateModel objBrokerRenwalModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetBrokerDetails"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerUpdateModel>("GetBrokerDetails", objBrokerRenwalModel);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }
        //FNCheckAutheticationForBrokerRenwalPostRedirectAfterPayemnts
        public DataSet FNCheckAutheticationForBrokerRenwal(BrokerRenewalModel objBrokerRenwalModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetFNCheckAutheticationForBrokerRenwal"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerRenewalModel>("GetFNCheckAutheticationForBrokerRenwal", objBrokerRenwalModel);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }
        public DataSet BrokerDetailspost(BrokerUpdateModel objBrokerRenwalModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UpdateBrokerUserDetails"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerUpdateModel>("UpdateBrokerUserDetails", objBrokerRenwalModel);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }
        #endregion
        public DataTable Logonmethod(LogOnRequest objlogon)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["LogOnAction"].ToString());
                //HTTP POST
                var postTask = client.PostAsJsonAsync<LogOnRequest>("LogOnAction", objlogon);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }

        public DataTable SignUp(User objuser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["SignUp"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<User>("SignUp", objuser);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable GetCivilIDDetailsFromMoci(User objuser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetCivilIDDetailsFromMoci"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<User>("GetCivilIDDetailsFromMoci", objuser);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable GetGovernorates()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetGovernorates"].ToString());

                //HTTP POST
                ReqObj objuser = new ReqObj();
                var postTask = client.PostAsJsonAsync<ReqObj>("GetGovernorates", objuser);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable GovernorateDetails(ReqObj objuser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GovernorateDetails"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("GovernorateDetails", objuser);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable UniqueTradeLicenseCheck(ReqObj objuser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UniqueTradeLicenseCheck"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("UniqueTradeLicenseCheck", objuser);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable GETParentUserActiveServices(ReqObj R, bool ASSOCPURPOSE = true)//int ParentID)
        {
            //if (!ASSOCPURPOSE)//TO not GET ASSOCIATED SERVICES AT ENTITY LEVEL //This param is not used now
            //{
            //    R.Action = "False";
            //}
            //else
            //{
            //    R.Action = "True";
            //}
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GETParentUserActiveServices"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("GETParentUserActiveServices", R);//?ParentID=" + ParentID, null);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable GETAdminUserOrganization(ReqObj R)//int ParentID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GETAdminUserOrganization"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("GETAdminUserOrganization", R);//?ParentID=" + ParentID, null);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable GETChildUsers(ReqObj R)//int ParentID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GETChildUsers"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("GETChildUsers", R);//?ParentID=" + ParentID, null);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataSet GETUserDetail(ReqObj R)//int ParentID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GETUserDetail"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("GETUserDetail", R);//?ParentID=" + ParentID, null);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        public DataTable CanCreateOrg(ReqObj R)//int ParentID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CanCreateOrg"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("CanCreateOrg", R);//?ParentID=" + ParentID, null);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable DeActivateChildUser(ReqObj R)//int ParentID, int ChildUser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["DeActivateChildUser"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("DeActivateChildUser", R);//?ParentID=" + ParentID + "&ChildUser=" + ChildUser, null);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable ServicesAndOrgManagementFortheUser(ServicesAndOrgManagementFortheUser R)//int ParentID, int ChildUser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServicesAndOrgManagementFortheUser"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ServicesAndOrgManagementFortheUser>("ServicesAndOrgManagementFortheUser", R);//?ParentID=" + ParentID + "&ChildUser=" + ChildUser, null);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable UserSignUp(UserProfile objuser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UserSignUp"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<UserProfile>("UserSignUp", objuser);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable ReSendOTP(ReSendOTPParams ObjReSendOTPParams)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ReSendOTP"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReSendOTPParams>("ReSendOTP", ObjReSendOTPParams);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable EmailVerificationdata(VerifyOTPParams objVerifyOTPParams)
        {
            using (var client = new HttpClient())
            {
                string url = string.Empty;
                string urlmethod = string.Empty;
                if (objVerifyOTPParams.ReferenceId != null)
                {
                    url = ConfigurationManager.AppSettings["VerifyOrgEmail"].ToString();
                    urlmethod = "VerifyOrgEmail";
                }
                else
                {
                    url = ConfigurationManager.AppSettings["VerifyOTP"].ToString();
                    urlmethod = "VerifyOTP";
                }
                client.BaseAddress = new Uri(url);
                //HTTP POST
                var postTask = client.PostAsJsonAsync<VerifyOTPParams>(urlmethod, objVerifyOTPParams);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable ExistingOrgRequestFortheUser(ReqObj R)//int ParentID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ExistingOrgRequestFortheUser"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("ExistingOrgRequestFortheUser", R);//?ParentID=" + ParentID, null);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
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


        public String Requestprint(BrokerUpdateModel BrokerPrint)
        {
            string res = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ERequestPrintHtml"].ToString());
                WriteToLogFile(client.BaseAddress.ToString(), "public String Requestprint(BrokerUpdateModel BrokerPrint)");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerUpdateModel>("ERequestPrintHtml", BrokerPrint);
                postTask.Wait();

                var result = postTask.Result;
                WriteToLogFile(result.IsSuccessStatusCode.ToString(), "publi 222c String Requestprint(BrokerUpdateModel BrokerPrint)");

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonres = readTask.Result;
                    check(jsonres.ToString());
                    res = jsonres.ToString();
                }
                return res;
            }
        }
        public String OrgRequestprint(OrgReqResultInfoParams OrgReqPrint)
        {
            string res = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["OrgRequestPrintHtml"].ToString());
                WriteToLogFile(client.BaseAddress.ToString(), "OrgRequestprint(OrgReqResultInfoParams OrgReqPrint)");
                var postTask = client.PostAsJsonAsync<OrgReqResultInfoParams>("OrgRequestPrintHtml", OrgReqPrint);
                postTask.Wait();

                var result = postTask.Result;
                WriteToLogFile(result.IsSuccessStatusCode.ToString(), "OrgRequestprint(OrgReqResultInfoParams OrgReqPrint)");

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonres = readTask.Result;
                    check(jsonres.ToString());
                    res = jsonres.ToString();
                }
                return res;
            }
        }
        public String Paymentprint(paymentprint objpaymentprint)
        {
            string res = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EPaymentReceiptPrintHtml"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<paymentprint>("EPaymentReceiptPrintHtml", objpaymentprint);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonres = readTask.Result;
                    check(jsonres.ToString());
                    res = jsonres.ToString();
                }
                return res;
            }
        }
        public Captcha GetCaptcha(Captcha objCaptcha)
        {
            Captcha objCaptchadetails = new Captcha();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetCaptcha"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Captcha>("GetCaptcha", objCaptcha);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonres = readTask.Result;
                    check(jsonres.ToString());
                    objCaptchadetails = JsonConvert.DeserializeObject<Captcha>(jsonres.ToString());

                }
                return objCaptchadetails;
            }
        }
        public List<CommLicSubTypeslist> GetCommLicSubTypes(langParams objlangParams)
        {
            List<CommLicSubTypeslist> objCaptchadetailslist = new List<CommLicSubTypeslist>();
            CommLicSubTypeslist objCaptchadetails = new CommLicSubTypeslist();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CommLicSubTypes"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<langParams>("CommLicSubTypes", objlangParams);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonres = readTask.Result;
                    check(jsonres.ToString());
                    objCaptchadetails = JsonConvert.DeserializeObject<CommLicSubTypeslist>(jsonres.ToString());
                    objCaptchadetailslist.Add(objCaptchadetails);

                }
                return objCaptchadetailslist;
            }
        }

        //ForgotPwdOTP
        public DataTable ForgotPwdOTPResult(ForgotPwdOTPinput objinput)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ForgotPwdOTP"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ForgotPwdOTPinput>("ForgotPwdOTP", objinput);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable ResetPwdOTPResult(ResetPwdByOTPParams objinput)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ResetPwdByOTP"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ResetPwdByOTPParams>("ResetPwdByOTP", objinput);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataSet NotifyCount(SecurityParams objSecurityParams)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["NCount"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<SecurityParams>("Count", objSecurityParams);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        public List<OrgRequestlist> Notifylist(SecurityParams objSecurityParams)
        {
            List<OrgRequestlist> objNotifylist = new List<OrgRequestlist>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["NList"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<SecurityParams>("List", objSecurityParams);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonres = readTask.Result;
                    check(jsonres.ToString());
                    OrgRequestlist objorgreddata = JsonConvert.DeserializeObject<OrgRequestlist>(jsonres.ToString());
                    objNotifylist.Add(objorgreddata);

                }
                return objNotifylist;
            }
        }

        public DataTable MyOrganizations_Data(SecurityParams objmyorglist)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["OList"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<SecurityParams>("List", objmyorglist);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        private void check(string jsonres)
        {
            var jsonser = new JavaScriptSerializer();
            var obj = jsonser.Deserialize<dynamic>(jsonres);
            foreach (var x in obj)
            {
                if (x.Key == "status")
                {
                    if (x.Value == "-1")
                    {
                        HttpContext.Current.Session["DataStatus"] = "-1";
                    }
                    else
                    {
                        HttpContext.Current.Session["DataStatus"] = "0";
                    }

                }


            }
        }

        private string Status(string jsonres)
        {
            var jsonser = new JavaScriptSerializer();
            var obj = jsonser.Deserialize<dynamic>(jsonres);
            foreach (var x in obj)
            {
                if (x.Key == "status")
                {
                    return (x.Value);


                }


            }
            return "";
        }
        public DataTable Declarationsearch(DeclarationSearch objDeclarationSearch)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Declaration"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<DeclarationSearch>("Declaration", objDeclarationSearch);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable GetBrokerSubOrdinateDetails(BrokerSubOrdinateReq BrokerSubOrdinateReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetBrokerSubOrdinateDetails"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerSubOrdinateReq>("GetBrokerSubOrdinateDetails", BrokerSubOrdinateReq);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable HousebillSearch(HousebillSearch objHousebillSearch)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["HouseBill"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<HousebillSearch>("HouseBill", objHousebillSearch);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataSet Myaccount(SecurityParams objSecurityParams)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UserDetails"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<SecurityParams>("UserDetails", objSecurityParams);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }
        public DataTable Myaccountpost(User ObjUserDetails)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UpdateUserDetails"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<User>("UpdateUserDetails", ObjUserDetails);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable HSCodeSearch(HSCodeSearch objHSCodeSearch)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["HsCode"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<HSCodeSearch>("HsCode", objHSCodeSearch);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataSet HSCodeSearchid(HSCodeSearchid objHSCodeSearchid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["HSTree"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<HSCodeSearchid>("Tree", objHSCodeSearchid);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        public List<OrgRequestlist> OrgRegistration_Data(DeclarationSearchByIdParams objOrgreg, string Requesttype)
        {
            List<OrgRequestlist> objorgreddatalist = new List<OrgRequestlist>();
            using (var client = new HttpClient())
            {
                string url = string.Empty;
                string urlmethod = string.Empty;
                if (Requesttype == "Status")
                {
                    url = ConfigurationManager.AppSettings["OrgGet"].ToString();
                    urlmethod = "Get";
                }
                if (Requesttype == "created")
                {
                    url = ConfigurationManager.AppSettings["OrgReqForUpdate"].ToString();
                    urlmethod = "ReqForUpdate";
                }
                client.BaseAddress = new Uri(url);
                //HTTP POST
                var postTask = client.PostAsJsonAsync<DeclarationSearchByIdParams>(urlmethod, objOrgreg);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonresult = readTask.Result;
                    check(jsonresult.ToString());
                    DataSet ds = GetDataTableFromJsonString1(jsonresult);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].TableName != null)
                            {
                                jsonresult = jsonresult.ToString().Replace(ds.Tables[1].TableName.ToString(), "OrgGetBasicResult");
                            }
                        }
                        if (ds.Tables.Count > 2)
                        {
                            if (ds.Tables[2].TableName != null)
                            {
                                jsonresult = jsonresult.ToString().Replace(ds.Tables[2].TableName.ToString(), "OrgGetIndustrialResult");
                            }
                        }
                        if (ds.Tables.Count > 3)
                        {
                            if (ds.Tables[3].TableName != null)
                            {
                                jsonresult = jsonresult.ToString().Replace(ds.Tables[3].TableName.ToString(), "OrgGetImportLicenseResult");
                            }
                        }
                        if (ds.Tables.Count > 4)
                        {
                            if (ds.Tables[4].TableName != null)
                            {
                                jsonresult = jsonresult.ToString().Replace(ds.Tables[4].TableName.ToString(), "OrgGetCommercialLicenseResult");
                            }
                        }
                        //int DocTablePosition;
                        //if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableAdditionalAuthSignatory"].ToString()))
                        //{
                        //    if (ds.Tables.Count > 5)//added newly
                        //    {
                        //        if (ds.Tables[4].TableName != null && ds.Tables[5].TableName == "OrgAuthorizedSignatories")//added it newly  ds.Tables[4].TableName== "OrgAuthorizedSignatories"
                        //        {
                        //            jsonresult = jsonresult.ToString().Replace(ds.Tables[5].TableName.ToString(), "OrgAuthorizedSignatories");
                        //        }
                        //    }
                        //     DocTablePosition = 6;
                        //}
                        //else
                        //{
                        //    DocTablePosition = 5;
                        //}

                        int DocTablePosition = 5;
                        if (ds.Tables.Contains("OrgAuthorizedSignatories"))
                        {
                            if (ds.Tables.Count > 5)//added newly
                            {
                                if (ds.Tables[5].TableName != null && ds.Tables[5].TableName == "OrgAuthorizedSignatories")//added it newly  ds.Tables[4].TableName== "OrgAuthorizedSignatories"
                                {
                                    jsonresult = jsonresult.ToString().Replace(ds.Tables[5].TableName.ToString(), "OrgAuthorizedSignatories");
                                }
                            }
                            DocTablePosition = 6;
                        }
                        if (ds.Tables.Count > DocTablePosition)
                        {
                            if (ds.Tables[DocTablePosition].TableName != null && ds.Tables[DocTablePosition].TableName == "OrgRequestGetDocumentsResult")//added it newly  && ds.Tables[5].TableName== "OrgGetDocumentsResult") as the sp is now midified to return authrized signatories as well. Without thins change, if the there is no docs the it takes authsignatory datatable as docs table and it causes exceoption in upload page
                            {
                                jsonresult = jsonresult.ToString().Replace(ds.Tables[DocTablePosition].TableName.ToString(), "OrgGetDocumentsResult");
                            }
                        }

                        //if (ds.Tables.Count > 5)//added newly
                        //{
                        //    if (ds.Tables[5].TableName != null && ds.Tables[5].TableName == "OrgAuthorizedSignatories")//added it newly  ds.Tables[4].TableName== "OrgAuthorizedSignatories"
                        //    {
                        //        jsonresult = jsonresult.ToString().Replace(ds.Tables[5].TableName.ToString(), "OrgAuthorizedSignatories");
                        //    }
                        //}
                        //if (ds.Tables.Count > 6)
                        //    {
                        //        if (ds.Tables[6].TableName != null && ds.Tables[6].TableName == "OrgRequestGetDocumentsResult")//added it newly  && ds.Tables[5].TableName== "OrgGetDocumentsResult") as the sp is now midified to return authrized signatories as well. Without thins change, if the there is no docs the it takes authsignatory datatable as docs table and it causes exceoption in upload page
                        //        {
                        //            jsonresult = jsonresult.ToString().Replace(ds.Tables[6].TableName.ToString(), "OrgGetDocumentsResult");
                        //        }
                        //    }

                    }
                    OrgRequestlist objorgreddata = JsonConvert.DeserializeObject<OrgRequestlist>(jsonresult.ToString());
                    objorgreddatalist.Add(objorgreddata);
                }
                return objorgreddatalist;
            }
        }
        public DataSet OrgRegistration_Data_create(OrgGetBasicResult objorgreg)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["OrgCreateUpdate"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgGetBasicResult>("CreateUpdate", objorgreg);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        public string Documentdelete(OrgReqResultDocInfoParams objdelete)
        {

            using (var client = new HttpClient())
            {
                DataSet ds = new DataSet();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["DeleteOrgReqDoc"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgReqResultDocInfoParams>("DeleteOrgReqDoc", objdelete);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonresult = readTask.Result;
                    check(jsonresult.ToString());
                    ds = GetDataTableFromJsonString1(jsonresult);
                    if (ds.Tables.Count > 0)
                    {

                    }
                    //  OrgReq1Result objorgreddata = JsonConvert.DeserializeObject<OrgReq1Result>(jsonresult.ToString());
                    // objorgreddatalist.Add(objorgreddata);
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
                return "";
            }
        }
        public DataSet OrgAuthPerson_Data_create(OrgAuthorizedSignatory OrgAuthSignatory)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ManageOrgAuthPerson"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgAuthorizedSignatory>("ManageOrgAuthPerson", OrgAuthSignatory);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        public DataSet Orgindustrial_Data_create(OrgGetIndustrialResult objorgind)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UpdateIndLic"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgGetIndustrialResult>("UpdateIndLic", objorgind);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        public DataSet Orgimporter_Data_create(OrgGetImportLicenseResult objorgimp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UpdateImpLic"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgGetImportLicenseResult>("UpdateImpLic", objorgimp);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        public DataSet Orgcommercial_Data_create(OrgGetCommercialLicenseResult objorgcomm)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UpdateCommLic"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgGetCommercialLicenseResult>("UpdateCommLic", objorgcomm);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        public List<DocTypeslist> doctypes(Doctypesinput objDoctypesinput)
        {
            List<DocTypeslist> objdoctypeslist = new List<DocTypeslist>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["DocTypes"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Doctypesinput>("DocTypes", objDoctypesinput);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonresult = readTask.Result;
                    check(jsonresult.ToString());
                    DataSet ds = GetDataTableFromJsonString1(jsonresult);
                    DocTypeslist objdoctypes = JsonConvert.DeserializeObject<DocTypeslist>(jsonresult.ToString());
                    objdoctypeslist.Add(objdoctypes);
                }
                return objdoctypeslist;
            }
        }
        public List<DocTypeslist> OrgDoctypes(Doctypesinput objDoctypesinput)
        {
            List<DocTypeslist> objdoctypeslist = new List<DocTypeslist>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["OrgDoctypes"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Doctypesinput>("OrgDoctypes", objDoctypesinput);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonresult = readTask.Result;
                    check(jsonresult.ToString());
                    DataSet ds = GetDataTableFromJsonString1(jsonresult);
                    DocTypeslist objdoctypes = JsonConvert.DeserializeObject<DocTypeslist>(jsonresult.ToString());
                    objdoctypeslist.Add(objdoctypes);
                }
                return objdoctypeslist;
            }
        }
        //public List<OrganizationDocumentDetail> GetOrganizationDocuments(GetOrganizationDocuments GetOrganizationDocuments)
        //{
        //    List<OrganizationDocumentDetail> OrganizationDocumentDetailList = new List<OrganizationDocumentDetail>();
        //    using (var client = new HttpClient())
        //    {

        //           string url = ConfigurationManager.AppSettings["GetOrganizationDocuments"].ToString();

        //        client.BaseAddress = new Uri(url);
        //        var postTask = client.PostAsJsonAsync<GetOrganizationDocuments>("GetOrganizationDocuments", GetOrganizationDocuments);
        //        postTask.Wait();

        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsStringAsync();
        //            readTask.Wait();
        //            var jsonresult = readTask.Result;
        //            check(jsonresult.ToString());
        //            DataSet ds = GetDataTableFromJsonString1(jsonresult);
        //            if (ds.Tables.Count > 0)
        //            {

        //                    if (ds.Tables[0].TableName != null)
        //                    {
        //                        jsonresult = jsonresult.ToString().Replace(ds.Tables[0].TableName.ToString(), "GetOrganizationDocuments");
        //                    }

        //            }
        //            OrganizationDocumentDetail OrganizationDocument = JsonConvert.DeserializeObject<OrganizationDocumentDetail>(jsonresult.ToString());
        //            OrganizationDocumentDetailList.Add(OrganizationDocument);
        //        }
        //        return OrganizationDocumentDetailList;
        //    }
        //}

        public DataTable GetOrganizationDocuments(GetOrganizationDocuments GetOrganizationDocuments)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetOrganizationDocuments"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<GetOrganizationDocuments>("GetOrganizationDocuments", GetOrganizationDocuments);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        //Org Req Status
        public DataTable OrgReqRequest_Status_Data(SecurityParams objmyorglist)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["OrgReqList"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<SecurityParams>("List", objmyorglist);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable UpdateUserSession(UserSession objmyorglist)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UpdateUserSession"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<UserSession>("ChangeUserSession", objmyorglist);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;

            }
        }
        public DataTable paymentsearchtypes(paymentsearchinput objpaymentsearchinput)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetEpaymentSearchDropdown"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<paymentsearchinput>("GetEpaymentSearchDropdown", objpaymentsearchinput);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable Payment_Datalist(Epaymentlist objmypaylist)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EPaymentRequestDetails"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Epaymentlist>("EPaymentRequestDetails", objmypaylist);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public List<OrgRequestlist> Payment_Datalist1(Epaymentlist objmypaylist)
        {
            List<OrgRequestlist> objpayinfo = new List<OrgRequestlist>();
            DataTable dtlogondetails = new DataTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EPaymentRequestDetails"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Epaymentlist>("EPaymentRequestDetails", objmypaylist);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonresult = readTask.Result;
                    check(jsonresult.ToString());
                    string jsonres = jsonresult.ToString().Replace("MCReferenceNumber", "DeliveryOrderNo");
                    DataSet dtlogondetails1 = GetDataTableFromJsonString1(jsonresult.ToString());
                    if (dtlogondetails1.Tables.Count > 0)
                    {
                        dtlogondetails = dtlogondetails1.Tables[0];
                        if (dtlogondetails.Columns.Contains("MCReferenceNumber"))
                        {
                            dtlogondetails.Columns["MCReferenceNumber"].ColumnName = "DeliveryOrderNo";
                        }
                    }
                    OrgRequestlist objorgreddata = JsonConvert.DeserializeObject<OrgRequestlist>(jsonresult.ToString());
                    objpayinfo.Add(objorgreddata);

                }
                return objpayinfo;
            }
        }
        public List<OrgRequestlist> Payment_Datainfo(EPaymentRequestInfoParams objmypaylistinfo)
        {
            List<OrgRequestlist> objpayinfo = new List<OrgRequestlist>();
            DataTable dtlogondetails = new DataTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EPaymentRequestInfo"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EPaymentRequestInfoParams>("EPaymentRequestInfo", objmypaylistinfo);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonresult = readTask.Result;
                    check(jsonresult.ToString());
                    DataSet dtlogondetails1 = GetDataTableFromJsonString1(jsonresult.ToString());
                    if (dtlogondetails1.Tables.Count > 0)
                    {
                        dtlogondetails = dtlogondetails1.Tables[0];
                    }
                    OrgRequestlist objorgreddata = JsonConvert.DeserializeObject<OrgRequestlist>(jsonresult.ToString());
                    objpayinfo.Add(objorgreddata);

                }
                return objpayinfo;
            }
        }
        //DenyPayRequest
        public DataTable DenyPayRequest(EPaymentRequestInfoParams objmypaylistinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["DenyPayRequest"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EPaymentRequestInfoParams>("DenyPayRequest", objmypaylistinfo);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        //EPaymentOnCallbackRedirect
        public string EPaymentOnCallbackRedirect(CallbackRedirectInfo objCallbackRedirectInfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EPaymentOnCallbackRedirect"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<CallbackRedirectInfo>("EPaymentOnCallbackRedirect", objCallbackRedirectInfo);
                //                DataTable dt = commonmethod_datatable(postTask);

                string dt = GetStatus(postTask);


                return dt;
            }
        }
        //DeleteOrgReq
        public DataTable DeleteOrgReq(OrgReqdeleteParams objOrgReqdeleteParams)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["DeleteOrgReq"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgReqdeleteParams>("DeleteOrgReq", objOrgReqdeleteParams);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public string SubmitOrgReqHtml(OrgReqResultInfoParams objOrgReqResultInfoParams)
        {
            string res = string.Empty;
            DataTable dtorgprint = new DataTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["SubmitOrgReqHtml"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgReqResultInfoParams>("SubmitOrgReqHtml", objOrgReqResultInfoParams);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonres = readTask.Result;
                    check(jsonres.ToString());
                    res = jsonres.ToString();
                }
                return res;
            }
        }

        //ValidateContactsWithOTP
        public DataTable ValidateContactsWithOTP(ValidateContacts ObjValidateContacts)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ValidateContactsWithOTP"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ValidateContacts>("ValidateContactsWithOTP", ObjValidateContacts);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }

        //CheckOrgEmailIsVerified
        public DataTable CheckOrgEmailIsVerified(OrgReqResultInfoParams objOrgReqResultInfoParams)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CheckOrgEmailIsVerified"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgReqResultInfoParams>("CheckOrgEmailIsVerified", objOrgReqResultInfoParams);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        //SubmitOrgReq
        public DataTable SubmitOrgReq(OrgReqResultInfoParams objOrgReqResultInfoParams)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["SubmitOrgReq"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgReqResultInfoParams>("SubmitOrgReq", objOrgReqResultInfoParams);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataSet GetDataTableFromJsonString1(string json)
        {
            var jsonLinq = JObject.Parse(json);
            DataSet ds = new DataSet();
            // Find the first array using Linq
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray);
            var trgArray = new JArray();
            foreach (JArray row in srcArray)
            {
                if (row.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = JsonConvert.DeserializeObject<DataTable>(row.ToString());
                    if (dt.Columns.Contains("TableName"))
                    {
                        dt.TableName = dt.Rows[0]["TableName"].ToString();
                    }
                    ds.Tables.Add(dt);
                }

            }

            return ds;
        }
        //commonmethod_datatable
        private DataTable commonmethod_datatable(System.Threading.Tasks.Task<HttpResponseMessage> postTask)
        {
            DataTable dtcommon = new DataTable();
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                var jsonres = readTask.Result;
                check(jsonres.ToString());
                DataSet dtlogondetails1 = GetDataTableFromJsonString1(jsonres.ToString());
                if (dtlogondetails1.Tables.Count > 0)
                {
                    dtcommon = dtlogondetails1.Tables[0];
                }
            }
            return dtcommon;
        }
        private string GetStatus(System.Threading.Tasks.Task<HttpResponseMessage> postTask)
        {
            DataTable dtcommon = new DataTable();
            string res = string.Empty;
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                var jsonres = readTask.Result;
                res = Status(jsonres.ToString());
                //  res = jsonres.ToString();
            }
            return res;
        }
        //commonmethod_dataset
        private DataSet commonmethod_dataset(System.Threading.Tasks.Task<HttpResponseMessage> postTask)
        {
            DataSet dscommon = new DataSet();
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                var jsonresult = readTask.Result;
                check(jsonresult.ToString());
                dscommon = GetDataTableFromJsonString1(jsonresult);
            }
            return dscommon;
        }
        //public DataTable GetDataTableFromJsonString(string json)
        //{
        //    var jsonLinq = JObject.Parse(json);

        //    // Find the first array using Linq
        //    var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
        //    var trgArray = new JArray();
        //    foreach (JObject row in srcArray.Children<JObject>())
        //    {
        //        var cleanRow = new JObject();
        //        foreach (JProperty column in row.Properties())
        //        {
        //            // Only include JValue types
        //            if (column.Value is JValue)
        //            {
        //                cleanRow.Add(column.Name, column.Value);
        //            }
        //        }
        //        trgArray.Add(cleanRow);
        //    }

        //    return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        //}
        //public static DataTable ConvertJsonToDatatable(string jsonString)
        //{
        //    var jsonLinq = JObject.Parse(jsonString);

        //    // Find the first array using Linq
        //    var linqArray = jsonLinq.Descendants().Where(x => x is JArray).First();
        //    var jsonArray = new JArray();
        //    foreach (JObject row in linqArray.Children<JObject>())
        //    {
        //        var createRow = new JObject();
        //        foreach (JProperty column in row.Properties())
        //        {
        //            // Only include JValue types
        //            if (column.Value is JValue)
        //            {
        //                createRow.Add(column.Name, column.Value);
        //            }
        //        }
        //        jsonArray.Add(createRow);
        //    }

        //    return JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
        //}
        //        DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);

        //        DataTable dataTable = dataSet.Tables["Table1"];

        //        Console.WriteLine(dataTable.Rows.Count);
        //// 2

        //foreach (DataRow row in dataTable.Rows)
        //{
        //    Console.WriteLine(row["id"] + " - " + row["item"]);
        //}


        public DataSet EserviceFileDownload(OpenDocumentParams OpenDocumentParams)
        {
            HttpResponseMessage file = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["OpenFileForEservice"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OpenDocumentParams>("OpenFileForEservice", OpenDocumentParams);
                DataSet dt = commonmethod_dataset(postTask);
                //   return "test";
                return dt;
            }

        }


        public string DocumentdeleteForEservice(OrgReqResultDocInfoParams objdelete)
        {

            using (var client = new HttpClient())
            {
                DataSet ds = new DataSet();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["DeleteOrgReqDocForEservice"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgReqResultDocInfoParams>("DeleteOrgReqDocForEservice", objdelete);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonresult = readTask.Result;
                    check(jsonresult.ToString());
                    ds = GetDataTableFromJsonString1(jsonresult);
                    if (ds.Tables.Count > 0)
                    {

                    }
                    //  OrgReq1Result objorgreddata = JsonConvert.DeserializeObject<OrgReq1Result>(jsonresult.ToString());
                    // objorgreddatalist.Add(objorgreddata);
                }
                else
                {
                    //Console.WriteLine(result.StatusCode);
                }
                return "";
            }
        }

        #region Entity  Service List 
        public DataSet FNGetEntityServiceList(BrokerServiceRequestModel objBrokerServiceRequestModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetEntityServiceList"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerServiceRequestModel>("GetEntityServiceList", objBrokerServiceRequestModel);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }

        public DataSet FNPostEntityServiceList(BrokerServiceRequestModel objBrokerServiceRequestModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["PostEntityServiceList"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerServiceRequestModel>("PostEntityServiceList", objBrokerServiceRequestModel);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }

        //azharfor mapp
        public DataSet GetuserOrgMap(OrgGetBasicResult objorgreg)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetuserOrgMap"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrgGetBasicResult>("GetuserOrgMap", objorgreg);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        #endregion




        #region E Service Request


        public DataTable getExamDetailsByEservicesRequestId(EservicesRequests EserviceRequest)
        {
            DataTable dataTable = new DataTable();

            dataTable = getExamDetailsByEserviceRequestId(EserviceRequest);

            return dataTable;
        }

        private DataTable getExamDetailsByEserviceRequestId(EservicesRequests EserviceRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["getExamDetailsByEservicesRequestId"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EservicesRequests>("getExamDetailsByEservicesRequestId", EserviceRequest);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }



        public DataTable ConfirmExamAttendance(ExamCandidateInfo examCandidateInfo)
        {
            DataTable dataTable = new DataTable();

            dataTable = ConfirmexamAttendance(examCandidateInfo);

            return dataTable;
        }

        private DataTable ConfirmexamAttendance(ExamCandidateInfo examCandidateInfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["updateExamCandidateInfo"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ExamCandidateInfo>("updateExamCandidateInfo", examCandidateInfo);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }




        public DataTable SubmitEservicesRequest(EservicesRequests eserviceRequest)
        {
            return SubmitEserviceRequest(eserviceRequest);
        }

        private DataTable SubmitEserviceRequest(EservicesRequests eserviceRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EServicesRequests"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EservicesRequests>("SubmitEserviceRequest", eserviceRequest);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }


        #region Get Request If Exists
        public DataTable GetRequestIfExists(RequestExists RequestExist)
        {
            return GetRequestIfExist(RequestExist);
        }

        private DataTable GetRequestIfExist(RequestExists RequestExist)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["RequestExist"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<RequestExists>("GetRequestIfExists", RequestExist);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }

        #endregion Get Request If Exists




        #region Get Request If Exists By Civil Id
        public DataTable GetRequestIfExistsByCivilId(RequestExistsByCivilId RequestExistByCivilId)
        {
            return GetRequestIfExistByCivilId(RequestExistByCivilId);
        }

        private DataTable GetRequestIfExistByCivilId(RequestExistsByCivilId RequestExistByCivilId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["RequestIfExistsByCivilId"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<RequestExistsByCivilId>("GetRequestIfExistsByCivilId", RequestExistByCivilId);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }

        #endregion Get Request If Exists By Civil Id


        #region InitiateExamRequest


        public DataSet InitiateExamRequest(examRequestViewModel examRequestViewMd)
        {
            return InitiateExamsRequest(examRequestViewMd);
        }

        private DataSet InitiateExamsRequest(examRequestViewModel examRequestViewMd)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["InitiateExamRequest"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<examRequestViewModel>("InitiateExamRequest", examRequestViewMd);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }

        #endregion InitiateExamRequest



        #endregion E Service Request



        public DataTable GETRequestListfortheUser(EserviceRequest data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["RequestList"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EserviceRequest>("RequestList", data);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable GETRequestDetailsfortheRequest(EserviceRequest data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["RequestDetail"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EserviceRequest>("Detail", data);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }


        //get broker Types List

        public DataSet FNGetBrokerTypelist(BrokerUpdateModel objBrokerRenwalModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetBrokerTypesList"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerUpdateModel>("GetBrokerTypesList", objBrokerRenwalModel);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;

            }
        }

        //check mandatory docs
        public DataSet BrokerAffairsDocsCheck(BrokerUpdateModel objBrokerRenwalModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BrokerAffairsDocsCheck"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BrokerUpdateModel>("BrokerAffairsDocsCheck", objBrokerRenwalModel);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }


        #region DashBoard
        public DataTable getUserDashBoard(User user)
        {
            return getUserDashBoardd(user);
        }

        private DataTable getUserDashBoardd(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["getUserDashBoard"].ToString());
                //HTTP POST
                var postTask = client.PostAsJsonAsync<User>("getUserDashBoard", user);
                DataTable ds = commonmethod_datatable(postTask);
                return ds;
            }
        }


        #endregion DashBoard

        #region user Activity


        public DataSet UserActivitypost(LogUserActivity objLogUserActivity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UpdateUserActivityDetails"].ToString());

                //HTTP DocTypes
                var postTask = client.PostAsJsonAsync<LogUserActivity>("UpdateUserActivityDetails", objLogUserActivity);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }
        #endregion Useractivity



        #region Update Request 29 Dec
        public DataSet updateRequest(Dictionary<String, String> requestToUpdate)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["updateRequest"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Dictionary<String, String>>("updateRequest", requestToUpdate);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;

            }
        }
        #endregion Update Request 29 Dec



        #region Inspection Appointment
        public DataSet GetVehicleList(ReqObj ro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["getVehicleListFromDO"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("getVehicleListFromDO", ro);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }
        public DataTable GetInspectionRounds(ReqObj ro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetInspectionRounds"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("GetInspectionRounds", ro);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable CreateInspectionAppointment(InspectionAppointment IA)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CreateInspectionAppointment"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<InspectionAppointment>("CreateInspectionAppointment", IA);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataTable UpdateInspectionAppointment(InspectionAppointment IA)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UpdateInspectionAppointment"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<InspectionAppointment>("UpdateInspectionAppointment", IA);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }

        public DataTable CancelInspectionAppointment(ReqObj ro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CancelInspectionAppointment"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("CancelInspectionAppointment", ro);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }
        public DataSet GetInspectionAppointmentDetails(ReqObj ro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetInspectionAppointmentDetails"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("GetInspectionAppointmentDetails", ro);
                DataSet ds = commonmethod_dataset(postTask);
                return ds;
            }
        }

        public DataTable GetInspectionAppointments(ReqObj ro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetInspectionAppointments"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ReqObj>("GetInspectionAppointments", ro);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }

        #endregion Inspection Appointment


        #region Shipping autorization

        public DataSet GetShipmentRequest(ShipmentAuthorization ro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["GetShipmentRequest"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ShipmentAuthorization>("GetShipmentRequest", ro);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }


        public DataSet ShipmentReceivingAuthorization(ShipmentAuthorization ro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ShipmentReceivingAuthorization"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ShipmentAuthorization>("ShipmentReceivingAuthorization", ro);
                DataSet dt = commonmethod_dataset(postTask);
                return dt;
            }
        }



        // Requests 
        public DataTable ShipmentReceivingAuthorizationGetRequests(EserviceRequest request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ShipmentReceivingAuthorizationRequests"].ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EserviceRequest>("ShipmentRequestList", request);
                DataTable dt = commonmethod_datatable(postTask);
                return dt;
            }
        }

        #endregion Shipping autorization
    }
}