using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CustomsVisit
    {
        public string RequestId { get; set; }
        public string RequestNumber { get; set; }


        public string ReservationNo { get; set; }

        public List<VisitorTypeData> VisitorTypeData { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "VisitorTypeRequired")]
        //[Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "VisitorTypeRequired")]
        [Required(ErrorMessage ="Please select a VisitorType")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please select a VisitorType")]
        public string VisitorType { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "VisitorNameRequired")]
        [Required(ErrorMessage = "Please enter the name")]
        public string VisitorName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        [RegularExpression(@"^(1|2|3)((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(\d{5})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        public string CivilId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        [RegularExpression(@"^([0-9]{8})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        public string MobileNum { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validemail")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "validemail")]
        public string Email { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DepartmentRequired")]
        //[Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DepartmentRequired")]
        [Required(ErrorMessage = "Please select a Department")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please select a Department")]
        public string Department { get; set; }
        public string DeptAddress { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "VistPurposeRequired")]
        //[Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "VistPurposeRequired")]
        [Required(ErrorMessage = "Please select a VisitPurpose")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please select a VisitPurpose")]
        public string VisitPurpose { get; set; }
        public string Guideline { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AppointmentDateRequired")]
        [Required(ErrorMessage = "Please select Appointment date")]
        public string AppointmentDate { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AppointmentTimeSlotRequired")]
        [Required(ErrorMessage = "Please select the time")]
        public string AppointmentTimeSlot { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequestNoteRequired")]
        [Required(ErrorMessage = "Please enter the request notes")]
        public string RequestNote { get; set; }

        public List<DepartmentDetails> DepartmentDetails { get; set; }
        public string DepartmentDetailsstring { get; set; }
        public string VisitPurposestring { get; set; }
        public string AppointmentTimeSlotsstring { get; set; }
        

        public string Status { get; set; }


    



        //Reserve
        //Close

    }
    public class VisitorTypeData
    {
        public int VisitorTypeId { get; set; }
        public string VisitorTypeName { get; set; }
    }

    public class DepartmentDetails
    {
        public int DepartmentId { get; set; }
        public int ScheduleId { get; set; }
        public string DepartmentNameEng { get; set; }
        public string DepartmentNameAra { get; set; }
        public string DepartmentName { get; set; }
        public string DeptAddressEng { get; set; }
        public string DeptAddressAra { get; set; }
        public string DeptAddress { get; set; }
        //public string VisitPurposeEng { get; set; }
        //public string VisitPurposeAra { get; set; }
        //public string VisitPurposeName { get; set; }
        //public string GuidelineEng { get; set; }
        //public string GuidelineAra { get; set; }
        //public string Guideline { get; set; }
        public string AppointmentStartDate { get; set; }
        public string AppointmentEndDate { get; set; }
        public string WorkingDays { get; set; }
        public List<AppointmentTimeSlot> AppointmentTimeSlots { get; set; }
        public List<VisitPurpose> VisitPurpose { get; set; }

    }
    public class AppointmentTimeSlot
    {
        public int ScheduleId { get; set; }

        public int DepartmentId { get; set; }

        public int VisitPurposeId { get; set; }

        public string AppountmentHour { get; set; }
        public string Capacity { get; set; }
    }
    public class VisitPurpose
    {
        public int VisitPurposeId { get; set; }
        public int DepartmentId { get; set; }

        public string VisitPurposeEng { get; set; }
        public string VisitPurposeAra { get; set; }
        public string VisitPurposeName { get; set; }
        public string GuidelineEng { get; set; }
        public string GuidelineAra { get; set; }
        public string Guideline { get; set; }
    }
}