$(document).ready(function () {
            $('#inputenddate').datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "-60:+10"
            }).attr('readonly', 'readonly');
            $('#inputissuancedate').datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                maxDate: '0',
                minDate:'',
                changeYear: true,
                yearRange: "-60:+10"
            }).attr('readonly', 'readonly');
        });

$(document).ready(function () {
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

    if (ViewBagmodelerror !== null) {
        if (ViewBagmodelerror === "0") {
            //$("#modalDiv").removeClass("animated hinge fast");

            $("#modalDiv").css("display", "block");
        }
    }

    if (GotoReqList !== null) {
        if (GotoReqList === "0") {
            setTimeout(function () {
                window.location.href = '../Request/RequestListForTheUser'
            }, 2000);
        }
    }

            if (result=="0") {
                $("#mydiv :input").attr("disabled", true);
                //$("#mydiv :input").attr("style", "background-color:#ccc;width:100%;");
                $('.btn').prop('disabled', false);
                //$('.btn').attr("style", "background-color:#BD3B2E;");
            }
            else
            {
                var res=false;
                if ($('#temporaryid').is(":checked")) {
                    if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputyear').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                        res=true;
                    }
                }
                else
                {
                    if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                        res=true;
                    }
                }
           
                if(!($('#permanentid').is(":checked") || $('#temporaryid').is(":checked"))){
                    res=true;
                }
                if (res) {
                    $('#btnsubmit').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit').prop('disabled', false);
                }
                $('input[type="radio"]').click(function(){
                    var res1=false;
                    if ($('#temporaryid').is(":checked")) {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputyear').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res1=true;
                        }
                    }
                    else
                    {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res1=true;
                        }
                    }
                    if(!($('#permanentid').is(":checked") || $('#temporaryid').is(":checked"))){
                        res1=true;
                    }
                    if (res1) {
                        $('#btnsubmit').prop('disabled', true);
                    }
                    else
                    {
                        $('#btnsubmit').prop('disabled', false);
                    }
                })
                $( ".textval" ).keydown(function() {
                    var res1=false;
                    if ($('#temporaryid').is(":checked")) {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputyear').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res1=true;
                        }
                    }
                    else
                    {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res1=true;
                        }
                    }
                    if(!($('#permanentid').is(":checked") || $('#temporaryid').is(":checked"))){
                        res1=true;
                    }
                    if (res1) {
                        $('#btnsubmit').prop('disabled', true);
                    }
                    else
                    {
                        $('#btnsubmit').prop('disabled', false);
                    }
                })
                $( ".textval" ).keyup(function() {
                    var res1=false;
                    if ($('#temporaryid').is(":checked")) {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputyear').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res1=true;
                        }
                    }
                    else
                    {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res1=true;
                        }
                    }
                    if(!($('#permanentid').is(":checked") || $('#temporaryid').is(":checked"))){
                        res1=true;
                    }
                    if (res1) {
                        $('#btnsubmit').prop('disabled', true);
                    }
                    else
                    {
                        $('#btnsubmit').prop('disabled', false);
                    }
                })
                $( ".textval" ).click(function() {
                    var res2=false;
                    if ($('#temporaryid').is(":checked")) {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputyear').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res2=true;
                        }
                    }
                    else
                    {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res2=true;
                        }
                    }
                    if(!($('#permanentid').is(":checked") || $('#temporaryid').is(":checked"))){
                        res2=true;
                    }
                    if (res2) {
                        $('#btnsubmit').prop('disabled', true);
                    }
                    else
                    {
                        $('#btnsubmit').prop('disabled', false);
                    }
                })
                $( ".mydate" ).change(function() {
                    var res2=false;
                    if ($('#temporaryid').is(":checked")) {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputyear').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res2=true;
                        }
                    }
                    else
                    {
                        if ($.trim( $('#inputLicenseno').val() ) == ""||$.trim( $('#inputissuancedate').val() ) == ""||$.trim( $('#inputenddate').val() ) == "") {
                            res2=true;
                        }
                    }
                    if(!($('#permanentid').is(":checked") || $('#temporaryid').is(":checked"))){
                        res2=true;
                    }
                    if (res2) {
                        $('#btnsubmit').prop('disabled', true);
                    }
                    else
                    {
                        $('#btnsubmit').prop('disabled', false);
                    }
                })
            }
        });