$(document).ready(function () {
    $("#idpopup").click(function () {
        $("#loginModal").modal('show');
    });

    $("#btnHideModal").click(function () {
        $("#loginModal").modal('hide');
    });
    if (ViewBagmodelerror != null) {
        if (ViewBagmodelerror == "0") {
            $("#dialog").modal('show');
        }
    }
    
 
});
