<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="KnetPayment.GCSPayment" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="../GCSKnetassets/img/Titleicon.jpg" />
    <link rel="icon" type="image/png" href=".jpg" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title><%=Resources.Resource.SelfPaymentServices %></title>

    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
    <meta name="viewport" content="width=device-width" />


    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="../GCSKnetassets/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <!-- CSS Files -->
    <link href="../GCSKnetassets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../GCSKnetassets/css/gsdk-bootstrap-wizard.css" rel="stylesheet" />

    <link href="../GCSKnetassets/css/demo.css" rel="stylesheet" />

    <script>
</script>

    
    <script src="../Scripts/jquery.min.js" type="text/javascript" nonce="EDNnf03nceIOfn39fn3e9h3sdfa"></script>
    <%--<script src="../GCSKnetassets/js/jquery-2.2.4.min.js" type="text/javascript"></script>--%>
    <script src="../GCSKnetassets/js/bootstrap.min.js" type="text/javascript"></script>

   <%-- <script src="../GCSKnetassets/js/jquery.validate.min.js"></script>
    <script src="../Scripts/GCSKnet/jquery.unobtrusive-ajax.min.js"></script>
    <script src="../Scripts/GCSKnet/jquery.blockUI.js"></script>--%>

    
<script src="../Scripts/jquery.unobtrusive-ajax.min.js" nonce="EDNnf03nceIOfn39fn3e9h3sdfa"></script>
<script src="../Scripts/jquery.validate.min.js" nonce="EDNnf03nceIOfn39fn3e9h3sdfa"></script>
<script src="../Scripts/jquery.validate.unobtrusive.min.js" nonce="EDNnf03nceIOfn39fn3e9h3sdfa"></script>


    <script src="../GCSKnetassets/js/Common.js"></script>



</head>

<body style="background-color: #9ea9f917;" onfocus="parent_disable();" onclick="parent_disable();" dir="<%=Resources.Resource.dir%> ">

    <script>
        var SecurityCodeMsg1 = "<%=Resources.Resource.SecurityCodeMsg1%>";// "Please enter above References ";// "Please enter your TIRCNumber (or) Reference Number first !";
        var SecurityCodeMsg2 = "<%=Resources.Resource.SecurityCodeMsg2%>";//"Please enter your SecurityCode";
        //var SecurityCodeMsg3 = "Please enter your TIRCNumber and Security Code";
        var TIRCNumberMsg1 = "<%=Resources.Resource.TIRCNumberRequired%>";//"Please enter your TIRCNumber ";
        //console.log(TIRCNumberValue + " =" + ReferenceNumberValue);
        var SecurityCodeMsg3 = "<%=Resources.Resource.SecurityCodeMsg3%>";//"Please enter your TIRCNumber and  SecurityCode";

        //added 09/06
        var MobileMinLengthmsg = "<%=Resources.Resource.Mobileminlength%>";//"Please enter your TIRCNumber and  SecurityCode";
        var CivilIdmsg = "<%=Resources.Resource.CivilIdValidationFail%>";//"Please enter your TIRCNumber and  SecurityCode";


        var RefNumRequired = "<%=Resources.Resource.RefNumRequired%>";//"Please enter your RefNum";

        var messageConfig = {
            TIRCNumber: "<%=Resources.Resource.TIRCNumberRequired%>",//"Please enter your TIRCNumber",
            SecurityCode: "<%=Resources.Resource.TIRCNumberRequired%>",//"Please enter your TIRCNumber (or) Reference Number first !",//uncommented
            Mobile: {
                required:  "<%=Resources.Resource.MobileRequired1%>",//"Please enter your Mobile Number",
                minlength:  "<%=Resources.Resource.Mobileminlength%>",//"Please enter a valid Mobile number of 8 digits ",
                maxlength:  "<%=Resources.Resource.Mobileminlength%>"//"Please enter a valid Mobile number of 8 digits "
                //KuwaitMobileNum: "Please enter a valid Mobile number of 8 digits "
            },
            Email: {
                required:  "<%=Resources.Resource.EmailRequired1%>",//"Please enter a valid email address",
                minlength:  "<%=Resources.Resource.Emailminlength%>"////"Please enter a valid email address"
            },
            ReferenceNumber: {
                required:  "<%=Resources.Resource.RefNumRequired%>"//"Please enter a Reference Number"
            },
            CivilId: {
                required: "<%=Resources.Resource.CivilIdValidationFail%>"//"Please enter a Reference Number"
             }

        }


    </script>

    <script src="../GCSKnetassets/js/jquery.bootstrap.wizard.js" type="text/javascript"></script>

    <script src="../GCSKnetassets/js/Common.js"></script>
    <script src="../GCSKnetassets/js/gsdk-bootstrap-wizard.js"></script>



    <style>

