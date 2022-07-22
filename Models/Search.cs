using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Search
    {
    }
    public class DeclarationSearch: SecurityParams
    {
        public string tempDeclNumber { get; set; }
        public string lang { get; set; }
    }
    public class Declaration : SecurityParams
    {
        public string Consignee { get; set; }
        public string DeclarationCCP { get; set; }
        public string DeclarationNumber { get; set; }
        public string DeclarationStatus { get; set; }
        public string DeclarationTemporaryNumber { get; set; }
        public string DeclarationType { get; set; }
        public string DeliveryOrderNumber { get; set; }
        public string HouseBillNumber { get; set; }
        public string PortofDischargeCountry { get; set; }
        public string PortofLoadingCountry { get; set; }
        public string TableName { get; set; }

    }
    public class HousebillSearch : SecurityParams
    {
        public string hbNumber { get; set; }
        public bool HBSearch { get; set; }// OPTION 1 IS TRUE OPTION 2 IS FALSE
        public string DONumber { get; set; }
        public string SecurityCode { get; set; }
    }
    public class HouseBills : SecurityParams
    {
        public string CarrierandShippingAgent { get; set; }
        public string Consignee { get; set; }
        public string ConsolidatedFlagStatus { get; set; }
        public string DONumber { get; set; }
        public string DeclarationNumber { get; set; }
        public string HousebillAuto { get; set; }
        public string HousebillNumber { get; set; }
        public string Quantity { get; set; }
        public string Status { get; set; }
        public string TableName { get; set; }
        public string TempDeclNumber { get; set; }
        public string Weight { get; set; }

    }
    //public class HSCode : SecurityParams
    //{
    //    public string ChaptersCode { get; set; }
    //    public string ChaptersNameAra { get; set; }
    //    public string ChaptersNameEng { get; set; }
    //    public string HSCode { get; set; }
    //    public string HSCodeDuty { get; set; }
    //    public string HSCodeId { get; set; }
    //    public string HSCodeNameAra { get; set; }
    //    public string HSCodeNameEng { get; set; }
    //    public string HSCodeProhibitionExport { get; set; }
    //    public string HSCodeProhibitionImport { get; set; }
    //    public string HSCodeRestrictionExport { get; set; }
    //    public string HSCodeRestrictionImport { get; set; }
    //    public string HSCodeShortDescription { get; set; }
    //    public string HSCodeUnit { get; set; }
    //    public string HeadingsCode { get; set; }
    //    public string HeadingsNameAra { get; set; }
    //    public string HeadingsNameEng { get; set; }
    //    public string SectionsCode { get; set; }
    //    public string SectionsNameAra { get; set; }
    //    public string SectionsNameEng { get; set; }
    //    public string SubHeadingsCode { get; set; }
    //    public string SubHeadingsNameAra { get; set; }
    //    public string SubHeadingsNameEng { get; set; }
    //    public string TableName { get; set; }
    //}
    public class HSCodeSearch : SecurityParams
    {
        public string data { get; set; }
        public string paramType { get; set; }
        public string lang { get; set; }

    }
    public class HSCodeSearchid : SecurityParams
    {
        public string hsCodeId { get; set; }
    }
    public class UserDetails : SecurityParams
    {
        public string AccountStatus { get; set; }
        [Required]
        public string CivilId { get; set; }
        public string EmailCStaus { get; set; }
        public string EmailId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string IsEmailVerified { get; set; }
        public string IsMobileVerified { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string TableName { get; set; }
    }
    public class paymentsearchinput : SecurityParams
    {
        public string lang { get; set; }
    }
}