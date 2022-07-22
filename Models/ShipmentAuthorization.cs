using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class ShipmentAuthorization : BrokerRenewalModel
    {
        //public Int64 EShipmentAuthorizationRequestId { get; set; }
        public String RequestNumber { get; set; }
        public String CompanyName { get; set; }
        public String CommercialLicenseNo { get; set; }
        public Int64 OrganizationId { get; set; }


        public List<SelectListItem> AuthorizationFormValidityType { get {
                return new List<SelectListItem>
                {
                    new SelectListItem{ Text="Please select validity type", Value="0" },
                    new SelectListItem{ Text="One use", Value="1" },
                    new SelectListItem{ Text="One year", Value="2" }

                };
            } }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        [Range(1, Int32.MaxValue)]//, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        public String AuthorizationFormValidity { get; set; }


        public Int64 RequesterUserId { get; set; }
        public String RequestSubmissionDateTime { get; set; }
        public String RequestCompletionDateTime { get; set; }
        public String StateId { get; set; }
        public String DateCreated { get; set; }
        public String CreatedBy { get; set; }
        public String ModifiedBy { get; set; }
        public String DateModified { get; set; }
        public Int64 OwnerOrgId { get; set; }
        public Int64 OwnerLocId { get; set; }
        public string docsid { get; set; }
        public List<SelectListItem> ddlDocumentTypesitems { get; set; }
        public List<BrFileResult> ListOfUploadedFiles { get; set; }
        public List<ShipmentAuthorizationDetails> shipmentAuthorizationDetails { get; set; }

                    
        public string HidDelegateInfo { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameDA")]
        [Required]
        public string AuthorizationFormRemarks { get; set; }

        public string Renewal { get; set; }
        public string Edit { get; set; }

    }

    public static class defaultshipmentAuthorizationDetails
    {
        public static List<ShipmentAuthorizationDetails> getdefaultvalue(List<SelectListItem> NT)
        {
            List<ShipmentAuthorizationDetails> shipmentAuthorizationDetailst =new  List<ShipmentAuthorizationDetails>();
            ShipmentAuthorizationDetails ShipmentAuthorizationDetailstt = new ShipmentAuthorizationDetails() { NameOfDelegate = "", NationalityType=NT,ShipmentAuthorizationDetailsSeqId = 1 };
            shipmentAuthorizationDetailst.Add(ShipmentAuthorizationDetailstt);
            //shipmentAuthorizationDetailst.Insert(0, ShipmentAuthorizationDetailstt);
            return shipmentAuthorizationDetailst;
        }
    }


    public class ShipmentAuthorizationDetails
    {

        //public Int64 EShipmentAuthorizationRequestsDetailsId { get; set; }
        //public Int64 EShipmentAuthorizationRequestId { get; set; }
        public String Delegationvalidity { get; set; }

        
        public int ShipmentAuthorizationDetailsSeqId { get; set; }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameDA")]
        public String NameOfDelegate { get; set; }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        [RegularExpression(@"^(1|2|3)((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(\d{5})$")]//, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CivilIdValidationFail")]
        public String CivilId { get; set; }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        [Range(1, Int32.MaxValue)]//, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        public String Gender { get; set; }

        public List<SelectListItem> NationalityType { get; set; }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        [Range(1, Int32.MaxValue)]//, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        public int? Nationality { get; set; }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        [RegularExpression(@"^([0-9]{8})$")]//, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileNumberValid")]
        public String MobileNumber { get; set; }
        public String AuthorizationNumber { get; set; }
        public String AuthorizationFormIssuedFrom { get; set; }
        public DateTime AuthorizationFormIssueDate { get; set; }
        public DateTime AuthorizationFormExpiryDate { get; set; }
        public String AuthorizationFormNotes { get; set; }
        public String StateId { get; set; }
        public DateTime DateCreated { get; set; }
        public String CreatedBy { get; set; }
        public String ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public Int64 OwnerOrgId { get; set; }
        public Int64 OwnerLocId { get; set; }

        public List<SelectListItem> AttorneyIssuedbyType
        {
            get
            {
                return new List<SelectListItem>
                {
                    //new SelectListItem{ Text="Please select a Attorney Issuer", Value="0" },
                    new SelectListItem{ Text="Ministry of Justice", Value="1" },
                    new SelectListItem{ Text="Authorization letter from the Organization", Value="2" }

                };
            }
        }


        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameDA")]
        public String AttorneyNumber { get; set; }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameDA")]
        [Range(1, Int32.MaxValue)]//, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameDA")]
        public String AttorneyIssuedby{ get; set; }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameDA")]
        public DateTime AttorneyIssueDate { get; set; }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameDA")]
        public DateTime AttorneyExpiryDate { get; set; }
        [Required]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameDA")]
        public String AttorneyRemarks { get; set; }

    }
    public class GenderTypes
    {
        public int GenderTypeID { get; set; }
        public string GenderTypeValue { get; set; }
    }

    public static class NationalityEng
    {
        
        public static List<SelectListItem> NationalityType()
        {
            
            return new List<SelectListItem>
            { new SelectListItem  {Value="0" ,Text="Please select the Nationality"},
                 new SelectListItem  {Value="5004" ,Text="Afghanistan"},
                                                new SelectListItem {Value="5010" ,Text="Albania"},
                                                new SelectListItem {Value="5081" ,Text="Algeria"},
                                                new SelectListItem {Value="5294" ,Text="American Samoa"},
                                                new SelectListItem {Value="5011" ,Text="Andorra"},
                                                new SelectListItem {Value="5007" ,Text="Angola"},
                                                new SelectListItem {Value="5008" ,Text="Anguilla"},
                                                new SelectListItem {Value="5015" ,Text="ANTARCTICA"},
                                                new SelectListItem {Value="5021" ,Text="Antigua & Barbuda"},
                                                new SelectListItem {Value="5013" ,Text="Argentina"},
                                                new SelectListItem {Value="5014" ,
                                                    Text="Armenia"},
                                                new SelectListItem {Value="5016" ,
                                                    Text="Aruba"},
                                                new SelectListItem {Value="5017" ,
                                                    Text="Australia"},
                                                new SelectListItem {Value="5018" ,
                                                    Text="Austria"},
                                                new SelectListItem {Value="5019" ,
                                                    Text="Azerbaijan"},
                                                new SelectListItem {Value="5028" ,
                                                    Text="Bahamas"},
                                                new SelectListItem {Value="7051030" ,
                                                    Text="Bahrain Free Zone"},
                                                new SelectListItem {Value="5025" ,
                                                    Text="Bangladesh"},
                                                new SelectListItem {Value="5035" ,
                                                    Text="Barbados"},
                                                new SelectListItem {Value="5042" ,
                                                    Text="Belarus"},
                                                new SelectListItem {Value="5023" ,
                                                    Text="Belgium"},
                                                new SelectListItem {Value="5029" ,
                                                    Text="Belize"},
                                                new SelectListItem {Value="5024" ,
                                                    Text="Benin"},
                                                new SelectListItem {Value="5031" ,
                                                    Text="Bermuda"},
                                                new SelectListItem {Value="5039" ,
                                                    Text="Bhutan"},
                                                new SelectListItem {Value="5032" ,
                                                   Text="Bolivia"},
                                                new SelectListItem {Value="5033" ,
                                                    Text="Bosnia And Herzegovina"},
                                                new SelectListItem {Value="5041" ,
                                                    Text="Botswana"},
                                                new SelectListItem {Value="7054218" ,
                                                    Text="BOUVET ISLAND"},
                                                new SelectListItem {Value="5034" ,
                                                    Text="Brazil"},
                                                new SelectListItem {Value="5133" ,
                                                    Text="British Indian Ocean Territory"},
                                                new SelectListItem {Value="5286" ,
                                                    Text="British Virgin Islands"},
                                                new SelectListItem {Value="5038" ,
                                                    Text="Brunei  Darussalam"},
                                                new SelectListItem {Value="5026" ,
                                                    Text="Bulgaria"},
                                                new SelectListItem {Value="5036" ,
                                                    Text="Burkina Faso (Upper Volta)"},
                                                new SelectListItem {Value="5022" ,
                                                    Text="Burundi"},
                                                new SelectListItem {Value="5144" ,
                                                    Text="Cambodia"},
                                                new SelectListItem {Value="5051" ,
                                                    Text="Cameroon"},
                                                new SelectListItem {Value="5044" ,
                                                    Text="Canada"},
                                                new SelectListItem {Value="5057" ,
                                                    Text="Cape Verde"},
                                                new SelectListItem {Value="5065" ,
                                                    Text="Cayman Islands"},
                                                new SelectListItem {Value="5043" ,
                                                    Text="Central African  Republic"},
                                                new SelectListItem {Value="5258" ,
                                                    Text="Chad"},
                                                new SelectListItem {Value="5052" ,
                                                    Text="Channel Islands"},
                                                new SelectListItem {Value="5047" ,
                                                    Text="Chile"},
                                                new SelectListItem {Value="5048" ,
                                                    Text="China"},
                                                new SelectListItem {Value="5064" ,
                                                    Text="Christmas Island"},
                                                new SelectListItem {Value="5045" ,
                                                    Text="Cocos (Keeling) Islands"},
                                                new SelectListItem {Value="5055" ,
                                                    Text="Colombia"},
                                                new SelectListItem {Value="5056" ,
                                                    Text="Comoros ( Islands )"},
                                                new SelectListItem {Value="5053" ,
                                                    Text="Congo ( Brazaville )"},
                                                new SelectListItem {Value="5054" ,
                                                    Text="Cook Islands"},
                                                new SelectListItem {Value="5059" ,
                                                    Text="Costa Rica"},
                                                new SelectListItem {Value="5049" ,
                                                    Text="Cote D'Ivoire  ( Ivory Coast )"},
                                                new SelectListItem {Value="5060" ,
                                                    Text="Croatia"},
                                                new SelectListItem {Value="5063" ,
                                                    Text="Cuba"},
                                                new SelectListItem {Value="7054225" ,
                                                    Text="CuRACAO"},
                                                new SelectListItem {Value="5066" ,
                                                    Text="Cyprus"},
                                                new SelectListItem {Value="5061" ,
                                                    Text="Czeck  Republic"},
                                                new SelectListItem {Value="5300" ,
                                                    Text="Democratic Republic Of  Congo ( Zaire )"},
                                                new SelectListItem {Value="5078" ,
                                                    Text="Denmark"},

                                                new SelectListItem {Value="5076" ,
                                                    Text="Djibouti"},
                                                new SelectListItem {Value="5077" ,
                                                   Text="Dominica"},
                                                new SelectListItem {Value="5079" ,
                                                   Text="Dominican Republic"},
                                                new SelectListItem {Value="5264" ,
                                                   Text="East Timur"},
                                                new SelectListItem {Value="5084" ,
                                                   Text="Egypt"},
                                                new SelectListItem {Value="5244" ,
                                                   Text="Elsalvador"},
                                                new SelectListItem {Value="5082" ,
                                                   Text="Equador"},
                                                new SelectListItem {Value="5083" ,
                                                   Text="Equatorial Guinea"},
                                                new SelectListItem {Value="5085" ,
                                                   Text="Eritrea"},
                                                new SelectListItem {Value="5087" ,
                                                   Text="Estonia"},
                                                new SelectListItem {Value="5088" ,
                                                   Text="Ethiopia"},

                                                new SelectListItem {Value="5090" ,
                                                   Text="Faeroe  Islands"},
                                                new SelectListItem {Value="5096" ,
                                                   Text="Falkland Islands ( Malvinas )"},
                                                new SelectListItem {Value="5095" ,
                                                   Text="Fiji"},
                                                new SelectListItem {Value="5094" ,
                                                   Text="Finland"},
                                                new SelectListItem {Value="5097" ,
                                                   Text="France"},
                                                new SelectListItem {Value="7050866" ,
                                                   Text="Free Trade Zone"},
                                                new SelectListItem {Value="5210" ,
                                                   Text="French  Polynesia"},
                                                new SelectListItem {Value="5123" ,
                                                   Text="French Guiana"},
                                                new SelectListItem {Value="7054219" ,
                                                   Text="FRENCH SOUTHERN"},
                                                new SelectListItem {Value="5107" ,
                                                   Text="Gabon"},
                                                new SelectListItem {Value="5115" ,
                                                   Text="Gambia"},
                                                new SelectListItem {Value="5110" ,
                                                   Text="Georgia"},
                                                new SelectListItem {Value="7054221" ,
                                                   Text="GEORGIA&SANDWICH ISL"},
                                                new SelectListItem {Value="5070" ,
                                                   Text="Germany"},
                                                new SelectListItem {Value="5111" ,
                                                   Text="Ghana"},
                                                new SelectListItem {Value="5112" ,
                                                   Text="Gibraltar"},
                                                new SelectListItem {Value="5118" ,
                                                   Text="Greece"},
                                                new SelectListItem {Value="5120" ,
                                                   Text="Greenland"},
                                                new SelectListItem {Value="5119" ,
                                                   Text="Grenada"},
                                                new SelectListItem {Value="5114" ,
                                                   Text="Guadelope"},
                                                new SelectListItem {Value="5124" ,
                                                   Text="Guam"},
                                                new SelectListItem {Value="5121" ,
                                                   Text="Guatemala"},
                                                new SelectListItem {Value="7054224" ,
                                                   Text="GUERNSEY"},
                                                new SelectListItem {Value="5122" ,
                                                   Text="Guinea-Bissau"},
                                                new SelectListItem {Value="5125" ,
                                                   Text="Guyana"},
                                                new SelectListItem {Value="5129" ,
                                                   Text="Haiti"},
                                                new SelectListItem {Value="7050992" ,
                                                   Text="Heard Island and McDonald Islands"},
                                                new SelectListItem {Value="5283" ,
                                                   Text="Holy Sea"},
                                                new SelectListItem {Value="5128" ,
                                                   Text="Honduras"},
                                                new SelectListItem {Value="5127" ,
                                                   Text="Hong Kong (Special Administrative Region Of China)"},
                                                new SelectListItem {Value="5130" ,
                                                   Text="Hungary"},
                                                new SelectListItem {Value="5137" ,
                                                   Text="Iceland"},
                                                new SelectListItem {Value="5132" ,
                                                   Text="India"},
                                                new SelectListItem {Value="5131" ,
                                                   Text="Indonesia"},
                                                new SelectListItem {Value="5135" ,
                                                   Text="Iran"},
                                                new SelectListItem {Value="5136" ,
                                                   Text="Iraq"},
                                                new SelectListItem {Value="5134" ,
                                                   Text="Ireland"},
                                                new SelectListItem {Value="7050994" ,
                                                   Text="Isle Of Man"},
                                                new SelectListItem {Value="5139" ,
                                                   Text="Italy"},
                                                new SelectListItem {Value="5140" ,
                                                   Text="Jamaica"},
                                                new SelectListItem {Value="5142" ,
                                                   Text="Japan"},
                                                new SelectListItem {Value="7054226" ,
                                                   Text="JERSEY"},
                                                new SelectListItem {Value="5141" ,
                                                   Text="Jordan"},
                                                new SelectListItem {Value="5151" ,
                                                   Text="Kazakhstan"},
                                                new SelectListItem {Value="5145" ,
                                                   Text="Kenya"},
                                                new SelectListItem {Value="5149" ,
                                                   Text="Kiribati"},
                                                new SelectListItem {Value="5216" ,
                                                   Text="Korea (North)"},
                                                new SelectListItem {Value="5147" ,
                                                   Text="Korea (South)"},
                                                new SelectListItem {Value="7238404" ,
                                                   Text="KOSOVO"},
                                                new SelectListItem {Value="5150" ,
                                                   Text="Kuwait"},
                                                new SelectListItem {Value="5146" ,
                                                   Text="Kyrgyzstan"},
                                                new SelectListItem {Value="5152" ,
                                                   Text="Lao People's Democratic Republic (Laos)"},
                                                new SelectListItem {Value="5161" ,
                                                   Text="Latvia"},
                                                new SelectListItem {Value="5153" ,
                                                   Text="Lebanon"},
                                                new SelectListItem {Value="5159" ,
                                                   Text="Lesotho"},
                                                new SelectListItem {Value="5154" ,
                                                   Text="Liberia"},
                                                new SelectListItem {Value="5155" ,
                                                   Text="Libya"},
                                                new SelectListItem {Value="5156" ,
                                                   Text="Liechtenstein"},
                                                new SelectListItem {Value="5160" ,
                                                   Text="Lithuania"},
                                                new SelectListItem {Value="5162" ,
                                                   Text="Luxembourg"},
                                                new SelectListItem {Value="7051036" ,
                                                   Text="Macao"},
                                                new SelectListItem {Value="5169" ,
                                                   Text="Macedonia"},
                                                new SelectListItem {Value="5171" ,
                                                   Text="Madagascar"},
                                                new SelectListItem {Value="5188" ,
                                                   Text="Malawi"},
                                                new SelectListItem {Value="5190" ,
                                                   Text="Malaysia"},
                                                new SelectListItem {Value="5172" ,
                                                   Text="Maldives"},
                                                new SelectListItem {Value="5176" ,
                                                   Text="Mali"},
                                                new SelectListItem {Value="5177" ,
                                                   Text="Malta"},
                                                new SelectListItem {Value="5183" ,
                                                   Text="Marshall Islands"},
                                                new SelectListItem {Value="5186" ,
                                                   Text="Martinique"},
                                                new SelectListItem {Value="5182" ,
                                                   Text="Mauritania"},
                                                new SelectListItem {Value="5187" ,
                                                   Text="Mauritius"},
                                                new SelectListItem {Value="5297" ,
                                                   Text="Mayotte"},
                                                new SelectListItem {Value="5173" ,
                                                   Text="Mexico"},
                                                new SelectListItem {Value="5098" ,
                                                   Text="Micronesia, Federated States Of"},
                                                new SelectListItem {Value="5175" ,
                                                   Text="Moldova"},
                                                new SelectListItem {Value="5170" ,
                                                   Text="Monaco"},
                                                new SelectListItem {Value="5178" ,
                                                   Text="Mongolia"},
                                                new SelectListItem {Value="5185" ,
                                                   Text="MONTENEGRO"},
                                                new SelectListItem {Value="5184" ,
                                                   Text="Montserrat"},
                                                new SelectListItem {Value="5167" ,
                                                   Text="Morocco"},
                                                new SelectListItem {Value="5179" ,
                                                   Text="Mozambique"},
                                                new SelectListItem {Value="5189" ,
                                                   Text="Myanmar  ( Burma )"},
                                                new SelectListItem {Value="5191" ,
                                                    Text="Namibia"},
                                                new SelectListItem {Value="5201" ,
                                                    Text="Nauru"},
                                                new SelectListItem {Value="5200" ,
                                                    Text="Nepal"},
                                                new SelectListItem {Value="5198" ,
                                                    Text="Netherlands"},
                                                new SelectListItem {Value="5012" ,
                                                    Text="Netherlands  Antilles  Islands"},
                                                new SelectListItem {Value="7054220" ,
                                                    Text="NETHERLANDS ANTILLES"},
                                                new SelectListItem {Value="5192" ,
                                                    Text="New Caledonia"},
                                                new SelectListItem {Value="5202" ,
                                                    Text="New Zealand"},
                                                new SelectListItem {Value="5196" ,
                                                    Text="Nicaragua"},
                                                new SelectListItem {Value="5193" ,
                                                    Text="Niger"},
                                                new SelectListItem {Value="5195" ,
                                                    Text="Nigeria"},
                                                new SelectListItem {Value="5197" ,
                                                    Text="Niue"},
                                                new SelectListItem {Value="7054538" ,
                                                    Text="NON-KUWAITI"},
                                                new SelectListItem {Value="5194" ,
                                                    Text="Norfolk Island"},
                                                new SelectListItem {Value="5181" ,
                                                    Text="Northern Mariana Islands"},
                                                new SelectListItem {Value="5199" ,
                                                    Text="Norway"},
                                                new SelectListItem {Value="5204" ,
                                                    Text="Pakistan"},
                                                new SelectListItem {Value="5212" ,
                                                    Text="Palau"},
                                                new SelectListItem {Value="5211" ,
                                                    Text="Palestine (Gazza)"},
                                                new SelectListItem {Value="5205" ,
                                                    Text="Panama"},
                                                new SelectListItem {Value="5213" ,
                                                    Text="Papua New Guinea"},
                                                new SelectListItem {Value="5218" ,
                                                    Text="Paraguay"},
                                                new SelectListItem {Value="5207" ,
                                                    Text="Peru"},
                                                new SelectListItem {Value="5208" ,
                                                    Text="Philippines"},
                                                new SelectListItem {Value="5219" ,
                                                    Text="Pitcairn"},
                                                new SelectListItem {Value="5214" ,
                                                    Text="Poland"},
                                                new SelectListItem {Value="5217" ,
                                                    Text="Portugal"},
                                                new SelectListItem {Value="7050993" ,
                                                    Text="Puerto Rico"},
                                                new SelectListItem {Value="5220" ,
                                                    Text="Qatar"},
                                                new SelectListItem {Value="5113" ,
                                                  Text="Republic Of Guinea"},
                                                new SelectListItem {Value="5223" ,
                                                  Text="Reunion"},
                                                new SelectListItem {Value="5224" ,
                                                  Text="Romania"},
                                                new SelectListItem {Value="5225" ,
                                                  Text="Russian Federation"},
                                                new SelectListItem {Value="5226" ,
                                                  Text="Rwanda"},
                                                new SelectListItem {Value="5237" ,
                                                    Text="Saint Helena"},
                                                new SelectListItem {Value="5239" ,
                                                    Text="Saint Kitts And Nevis"},
                                                new SelectListItem {Value="5243" ,
                                                    Text="Saint Lucia"},
                                                new SelectListItem {Value="5247" ,
                                                    Text="Saint Pierre And Miquelon"},
                                                new SelectListItem {Value="5284" ,
                                                    Text="Saint Vincent And The Grenadines"},
                                                new SelectListItem {Value="5227" ,
                                                    Text=" ( Western Samoa )"},
                                                new SelectListItem {Value="5245" ,
                                                    Text="San Marino"},
                                                new SelectListItem {Value="5248" ,
                                                    Text="Sao Tome And Principe"},
                                                new SelectListItem {Value="5228" ,
                                                    Text="Saudi Arabia"},
                                                new SelectListItem {Value="5231" ,
                                                    Text="Senegal"},
                                                new SelectListItem {Value="5255" ,
                                                    Text="Seychelles"},
                                                new SelectListItem {Value="5241" ,
                                                    Text="Sierra Leone"},
                                                new SelectListItem {Value="5234" ,
                                                    Text="Singapore"},
                                                new SelectListItem {Value="5242" ,
                                                    Text="Slovakia"},
                                                new SelectListItem {Value="5252" ,
                                                   Text="Slovenia"},
                                                new SelectListItem {Value="5240" ,
                                                    Text="Solomon Islands"},
                                                new SelectListItem {Value="5246" ,
                                                    Text="Somalia"},
                                                new SelectListItem {Value="5299" ,
                                                    Text="South Africa"},
                                                new SelectListItem {Value="5086" ,
                                                    Text="Spain"},
                                                new SelectListItem {Value="5157" ,
                                                    Text="Sri Lanka (Ceylon)"},
                                                new SelectListItem {Value="5230" ,
                                                    Text="Sudan"},
                                                new SelectListItem {Value="5203" ,
                                                    Text="Sultanate Of Oman"},
                                                new SelectListItem {Value="5250" ,
                                                    Text="Suriname"},
                                                new SelectListItem {Value="7050991" ,
                                                    Text="Svalbard And Jan Mayen Islands"},
                                                new SelectListItem {Value="5254" ,
                                                    Text="Swaziland"},
                                                new SelectListItem {Value="5253" ,
                                                    Text="Sweden"},
                                                new SelectListItem {Value="5046" ,
                                                    Text="Switzerland"},
                                                new SelectListItem {Value="5256" ,
                                                    Text="Syria"},
                                                new SelectListItem {Value="5272" ,
                                                    Text="Taiwan"},
                                                new SelectListItem {Value="5262" ,
                                                    Text="Tajikistan"},
                                                new SelectListItem {Value="5273" ,
                                                    Text="Tanzania"},
                                                new SelectListItem {Value="5260" ,
                                                    Text="Thailand"},
                                                new SelectListItem {Value="5259" ,
                                                    Text="Togo"},
                                                new SelectListItem {Value="7050990" ,
                                                    Text="Tokelau"},
                                                new SelectListItem {Value="5266" ,
                                                    Text="Tonga"},
                                                new SelectListItem {Value="5267" ,
                                                    Text="Trinidad & Tobago"},
                                                new SelectListItem {Value="5269" ,
                                                    Text="Tunisia"},
                                                new SelectListItem {Value="5270" ,
                                                    Text="Turkey"},
                                                new SelectListItem {Value="5263" ,
                                                    Text="Turkmenistan"},
                                                new SelectListItem {Value="5257" ,
                                                    Text="Turks And Caicos  Islands"},
                                                new SelectListItem {Value="5271" ,
                                                    Text="Tuvalu"},
                                                new SelectListItem {Value="5277" ,
                                                   Text="Uganada"},
                                                new SelectListItem {Value="5278" ,
                                                   Text="Ukraine"},
                                                new SelectListItem {Value="5274" ,
                                                   Text="United Arab Emirates"},
                                                new SelectListItem {Value="5109" ,
                                                   Text="United Kingdom"},
                                                new SelectListItem {Value="5279" ,
                                                   Text="United States Minor Outlying Islands"},
                                                new SelectListItem {Value="5281" ,
                                                   Text="United States Of America"},
                                                new SelectListItem {Value="5287" ,
                                                   Text="United States Virgin Islands"},
                                                new SelectListItem {Value="5280" ,
                                                   Text="Uruguay"},
                                                new SelectListItem {Value="5282" ,
                                                   Text="Uzbekistan"},
                                                new SelectListItem {Value="5290" ,
                                                    Text="Vanuatu"},
                                                new SelectListItem {Value="5285" ,
                                                    Text="Venezuela"},
                                                new SelectListItem {Value="5288" ,
                                                    Text="Vietnam"},
                                                new SelectListItem {Value="5292" ,
                                                    Text="Wallis And Futuna Islands"},
                                                new SelectListItem {Value="5293" ,
                                                    Text="Western Sahara"},
                                                new SelectListItem {Value="5296" ,
                                                   Text="Yemen"},
                                                new SelectListItem {Value="5298" ,
                                                   Text="Yugoslavia ( Serbia & Montenegro )"},
                                                new SelectListItem {Value="5301" ,
                                                    Text="Zambia"},
                                                new SelectListItem {Value="5302" ,
                                                    Text="Zimbabwe"}
            };
        }
       
        
    }
    public static class NationalityAra
    {
        public static List<SelectListItem> NationalityType() {  return new List<SelectListItem> {new SelectListItem { Value = "0", Text = "اختيار الجنسية" },
                                                new SelectListItem
                                                {
                                                    Value = "5004"
                                                    ,
                                                    Text = "أفغانستان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5010"
                                                    ,
                                                    Text = "البانيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5081"
                                                    ,
                                                    Text = "الجزائر "
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5294"
                                                    ,
                                                    Text = "ساموا الامريكية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5011"
                                                    ,
                                                    Text = "أندورا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5007"
                                                    ,
                                                    Text = "أنغولا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5008"
                                                    ,
                                                    Text = "انغويلا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5015"
                                                    ,
                                                    Text = " انتراكتيكا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5021"
                                                    ,
                                                    Text = "  انتيكوا و باربودا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5013"
                                                    ,
                                                    Text = " الارجنتين"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5014"
                                                    ,
                                                    Text = " ارمينيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5016"
                                                    ,
                                                    Text = " آروبا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5017"
                                                    ,
                                                    Text = " استراليا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5018"
                                                    ,
                                                    Text = " النمسا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5019"
                                                    ,
                                                    Text = "  أذربيجان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5028"
                                                    ,
                                                    Text = " جزر البهاما"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5025"
                                                    ,
                                                    Text = " بنغلادش"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5035"
                                                    ,
                                                    Text = " باربادوس"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5042"
                                                    ,
                                                    Text = " بلارس"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5023"
                                                    ,
                                                    Text = " بلجيكا"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5039"
                                                    ,
                                                    Text = " بوتان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5032"
                                                    ,
                                                    Text = " بوليفيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5033"
                                                    ,
                                                    Text = "  البوسنة والهرسك"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5041"
                                                    ,
                                                    Text = "  بتسوانا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7054218"
                                                    ,
                                                    Text = " جزيرة بوفيت"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5034"
                                                    ,
                                                    Text = " البرازيل"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5133"
                                                    ,
                                                    Text = " مقاطعات المحيط الهند"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5286"
                                                    ,
                                                    Text = " جزر العذراء البريطانية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5038"
                                                    ,
                                                    Text = " بروناى دار السلام"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5026"
                                                    ,
                                                    Text = " بلغاريا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5036"
                                                    ,
                                                    Text = " بوركينا فاسو (فولتا العليا)"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5022"
                                                    ,
                                                    Text = " بورندى"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5144"
                                                    ,
                                                    Text = " كمبوديا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5051"
                                                    ,
                                                    Text = " الكاميرون"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5044"
                                                    ,
                                                    Text = " كندا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5057"
                                                    ,
                                                    Text = " كيب فيردى"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5065"
                                                    ,
                                                    Text = "  جزر كايمن"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5043"
                                                    ,
                                                    Text = " جمهورية إفريقيا الوسطي"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5258"
                                                    ,
                                                    Text = " تشاد"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5052"
                                                    ,
                                                    Text = " جزر شانيل"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5047"
                                                    ,
                                                    Text = "  شيلي"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5048"
                                                    ,
                                                    Text = "  الصين"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5064"
                                                    ,
                                                    Text = " جزيرة الكريستماس"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5045"
                                                    ,
                                                    Text = " جزر الكوكوس"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5055"
                                                    ,
                                                    Text = " كولومبيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5056"
                                                    ,
                                                    Text = " جزر القمر"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5053"
                                                    ,
                                                    Text = " الكنغو (برازافيل)"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5054"
                                                    ,
                                                    Text = " جزر كوك"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5059"
                                                    ,
                                                    Text = " كوستاريكا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5049"
                                                    ,
                                                    Text = "  ساحل العاج"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5060"
                                                    ,
                                                    Text = "  كرواتيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5063"
                                                    ,
                                                    Text = " كوبا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7054225"
                                                    ,
                                                    Text = "  كوراكاو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5066"
                                                    ,
                                                    Text = "  قبرص"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5061"
                                                    ,
                                                    Text = " التشيك"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5300"
                                                    ,
                                                    Text = " زائير"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5078"
                                                    ,
                                                    Text = "  الدانمارك"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5076"
                                                    ,
                                                    Text = "  جيبوتي"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5077"
                                                    ,
                                                    Text = " دومينيكا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5079"
                                                    ,
                                                    Text = "  جمهورية الدومينيكان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5264"
                                                    ,
                                                    Text = " تيمور الشرقية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5084"
                                                    ,
                                                    Text = "  مصر"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5244"
                                                    ,
                                                    Text = " السلفادور"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5082"
                                                    ,
                                                    Text = "  اكوادور"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5083"
                                                    ,
                                                    Text = " غينيا الاستوائية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5085"
                                                    ,
                                                    Text = " اريتريا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5087"
                                                    ,
                                                    Text = " أستونيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5088"
                                                    ,
                                                    Text = " أثيوبيا"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5090"
                                                    ,
                                                    Text = "  جزر الفاورو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5096"
                                                    ,
                                                    Text = "  جزر الفوكلاند ( مالفيناس )"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5095"
                                                    ,
                                                    Text = "  جزر فيجي"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5094"
                                                    ,
                                                    Text = " فنلندا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5097"
                                                    ,
                                                    Text = " فرنسا"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5210"
                                                    ,
                                                    Text = "  جزر بولينيشيا الفرنسية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5123"
                                                    ,
                                                    Text = "  غوىيانا الفرنسية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7054219"
                                                    ,
                                                    Text = "  مناطق فرنسا الشمالية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5107"
                                                    ,
                                                    Text = "  غابون"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5115"
                                                    ,
                                                    Text = " غامبيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5110"
                                                    ,
                                                    Text = "  جورجيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7054221"
                                                    ,
                                                    Text = "  جزر جورجيا & ساندويش"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5070"
                                                    ,
                                                    Text = "  المانيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5111"
                                                    ,
                                                    Text = "  غانا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5112"
                                                    ,
                                                    Text = "  جبل طارق"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5118"
                                                    ,
                                                    Text = "  اليونان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5120"
                                                    ,
                                                    Text = " جزيرة غرينلاند"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5119"
                                                    ,
                                                    Text = " غرينادا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5114"
                                                    ,
                                                    Text = "  غواديلوب"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5124"
                                                    ,
                                                    Text = " غوام"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5121"
                                                    ,
                                                    Text = "  غواتيمالا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7054224"
                                                    ,
                                                    Text = " غوريسني"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5122"
                                                    ,
                                                    Text = "  غينيا بيساو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5125"
                                                    ,
                                                    Text = " غويانا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5129"
                                                    ,
                                                    Text = " هاييتي"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7050992"
                                                    ,
                                                    Text = " دول أقيانوسيا الاخرى"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5283"
                                                    ,
                                                    Text = "  البحر المقدس"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5128"
                                                    ,
                                                    Text = "  هوندوراس"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5127"
                                                    ,
                                                    Text = "  هونغ كونغ"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5130"
                                                    ,
                                                    Text = " هنغاريا (المجر)"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5137"
                                                    ,
                                                    Text = " ايسلندا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5132"
                                                    ,
                                                    Text = "  الهند"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5131"
                                                    ,
                                                    Text = "  أندونيسيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5135"
                                                    ,
                                                    Text = "  إيران"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5136"
                                                    ,
                                                    Text = "  العراق"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5134"
                                                    ,
                                                    Text = "  ايرلندا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7050994"
                                                    ,
                                                    Text = "  جزيرة  مان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5139"
                                                    ,
                                                    Text = "  ايطاليا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5140"
                                                    ,
                                                    Text = " جامايكا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5142"
                                                    ,
                                                    Text = "  اليابان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7054226"
                                                    ,
                                                    Text = "  جيرسي"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5141"
                                                    ,
                                                    Text = "  الأردن"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5151"
                                                    ,
                                                    Text = " كازاخستان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5145"
                                                    ,
                                                    Text = " كينيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5149"
                                                    ,
                                                    Text = " كيريباتى"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5216"
                                                    ,
                                                    Text = "  كوريا ش"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5147"
                                                    ,
                                                    Text = "  كوريا ج"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7238404"
                                                    ,
                                                    Text = " كوسوفو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5150"
                                                    ,
                                                    Text = "  الكويت"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5146"
                                                    ,
                                                    Text = "  كيرجستان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5152"
                                                    ,
                                                    Text = "  جمهورية لاو ( لاوس )"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5161"
                                                    ,
                                                    Text = " لاتفيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5153"
                                                    ,
                                                    Text = " لبنان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5159"
                                                    ,
                                                    Text = "  ليسوتو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5154"
                                                    ,
                                                    Text = " ليبريا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5155"
                                                    ,
                                                    Text = " ليبيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5156"
                                                    ,
                                                    Text = " ليشتنشتاين"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5160"
                                                    ,
                                                    Text = " ليتوانيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5162"
                                                    ,
                                                    Text = " لوكسمبرغ"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7051036"
                                                    ,
                                                    Text = " ماكاو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5169"
                                                    ,
                                                    Text = " مقدونيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5171"
                                                    ,
                                                    Text = " مدغشقر"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5188"
                                                    ,
                                                    Text = " ملاوى"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5190"
                                                    ,
                                                    Text = " ماليزيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5172"
                                                    ,
                                                    Text = "  جزر المالديف"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5176"
                                                    ,
                                                    Text = "  مالي"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5177"
                                                    ,
                                                    Text = "  مالطه"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5183"
                                                    ,
                                                    Text = "  جزر مارشال"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5186"
                                                    ,
                                                    Text = "  مارتينيك"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5182"
                                                    ,
                                                    Text = "  موريتانيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5187"
                                                    ,
                                                    Text = "  موريشيوس"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5173"
                                                    ,
                                                    Text = " المكسيك"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5098"
                                                    ,
                                                    Text = "  دول ميكرونيشيا الاتحادية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5175"
                                                    ,
                                                    Text = "  مولدوفيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5170"
                                                    ,
                                                    Text = " موناكو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5178"
                                                    ,
                                                    Text = " منغوليا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5185"
                                                    ,
                                                    Text = "  مونتينيغرو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5184"
                                                    ,
                                                    Text = "  مونتسرات"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5167"
                                                    ,
                                                    Text = "  المغرب"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5179"
                                                    ,
                                                    Text = " موزمبيق"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5189"
                                                    ,
                                                    Text = " ميانمار (بورما )"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5191"
                                                    ,
                                                    Text = " ناميبيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5201"
                                                    ,
                                                    Text = "  ناورو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5200"
                                                    ,
                                                    Text = "  نيبال"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5198"
                                                    ,
                                                    Text = "  هولندا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5012"
                                                    ,
                                                    Text = " جزر انتيل  الهولندية"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5192"
                                                    ,
                                                    Text = " كاليدونيا الجديدة"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5202"
                                                    ,
                                                    Text = "  نيوزيلاندا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5196"
                                                    ,
                                                    Text = "  نيكاراجوا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5193"
                                                    ,
                                                    Text = "  النيجر"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5195"
                                                    ,
                                                    Text = " نيجيريا"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "7054538"
                                                    ,
                                                    Text = "  غير محدد الجنسية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5194"
                                                    ,
                                                    Text = " جزيرة نورفولك"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5181"
                                                    ,
                                                    Text = " جزر شمال ماريانا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5199"
                                                    ,
                                                    Text = "  النرويج"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5204"
                                                    ,
                                                    Text = " باكستان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5212"
                                                    ,
                                                    Text = "  بالاو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5211"
                                                    ,
                                                    Text = "  فلسطين (غزة)"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5205"
                                                    ,
                                                    Text = "  بنما"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5213"
                                                    ,
                                                    Text = "  بابوا غينيا الجديدة"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5218"
                                                    ,
                                                    Text = "  براغواى"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5207"
                                                    ,
                                                    Text = "  بيرو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5208"
                                                    ,
                                                    Text = " الفلبين"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5214"
                                                    ,
                                                    Text = "  بولندا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5217"
                                                    ,
                                                    Text = " البرتغال"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "7050993"
                                                    ,
                                                    Text = "  بورتو ريكو"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5220"
                                                    ,
                                                    Text = "  قطر"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5113"
                                                    ,
                                                    Text = " جمهورية غينيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5224"
                                                    ,
                                                    Text = " رومانيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5225"
                                                    ,
                                                    Text = "  روسيا الاتحادية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5226"
                                                    ,
                                                    Text = " رواندا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5237"
                                                    ,
                                                    Text = "  سانت هيلينا"
                                                },




                                                new SelectListItem
                                                {
                                                    Value = "5227"
                                                    ,
                                                    Text = "  ساموا ( ساموا الغربية )"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5245"
                                                    ,
                                                    Text = "  سان مارينو"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5228"
                                                    ,
                                                    Text = " السعودية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5231"
                                                    ,
                                                    Text = "  السنغال"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5255"
                                                    ,
                                                    Text = "  جزر سيشل"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5241"
                                                    ,
                                                    Text = "  سييراليون"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5234"
                                                    ,
                                                    Text = "  سنغافورة"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5242"
                                                    ,
                                                    Text = " سلوفاكيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5252"
                                                    ,
                                                    Text = " سلوفينيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5240"
                                                    ,
                                                    Text = "  جزر  سولومون"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5246"
                                                    ,
                                                    Text = "  الصومال"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5299"
                                                    ,
                                                    Text = "  جنوب أفريقيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5086"
                                                    ,
                                                    Text = " اسبانيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5157"
                                                    ,
                                                    Text = "  سريلانكا (سيلان)"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5230"
                                                    ,
                                                    Text = "  السودان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5203"
                                                    ,
                                                    Text = "  سلطنة عمان"
                                                },



                                                new SelectListItem
                                                {
                                                    Value = "5253"
                                                    ,
                                                    Text = "  السويد"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5046"
                                                    ,
                                                    Text = "  سويسرا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5256"
                                                    ,
                                                    Text = " سوريا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5272"
                                                    ,
                                                    Text = " تايوان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5262"
                                                    ,
                                                    Text = " طاجكستان"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5273"
                                                    ,
                                                    Text = "  تنزانيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5260"
                                                    ,
                                                    Text = "  تايلاند"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5259"
                                                    ,
                                                    Text = "  توغو"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5266"
                                                    ,
                                                    Text = "  تونغا"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5269"
                                                    ,
                                                    Text = "  تونس"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5270"
                                                    ,
                                                    Text = " تركيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5263"
                                                    ,
                                                    Text = " تركمانستان"
                                                },


                                                new SelectListItem
                                                {
                                                    Value = "5277"
                                                    ,
                                                    Text = "  أوغندا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5278"
                                                    ,
                                                    Text = "  أوكرانيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5274"
                                                    ,
                                                    Text = "   الإمارات"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5109"
                                                    ,
                                                    Text = "  المملكة المتحدة"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5281"
                                                    ,
                                                    Text = " امريكا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5287"
                                                    ,
                                                    Text = " جزر العذراء الامريكية"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5280"
                                                    ,
                                                    Text = " الاورجواي"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5282"
                                                    ,
                                                    Text = "  أوزبكستان"
                                                },

                                                new SelectListItem
                                                {
                                                    Value = "5285"
                                                    ,
                                                    Text = "  فنزويلا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5288"
                                                    ,
                                                    Text = " فيتنام"
                                                },


                                                new SelectListItem
                                                {
                                                    Value = "5296"
                                                    ,
                                                    Text = "  اليمن"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5298"
                                                    ,
                                                    Text = " يوغوسلافيا (صربيا)"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5301"
                                                    ,
                                                    Text = "  زامبيا"
                                                },
                                                new SelectListItem
                                                {
                                                    Value = "5302"
                                                    ,
                                                    Text = " زمبابوى"
                                                }
                                                };
        }
       
    }
}


