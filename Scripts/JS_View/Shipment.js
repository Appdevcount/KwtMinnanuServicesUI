$().ready(function () {
    $('.Issuedate').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+10"
    }).attr('readonly', 'readonly');
    $('.expirydate').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        maxDate: '0',
        minDate: '',
        changeYear: true,
        yearRange: "-60:+10"
    }).attr('readonly', 'readonly');


})



function Checkauthpers(event) {
    alert('testet');
}

function clone() {
    ////var $button = $('.clone').last().clone().append('<img class="btnimg button-remove"  src="~/images/Buttons/Delete.png"/>')
    //// //   append('<input class="button-remove" style="display:block" class="btn btn-info btn-block col-centered width50Percent"  type="button" value="Remove">');;
    ////var $last = $(".clone").eq(0).after($button);




    ////$(".button-remove").css("display", "block");

    //$('.clone:last').clone().insertAfter(".clone:last").
    //    append('<img class="btnimg button-remove" src="http://localhost:58449/images/Buttons/Delete.png"/>');

    ////  $(".button-remove").attr("src", "~/images/Buttons/Delete.png");
    //$(".button-remove").css("display", "block");

    var $authpernum = $('.clone:last').clone();
    //console.log($authpernum);
    //var elemaup = $authpernum;
    //console.log(elemaup);
    ////var $authpernum1 = $('.clone:first').clone();
    ////console.log($authpernum1);


    var num = parseInt($authpernum.find('.NameOfDelegate').prop("name").match(/\d+/g), 10) + 1;
    console.log(num);
    var idgen = "shipmentAuthorizationDetails_" + num + "__";//+ "__MobileNumber";
    var namegen = "shipmentAuthorizationDetails[" + num + "].";//+ "__MobileNumber";
    console.log(idgen);

    $authpernum.find('.NameOfDelegate').attr('id', idgen + "NameOfDelegate");
    $authpernum.find('.NameOfDelegate').attr('name', namegen + "NameOfDelegate");
    $authpernum.find('.CivilId').attr('id', idgen + "CivilId");
    $authpernum.find('.CivilId').attr('name', namegen + "CivilId");
    $authpernum.find('.Gender').attr('id', idgen + "Gender");
    $authpernum.find('.Gender').attr('name', namegen + "Gender");
    $authpernum.find('.Nationality').attr('id', idgen + "Nationality");
    $authpernum.find('.Nationality').attr('name', namegen + "Nationality");
    $authpernum.find('.MobileNumber').attr('id', idgen + "MobileNumber");
    $authpernum.find('.MobileNumber').attr('name', namegen + "MobileNumber");
    $authpernum.find('.AttorneyNumber').attr('id', idgen + "AttorneyNumber");
    $authpernum.find('.AttorneyNumber').attr('name', namegen + "AttorneyNumber");
    $authpernum.find('.AttorneyIssuedby').attr('id', idgen + "AttorneyIssuedby");
    $authpernum.find('.AttorneyIssuedby').attr('name', namegen + "AttorneyIssuedby");
    $authpernum.find('.AttorneyIssueDate').attr('id', idgen + "AttorneyIssueDate");
    $authpernum.find('.AttorneyIssueDate').attr('name', namegen + "AttorneyIssueDate");
    $authpernum.find('.AttorneyExpiryDate').attr('id', idgen + "AttorneyExpiryDate");
    $authpernum.find('.AttorneyExpiryDate').attr('name', namegen + "AttorneyExpiryDate");
    $authpernum.find('.AttorneyRemarks').attr('id', idgen + "AttorneyRemarks");
    $authpernum.find('.AttorneyRemarks').attr('name', namegen + "AttorneyRemarks");

    $authpernum.find('.NameOfDelegate').val("");
    $authpernum.find('.CivilId').val("");
    $authpernum.find('.MobileNumber').val("");
    $authpernum.find('.AttorneyNumber').val("");
    $authpernum.find('.AttorneyIssueDate').val("");
    $authpernum.find('.AttorneyExpiryDate').val("");
    $authpernum.find('.AttorneyRemarks').val("");
    $authpernum.find('.Gender').val("0");
    $authpernum.find('.Nationality').val("0");
    $authpernum.find('.AttorneyIssuedby').val("1");

    //$authpernum.find('.AttorneyIssueDate').removeClass().addClass("selectpicker form-control  select2 AttorneyIssueDate");
    //$authpernum.find('.AttorneyExpiryDate').removeClass().addClass("selectpicker form-control  select2 AttorneyExpiryDate");

    $authpernum.find('.AttorneyIssueDate').removeClass("hasDatepicker");//.datepicker("destroy");
    $authpernum.find('.AttorneyIssueDate').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+10"
        }).attr('readonly', 'readonly');
    $authpernum.find('.AttorneyExpiryDate').removeClass("hasDatepicker");//.datepicker("destroy");
    $authpernum.find('.AttorneyExpiryDate').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        maxDate: '0',
        minDate: '',
        changeYear: true,
        yearRange: "-60:+10"
    }).attr('readonly', 'readonly');
   

    //console.log($authpernum.find('.NameOfDelegate'));
    //console.log($authpernum.find('.CivilId'));
    //console.log($authpernum.find('.Gender'));
    //console.log($authpernum.find('.Nationality'));
    //console.log($authpernum.find('.MobileNumber'));
    //console.log($authpernum.find('.AttorneyNumber'));
    //console.log($authpernum.find('.AttorneyIssuedby'));
    //console.log($authpernum.find('.AttorneyIssueDate'));
    //console.log($authpernum.find('.AttorneyExpiryDate'));
    //console.log($authpernum.find('.AttorneyRemarks'));
    console.log($authpernum.children('div.removediv'));
    console.log($authpernum.children('div.removediv').length);
    //if ($authpernum.children('div.removediv').length > 0) {
    //    $authpernum.insertAfter(".clone:last");
    //    $(".button-remove").css("display", "block");
    //}
    //else {
    //    $authpernum.insertAfter(".clone:last").
    //        append('<div class="form-group  removediv "><label class = "control-label col-md-2  " > &nbsp;</label><div class="col-md-6 inputGroupContainer "><img class="btnimg button-remove" src="../Images/UploadDocIcons/Delete.png" /></div></div >');
    //    $(".button-remove").css("display", "block");
    //}
    if ($authpernum.children('img.btnimg').length > 0) {
        $authpernum.insertAfter(".clone:last");
        $(".button-remove").css("display", "block");
    }
    else {
        $authpernum.find('.removespan').html('<img class="btnimg button-remove" src="../Images/UploadDocIcons/Delete.png" />');
        $authpernum.insertAfter(".clone:last");
        $(".button-remove").css("display", "block");
           }
   
    //$authpernum.insertAfter(".clone:last").
    //    append('<img class="btnimg button-remove" src="http://localhost:58449/images/Buttons/Delete.png"/>');
    //var $lascld = $('div.a:last');
    //console.log($lascld);

    //var num = parseInt($authpernum.prop("id").match(/\d+/g), 10) + 1;
    //console.log(num);
    //var idgen = "shipmentAuthorizationDetails_" + num;//+ "__MobileNumber";
    //console.log(idgen);

    //document.getElementsByClassName("clone").

    //cln.getElementsByClassName("NameOfDelegate")[0].id = idgen + "NameOfDelegate";
    //cln.getElementsByClassName("CivilId")[0].id = idgen + "CivilId";
    //cln.getElementsByClassName("Gender")[0].id = idgen + "Gender";
    //cln.getElementsByClassName("Nationality")[0].id = idgen + "Nationality";
    //cln.getElementsByClassName("MobileNumber")[0].id = idgen + "MobileNumber";
    //cln.getElementsByClassName("AttorneyNumber")[0].id = idgen + "AttorneyNumber";
    //cln.getElementsByClassName("AttorneyIssuedby")[0].id = idgen + "AttorneyIssuedby";
    //cln.getElementsByClassName("AttorneyIssueDate")[0].id = idgen + "AttorneyIssueDate";
    //cln.getElementsByClassName("AttorneyExpiryDate")[0].id = idgen + "AttorneyExpiryDate";
    //cln.getElementsByClassName("AttorneyRemarks")[0].id = idgen + "AttorneyRemarks";




}


