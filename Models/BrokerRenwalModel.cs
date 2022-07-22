﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace WebApplication1.Models
{
    public class BrokerUpdateModel : PaymentsModel
    {
        public string Userid { get; set; }
        public string BrokerType { get; set; }
        public string requestNo { get; set; }
        public string ParentBrokerName { get; set; }
        public string BrokerArabicName { get; set; }
        public string BrokerEnglishName { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        public string CivilIdNo { get; set; }

        public string TempCivilIdExpirydate { get; set; }

        public string TempPassportExpirydate { get; set; }

        public string TempTradeLicenseExpiryDate { get; set; }
        public int Organizationid { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        [checkdaterange]
        
        public string CivilIdExpirydate { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "passportNoRequired")]
        public string passportNo { get; set; }

        public string TemppassportNo { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        [checkdaterange]
        public string PassportExpirydate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        //[checkdaterange]
        public string TradeLicenseExpiryDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        [RegularExpression(@"^([0-9]{8})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "emailRequired")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "emailRequired")]
        public string MailAddress { get; set; }
        public string officialAddress { get; set; }
        public string stateid { get; set; }
        public string RequestNumber { get; set; }
        public string Eservicerequestid { get; set; }
        public string tokenId { get; set; }
        public string ownerlocid { get; set; }
        public string ownerorgid { get; set; }
        public string lang { get; set; }
        public string sessionid { get; set; }
        public string SubmitRequest { get; set; }
        public string UploadDiv { get; set; }
        public string Referenceprofile { get; set; }
        public List<SelectListItem> ddlDocumentTypesitems { get; set; }

        [checkAllbrokerlicense(ErrorMessageResourceName = "GeneralBrokerLicenseRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string BrokerLicenseNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ValidLicenseNumberRequired")]
        //[checkAllbrokerlicense(ErrorMessageResourceName = "ValidLicenseNumberRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string LicenseNumber { get; set; }


        public string TypeOfAction { get; set; }
        public List<BrFileResult> ListOfUploadedFiles { get; set; }
        public List<AssoicatedOrgList> ListofAssocaitedORg { get; set; }
        public string SelectedFileId { get; set; }
        public string docsid { get; set; }
        public int Serviceid { get; set; }

        public string SelectedOrgidForIssuance { get; set; }
        // 27-FEB
        public Int64 requestForMobileUserId { get; set; }
        public int mobileUserId { get; set; }

        public string Requestorserviceid { get; set; }
        public string Nationality { get; set; }
        public string RejectionReason { get; set; }


        public string eServiceUserEmailId { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AgreeInfoCorrectnessRequired")]
        [Range(typeof(bool), "false", "true", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AgreeInfoCorrectnessRequired")]
        public bool AgreeInfoCorrectness { get; set; }//added newly OrgSvcNew Chnages   -Siraj [This property is specific to Organization service Document Upload]


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "fromBusinessValidate")]
        public String fromBusiness { get; set; }

        public String CommercialLicenseNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ChangeJobTitleFromValidate")]
        public String ChangeJobTitleFrom { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ChangeJobTitleToValidate")]
        public String ChangeJobTitleTo { get; set; }

        public String BankGuaranteeNo { get; set; }
        public String BankName { get; set; }
        public String BankGuaranteeIssuanceDate { get; set; }
        public String BankGuaranteeExpiryDate { get; set; }
        public String BankGuaranteeStatus { get; set; }


        

    }


    public class BrokerRenewalModel
    {
        public string tokenid { get; set; }

        public string Userid { get; set; }
        public string paidfor { get; set; }
        public string paidby { get; set; }

        public string PaidByType { get; set; }

        public string urltoredirectforpayments { get; set; }
        public string redirecturl { get; set; }
        public string PaymentTypeId { get; set; }
       // added byazhar on 04072019
        public string Orgid { get; set; }
        public string Requestorserviceid { get; set; }

    }

    public class ddlDocumentTypes
    {
        public int Value { get; set; }

        public int selectedValue { get; set; }


        public string Text { get; set; }

    }
    public class PaymentsModel : BrFileResult
    {
        public string paymentMode { get; set; }
        public string PaymentTypeId { get; set; }

        public string paidfor { get; set; }
        public string paidby { get; set; }

        public string PaidByType { get; set; }

        public string urltoredirectforpayments { get; set; }
        public string redirecturl { get; set; }
    }

    public class BrFileResult
    {
        //  public string FileName { get; set; }

        public string NewFileName { get; set; }
        public string OrgReqId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public char IsUploaded { get; set; }
        public string Createddate { get; set; }
        public string CreatedBy { get; set; }
}

 public class AssoicatedOrgList
    {
        //  public string FileName { get; set; }

        public string AssoicatedOrgName { get; set; }
        public string AssoicatedOrgId { get; set; }
        public string isselected { get; set; }
        public string MainOrgId { get; set; }
    }

    #region BrokerEntityServiceRequestModel

    public class BrokerServiceRequestModel
    {
        public string userid { get; set; }
        public string legalentity { get; set; }
        public string SelectedFileId { get; set; }
        public string UnSelectedFileId { get; set; }
        public string requestNo { get; set; }
        public string Status { get; set; }
        public List<EserviceList> ListOfAvailableServices { get; set; }
        public string docsid { get; set; }
        public string SelectedServiceids { get; set; }
        public string UnSelectedServiceids { get; set; }
        public bool? CheckAvailableServicesforRequest { get; set; }
        public int MobileUserid { get; set; }
        public int RequestedForMobileUserid { get; set; }

    }
    public class EserviceList
    {
        public string AvailableEserviceId { get; set; }
        public string AvailableEservicename { get; set; }
        public string StatusOfRequestedServices { get; set; }
        public string EservicerequestNoForServiceId { get; set; }

    }
    public class RequestedService
    {
        public string AvailableEserviceId { get; set; }
        public string AvailableEservicename { get; set; }
        public string StatusOfRequestedServices { get; set; }

    }


    #endregion


    // logging of UserActivities
    public class LogUserActivity
    {
        public string LoginUserid { get; set; }
        public string LoginTime { get; set; }
        public string sessionId { get; set; }
        public string Serviceid { get; set; }
        public string IPAddress { get; set; }
        public string McUserOrgId { get; set; }
        public string McUserName { get; set; }
        public string legalentity { get; set; }
        public bool ClearingAgentservice { get; set; }
        public bool OrganizationService { get; set; }
        public string ActivityPerformed { get; set; }
        public string SignInSignOut { get; set; }
        public string LogOutTime { get; set; }
        public string OtherAdditionalInfo { get; set; }
    }
}



//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//namespace WebApplication1.Models
//{
//    public class BrokerUpdateModel:PaymentsModel
//    {
//        public string Userid { get; set; }
//        public string BrokerType { get; set; }
//        public string requestNo { get; set; }
//        public string ParentBrokerName { get; set; }
//        public string BrokerArabicName { get; set; }
//        public string BrokerEnglishName { get; set; }
//        public string CivilIdNo { get; set; }
//        public int Organizationid { get; set; }
//        public string CivilIdExpirydate { get; set; }
//        public string passportNo { get; set; }
//        public string PassportExpirydate { get; set; }
//        public string TradeLicenseExpiryDate { get; set; }
//        public string MobileNumber { get; set; }
//        public string MailAddress { get; set; }
//        public string officialAddress { get; set; }
//        public string stateid { get; set; }
//        public string RequestNumber { get; set; }
//        public string Eservicerequestid { get; set; }
//        public string tokenId { get; set; }
//        public string ownerlocid { get; set; }
//        public string ownerorgid { get; set; }
//        public string lang { get; set; }
//        public string sessionid { get; set; }
//        public string SubmitRequest { get; set; }
//        public string UploadDiv { get; set; }
//        public string Referenceprofile { get; set; }
//        public List<SelectListItem> ddlDocumentTypesitems { get; set; }

//        public List<BrFileResult> ListOfUploadedFiles { get; set; }

//        public string docsid { get; set; }

//    }


//    public class BrokerRenewalModel
//    {
//        public string tokenid { get; set; }

//        public string Userid { get; set; }
//        public string paidfor { get; set; }
//        public string paidby { get; set; }

//        public string PaidByType { get; set; }

//        public string urltoredirectforpayments { get; set; }
//        public string redirecturl { get; set; }
//        public string PaymentTypeId { get; set; }


//    }

//    public class ddlDocumentTypes
//    {
//        public int Value { get; set; }

//        public int selectedValue { get; set; }


//        public string Text { get; set; }

//    }
//    public class PaymentsModel: BrFileResult
//    {
//            public string paymentMode { get; set; }
//            public string PaymentTypeId { get; set; }

//            public string paidfor { get; set; }
//            public string paidby { get; set; }

//            public string PaidByType { get; set; }

//            public string urltoredirectforpayments { get; set; }
//            public string redirecturl { get; set; }
//        }

//    public class BrFileResult
//    {
//        //  public string FileName { get; set; }

//        public string NewFileName { get; set; }
//        public string OrgReqId { get; set; }
//        public string DocumentName { get; set; }
//        public string DocumentId { get; set; }
//        public string DocumentType { get; set; }
//        public string Name { get; set; }
//        public string ContentType { get; set; }
//        public string FilePath { get; set; }
//        public string FileSize { get; set; }
//        public char IsUploaded { get; set; }
//        public string Createddate { get; set; }
//        public string CreatedBy { get; set; }
//    }

//    #region BrokerEntityServiceRequestModel

//   public class BrokerServiceRequestModel
//    {
//        public string userid { get; set; }
//        public string legalentity { get; set; }
//        public string SelectedFileId { get; set; }
//        public string requestNo { get; set; }
//        public string Status { get; set; }
//        public List<EserviceList> ListOfAvailableServices { get; set; }
//        public string docsid { get; set; }
//    }
//    public class EserviceList
//    {
//        public string AvailableEserviceId { get; set; }
//        public string AvailableEservicename { get; set; }

//    }
//    #endregion


//}