$("#OrgReg").submit(function (e) {

    //$(".inlineBlock").addClass("marginRightLeft10PX");
        $("#myLoadingElement").show();
    });
    $(".glyphicon").click(function(e) {
        $("#myLoadingElement").show();
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

    $(".inlineBlock").bind('DOMSubtreeModified', function (event) {
        if ($(event.target).html().length !== 0) {
            $(event.target).addClass('marginRightLeft10PX');
        }
        else {
            $(event.target).removeClass('marginRightLeft10PX');
        }
    });

    if (ViewBagmodelerror !== null) {
        if (ViewBagmodelerror === "0") {
            //$("#modalDiv").removeClass("animated hinge fast");

            $("#modalDiv").css("display", "block");
            setTimeout(function () {
                window.location.href = '../Dashboard/MenuView'
            }, 2000);
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
            $("#mydiv :input").attr("style", "background-color:#ccc;");
            //$('#btnsubmit').prop('disabled', false);
           //$('#btnsubmit').attr("style", "background-color:#BD3B2E;");
        }
        else
        {
            var res=false;
            if ( $.trim( $('#inputCompanyName').val() ) == ""||$.trim( $('#inputTradelicenceno').val() ) == ""||$.trim( $('#inputCivilId').val() ) == ""||$.trim( $('#inputAuthorizedPerson').val() ) == ""||$.trim( $('#inputCNameinArabic').val() ) == ""||$.trim( $('#inputPOBoxNo').val() ) == ""||$.trim( $('#inputCity').val() ) == ""||$.trim( $('#inputKuwait').val() ) == ""||$.trim( $('#inputBusinessNumber').val() ) == ""||$.trim( $('#inputEmailID').val() ) == "") {
                res=true;
            }
            if (res) {
                //$('#btnsubmit').prop('disabled', true);
            }
            else
            {
                $('#btnsubmit').prop('disabled', false);
            }
            $( ".textval" ).keydown(function() {
                var res1=false;
                if ( $.trim( $('#inputCompanyName').val() ) == ""||$.trim( $('#inputTradelicenceno').val() ) == ""||$.trim( $('#inputCivilId').val() ) == ""||$.trim( $('#inputAuthorizedPerson').val() ) == ""||$.trim( $('#inputCNameinArabic').val() ) == ""||$.trim( $('#inputPOBoxNo').val() ) == ""||$.trim( $('#inputCity').val() ) == ""||$.trim( $('#inputKuwait').val() ) == ""||$.trim( $('#inputBusinessNumber').val() ) == ""||$.trim( $('#inputEmailID').val() ) == "") {
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
                if ( $.trim( $('#inputCompanyName').val() ) == ""||$.trim( $('#inputTradelicenceno').val() ) == ""||$.trim( $('#inputCivilId').val() ) == ""||$.trim( $('#inputAuthorizedPerson').val() ) == ""||$.trim( $('#inputCNameinArabic').val() ) == ""||$.trim( $('#inputPOBoxNo').val() ) == ""||$.trim( $('#inputCity').val() ) == ""||$.trim( $('#inputKuwait').val() ) == ""||$.trim( $('#inputBusinessNumber').val() ) == ""||$.trim( $('#inputEmailID').val() ) == "") {
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
                if ( $.trim( $('#inputCompanyName').val() ) == ""||$.trim( $('#inputTradelicenceno').val() ) == ""||$.trim( $('#inputCivilId').val() ) == ""||$.trim( $('#inputAuthorizedPerson').val() ) == ""||$.trim( $('#inputCNameinArabic').val() ) == ""||$.trim( $('#inputPOBoxNo').val() ) == ""||$.trim( $('#inputCity').val() ) == ""||$.trim( $('#inputKuwait').val() ) == ""||$.trim( $('#inputBusinessNumber').val() ) == ""||$.trim( $('#inputEmailID').val() ) == "") {
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