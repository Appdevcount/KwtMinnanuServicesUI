using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class InspectionAppointment
    {

        public string AppointmentId { get; set; }
        //[Required(ErrorMessage ="DO Number is required")]
        public string DONumber { get; set; }
        //[Required(ErrorMessage = "Security Code is required")]
        public string SecurityCode { get; set; }
        //public string PortID { get; set; }
        public string PortName { get; set; }
        // added by azhar 
        public string VehiclePlateNumber { get; set; }

        //[Required(ErrorMessage = "Atleast One vehicle should be while creating inspection schedule")]
        public string SelectedVehicleList { get; set; }

        public List<VehicleList> VehicleFullList { get; set; }
        public List<VehicleList> SelectedVehicleDetails { get; set; }

        //[Required(ErrorMessage = "Inspection Date is required")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "InspectionDateRequired")]
        public string InspectionDate { get; set; }
        //[Required]
        public List<InspectionRounds> InspectionRounds { get; set; }
        //[Required(ErrorMessage = "InspectionRound is required")]
        //[Range(1, Int64.MaxValue, ErrorMessage = "InspectionRound is required")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "InspectionRoundRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "InspectionRoundRequired")]
        public string InspectionRound { get; set; }


        //==============
        //public string RequestId { get; set; }
        public string RequestNumber { get; set; }
        //public string Requestertype { get; set; }
        public string Requester { get; set; }
        public string DeclarationId { get; set; }
        //public string DONumber { get; set; }
        //public List<VehicleList> SelectedVehicleDetails { get; set; }
        //public string InspectionDate { get; set; }
        //public string RoundId { get; set; }
        public string RoundName { get; set; }//
        public string Status { get; set; }
        public bool UpdateRequest { get; set; }
        public bool EditableRequest { get; set; }

        //===================
        public string UserId { get; set; }
        public string OrgId { get; set; }

        //[Required(ErrorMessage = "Port Id is required")]
        //[Range(1, Int64.MaxValue, ErrorMessage = "Port Id is required")]
        public string PortId { get; set; }

        public string RequesterType { get; set; }
        public string RequestSubmissionDateTime { get; set; }
        public string DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string OwnerOrgId { get; set; }
        public string OwnerLocId { get; set; }

        //==========

        public string DeclarationType { get; set; }
        //[Required(ErrorMessage = "Inspection Zone is required")]
        //[Range(1, Int64.MaxValue, ErrorMessage = "Inspection Zone is required")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "InspectionZonerequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "InspectionZonerequired")]
        public string InspectionZone { get; set; }
        //[Required(ErrorMessage = "Inspection Terminal is required")]
        //[Range(1, Int64.MaxValue, ErrorMessage = "Inspection Terminal is required")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "InspectionTerminalrequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "InspectionTerminalrequired")]
        public string InspectionTerminal { get; set; }


        public string TempDeclarationId { get; set; }


        public string StatusNumber { get; set; }

        public List<InspectionZone> InsZone { get; set; }
        public List<InspectionTerminal> InsTerminal { get; set; }
        public List<InspectionRounds> InsRound { get; set; }


        public string DeclarationIduid { get; set; }


        public string BayanType { get; set; }
        public string BayanPort { get; set; }
        public string BayanYear { get; set; }


        public String DriverNameToPrint { get; set; }
        public String DriverMobileNumberToPrint { get; set; }
        public String ContainerNumberToPrint { get; set; }
        public String WeightToPrint { get; set; }
        public String DeclarationNumberToPrint { get; set; }
    }

    public class InspectionRounds
    {
        public string Port { get; set; }
        public string PortName { get; set; }
        public string RoundId { get; set; }
        public string RoundName { get; set; }
        public string Timing { get; set; }
        public int Capacity { get; set; }
        public bool Active { get; set; }
        public bool Availability { get; set; }

        public string DeclType { get; set; }
        public string RecieverDeliver { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Selected { get; set; }
    }
    public class InspectionZone
    {
        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
    }
    public class InspectionTerminal
    {
        public string TerminalId { get; set; }
        public string TerminalName { get; set; }
    }

    public class VehicleList
    {
        public string VehicleID { get; set; }
        public string VehiclePlateNumber { get; set; }
        public string VehicleType { get; set; }
        public string ContainerNumber { get; set; }
        public string DriverName { get; set; }
        public string PortId { get; set; }
        public string PortName { get; set; }
        public string DONumber { get; set; }
        public string DeclarationId { get; set; }



        public string DeclarationType { get; set; }

        public string Weight { get; set; }
        public string MobileNumber { get; set; }
    }
    public class declgroup
    {

        public String DeclarationNumberToPrint_ { get; set; }

        public string DeclarationId { get; set; }
        public List<InspectionAppointment> VehiclesAppointment { get; set; }
    }
   
}