// Document Ready
var page_name = '';
var la = '';

var applicationUrl = 'http://10.10.26.226/eServices/';



function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}








$(document).ready(function () {

    //if (NotAllowedToByPassForOrgCreation !== null) {
    //    if (NotAllowedToByPassForOrgCreation === "0") {
    //        //$("#modalDiv").removeClass("animated hinge fast");

    //        $("#modalDiv").css("display", "block");
    //    }
    //}


    page_name = document.location.href;

    //alert(page_name);

    if (page_name.indexOf("MyAccount") != -1) {
        if (ViewBagShowmsg !== null) {
            if (ViewBagShowmsg === "0") {
                //$("#modalDiv").removeClass("animated hinge fast");

                $("#modalDiv").css("display", "block");
            }
        }
    }

   


    //$("[placeholder]").focus(function () {
    //    $(this).tooltip({
    //        items: "[placeholder]",
    //        content: function () {
    //            return $(this).attr("placeholder");
    //            //alert($(this).attr("placeholder"));
    //        }
    //    });
    //}).tooltip({
    //    items: "[placeholder]",
    //    content: function () {
    //        return $(this).attr("placeholder");
    //        //alert($(this).attr("placeholder"));
    //    }
    //});


    //$.ajax({
    //    type: "POST",
    //    url: applicationUrl + "User/GetNotifications",
    //    error: function (XMLHttpRequest, textStatus, errorThrown) {
    //        console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
    //        return false;
    //    },
    //    success: function (result) {
    //        console.log(result);
    //    },
    //    failure: function (errorThrown) {
    //        console.log(errorThrown);
    //        return false;
    //    }
    //}
    //);


    // 06 April
    var specialKeys = new Array();
    specialKeys.push(8);  //Backspace
    specialKeys.push(9);  //Tab
    specialKeys.push(46); //Delete
    specialKeys.push(36); //Home
    specialKeys.push(35); //End
    specialKeys.push(37); //Left
    specialKeys.push(39); //Right
    specialKeys.push(32); //Space

    // // Numbers With Letters, Without Special Characters
    $(".passport").keypress(function (e) {

        var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;

        if ((
            (keyCode >= 48 && keyCode <= 57) ||
            (keyCode >= 65 && keyCode <= 90) ||
            (keyCode >= 96 && keyCode <= 115)) && keyCode != 16) {
            console.log("true " + keyCode + " " + e.charCode)
            return true;
        }
        else if (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode) {
            console.log("true " + keyCode + " " + e.charCode)
            return true;
        }
        else {
            console.log("False " + keyCode + " " + e.charCode)
            return false;
        }

    });


    // Numbers Without any Letter
    $(".numericOnly").keydown(function (e) {

        var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
        if (((keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 115))) {
            return true;
        }

        else if (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode) {
            return true;
        }

        else {
            return false;
        }


    }
    );
    // Numbers Without any Letter
    $(".numericOnly").keypress(function (e) {

        var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
        if (((keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 115))) {
            console.log("true " + keyCode + " " + e.charCode)
            return true;
        }

        else if (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode) {
            console.log("true " + keyCode + " " + e.charCode)
            return true;
        }

        else {
            console.log("False " + keyCode + " " + e.charCode)
            return false;
        }


    }
    );

    // Letters Without Numbers
    $(".lettersOnly").keydown(function (e) {

        var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
        var ret = ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
        console.log(ret);
        console.log(keyCode);
        return ret;
    }
    );

    // Letters Without Numbers
    $(".lettersOnly").keyup(function (e) {

        var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
        var ret = ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
        console.log(ret);
        console.log(keyCode);
        return ret;
    }
    );


    $(".form-control").on("cut copy paste", function (e) {
        e.preventDefault();
        return false;
    });



    //$(".numericc").keydown(function (event) {
    //    //var keyCode = (event.which ? event.which : event.keyCode);

    //    //var ret = ((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode == 46 || (keyCode >= 96 && keyCode <= 105));

    //    var ckeckChars = /[^0-9]/gi;
    //    console.log(ckeckChars.test(event.target.value));
    //    return !ckeckChars.test(event.target.value);
    //    //return ret;
    //}
    //);













    // 

    //$(".arabicOnly").keydown(function (event)
    //{

    //    var isArabic = /[\u0600-\u06FF]/;

    //    var keyCode = event.which ? event.which : event.keyCode

    //    if (keyCode == 8 || keyCode == 46)
    //    {
    //        return true;
    //    }


    //    var value = String.fromCharCode(keyCode);

    //    if (isArabic.test(value))
    //    {
    //        return true;
    //    }


    //    return false;

    //}
    //);



    //$(".numeric").onChange(function (event) {
    //    var ckeckChars = /[^0-9]/gi;
    //    if (checkChars.test(event.target.value)) {
    //        event.target.value = event.target.value.replace(ckeckChars, "");
    //    }
    //});

    $(document).ajaxStart(function () {
        $("#loading").show();
    }).ajaxStop(function () {
        $("#loading").hide();
    });

    $("#CanCreateOrg").click(function (event) {
        //alert('CanCreateOrg');
        //window.location.href = applicationUrl + 'User/Org_Registration';
        $.ajax({
            type: "POST",
            //cache: false, // Added for Security 
            url: applicationUrl + "User/CanCreateOrg", //applicationUrl + "BrokerRenewal/CanCreateOrg",// applicationUrl + "User/CanCreateOrg", 
            data: { 'UserId': LUserId },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                alert('Some issues has occured');
                event.preventDefault();
                return false;
            },
            success: function (result) {
                console.log(result);
                console.log(result.Status);
                if (result.Status === "True") {
                    window.location.href = applicationUrl + 'User/Org_Registration';
                    return true;
                }
                else {
                    //alert('Main Organization is yet to be approved, untill that you cannot create new organizations');
                    //window.location.href = applicationUrl + 'User/Org_Registration';
                    //$("#OrgmodalMessage").text(CanCreateOrgMsg);
                    //$("#modalDiv").removeClass("animated hinge fast");

                    $("#modalDiv").css("display", "block");
                    event.preventDefault();
                    return false;
                }
            },
            failure: function (errorThrown) {
                //alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                alert('Some issues has occured');
                event.preventDefault();
                return false;
            }

        }
        );
    });

    $("#selectedCountryVal").val("0");

    $("#genderSelect").val("M");



    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });


    $('#sidebarCollapsee').on('click', function () {
        $('#sidebar').toggleClass('active');
    });



    $('#sidebarCollapseee').on('click', function () {
        $('#sidebar').toggleClass('active');
    });





    $("#AEF").click(function () {
        event.preventDefault();
        var usageAgreement = $("#usageAgreement").val();
        var usageAgreementHeader = $("#usageAgreementHeader").val();

        $("#modalMessage").text(usageAgreement);
        $("#modelHeaderTitle").text(usageAgreementHeader);


        //$("#modalDiv").removeClass("animated hinge fast");

        $("#modalDiv").css("display", "block");

    });

    //$("#col12Login").addClass("animated hinge slow");
    $(".dateClass").datepicker($.datepicker.regional["ar"]);

    page_name = document.location.href;//.match(/[^\/]+$/)[0];

    $(document).keypress(function (e) {

        if (e.which == 13) {

            //alert(e.which);


            $("#changeLangToEnglish").click(function (event) {
                event.preventDefault();
                // Do something
            });

            $("#changeLangToArabic").click(function (event) {
                event.preventDefault();
                // Do something
            });



            if (validateForm()) {
                $("#loginBtn").click();
            }
        }

    });


    $("#searchDeclaration").click(function () {

        var DescSearch = $("#DescSearch").val();

        if (!DescSearch) {
            event.preventDefault();
            $("#declarationErrorMsg").css("display", "block");

            $("#declarationErrorMsg").addClass("marginRightLeft10PX");
        }

        else {
            $("#declarationErrorMsg").css("display", "none");
        }
    });



    $("#searchHouseBill").click(function () {

        var housebillSearch = $("#HousebillSearch").val();

        if (!housebillSearch) {
            event.preventDefault();
            $("#housebillErrorMsg").css("display", "block");
            $("#housebillErrorMsg").addClass("marginRightLeft10PX");
        }

        else {
            $("#housebillErrorMsg").css("display", "none");
        }
    });





    $("#arrowUp").css("display", "none");
    $("#arrowUpp").css("display", "none");
    $("#arrowUppp").css("display", "none");
    $("#showInformationlbl").css("display", "none");




    $("#companyState").change(function () {
        var selectedCountry = $(this).children("option:selected").val();
        $("#selectedCountryVal").val(selectedCountry);
        //alert("You have selected the country - " + selectedCountry);
    });


    $("#genderSelect").change(function () {
        var selectedGenderVal = $(this).children("option:selected").val();
        $("#selectedGenderVal").val(selectedGenderVal);

    });


    //$("#navbar").css("background-color", "#c1daf5");

    //$("#navDiv").css("text-align", "center");

    //$(".inputBorder").css("border-bottom-color", "chocolate");

    // All Pages
    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').click(function () {

        $('body,html').animate({
            scrollTop: 0
        }, 500);
        return false;
    });

    function process(date) {
        var parts = date.split("/");
        return new Date(parts[2], parts[1] - 1, parts[0]);
    }
    $('#SubmitRequest').click(function (e) {



        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();

        today = dd + '/' + mm + '/' + yyyy;



        var lgHidden = getCookie("culture");

        //var EmailAlert;
        //var DateAlert;
        //if (lgHidden.indexOf("en") != -1) {
        //    EmailAlert = 'Email  is Mandatory';
        //    DateAlert = 'selected date Should Be Greater than Todays'
        //}
        //else {
        //    EmailAlert = 'ArEmail  is Mandatory';
        //    DateAlert = 'يجب أن يكون التاريخ المحدد أكبر من اليوم ';
        //}

        //var mySelectedDateForPassport = new Date($('#txtPassportExpirydatePickerhdn').val());


        //if ($('#CivilIdExpirydatePicker').val())
        //{
        //    if (process($('#CivilIdExpirydatePicker').val()) < process(today)) {
        //        alert(DateAlert);
        //        e.preventDefault();
        //    }
        //}

        //else
        //{
        //    e.preventDefault();
        //    alert("Plaadrsdfsdf");
        //}


        if ($('#CivilIdExpirydatePicker').val() != "") {

            //if (process($('#CivilIdExpirydatePicker').val()) < process(today)) {
            //    alert(DateAlert);
            //    e.preventDefault();
            //}
            //if ($('#txtPassportExpirydatePickerhdn').val() < today) {
            //    alert(DateAlert);
            //    e.preventDefault();
            //}
        }


        //if ($('#PassportExpirydatePicker').val() != "") {

        //    if (process($('#PassportExpirydatePicker').val()) < process(today)) {
        //        alert(DateAlert);
        //        e.preventDefault();
        //    }

        //}




        //if ($('#txtPassportExpirydatePickerhdn').val() != "") {

        //    if (process($('#txtPassportExpirydatePickerhdn').val()) < process(today)) {
        //        alert(DateAlert);
        //        e.preventDefault();
        //    }
        //    //if ($('#txtPassportExpirydatePickerhdn').val() < today) {
        //    //    alert(DateAlert);
        //    //    e.preventDefault();
        //    //}
        //}
        //if ($('#txtCivilIdExpirydatePickerhdn').val() != "") {

        //    if (process($('#txtCivilIdExpirydatePickerhdn').val()) < process(today)) {
        //        alert(DateAlert);
        //        e.preventDefault();
        //    }
        //    //if ($('#txtPassportExpirydatePickerhdn').val() < today) {
        //    //    alert(DateAlert);
        //    //    e.preventDefault();
        //    //}
        //}




        //if ($('#txtCivilIdExpirydatePickerhdn').val() != "") {

        //    if ($('#txtCivilIdExpirydatePickerhdn').val() < today) {
        //        alert(DateAlert);
        //        e.preventDefault();
        //    }
        //}
        //if ($('#txtTradeLicenseExpiryDatePickerhdn').val() != "") {
        //    if ($('#txtTradeLicenseExpiryDatePickerhdn').val() < today) {
        //        alert(DateAlert);
        //        e.preventDefault();
        //    }
        //}
        var MailAddresstxtElement = document.getElementById("MailAddresstxt");

        //if (MailAddresstxtElement != null) {
        //    if (document.getElementById("MailAddresstxt").value == "") {

        //        alert(EmailAlert);

        //        e.preventDefault();

        //    }
        //}



        var inlineBlockels = $(".inlineBlock");
        for (var i = 0; i < inlineBlockels.length; i++) {
            var element = inlineBlockels.eq(i);
            var $el = element;
            if ($el.html().length !== 0) {
                $el.addClass('marginRightLeft10PX');
            }
            else {
                $el.removeClass('marginRightLeft10PX');
            }
        }
    });


    if (page_name.indexOf("BrokerRenewal/BrokerUpdate") != -1) {
        //alert(page_name);

        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();

        today = dd + '/' + mm + '/' + yyyy;

        var lgHidden = getCookie("culture");

        var EmailAlert;
        var DateAlert;
        if (lgHidden.indexOf("en") != -1) {
            EmailAlert = 'Email  is Mandatory';
            DateAlert = 'selected date Should Be Greater than Todays'
        }
        else {
            EmailAlert = 'ArEmail  is Mandatory';
            DateAlert = 'يجب أن يكون التاريخ المحدد أكبر من اليوم ';
        }
        //28/02/2019

        //$(function () {
        //    $("#TradeLicenseExpiryDate").datepicker({ dateFormat: "yy-mm-dd" });
        //    $("#TradeLicenseExpiryDate").on("change", function () {
        //        var MySelecteddateForTradeLicense = $(this).val();
        //        $('#txtTradeLicenseExpiryDatePickerhdn').val(MySelecteddateForTradeLicense);
        //        //  alert(MySelecteddateForTradeLicense);
        //        //if (MySelecteddateForTradeLicense < today) {
        //        //    alert(DateAlert);
        //        //    //  e.preventDefault();
        //        //}


        //        if (process(MySelecteddateForTradeLicense) < process(today)) {
        //            alert(DateAlert);
        //            e.preventDefault();
        //        }



        //    });

        //    $("#PassportExpirydatePicker").datepicker({ dateFormat: "yy-mm-dd" });
        //    $("#PassportExpirydatePicker").on("change", function () {
        //        var mySelectedDateForPassport = $(this).val();
        //        //  alert(MySelecteddateForTradeLicense);
        //        $('#txtPassportExpirydatePickerhdn').val(mySelectedDateForPassport);

        //        //if (mySelectedDateForPassport < today) {
        //        //    alert(DateAlert);
        //        //    //  e.preventDefault();
        //        //}

        //        if (process(mySelectedDateForPassport) < process(today)) {
        //            alert(DateAlert);
        //            e.preventDefault();
        //        }

        //    });

        //    $("#CivilIdExpirydatePicker").datepicker({ dateFormat: "yy-mm-dd" });
        //    $("#CivilIdExpirydatePicker").on("change", function () {
        //        var mySelectedDateForCivilID = $(this).val();
        //        //  alert(MySelecteddateForTradeLicense);
        //        $('#txtCivilIdExpirydatePickerhdn').val(mySelectedDateForCivilID);

        //        //if (process(mySelectedDateForCivilID) < process(today)) {
        //        //    alert(DateAlert);
        //        //    e.preventDefault();
        //        //}

        //        if (mySelectedDateForCivilID < today) {
        //            alert(DateAlert);
        //            //  e.preventDefault();
        //        }

        //    });

        //});

        //$('#CivilIdExpirydatePicker').datepicker({
        //    //   $('#txtdate').val('');
        //    dateFormat: 'yy-mm-dd',
        //    onSelect: function (datetext) {
        //        var d = new Date(); // for now
        //        var h = d.getHours();
        //        h = (h < 10) ? ("0" + h) : h;

        //        var m = d.getMinutes();
        //        m = (m < 10) ? ("0" + m) : m;

        //        var s = d.getSeconds();
        //        s = (s < 10) ? ("0" + s) : s;

        //        $("txtCivilIdExpirydatePickerhdnForvaildate").val(datetext);
        //        datetext = datetext + " " + h + ":" + m + ":" + s;
        //        $('#txtCivilIdExpirydatePickerhdn').val(datetext);


        //        // $('#CivilIdExpirydatePicker').val(datetext);
        //    },
        //});
        //$('#TradeLicenseExpiryDate').datepicker({
        //    //   $('#txtdate').val('');
        //    dateFormat: 'yy-mm-dd',
        //    onSelect: function (datetext) {
        //        var d = new Date(); // for now
        //        var h = d.getHours();
        //        h = (h < 10) ? ("0" + h) : h;

        //        var m = d.getMinutes();
        //        m = (m < 10) ? ("0" + m) : m;

        //        var s = d.getSeconds();
        //        s = (s < 10) ? ("0" + s) : s;

        //        $("txtTradeLicenseExpiryDatePickerhdnForvaildate").val(datetext);
        //        datetext = datetext + " " + h + ":" + m + ":" + s;
        //        $('#txtTradeLicenseExpiryDatePickerhdn').val(datetext);

        //        // $('#TradeLicenseExpiryDate').val(datetext);
        //    },
        //});
        //$('#PassportExpirydatePicker').datepicker({
        //    //   $('#txtdate').val('');
        //    dateFormat: 'yy-mm-dd',
        //    onSelect: function (datetext) {
        //        var d = new Date(); // for now
        //        var h = d.getHours();
        //        h = (h < 10) ? ("0" + h) : h;

        //        var m = d.getMinutes();
        //        m = (m < 10) ? ("0" + m) : m;

        //        var s = d.getSeconds();
        //        s = (s < 10) ? ("0" + s) : s;

        //        $("txtPassportExpirydatePickerhdnForvaildate").val(datetext);
        //        datetext = datetext + " " + h + ":" + m + ":" + s;
        //        $('#txtPassportExpirydatePickerhdn').val(datetext);


        //        // $('#PassportExpirydatePicker').val(datetext);
        //    },
        //});

    }




    //var page_name = document.location.href;//.match(/[^\/]+$/)[0];
    //alert(page_name);

    if (page_name.indexOf("BrokerRenewal/BrokerRenewal") != -1) {
        //alert(page_name);
        callAjax();
    }


    if (page_name.indexOf("BrokerServiceRequestUpdates") != -1) {
        //alert(page_name);
        // alert('started');
        // callAjaxForBrokerServiceRequest();
        GetListOfAvailableEservices();
    }

    //alert(document.location.href.match(/[^\/]+$/)[0]);



    //var expressionCiviId = "^(1|2|3)((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(\d{5})$";

    //$("#brokerCivilId").keyup(function () {
    //    var VAL = this.value;

    //    var regex = new RegExp(expressionCiviId);

    //    if (regex.test(VAL))
    //    {
    //        $("#civilIdErrorMsg").css("display", "none");
    //    }
    //    else
    //    {
    //        $("#civilIdErrorMsg").css("display", "block");
    //    }
    //});


    //alert(page_name);
    if (page_name.indexOf("ClearingAgentExamRequest") != -1) {

        $("#Uploadedfileslist").on('click', '.mcbutton', function (e) {
            var whichtr = $(this).closest("tr");

            // Alert does not work
            whichtr.remove();
        });


        $("#sumbitExamRequest").click(function () {

            var brokerNameEnglishFirst = $("#brokerNameEnglishFirst").val();
            var brokerNameEnglishLast = $("#brokerNameEnglishLast").val();
            var brokerCivilId = $("#brokerCivilId").val();
            var brokerCivilIdexpiryDate = $("#brokerCivilIdexpiryDate").val();
            var brokerPassportNo = $("#brokerPassportNo").val();
            var brokerPassportExpiryDate = $("#brokerPassportExpiryDate").val();
            var brokerAddress = $("#brokerAddress").val();

            var brokerNameArabicFirst = $("#brokerNameArabicFirst").val();
            var brokerNameArabicSecond = $("#brokerNameArabicSecond").val();
            var brokerNameArabicThird = $("#brokerNameArabicThird").val();
            var brokerNameArabicLast = $("#brokerNameArabicLast").val();





            var brokerEmailID = $("#brokerEmailID").val();

            var brokerMobileNumber = $("#brokerMobileNumber").val();

            var brokertY = $("#brokertY").val();

            var nationality = $("#selectedCountryVal").val();


            // alert(" Abdul Karim  Sorry   " + brokertY);

            var valid = true;

            if (brokerNameEnglishFirst) {

                $("#firstnameErrorMsg").css("display", "none");
            }
            else {
                event.preventDefault();
                $("#firstnameErrorMsg").css("display", "block");
                valid = false;
            }

            if (brokerNameEnglishLast) {
                $("#lastnameErrorMsg").css("display", "none");
            }
            else {
                event.preventDefault();
                $("#lastnameErrorMsg").css("display", "block");
                valid = false;
            }

            if (brokerCivilId) {
                $("#civilIdErrorMsg").css("display", "none");
            }
            else {
                event.preventDefault();
                $("#civilIdErrorMsg").css("display", "block");
                valid = false;
            }

            if (brokerCivilIdexpiryDate)
            {
                $("#civilIdExpiryErrorMsg").css("display", "none");
                //var isDate = brokerCivilIdexpiryDate instanceof Date && !isNaN(brokerCivilIdexpiryDate.valueOf());

                //console.log(isDate); 

                //if (isDate) {
                //    $("#civilIdExpiryErrorMsg").css("display", "none");
                //}
                //else {
                //    event.preventDefault();
                //    $("#civilIdExpiryErrorMsg").css("display", "block");
                //    valid = false;
                //}

            }
            else {
                event.preventDefault();
                $("#civilIdExpiryErrorMsg").css("display", "block");
                valid = false;
            }



            if (nationality && nationality != 0) {
                $("#nationalityErrorMsg").css("display", "none");
            }
            else {
                event.preventDefault();
                $("#nationalityErrorMsg").css("display", "block");
                valid = false;
            }


            var regexEmail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            var emailTest = regexEmail.test(brokerEmailID);


            var regexCivilId = /^(1|2|3)((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(\d{5})$/;
            var civilIdTest = regexCivilId.test(brokerCivilId);

            //alert(emailTest);

            if (brokerCivilId && civilIdTest) {
                $("#civilIdErrorMsg").css("display", "none");
            }

            else {
                event.preventDefault();
                $("#civilIdErrorMsg").css("display", "block");
                valid = false;
            }

            if (brokerEmailID && emailTest) {
                $("#brokerEmailIDErrorMsg").css("display", "none");
            }

            else {
                event.preventDefault();
                $("#brokerEmailIDErrorMsg").css("display", "block");
                valid = false;
            }


            if (brokerMobileNumber) {
                $("#brokerMobileNumberErrorMsg").css("display", "none");
            }
            else {
                event.preventDefault();
                $("#brokerMobileNumberErrorMsg").css("display", "block");
                valid = false;
            }


            if (brokertY) {
                if (brokertY != "33224591") {
                    if (brokerPassportNo) {
                        $("#passportNoErrorMsg").css("display", "none");
                    }
                    else {
                        event.preventDefault();
                        $("#passportNoErrorMsg").css("display", "block");
                        valid = false;
                    }


                    if (brokerPassportExpiryDate)
                    {

                        $("#passportExpiryDateErrorMsg").css("display", "none");

                        //var isdate = brokerPassportExpiryDate instanceof Date && !isNaN(brokerPassportExpiryDate.valueOf())
                        //console.log(isdate);

                        //if (isdate) {
                        //    $("#passportExpiryDateErrorMsg").css("display", "none");
                        //}
                        //else {
                        //    event.preventDefault();
                        //    $("#passportExpiryDateErrorMsg").css("display", "block");
                        //    valid = false;
                        //}

                    }
                    else {
                        event.preventDefault();
                        $("#passportExpiryDateErrorMsg").css("display", "block");
                        valid = false;
                    }
                }
            }




            if (brokerAddress) {
                $("#addressErrorMsg").css("display", "none");
            }
            else {
                event.preventDefault();
                $("#addressErrorMsg").css("display", "block");
                valid = false;
            }

            if (brokerNameArabicFirst &&
                brokerNameArabicSecond &&
                brokerNameArabicThird &&
                brokerNameArabicLast) {
                $("#nameErrorMsg").css("display", "none");
            }
            else {
                event.preventDefault();
                $("#nameErrorMsg").css("display", "block");
                valid = false;
            }


            if (valid === true) {

                //var language = getCookie("culture");

                //var successHeader = "رسالة";
                //var successMsg = "تم ارسال طلبك بنجاح";

                //if (language.indexOf("en") != -1) {
                //    successHeader = "Message";
                //    successMsg = "Your Request has been Sent";
                //}


                //$("#modelHeaderTitle").text(successHeader);
                //$("#modalMessage").text(successMsg);


                ////$("#modalDiv").removeClass("animated hinge fast");

                //$("#modalDiv").css("display", "block");

                //setTimeout(function () {
                //    window.location.href = applicationUrl + 'Request/RequestListfortheUser';
                //}, 400);
            }

            else {
                $(".errorMsg").addClass("marginRightLeft10PX");
            }

        });
    }







    $("#closeError").click(function () {

        //$("#modalDiv").css("display", "none");
        //$("#modalDiv").addClass("animated hinge fast");
        setTimeout(function () {
            $("#modalDiv").css("display", "none");
        }, 200);

    });



    $("#closeModal").click(function () {

        $("#closeError").click();

    });



    var modal = document.getElementById('modalDiv');

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            //modal.style.display = "none";
            //$("#modalDiv").addClass("animated hinge slow");

            setTimeout(function () {
                $("#modalDiv").css("display", "none");
            }, 200);

        }

    }


});