/*ul {
    list-style-type:none;
    counter-reset:item 6;
}
ul > li {
    counter-increment:item -1;
}
ul > li:after {
    content: counter(item);
}*/

        .col-xs-2 {
            width: 31.666667%;
            min-height: 54px;
        }

        p {
            font-size: xx-small;
        }

        .fa-input {
            font-family: FontAwesome, 'Helvetica Neue', Helvetica, Arial, sans-serif;
        }

        .cardauthr {
            padding-left: 5px;
            padding-right: 5px;
        }

        .form-group {
            margin-bottom: 1px;
        }

        .spacer {
            margin-top: 10px
        }

        .wizard-card .info-text {
            margin: 0px 0 10px;
        }

        .wizard-card {
            background-color: #FFFFFF;
            /*#FFFFFF;*/
            box-shadow: 0 0 15px rgb(0, 0, 0), 0 0 1px 1px rgba(0, 0, 0, 0.18);
            border-radius: 15px;
        }

        .alert {
            padding: 10px;
            margin-bottom: -7px;
            margin-top: 10px;
            MARGIN-LEFT: 35px;
            margin-right: 35px;
            border: 1px solid transparent;
            border-radius: 50px;
        }

        .well {
            min-height: 20px;
            padding: 15px;
            /* margin-bottom: 20px; */
            margin: 7px;
            background-color: #0a090903;
            border: 1px solid #08080821;
            border-radius: 44px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.05);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.05);
        }

        .wizard-card[data-color="blue"] .moving-tab {
            /*font-size: 10px;*/
            text-transform: none;
            font-size: 17px;
            bottom: 0px;
        }

        .nav-pills > li > a {
            font-size: 14px;
            text-transform: none;
        }

        .nav-pills > li > a {
            /*Commneted this in main css file -   /*border: 1 !important;*/
            border-left: 1px solid #47494a73;
            border-right: 1px solid #47494a73;
            border-top: 1px solid #999999;
            border-bottom: 1px solid #999999;
        }

        .table-condensed > tbody > tr > td, .table-condensed > tbody > tr > th, .table-condensed > tfoot > tr > td, .table-condensed > tfoot > tr > th, .table-condensed > thead > tr > td, .table-condensed > thead > tr > th {
            padding: 3px;
            padding-left: 10px;
            padding-right: 10px;
        }

        .wizard-card .tab-content {
            min-height: 337px;
            padding: 5px 0px;
        }

        .table-bordered > tbody > tr > td, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > td, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > thead > tr > th {
            border: 7px solid #ddd;
        }
        /*.vertical-align {
    display: flex;
    align-items: center;
}*/
        /*.bordershdw {
             border-radius: 44px;
             box-shadow: inset 0 1px 1px rgba(0,0,0,.05);
        }*/
        .table-bordered {
            border: 9px solid #ddd;
        }
        /*@media screen and (max-width: 767px) {
            .table-responsive > .table-bordered {
                border: -1px;
            }
        }*/
        /*jqueryvalidation error block*/
        .help-block {
            display: block;
            margin-top: 5px;
            margin-bottom: 10px;
            color: #fb0d0dc7;
            /* border-color: red; */
            font-size: 11px;
        }
        /*form fonticon validation symbol placement*/
        .form-control + .form-control-feedback {
            border-radius: 6px;
            font-size: 19px;
            padding: 0 12px 0 0;
            position: absolute;
            right: 12px;
            top: 5px;
            vertical-align: middle;
            /*background-color: white;*/ /*version 2*/
            width: 1px;
        }

        .Splh {
            margin-top: 25px;
        }

        h4, .h4 {
            font-size: 18px;
        }

            .h4 .small, .h4 small, .h5 .small, .h5 small, .h6 .small, .h6 small, h4 .small, h4 small, h5 .small, h5 small, h6 .small, h6 small {
                font-size: 58%;
            }

        body {
            font-size: 12px;
        }

        .form-control[disabled], .form-control[readonly], fieldset[disabled] .form-control {
            background-color: #eeeeee0f;
        }

     
    .modal
    {
        position: fixed;
        z-index: 999;
        height: 100%;
        width: 100%;
        top: 0;
        left: 0;
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
        -moz-opacity: 0.8;
    }
    .center
    {
        z-index: 1000;
        margin: 300px auto;
        padding: 10px;
        width: 130px;
        background-color: White;
        border-radius: 10px;
        filter: alpha(opacity=100);
        opacity: 1;
        -moz-opacity: 1;
    }
    .center img
    {
        height: 128px;
        width: 128px;
    }
    </style>
    <%--http://localhost/es/GCSKnetPayment/GCSPayment.aspx--%>
    <div class="container">

        <div class="row Splh">
            <div class="col-lg-offset-2 col-lg-8 col-lg-offset-2 ">


                <div class="wizard-container" style="padding-top: 3px">

                    <div class="card wizard-card" data-color="blue" id="wizardProfile">
                        <form runat="server" action="" method="" onsubmit="RemovevalidationforReload()">
                            <!--        You can switch ' data-color="orange" '  with one of the next bright colors: "blue", "green", "orange", "red"          -->
                            
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="GCSKnetAnonymousUser" runat="server" />
    <asp:HiddenField ID="HiddenMobile" runat="server" />
    <asp:HiddenField ID="HiddenEmail" runat="server" />
                            <div class="wizard-header">
                                <%-- <h3>
                                    <b>Pay your Invoice </b>
                                    <br />
                                    <small>Fill in the form for instant E-Payment</small>

                                </h3>--%>
                                <div class="row">

                                    <div class="col-xs-4 text-center">
                                        <%-- <i class="fa fa-cc-visa fa-2x"></i>
                                        <i class="fa fa-cc-mastercard fa-2x"></i>
                                        <i class="fa fa-cc-amex fa-2x"></i>
                                        <i class="fa fa-cc-discover fa-2x"></i>--%>
                                        <img src="../Images/GCSKnet/gcslogo.png" class="img-responsive" style="min-height: 92px; min-width: 92px;" />
                                    </div>
                                    <%--    <div class="col-xs-offset-4 col-xs-4 col-xs-offset-4   text-center">--%>
                                    <div class="col-xs-4  text-center">
                                        <h4 style="padding-top: 0px; margin-top: 0px;">
                                            <b><%--Pay your Invoice --%><%=Resources.Resource.PayInVoice%> </b>
                                            <br />
                                            <small><%--Fill in the form for instant E-Payment--%><%=Resources.Resource.FillForm%></small>
                                            <div class="row">
                                                <div class="col-sm-12 col-xs-6">
                                                    <button class="btn btn-sm btn-info" type="button" style="color: white;
                                            background-color: #2CA8FF;
                                            border-color: #2CA8FF;cursor:pointer"  onclick="window.location.href='../Dashboard/MenuView'"><%=Resources.Resource.home%></button>
                                                </div>
                                                <div class="col-sm-12 col-xs-6">
                                                      <asp:ImageButton ID="LanguageSwitch" OnClick="LanguageSwitch_Click" CssClass="text-center" style="     width: 22px;
    cursor: pointer;
    padding-top: 4px;
    margin-left: 8px;" runat="server" ImageUrl="~/Images/GCSKnet/Lang.png" />
                                                </div>
                                            </div>
                                            <%--<img src="../Images/GCSKnet/Lang.png" runat="server" onclick="changeLang('lang');" class="text-center" style="    width: 22px;
                                            cursor: pointer;
                                            padding-top: 2px;" />--%>
                                        </h4>
                                    </div>
                                    <div class="col-xs-4 text-center" style="padding-left: 0px;">
                                        <%-- <i class="fa fa-cc-visa fa-2x"></i>
                                        <i class="fa fa-cc-mastercard fa-2x"></i>
                                        <i class="fa fa-cc-amex fa-2x"></i>
                                        <i class="fa fa-cc-discover fa-2x"></i>--%>
                                        <img src="../Images/GCSKnet/Self Payment.png"  class="img-responsive" style="width: 74%; min-height: 92px; min-width: 92px; cursor: pointer;height: 103px;" />
                                    </div>
                                </div>




                            </div>


                            <div class="wizard-navigation">

                                <% if (Resources.Resource.dir.ToLower() == "ltr") { %>
                                <ul class="nav nav-pills nav-wizard">
                                    <li><a href="#Receipt" data-toggle="tab"><%--Receipt to  Pay--%><%=Resources.Resource.ReceiptToPay%>  &raquo;&raquo;</a></li>
                                    <li><a href="#Details" data-toggle="tab"><%--Payment Details--%><%=Resources.Resource.PaymentDetails%> &raquo;&raquo;</a></li>
                                    <li><a href="#ProceedToPay" data-toggle="tab"><%--Proceed To Pay--%><%=Resources.Resource.ProceedPay%></a></li>
                                </ul>
                                <%}
                                    else { %>
                                <%-- <ul class="nav nav-pills nav-wizard">
                                    <li><a href="#ProceedToPay" data-toggle="tab"> <%=Resources.Resource.ProceedPay%></a></li>
                                    <li><a href="#Details" data-toggle="tab">&laquo;&laquo; <%=Resources.Resource.PaymentDetails%> </a></li>
                                     <li><a href="#Receipt" data-toggle="tab">&laquo;&laquo; <%=Resources.Resource.ReceiptToPay%> </a></li>
                                 </ul>--%>

                                 <ul id="testidforversion2" class="nav nav-pills nav-wizard">
                                    <li><a href="#Receipt" data-toggle="tab"><%--Receipt to  Pay--%><%=Resources.Resource.ReceiptToPay%>  &raquo;&raquo;</a></li>
                                    <li><a href="#Details" data-toggle="tab"><%--Payment Details--%><%=Resources.Resource.PaymentDetails%> &raquo;&raquo;</a></li>
                                    <li><a href="#ProceedToPay" data-toggle="tab"><%--Proceed To Pay--%><%=Resources.Resource.ProceedPay%></a></li>
                                </ul>
                                <%}
                                   %>
                            </div>

                            <div class="tab-content"  >
                               <div class="tab-pane" id="Receipt"  dir="<%=Resources.Resource.dir%>"  >
                                    <div class="row well" style="padding-left: 10px; padding-right: 10px">
                                        <h4 class="info-text "><b><%--Enter your Receipt Details--%><%=Resources.Resource.EnterReceiptDetails%></b></h4>

                                        <div class="col-xs-6 ">
                                            <div class="form-group fieldDivAlign" style="padding-bottom: 1px; min-height: 94PX;">

                                                <label><strong><%--TIRC Number--%><%=Resources.Resource.TIRCNum%> </strong><%--<small>(required)</small>--%></label>
                                                <div class="IP">
                                                    <%--<input name="TIRCNumber" runat="server" id="TIRCNumber" type="text" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="TIRC Number..." />--%>
                                                    <input name="TIRCNumber" runat="server" id="TIRCNumber" type="text" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="" />
                                                </div>
                                            </div>

                                            <div class="form-group fieldDivAlign" style="">
                                                <label><strong><%--Security Code--%> <%=Resources.Resource.SecurityCode%></strong><%--<small>(required)</small>--%></label>
                                                <div class="IP">
                                                    <%--<input name="SecurityCode" runat="server" id="SecurityCode" type="text" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="Security Code..." />--%>
                                                    <input name="SecurityCode" runat="server" id="SecurityCode" type="text" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-xs-6 ">
                                            <div class="form-group fieldDivAlign" style="padding-bottom: 1px; min-height: 94PX;">

                                                <label><strong><%--Reference Number--%><%=Resources.Resource.ReferenceNumber%> / Civil Id</strong><%--<small>(required)</small>--%></label>
                                                <div class="IP">
                                                    <%--<input name="ReferenceNumber" runat="server" id="ReferenceNumber" type="text" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="Reference Number..." />--%>
                                                    <input name="ReferenceNumber" runat="server" id="ReferenceNumber" type="text" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="" />
                                                </div>
                                            </div>
                                            <div class="form-group fieldDivAlign" style="">
                                                <label><strong><%--Mobile--%> <%=Resources.Resource.Mobile%></strong><%--<small>(required)</small>--%></label>
                                                <div class="IP">
                                                    <%--<input name="Mobile" runat="server" id="Mobile" type="text" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="Mobile..." />--%>
                                                    <input name="Mobile" runat="server"  id="Mobile" type="text" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="" />
                                              <%--readonly--%>
                                                    </div>
                                            </div>



                                        </div>
                                        <div class="col-xs-offset-3 col-xs-6 col-xs-offset-3 text-center ">

                                            <div class="form-group fieldDivAlign">
                                                <label><strong><%--Email--%> <%=Resources.Resource.Email%></strong><%--<small>(required)</small>--%></label>
                                                <div class="IP">
                                                    <%--<input name="Email" runat="server" type="text" id="Email" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="Email(User@abc.com)" />--%>
                                                    <input name="Email" runat="server"  type="text" id="Email" class="form-control input-sm" onkeydown="return (event.keyCode!=13);" placeholder="" />
                                              <%--readonly--%>
                                                    </div>
                                            </div>

                                        </div>

                                        <div id="ReceiptValidatorStatus" class="col-xs-12">
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="Details" >
                                    <h4 class="info-text" style="padding-top: 12px; padding-bottom: 12px;"><%--Verify and Confirm--%><%=Resources.Resource.VerifyandConfirm%></h4>
                                    <div class="row">

                                        <div class="col-sm-offset-2 col-sm-8 col-sm-offset-2  table-responsive  ">
                                            <table class="table table-bordered table-condensed table-hover table-sm fieldDivAlign"  dir="<%=Resources.Resource.dir%>" >
                                                <thead>
                                                    <tr>
                                                        <td colspan="2" class="text-center bg-primary"><strong><%--Receipt Lookup--%><%=Resources.Resource.ReceiptLookup%></strong></td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 50%"><strong><small><%--Reference Number--%><%=Resources.Resource.ReferenceNumber%></small></strong></td>
                                                        <td style="width: 50%" id="PD_ReferenceNumber" class="text-primary" runat="server"></td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 50%"><strong><small><%--Amount--%><%=Resources.Resource.Amount%></small></strong></td>
                                                        <td style="width: 50%" id="PD_Amount" class="text-primary" runat="server"></td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 50%"><strong><small><%--Payee Name--%><%=Resources.Resource.PayeeName%></small></strong></td>
                                                        <td style="width: 50%" id="PD_PayeeName" class="text-primary" runat="server"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%"><strong><small><%--Customer Mobile No.--%><%=Resources.Resource.CustomerMobNum%></small></strong></td>
                                                        <td style="width: 50%" id="PD_Mobile" class="text-primary" runat="server"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%"><strong><small><%--Email--%><%=Resources.Resource.Email%></small></strong></td>
                                                        <td style="width: 50%" id="PD_Email" class="text-primary" runat="server"></td>
                                                    </tr>
                                                </tbody>

                                            </table>

                                            <asp:HiddenField ID="SEAT" runat="server" />

                                        </div>

                                    </div>
                                </div>
                                <div class="tab-pane" id="ProceedToPay" >
                                    <h4 class="info-text" style="padding-top: 30px"><%=Resources.Resource.AgreeToPay%>  <%--Agree to E-Payment--%></h4>
                                    <div class="row">

                                        <div class="col-xs-offset-4 col-xs-4 col-xs-offset-4 img-responsive">
                                            <div class="choice" data-toggle="wizard-checkbox">
                                                <%--<asp:ImageButton ID="PayButton" OnClick="PayButton_Click" OnClientClick="AgreeTC(event)" Style="height: 140px;" ImageUrl="../GCSKnetassets/img/Pay.png" runat="server" />--%>
                                                <asp:ImageButton ID="PayButton" OnClientClick="AgreeTC(event)" Style="height: 140px;" ImageUrl="../GCSKnetassets/img/Pay.png" runat="server" />
                                            </div>
                                        </div>
                                        <div id="PaymentVerifierOnStart" runat="server" class="col-xs-12 text-center img-responsive">
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="wizard-footer height-wizard">
                                <div class="pull-right">
                                    <!-- &#xf0a9; -->
                                    <input type='button' id="Next" class='btn btn-next btn-fill btn-info btn-wd btn-sm fa-input' name='next' value='  &#8658; &nbsp; <%=Resources.Resource.Next %> ' />

                                </div>
                                <div class="pull-left">
                                    <!-- &#xf0a8;  -->
                                    <input type='button' id="Previous" class='btn btn-previous btn-fill btn-info btn-wd btn-sm fa-input' name='previous' value=' &#8656;  &nbsp;  <%=Resources.Resource.Previous%>' />
                                </div>
                                <div class="clearfix"></div>
                            </div>

                        </form>
                    </div>
                </div>
            </div>
        </div>

        <!-- <div class="row">

            <div class="col-xs-12 text-center" style="padding-top: 15px">
                <p>

                    <%=Resources.Resource.WelcomeGCS%>
                </p>
                <p>
                    <%=Resources.Resource.Address%>
                </p>
                <p>
                    <%=Resources.Resource.Telephone%>
                </p>
                <p>
                    <%=Resources.Resource.Fax%>
                </p>
            </div>

        </div> -->

    </div>
    <script type="text/javascript">


        var GCSKnetAnonymousUser ='<%=GCSKnetAnonymousUser.Value%>';
        var UserId ='<%= UserId.Value %>';
        var SEATV;
        var PGUrl = '<%= System.Configuration.ConfigurationManager.AppSettings["PGUrl"].ToString() %>';

        function RemovevalidationforReload() {
            $("#TIRCNumber").rules("remove");
            $("#ReferenceNumber").rules("remove");
            $("#SecurityCode").rules("remove");
            $("#Mobile").rules("remove");//DUPLICATE REMOVAL ONLY
            $("#Email").rules("remove");//DUPLICATE REMOVAL ONLY
        }
     <%--   $().ready(function () {
            if (GCSKnetAnonymousUser === "True") {
                //  TIRCNumber.Disabled = true;
                //SecurityCode.Disabled = true;
                $("#<%=TIRCNumber.ClientID%>").prop("readonly", true);
                $("#<%=SecurityCode.ClientID%>").prop("readonly", true);
            }
            else {
                $("#<%=Mobile.ClientID%>").val($("#<%=HiddenMobile.ClientID%>").val());
                $("#<%=Email.ClientID%>").val($("#<%=HiddenEmail.ClientID%>").val());
                $("#<%=Mobile.ClientID%>").prop("readonly", true);
                $("#<%=Email.ClientID%>").prop("readonly", true);
                $("#<%=ReferenceNumber.ClientID%>").prop("readonly", true);
                // Mobile.Disabled = true;
                //Email.Disabled = true;
                //ReferenceNumber.Disabled = true;
            }
        });--%>

        function PaymentVerifierOnStart(msg) {

            $("#ReceiptValidatorStatus").empty();
            $("#ReceiptValidatorStatus").html("<div class='alert alert-danger text-center'>" + msg + "</div>");


        }
        $("<%=PayButton.ClientID%>").click(function () { return false; }
        );

        var popupWindow = null;

        function AgreeTC(event) {

            event.preventDefault();

            var strWindowFeatures = "location=yes,height=650,width=820,scrollbars=yes,status=yes,left=20,top=20,titlebar=no,menubar=no,toolbar=no";
           //var SEATV = "<%=HttpUtility.UrlEncode( SEAT.Value)%>";
            var URL = PGUrl + "?tokenId=" + SEATV;//+ "&Mobile=" + $("#<%=Mobile.ClientID%>").val() + "&Email=" + $("#<%=Email.ClientID%>").val();//encodeURI(SEATV) ;'

            if (popupWindow && !popupWindow.closed) {
                popupWindow.focus();
            }
            else {
                popupWindow = window.open(URL, "_blank", strWindowFeatures);
                var timer = setInterval(function () {
                    //if (popupWindow && !popupWindow.closed) {
                    if (popupWindow.closed) {
                        clearInterval(timer);
                        window.location.reload();
                    }
                    //}
                }, 500);
            }



        }


        function parent_disable() {
            if (popupWindow && !popupWindow.closed) {
                popupWindow.focus();
            }

        }


        var ProceedConfirmation = false;


        function ReceiptValidatorCall() {
            //$(".modal").show();
            console.log('newwwww');
            $("#ReceiptValidatorStatus").empty();

            //var param = { ReceiptNumber: $("#<@%=TIRCNumber.ClientID%>").val(), ReferenceNumber: $("#<@%=ReferenceNumber.ClientID%>").val(), Mobile: $("#<@%=Mobile.ClientID%>").val(), SecurityCode: $("#<@%=SecurityCode.ClientID%>").val(), Email: $("#<@%=Email.ClientID%>").val() };

            var param = { CommonData: $("#<%=TIRCNumber.ClientID%>").val(), CommonData1: $("#<%=ReferenceNumber.ClientID%>").val(), CommonData2: $("#<%=Mobile.ClientID%>").val(), CommonData4: $("#<%=SecurityCode.ClientID%>").val(), CommonData3: $("#<%=Email.ClientID%>").val() };
            //console.log($("#TIRCNumber").val());
            console.log(param);
            $.ajax({
                type: "POST",
                url: "../GCSKnet/IsReceiptValid",// "GCSPayment.aspx/IsReceiptValid",
                data: JSON.stringify(param),// '{ReceiptNumber: "' + $("#PaymentDetails_TIRCNumber").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                timeout: 3000,
                success: OnSuccess,
                failure: OnFailure,
                beforeSend: function () {
                    $(".modal").show();
                },
                complete: function () {
                    $(".modal").hide();
                }
            });
            //console.log("MAIN= " + ProceedConfirmation);
            return ProceedConfirmation;
        }
        function OnSuccess(response) {
            console.log("ReceiptValidatorStatus");
            console.log(response);
            //console.log(response.d);
            var ReceiptAction = response;//.d;
            $(ReceiptAction).each(function () {

                if (ReceiptAction.VerifyReceiptDetailsforGCSSite.Proceed)// (Verifier.Proceed)// (response.d == true) 
                {  //alert("inga")
                    //console.log("TRUE==>" + ReceiptAction);
                    //console.log("TRUE==>" + ReceiptAction.VerifyReceiptDetailsforGCSSite);
                    //console.log("TRUE==>" + ReceiptAction.ReceiptDetailsMinified);
                    //console.log("TRUE==>" + ReceiptAction.VerifyReceiptDetailsforGCSSite.Proceed);
                    //console.log("TRUE==>" + ReceiptAction.ReceiptDetailsMinified.PayeeName);
                    $("#ReceiptValidatorStatus").empty();
                    $("#ReceiptValidatorStatus").html("<div class='alert alert-success text-center'>" + GetMessage(ReceiptAction.VerifyReceiptDetailsforGCSSite.Message) + " </div>");
                    //$("#Next").prop('disabled', false);
                    //GetReceiptDetails();
                    //console.log(det.ReferenceId);

                    $("#PD_PayeeName").html(ReceiptAction.ReceiptDetailsMinified.PayeeName);//.UserId);//.PaidByName);//.PayeeName);
                    $("#PD_Mobile").html(ReceiptAction.ReceiptDetailsMinified.Mobile);
                    $("#PD_Email").html(ReceiptAction.ReceiptDetailsMinified.CustEmail);
                    $("#PD_ReferenceNumber").html(ReceiptAction.ReceiptDetailsMinified.ReferenceNumber);
                    $("#PD_Amount").html(ReceiptAction.ReceiptDetailsMinified.Amount);
                    $("#<%=SEAT.ClientID%>").val(ReceiptAction.ReceiptDetailsMinified.TokenId);
                    SEATV = ReceiptAction.ReceiptDetailsMinified.TokenId;
                    //alert(SEATV);
                    ProceedConfirmation = true;
                    //console.log("MAIN S= " + ProceedConfirmation);

                }
                else {
                    //alert("anga")
                    $("#ReceiptValidatorStatus").empty();
                    $("#ReceiptValidatorStatus").html("<div class='alert alert-danger text-center'>" + GetMessage(ReceiptAction.VerifyReceiptDetailsforGCSSite.Message) + "</div>");
                    //$("#Next").prop('disabled', true);
                    ProceedConfirmation = false;
                    //console.log("MAIN SE= " + ProceedConfirmation);
                }
            });
        }
        function OnFailure(response) {
            console.log("failure");
            //console.log(response.d);
            var Verifier = response.d;
            $("#ReceiptValidatorStatus").empty();
            $("#ReceiptValidatorStatus").html("<div class='alert alert-danger text-center'>" + Verifier.Message + " </div>");
            //$("#Next").prop('disabled', true);

            ProceedConfirmation = false;
            //console.log("MAIN/SUB E= " + ProceedConfirmation);
        }


        $().ready(function () {
            $("#TIRCNumber , #ReferenceNumber ").focus(function () {
                $("#ReceiptValidatorStatus").empty();
            });


            $("#ReferenceNumber , #TIRCNumber").change(function () {
                var Civilidnumpassed = false;
                if ($("#ReferenceNumber").val().indexOf("/") < 0) {
                    Civilidnumpassed = true
                }



                //alert($("#ReferenceNumber").val().indexOf("TDR"));
                //if (1 === 1) {
                    if ($("#TIRCNumber").val() === '' && ($("#ReferenceNumber").val().indexOf("TDR") >= 0 || $("#ReferenceNumber").val().indexOf("tdr") >= 0)) {

                        $("#SecurityCode").parents(".IP").addClass("has-success").removeClass("has-error");
                        $("#SecurityCode").attr("aria-invalid", "false");
                        $("#SecurityCode").next("span").addClass("fa-check-circle").removeClass("fa-times-circle");
                        $("#SecurityCode-error").empty();

                        $("#TIRCNumber").parents(".IP").addClass("has-success").removeClass("has-error");
                        $("#TIRCNumber").attr("aria-invalid", "false");
                        $("#TIRCNumber").next("span").addClass("fa-check-circle").removeClass("fa-times-circle");
                        $("#TIRCNumber-error").empty();


                        $("#Email").parents(".IP").addClass("has-success").removeClass("has-error");
                        $("#Email").attr("aria-invalid", "false");
                        $("#Email").next("span").addClass("fa-check-circle").removeClass("fa-times-circle");
                        $("#Email-error").empty();
                    }
                    else if ($("#TIRCNumber").val() === '' && ($("#ReferenceNumber").val().indexOf("TDR") < 0 && $("#ReferenceNumber").val().indexOf("tdr") < 0)) {
                        //alert("new cond");

                        $("#SecurityCode").parents(".IP").addClass("has-error").removeClass("has-success");
                        $("#SecurityCode").attr("aria-invalid", "true");
                        $("#SecurityCode").next("span").addClass("fa-times-circle").removeClass("fa-check-circle");
                        $("#SecurityCode-error").html(SecurityCodeMsg3);//("Please enter your TIRCNumber and  SecurityCode");

                        $("#Email").parents(".IP").addClass("has-error").removeClass("has-success");
                        $("#Email").attr("aria-invalid", "true");
                        $("#Email").next("span").addClass("fa-times-circle").removeClass("fa-check-circle");
                        $("#Email-error").html(messageConfig.Email.required);




                    }
                    else if ($("#TIRCNumber").val() !== '' && ($("#ReferenceNumber").val().indexOf("TDR") < 0 && $("#ReferenceNumber").val().indexOf("tdr") < 0) && $("#SecurityCode").val() === '') {

                        $("#SecurityCode").parents(".IP").addClass("has-error").removeClass("has-success");
                        $("#SecurityCode").attr("aria-invalid", "true");
                        $("#SecurityCode").next("span").addClass("fa-times-circle").removeClass("fa-check-circle");
                        $("#SecurityCode-error").html(SecurityCodeMsg2);

                        $("#Email").parents(".IP").addClass("has-error").removeClass("has-success");
                        $("#Email").attr("aria-invalid", "true");
                        $("#Email").next("span").addClass("fa-times-circle").removeClass("fa-check-circle");
                        $("#Email-error").html(messageConfig.Email.required);
                    }
                //}
            });

        });


        function GetMessage(msgcode) {
            //debugger;
            var messagetext = '';
            switch (msgcode) {
                case "1":
                    messagetext =  "<%=Resources.Resource.MessageCode1%>";
                    break;
                case "2":
                    messagetext =  "<%=Resources.Resource.MessageCode2%>";
                    break;
                case "3":
                    messagetext =  "<%=Resources.Resource.MessageCode3%>";
                    break;
                case "4":
                    messagetext =  "<%=Resources.Resource.MessageCode4%>";
                    break;
                case "5":
                    messagetext =  "<%=Resources.Resource.MessageCode5%>";
                    break;
                case "13":
                    messagetext =  "<%=Resources.Resource.MessageCode13%>";
                    break;
                case "11":
                    messagetext =  "<%=Resources.Resource.MessageCode11%>";
                    break;
                <%--case "31":
                    messagetext = "<%=Resources.Resource.MessageCode31%>";
                    break;
                case "33":
                    messagetext = "<%=Resources.Resource.MessageCode33%>";
                    break;--%>
                default:
                    messagetext =  "<%=Resources.Resource.Someissuehasoccured%>";
            }
            return messagetext;
        }

        var SessionValid = false;
        function SessionChecker() {
            //$(".modal").show();
            console.log('sesssion');
            var param = { "UserId": UserId };
            console.log(param);
            $.ajax({
                type: "POST",
                url: "../GCSKnet/SessionChecker",
                data: JSON.stringify(param),// '{ReceiptNumber: "' + $("#PaymentDetails_TIRCNumber").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                timeout: 3000,
                success: OnSessionCheckerSuccess,
                failure: OnSessionCheckerFailure,
                beforeSend: function () {
                    $(".modal").show();
                },
                complete: function () {
                    $(".modal").hide();
                }
            });
            return SessionValid;
        }
        function OnSessionCheckerSuccess(response) {
            console.log(response);

            SessionValid = response.Active;
        }
        function OnSessionCheckerFailure(response) {
            console.log(response);

            SessionValid = response.Active;
        }


    </script>

    <div class="modal" style="display: none">
    <div class="center">
        <img alt="" src="../GCSKnetassets/img/loader.gif" />
    </div>
</div>
</body>



</html>


