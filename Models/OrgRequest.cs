using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrgGetBasicResult : SecurityParams
    {
        //**RequiredField
        public int OrganizationId { get; set; }
        public int OrganizationRequestId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName =  "OrgEngNameRequired")]
        //[RegularExpression(@"^[a-zA-Z\s\.\&\-\,\'\` ]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "OrgEngNameRequired")]// "AlphaNumeric")]
        public string OrgEngName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName =  "OrgAraNameRequired")]
        [RegularExpression(@"^[\u0600-\u06FF\-\,\'\` ]+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "OrgAraNameRequired")]// "companynamearabic")]
        public string OrgAraName { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName =  "TradeLicenseRequired")]
        public string TradeLicNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        [RegularExpression(@"^(1|2|3)((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(\d{5})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        public string CivilId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName =  "AuthorizzedPersonName")]
        //[RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AlphaNumeric")]
        [RegularExpression(@"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z\s]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_\s]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersAndSpecialCharactersNotAllowed")]
        public string AuthPerson { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PostBoxNumberRequired")]
        public string POBoxNo { get; set; }
        public string Address { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CityRequired")]
        public string City { get; set; }
        public string State { get; set; }

        public string CityBiling { get; set; }
        public string StateBiling { get; set; }

        public string PostalCode { get; set; }
        public string CountryId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName =  "BusinessNumberRequired")]
        //[RegularExpression("^[0-9]$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersOnly")]//enabled siraj
        public string BusiNo { get; set; }

      //  [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "BusinessFaxNumRequired")]
       
        //commented as per request shan 
        //[RegularExpression("^[0-9]$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersOnly")]//enabled siraj
        public string BusiFaxNo { get; set; }
        //[RegularExpression("^[0-9]$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersOnly")]//enabled siraj
        public string MobileNo { get; set; }
        //[RegularExpression("^[0-9]$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersOnly")]//enabled siraj
        public string ResidenceNo { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EnterMobileEmail1")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validemail")]
        public string EmailId { get; set; }
        public string WebPageAddress { get; set; }
        public bool isIndustrial { get; set; }
        public string Editable { get; set; }
        public string RejectionRemarks { get; set; }
        public string RequestType { get; set; }
        public string TableName { get; set; }
 
        public bool IsmainCompany { get; set; }
        public bool IsbranchCompany { get; set; }
        public bool IsFirstRequest { get; set; }

        public string CompanyType { get; set; }
        public string RequestNumber { get; set; }//This field will be used to just fetch the OrganizationRequestNumber if the OrgRequestId is not zero
                                                 //public string mUserId { get; set; }
                                                 //public string Operation { get; set; }


        public string Block { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
        public string Street { get; set; }//added newly OrgSvcNew Chnages 02-20-2019  -Siraj
    }
    public class OrgReq1Result
    {
        public string OrganizationId { get; set; }
        public string OrganizationRequestId { get; set; }
        public string RequestNumber { get; set; }
        public string RequestType { get; set; }
        public string Status { get; set; }
        public string TableName { get; set; }
    }
    public class OrgRequestlist 
    {
        public Data Data { get; set; }
    }
    public class Data
    {
        public List<OrgGetBasicResult> OrgGetBasicResult { get; set; }
        public List<OrgGetImportLicenseResult> OrgGetImportLicenseResult { get; set; }
        public List<OrgGetCommercialLicenseResult> OrgGetCommercialLicenseResult { get; set; }
        public List<OrgGetIndustrialResult> OrgGetIndustrialResult { get; set; }
        public List<OrgGetDocumentsResult> OrgGetDocumentsResult { get; set; }
        public List<ListOrgRequests> ListOrgRequests { get; set; }
        public List<EPaymentRequestInfo> EPaymentRequestInfo { get; set; }
        public List<NotificationsList> NotificationsList { get; set; }
        public List<EPaymentRequestDetails> EPaymentRequestDetails { get; set; }
        public List<OrgAuthorizedSignatory> OrgAuthorizedSignatories { get; set; }
    }
    public class NotificationsList : SecurityParams
    {
        public string DateCreated { get; set; }
        public string NotificationType { get; set; }
        public string ReadStatus { get; set; }
        public string ReferenceId { get; set; }
        public string ReffId { get; set; }
        public string ReffIdencry { get; set; }
        public string ReffType { get; set; }
        public string TableName { get; set; }
        public string msg { get; set; }
    }
    public class ListOrgRequests : SecurityParams
    {
        public string Name { get; set; }
        public string OrganizationRequestId { get; set; }
        public string RequestNumber { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string TableName { get; set; }
    }
        public class OrgGetIndustrialResult : SecurityParams
    {
        public string OrgAraName { get; set; }
        public int OrganizationId1 { get; set; }
        public int OrganizationId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "IndustrialLicenseNumberRequired")]
        [RegularExpression(@"^([0-9]{3,20})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Licenselessthandigits")]
        public string IndustrialLicenseNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "IssueDateRequired")]
        [RegularExpression(@"^\d{1,2}/\d{1,2}/\d{4}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        public string IssueDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ExpiryDateisRequired")]
        [RegularExpression(@"^\d{1,2}/\d{1,2}/\d{4}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        public string ExpiryDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "IndustrialRegistrationNumberRequired")]
        //[RegularExpression("^[0-9]$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersOnly")]//enabled siraj
        public string IndustrialRegistrationNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName =  "IssuancedateofIndustrialRegistrationNumber")]
        [RegularExpression(@"^\d{1,2}/\d{1,2}/\d{4}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        public string IssuanceDate { get; set; }
        public string TableName { get; set; }
        public int OrganizationRequestId { get; set; }
        
    }

    public class OrgGetCommercialLicenseResult : SecurityParams
    {
        public string OrgAraName { get; set; }
        public int OrganizationId1 { get; set; }
        public int OrganizationId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName =  "CommercialLicensetypeRequired")]
        public string CommLicType { get; set; }
        public string CommLicSubType { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CommercialLicensenumberRequired")]
     //   [RegularExpression(@"^([0-9]{3,5})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Licenselessthandigits")]
        public string CommLicNo { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CommercialLicenseissuedyearRequired")]
        //[RegularExpression("^[0-9]$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersOnly")]//enabled siraj
        public string Year { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "IssueDateRequired")]
        [RegularExpression(@"^\d{1,2}/\d{1,2}/\d{4}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        public string IssueDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ExpiryDateisRequired")]
        [RegularExpression(@"^\d{1,2}/\d{1,2}/\d{4}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        public string ExpiryDate { get; set; }
        public string TableName { get; set; }
        public int OrganizationRequestId { get; set; }

    }
    public class CommLicSubTypeslist
    {
        public List<CommLicSubTypes> CommLicSubTypes { get; set; }
    }
        public class CommLicSubTypes
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
        public int TypeId { get; set; }
    }
    public class OrgAuthorizedSignatory : SecurityParams //added newly --OrgAuthorizedSignatories
    {
        public string OrgAraName { get; set; }
        public Int64? OrgAuthorizedSignatoryId { get; set; }
        public Int64? OrganizationRequestId { get; set; }
        public Int64? OrganizationId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AuthorizzedPersonName")]
        [RegularExpression(@"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z\s]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_\s]*$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NumbersAndSpecialCharactersNotAllowed")]
        public string AuthPerson { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        [RegularExpression(@"^(1|2|3)((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(\d{5})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        public string CivilId { get; set; }
        public bool NewPerson { get; set; }
    }
    public class OrgGetImportLicenseResult : SecurityParams
    {
        public string OrgAraName { get; set; }
        public int OrganizationId1 { get; set; }
        public int OrganizationId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ImporterLicensetypeRequired")]
        public string ImpLicType { get; set; }
        public string Year { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName =  "ImporterLicensenumberRequired")]
        [RegularExpression(@"^([0-9]{3,10})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Licenselessthandigits")]
        public string ImpLicNo { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "IssueDateRequired")]
        [RegularExpression(@"^\d{1,4}/\d{1,4}/\d{4}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        public string IssueDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ExpiryDateisRequired")]
        [RegularExpression(@"^\d{1,2}/\d{1,2}/\d{4}$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateFormat")]
        public string ExpiryDate { get; set; }
        public string TableName { get; set; }
        public int OrganizationRequestId { get; set; }
        public Int64 OrgImporterLicenseId { get; set; }//added newly
    }
    public class DocTypes
    {
        public string Name { get; set; }
        public string TypeId { get; set; }
        public string Code { get; set; }
        public string TableName { get; set; }
    }
    public class DocTypeslist
    {
        public List<DocTypes> DocTypes { get; set; }
    }

    public class OrgGetDocumentsResult
    {
        public string OrgAraName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeCode { get; set; }
        public string FileNewName { get; set; }
        public string OrganizationRequestDocumentId { get; set; }
        public int OrganizationRequestId { get; set; }
        public string TableName { get; set; }
        public string UploadStatus { get; set; }
        public string Createddate { get; set; }
    }
    public class FileUploadResult
    {
        public string OrgAraName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeCode { get; set; }
        public string FileNewName { get; set; }
        public string OrganizationRequestDocumentId { get; set; }
        public string OrganizationRequestId { get; set; }
        public string TableName { get; set; }
        public string UploadStatus { get; set; }
    }
    public class paymentprint:SecurityParams
    {
        public string OPTokenId { get; set; }
        public string PaymentStatus { get; set; }
    }
        public class EPaymentRequestInfo
    {
        public string AmountField { get; set; }
        public string Broker { get; set; }
        public string Certificates { get; set; }
        public string ConsigeeName { get; set; }
        public string CustomsAccount { get; set; }
        public string CustomsDuty { get; set; }
        public string DeliveryOrderNo { get; set; }
        public string Guarantees { get; set; }
        public string HandlingCharges { get; set; }
        public string OLPaymentId { get; set; }
        public string OPTokenId { get; set; }
        public string OnlineReceiptDatetime { get; set; }
        public string OnlineReceiptNo { get; set; }
        public string Others { get; set; }
        public string PaidBy { get; set; }
        public string PayAmount { get; set; }
        public string PayFor { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentType { get; set; }
        public string Penalties { get; set; }
        public string Printing { get; set; }
        public string RequestNo { get; set; }
        public string STATUS { get; set; }
        public string Storage { get; set; }
        public string TableName { get; set; }
    }
    public class EPaymentRequestDetails : SecurityParams
    {
        public string Broker { get; set; }
        public string ConsigeeName { get; set; }
        public string DateCreated { get; set; }
        public string DeliveryOrderNo { get; set; }
        public string PayAmount { get; set; }
        public string PayFor { get; set; }
        public string RequstNo { get; set; }
        public string RequstNoenyp { get; set; }
        public string Status { get; set; }
        public string TableName { get; set; }
        public string rownum { get; set; }
    }
    public class ResponseData
    {
        public string data { get; set; }
    }
}