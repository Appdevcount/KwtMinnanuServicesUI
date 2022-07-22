using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class EservicesRequests
    {
        public Int64 EserviceRequestId { get; set; }
        public String EserviceRequestNumber { get; set; }
        public Int64 RequesterUserId { get; set; }
        public Int64 ServiceId { get; set; }
        public String StateId { get; set; }
        public DateTime? DateCreated { get; set; }
        public String CreatedBy { get; set; }
        public String ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int OwnerOrgId { get; set; }
        public int OwnerLocId { get; set; }


        public List<EServiceRequestsDetails> RequestsDetails { get; set; }

    }


    public class EServiceRequestsDetails
    {
        public Int64 EserviceRequestDetailsId { get; set; }
        public Int64 EserviceRequestId { get; set; }
        public Int64 RequestForUserType { get; set; }
        public String RequestServicesId { get; set; }
        public int OrganizationId { get; set; }

        public int RequesterUserType { get; set; }
        public String RequesterLicenseNumber { get; set; }
        public String RequesterMobileNumber { get; set; }
        public String RequesterArabicName { get; set; }
        public String RequesterEnglishName { get; set; }

        public String ArabicFirstName { get; set; }
        public String ArabicSecondName { get; set; }
        public String ArabicThirdName { get; set; }
        public String ArabicLastName { get; set; }
        public String EnglishFirstName { get; set; }
        public String EnglishSecondName { get; set; }
        public String EnglishThirdName { get; set; }
        public String EnglishLastName { get; set; }
        public int Nationality { get; set; }
        public String CivilID { get; set; }
        public DateTime? CivilIDExpiryDate { get; set; }
        public String MobileNumber { get; set; }
        public String PassportNo { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String LicenseNumber { get; set; }
        public DateTime? LicenseNumberExpiryDate { get; set; }
        public String Remarks { get; set; }
        public String RejectionRemarks { get; set; }
        public Boolean RequestForVisit { get; set; }
        public String RequestForVisitRemarks { get; set; }
        public Int64 ExamAddmissionId { get; set; }
        public Int64 ExamDetailsId { get; set; }


        public String Gender { get; set; }

        public String status { get; set; }
        public String StateId { get; set; }
        public DateTime? DateCreated { get; set; }
        public String CreatedBy { get; set; }
        public String ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int OwnerOrgId { get; set; }
        public int OwnerLocId { get; set; }


        public String CivilIDExpiryDateFormatted { get; set; }
        public String PassportExpiryDateFormatted { get; set; }


    }



    public class examRequestViewModel : EservicesRequests
    {

        public String Referenceprofile { get; set; }
        public String lang { get; set; }
        public Boolean EXAMREQ { get; set; }
        public int Serviceid { get; set; }

        public String BrokerType { get; set; }



        public List<SelectListItem> ddlDocumentTypesitems { get; set; }


        public List<BrFileResult> ListOfUploadedFiles { get; set; }
        public String SelectedFileId { get; set; }
        public String docsid { get; set; }

        public String UploadDiv { get; set; }
        public String SubmitRequest { get; set; }

        public String organizationid { get; set; }

        // 27-FEB
        public Int64 requestForMobileUserId { get; set; }
        public int mobileUserId { get; set; }


        public String requesterName { get; set; }
        public String requesterLicenceNo { get; set; }
        public String requesterMobileNo { get; set; }


        public String brokerTypeString { get; set; }
        public String brokerTy { get; set; }








    }


    public class RequestExists
    {
        public String RequestForUserType { get; set; }
        public String RequesterUserId { get; set; }
        public String ServiceId { get; set; }
    }


    public class RequestExistsByCivilId
    {

        public String CivilId { get; set; }
        public String ServiceId { get; set; }
        public String StateId { get; set; }

    }

    public class ExamCandidateInfo
    {
        public String EserviceRequestId { get; set; }
        public String stateId { get; set; }

    }


    #region Siraj G
    public class EserviceRequest
    {
        public string ESERVICEREQUESTNUMBER { get; set; }
        public int RequesterUserId { get; set; }
    }
    public class RequestList
    {
        public int EServiceRequestID { get; set; }
        public string EServiceRequestNumber { get; set; }
        public int RequestServicesId { get; set; }
        public int? RequesterUserID { get; set; }
        public int? RequestForUserID { get; set; }
        public string Status { get; set; }

        public String DateCreated { get; set; }
        public string StatusArabic { get; set; }
        public string StateId { get; set; }
        //public DateTime? DateCreated { get; set; }

        public string RejectionRemarks { get; set; }//added newly
        public string RequestForVisitRemarks { get; set; }//added newly

        //==Org Registration Request
        public string IsMainCompany { get; set; }
        public string OrganizationRequestId { get; set; }
        public string Name { get; set; }
        //==Org Registration Request

     //==Added For Print by azhar 

        public string LicenseNumber { get; set; }
        public string BrokerType { get; set; }
        public string BrokerName { get; set; }
        public string GenBrokerName { get; set; }


    }
    public class GroupedRequests
    {
        public List<RequestList> ExamRequests { get; set; }
        public List<RequestList> BrokerRenewalRequests { get; set; }
        public List<RequestList> OrganizationRegistrationRequests { get; set; }
        public List<RequestList> MultiSubscription { get; set; }

        public List<RequestList> BrokerDeactivateRequests { get; set; }

        public List<RequestList> BrokerCancelRequests { get; set; }

        public List<RequestList> BrokerTransferRequests { get; set; }


        public List<RequestList> BrokerIssuanceRequests { get; set; }



        public List<RequestList> BrokerToWhomItConcern { get; set; }

        public List<RequestList> BrokerPrintLostIdCard { get; set; }


        public List<RequestList> BrsPrintingCancelLicense { get; set; }
        public List<RequestList> BrsPrintingIssueLicense { get; set; }
        public List<RequestList> BrsPrintingChangeLicenseAddress { get; set; }
        public List<RequestList> BrsPrintingChangeLicenseActivity { get; set; }
        public List<RequestList> BrsPrintingReleaseBankGuarantee { get; set; }
        public List<RequestList> BrsPrintingGoodBehave { get; set; }
        public List<RequestList> BrsPrintingRenewLicense { get; set; }
        public List<RequestList> BrsPrintingChangeJobTitleRenewResidency { get; set; }
        public List<RequestList> BrsPrintingChangeJobTitleTransferResidency { get; set; }
        public List<RequestList> BrsPrintingRenewResidency { get; set; }
        public List<RequestList> BrsPrintingTransferResidency { get; set; }
        public List<RequestList> BrsPrintingChangeJobTitle { get; set; }
        public List<RequestList> BrsPrintingChangeJobTitleCivil { get; set; }
        public List<RequestList> BrsPrintingDeActivateLicenseDeath { get; set; }


        public List<declgroup> InspectionAppointments { get; set; }
    }
    public class RequestDetailMain
    {
        public int EServiceRequestID { get; set; }
        public string EServiceRequestNumber { get; set; }
        //public DateTime? DateCreated { get; set; }
        public String DateCreated { get; set; }
        public string ArabicFirstName { get; set; }
        public string ArabicSecondName { get; set; }
        public string ArabicThirdName { get; set; }
        public string ArabicLastName { get; set; }
        public string EnglishFirstName { get; set; }
        public string EnglishSecondName { get; set; }
        public string EnglishThirdName { get; set; }
        public string EnglishLastName { get; set; }
        public string Nationality { get; set; }
        public string CivilID { get; set; }
        public string CivilIDExpiryDate { get; set; }
        public string MobileNumber { get; set; }
        public string PassportNo { get; set; }
        public string PassportExpiryDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseNumberExpiryDate { get; set; }
        public string CreatedBy { get; set; }

        public string BrokerType { get; set; }
        public string BrokerName { get; set; }
        public string RequestedOrganizations { get; set; }

        public string ListOfRequestedOrg { get; set; }
        public String RejectionRemarks { get; set; }


        public List<RequestDetail> ReqDetail { get; set; }

    }

    public class RequestDetail
    {
        //public int EServiceRequestID { get; set; }
        //public int EServiceRequestDetailsID { get; set; }
        //public string EServiceRequestNumber { get; set; }
        //public string ServiceNameEng { get; set; }
        //public string ServiceNameAra { get; set; }
        //public int? RequesterUserID { get; set; }
        //public int? RequestForUserID { get; set; }
        //public DateTime? DateCreated { get; set; }
        //public DateTime? DateModified { get; set; }
        //public string Status { get; set; }


        //public string RequestForUserType { get; set; }
        //public string RequestServicesId { get; set; }
        //public string OrganizationId { get; set; }
        //public string RequesterLicenseNumber { get; set; }
        //public string RequesterArabicName { get; set; }
        //public string RequesterEnglishName { get; set; }
        //public string ArabicFirstName { get; set; }
        //public string ArabicSecondName { get; set; }
        //public string ArabicThirdName { get; set; }
        //public string ArabicLastName { get; set; }
        //public string EnglishFirstName { get; set; }
        //public string EnglishSecondName { get; set; }
        //public string EnglishThirdName { get; set; }
        //public string EnglishLastName { get; set; }
        //public string Nationality { get; set; }
        //public string CivilID { get; set; }
        //public string CivilIDExpiryDate { get; set; }
        //public string MobileNumber { get; set; }
        //public string PassportNo { get; set; }
        //public string PassportExpiryDate { get; set; }
        //public string Address { get; set; }
        //public string Email { get; set; }
        //public string LicenseNumber { get; set; }
        //public string LicenseNumberExpiryDate { get; set; }
        //public string Remarks { get; set; }
        //public string RejectionRemarks { get; set; }
        //public string RequestForVisit { get; set; }
        //public string RequestForVisitRemarks { get; set; }
        //public string ExamAddmissionId { get; set; }
        //public string ExamDetailsId { get; set; }
        //public string CreatedBy { get; set; }
        //public string OwnerOrgId { get; set; }
        //public string OwnerLocId { get; set; }
        //public string RequestForUserId { get; set; }
        //public string RequestForName { get; set; }
        //public string RequestForEmail { get; set; }


        public int EServiceRequestDetailsID { get; set; }
        public string ServiceNameEng { get; set; }
        public string ServiceNameAra { get; set; }
        public int? RequesterUserID { get; set; }
        public int? RequestForUserID { get; set; }
        public DateTime? DateModified { get; set; }
        public string Status { get; set; }

        public String StatusArabic { get; set; }

        public String RequestForUserType { get; set; }
        public String RequestServicesId { get; set; }
        public String OrganizationId { get; set; }
        public String RequesterLicenseNumber { get; set; }
        public String RequesterArabicName { get; set; }
        public String RequesterEnglishName { get; set; }
        public String Remarks { get; set; }
        public String RejectionRemarks { get; set; }
        public String RequestForVisit { get; set; }
        public String RequestForVisitRemarks { get; set; }
        public String ExamAddmissionId { get; set; }
        public String ExamDetailsId { get; set; }
        public String OwnerOrgId { get; set; }
        public String OwnerLocId { get; set; }
        public String RequestForUserId { get; set; }
        public String RequestForName { get; set; }
        public String RequestForEmail { get; set; }

        public String EserviceRequestStatusArabic { get; set; }
        public String EserviceRequestStatusEnglish { get; set; }

    }

    public class ServiceSubscription
    {
        public String SelectedFileId { get; set; }
        public String UnSelectedFileId { get; set; }
        public List<AvailableEServices> AvailableEServices { get; set; }
    }

    public class AvailableEServices
    {
        public String State { get; set; }
        public String ServiceName { get; set; }
        public int ServiceId { get; set; }

        public string SubscriptionId { get; set; }
        public String ServiceNameEng { get; set; }
        public String ServiceNameAra { get; set; }
        public String LegalEntityType { get; set; }

    }
    #endregion Siraj G





}