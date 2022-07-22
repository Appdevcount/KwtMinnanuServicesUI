using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class SecurityParams
    {
        private string dtype = string.Empty;
        public string deviceType
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Request.Browser.IsMobileDevice)
                    {
                        dtype = "Android";
                    }
                    else
                    {
                        dtype = "Browser";
                    }
                }
                return dtype;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Request.Browser.IsMobileDevice)
                    {
                        dtype = "Android";
                    }
                    else
                    {
                        dtype = "Browser";
                    }
                }
            }
        }
        public string tokenId { get; set; }
        public string mUserid { get; set; }
    }
    public static class Constantclass
    {
        public const string number = "0";
        public const string number1 = "1";
        public const string number2 = "2";
        public const string number3 = "3";
        public const string number4 = "4";
        public const string number5 = "5";
        public const string number6 = "6";
        public const string number7 = "7";
        public const string number8 = "8";
        public const string number9 = "9";
        public const string number10 = "10";
        public const string disnone = "display: none;";
        public const string disblock = "display: block;";

        // User Already logged In
        public const string number99 = "99";

        // public const int TheAnswer = 42;
    }
    public class DeclarationSearchParams : SecurityParams
    {
        public string tempDeclNumber { get; set; }

    }
    public class DeclarationSearchByIdParams : SecurityParams
    {
        public string OrganizationId { get; set; }
        public string sOrgReqId { get; set; }
        public bool ApprovedDetail { get; set; }

    }
    public class GetOrganizationDocuments : SecurityParams
    {
        public string OrganizationId { get; set; }
        public string OrganizationRequestId { get; set; }

    }
    public class HBSearchParams : SecurityParams
    {
        public string hbNumber { get; set; }

    }
    public class HSCodeSearchParams : SecurityParams
    {

        public string data { get; set; }
        public string paramType { get; set; }
    }
    public class HSCodeTreeViewParams : SecurityParams
    {

        public string hsCodeId { get; set; }

    }
    public class Myaccountinput : SecurityParams
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Please Enter alphabets Only")]
        public string FirstName { set; get; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Please Enter alphabets Only")]
        public string LastName { set; get; }
        public string CivilId { set; get; }
        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Numeric values with 10 digits")]
        //[RegularExpression(@"^[7-9][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$", ErrorMessage = "")]
        public string MobileTelNumber { set; get; }
        public string EmailId { set; get; }
        public string LicenseNumber { set; get; }
    }
    public class LogOnRequest
    {
        private string dtype = string.Empty;
        [Required(ErrorMessage = "Please Enter UserName")]
        public string email { set; get; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string pwd { set; get; }
        public string Lang { set; get; }
        public string deviceType
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Request.Browser.IsMobileDevice)
                    {
                        dtype = "Android";
                    }
                    else
                    {
                        dtype = "Browser";
                    }
                }
                return dtype;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Request.Browser.IsMobileDevice)
                    {
                        dtype = "Android";
                    }
                    else
                    {
                        dtype = "Browser";
                    }
                }
            }
        }
        public bool RememberMe { get; set; }
        public string gln { get; set; }
    }
    public class VerifyOTPParams : SecurityParams
    {
        [Required]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ReferenceId { get; set; }
        public int ParentID { get; set; }
        public string Lang { get; set; }

    }
    public class ReSendOTPParams : SecurityParams
    {

        public string rType { get; set; }

    }
    public class langParams
    {

        public string lang { get; set; }

    }
    public class Doctypesinput
    {

        public string lang { get; set; }
        public string sOrgReqId { get; set; }
        public string sOrgId { get; set; }
    }
    public class Epaymentlist : SecurityParams
    {

        public string lang { get; set; }
        public string pagenumber { get; set; }
        public string searchCriteria { get; set; }
        public string searchDropdown { get; set; }
        public List<SelectListItem> searchtypes { get; set; }

    }
    public class EPaymentRequestInfoParams : SecurityParams
    {
        public string RequestNo { get; set; }
        public string lang { get; set; }

    }
    public class CallbackRedirectInfo : SecurityParams
    {
        public string OPTokenId { get; set; }
        public string RedirectUrl { get; set; }
        public string EpayReqNo { get; set; }
    }

    public class ForgotPassWordInput
    {

        public string emailId { get; set; }
        public string mobileNo { get; set; }
        public int CaptchaId { get; set; }
        public string CaptchaValue { get; set; }
    }


    public class ResetPwdByOTPParams
    {
        public string otpId { get; set; }
        public string otpValue { get; set; }
        public string mUserid { get; set; }
        public string newPwd { get; set; }


    }
    public class EPaymentRequestDetailsParams : SecurityParams
    {
        public string lang { get; set; }
    }

    public class OrgReqResultInfoParams : SecurityParams
    {

        public string sOrgReqId { get; set; }

    }
    public class OrgReqdeleteParams : SecurityParams
    {

        public String OrganizationRequestId { get; set; }

    }
    public class OrgReqResultDocInfoParams : SecurityParams
    {

        public String EserviceRequestid { get; set; }
        public String sOrgReqDocId { get; set; }

    }
    public class ValidateContacts : SecurityParams
    {
        public String Reference { get; set; }
        public String ReferenceType { get; set; }
        public String ReferenceId { get; set; }
    }
    public class OpenDocumentParams : SecurityParams
    {
        public string DocumentId { get; set; }
        public string EserviceRequestid { get; set; }
        public string hiderefprofile { get; set; }
        public string tokenId { get; set; }

    }

    public class Verifypassword : SecurityParams
    {
        public string email { get; set; }
        public string password { get; set; }

    }

}


