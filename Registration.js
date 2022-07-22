function getLangCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

var currentlang = getLangCookie("culture");


function loadCaptcha() {
    //console.log("clicked");
    $("#m_imgCaptcha").removeAttr("src").attr('src', "../registration/GetCaptchaImage?Refreshforce=" + Math.floor(Math.random() * 1000));
    //$.ajax({
    //    type: 'GET',
    //    url: 'registration/GetCaptchaImage',
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    cache: false,
    //    success: function (data) {
    //        console.log(data);
    //        $("#m_imgCaptcha").attr('src', data);
    //    },
    //    error: function (data) { alert("Error while loading captcha image") }
    //});
}

$().ready(function () {
    //$("#CaptchaInputText").before("</br>");
    //$("#CaptchaInputText").addClass("");
    //$("#CaptchaInputText").css({ "border-bottom-color": "chocolate" });
    ////$("#CaptchaInputText").attr('data-val', true);
    ////$("#CaptchaInputText").attr('data-val-required', 'Please enter the Captcha');
    ////$("#CaptchaInputText").after("<span class='field-validation-valid text-danger' data-valmsg-for='CaptchaInputText' data-valmsg-replace='true'></span>");

    //console.log(SelectedEntity);
    if (SelectedEntity !== null) {
        if (SelectedEntity === "1") {
            //console.log("1st");
            //$("#LegalEntity").val("1");
            $("#LegalEntity option[value='1']").attr('selected', 'selected');
            $("#LegalEntity").trigger('change');
        }
        else if (SelectedEntity === "2") {
            //console.log("2nd");
            //$("#LegalEntity").val("2");
            $("#LegalEntity option[value='2']").attr('selected', 'selected');
            $("#LegalEntity").trigger('change');
        }

        //console.log($("#LegalEntity").val());
        $('.LegalEntityBlock').attr('data-val', false);
        $(".LegalEntityBlock").css('display', 'none');
        //$(".LegalEntityBlock").attr('readonly', true);
    }




    $("#example_previous a").first().text("<<");
    $("#example_next a").first().text(">>");
    $("#exampleO_previous a").first().text("<<");
    $("#exampleO_next a").first().text(">>");
}
);

$("#IsAdmin").change(function () {
    if (this.checked) {
        $(".AdminF").removeClass('ClearingAgentNP').addClass('ClearingAgentP');
        $("#example_wrapper").removeClass('ClearingAgentNP').addClass('ClearingAgentP');
        $("#exampleO_wrapper").removeClass('ClearingAgentNP').addClass('ClearingAgentP');
        $("#Datatablevalidation").remove();
    } else {
        $(".AdminF").removeClass('ClearingAgentP').addClass('ClearingAgentNP');
        $("#example_wrapper").removeClass('ClearingAgentP').addClass('ClearingAgentNP');
        $("#exampleO_wrapper").removeClass('ClearingAgentP').addClass('ClearingAgentNP');

    }
});
$("#ExistingUser").change(function () {
    if (this.checked) {
        $(".ClearingAgentPOPT").removeClass('ClearingAgentNP').addClass('ClearingAgentP');
        $(".ClearingAgentExistingUser").removeClass('ClearingAgentP').addClass('ClearingAgentNP');
        $('.ClearingAgent').attr('data-val', false);

    } else {
        $(".ClearingAgentPOPT").removeClass('ClearingAgentP').addClass('ClearingAgentNP');
        $(".ClearingAgentExistingUser").removeClass('ClearingAgentNP').addClass('ClearingAgentP');
        $('.ClearingAgent').attr('data-val', false);

    }
});


$().ready(function () {
    $(".OrgPOPT").removeClass('OrgNP').addClass('OrgP');
}
);


$("#IsIndustrial").change(function () {
    if (this.checked) {
        $(".OrgPOPT").removeClass('OrgP').addClass('OrgNP');
        $('.OrgPOPT').attr('data-val', true);

    } else {
        $(".OrgPOPT").removeClass('OrgNP').addClass('OrgP');
        $('.OrgPOPT').attr('data-val', false);
        $('#IndustrialLicenseNumber').val('');
    }
});

