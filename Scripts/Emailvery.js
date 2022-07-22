

$(document).ready(function ()
{
    //var applicationURL = 'http://10.10.26.226/eServiceProd/';
    //var applicationURL = 'http://10.10.26.226/eServiceProdV2PP/';
    var applicationURL = 'http://10.10.26.226/eSOnePPIA/';
  //  var applicationURL = 'https://eservices.kgac.gov.kw/';
    //'http://10.10.26.226/eServices';

    $("#EmailVerificationResend").click(function () {
        $.ajax({
            type: "GET",
            url: applicationUrl + "registration/EmailVerificationResend",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                return false;
            },
            success: function (result) {
                console.log(result);
                Showmessageonresend(EmailVerificationResend);
                event.preventDefault();
            },
            failure: function (errorThrown) {
                console.log(errorThrown);
                return false;
            }
        }
        );
    });
    $("#MobileVerificationResend").click(function () {
        $.ajax({
            type: "GET",
            url: applicationUrl + "registration/MobileVerificationResend",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                return false;
            },
            success: function (result) {
                console.log(result);
                Showmessageonresend(MobileVerificationResend);
                event.preventDefault();
            },
            failure: function (errorThrown) {
                console.log(errorThrown);
                return false;
            }
        }
        );
    });

    $("#btnsubmitVerification").click(function () {

        if ($("#inputEmailCode").length) {
            var inputEmailCode = $("#inputEmailCode").val();
            if (!inputEmailCode) {
                event.preventDefault();
                $("#emailIdErrorMsg").css("display", "block");
            }

            else {
                $("#emailIdErrorMsg").css("display", "none");
            }
        }
        if ($("#inputMobileCode").length) {
            var inputMobileCode = $("#inputMobileCode").val();
            if (!inputMobileCode) {
                event.preventDefault();
                $("#mobileErrorMsg").css("display", "block");
            }

            else {
                $("#mobileErrorMsg").css("display", "none");
            }
        }
        
    });

    if (msg)
    {
        $("#modalMessage").text(msg);
        $("#modalDiv").css("display", "block");

        if (success)
        {
            if (success == "true")
            {
                setTimeout(function () {
                    window.location.assign(applicationURL + 'registration/index');
                    //window.location.assign(applicationURL + 'registration/Start');
                }, 3000);
            }

        }
    }

});


function Showmessageonresend(msg) {
    if (msg) {
        $("#modalMessage").text(msg);
        $("#modalDiv").css("display", "block");
    }
}