//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace WebApplication1.Models
//{
//    public class SecurityParams
//    {
//        private string dtype = string.Empty;
//        public string deviceType
//        {
//            get
//            {
//                if (HttpContext.Current != null)
//                {
//                    if (HttpContext.Current.Request.Browser.IsMobileDevice)
//                    {
//                        dtype = "Android";
//                    }
//                    else
//                    {
//                        dtype = "Browser";
//                    }
//                }
//                return dtype;
//            }
//            set
//            {
//                if (HttpContext.Current != null)
//                {
//                    if (HttpContext.Current.Request.Browser.IsMobileDevice)
//                    {
//                        dtype = "Android";
//                    }
//                    else
//                    {
//                        dtype = "Browser";
//                    }
//                }
//            }
//        }
//        public string tokenId { get; set; }
//        public string mUserid { get; set; }
//    }
//    public static class Constantclass
//    {
//        public const string number = "0";
//        public const string number1 = "1";
//        public const string number2 = "2";
//        public const string number3 = "3";
//        public const string number4 = "4";
//        public const string number5 = "5";
//        public const string number6 = "6";
//        public const string number7 = "7";
//        public const string number8 = "8";
//        public const string number9 = "9";
//        public const string number10 = "10";
//        public const string disnone = "display: none;";
//        public const string disblock = "display: block;";
//        // public const int TheAnswer = 42;
//    }
//    public class DeclarationSearchParams : SecurityParams
//    {
//        public string tempDeclNumber { get; set; }

//    }
//    public class DeclarationSearchByIdParams : SecurityParams
//    {
//        public string OrganizationId { get; set; }
//        public string sOrgReqId { get; set; }

//    }
//    public class HBSearchParams : SecurityParams
//    {
//        public string hbNumber { get; set; }

//    }
//    public class HSCodeSearchParams : SecurityParams
//    {

//        public string data { get; set; }
//        public string paramType { get; set; }
//    }
//    public class HSCodeTreeViewParams : SecurityParams
//    {

//        public string hsCodeId { get; set; }