$('#LegalEntity').change(function () {
    var val = this.value;
    //alert(val);
    if (val === "1") {
        //$('.ClearingAgentP').show(); // Shows
        //$('.OrgP').hide(); // hides

        //$('.ClearingAgentP').css({'display':''}); // Shows
        //$('.OrgP').css({ 'display': 'none' }); // hides
        $("#ExistingUser").prop("checked", false);

        $(".ClearingAgentP").removeClass('ClearingAgentP').addClass('ClearingAgentNP');
        $(".OrgNP").removeClass('OrgNP').addClass('OrgP');

        //Special handling for existing user field , adding display none again
        $(".ClearingAgentExistingUser").removeClass('ClearingAgentNP').addClass('ClearingAgentP');


        $('.ClearingAgent').attr('data-val', true);
        $('.Org').attr('data-val', false);

    } else if (val === "2") {
        //$('.ClearingAgentP').hide(); // Shows
        //$('.OrgP').show(); // hides

        //$('.ClearingAgentP').css({ 'display': 'none' }); // Shows
        //$('.OrgP').css({ 'display': '' }); // hides

        $(".OrgP").removeClass('OrgP').addClass('OrgNP');
        $(".ClearingAgentNP").removeClass('ClearingAgentNP').addClass('ClearingAgentP');

        $('.ClearingAgent').attr('data-val', false);
        $('.Org').attr('data-val', true);
    } else if (val === "0") {
        //$('.ClearingAgentP').hide(); // Shows
        //$('.OrgP').hide(); // hides

        //$('.ClearingAgentP').css({ 'display': 'none' }); // Shows
        //$('.OrgP').css({ 'display': 'none' }); // hides

        $(".ClearingAgentNP").removeClass('ClearingAgentNP').addClass('ClearingAgentP');
        $(".OrgNP").removeClass('OrgNP').addClass('OrgP');

        $('.ClearingAgent').attr('data-val', true);
        $('.Org').attr('data-val', true);
    }

});

$("#example_paginate ,#exampleO_paginate").bind('contentchanged', function () {
    $("#example_previous a").first().text("<<");
    $("#example_next a").first().text(">>");
    $("#exampleO_previous a").first().text("<<");
    $("#exampleO_next a").first().text(">>");
}
);