//$('#cloneDiv').click(function () {


//    // get the last DIV which ID starts with ^= "klon"
//    var $div = $('div[class^="clone"]:last');

//    // Read the Number from that DIV's ID (i.e: 3 from "klon3")
//    // And increment that number by 1
//    var num = parseInt($div.prop("id").match(/\d+/g), 10) + 1;

//    // Clone it and assign the new ID (i.e: from num 4 to ID "klon4")
//    var $klon = $div.clone().prop('id', 'klon' + num);

//    // Finally insert $klon wherever you want
//    $div.after($klon.text('klon' + num));

//    shipmentAuthorizationDetails[0].NameOfDelegate

//});

$(document).on("click", ".button-remove", function () {
    console.log('removable');
    $(this).closest(".clone").remove();
});

var data = [];

function Checkauthpersdata() {
    //alert('begin');
    $("#HidDelegateInfo").val('');
    data.length = 0;
    var authpersarr = [];
    var authpercivilidsarr = [];
    var numberofauthpers = $('.clone').length;
    console.log(numberofauthpers);
    for (var i = 0; i < numberofauthpers; i++) {
        var NameOfDelegate = "shipmentAuthorizationDetails_" + i + "__NameOfDelegate";
        var CivilId = "shipmentAuthorizationDetails_" + i + "__CivilId";
        var Gender = "shipmentAuthorizationDetails_" + i + "__Gender";
        var Nationality = "shipmentAuthorizationDetails_" + i + "__Nationality";
        var MobileNumber = "shipmentAuthorizationDetails_" + i + "__MobileNumber";
        var AttorneyNumber = "shipmentAuthorizationDetails_" + i + "__AttorneyNumber";
        var AttorneyIssuedby = "shipmentAuthorizationDetails_" + i + "__AttorneyIssuedby";
        var AttorneyIssueDate = "shipmentAuthorizationDetails_" + i + "__AttorneyIssueDate";
        var AttorneyExpiryDate = "shipmentAuthorizationDetails_" + i + "__AttorneyExpiryDate";
        var AttorneyRemarks = "shipmentAuthorizationDetails_" + i + "__AttorneyRemarks";
        var fieldnmeid = '#' + CivilId;
        console.log(fieldnmeid);
        authpercivilidsarr.push($(fieldnmeid).val());
        console.log(authpercivilidsarr);
        data.push("NameOfDelegate=" + $('#'+NameOfDelegate).val()+",CivilId=" + $('#'+CivilId).val()+",Gender=" + $('#'+Gender).val()+",Nationality=" + $('#'+Nationality).val()+",MobileNumber=" + $('#'+MobileNumber).val()+",AttorneyNumber=" + $('#'+AttorneyNumber).val()+",AttorneyIssuedby=" + $('#'+AttorneyIssuedby).val()+",AttorneyIssueDate=" + $('#'+AttorneyIssueDate).val()+",AttorneyExpiryDate=" + $('#'+AttorneyExpiryDate).val()+",AttorneyRemarks=" + $('#'+AttorneyRemarks).val());
        console.log(data);


    }
    console.log("duplicates present " + authpercivilidsarr.some(x => authpercivilidsarr.indexOf(x) != authpercivilidsarr.lastIndexOf(x)));
    if (authpercivilidsarr.some(x => authpercivilidsarr.indexOf(x) != authpercivilidsarr.lastIndexOf(x))) {
        $("#modalMessage").text(Duplicateauthperspresent);

        $("#modalDiv").css("display", "block");
        //show modal as duplicate auth present . auth persons with same civil id cannot be entered more tha once
        event.preventDefault();
        //alert('duplicates');
        return false;
    }

    //$('.clone').each(function () {
    //    var NameOfDelegate = $(this).find('#item_NameOfDelegate').val();

    //    var CivilId = $(this).find('#item_CivilId').val();

    //    authpersarr.push(CivilId)

    //    var Gender = $(this).find('#Gender').val();
    //    var Nationality = $(this).find('#Nationality').val();
    //    var MobileNumber = $(this).find('#MobileNumber').val();
    //    var AttorneyNumber = $(this).find('#AttorneyNumber').val();
    //    var AttorneyIssuedby = $(this).find('#AttorneyIssuedby').val();
    //    var AttorneyIssueDate = $(this).find('#AttorneyIssueDate').val();
    //    var AttorneyExpiryDate = $(this).find('#AttorneyExpiryDate').val();
    //    var AttorneyRemarks = $(this).find('#AttorneyRemarks').val();

    //    data.push("NameOfDelegate=" + NameOfDelegate + ',' + "CivilId=" + CivilId + ',' + "Gender=" + Gender
    //        + ',' + "Nationality=" + Nationality
    //        + ',' + "MobileNumber=" + MobileNumber
    //        + ',' + "AttorneyNumber=" + AttorneyNumber
    //        + ',' + "AttorneyIssuedby=" + AttorneyIssuedby
    //        + ',' + "AttorneyIssueDate=" + AttorneyIssueDate
    //        + ',' + "AttorneyExpiryDate=" + AttorneyExpiryDate
    //        + ',' + "AttorneyRemarks=" + AttorneyRemarks);



    //    //var authper = {

    //    //    "NameOfDelegate": $(this).find('#item_NameOfDelegate').val(),
    //    //    "CivilId": $(this).find('#item_CivilId').val(),
    //    //    "Gender": $(this).find('#genderTypeSelect').val(),
    //    //    "Nationality": $(this).find('#companystate').val(),
    //    //    "MobileNumber": $(this).find('#companystate').val(),
    //    //    "AttorneyNumber": $(this).find('#companystate').val(),
    //    //    "AttorneyIssuedby": $(this).find('#companystate').val(),
    //    //    "AttorneyIssueDate": $(this).find('#companystate').val(),
    //    //    "AttorneyExpiryDate": $(this).find('#companystate').val(),
    //    //    "AttorneyRemarks": $(this).find('#companystate').val()
    //    //};
    //    //data.push(authper);


    //    //  alert(firstName);
    //    //  alert(lastName);

    //    //var firstName = $(this).find('.f-name01').val();
    //    //var lastName = $(this).find('.l-name01').val();
    //    //var emailId = $(this).find('.email01').val();
    //    //var alldata = {
    //    //    'FName': firstName,
    //    //    'LName': lastName,
    //    //    'EmailId': emailId
    //    //}
    //    //data.push(alldata);
    //});
    var concatauthpersdata = data.join('~');
    console.log(concatauthpersdata);
    $("#HidDelegateInfo").val(concatauthpersdata);
    return true;
    //$("#HidDelegateInfo").val(data);
    //   document.forms[0].submit;
}