//    }
//    public class Myaccountinput : SecurityParams
//    {
//        [Required]
//        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Please Enter alphabets Only")]
//        public string FirstName { set; get; }
//        [Required]
//        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Please Enter alphabets Only")]
//        public string LastName { set; get; }
//        public string CivilId { set; get; }
//        [Required]
//        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Numeric values with 10 digits")]
//        //[RegularExpression(@"^[7-9][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$", ErrorMessage = "")]
//        public string MobileTelNumber { set; get; }
//        public string EmailId { set; get; }
//    }
//    public class LogOnRequest
//    {
//        private string dtype = string.Empty;
//        [Required(ErrorMessage = "Please Enter UserName")]
//        public string email { set; get; }
//        [Required(ErrorMessage = "Please Enter Password")]
//        public string pwd { set; get; }
//        public string Lang { set; get; }
//        public string deviceType
//        {
//            get
//            {
//                if (HttpContext.Current != null)
//                {
//                    if (HttpContext.Current.Request.Browser.IsMobileDevice)
//                    {
//                        dtype = "Android";
//                    }
//                    else
//                    {
//                        dtype = "Browser";
//                    }
//                }
//                return dtype;
//            }
//            set
//            {
//                if (HttpContext.Current != null)
//                {
//                    if (HttpContext.Current.Request.Browser.IsMobileDevice)
//                    {
//                        dtype = "Android";
//                    }
//                    else
//                    {
//                        dtype = "Browser";
//                    }
//                }
//            }
//        }
//        public bool RememberMe { get; set; }
//        public string gln { get; set; }
//    }
//    public class VerifyOTPParams : SecurityParams
//    {
//        [Required]
//        public string Email { get; set; }
//        public string Mobile { get; set; }
//        public string ReferenceId { get; set; }
//    }
//    public class ReSendOTPParams : SecurityParams
//    {

//        public string rType { get; set; }

//    }
//    public class langParams
//    {

//        public string lang { get; set; }

//    }
//    public class Doctypesinput
//    {

//        public string lang { get; set; }
//        public string sOrgReqId { get; set; }
//    }
//    public class Epaymentlist : SecurityParams
//    {

//        public string lang { get; set; }
//        public string pagenumber { get; set; }
//        public string searchCriteria { get; set; }
//        public string searchDropdown { get; set; }
//        public List<SelectListItem> searchtypes { get; set; }

//    }
//    public class EPaymentRequestInfoParams : SecurityParams
//    {
//        public string RequestNo { get; set; }
//        public string lang { get; set; }

//    }
//    public class CallbackRedirectInfo : SecurityParams
//    {
//        public string OPTokenId { get; set; }
//        public string RedirectUrl { get; set; }
//        public string EpayReqNo { get; set; }
//    }

//    public class ForgotPassWordInput
//    {

//        public string emailId { get; set; }
//        public string mobileNo { get; set; }
//        public int CaptchaId { get; set; }
//        public string CaptchaValue { get; set; }
//    }


//    public class ResetPwdByOTPParams
//    {
//        public string otpId { get; set; }
//        public string otpValue { get; set; }
//        public string mUserid { get; set; }
//        public string newPwd { get; set; }


//    }
//    public class EPaymentRequestDetailsParams : SecurityParams
//    {
//        public string lang { get; set; }
//    }

//    public class OrgReqResultInfoParams : SecurityParams
//    {

//        public string sOrgReqId { get; set; }

//    }
//    public class OrgReqdeleteParams : SecurityParams
//    {

//        public string OrganizationRequestId { get; set; }

//    }
//    public class OrgReqResultDocInfoParams : SecurityParams
//    {

//        public string EserviceRequestid { get; set; }
//        public string sOrgReqDocId { get; set; }

//    }
//    public class ValidateContacts : SecurityParams
//    {
//        public string Reference { get; set; }
//        public string ReferenceType { get; set; }
//        public string ReferenceId { get; set; }
//    }
//    public class OpenDocumentParams : SecurityParams
//    {
//        public string DocumentId { get; set; }
//        public string EserviceRequestid { get; set; }
//        public string hiderefprofile { get; set; }
//    }

//    public class Verifypassword : SecurityParams
//    {
//        public string email { get; set; }
//        public string password { get; set; }

//    }

//}