function getLang(cname) {
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



// extend jquery range validator to work for required checkboxes
var defaultRangeValidator = $.validator.methods.range;
$.validator.methods.range = function (value, element, param) {
    if (element.type === 'checkbox') {
        // if it's a checkbox return true if it is checked
        return element.checked;
    } else {
        // otherwise run the default validation function
        return defaultRangeValidator.call(this, value, element, param);
    }
};

function RegSuccess(data) {
    //$('#ddd').show();
    loadCaptcha();
    //window.location.href = "Accountverification?";
    //alert(data.message);
    if (data.StatusCode === 0)
    {
        var successHeader = "رسالة";
        var successMsg = "تمت عملية التسجيل بنجاح";

        var language = getLang("culture");

        if (language.indexOf("en") != -1)
        {
             successHeader = "Message";
             successMsg = "Registeration Done Successfully";
        }


        $("#modelHeaderTitle").text(successHeader);
        $("#modalMessage").text(successMsg);//data.message);//successMsg);

        //$("#modalDiv").removeClass("animated hinge fast");
        setTimeout(function ()
        {
            $("#modalDiv").css("display", "block");
        }, 200);


        setTimeout(function () {
            window.location.href = data.URL;
            console.log(data.URL);
            console.log(window.location.href);
        }, 1500);

        
    }
    else {
        //alert(data.message);

        $("#modalMessage").text(data.message);

        $("#modalDiv").css("display", "block");

        if (data.StatusCode === 88) {
            setTimeout(function () { //added in case of - something went wrong message
                window.location.href = "../registration/index";
            }, 2500);
        }

    }
    ////StatusCode

    console.log(JSON.stringify(data));
    //console.log(data.result);
}


function RegFailure(data) {
    loadCaptcha();
    //alert('Some issue has occured. Please try again');
    alert(data.message);
}

var SVCARRAY = new Array();
var ORGARRAY = new Array();


$(document).ready(function () {


    var lag = getLang("culture");

    if (lag.indexOf("en") != -1)

    {

        var table = $('#example').DataTable({
            //'ajax': {
            //    'url': '/lab/articles/jquery-datatables-how-to-add-a-checkbox-column/ids-arrays.txt'
            //},
            "bPaginate": true,
            "bFilter": false,
            "bInfo": false,
            "pageLength": 5,
            'pagingType': 'numbers',
            "bLengthChange": false,
            'columnDefs': [{
                'targets': 0,
                'searchable': false,
                'orderable': false,
                'className': 'dt-body-center'
                //,'render': function (data, type, full, meta) {
                //    return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                //}
            },
            {
                "targets": 1,
                "visible": false
            }],
            'order': [[2, 'asc']]
        });


        var table1 = $('#exampleO').DataTable({
            //'ajax': {
            //    'url': '/lab/articles/jquery-datatables-how-to-add-a-checkbox-column/ids-arrays.txt'
            //},
            "bPaginate": true,
            "bFilter": false,
            "bInfo": false,
            "pageLength": 5,
            'pagingType': 'numbers',
            "bLengthChange": false,
            'columnDefs': [{
                'targets': 0,
                'searchable': false,
                'orderable': false,
                'className': 'dt-body-center'
                //,'render': function (data, type, full, meta) {
                //    return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                //}
            },
            {
                "targets": 1,
                "visible": false
            }],
            'order': [[2, 'asc']]
        });


    }

    else {
        var table = $('#example').DataTable({
            //'ajax': {
            //    'url': '/lab/articles/jquery-datatables-how-to-add-a-checkbox-column/ids-arrays.txt'
            //},

            "language":
            {
                "url": applicationUrl + "/scripts/arabic.json"
            },
            "bPaginate": true,
            "bFilter": false,
            "bInfo": false,
            "pageLength": 5,
            'pagingType': 'numbers',
            "bLengthChange": false,
            'columnDefs': [{
                'targets': 0,
                'searchable': false,
                'orderable': false,
                'className': 'dt-body-center'
                //,'render': function (data, type, full, meta) {
                //    return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                //}
            },
            {
                "targets": 1,
                "visible": false
            }],
            'order': [[2, 'asc']]
        });


        var table1 = $('#exampleO').DataTable({
            //'ajax': {
            //    'url': '/lab/articles/jquery-datatables-how-to-add-a-checkbox-column/ids-arrays.txt'
            //},

            "language":
            {
                "url": applicationUrl + "/scripts/arabic.json"
            },
            "bPaginate": true,
            "bFilter": false,
            "bInfo": false,
            "pageLength": 5,
            'pagingType': 'numbers',
            "bLengthChange": false,
            'columnDefs': [{
                'targets': 0,
                'searchable': false,
                'orderable': false,
                'className': 'dt-body-center'
                //,'render': function (data, type, full, meta) {
                //    return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                //}
            },
            {
                "targets": 1,
                "visible": false
            }],
            'order': [[2, 'asc']]
        });
    }


    
    

    // Handle click on "Select all" control
    $('#example-select-all').on('click', function () {
        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
        if (this.checked) {
            SVCARRAY = table
                .column(1)
                .data()
                .toArray();
            console.log(SVCARRAY);
        } else {
            SVCARRAY.length = 0;
        }
        //$(".activateselectedservice").prop('checked', $(this).prop('checked'));
    });
    $('#example-select-allO').on('click', function () {
        // Get all rows with search applied
        var rows = table1.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
        if (this.checked) {
            ORGARRAY = table1
                .column(1)
                .data()
                .toArray();
            console.log(ORGARRAY);
        } else {
            ORGARRAY.length = 0;
        }
        //$(".activateselectedserviceO").prop('checked', $(this).prop('checked'));
    });
    // Handle click on checkbox to set state of "Select all" control
    $('#example tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            $('#example-select-all').prop('checked', false);
            var svcindx = SVCARRAY.indexOf(this.name);
            SVCARRAY.splice(svcindx, 1);
            console.log(SVCARRAY);
        }
        else {
            if (SVCARRAY.indexOf(this.name) === -1) SVCARRAY.push(this.name);
            if (SVCARRAY.length === table.column(1).data().length) {
                $('#example-select-all').prop('checked', true);
            }
            console.log(SVCARRAY);
        }
        //if ($('.activateselectedservice:checked').length > 0 && $('.activateselectedserviceO:checked').length > 0 && $("#Datatablevalidation").length > 0) {
        //    $("#Datatablevalidation").remove();
        //}
        var MinCriteriamsg = '';
        if (ViewBagOrgAdmin === 'True') { MinCriteriamsg = OrgAdditionalUserCriteria} else { MinCriteriamsg=ClearingAgentAdditionalUserCriteria}

        if (ORGARRAY.length > 0 && SVCARRAY.length > 0) {
            if ($("#Datatablevalidation").length > 0)

                $("#Datatablevalidation").remove();
        }
        else {
            if ($("#Datatablevalidation").length <= 0)
                $("#DTVald").after('<p class="contact" id="Datatablevalidation"><span class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true"><span for="Pass" class="">' + MinCriteriamsg+'</span></span></p>');

        }

    });
    $('#exampleO tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            $('#example-select-allO').prop('checked', false);
            var svcindx = ORGARRAY.indexOf(this.name);
            //console.log(svcindx);
            ORGARRAY.splice(svcindx, 1);
            console.log(ORGARRAY);
        }
        else {
            if (ORGARRAY.indexOf(this.name) === -1) ORGARRAY.push(this.name);
            if (ORGARRAY.length === table1.column(1).data().length) {
                $('#example-select-allO').prop('checked', true);
            }
            //console.log(ORGARRAY.length + "  " + table1.column(1).data().length);
            console.log(ORGARRAY);
        }
        //if ($('.activateselectedserviceO:checked').length > 0 && $('.activateselectedservice:checked').length > 0 && $("#Datatablevalidation").length > 0) {
        //    $("#Datatablevalidation").remove();
        //}
        var MinCriteriamsg = '';
        if (ViewBagOrgAdmin === 'True') { MinCriteriamsg = OrgAdditionalUserCriteria } else { MinCriteriamsg = ClearingAgentAdditionalUserCriteria }

        if (ORGARRAY.length > 0 && SVCARRAY.length > 0) {
            if ($("#Datatablevalidation").length > 0)
                $("#Datatablevalidation").remove();
        }
        else {
            if ($("#Datatablevalidation").length <= 0)
                $("#DTVald").after('<p class="contact" id="Datatablevalidation"><span class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true"><span for="Pass" class="">' + MinCriteriamsg+'</span></span></p>');

        }
    });



    $('#example_wrapper , #exampleO_wrapper').css({ "width": "90%" });

    //$("#SubmitRequest").on("click", function () {


    //    // Iterate over all checkboxes in the table
    //    table1.$('input[type="checkbox"]').each(function () {
    //        // If checkbox doesn't exist in DOM
    //        if (!$.contains(document, this)) {
    //            // If checkbox is checked
    //            if (this.checked) {
    //                console.log(this.name);

    //            }
    //        }
    //    });
    //        });

});



