$(document).ready(function () {
    $("#Regreshcaptch").click(function () {
        $.ajax({
            type: "POST",
            url: "/Etrade/registration/Registration1",
            data: '{captchaid: "' + Captchid + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (response) {
                // console.log(response);
                if (response != null) {
                    $("#captchasrc").attr('src', response.CaptchaImage);
                }
            },
            failure: function (response) {
                $("#captchasrc").attr('src', '');
            }
        });
    });
});
