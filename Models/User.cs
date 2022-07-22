using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User : SecurityParams
    {
        //[Required(ErrorMessage = "Please Enter FirstName.")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNamerDA")]
        [RegularExpression(@"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z ]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_ ]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersAndSpecialCharactersNotAllowed")]
        //[RegularExpression(@"^[a-zA-Z\s\.\&]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AlphaNumeric")]
        public string FirstName { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LastNamerDA")]
        //[RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AlphaNumeric")]
        [RegularExpression(@"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z ]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_ ]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersAndSpecialCharactersNotAllowed")]
        public string LastName { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        [RegularExpression(@"^(1|2|3)((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(\d{5})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        public string CivilId { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        [RegularExpression(@"^([0-9]{8})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        //[RegularExpression(@"^[7-9][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$", ErrorMessage = "")]
        public string MobileTelNumber { set; get; }
[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PhoneDontMatch")]
        [Compare("MobileTelNumber", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PhoneDontMatch")]
        public string ConfirmMobileTelNumber { set; get; }
        
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validemail")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validemail")]
        public string EmailId { set; get; }
  [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailDontMatch")]
        [Compare("EmailId", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailDontMatch")]
        public string ConfirmEmailId { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validpassword")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validpassword")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validpassword")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validpassword")]
        public string Pass { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Passwordsarenotmatching")]
        [Compare("Pass", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Passwordsarenotmatching")]
        public string ConfirmPass { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        [Range(typeof(bool), "false", "true", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        public bool Term_conditions { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PleaseselectaLegalEntity")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PleaseselectaLegalEntity")]
        [Display(Name = "LegalEntity")]
        public string LegalEntity { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "GenderRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "GenderRequired")]
        //[Display(Name = "Gender")]
        public string Gender { set; get; }
        //public List<UserType> UserTypes { get; set; }
 //by azhar for nationality on 01-21-2020
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "nationailityRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "nationailityRequired")]
        public string Nationality { set; get; }
        public string Language { set; get; }
        public int CaptchaId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EntercorrectCaptcha")]
        public string CaptchaValue { get; set; }

        public List<EService> AvailableEServices { get; set; }
        public List<Organization> AvailableOrganizations { get; set; }

        public string SelectedServices { get; set; }
        public string SelectedOrganizations { get; set; }

        public string LicenceNumber { get; set; }



  public string Themes { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PleaseselectaLegalEntity")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PleaseselectaLegalEntity")]
        [Display(Name = "LegalEntitySubType")]
        public string LegalEntitySubType { set; get; }

        public bool ExistingUser { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TradingLicenseRequired")]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TradingLicenseRequired")]
        public string TradeLicenseNumber { get; set; }
        //[Required]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter 10 digit Numeric GeneralBroker LicenseNumber")]
        [checkgeneralbrokerlicense(ErrorMessageResourceName = "GeneralBrokerLicenseRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string GeneralBrokerLicenseNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MCUserNameRequired")]
        public string MCUserID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MCPasswordRequired")]
        public string MCPassword { get; set; }
        public bool IsAdmin { get; set; }


        public bool SubUser { get; set; }
        public int ParentID { get; set; }


        public string Token { get; set; }//added newly for additional user cross check for - security feedback -Siraj


        public bool IsIndustrial { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "IndustrialLicenseNumberRequired")]
        public string IndustrialLicenseNumber { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CommercialLicensenumberRequired")]
        public string CommercialLicenseNumber { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        public string CommercialLicenseYear { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "GovernorateRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "GovernorateRequired")]
        public string Governorate { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RegionRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RegionRequired")]
        public string Region { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "BlockRequired")]
        public string Block { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "StreetRequired")]
        public string Street { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PostalCodeRequired")]
        public string PostalCode { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "addressRequired")]
        public string Address { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AgreeInfoCorrectnessRequired")]
        [Range(typeof(bool), "false", "true", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AgreeInfoCorrectnessRequired")]
        public bool AgreeInfoCorrectness { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj

 [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        public string CompanyCivilId { set; get; }
   // added regular expression for companycivil id by azhar 
        public string ImporterLicenseNumber { get; set; }//added newly OrgSvcNew Chnages 11-07-2019  -Azhar


        public string CompanyName { get; set; }

        //     Is Industrial
        //  Industrial License Number
        //    IndustrialLicenseNumberRequired-     Please enter the Industrial License Number

        // Commercial License Number
        //    CommercialLicenseNumberRequired-     Please enter the Commercial License Number
        // Governorate
        //    GovernorateRequired-     Please select the Governorate

        // Region
        //    RegionRequired-     Please select the Region
        //Block
        //BlockRequired - Please enter the Block

        // Street
        // StreetRequired-        Please enter the Street

        // Postal Code
        // PostalCodeRequired -        Please enter the Postal Code

        // Address
        //  AddressRequired-       Please enter the Address
        //Checkbox - AgreeInfoCorrectnessRequired -Please agree to correctness of the information

        //Company Legal Address
    }

    public class GovernorateRegions
    {
        public int GovernorateID { get; set; }
        public string GovernorateNameEng { get; set; }
        public string GovernorateNameAra { get; set; }
        public string GovernorateName { get; set; }
        public int RegionID { get; set; }
        public string RegionNameEng { get; set; }
        public string RegionNameAra { get; set; }
        public string RegionName { get; set; }
        public string PostalCode { get; set; }
    }

    public class EService
    {
        public int SubscriptionID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceNameEng { get; set; }
        public string ServiceNameAra { get; set; }
    }
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string OrgNameEng { get; set; }
        public string OrgNameAra { get; set; }
    }
    public class Manageuser
    {
        public List<EService> EServices { get; set; }
        public List<Organization> Organizations { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileTelNumber { set; get; }
        public string LicenceNumber { set; get; }
        public string OrgID { set; get; }
    }
    public class ChildUsers
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountStatus { get; set; }
        public string ServiceNameEng { get; set; }
        public string OrgNameEng { get; set; }
    }
    public class ChildUsersModel
    {
        public List<ChildUsers> ChildUsers { get; set; }
        public int ParentID { get; set; }
    }
    public class ReqObj
    {
        public int ParentID { get; set; }
        public int ChildUser { get; set; }
        public string Action { get; set; }
        public string CommonData { get; set; }
        public string CommonData1 { get; set; }
        public string CommonData2 { get; set; }
        public string OrgID { get; set; }

    }
    public class ResObj
    {
        public string Status { get; set; }
    }
    public class UserType
    {
        public int UserTypeID { get; set; }
        public string UserTypeName { get; set; }
    }
    public class LegalEntity
    {
        public int LegalEntityID { get; set; }
        public string LegalEntityName { get; set; }
    }
    public class Gender
    {
        public int GenderTypeID { get; set; }
        public string GenderTypeValue { get; set; }
    }
 //added by azhar for nationality in user regn
    public class Nationality
    {
        public int NationalityID { get; set; }
        public string NationalityValue { get; set; }
    }
    public class encrypt
    {
        public static string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }
    }
    public class Captcha
    {
        public int CaptchaId { get; set; }
        public string CaptchaImage { get; set; }
        public string CaptchaType { get; set; }
    }
    public class ForgotPwdOTPinput
    {
        public int CaptchaId { get; set; }
        [Required]
        public string CaptchaValue { get; set; }
        [Required]
        public string emailId { get; set; }
        public string mobileNo { get; set; }
        public string otpValue { get; set; }
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validpassword")]

        public string newPwd { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Passwordsarenotmatching")]
        //[Compare("newPwd", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Passwordsarenotmatching")]
        public string CPassword { get; set; }
    }
    public class ForgotPwdOTPResult
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string OTPKeyId { get; set; }
        public string OTPKeyVal { get; set; }
        public string Status { get; set; }
        public string TableName { get; set; }
        public string mUserId { get; set; }
    }


    public class BasicUser : SecurityParams
    {

        [Required]
        [RegularExpression(@"^(1|2|3)((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(\d{5})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        public string CivilId { set; get; }
        public string CivilIdExpirydate { get; set; }
        public string PassportNo { get; set; }
        public string PassportExpirydate { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Numeric values with 10 digits")]
        //[RegularExpression(@"^[7-9][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$", ErrorMessage = "")]
        public string MobileTelNumber { set; get; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validemail")]
        public string EmailId { set; get; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validpassword")]
        public string Pass { set; get; }
        [Required]
        [Compare("Pass", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Passwordsarenotmatching")]
        public string ConfirmPass { set; get; }
        [Required]
        [Range(typeof(bool), "false", "true", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        public bool Term_conditions { set; get; }

        //[Required]
        //[Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        //public string UserType { set; get; }

        public string OfficialAddress { get; set; }

    }


    public class OrganizationUser : BasicUser
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s\.\&]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AlphaNumeric")]
        public string OrgEngName { get; set; }
        [Required]
        [RegularExpression(@"^[\u0600-\u06FF ]+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "companynamearabic")]
        public string OrgAraName { get; set; }
        public string Description { get; set; }
        [Required]
        public string TradeLicNumber { get; set; }

        public string TradeLicenseExpiryDate { get; set; }

        [Required]
        public string POBoxNo { get; set; }
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryId { get; set; }
        [Required]
        public string BusiNo { get; set; }
        [Required]
        public string BusiFaxNo { get; set; }
        public string ResidenceNo { get; set; }
        public string WebPageAddress { get; set; }
        public bool isIndustrial { get; set; }
    }
    public class ClearingAgentUser : BasicUser
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AlphaNumeric")]
        public string FirstName { set; get; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AlphaNumeric")]
        public string LastName { set; get; }
        [RegularExpression(@"^[\u0600-\u06FF ]+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "companynamearabic")]
        public string FirstNameAra { get; set; }
        [RegularExpression(@"^[\u0600-\u06FF ]+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "companynamearabic")]
        public string LastNameAra { get; set; }

        [Required]
        public string BrokerLicenseNumber { get; set; }

        public string BrokerLicenseExpiryDate { get; set; }
        public string Nationality { get; set; }

    }
    public class AdditionalClearingAgentUser : ClearingAgentUser
    {
        public List<EService> AvailableEServices { get; set; }

        public string SelectedServices { get; set; }

    }
    public class UserProfile : SecurityParams
    {
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        public string UserType { set; get; }

        [Required]
        [RegularExpression(@"^(1|2|3)((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(\d{5})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        public string CivilId { set; get; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        public string CivilIdExpirydate { get; set; }
        [Required]
        public string PassportNo { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        public string PassportExpirydate { get; set; }

        public string Nationality { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{8})$", ErrorMessage = "Please Enter Numeric values with 8 digits")]
        //[RegularExpression(@"^[7-9][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$", ErrorMessage = "")]
        public string MobileTelNumber { set; get; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validemail")]
        public string EmailId { set; get; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validpassword")]
        public string Pass { set; get; }
        [Required]
        [Compare("Pass", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Passwordsarenotmatching")]
        public string ConfirmPass { set; get; }
        [Required]
        [Range(typeof(bool), "false", "true", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        public bool Term_conditions { set; get; }

        //[Required]
        //[Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        //public string UserType { set; get; }

        public string OfficialAddress { get; set; }





        [Required]
        [RegularExpression(@"^[a-zA-Z\s\.\&]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AlphaNumeric")]
        public string OrgEngName { get; set; }
        [Required]
        [RegularExpression(@"^[\u0600-\u06FF ]+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "companynamearabic")]
        public string OrgAraName { get; set; }
        public string Description { get; set; }
        [Required]
        public string TradeLicNumber { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        public string TradeLicenseExpiryDate { get; set; }

        //[Required]
        public string POBoxNo { get; set; }
        public string Address { get; set; }
        //[Required]
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryId { get; set; }
        [Required]
        public string BusiNo { get; set; }
        [Required]
        public string BusiFaxNo { get; set; }
        public string ResidenceNo { get; set; }
        public string WebPageAddress { get; set; }
        public bool isIndustrial { get; set; }






        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AlphaNumeric")]
        public string FirstName { set; get; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AlphaNumeric")]
        public string LastName { set; get; }
        [RegularExpression(@"^[\u0600-\u06FF ]+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "companynamearabic")]
        public string FirstNameAra { get; set; }
        [RegularExpression(@"^[\u0600-\u06FF ]+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "companynamearabic")]
        public string LastNameAra { get; set; }

        [Required]
        public string BrokerLicenseNumber { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SignUPTermeConditions")]
        public string BrokerLicenseExpiryDate { get; set; }




        public List<EService> AvailableEServices { get; set; }

        public string SelectedServices { get; set; }

        public string ParentID { get; set; }

        public bool SubUser { get; set; }
        public string Language { get; set; }

    }

    public class ServicesAndOrgManagementFortheUser
    {
        public int UserID { get; set; }
        public string ServiceID { get; set; }
        public string SubscriptionID { get; set; }
        public bool isLinked { get; set; }
        public int ParentUserID { get; set; }
        public string OrganizationID { get; set; }
        public string ActionType { get; set; }
    }
    public class UserSession
    {
        public string lang { get; set; }
        public int Userid { get; set; }
    }
    public class OrganizationDocument
    {
        public string Id { get; set; }//OrgId //OrgReqId
        public string OrgId { get; set; }
        public string OrgReqId { get; set; }
        public string OrgName { get; set; }

        public string Issuer { get; set; }
        public string LicenseType { get; set; }
        public string LicenseNumber { get; set; }
        public string Year { get; set; }
        public string IssuanceDate { get; set; }
        public string ExpiryDate { get; set; }
        public string DocumentName { get; set; }

        public string DocumentTypeCode { get; set; }

        public string ContentType { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public char IsUploaded { get; set; }

        public List<System.Web.Mvc.SelectListItem> DocumentTypeList { get; set; }
        public List<System.Web.Mvc.SelectListItem> LicenceTypeList { get; set; }
        public List<System.Web.Mvc.SelectListItem> IssuerList { get; set; }

        public List<OrganizationDocumentDetail> OrganizationDocuments { get; set; }

    }
    public class OrganizationDocumentDetail
    {
        //public string DocumentFor { get; set; }//company name or orgname
        public string OrganizationRequestDocumentId { get; set; }
        public string DocumentId { get; set; } //This will be scanuploddocid of orgreqdoc table and its primary key in doc table which is enough
        //public string DocumentReferenceId { get; set; }//OrgId //OrgReqId
        public string LicenseNumber { get; set; }
        public string DocumentTypeCode { get; set; }
        //public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string IssuanceDate { get; set; }
        public string ExpiryDate { get; set; }
        public string LicenseType { get; set; }
        //public string State { get; set; }
        //public bool isActive { get; set; }
        //public bool ToSync { get; set; }

        public string CreatedDate { get; set; }
        //public string CreatedBy { get; set; }
        //public string ModifiedDate { get; set; }
        //public string ModifiedBy { get; set; }

    }
    public class OrganizationDetail
    {
        public string Id { get; set; }
        public string OrgId { get; set; }
        public string OrgReqId { get; set; }
        public string OrgName { get; set; }
        //public string OrgEngName { get; set; }
        //public string OrgAraName { get; set; }
        public bool IsMainCompany { get; set; }
        //public List<SelectListItem> OrgEngName { get; set; }
        //public List<SelectListItem> OrgAraName { get; set; }
        public List<System.Web.Mvc.SelectListItem> Organizations { get; set; }

    }
}