function SetSelectedServices() // Handle form submission event
{
    //$(".inlineBlock").css("margin-right", "10px");
    //$(".inlineBlock").css("margin-left", "10px");

    $(".inlineBlock").addClass("marginRightLeft10PX");

    

    $("#SelectedOrganizations").val(ORGARRAY.join(','));
    $("#SelectedServices").val(SVCARRAY.join(','));


    console.log(SVCARRAY.join(','));
    console.log(ORGARRAY.join(','));


    if ($("#IsAdmin").prop("checked") === false)
    //if ($(".AdminF").length > 0)
    {

        //if ($('.activateselectedservice:checkbox:checked').length <= 0 || $('.activateselectedserviceO:checkbox:checked').length <= 0) {

        //currentlang = getLangCookie("culture");

        //console.log("test");
        //console.log(currentlang);
        //if (currentlang.indexOf("ar") !== -1) {//Fix KITS-16161 - Missing translation after selecting a service to assign in additional user screen
        //    OrgAdditionalUserCriteria = OrgAdditionalUserCriteriaAra;//'@Resources.Resource.OrgAdditonalUserCriteria';
        //    ClearingAgentAdditionalUserCriteria = ClearingAgentAdditionalUserCriteriaAra;//'@Resources.Resource.ClearingAgentAdditonalUserCriteria';
          
        //}
        //else {
        //    OrgAdditionalUserCriteria = OrgAdditionalUserCriteriaEng;//'@Resources.Resource.OrgAdditonalUserCriteria';
        //    ClearingAgentAdditionalUserCriteria = ClearingAgentAdditionalUserCriteriaEng;//'@Resources.Resource.ClearingAgentAdditonalUserCriteria';

        //}

        if (ViewBagOrgAdmin === 'True') {
            if (SVCARRAY.length <= 0 || ORGARRAY.length <= 0 ) {

                if ($("#Datatablevalidation").length <= 0) {
                    $("#DTVald").after('<p class="contact" id="Datatablevalidation"><span class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true"><span for="Pass" class="">' + OrgAdditionalUserCriteria+'</span></span></p>');
                    //$('<p class="contact" id="Datatablevalidation"><span class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true"><span for="Pass" class="">Please ensure to select minimum one Service and one Organization for the Additional user</span></span></p>').insertAfter($("#DTVald"));
                }
                event.preventDefault();
            }
            else {
                $("#Datatablevalidation").remove();
            }
        }
        else {
            if (SVCARRAY.length <= 0 ) {

                if ($("#Datatablevalidation").length <= 0) {
                    $("#DTVald").after('<p class="contact" id="Datatablevalidation"><span class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true"><span for="Pass" class="">' + ClearingAgentAdditionalUserCriteria+'</span></span></p>');
                    //$('<p class="contact" id="Datatablevalidation"><span class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true"><span for="Pass" class="">Please ensure to select minimum one Service and one Organization for the Additional user</span></span></p>').insertAfter($("#DTVald"));
                }
                event.preventDefault();
            }
            else {
                $("#Datatablevalidation").remove();
            }
        }
       
    }

}



