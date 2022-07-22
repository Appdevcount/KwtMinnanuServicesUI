$(document).ready(function () {
    if (resultc == "1") {
        $("#Selectsubtype").click(function () {
            var value = $("#dtypeid").val();
            var value1 = $("#dtypefirst").val();
            if (value != "") {
                $('#aa' + value).prop('checked', true);
            }
            else {
                if (value1 != "") {
                    $('#aa' + value1).prop('checked', true);
                }
            }
            $("#CommLicSubTypesModal").modal('show');
            $("#myLoadingElement").hide();
        });
    
    $(".btnhide").click(function () {
        $("#CommLicSubTypesModal").modal('hide');
        $("#myLoadingElement").hide();
    });
    }
});
function documettype() {
    var radios = document.getElementsByName('ossm');

    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {

            // do whatever you want with the checked radio
            var selector = 'label[for=' + radios[i].id + ']';
            var label = document.querySelector(selector);
            var text = label.innerHTML;
            document.getElementById('dtypeid').value = radios[i].value;
            //document.getElementById('spandtype').innerHTML = text;
            $('#spandtype')
                   .text(text);
            // only one radio can be logically checked, don't check the rest
        }
    }
}
function w3_open() {
    document.getElementById("mySidebar").style.width = "30%";
    document.getElementById("mySidebar").style.display = "block";
}
function w3_close() {
    document.getElementById("mySidebar").style.display = "none";
}
$(document).ready(function () {
    if (ViewBagmodelerror=="0")
    {
        $("#dialog").modal('show');
    }
});