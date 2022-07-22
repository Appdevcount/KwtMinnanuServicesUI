using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Diagnostics;
using KnetPayment;
using System.Text;

namespace WebApplication1.Models
{
    public class GCSKnetDataclass
    {
        public static ReceiptAction IsReceiptValid(GCSReqObj ReqObj)
        {
            //string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

            //bool EnglishCulture = culture.Contains("English");
            //ReqObj.CommonData5 = EnglishCulture ? "eng" : "ara";
            Uri apiUrl = new Uri(ConfigurationManager.AppSettings["IsReceiptValid"].ToString());

            string ReqObjj = (new JavaScriptSerializer()).Serialize(ReqObj);

            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(ReqObjj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, inputContent).Result;
            ReceiptAction ReceiptAction = null;
            if (response.IsSuccessStatusCode)
            {
                ReceiptAction = (new JavaScriptSerializer()).Deserialize<ReceiptAction>(response.Content.ReadAsStringAsync().Result);

            }
            return ReceiptAction;
        }
        public static VerifyReceiptDetailsforGCSSite VerifyReceiptDetailsforGCSSite(GCSReqObj ReqObj)
        {

            Uri apiUrl = new Uri(ConfigurationManager.AppSettings["VerifyReceiptDetailsforGCSSite"].ToString());

            string ReqObjj = (new JavaScriptSerializer()).Serialize(ReqObj);

            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(ReqObjj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, inputContent).Result;
            VerifyReceiptDetailsforGCSSite VerifyReceiptDetailsforGCSSite = null;
            if (response.IsSuccessStatusCode)
            {
                VerifyReceiptDetailsforGCSSite = (new JavaScriptSerializer()).Deserialize<VerifyReceiptDetailsforGCSSite>(response.Content.ReadAsStringAsync().Result);

            }
            return VerifyReceiptDetailsforGCSSite;
        }

        public static ReceiptDetailsMinified GetPaymentDetailsforGCSSite(GCSReqObj ReqObj)
        {
            //string culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

            //bool EnglishCulture = culture.Contains("English");
            //ReqObj.CommonData5 = EnglishCulture ? "eng" : "ara";

            Uri apiUrl = new Uri(ConfigurationManager.AppSettings["GetPaymentDetailsforGCSSite"].ToString());

            string ReqObjj = (new JavaScriptSerializer()).Serialize(ReqObj);

            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(ReqObjj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, inputContent).Result;
            ReceiptDetailsMinified ReceiptDetailsMinified = null;
            if (response.IsSuccessStatusCode)
            {
                ReceiptDetailsMinified = (new JavaScriptSerializer()).Deserialize<ReceiptDetailsMinified>(response.Content.ReadAsStringAsync().Result);

            }
            return ReceiptDetailsMinified;
        }
        public static string ExplicitDecryptTokenCall(string data)
        {
            GCSReqObj ReqObj = new GCSReqObj { CommonData = data };
            Uri apiUrl = new Uri(ConfigurationManager.AppSettings["ExplicitDecryptTokenCall"].ToString());

            string ReqObjj = (new JavaScriptSerializer()).Serialize(ReqObj);

            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(ReqObjj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, inputContent).Result;
            GCSResp GCSResp = null;
            if (response.IsSuccessStatusCode)
            {
                GCSResp = (new JavaScriptSerializer()).Deserialize<GCSResp>(response.Content.ReadAsStringAsync().Result);

            }
            return GCSResp.CommonData;
        }
        public static string Encrypt(string data)
        {
            GCSReqObj ReqObj = new GCSReqObj { CommonData = data };
            Uri apiUrl = new Uri(ConfigurationManager.AppSettings["Encrypt"].ToString());

            string ReqObjj = (new JavaScriptSerializer()).Serialize(ReqObj);

            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(ReqObjj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, inputContent).Result;
            GCSResp GCSResp = null;
            if (response.IsSuccessStatusCode)
            {
                GCSResp = (new JavaScriptSerializer()).Deserialize<GCSResp>(response.Content.ReadAsStringAsync().Result);

            }
            return GCSResp.CommonData;
        }


    }
}