$(function () {

    jQuery.validator.addMethod('checkgeneralbrokerlicense', function (value, element, params) {

        console.log(value);
        if (value.length >= 6) {
            var Firstchar = value.substr(0, 1);
            var SixthChar = value.substr(5, 1);
            if (Firstchar === "1" && SixthChar === "1") {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }


    }, '');

    jQuery.validator.unobtrusive.adapters.add('checkgeneralbrokerlicense', function (options) {
        options.rules['checkgeneralbrokerlicense'] = {};
        options.messages['checkgeneralbrokerlicense'] = options.message;
    });

}(jQuery));




$("#Regreshcaptch").click(function () {

    $("#m_imgCaptcha").removeAttr("src").attr('src', "../registration/GetCaptchaImage?Refreshforce=" + Math.floor(Math.random() * 1000));

});



// by azhar for autopopulate user details in registration 

var RegistrationMociCodes;


// this code is wriiten as the contrl loses focus on blur so we will capture if the keyup for 
// the next control came via tab or enter and so we will populate data 
    var tabpressed;
    $('input[name=ImporterLicenseNumber]').on('keyup', function (e) {
        var code = e.keyCode || e.which;
        if (code == 9 || code == 13) {
            getmocidetails('civilid');
       }
    });


    $('input[name=TradeLicenseNumber]').on('keyup', function (e) {
       var code = e.keyCode || e.which;
        if (code == 9 || code == 13) {
            getmocidetails('ImporterLicenseNumber');
       }
    });
//var isMobile = {
//    Android: function () {
//        return navigator.userAgent.match(/Android/i);
//    },
//    BlackBerry: function () {
//        return navigator.userAgent.match(/BlackBerry/i);
//    },
//    iOS: function () {
//        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
//    },
//    Opera: function () {
//        return navigator.userAgent.match(/Opera Mini/i);
//    },
//    Windows: function () {
//        return navigator.userAgent.match(/IEMobile/i);
//    },
//    any: function () {
//        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
//    }
//};
//if (isMobile.any()) {
//    var tabpressed;
//    $('input[name=ImporterLicenseNumber]').on('focus', function (e) {
//        var code = $("#CompanyCivilId").val();
//        if (code !="")
//        {
//            getmocidetails('civilid');
//        }
//    });


//    $('input[name=TradeLicenseNumber]').on('focus', function (e) {
//        var code = $("#ImporterLicenseNumber").val();
//        if (code != "") {
//            getmocidetails('ImporterLicenseNumber');
//        }
//    });

//}

function getmocidetails(reftype) {

    var msg;
    var language = getLang("culture");

    if (language.indexOf("en") != -1) {
        msg = "There is no information available for entered organization Civil ID, please re-enter the correct organization Civil ID or enter the import license number and try again";
        $("#CompanyCivilId").val('');
    }
    else
    {
        $("#CompanyCivilId").val('');
        msg = "لا يوجد معلومات متوفرة عن رقم الجهة المدني الذي تم إدخاله، يرجي إعادة إدخال رقم الجهة المدني بشكل صحيح أو قم بإدخال رقم رخصة الإستيراد وإعادة المحاولة ";
    }

    var myid;
    if (reftype == 'civilid') {

        myid = $('#CompanyCivilId').val();
        $('#ImporterLicenseNumber').prop('readonly', false);
    }
    else {
        myid = $('#ImporterLicenseNumber').val();
        // myid = encodeURIComponent($('#ImporterLicenseNumber').val());
    }

    if ($('#ImporterLicenseNumber').prop('readonly')) {

    }
    else {
       // var btn1 = document.getElementById('CompanyCivilId');

     //   document.addEventListener('keyup', function (event) {
          //  if (event.keyCode == 9 )
            {
            // alert($("#TradeLicenseNumber").val());
            if ($("#TradeLicenseNumber").val() == "" )
                   {
               $.ajax({
                    type: "post",
                    url: "/esonepp/registration/GetCivilIDDetailsFromMoci",
                    //  url: "/registration/GetCivilIDDetailsFromMoci",

                    data: { CompanyCivilOrImporterId: myid, reftype: reftype },
                    datatype: "json",
                    traditional: true,
                    success: function (data) {
                        RegistrationMociCodes = data;
                        console.log(RegistrationMociCodes);
                        if (data[0] != null) {
                            if (data[0].ImporterLicenseNumber.length != '0') {
                                $("#ImporterLicenseNumber").val(data[0].ImporterLicenseNumber).attr('readonly', 'true');

                            }
                            else {
                        
                            }
                            if ((data[0].TradeLicenseNumber.val == 'undefined' || data[0].TradeLicenseNumber.length == '0')  )
                            {
                                
                            }
                            else {

                                $("#TradeLicenseNumber").val(data[0].TradeLicenseNumber).attr('readonly', 'true');
                            }



                            if ($("#TradeLicenseNumber").val() == '') {
                                $("#modalMessage").text(msg);
                                $("#modalDiv").css("display", "block");
                            }

                            if (data[0].CommercialLicenseNumber.val == 'undefined') {

                            }
                            else {
                                $("#CommercialLicenseNumber").val(data[0].CommercialLicenseNumber).attr('readonly', 'true');
                            }

                        }

                    }
                    , error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        console.log(msg);
                    }
                });
             }
        }
  
  //  });
      


       


      



    }
}



