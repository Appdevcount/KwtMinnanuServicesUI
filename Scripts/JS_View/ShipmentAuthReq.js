
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

   