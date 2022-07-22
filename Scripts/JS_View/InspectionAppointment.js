
//var AlreadyScheduledVehicle = 'ViewBag.AlreadyScheduledVehicle';
//var DeclarationId;
var PortID;
var PortName;
var DeclarationType;
var DeclarationIduid;

var choosed = '';

var errorMsgEmptyDeclaration = "Please Write a Valid Declaration Number";

var DeclComp = '';
var tempDelarationNo = '';

var enablefieldsforinputchange = true;

var VehicleListArray = new Array();

function SwitchDecType(event) {
    var Id = event.target.id;
   // console.log(Id);
    if (enablefieldsforinputchange) {//to allow user to change field value only if user clicked on clear button or before clicking on verify button
        if (Id === "DecId" || Id === "DeclarationId" || Id === "Didlabel" || Id === "BayanType" || Id === "BayanPort" || Id === "BayanYear" || Id === "DeclGroupontrol") {
            document.getElementById("TempDeclarationId").value = '';
            document.getElementById("TempDeclarationId").readOnly = true;

            document.getElementById("DeclarationId").readOnly = false;
            document.getElementById("DecId").checked = true;


        
            // document.getElementById("BayanType").disabled = "";
            // document.getElementById("BayanPort").disabled = "";
            // document.getElementById("BayanYear").disabled = "";
            //document.getElementById("").readOnly = false;

            // document.getElementById("BayanType").readOnly = false;
            // document.getElementById("BayanPort").readOnly = false;
            // document.getElementById("BayanYear").readOnly = false;
        
        
            document.getElementById("BayanType").classList.remove("hideclass");
            document.getElementById("BayanPort").classList.remove("hideclass");
            document.getElementById("BayanYear").classList.remove("hideclass");
        
            choosed = "declarationId";
        
        }
        else if (Id === "TDecId" || Id === "TempDeclarationId" || Id === "TDidlabel") {
            document.getElementById("DeclarationId").value = '';
            document.getElementById("TempDeclarationId").readOnly = false;

            document.getElementById("DeclarationId").readOnly = true;
            document.getElementById("TDecId").checked = true;


            // document.getElementById("BayanType").disabled = "disabled";
            // document.getElementById("BayanPort").disabled = "disabled";
            // document.getElementById("BayanYear").disabled = "disabled";

        
            // document.getElementById("BayanType").readOnly = true;
            // document.getElementById("BayanPort").readOnly = true;
            // document.getElementById("BayanYear").readOnly = true;

        
            document.getElementById("BayanType").classList.add("hideclass");
            document.getElementById("BayanPort").classList.add("hideclass");
            document.getElementById("BayanYear").classList.add("hideclass");


        
            document.getElementById('BayanType').selectedIndex = 0;
            document.getElementById('BayanPort').selectedIndex = 0;
            document.getElementById('BayanYear').selectedIndex = 0;
        

            choosed = "tempDeclarationId";
        }
    }
}
function VerifyAndFetch(event) {
    //onChange = "inputChange(event)"
    //var SecurityCode = $('input[name=SecurityCode]').val();
    //var DONumber = $('input[name=DONumber]').val();
    var DeclarationId = $('input[name=DeclarationId]').val();

    var lgu = getCookie("culture");

    if (lgu.indexOf("ar") != -1)
    {
        errorMsgEmptyDeclaration = "الرجاء كتابة رقم بيان جمركي صحيح";
    }


    if (!choosed)
    {
        if (!document.body.contains(document.getElementById('TempDeclarationIdValidVMSG'))) {
            $("#TempDeclarationIdValid").after('<span id="TempDeclarationIdValidVMSG" class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true">' + errorMsgEmptyDeclaration + '</span>');
            //console.log(lgu);
        }
        else {
            var TempDeclarationIdValidVMSG = document.getElementById("TempDeclarationIdValidVMSG");
            TempDeclarationIdValidVMSG.remove();
            $("#TempDeclarationIdValid").after('<span id="TempDeclarationIdValidVMSG" class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true">' + errorMsgEmptyDeclaration + '</span>');
            //console.log("here  " + lgu);
        }
        return;     
    }


    if (choosed == "declarationId")
    {
        DeclComp = document.getElementById("BayanType").value + '/' + $("input[name='DeclarationId']").val() + '/' + document.getElementById("BayanPort").value + document.getElementById("BayanYear").value;

        tempDelarationNo = '';

        if (DeclarationId === "" || !DeclarationId || isNaN(DeclarationId))// && TempDeclarationId === "") 
        {   ////console.log('SecurityCode');
            if (!document.body.contains(document.getElementById('TempDeclarationIdValidVMSG')))
            {
                $("#TempDeclarationIdValid").after('<span id="TempDeclarationIdValidVMSG" class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true">' + errorMsgEmptyDeclaration + '</span>');
                //console.log(lgu);
            }
            else
            {
                var TempDeclarationIdValidVMSG = document.getElementById("TempDeclarationIdValidVMSG");
                TempDeclarationIdValidVMSG.remove();
                $("#TempDeclarationIdValid").after('<span id="TempDeclarationIdValidVMSG" class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true">' + errorMsgEmptyDeclaration + '</span>');
                //console.log("here  " + lgu);
            }
            return;            
        }
    }
    else if (choosed == "tempDeclarationId")
    {
        tempDelarationNo = document.getElementById("TempDeclarationId").value;
        DeclComp = '';

        if (!tempDelarationNo)
        {

            if (!document.body.contains(document.getElementById('TempDeclarationIdValidVMSG'))) {
                $("#TempDeclarationIdValid").after('<span id="TempDeclarationIdValidVMSG" class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true">' + errorMsgEmptyDeclaration + '</span>');
                //console.log(lgu);
            }
            else {
                var TempDeclarationIdValidVMSG = document.getElementById("TempDeclarationIdValidVMSG");
                TempDeclarationIdValidVMSG.remove();
                $("#TempDeclarationIdValid").after('<span id="TempDeclarationIdValidVMSG" class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true">' + errorMsgEmptyDeclaration + '</span>');
                //console.log("here  " + lgu);
            }
            return;  

        }
       

    }

    
   
    if (DeclarationId !== "" || TempDeclarationId !== "")
    {
        


        //var datatp = { DeclarationId: DeclComp, TempDeclarationId: '' };
        //console.log(datatp);
        console.log(tempDelarationNo);
        console.log(DeclComp);
        //debugger;
        $.ajax({
            type: "post",
            url: applicationUrl + "Appointments/GetVehicleList",

            data: { "DeclarationId": DeclComp, "TempDeclarationId": tempDelarationNo }, //datatp,// $("input[name='TempDeclarationId']").val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                //console.log(data);


                //console.log('VALID');


                if (data && data.StatusCode === 1) {

                    enablefieldsforinputchange = false;

                    
                    
                    document.getElementById('DecId').disabled = true;
                    document.getElementById("TDecId").disabled = true;

                    var ShowIfVehiclesAvailableelsa = document.getElementsByClassName('ShowIfVehiclesAvailable');
                    if (ShowIfVehiclesAvailableelsa && ShowIfVehiclesAvailableelsa.length > 0) {
                      for (ik = 0; ik < ShowIfVehiclesAvailableelsa.length; ik++) {
                        ShowIfVehiclesAvailableelsa[ik].setAttribute("style","display:block" );
                          }
                    }
                    
        document.getElementById("BayanType").classList.add("hideclass");
        document.getElementById("BayanPort").classList.add("hideclass");
        document.getElementById("BayanYear").classList.add("hideclass");

                    
                    document.getElementById('Verify').disabled = true;
                    document.getElementById("SubmitSchedule").disabled = false;

                    document.getElementById('DeclarationId').readOnly = true;
                    document.getElementById('TempDeclarationId').readOnly = true;

                    document.getElementById("DeclarationId").classList.remove("hoverpointer");
                    document.getElementById("DeclarationId").classList.add("hideclass");
                    // document.getElementById("TempDeclarationId").classList.remove("hoverpointer");
                    // document.getElementById("TempDeclarationId").classList.add("hideclass");

                    // document.getElementById("DecId").classList.remove("hoverpointer");
                    //document.getElementById("DecId").classList.add("hideclass");
                    //document.getElementById("TDecId").classList.remove("hoverpointer");
                    //document.getElementById("TDecId").classList.add("hideclass");

                    document.getElementById("Didlabel").classList.remove("hoverpointer");
                    document.getElementById("Didlabel").classList.add("hideclass");
                    document.getElementById("TDidlabel").classList.remove("hoverpointer");
                    document.getElementById("TDidlabel").classList.add("hideclass");


                    if (document.body.contains(document.getElementById('TempDeclarationIdValidVMSG'))) {
                        var TempDeclarationIdValidVMSG = document.getElementById("TempDeclarationIdValidVMSG");
                        TempDeclarationIdValidVMSG.remove();
                    }


                    var vlist = $('#vehlisttable').DataTable();

                    //var vlist =  $('#vehlisttable').DataTable({
                    //    responsive: true
                    //});

                    vlist.clear().draw();
                    $('#vehlisttable-select-all').prop('checked', false);

                    if (data.Vehicles) {
                        var Vehicles = data.Vehicles;

                        //var lefttoright = '\u202A';

                        $.each(Vehicles, function (index, element) {


                            vlist.row.add([
                                '',
                                '<input type="checkbox" name="' + Vehicles[index].VehicleID + '" class="activateselectedservice text-center" id=' + Vehicles[index].VehicleID + ' />'
                                , Vehicles[index].VehicleID
                                , Vehicles[index].VehiclePlateNumber  
                                //, data[index].ContainerNumber
                                , Vehicles[index].ContainerNumber

                                , Vehicles[index].Weight
                                , Vehicles[index].DriverName
                                , Vehicles[index].MobileNumber
                                //,''
                            ]).draw();
                            //DeclarationId = data[index].DeclarationId;
                            PortId = Vehicles[index].PortId;
                            PortName = Vehicles[index].PortName;
                            // PortName = Vehicles[index].PortName;
                            DeclarationType = Vehicles[index].DeclarationType;
                            //DeclarationIduid = data[index].DeclarationIduid;

                        });

                        document.getElementById("PortId").value = PortId;
                        document.getElementById("PortName").value = PortName;
                        document.getElementById("DeclarationType").value = DeclarationType;
                    }


                    //var DONumberVMSGC = document.getElementById("DONumberVMSG");
                    //DONumberVMSGC.remove();
                    //var SecurityCodeVMSGC = document.getElementById("SecurityCodeVMSG");
                    //SecurityCodeVMSGC.remove();

                    //document.getElementById('DeclarationId').value = DeclarationId;// data[index].DeclarationId;
                    //document.getElementById('PortID').value = PortID;// data[index].PortID;

                    //var InspectionDate = document.getElementById("InspectionDate");
                    //InspectionDate.classList.remove("hideclass");
                    //var InspectionRound = document.getElementById("InspectionRound");
                    //InspectionRound.classList.remove("hideclass");
                    //var vehlisttable = document.getElementById("vehlisttable");
                    //vehlisttable.classList.remove("hideclass");
                    //var SubmitSchedule = document.getElementById("SubmitSchedule");
                    //SubmitSchedule.classList.remove("disablebuttonclass");


                    var InspectionDate = document.getElementById("InspectionDate");
                    InspectionDate.classList.remove("hideclass");
                    var InspectionZone = document.getElementById("InspectionZone");
                    InspectionZone.classList.remove("hideclass");
                    var InspectionTerminal = document.getElementById("InspectionTerminal");
                    InspectionTerminal.classList.remove("hideclass");
                    var InspectionRound = document.getElementById("InspectionRound");
                    InspectionRound.classList.remove("hideclass");
                    var vehlisttable = document.getElementById("vehlisttable");
                    vehlisttable.classList.remove("hideclass");
                    var SubmitSchedule = document.getElementById("SubmitSchedule");
                    SubmitSchedule.classList.remove("disablebuttonclass");



                    var InspectionZones = data.InspectionZone;
                    var InspectionTerminals = data.InspectionTerminal;
                    var InspectionRounds = data.InspectionRound;

                    $('#InspectionZone').children('option:not(:first)').remove();
                    $('#InspectionTerminal').children('option:not(:first)').remove();
                    $('#InspectionRound').children('option:not(:first)').remove();


                    for (var i = 0; i < InspectionZones.length; i++) {
                        $('#InspectionZone').append('<option value=' + InspectionZones[i].ZoneId + '>' + InspectionZones[i].ZoneName + '</option>');
                    }
                    for (var j = 0; j < InspectionTerminals.length; j++) {
                        $('#InspectionTerminal').append('<option value=' + InspectionTerminals[j].TerminalId + '>' + InspectionTerminals[j].TerminalName + '</option>');
                    }
                    for (var k = 0; k < InspectionRounds.length; k++) {
                        $('#InspectionRound').append('<option value=' + InspectionRounds[k].RoundId + '>' + InspectionRounds[k].RoundName + '</option>');
                    }

                }
                //else if (data && data.StatusCode === -1) 
                //{
                //    $("#modalMessage").text(data.message);

                //    $("#modalDiv").css("display", "block");

                //    //setTimeout(function () {
                //    //    window.location.reload();
                //    //}, 1500);
                //}
                else {
                    $("#modalMessage").text(data.message);// $("#modalMessage").text("No Vehicles available for the provided declaration");

                    $("#modalDiv").css("display", "block");
                }


                //event.target.disabled = true;
            }
            , error: function (jqXHR, exception) {
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
        });


    }
}
function Clear() {
    //document.getElementById('DONumber').value = '';
    //document.getElementById('SecurityCode').value = '';

    enablefieldsforinputchange = true;

    document.getElementById('Verify').disabled = false;

    
    document.getElementById('DecId').disabled = false;
    document.getElementById("TDecId").disabled = false;

    document.getElementById("SubmitSchedule").disabled = true;

    document.getElementById('DeclarationId').value = '';
     document.getElementById('TempDeclarationId').value = '';
    document.getElementById('PortName').value = '';
    // document.getElementById('DeclarationIduid').value = '';
    document.getElementById('DeclarationType').value = '';
    document.getElementById('InspectionDate').value = '';
    document.getElementById('InspectionZone').selectedIndex = 0;
    document.getElementById('InspectionTerminal').selectedIndex = 0;
    document.getElementById('InspectionRound').selectedIndex = 0;
    document.getElementById('DeclarationId').readOnly = false;
    document.getElementById('TempDeclarationId').readOnly = false;
    document.getElementById('DecId').checked = false;
    document.getElementById('TDecId').checked = false;
    document.getElementById('BayanType').selectedIndex = 0;
    document.getElementById('BayanPort').selectedIndex = 0;
    document.getElementById('BayanYear').selectedIndex = 0;

    var InspectionDate = document.getElementById("InspectionDate");
    InspectionDate.classList.add("hideclass");
    var InspectionZone = document.getElementById("InspectionZone");
    InspectionZone.classList.add("hideclass");
    var InspectionTerminal = document.getElementById("InspectionTerminal");
    InspectionTerminal.classList.add("hideclass");
    var InspectionRound = document.getElementById("InspectionRound");
    InspectionRound.classList.add("hideclass");
    var vehlisttable = document.getElementById("vehlisttable");
    vehlisttable.classList.add("hideclass");
    // var SubmitSchedule = document.getElementById("SubmitSchedule");
    // SubmitSchedule.classList.add("disablebuttonclass");


    document.getElementById("DeclarationId").classList.add("hoverpointer");
    document.getElementById("DeclarationId").classList.remove("hideclass");
    // document.getElementById("TempDeclarationId").classList.add("hoverpointer");
    // document.getElementById("TempDeclarationId").classList.remove("hideclass");
    document.getElementById("DecId").classList.add("hoverpointer");
    document.getElementById("DecId").classList.remove("hideclass");
    document.getElementById("TDecId").classList.add("hoverpointer");
    document.getElementById("TDecId").classList.remove("hideclass");

    document.getElementById("Didlabel").classList.add("hoverpointer");
    document.getElementById("Didlabel").classList.remove("hideclass");
    document.getElementById("TDidlabel").classList.add("hoverpointer");
    document.getElementById("TDidlabel").classList.remove("hideclass");

    
    document.getElementById("BayanType").classList.add("hoverpointer");
    document.getElementById("BayanType").classList.remove("hideclass");
    document.getElementById("BayanPort").classList.add("hoverpointer");
    document.getElementById("BayanPort").classList.remove("hideclass");
    document.getElementById("BayanYear").classList.add("hoverpointer");
    document.getElementById("BayanYear").classList.remove("hideclass");

    document.getElementById("SubmitSchedule").disabled = true;


    if (document.body.contains(document.getElementById('VehicleListVMSG'))) {
        var helperclassforjstoremove = document.getElementById("VehicleListVMSG");
        helperclassforjstoremove.remove();
    }


    if (document.body.contains(document.getElementById('TempDeclarationIdValidVMSG'))) {
        var TempDeclarationIdValidVMSG = document.getElementById("TempDeclarationIdValidVMSG");
        TempDeclarationIdValidVMSG.remove();
    }

    var velist = $('#vehlisttable').DataTable();

    velist.clear().draw();

    var helperclassforjsip = document.getElementsByClassName('helperclassforjs');
    if (helperclassforjsip) {
        for (ik = 0; ik < helperclassforjsip.length; ik++) {
            // helperclassforjsip[ik].setAttribute('data-val', true);
            helperclassforjsip[ik].classList.remove("input-validation-error");
            helperclassforjsip[ik].classList.add("valid");
        }
    }
    var helperclassforjstoremovevaldmsg = document.getElementsByClassName('helperclassforjsval');
    if (helperclassforjstoremovevaldmsg && helperclassforjstoremovevaldmsg.length > 0) {
        for (ik = 0; ik < helperclassforjstoremovevaldmsg.length; ik++) {
            // helperclassforjstoremovevaldmsg[ik].setAttribute('data-val', true);
            helperclassforjstoremovevaldmsg[ik].classList.remove("field-validation-error");
            helperclassforjstoremovevaldmsg[ik].classList.add("field-validation-valid");
            // console.log(helperclassforjstoremovevaldmsg[ik]);
            // console.log(helperclassforjstoremovevaldmsg[ik].children[0]);
            //helperclassforjstoremovevaldmsg[ik].removeChild(helperclassforjstoremovevaldmsg[ik].children[0])
        }
    }

    var ShowIfVehiclesAvailableels = document.getElementsByClassName('ShowIfVehiclesAvailable');
    if (ShowIfVehiclesAvailableels && ShowIfVehiclesAvailableels.length > 0) {
        for (ik = 0; ik < ShowIfVehiclesAvailableels.length; ik++) {
            ShowIfVehiclesAvailableels[ik].setAttribute("style","display:none" );
        }
    }

    //$('#vehlisttable tbody').empty();

    var vlist = $('#vehlisttable').DataTable();
    vlist.clear().draw();
    $('#vehlisttable-select-all').prop('checked', false);
    //document.getElementById('DeclarationId').value = '';
    document.getElementById('PortId').value = '';
    //document.getElementById('UpdateRequest').value = 'false';
    //document.getElementById('EditableRequest').value = 'false';
    document.getElementById('SelectedVehicleList').value = '';
    if (document.body.contains(document.getElementById('TempDeclarationIdValidVMSG'))) {
        var TempDeclarationIdValidVMSG1 = document.getElementById("TempDeclarationIdValidVMSG");
        TempDeclarationIdValidVMSG1.remove();
    }


}