var GovernorateRegionPostalCodes;
$(document).ready(function () {
    $('#Governorate').change(function () {
        $.ajax({
            type: "post",
       url: "/esonepp/registration/GetCitiesofGovernorate",
      //  url: "/registration/GetCitiesofGovernorate",

            data: { GovernorateId: $('#Governorate').val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                GovernorateRegionPostalCodes = data;
                console.log(GovernorateRegionPostalCodes);
                $('#Region').children('option:not(:first)').remove();
                //var Region = "<select id='Region'>";
                //Region = Region + '<option value="">--Select--</option>';
                for (var i = 0; i < data.length; i++) {
                    //district = district + '<option value=' + data[i].RegionID + '>' + data[i].RegionName + '</option>';
                    $('#Region').append('<option value=' + data[i].RegionID + '>' + data[i].RegionName + '</option>');
                }
                //district = district + '</select>';
                //$('#District').html(district);
            }
            , error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                console.log(msg);
            }
        });
    });


    $('#Region').change(function () {

        //console.log(GovernorateRegionPostalCodes);
        ////console.log(GovernorateRegionPostalCodes[0]);
        ////console.log(GovernorateRegionPostalCodes[0].PostalCode);
        //console.log($('#Region').val());
        //console.log("test");
        //console.log(GovernorateRegionPostalCodes);


        var temp = GovernorateRegionPostalCodes.filter(function (item) { return item.RegionID == $('#Region').val(); });


        //$(GovernorateRegionPostalCodes).each(function (index)
        //{

        //    console.log("There " +  GovernorateRegionPostalCodes[index].RegionID);

        //    if (GovernorateRegionPostalCodes[index].RegionID == $('#Region').val())
        //    {
        //        console.log("I'm Here  " +  index);
        //    }
        //});



        console.log(temp);
        if (temp) {
            //var PostalCodeOfTheRegion = GovernorateRegionPostalCodes.filter(function (item) { return item.RegionID === $('#Region').val(); });
            console.log('found', temp);
            console.log('found', temp[0].PostalCode);

            $('#PostalCode').val(temp[0].PostalCode);
        }



    });

});