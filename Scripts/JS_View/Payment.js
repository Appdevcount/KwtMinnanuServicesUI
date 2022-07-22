var pageIndex = 1;
var NoMoredata = false;

var inProgress = false;

$(document).ready(function () {
    $('#paydiv').scroll(function () {
        //  if ($('#paydiv').scrollTop() == $('#paydiv').do())
        if ($("#paydiv").scrollTop() >= ($("#wrapperDiv").height() - $("#paydiv").height()) && !NoMoredata && !inProgress) {
            //$("#myLoadingElement1").show();
            Searchepayment();
        }
    });
    function Searchepayment() {
        pageIndex++;
        var aa = pageIndex.toString();
        $.ajax({
            type: "POST",
            url: "",
            data: '{pageIndex: ' + aa + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (response) {
                // console.log(response);
                if (response != null) {
                    var j = "";
                    $.each(response, function (k, v) {
                        if (v.RequstNo != null) {
                            var docid = v.RequstNoenyp;
                            // var str = '@Url.Action("EPayment", "User", new { Id = "aa" })';
                            var str = '/Etrade/User/EPayment?Id=aa';
                            var res = str.replace(/aa/g, docid);
                            var url1 = "location.href='" + res + "'";
                            j += "<tr>";
                            j += "<td>";
                            j += "<div class='" + databind_class + "' onclick=" + url1 + ">";
                            j += "<strong style='color:black;font-weight:normal'>" + Epayment_RequestNumber + " </strong><strong style='color:black;font-weight:normal'>" + v.RequstNo + "</strong><br />";
                            j += " <strong style='color:black;font-weight:normal'>" + v.DeliveryOrderNo + " " + by + " " + v.Broker + " " + To + " " + v.ConsigeeName + " </strong><br />";
                            j += "<strong style='color:black;font-weight:normal'>" + v.PayAmount + " for Duties is " + v.Status + "</strong>";
                            j += "</div></td></tr>";
                            inProgress = false;
                            //$("#myLoadingElement1").hide();
                        }
                        else {
                            NoMoredata = true;
                            //$("#myLoadingElement1").hide();
                        }
                    });
                    //   $("#epaydiv").empty();
                    //$('#epaydiv').html("");
                    $('#epaydiv').append(j);
                    // $('#epaydiv').append(j);
                }

            },
            failure: function (response) {
                alert(response.d);
            }
        });
    }




    $('.textval1').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        maxDate: '0',
        minDate: '',
        changeYear: true,
        yearRange: "-60:+10"
    }).attr('readonly', 'readonly');


    $(".textval1").css('display', 'none');

});




$(document).ready(function () {
    var res = false;
    var dvaue = $('#searchDropdown').val();
    if ($.trim($('input[type=text]').val()) == "") {
        res = true;
    }
    if ($('#dtypeid').val() == "0") {
        res = true;
    }
    if (res) {
        $('#btnsubmit').prop('disabled', true);
    }
    else {
        $('#btnsubmit').prop('disabled', false);
    }
    $('#idselect').change(function () {
        var res1 = false;
        if ($.trim($('input[type=text]').val()) == "") {
            res1 = true;
        }
        if ($('#dtypeid').val() == "0") {
            res1 = true;
        }
        if (res1) {
            $('#btnsubmit').prop('disabled', true);
        }
        else {
            $('#btnsubmit').prop('disabled', false);
        }
    })
    $(".textval").keydown(function () {
        var res1 = false;
        if ($.trim($('input[type=text]').val()) == "") {
            res1 = true;
        }
        if ($('#dtypeid').val() == "0") {
            res1 = true;
        }
        if (res1) {
            $('#btnsubmit').prop('disabled', true);
        }
        else {
            $('#btnsubmit').prop('disabled', false);
        }
    })
    $(".textval").keyup(function () {
        var res1 = false;
        if ($.trim($('input[type=text]').val()) == "") {
            res1 = true;
        }
        if ($('#dtypeid').val() == "0") {
            res1 = true;
        }
        if (res1) {
            $('#btnsubmit').prop('disabled', true);
        }
        else {
            $('#btnsubmit').prop('disabled', false);
        }
    })
    $(".textval").click(function () {
        var res2 = false;
        if ($.trim($('input[type=text]').val()) == "") {
            res2 = true;
        }
        if ($('#dtypeid').val() == "0") {
            res2 = true;
        }
        if (res2) {
            $('#btnsubmit').prop('disabled', true);
        }
        else {
            $('#btnsubmit').prop('disabled', false);
        }
    })

});



$(document).ready(function () {

    $('#searchList').on('change', function () {

        $('#dtypeid').val(this.value);
        //alert(this.value);
    });

    if (parseInt($(window).width()) < 450) {
        var text = $('#spandtype').text();
        if (text.length > 10) {
            text = text.substring(0, 10) + "..";
            $('#spandtype')
                .text(text);
        }
    }

    $("#Selectsearchtype").click(function () {
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
        $("#paynentsearchtypesModal").modal('show');
        //$("#myLoadingElement").hide();
    });

    $(".btnhide").click(function () {
        $("#paynentsearchtypesModal").modal('hide');
        //$("#myLoadingElement").hide();
    });
});
function documettype2() {
    var radios = document.getElementsByName('ossm');

    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {

            // do whatever you want with the checked radio
            var selector = 'label[for=' + radios[i].id + ']';
            var label = document.querySelector(selector);
            var text = label.innerHTML;
            document.getElementById('dtypeid').value = radios[i].value;
            //document.getElementById('spandtype').innerHTML = text;
            if (parseInt($(window).width()) < 450) {
                if (text.length > 10) {
                    text = text.substring(0, 10) + "..";

                }
            }
            $('#spandtype')
                .text(text);
            var res2 = false;
            if ($.trim($('input[type=text]').val()) == "") {
                res2 = true;
            }
            if ($('#dtypeid').val() == "0") {
                res2 = true;
            }
            if (res2) {
                $('#btnsubmit').prop('disabled', true);
            }
            else {
                $('#btnsubmit').prop('disabled', false);
            }
            // only one radio can be logically checked, don't check the rest
        }
    }
}



$("#btnsubmit").click(function () {

    var selected = $("#searchList").children("option:selected").val();
    //    //console.log(selected);
    var value = $("#PaySearch").val();

    if (selected == 'Date') {
        if (isDate(value)) {
           
            $("#dateError").css('display', 'none');
        }
        else {

            event.preventDefault();
            $("#dateError").css('display', 'inline-block');
            return;

           
        }
    }

});