//$('input[name=SecurityCode]').on('blur', function (e) {
//    var SecurityCode = $('input[name=SecurityCode]').val();
//    var DONumber = $('input[name=DONumber]').val();
//    if (DONumber !== "" && SecurityCode !== "") {
//        getmocidetails('ImporterLicenseNumber');

//    }
//});
var table = $('#vehlisttable').DataTable({


    "language":
    {
        //"url": applicationUrl + "scripts/arabic.json"
        //"emptyTable": ""// NoVehiclesforgivendeclaration
    },
    "sEmptyTable": "",
    "bPaginate": false,
    "bFilter": false,
    "bInfo": false,
    //"pageLength": 5,
    'pagingType': 'numbers',
    "bLengthChange": false,
    'columnDefs': [{
        'targets': 1,
        'searchable': false,
        'orderable': false,
        'className': 'dt-body-center'
        //,'render': function (data, type, full, meta) {
        //    return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
        //}
    },
    {
        "targets": 2,
        "visible": false,
        "searchable": false
    },
    {
        "targets": 3,
        'searchable': false,
        'orderable': false,
        'className': 'dt-body-center'
    }
        //   ,
        //{
        //       "targets": 2,
        //       'searchable': false,
        //       'orderable': false,
        //       'className': 'dt-body-center'
        //   }
    ]
    , 'order': [[2, 'asc']]
});
$('#vehlisttable-select-all').on('click', function () {
    var rows = table.rows({ 'search': 'applied' }).nodes();
    $('input[type="checkbox"]', rows).prop('checked', this.checked);
    if (this.checked) {
        VehicleListArray = table
            .column(2)
            .data()
            .toArray();
        //console.log(VehicleListArray);
    } else {
        VehicleListArray.length = 0;
        //console.log(VehicleListArray);
    }
});
$('#vehlisttable tbody').on('change', 'input[type="checkbox"]', function () {
    if (!this.checked) {
        $('#vehlisttable-select-all').prop('checked', false);
        var svcindx = VehicleListArray.indexOf(this.name);
        VehicleListArray.splice(svcindx, 1);
        //console.log(VehicleListArray);
    }
    else {
        //console.log(VehicleListArray.length);
        //console.log(table.column(1).data().length);
        if (VehicleListArray.indexOf(this.name) === -1) VehicleListArray.push(this.name);
        if (VehicleListArray.length === table.column(2).data().length) {
            $('#vehlisttable-select-all').prop('checked', true);
        }
        //console.log(VehicleListArray);
        if (document.body.contains(document.getElementById('VehicleListVMSG'))) {
            var VehicleListVMSG1 = document.getElementById("VehicleListVMSG");
            VehicleListVMSG1.remove();
        }
    }


    //if (ORGARRAY.length > 0 && SVCARRAY.length > 0) {
    //    if ($("#Datatablevalidation").length > 0)

    //        $("#Datatablevalidation").remove();
    //}
    //else {
    //    if ($("#Datatablevalidation").length <= 0)
    //        $("#DTVald").after('<p class="contact" id="Datatablevalidation"><span class="text-danger field-validation-error" data-valmsg-for="Pass" data-valmsg-replace="true"><span for="Pass" class="">' + MinCriteriamsg + '</span></span></p>');

    //}

});
function TakeSelectedVehicles(event) {
    if (VehicleListArray.length > 0) {
        //console.log('vehicle selected');
        $("#SelectedVehicleList").val(VehicleListArray.join(','));
        if (document.body.contains(document.getElementById('VehicleListVMSG'))) {
            var VehicleListVMSG = document.getElementById("VehicleListVMSG");
            VehicleListVMSG.remove();
        }
    }
    else {
        //console.log('no vehicle selected');
        if (!document.body.contains(document.getElementById('VehicleListVMSG'))) {
            //console.log('no vehicle selected msg added');
            $("#vehlisttable_wrapper").after('<span id="VehicleListVMSG" class="text-danger field-validation-error  " data-valmsg-for="Pass" data-valmsg-replace="true">' + AtleastOnevehiclerequiredforappointment + '</span>');
        }
        event.preventDefault();
    }


}
var GovernorateRegionPostalCodes;