$("#uploadWindow").click(function () {

    event.preventDefault();
    popitup('./uploadfiles.aspx');


});

$("#CloseBtn").click(function () {
    window.close();
});


$("#btnClearForm").click(function () {
    //alert("Hi");
    event.preventDefault();
    $('#licenceNo').val('');
    $('#status').val('');
    $('#civilIdNo').val('');
    $('#BrokerType').val('');
});



$("#btnSearchForm").click(function () {
    event.preventDefault();
    var licenceNo = $('#licenceNo').val();
    var status = $('#status').val();
    var civilIdNo = $('#civilIdNo').val();
    var BrokerType = $('#BrokerType').val();
});



function popitup(url) {

    newwindow = window.open(url, '_blank', 'height=600px,width=600px');
    if (window.focus) { newwindow.focus() }
    return false;
}


function callAjax() {

    var lgHidden = getCookie("culture");
    //var page_name = document.location.href;//.match(/[^\/]+$/)[0];
    var showInformation = "اظهار المعلومات";

    if (lgHidden.indexOf("en") != -1) {
        showInformation = "Show Information";
    }


    //EServiceRequestCreatedState   //Still NOT Paid
    //EServiceRequestSubmittedState   // Created and PAID
    //EServiceRequestCompletedState   //  renewal DONE
    //EServiceRequestProceedState    //Sent to MicroClear

    //EServiceRequestRejectedState  // Rejected

    var requestStatus =
        {
            "EServiceRequestCreatedStatear": "تم الانشاء",
            "EServiceRequestCreatedStateen": "Created",

            "EServiceRequestSubmittedStatear": "تم الارسال",
            "EServiceRequestSubmittedStateen": "Submitted",

            "EServiceRequestCompletedStatear": "تم التحديث",
            "EServiceRequestCompletedStateen": "Updated",

            "EServiceRequestAcceptedStatear": "تمت الموافقة",
            "EServiceRequestAcceptedStateen": "Approved",

            "EServiceRequestProceedStatear": "تم الانشاء",
            "EServiceRequestProceedStateen": "Created",

            "EServiceRequestRejectedStatear": "رُفض",
            "EServiceRequestRejectedStateen": "Rejected"

        };
    //getsubBrokers();
    //alert(applicationUrl + "BrokerRenewal/GetRequestDetails");
    $.ajax({
        type: "POST",
        cache: false, // Added for Security 
        // url: "/eservice/BrokerRenewal/GetRequestDetails",
        url: applicationUrl + "BrokerRenewal/GetRequestDetails", //"/BrokerRenewal/GetRequestDetails"
        data: JSON.stringify({ 'nothing': ' ' }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {


            if (result) {

                var obj = result;
                $.each(obj, function (index, element) {

                    var replaceRequestsubmissionDateTime = obj[index].RequestsubmissionDateTime.replace("/", "").replace("Date", "").replace("(", "").replace(")", "").replace("/", "");
                    var RequestSubmissionDate = new Date(obj[index].RequestsubmissionDateTime);//new Date(parseInt(replaceRequestsubmissionDateTime));

                    var dd = RequestSubmissionDate.getDate();
                    var mm = RequestSubmissionDate.getMonth() + 1; //January is 0!

                    var yyyy = RequestSubmissionDate.getFullYear();

                    // alert(dd + " " + mm + " " + yyyy); 

                    if (dd < 10) {
                        dd = '0' + dd;
                    }
                    if (mm < 10) {
                        mm = '0' + mm;
                    }


                    RequestSubmissionDate = dd + '/' + mm + '/' + yyyy;

                    // var targetPage = "/eservice/BrokerRenewal/BrokerUpdate?RequestNumber=" + obj[index].encryptedEServiceRequestNumber + "";
                    var targetPage = "/BrokerRenewal/BrokerUpdate?RequestNumber=" + obj[index].encryptedEServiceRequestNumber + "";

                    var stateId = obj[index].stateid;


                    var ind = stateId + lgHidden;

                    //alert(ind);

                    stateId = requestStatus[ind];

                    // alert(stateId);

                    var request =
                        '<div class="col-md-4 shadow-lg" >' +
                        '<div class="card text-white bg-info mb-3">' +
                        '<div class="card-header text-center bg-secondary">' +
                        '<ul class="nav nav-pills card-header-pills text-center flex-row-reverse">' +
                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="text-dark requestNoLargeFont">' +
                        obj[index].EServiceRequestNumber +
                        '</b>' +
                        '</a>' +
                        '</li>' +
                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="text-dark ">' +
                        //obj[index].RequestsubmissionDateTime +
                        RequestSubmissionDate +
                        '</b>' +
                        '</a>' +
                        '</li>' +

                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="text-dark requestNoLargeFont">' +
                        obj[index].EServiceRequestCreatedFor +
                        '</b>' +
                        '</a>' +
                        '</li>' +


                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="bg-light">' +
                        stateId +
                        '</b>' +
                        '</a>' +
                        '</li>' +



                        '</ul>' +
                        '<div class="row">' +
                        '<div class="col-md-6">' +
                        '<div class="col-md-12  ">' +
                        '<a class="btn btn-info btn-sm" style="font-family:inherit;" href="' + targetPage + '">' +
                        '<b>' +
                        '<i class="glyphicon glyphicon-arrow-left">' +
                        '</i>' +
                        '&nbsp;' +
                        '&nbsp;' +

                        showInformation +

                        '</b>' +
                        '</a>' +
                        '</div>' +
                        '</div>' +
                        '</div>'
                    '</div>' +
                        '</div>' +
                        '</div>';
                    $("#requests").append(request);
                });





            }


            getsubBrokers();


        }// End Success

    });
    // End Ajax Request


    function getsubBrokers() {


        var lgHidden = getCookie("culture");

        //var showInformation = "اظهار المعلومات";
        var showInformation = "طلب تجديد";
        var expiryDate = "تاريخ انتهائه";
        var bankGurantee = "ضمان بنكي";



        if (lgHidden.indexOf("en") != -1) {
            //showInformation = "Show Information";
            showInformation = "Request Renewal";
            expiryDate = "Expiry Date";
            bankGurantee = "Bank Guarantee";
        }

        $.ajax({
            type: "POST",
            cache: false, // Added for Security 
            // url: "/eservice/BrokerRenewal/GetBrokersDetails",
            url: applicationUrl + "BrokerRenewal/GetBrokersDetails",
            data: JSON.stringify({ 'nothing': ' ' }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            },
            success: function (result) {
                if (result) {
                    //  var obj = JSON.parse(result['d']);
                    var obj = result;
                    $.each(obj, function (index, element) {

                        //console.log(obj[index]);
                        //var replaceRequestsubmissionDateTime = obj[index].RequestsubmissionDateTime.replace("/", "").replace("Date", "").replace("(", "").replace(")", "").replace("/", "");
                        //var RequestSubmissionDate = new Date(parseInt(replaceRequestsubmissionDateTime));

                        //var dd = RequestSubmissionDate.getDate();
                        //var mm = RequestSubmissionDate.getMonth() + 1; //January is 0!

                        //var yyyy = RequestSubmissionDate.getFullYear();
                        //if (dd < 10) {
                        //    dd = '0' + dd;
                        //}
                        //if (mm < 10) {
                        //    mm = '0' + mm;
                        //}
                        //RequestSubmissionDate = dd + '/' + mm + '/' + yyyy;

                        // var targetPage = "/eservice/BrokerRenewal/BrokerUpdate?Orgid=" + obj[index].OrganizationId + "";
                        var targetPage = applicationUrl + "BrokerRenewal/BrokerUpdate?Orgid=" + obj[index].OrganizationId + "";

                        var brokerType = '';
                        var brokerStatus = obj[index].AccountStatus;
                        var bankGuaranteeNo = obj[index].BankGuaranteeNo;
                        var bankGuaranteeExpiryDate = obj[index].BankGuaranteeExpiryDatee;

                        if (lgHidden.indexOf("en") != -1) {
                            brokerType = obj[index].EseBrokerType;
                        }

                        else {
                            brokerType = obj[index].BrokerType;

                            if (brokerStatus == "ACTIVE") {
                                brokerStatus = "نشط";
                            }
                            else {
                                brokerStatus = "غير نشط";
                            }
                        }

                        //alert(bankGuaranteeNo);

                        if (bankGuaranteeNo == "NULL") {
                            bankGuaranteeNo = "---";
                        }

                        if (!bankGuaranteeExpiryDate || bankGuaranteeExpiryDate == "NULL") {
                            bankGuaranteeExpiryDate = "---";
                        }

                        var broker =
                            '<div class="col-md-6 shadow-lg" >' +
                            '<div class="card text-white bg-info mb-3">' +
                            '<div class="card-header text-center bg-secondary">' +
                            '<ul class="nav nav-pills card-header-pills text-center flex-row-reverse">' +
                            '<li class="nav-item ">' +
                            '<a class="nav-link text-light" href="#">' +
                            '<b class="text-dark requestNoLargeFont">' +
                            obj[index].BrokerName +
                            '</b>' +
                            '</a>' +
                            '</li>' +
                            '</ul>' +
                            '<ul class="nav nav-pills card-header-pills text-center flex-row-reverse">' +
                            '<li class="nav-item  flex-row-reverse">' +
                            '<a class="nav-link text-light" href="#">' +
                            '<b class="text-dark">' +
                            obj[index].TradeLicenseNumber +
                            '&nbsp;' +
                            '  ' +
                            brokerType +
                            '<br/>' +
                            '</b>' +
                            '</a>' +
                            '</li>' +
                            '</ul>' +
                            '<b>' +
                            '</b>' +
                            '<b class="text-center">' +
                            '</b>' +
                            '</div>' +
                            '<div class="card-body bg-light">' +
                            '<div class="row">' +
                            '<div class="col-md-12 ">' +
                            //'<div class="row">' +
                            //'<div class="col-md-12 ">' +
                            //'<p class=" text-dark">' +
                            //'<b>' +
                            //expiryDate +
                            //'<br/>' +
                            //'</b>' +
                            //obj[index].DtOfExpiry +
                            //'<br/>' +
                            //'</p>' +
                            //'</div>' +
                            //'</div>' +
                            '<div class="row">' +
                            '<div class="col-md-12 ">' +
                            '<p class="">' +
                            '<b class="text-dark">' +
                            bankGurantee +
                            '<br/>' +
                            //obj[index].BankGuaranteeNo +
                            bankGuaranteeNo +
                            '<br/>' +
                            '</b>' +
                            '</p>' +
                            '<p class="w-100  text-dark">' +
                            '<b>' +
                            expiryDate +
                            '<br/>' +
                            '</b>' +
                            //obj[index].BankGuaranteeExpiryDate +
                            bankGuaranteeExpiryDate +

                            '<br />' +
                            '</p>' +
                            '<div class="row">' +
                            '<div class="col-md-6">' +
                            '<div class="col-md-12 ">' +
                            '<a class="btn btn-info btn-sm" style="font-family:inherit;" href="' + targetPage + '">' +
                            '<b>' +
                            '<i class="glyphicon glyphicon-arrow-left">' +
                            '</i>' +
                            '&nbsp;' +
                            '&nbsp;' +

                            showInformation +

                            '</b>' +
                            '</a>' +
                            '</div>' +
                            '</div>' +
                            '<div class="col-md-6">' +
                            '<h5 class="">' +
                            '<b>' +
                            '<p class="bg-light">' +
                            brokerStatus +
                            '</p>' +
                            '</b>' +
                            '</h5>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>';
                        $("#brokers").append(broker);

                    });
                }


            }// End Success
        });  // End Ajax Request
    }
}




function callAjaxForBrokerServiceRequest() {


    var lgHidden = getCookie("culture");


    var showInformation = "اظهار المعلومات";

    if (lgHidden.indexOf("en") != -1) {
        showInformation = "Show Information";
    }


    //EServiceRequestCreatedState   //Still NOT Paid
    //EServiceRequestSubmittedState   // Created and PAID
    //EServiceRequestCompletedState   //  renewal DONE
    //EServiceRequestProceedState    //Sent to MicroClear

    //EServiceRequestRejectedState  // Rejected

    var requestStatus =
        {
            "EServiceRequestCreatedStateAra": "تم الانشاء",
            "EServiceRequestCreatedStateEng": "Created",

            "EServiceRequestSubmittedStateAra": "تم الارسال",
            "EServiceRequestSubmittedStateEng": "Submitted",

            "EServiceRequestCompletedStateAra": "تم التحديث",
            "EServiceRequestCompletedStateEng": "Updated",

            "EServiceRequestProceedStateAra": "بأنتظار الموافقة",
            "EServiceRequestProceedStateEng": "Pending",

            "EServiceRequestRejectedStateAra": "رُفض",
            "EServiceRequestRejectedStateEng": "Rejected"

        };


    $.ajax({
        type: "POST",
        cache: false, // Added for Security 
        // url: "/eservice/BrokerRenewal/GetBrokerSubmissionRequests",
        url: applicationUrl + "BrokerRenewal/GetBrokerSubmissionRequests",
        data: JSON.stringify({ 'nothing': ' ' }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {


            if (result) {

                var obj = result;
                $.each(obj, function (index, element) {

                    //var replaceRequestsubmissionDateTime = obj[index].RequestsubmissionDateTime.replace("/", "").replace("Date", "").replace("(", "").replace(")", "").replace("/", "");
                    //var RequestSubmissionDate = new Date(obj[index].RequestsubmissionDateTime);//new Date(parseInt(replaceRequestsubmissionDateTime));

                    //var dd = RequestSubmissionDate.getDate();
                    //var mm = RequestSubmissionDate.getMonth() + 1; //January is 0!

                    //var yyyy = RequestSubmissionDate.getFullYear();

                    //// alert(dd + " " + mm + " " + yyyy); 

                    //if (dd < 10) {
                    //    dd = '0' + dd;
                    //}
                    //if (mm < 10) {
                    //    mm = '0' + mm;
                    //}


                    //RequestSubmissionDate = dd + '/' + mm + '/' + yyyy;

                    //var targetPage = "/BrokerRenewal/BrokerUpdate?RequestNumber=" + obj[index].encryptedEServiceRequestNumber + "";

                    //var stateId = obj[index].stateid;


                    //var ind = stateId + lgHidden;

                    ////alert(ind);

                    //stateId = requestStatus[ind];

                    // alert(stateId);

                    var request =
                        '<div class="col-md-4 shadow-lg" >' +
                        '<div class="card text-white bg-info mb-3">' +
                        '<div class="card-header text-center bg-secondary">' +
                        '<ul class="nav nav-pills card-header-pills text-center flex-row-reverse">' +
                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="text-dark requestNoLargeFont">' +
                        obj[index].EServiceRequestNumber +
                        '</b>' +
                        '</a>' +
                        '</li>' +
                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="text-dark ">' +
                        //obj[index].RequestsubmissionDateTime +
                        obj[index].servicename +
                        '</b>' +
                        '</a>' +
                        '</li>' +

                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="text-dark requestNoLargeFont">' +
                        //  obj[index].Serviceid +
                        '<input type="checkbox" onclick="CollectServiceIdvalues(this)" name= "' + obj[index].Serviceid + '" checked disabled class="dynamiccheckbox" id= "' + obj[index].Serviceid + '" />' +
                        '</b>' +
                        '</a>' +
                        '</li>' +


                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="bg-light">' +
                        obj[index].requestdetailsstateid +
                        '</b>' +
                        '</a>' +
                        '</li>' +



                        '</ul>' +
                        //'<div class="row">' +
                        //'<div class="col-md-6">' +
                        //'<div class="col-md-12 ">' +
                        //'<a class="btn btn-info btn-sm" style="font-family:inherit;" href="_blank">' +
                        //'<b>' +
                        //'<i class="glyphicon glyphicon-arrow-left">' +
                        //'</i>' +
                        //'&nbsp;' +
                        //'&nbsp;' +

                        //showInformation +

                        //'</b>' +
                        //'</a>' +
                        //'</div>' +
                        //'</div>' +
                        //'</div>'
                        '</div>' +
                        '</div>' +
                        '</div>';
                    $("#requests").append(request);
                });





            }


            GetListOfAvailableEservice();


        }// End Success

    });
    // End Ajax Request



    function GetListOfAvailableEservice() {


        var lgHidden = getCookie("culture");

        var showInformation = "اظهار المعلومات";
        var expiryDate = "تاريخ انتهائه";
        var bankGurantee = "ضمان بنكي";



        if (lgHidden.indexOf("en") != -1) {
            showInformation = "Show Information";
            expiryDate = "Expiry Date";
            bankGurantee = "Bank Guarantee";
        }

        $.ajax({
            type: "POST",
            cache: false, // Added for Security 
            // url: "/eservice/BrokerRenewal/GetListOfAvailableEservices",
            url: applicationUrl + "BrokerRenewal/GetListOfAvailableEservices",
            data: JSON.stringify({ 'nothing': ' ' }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            },
            success: function (result) {


                if (result) {

                    var obj = result;
                    //  $("#brokers").innerHTML('');

                    document.getElementById('brokers').innerHTML = "";
                    $.each(obj, function (index, element) {

                        //var replaceRequestsubmissionDateTime = obj[index].RequestsubmissionDateTime.replace("/", "").replace("Date", "").replace("(", "").replace(")", "").replace("/", "");
                        //var RequestSubmissionDate = new Date(obj[index].RequestsubmissionDateTime);//new Date(parseInt(replaceRequestsubmissionDateTime));

                        //var dd = RequestSubmissionDate.getDate();
                        //var mm = RequestSubmissionDate.getMonth() + 1; //January is 0!

                        //var yyyy = RequestSubmissionDate.getFullYear();

                        //// alert(dd + " " + mm + " " + yyyy); 

                        //if (dd < 10) {
                        //    dd = '0' + dd;
                        //}
                        //if (mm < 10) {
                        //    mm = '0' + mm;
                        //}


                        //RequestSubmissionDate = dd + '/' + mm + '/' + yyyy;

                        //var targetPage = "/BrokerRenewal/BrokerUpdate?RequestNumber=" + obj[index].encryptedEServiceRequestNumber + "";

                        //var stateId = obj[index].stateid;


                        //var ind = stateId + lgHidden;

                        ////alert(ind);

                        //stateId = requestStatus[ind];

                        // alert(stateId);

                        var request =
                            '<div class="col-md-4 shadow-lg" >' +
                            '<div class="card text-white bg-info mb-3">' +
                            '<div class="card-header text-center bg-secondary">' +
                            '<ul class="nav nav-pills card-header-pills text-center flex-row-reverse">' +
                            '<li class="nav-item ">' +
                            '<a class="nav-link text-light" href="#">' +
                            '<b class="text-dark requestNoLargeFont">' +
                            obj[index].EServiceRequestNumber +
                            '</b>' +
                            '</a>' +
                            '</li>' +
                            '<li class="nav-item ">' +
                            '<a class="nav-link text-light" href="#">' +
                            '<b class="text-dark ">' +
                            //obj[index].RequestsubmissionDateTime +
                            obj[index].servicename +
                            '</b>' +
                            '</a>' +
                            '</li>' +

                            '<li class="nav-item ">' +
                            '<a class="nav-link text-light" href="#">' +
                            '<b class="text-dark requestNoLargeFont">' +
                            //  obj[index].Serviceid +
                            '<input type="checkbox" onclick="CollectServiceIdvalues(this)" runat="server"  name= "' + obj[index].Serviceid + '" class="dynamiccheckbox" id= "' + obj[index].Serviceid + '" />' +
                            '</b>' +
                            '</a>' +
                            '</li>' +


                            '<li class="nav-item ">' +
                            '<a class="nav-link text-light" href="#">' +
                            '<b class="bg-light">' +
                            obj[index].requestdetailsstateid +
                            '</b>' +
                            '</a>' +
                            '</li>' +



                            '</ul>' +
                            //'<div class="row">' +
                            //'<div class="col-md-6">' +
                            //'<div class="col-md-12 ">' +
                            //'<a class="btn btn-info btn-sm" style="font-family:inherit;" href="_blank">' +
                            //'<b>' +
                            //'<i class="glyphicon glyphicon-arrow-left">' +
                            //'</i>' +
                            //'&nbsp;' +
                            //'&nbsp;' +

                            //showInformation +

                            //'</b>' +
                            //'</a>' +
                            //'</div>' +
                            //'</div>' +
                            //'</div>'
                            '</div>' +
                            '</div>' +
                            '</div>';
                        $("#brokers").append(request);
                    });

                }




            }// End Success

        });

    }


    function getsubBrokers() {


        var lgHidden = getCookie("culture");

        var showInformation = "اظهار المعلومات";
        var expiryDate = "تاريخ انتهائه";
        var bankGurantee = "ضمان بنكي";



        if (lgHidden.indexOf("en") != -1) {
            showInformation = "Show Information";
            expiryDate = "Expiry Date";
            bankGurantee = "Bank Guarantee";
        }

        $.ajax({
            type: "POST",
            cache: false, // Added for Security 
            // url: "/eservice/BrokerRenewal/GetBrokersDetails",
            url: "/BrokerRenewal/GetBrokersDetails",
            data: JSON.stringify({ 'nothing': ' ' }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            },
            success: function (result) {
                if (result) {
                    //  var obj = JSON.parse(result['d']);
                    var obj = result;


                    $.each(obj, function (index, element) {

                        //var replaceRequestsubmissionDateTime = obj[index].RequestsubmissionDateTime.replace("/", "").replace("Date", "").replace("(", "").replace(")", "").replace("/", "");
                        //var RequestSubmissionDate = new Date(parseInt(replaceRequestsubmissionDateTime));

                        //var dd = RequestSubmissionDate.getDate();
                        //var mm = RequestSubmissionDate.getMonth() + 1; //January is 0!

                        //var yyyy = RequestSubmissionDate.getFullYear();
                        //if (dd < 10) {
                        //    dd = '0' + dd;
                        //}
                        //if (mm < 10) {
                        //    mm = '0' + mm;
                        //}
                        //RequestSubmissionDate = dd + '/' + mm + '/' + yyyy;

                        var targetPage = "/BrokerRenewal/BrokerUpdate?Orgid=" + obj[index].OrganizationId + "";

                        var brokerType = '';
                        var brokerStatus = obj[index].AccountStatus;
                        var bankGuaranteeNo = obj[index].BankGuaranteeNo;
                        var bankGuaranteeExpiryDate = obj[index].BankGuaranteeExpiryDate;

                        if (lgHidden.indexOf("en") != -1) {
                            brokerType = obj[index].EseBrokerType;
                        }

                        else {
                            brokerType = obj[index].BrokerType;

                            if (brokerStatus == "ACTIVE") {
                                brokerStatus = "نشط";
                            }
                            else {
                                brokerStatus = "غير نشط";
                            }
                        }

                        //alert(bankGuaranteeNo);

                        if (bankGuaranteeNo == "NULL") {
                            bankGuaranteeNo = "---";
                        }

                        if (!bankGuaranteeExpiryDate || bankGuaranteeExpiryDate == "NULL") {
                            bankGuaranteeExpiryDate = "---";
                        }

                        var broker =
                            '<div class="col-md-4 shadow-lg" >' +
                            '<div class="card text-white bg-info mb-3">' +
                            '<div class="card-header text-center bg-secondary">' +
                            '<ul class="nav nav-pills card-header-pills text-center flex-row-reverse">' +
                            '<li class="nav-item ">' +
                            '<a class="nav-link text-light" href="#">' +
                            '<b class="text-dark requestNoLargeFont">' +
                            obj[index].BrokerName +
                            '</b>' +
                            '</a>' +
                            '</li>' +
                            '</ul>' +
                            '<ul class="nav nav-pills card-header-pills text-center flex-row-reverse">' +
                            '<li class="nav-item  flex-row-reverse">' +
                            '<a class="nav-link text-light" href="#">' +
                            '<b class="text-dark">' +
                            obj[index].TradeLicenseNumber +
                            '&nbsp;' +
                            '  ' +
                            brokerType +
                            '<br/>' +
                            '</b>' +
                            '</a>' +
                            '</li>' +
                            '</ul>' +
                            '<b>' +
                            '</b>' +
                            '<b class="text-center">' +
                            '</b>' +
                            '</div>' +
                            '<div class="card-body bg-light">' +
                            '<div class="row">' +
                            '<div class="col-md-12 ">' +
                            '<div class="row">' +
                            '<div class="col-md-12 ">' +
                            '<p class=" text-dark">' +
                            '<b>' +
                            expiryDate +
                            '<br/>' +
                            '</b>' +
                            obj[index].DtOfExpiry +
                            '<br/>' +
                            '</p>' +
                            '</div>' +
                            '</div>' +
                            '<div class="row">' +
                            '<div class="col-md-12 ">' +
                            '<p class="">' +
                            '<b class="text-dark">' +
                            bankGurantee +
                            '<br/>' +
                            //obj[index].BankGuaranteeNo +
                            bankGuaranteeNo +
                            '<br/>' +
                            '</b>' +
                            '</p>' +
                            '<p class="w-100  text-dark">' +
                            '<b>' +
                            expiryDate +
                            '<br/>' +
                            '</b>' +
                            //obj[index].BankGuaranteeExpiryDate +
                            bankGuaranteeExpiryDate +

                            '<br />' +
                            '</p>' +
                            '<div class="row">' +
                            '<div class="col-md-6">' +
                            '<div class="col-md-12  ">' +
                            '<a class="btn btn-info btn-sm" style="font-family:inherit;" href="' + targetPage + '">' +
                            '<b>' +
                            '<i class="glyphicon glyphicon-arrow-left">' +
                            '</i>' +
                            '&nbsp;' +
                            '&nbsp;' +

                            showInformation +

                            '</b>' +
                            '</a>' +
                            '</div>' +
                            '</div>' +
                            '<div class="col-md-6">' +
                            '<h5 class="">' +
                            '<b>' +
                            '<p class="bg-light">' +
                            brokerStatus +
                            '</p>' +
                            '</b>' +
                            '</h5>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>';
                        $("#brokers").append(broker);
                    });
                }


            }// End Success
        });  // End Ajax Request
    }
}

var uniqueArray;

var uniqueArray1;
var checkselected = new Array();

var checkUnselected = new Array();
function CollectServiceIdvalues(elem) {

    //console.log(checkselected);
    //console.log(checkUnselected);

    if (elem.checked) {

        checkselected.push(elem.name);

    }
    else {

        checkUnselected.push(elem.name);

    }




}



function GetListOfAvailableEservices() {


    //alert('GetListOfAvailableEservices');


    var lgHidden = getCookie("culture");


    //var showInformation = "اظهار المعلومات";
    //var expiryDate = "تاريخ انتهائه";
    //var bankGurantee = "ضمان بنكي";


    //if (lgHidden.indexOf("en") != -1) {
    //    showInformation = "Show Information";
    //    expiryDate = "Expiry Date";
    //    bankGurantee = "Bank Guarantee";
    //}

    $.ajax({
        type: "POST",
        cache: false, // Added for Security 
        // url: "/eservice/BrokerRenewal/GetListOfAvailableEservices",
        url: applicationUrl + "BrokerRenewal/GetListOfAvailableEservices",
        data: JSON.stringify({ 'nothing': ' ' }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {

            //alert('fsfg');
            console.log(result);
            console.log(result.length);
            if (result) {

                var obj = result;
                //console.log(obj);
                //  $("#brokers").innerHTML('');

                document.getElementById('brokers').innerHTML = "";
                $.each(obj, function (index, element) {

                    //var replaceRequestsubmissionDateTime = obj[index].RequestsubmissionDateTime.replace("/", "").replace("Date", "").replace("(", "").replace(")", "").replace("/", "");
                    //var RequestSubmissionDate = new Date(obj[index].RequestsubmissionDateTime);//new Date(parseInt(replaceRequestsubmissionDateTime));

                    //var dd = RequestSubmissionDate.getDate();
                    //var mm = RequestSubmissionDate.getMonth() + 1; //January is 0!

                    //var yyyy = RequestSubmissionDate.getFullYear();

                    //// alert(dd + " " + mm + " " + yyyy); 

                    //if (dd < 10) {
                    //    dd = '0' + dd;
                    //}
                    //if (mm < 10) {
                    //    mm = '0' + mm;
                    //}
                    //RequestSubmissionDate = dd + '/' + mm + '/' + yyyy;

                    //var targetPage = "/BrokerRenewal/BrokerUpdate?RequestNumber=" + obj[index].encryptedEServiceRequestNumber + "";

                    //var stateId = obj[index].stateid;


                    //var ind = stateId + lgHidden;

                    ////alert(ind);

                    //stateId = requestStatus[ind];

                    // alert(stateId);

                    var serviceNametoDisplay = '';
                    var serviceState = '';

                    if (lgHidden.indexOf("en") != -1) {
                        serviceNametoDisplay = obj[index].servicename;
                        serviceState = obj[index].requestdetailsstateid;
                    }

                    else {
                        serviceNametoDisplay = obj[index].ServiceNameAra;
                        serviceState = 'متاحة لطلب الاشتراك';
                    }

                    var request =
                        '<div class="col-md-6 shadow-lg" >' +
                        '<div class="card text-white bg-info mb-3">' +
                        '<div class="card-header text-center bg-secondary">' +
                        '<ul class="nav nav-pills card-header-pills text-center flex-row-reverse">' +
                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="text-dark requestNoLargeFont">' +
                        obj[index].EServiceRequestNumber +
                        '</b>' +
                        '</a>' +
                        '</li>' +
                        '<li class="nav-item ">' +
                        '<a class="nav-link text-light" href="#">' +
                        '<b class="text-dark ">' +
                        //obj[index].RequestsubmissionDateTime +
                        //obj[index].servicename +
                        serviceNametoDisplay +
                        '</b>' +
                        '</a>' +
                        '</li>' +

                        '<li class="nav-item ">' +
                        //'<a class="nav-link text-light" href="#">' +
                        '<b class="text-dark requestNoLargeFont">' +
                        //  obj[index].Serviceid +
                        '<input type="checkbox" onclick="CollectServiceIdvalues(this)" runat="server"  name= "' + obj[index].Serviceid + '" class="dynamiccheckbox" id= "' + obj[index].Serviceid + '" />' +
                        '</b>' +
                        //'</a>' +
                        '</li>' +


                        '<li class="nav-item ">' +
                        '<p class="nav-link text-light" href="#">' +
                        '<b class="bg-light">' +
                        //obj[index].requestdetailsstateid +
                        serviceState +
                        '</b>' +
                        '</p>' +
                        '</li>' +



                        '</ul>' +
                        //'<div class="row">' +
                        //'<div class="col-md-6">' +
                        //'<div class="col-md-12 ">' +
                        //'<a class="btn btn-info btn-sm" style="font-family:inherit;" href="_blank">' +
                        //'<b>' +
                        //'<i class="glyphicon glyphicon-arrow-left">' +
                        //'</i>' +
                        //'&nbsp;' +
                        //'&nbsp;' +

                        //showInformation +

                        //'</b>' +
                        //'</a>' +
                        //'</div>' +
                        //'</div>' +
                        //'</div>'
                        '</div>' +
                        '</div>' +
                        '</div>';
                    $("#brokers").append(request);
                });

            }


            //if (result.length===0)
            //{
            //    console.log('else');
            if (!document.getElementById("brokers").hasChildNodes()) {
                //alert('yes');
                document.getElementById("brokers").classList.add("text-center");
                console.log(ServicesNotAvailableToSubscribe);
                document.getElementById("brokers").innerHTML = ServicesNotAvailableToSubscribe;
                //alert('');
                //document.getElementById("postServiceid").remove();
                document.getElementById("postServiceid").setAttribute('style', 'display:none');
            }
            else {

                document.getElementById("postServiceid").removeAttribute("style");
            }
            //}

            //  getsubBrokers();


        }// End Success

    });

}




$('#postServiceid').click(function () {
    //  CollectServiceIdvalues();
    //alert('started');

    if (checkselected.length == 0 && checkUnselected.length == 0) {
        alert('Please select Any Operation ');
        return;
    }


    uniqueArray = checkselected.filter(function (elem, pos) {
        return checkselected.indexOf(elem) == pos;
    });
    console.log(uniqueArray);
    var uniqueArrayj = uniqueArray.join(',');
    console.log(uniqueArrayj);
    //document.getElementById('SelectedFileId').value = uniqueArrayj;// uniqueArray.join(',');

    uniqueArray1 = checkUnselected.filter(function (elem, pos) {
        return checkselected.indexOf(elem) == pos;
    });
    console.log(uniqueArray1);
    var uniqueArray1j = uniqueArray1.join(',');
    console.log(uniqueArray1j);

    //document.getElementById('UnSelectedFileId').value = uniqueArray1j;// uniqueArray1.join(',');

    var SelectedFileId = uniqueArrayj;// document.getElementById('SelectedFileId').value;

    var UnSelectedFileId = uniqueArray1j;// document.getElementById('UnSelectedFileId').value;

    // alert('callingapi');

    $.ajax({
        type: "POST",
        cache: false, // Added for Security 
        // switch over after hosting 
        //    url: "/ScanUploadServiceAugpp/Home/DeleteFile",     
        //url: "/ScanUploadServiceAugpp/Home/DeleteFile",
        // url: "/eservice/BrokerRenewal/BrokerServiceRequestUpdatesPost",
        url: applicationUrl + "BrokerRenewal/BrokerServiceRequestUpdatesPost",
        data: { 'SelectedFileId': SelectedFileId, 'UnSelectedFileId': UnSelectedFileId },
        // processData: true,
        success: function (response) {
            //   $("#UploadForm").submit();
            //  alert(message);
            //    $('#headercheck').prop('checked', false);
            // $("#requests").html('');

            checkUnselected.length = 0

            checkselected.length = 0
            // document.getElementById('requests').innerHTML = "";
            document.getElementById('brokers').innerHTML = "";
            //callAjaxForBrokerServiceRequest();
            GetListOfAvailableEservices();
            //$("#brokers")

            //$("#modelHeaderTitle").text(fileErrorHeaderMSG);
            $("#modalMessage").text(Subscribed);

            //$("#modalDiv").removeClass("animated hinge fast");
            setTimeout(function () {
                $("#modalDiv").css("display", "block");
            }, 200);


            //alert(Subscribed);//'Succesfully subscribed to selected services');

            //if (!document.getElementById("brokers").hasChildNodes()) {
            //    //alert('yes');
            //    document.getElementById("brokers").classList.add("text-center");
            //    console.log(ServicesNotAvailableToSubscribe);
            //    document.getElementById("brokers").innerHTML = ServicesNotAvailableToSubscribe;
            //    //alert('');
            //    //document.getElementById("postServiceid").remove();
            //    document.getElementById("postServiceid").setAttribute( 'style': 'display:none' );
            //}
            //else {

            //    document.getElementById("postServiceid").removeAttribute("style");
            //}
        },
        failure: function (response) {
            alert(SomeIssuehasoccured);//response.responseText);
        },
        error: function (response) {
            alert(SomeIssuehasoccured);//response.responseText);
        }
    });
});



$("#requestListDivExpand").click(function () {

    if ($("#requestListDiv").css('display') == 'none') {

        $("#arrowUpp").css("display", "none");
        $("#arrowDownn").css("display", "inline-block")

        $("#showInformationlbl").css("display", "none");
        $("#hideInformationlbl").css("display", "inline-block");
    }

    else {
        $("#arrowUpp").css("display", "inline-block");
        $("#arrowDownn").css("display", "none")

        $("#showInformationlbl").css("display", "inline-block");
        $("#hideInformationlbl").css("display", "none");
    }

    $("#requestListDiv").slideToggle("slow", function () {
        // Animation complete.
    });
});


$("#brokersListDivExpand").click(function () {


    if ($("#brokersListDiv").css('display') == 'none') {

        $("#arrowUp").css("display", "none");
        $("#arrowDown").css("display", "inline-block")
    }

    else {
        $("#arrowUp").css("display", "inline-block");
        $("#arrowDown").css("display", "none")
    }

    $("#brokersListDiv").slideToggle("slow", function () {

        // Animation complete.
    });
});


$("#expandSearch").click(function () {


    if ($("#firstSectionSearch").css('display') == 'none') {

        $("#arrowUppp").css("display", "none");
        $("#arrowDownnn").css("display", "inline-block")
    }

    else {
        $("#arrowUppp").css("display", "inline-block");
        $("#arrowDownnn").css("display", "none")
    }

    $("#firstSectionSearch").slideToggle("slow", function () {
        // Animation complete.
    });


    $("#secondSectionSearch").slideToggle("slow", function () {
        // Animation complete.
    });

    $("#thirdSectionSearch").slideToggle("slow", function () {
        // Animation complete.
    });



});


$("#loginBtn").click(function () {


    if (!validateForm()) {
        event.preventDefault();
    }


});


$("#linkToBrokersManagement").click(function () {

    parent.history.back();
    return false;

});









function validateForm() {

    var userName = $("#Username").val();
    var password = $("#password").val();
    var valid = false;

    if (!userName) {
        event.preventDefault();
        $("#Username").removeClass("animated bounce slow");

        $("#userNameErrorMsg").css("display", "block");
        valid = false;
        setTimeout(function () {
            $("#Username").addClass("animated bounce slow");
        }, 200);


    }
    else {
        $("#Username").removeClass("animated bounce slow");
        $("#userNameErrorMsg").css("display", "none");
        valid = true;
    }
    if (!password) {
        event.preventDefault();
        $("#password").removeClass("animated bounce slow");

        $("#PasswordErrorMsg").css("display", "block");
        valid = false;
        setTimeout(function () {
            $("#password").addClass("animated bounce slow");
        }, 200);


    }
    else {
        $("#password").removeClass("animated bounce slow");
        $("#PasswordErrorMsg").css("display", "none");
        valid = true;
    }

    return valid;
}



//forBrokerUpdatesUploadform 
page_name = document.location.href;

if (page_name.indexOf("BrokerRenewal/BrokerUpdate") != -1 ||
    page_name.indexOf("ClearingAgentExamRequest") != -1 ||
    page_name.indexOf("UploadDocument") != -1) {
    //if (page_name.indexOf("BrokerRenewal/BrokerUpdate") != -1 || page_name.indexOf("ClearingAgentExamRequest") != -1) {

    // $('.select2').select2();

    var lgHidden = getCookie("culture");

    var DropdownAlert;
    // var DateAlert;
    if (lgHidden.indexOf("en") != -1) {
        DropdownAlert = 'No results Found';
        //  DateAlert = 'selected date Should Be Greater than Todays'
    }
    else {
        DropdownAlert = 'لا توجد نتائج';
        // DateAlert = 'يجب أن يكون التاريخ المحدد أكبر من اليوم ';
    }


    $('select').select2({
        language: {
            noResults: function (params) {
                return DropdownAlert;
            }
        }
    });

    var UploadDocsURL = '@ViewBag.UploadDocsURL';
    var UploadDocs_actioncall = '@ViewBag.UploadDocs_actioncall';
    var ViewBagmodelstatus = '@ViewBag.modelerror';
    var result = '@ViewBag.dis';
    if (result === null) {
        result = '';
    }
    var Tid = '@ViewBag.TokenId';
    var uid = '@ViewBag.UserId';
    var reqid1 = '@ViewBag.reqid1';
    var UploadDocument_align_style = '@Resources.Resource.UploadDocument_align_style';

    $(document).ready(function () {
        $("#Archive").click(function () {
            // $("#Uploadedfileslist").html('');
            var message = '';
            var HostedLocation = location.pathname.split('/')[1];
            var delteselected = new Array();



            var count = 0;
            $('#Uploadedfileslist').find('input.deleteselctedcheckbox:checkbox:checked').each(function () {


                {
                    //   alert($(this).attr('name'));
                    delteselected.push($(this).attr('name'));
                    count++;
                    //$(this).closest('tr').remove();

                }
            });
            if ($("#Uploadedfileslist").find(".deleteselctedcheckbox:checkbox:checked").text().trim() == "") {
                $('#headercheck').prop('checked', false);
            }
            if ($("#Uploadedfileslist").find("input:checkbox:not(:checked)")) {
                $('#headercheck').prop('checked', false);

            }


            {
                //   var getlanguageforalert = document.getElementById('languageAlert').value;
                document.getElementById('SelectedFileId').value = delteselected.join('|');

                var dataItem = delteselected.join(',');


            }
            if (dataItem != "") {
                $.ajax({
                    type: "POST",
                    cache: false, // Added for Security 
                    url: "/Home/MoveToArchive",
                    data: { 'dataItem': dataItem },
                    success: function (response) {
                        if (response.Status == "Success") {
                            alert(message);
                            //Popup(message);
                            $("#UploadForm").submit();
                            //location.reload();
                        }
                        else if (response.Status == "Failed") {
                            alert(DuplicateFailuremsg);
                            //Popup(DuplicateFailuremsg);
                            $("#UploadForm").submit();
                        }
                        else {
                            alert(errormsg);//  alert('Some error has occured');
                            //Popup(errormsg);
                            $("#UploadForm").submit();
                        }
                    },
                    failure: function (response) {
                        alert(errormsg);//alert(response.responseText);
                        //Popup(errormsg);
                    },
                    error: function (response) {
                        alert(errormsg);//alert(response.responseText);
                        //Popup(errormsg);
                    }
                });

            }
            return false;
        });


        $("#fileUploadCtrl").fileinput({
            browseClass: "btn btn-primary btn-block",
            showCaption: false,
            showRemove: false,
            showUpload: false,
            showZoom: false,
            maxFileSize: 500,
            maxFilesNum: 1,
            showUpload: false,
            allowedFileExtensions: ['jpg', 'jpeg', 'png', 'pdf'],
            language: "ar",
            browseOnZoneClick: true

        });
        //$.fn.dataTable.ext.pager.numbers_length = 5;


        $(".fileinput-cancel-button").css("display", "none");

        la = getCookie("culture");
        var table = '';

        //alert(la);

        if (la.indexOf("ar") != -1) {
            table = $('#Uploadedfileslist').dataTable({
                "language":
                {
                    "url": applicationUrl + "/scripts/arabic.json"
                },
                "searching": false,
                "bFilter": false,
                "bInfo": false,
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                "bPaginate": true,
                "bLengthChange": false,
                'sPaginationType': 'ellipses',
                "bFilter": false,
                "bInfo": false,
                //  "scrollY": "true",
                //  "scrollCollapse": true,
                "pageLength": 5,
                'pagingType': 'numbers',
                "info": false

            });
        }

        else {
            table = $('#Uploadedfileslist').dataTable({
                "oLanguage":
                {
                    "infoEmpty": ""
                },
                "searching": false,
                "bFilter": false,
                "bInfo": false,
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                "bPaginate": true,
                "bLengthChange": false,
                'sPaginationType': 'ellipses',
                "bFilter": false,
                "bInfo": false,
                //  "scrollY": "true",
                //  "scrollCollapse": true,
                "pageLength": 5,
                'pagingType': 'numbers',
                "info": false

            });
        }

    });

    page_name = document.location.href;

    if (page_name.indexOf("BrokerRenewal/BrokerUpdate") != -1 ||
        page_name.indexOf("ClearingAgentExamRequest") != -1 ||
        page_name.indexOf("UploadDocument") != -1) {
        if ($("#ddlDocumentTypes").length) {
            $("#ddlDocumentTypes").change(function () {
                // var selectedValue = $(this).val();
                $("#NewFilename").val($(this).find("option:selected").text())
            });
            //  $("#ddlDocumentTypes option:contains('(1)')").remove();

        }

    }


    //function up(pageName) {

    //    // var UploadDocsURL = 'http://10.10.26.226/API/api/Docs/Upload';
    //    // 'http://localhost/ETradeAPI/api/Docs/Upload';

    //    var UploadDocsURL = 'http://10.10.26.226/API/api/Docs/Upload';

    //  //  var UploadDocsURL = 'http://localhost/ETradeAPI/api/Docs/Upload';
    //    if ($('#fileUploadCtrl').val() == '') {
    //        //alert('Please Choose  file to Upload ')

    //        var fileErrorMsg = $("#fileErrorMsg").val();
    //        var fileErrorHeaderMSG = $("#fileErrorHeaderMSG").val();


    //        $("#modelHeaderTitle").text(fileErrorHeaderMSG);
    //        $("#modalMessage").text(fileErrorMsg);


    //        //$("#modalDiv").removeClass("animated hinge fast");
    //        setTimeout(function () {
    //            $("#modalDiv").css("display", "block");
    //        }, 200);
    //        return;
    //    }

    //    else {
    //        //$("#modalDiv").addClass("animated hinge fast");
    //        $("#modalDiv").css("display", "none");
    //    }


    //    if ($('#NewFilename').val() == '') {
    //        //alert('Please Give a file name ');
    //        $("#fileNameError").css("display", "block");
    //        return;
    //    }
    //    else {
    //        $("#fileNameError").css("display", "none");
    //    }

    //    var data = new FormData();
    //    var DocumentName = document.getElementById("NewFilename").value;

    //    var files = $("#fileUploadCtrl").get(0).files;

    //    if (files.length > 0) {
    //        data.append("Images", files[0]);
    //    }

    //    //var Tid = '@ViewBag.TokenId';
    //    //var uid = '@ViewBag.UserId';
    //    //var reqid1 = '@ViewBag.reqid1';


    //    var Tid = document.getElementById("Tid").value;

    //    var uid = document.getElementById("uid").value;

    //    var reqid1 = document.getElementById("reqid1").value;

    //    data.append("tokenid", Tid);
    //    data.append("mUserid", uid);
    //    data.append("OrgReqId", reqid1);
    //    data.append("DocumentName", DocumentName);
    //    data.append("DocumentType", $("#ddlDocumentTypes").val());
    //    data.append("Eservicerequestid", $("#Eservicerequestid").val());

    //    data.append("UploadedFrom", $("#Referenceprofile").val());

    //    //console.log(data);
    //    $.ajax({
    //        type: "POST",
    //        //cache: false, // Added for Security 
    //        url: UploadDocsURL,
    //        contentType: false,
    //        processData: false,
    //        dataType: 'json',
    //        data: data,
    //        success: function (responseData) {
    //            //alert(responseData);
    //            //debugger;

    //            $("#fileUploadCtrl").fileinput('clear');

    //            var newformdata = new FormData();
    //            $.each(responseData.Data.FileUploadResult, function (i, item) {
    //                newformdata.append("DocumentName", item.DocumentName);
    //                newformdata.append("DocumentType", item.DocumentType);
    //                newformdata.append("DocumentTypeCode", item.DocumentTypeCode);
    //                newformdata.append("OrganizationRequestDocumentId", item.OrganizationRequestDocumentId);
    //                newformdata.append("OrganizationRequestId", item.OrganizationRequestId);
    //                newformdata.append("TableName", item.TableName);
    //                var docid = item.Encryptedid;
    //                //var str = '@Url.Action("UploadDocument1", "Use" +  "r", new { OrgReqDocId = "aa" })';

    //                //var deleteStr = '/BrokerRenewal/DeleteFile?dataItem=aa';

    //                //var downloadStr = '/' + pageName + '/DownloadFile?dataItem=aa';

    //                if ($("#Referenceprofile").val() == "OrganizationRequests") {
    //                    var downloadStr = applicationUrl + pageName+ '/DownloadFile?filename=aa&&referenceprofile=OrganizationRequests';
    //                    var deleteStr = applicationUrl + 'user' + '/UploadDocument1?dataItem=aa';
    //                }
    //                else {
    //                    var downloadStr = applicationUrl + pageName + '/DownloadFile?filename=aa';
    //                    var deleteStr = applicationUrl + pageName + '/DeleteFile?dataItem=aa';
    //                }

    //                //var res = deleteStr.replace(/aa/g, docid);
    //                var resDelete = deleteStr.replace(/aa/g, docid);
    //                var resDownload = downloadStr.replace(/aa/g, docid);



    //                //var deleteURL = "location.href='" + res + "'";
    //                //var downloadURL = "location.href='" + resDownload + "'";

    //                var selectedFileType = $("#ddlDocumentTypes  option:selected").text();
    //                //alert(selectedFileType);

    //                //var td = "<tr><td><input type='checkbox' class='deleteselctedcheckbox'  id='" + docid + "'/></td><td onclick=" + deleteURL + ">" + item.DocumentName + "</td><td> " + selectedFileType + " </td><td>Download</td><td>Delete</td></tr>"
    //                //$("#Uploadedfileslist tbody").append(td);

    //                $("#ddlDocumentTypes  option:selected").remove();

    //                var downloadString = "Download ";
    //                var deleteString = "Delete ";

    //                la = getCookie("culture");

    //                if (la.indexOf("ar") != -1) {
    //                    downloadString = "تحميل الملف";
    //                    deleteString = "حذف الملف";
    //                }

    //                var filesTable = $('#Uploadedfileslist').DataTable();


    //                var today = new Date();
    //                var dd = today.getDate();
    //                var mm = today.getMonth() + 1; //January is 0!
    //                var yyyy = today.getFullYear();

    //                if (dd < 10) {
    //                    dd = '0' + dd
    //                }

    //                if (mm < 10) {
    //                    mm = '0' + mm
    //                }

    //                today = dd + '/' + mm + '/' + yyyy;

    //                //  window.location.reload();
    //                if ($("#Referenceprofile").val() == "OrganizationRequests") {
    //                    filesTable.row.add
    //                        ([

    //                            //"<input type='checkbox' class='deleteselctedcheckbox id=" + "'" + docid + "'" + "/>",
    //                            "<td>" + item.DocumentName + "</td>",
    //                            "<td>" + selectedFileType + "</td>",

    //                            "<td class='btn btn-primary'>" + "<a href=" + resDownload + ">" + downloadString + "</a></td>",
    //                            "<td class='btn btn-primary'>" + "<a href=" + resDelete + ">" + deleteString + "</a></td>"

    //                        ]).draw(true);

    //                }

    //                else {
    //                    filesTable.row.add
    //                        ([

    //                            //"<input type='checkbox' class='deleteselctedcheckbox id=" + "'" + docid + "'" + "/>",
    //                            "<td>" + item.DocumentName + "</td>",
    //                            "<td>" + selectedFileType + "</td>",
    //                            "<td>" + today + "</td>",
    //                            "<td class='btn btn-primary'>" + "<a href=" + resDownload + ">" + downloadString + "</a></td>",
    //                            "<td class='btn btn-primary'>" + "<a href=" + resDelete + ">" + deleteString + "</a></td>"

    //                        ]).draw(true);

    //                }

    //            });
    //        },
    //        error: function (err) {
    //            alert(err.statusText);
    //        }
    //    });



    //}

    var language = getCookie("culture");
    var successMsg;

    if (language.indexOf("en") != -1) {

        successMsg = "File Deleted Successfully";
    }

    else {
        successMsg = "تم حذف الملف بنجاح";
    }
    function DeleteFilewithDocIdForClearingExam(event) {

        //alert('deletemethod');
        var id = $(event).attr("id");
        $(this).closest('tr').remove();

        $.ajax({
            type: "POST",
            url: applicationUrl + 'ClearingAgentExamRequest/DeleteFile?dataItem=' + id,

            dataType: "json",
            contentType: "application/json; charset=utf-8",

            processData: false,

            //data: id,
            success: function (responseData) {

                alert(successMsg);


            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('errorupload');
                alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            }
        });



    }



    var filesTable;
    function up(pageName) {

        // var UploadDocsURL = 'http://10.10.26.226/API/api/Docs/Upload';
        // 'http://localhost/ETradeAPI/api/Docs/Upload';

        var UploadDocsURL = 'http://10.10.26.226/API/api/Docs/Upload';

        //  var UploadDocsURL = 'http://localhost/ETradeAPI/api/Docs/Upload';
        if ($('#fileUploadCtrl').val() == '') {
            //alert('Please Choose  file to Upload ')

            var fileErrorMsg = $("#fileErrorMsg").val();
            var fileErrorHeaderMSG = $("#fileErrorHeaderMSG").val();


            $("#modelHeaderTitle").text(fileErrorHeaderMSG);
            $("#modalMessage").text(fileErrorMsg);


            //$("#modalDiv").removeClass("animated hinge fast");
            setTimeout(function () {
                $("#modalDiv").css("display", "block");
            }, 200);
            return;
        }

        else {
            //$("#modalDiv").addClass("animated hinge fast");
            $("#modalDiv").css("display", "none");
        }


        if ($('#NewFilename').val() == '') {
            //alert('Please Give a file name ');
            $("#fileNameError").css("display", "block");
            return;
        }
        else {
            $("#fileNameError").css("display", "none");
        }

        var data = new FormData();
        var DocumentName = document.getElementById("NewFilename").value;

        var files = $("#fileUploadCtrl").get(0).files;

        if (files.length > 0) {
            data.append("Images", files[0]);
        }

        //var Tid = '@ViewBag.TokenId';
        //var uid = '@ViewBag.UserId';
        //var reqid1 = '@ViewBag.reqid1';


        var Tid = document.getElementById("Tid").value;

        var uid = document.getElementById("uid").value;

        var reqid1 = document.getElementById("reqid1").value;

        data.append("tokenid", Tid);
        data.append("mUserid", uid);
        data.append("OrgReqId", reqid1);
        data.append("DocumentName", DocumentName);
        data.append("DocumentType", $("#ddlDocumentTypes").val());
        data.append("Eservicerequestid", $("#Eservicerequestid").val());

        data.append("UploadedFrom", $("#Referenceprofile").val());

        //console.log(data);
        $.ajax({
            type: "POST",
            //cache: false, // Added for Security 
            url: UploadDocsURL,
            contentType: false,
            processData: false,
            dataType: 'json',
            data: data,
            success: function (responseData) {
                //alert(responseData);
                //debugger;

                $("#fileUploadCtrl").fileinput('clear');

                var newformdata = new FormData();
                $.each(responseData.Data.FileUploadResult, function (i, item) {
                    newformdata.append("DocumentName", item.DocumentName);
                    newformdata.append("DocumentType", item.DocumentType);
                    newformdata.append("DocumentTypeCode", item.DocumentTypeCode);
                    newformdata.append("OrganizationRequestDocumentId", item.OrganizationRequestDocumentId);
                    newformdata.append("OrganizationRequestId", item.OrganizationRequestId);
                    newformdata.append("TableName", item.TableName);
                    var docid = item.Encryptedid;
                    //var str = '@Url.Action("UploadDocument1", "Use" +  "r", new { OrgReqDocId = "aa" })';

                    //var deleteStr = '/BrokerRenewal/DeleteFile?dataItem=aa';

                    //var downloadStr = '/' + pageName + '/DownloadFile?dataItem=aa';

                    if ($("#Referenceprofile").val() == "OrganizationRequests") {
                        var downloadStr = applicationUrl + pageName + '/DownloadFile?filename=aa&&referenceprofile=OrganizationRequests';
                        var deleteStr = applicationUrl + 'user' + '/UploadDocument1?OrgReqDocId=aa';
                        var resDelete = deleteStr.replace(/aa/g, docid);
                        var resDownload = downloadStr.replace(/aa/g, docid);
                    }


                    if ($("#Referenceprofile").val() == "BRSExamDOCS") {
                        var downloadStr = applicationUrl + pageName + '/DownloadFile?filename=aa&&referenceprofile=BRSExamDOCS';
                        //   var deleteStr ='<input type="button" id=' + docid +' value="Delete" onclick="DeleteFilewithDocIdForClearingExam(this)" style= "font-size:12Px" id= "DeleteBtn" class="btn btn-primary"/>'
                        //  var resDelete = deleteStr.replace(/aa/g, docid);
                        var deleteStr = '<input type="button" id=' + docid + ' value= "Delete" onclick="DeleteFilewithDocIdForClearingExam(this)" style= "font-size:12Px" id= "DeleteBtn" class="mcbutton btn btn-primary" /> '
                        var resDelete = deleteStr.replace(/aa/g, docid);
                        var resDownload = downloadStr.replace(/aa/g, docid);
                    }

                    if ($("#Referenceprofile").val() == "BRSERenewalDocs") {
                        var downloadStr = applicationUrl + pageName + '/DownloadFile?filename=aa';
                        var deleteStr = applicationUrl + pageName + '/DeleteFile?dataItem=aa';
                        //  var deleteStr = ' <input type="button" id=' + docid +' value= "Delete" onclick="DeleteFilewithDocIdForClearingExam(this)" style= "font-size:12Px" id= "DeleteBtn" class="mcbutton" /> '

                        var resDelete = deleteStr.replace(/aa/g, docid);
                        var resDownload = downloadStr.replace(/aa/g, docid);
                    }

                    //var res = deleteStr.replace(/aa/g, docid);



                    //var deleteURL = "location.href='" + res + "'";
                    //var downloadURL = "location.href='" + resDownload + "'";

                    var selectedFileType = $("#ddlDocumentTypes  option:selected").text();
                    //alert(selectedFileType);

                    //var td = "<tr><td><input type='checkbox' class='deleteselctedcheckbox'  id='" + docid + "'/></td><td onclick=" + deleteURL + ">" + item.DocumentName + "</td><td> " + selectedFileType + " </td><td>Download</td><td>Delete</td></tr>"
                    //$("#Uploadedfileslist tbody").append(td);

                    //   $("#ddlDocumentTypes  option:selected").remove();

                    var downloadString = "Download ";
                    var deleteString = "Delete ";

                    la = getCookie("culture");

                    if (la.indexOf("ar") != -1) {
                        downloadString = "استعراض";
                        deleteString = "حذف";
                    }

                    filesTable = $('#Uploadedfileslist').DataTable();


                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!
                    var yyyy = today.getFullYear();

                    if (dd < 10) {
                        dd = '0' + dd
                    }

                    if (mm < 10) {
                        mm = '0' + mm
                    }

                    today = dd + '/' + mm + '/' + yyyy;

                    //  window.location.reload();
                    if ($("#Referenceprofile").val() == "OrganizationRequests") {
                        filesTable.row.add
                            ([

                                //"<input type='checkbox' class='deleteselctedcheckbox id=" + "'" + docid + "'" + "/>",
                                "<td>" + item.DocumentName + "</td>",
                                "<td>" + selectedFileType + "</td>",

                                "<td class='btn btn-primary'>" + "<a class='btn btn-primary' href=" + resDownload + ">" + downloadString + "</a></td>",
                                "<td class='btn btn-primary'>" + "<a class='btn btn-primary' href=" + resDelete + ">" + deleteString + "</a></td>"

                            ]).draw(true);

                    }

                    if ($("#Referenceprofile").val() == "BRSExamDOCS") {
                        filesTable.row.add
                            ([

                                //"<input type='checkbox' class='deleteselctedcheckbox id=" + "'" + docid + "'" + "/>",
                                "<td>" + item.DocumentName + "</td>",
                                "<td>" + selectedFileType + "</td>",
                                "<td>" + today + "</td>",
                                "<td class='btn btn-primary'>" + "<a class='btn btn-primary' href=" + resDownload + ">" + downloadString + "</a></td>",
                                "<td class='btn btn-primary'>" + "" + deleteStr + "</td>"
                                //  "<td class='btn btn-primary'>" + "<a class='btn btn-primary' href=" + deleteStr + ">" + deleteString + "</a></td>"

                            ]).draw(true);

                    }

                    else {
                        if ($("#Referenceprofile").val() != "OrganizationRequests") {
                            filesTable.row.add
                                ([

                                    //"<input type='checkbox' class='deleteselctedcheckbox id=" + "'" + docid + "'" + "/>",
                                    "<td>" + item.DocumentName + "</td>",
                                    "<td>" + selectedFileType + "</td>",
                                    "<td>" + today + "</td>",
                                    "<td class='btn btn-primary'>" + "<a class='btn btn-primary' href=" + resDownload + ">" + downloadString + "</a></td>",
                                    "<td class='btn btn-primary'>" + "<a class='btn btn-primary' href=" + resDelete + ">" + deleteString + "</a></td>"

                                ]).draw(true);
                        }
                    }

                });
            },
            error: function (err) {
                alert(err.statusText);
            }
        });


        if (page_name.indexOf("ClearingAgentExamRequest") != -1) {
            //  filesTable = $('#Uploadedfileslist').DataTable();
            $('#Uploadedfileslist').on("click", ".mcbutton", function () {
                //  console.log($(this).parent());
                filesTable.row($(this).parents('tr')).remove().draw(false);
            });
        }

    }





    $("#DeleteBtn").click(function () {

        var message = '';
        var HostedLocation = location.pathname.split('/')[1];
        var delteselected = new Array();

        var count = 0;
        $('#Uploadedfileslist').find('input.deleteselctedcheckbox:checkbox:checked').each(function () {


            {
                //   alert($(this).attr('name'));
                delteselected.push($(this).attr('name'));
                count++;
                $(this).closest('tr').remove();

            }
        });
        if ($("#Uploadedfileslist").find(".deleteselctedcheckbox:checkbox:checked").text().trim() == "") {
            $('#headercheck').prop('checked', false);
        }
        if ($("#Uploadedfileslist").find("input:checkbox:not(:checked)")) {
            $('#headercheck').prop('checked', false);

        }

        {
            var getlanguageforalert = "eng";


            var dataItem = delteselected.join(',');



            if (dataItem == "") {

                if (getlanguageforalert == "eng") {

                    alert('No File selected to delete')
                    $('#headercheck').prop('checked', false);
                    return false;
                }
                else {
                    alert('لم يتم اختيار أي ملفات للحذف')
                    $('#headercheck').prop('checked', false);
                    return false;
                }

            }
            else {
                if (getlanguageforalert == "eng") {


                    message = ' File  deleted';


                }
                else {
                    message = " تم حذف الملف";


                }


                if ($("#Uploadedfileslist").find(".deleteselctedcheckbox:checkbox:checked").text().trim() == "") {
                    $('#headercheck').prop('checked', false);
                }
                if ($("#Uploadedfileslist").find("input:checkbox:not(:checked)")) {
                    $('#headercheck').prop('checked', false);

                }


                $.ajax({
                    type: "POST",
                    cache: false, // Added for Security 
                    url: "/BrokerRenewal/DeleteFile",
                    data: { 'dataItem': dataItem },
                    success: function (response) {

                        alert(response.responseText);

                    },
                    failure: function (response) {
                        alert(response.responseText);
                    },
                    error: function (response) {
                        alert(response.responseText);
                    }
                });
            }

        }
        return false;
    });



}
function DeleteFilewithDocId(docid, event) {
    //alert('deletemethod');
    $.ajax({
        type: "GET",
        url: applicationUrl + 'BrokerRenewal/DeleteFile?dataItem=' + docid,
        contentType: false,
        processData: false,
        dataType: 'json',
        //data: data,
        success: function (responseData) {
            //alert('success');
            //alert(responseData);
            //debugger;
            //var res = JSON.parse(responseData.Data);
            console.log(responseData);
            if (responseData.success === "0") {
                //alert(responseData.responseText);
                var d = "#" + docid;
                //$(d).remove();
                //$(this).closest('tr').remove();
                //console.log($(this).parent());
                //console.log($(this).closest('tr'));
                //console.log(event.target);
                //console.log(event.target.parentNode);
                //console.log(event.target.parentNode.parentNode);
                //console.log(event.target.parentNode.parentNode.parentNode.removeChild(event.target.parentNode.parentNode));
                //event.target.parent.remove()

                window.location.reload();
            }
            else {
                alert('Some issue has occured');
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert('errorupload');
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        }
    });



}





// Old Script  in File   OldScript