$("#fileUploadCtrl").fileinput({
    browseClass: "btn btn-primary btn-block",
    showCaption: false,
    showRemove: false,
    showUpload: false,
    showZoom: false,
    maxFileSize: 4096,
    maxFilesNum: 1,
    allowedFileExtensions: ['jpg', 'jpeg', 'png', 'pdf'],
    language: "ar",
    browseOnZoneClick: true

}).on('fileuploaderror', function (event, data, msg) {
    $("#fileUploadCtrl").fileinput('clear');
    //$.fn.dataTable.ext.pager.numbers_length = 5;
});

$(".fileinput-cancel-button").css("display", "none");

la = getCookie("culture");
var table = '';
var Orgtable = '';
var oTable = '';
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

        //  "scrollY": "true",
        //  "scrollCollapse": true,
        "pageLength": 5,
        'pagingType': 'numbers',
        "info": false,
        "fnDrawCallback": function (oSettings) {
            DrawTable()
        }


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

        //  "scrollY": "true",
        //  "scrollCollapse": true,
        "pageLength": 5,
        'pagingType': 'numbers',
        "info": false,
        "fnDrawCallback": function (oSettings) {
            DrawTable()
        }


    });
}


function CreateFailure(jqXHR, exception) {
    alert('in');
    var msg = '';
    if (jqXHR.status === 0) {
        msg = 'Not connect.\n Verify Network.';
    } else if (jqXHR.status === 404) {
        msg = 'Requested page not found. [404]';
    } else if (jqXHR.status === 500) {
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
    //console.log(msg);
    $("#modalMessage").text(msgSomeissuehasoccured);

    $("#modalDiv").css("display", "block");
}
function CreateSuccess(data) {
    //alert('in');
    console.log(data);
    //alert(data.message);
    if (data) {
        $("#modalMessage").text(data.message);

        //$("#modalDiv").css("display", "block");

        setTimeout(function () {
            $("#modalDiv").css("display", "block");
        }, 200);

        if (data.StatusCode === "0") {//|| data.StatusCode === 11) {
            setTimeout(function () {
                window.location.href = applicationUrl + 'Request/RequestListFortheUser';
                //console.log(data.URL);
                //console.log(window.location.href);
            }, 1500);
        }
        if (data.StatusCode === "-3") {
            window.location.reload();
        }
    }
    else {
        $("#modalMessage").text(msgSomeissuehasoccured + " in creating request ");

        $("#modalDiv").css("display", "block");
    }

}
