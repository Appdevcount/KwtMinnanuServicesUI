$(document).ready(function () {
    $("#resetPasswordBtn").click(function () {

        var email = $("#emailId").val();
        var captcha = $("#inputCaptcha").val();

        if (!email) {
            event.preventDefault();
            $("#emailIdErrorMsg").css("display", "block");
        }

        else {
            $("#emailIdErrorMsg").css("display", "none");
        }


        if (!captcha) {
            event.preventDefault();
            $("#captchaErrorMsg").css("display", "block");
        }

        else {
            $("#captchaErrorMsg").css("display", "none");
        }

    });

    $("#resetPassBtn").click(function () {

        //var captcha = $("#inputCaptcha").val();
        //if (!captcha) {
        //    event.preventDefault();
        //    $("#captchaErrorMsg").css("display", "block");
        //}
        //else {
        //    $("#captchaErrorMsg").css("display", "none");
        //}



        var otp = $("#inputcode").val();
        var newPass = $("#inputpwd").val();
        var cPass = $("#inputcpwd").val();

        if (!otp) {
            event.preventDefault();
            $("#inputcodeErrorMsg").css("display", "block");
        }

        else {
            $("#inputcodeErrorMsg").css("display", "none");
        }

       

        if (!newPass) {
            event.preventDefault();
            $("#newPwdErrorMsg").css("display", "block");
            $("#Passworddoesntmatch").css("display", "none");
        }

        else {
            $("#newPwdErrorMsg").css("display", "none");
        }


        if (!cPass) {
            event.preventDefault();
            $("#CPasswordErrorMsg").css("display", "block");
            $("#Passworddoesntmatch").css("display", "none");
        }

        else {
            $("#CPasswordErrorMsg").css("display", "none");
        }

        if (cPass && newPass) {
            if (newPass.length > 0 && cPass.length > 0) {
                if (cPass !== newPass) {

                    event.preventDefault();
                    $("#Passworddoesntmatch").css("display", "block");
                }

            }
        }

    });

    if (sts && sts != "")
    {
        if (err === "0") {
            //alert(sts);
            //alert(err);
            //event.preventDefault();
            $("#captchaErrorMsg").css("display", "block");
        }

        else if (err === "1") {
            //alert(sts);
            //alert(err);
            //event.preventDefault();


            //$("#modelHeaderTitle").text(fileErrorHeaderMSG);
            $("#modalMessage").text(sts);

            $("#modalDiv").css("display", "block");
        }
    }

    //alert('doc load');
    //alert(err);

    if (err === "3") {
        $("#modalMessage").text(msg);

        $("#modalDiv").css("display", "block");

        setTimeout(function () {
            window.location.href = "../registration/index";
        }, 1500);

    }
    else if (err === "2") 
    {
        $("#modalMessage").text(msg);

        $("#modalDiv").css("display", "block");

    }
    else if (err === "-11") {
        $("#modalMessage").text(msg);

        $("#modalDiv").css("display", "block");

    }
    else if (err === "-1") {
        $("#modalMessage").text(msg);

        $("#modalDiv").css("display", "block");

    }
    else if (err === "-1334") {
        $("#modalMessage").text(msg);

        $("#modalDiv").css("display", "block");

    }
    //else //if (err === "2")
    //{
    //    $("#modalMessage").text(msg);

    //    $("#modalDiv").css("display", "block");

    //}
   

});



$("#Regreshcaptch").click(function () {

    $("#m_imgCaptcha").removeAttr("src").attr('src', "../registration/GetCaptchaImage?Refreshforce=" + Math.floor(Math.random() * 1000));

});

function loadCaptcha() {
    //console.log("clicked");
    $("#m_imgCaptcha").removeAttr("src").attr('src', "../registration/GetCaptchaImage?Refreshforce=" + Math.floor(Math.random() * 1000));
   
}



function RegSuccess(data) {

    console.log(data.StatusCode);
    console.log(data);
    //console.log(ValidationAttribute);
    //alert('success');
    loadCaptcha();

    if (data.StatusCode === 0) {


        var successHeader = "رسالة";
        var successMsg = "تمت اعادة ضبط كلمة المرور بنجاح";

        var language = getLang("culture");

        if (language.indexOf("en") !== -1) {
            successHeader = "Message";
            successMsg = "Reset Password Done Successfully";
        }


        $("#modelHeaderTitle").text(successHeader);
        $("#modalMessage").text(successMsg);

        //$("#modalDiv").removeClass("animated hinge fast");

        $("#modalDiv").css("display", "block");


        setTimeout(function () {
            window.location.assign(data.URL);
        }, 200);
    }
    else {
        alert(data.message);
    }
    ////StatusCode

    console.log(JSON.stringify(data));
    //console.log(data.result);
}
function RegFailure(data) {

    //console.log(ValidationAttribute);
    //alert('failure');
    loadCaptcha();
    //alert('Some issue has occured. Please try again');
    alert(data.message);
}