$(document).ready(function () {
    //if (AlreadyScheduledVehicle === "1") {
    //    $("#modalMessage").text("Already Booked Vehicle has been selected for Appointment");

    //    $("#modalDiv").css("display", "block");
    //}

    //document.getElementById("TempDeclarationId").readOnly = true;
    var IfSelectedVehicleList = document.getElementById('SelectedVehicleList').value;
    if (IfSelectedVehicleList) {
        VehicleListArray = IfSelectedVehicleList.split(',');
    }

    $("input[name='InspectionDate']").datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+10"
    }).attr('readonly', 'readonly');

    //$("input[name='VehicleList']").multiselect();

    var ShowIfVehiclesAvailableelsak = document.getElementsByClassName('ShowIfVehiclesAvailable');
    if (ShowIfVehiclesAvailableelsak && ShowIfVehiclesAvailableelsak.length > 0) {
      for (ik = 0; ik < ShowIfVehiclesAvailableelsak.length; ik++) {
        ShowIfVehiclesAvailableelsak[ik].setAttribute("style",ShowIfVehiclesAvailable );
          }
    }

});

function CreateFailure(jqXHR, exception) {
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
    //console.log(data);
    //alert(data.message);
    if (data) {
        $("#modalMessage").text(data.message);

        //$("#modalDiv").css("display", "block");

        setTimeout(function () {
            $("#modalDiv").css("display", "block");
        }, 200);

        if (data.StatusCode === 1 || data.StatusCode === 11) {
            setTimeout(function () {
                window.location.href = applicationUrl + 'Request/RequestListFortheUser';
                //console.log(data.URL);
                //console.log(window.location.href);
            }, 1500);
        }
        if (data.StatusCode === -3) {
            window.location.reload();
        }
    }
    else {
        $("#modalMessage").text(msgSomeissuehasoccured + " in creating request ");

        $("#modalDiv").css("display", "block");
    }

}

    //$('#InspectionZone').change(function () {
    //    $.ajax({
    //        type: "post",
    //        url: "/eSOnePPIA/Appointments/GetInspectionRounds",
    //        //  url: "/registration/GetCitiesofGovernorate",

    //        data: { PortId: $('#PortId').val(), DeclarationType: $('#DeclarationType').val(), InspectionZone: $('#InspectionZone').val() },
    //        datatype: "json",
    //        traditional: true,
    //        success: function (data) {

    //            //console.log(data);
    //            $('#InspectionRound').children('option:not(:first)').remove();
    //            //var Region = "<select id='Region'>";
    //            //Region = Region + '<option value="">--Select--</option>';
    //            if (data) {
    //                for (var i = 0; i < data.length; i++) {
    //                    //district = district + '<option value=' + data[i].RegionID + '>' + data[i].RegionName + '</option>';
    //                    $('#InspectionRound').append('<option value=' + data[i].RoundId + '>' + data[i].RoundName + '</option>');
    //                }
    //            }
    //            //district = district + '</select>';
    //            //$('#District').html(district);
    //        }
    //        , error: function (jqXHR, exception) {
    //            var msg = '';
    //            if (jqXHR.status === 0) {
    //                msg = 'Not connect.\n Verify Network.';
    //            } else if (jqXHR.status == 404) {
    //                msg = 'Requested page not found. [404]';
    //            } else if (jqXHR.status == 500) {
    //                msg = 'Internal Server Error [500].';
    //            } else if (exception === 'parsererror') {
    //                msg = 'Requested JSON parse failed.';
    //            } else if (exception === 'timeout') {
    //                msg = 'Time out error.';
    //            } else if (exception === 'abort') {
    //                msg = 'Ajax request aborted.';
    //            } else {
    //                msg = 'Uncaught Error.\n' + jqXHR.responseText;
    //            }
    //            //console.log(msg);
    //            $("#modalMessage").text(msgSomeissuehasoccured + " in fetching Round details ");

    //            $("#modalDiv").css("display", "block");
    //        }
    //    });
    //});