/* old code 
using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ShipmentAuthorization:BrokerRenewalModel
    {
        //public Int64 EShipmentAuthorizationRequestId { get; set; }
        public String RequestNumber { get; set; }
        public String CompanyName { get; set; }
        public Int64 CommercialLicenseNo { get; set; }
        public Int64 OrganizationId { get; set; }
        public Int64 RequesterUserId { get; set; }
        public String RequestSubmissionDateTime { get; set; }
        public String RequestCompletionDateTime { get; set; }
        public String StateId { get; set; }
        public String DateCreated { get; set; }
        public String CreatedBy { get; set; }
        public String ModifiedBy { get; set; }
        public String DateModified { get; set; }
        public Int64 OwnerOrgId { get; set; }
        public Int64 OwnerLocId { get; set; }
        public List<SelectListItem> ddlDocumentTypesitems { get; set; }
        public List<BrFileResult> ListOfUploadedFiles { get; set; }
        public List<ShipmentAuthorizationDetails> shipmentAuthorizationDetails { get; set; }

    }



    public class ShipmentAuthorizationDetails
    {

        //public Int64 EShipmentAuthorizationRequestsDetailsId { get; set; }
        //public Int64 EShipmentAuthorizationRequestId { get; set; }
        public String Delegationvalidity { get; set; }
        public String NameOfDelegate { get; set; }
        public String CivilId { get; set; }
        public String Gender { get; set; }
        public int Nationality { get; set; }
        public String MobileNumber { get; set; }
        public String AuthorizationNumber { get; set; }
        public String AuthorizationFormIssuedFrom { get; set; }
        public DateTime AuthorizationFormIssueDate { get; set; }
        public DateTime AuthorizationFormExpiryDate { get; set; }
        public String AuthorizationFormNotes { get; set; }
        public String StateId { get; set; }
        public DateTime DateCreated { get; set; }
        public String CreatedBy { get; set; }
        public String ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public Int64 OwnerOrgId { get; set; }
        public Int64 OwnerLocId { get; set; }

    }

}
*/
