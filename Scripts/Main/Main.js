function w3_open() {
    document.getElementById("mySidebar").style.display = "block";
}
function w3_close() {
    document.getElementById("mySidebar").style.display = "none";
}
$(document).ready(function () {
    $("#idpopup").click(function () {
        $("#loginModal").modal('show');
    });
    $("#btnHideModal").click(function () {
        $("#loginModal").modal('hide');
    });
});


$(".menunew").click(function () {
    $("#myLoadingElement").show();
});


$(document).ready(function () {
    //var ViewBagmodelstatus = modelerror;
    //if (ViewBagmodelstatus == "5") {
    //    $("#modelcommon").modal('show');
    //}
    //if (ViewBagmodelstatus == "9") {
    //    $("#modelsession").modal('show');
    //}
    //$(".btnhide").click(function () {
    //    $("#modelcommon").modal('hide');
    //    $("#myLoadingElement").hide();
    //});
});


$(document).ready(function () {
    $(".clsorgdelete").click(function (e) {
        var className = this.className
        var res = className.replace("glyphicon glyphicon-trash clsorgdelete ", "");
      //  var str = '@Url.Action("OrgDelete", "User", new { OrgReqId = "aa" })';
        var str = '/Etrade/User/OrgDelete?OrgReqId=aa';
        var res1 = str.replace(/aa/g, res);
        var url1 = "location.href='" + res1 + "'";
        $('#deletecnfdiv').html("");
        $("#deletecnfdiv").append("<input type='button' id='btnok' class='btn button button4' value=" + Okbtn + " onclick=" + url1 + " Style='width:auto' />");
        $("#ConfirmModal").modal('show');
        $("#myLoadingElement").hide();
    });
    $(".btnhide").click(function () {
        $("#ConfirmModal").modal('hide');
        $("#myLoadingElement").hide();
    });
});


$(document).ready(function () {
    //$("#idcount").click(function () {
    //    $("#idcount").removeClass("notification-icon");
    //    $("#idcount").removeClass("notification-icon1");
    //});

    //UpdateNotifications();
    //setInterval(function () {
    //    UpdateNotifications();
    //}, 300000);

    $("#notifyidevent").click(function (e) {
        e.preventDefault();
        $("#idcount").removeClass("notification-icon");
        $("#idcount").removeClass("notification-icon1");
        UpdateNotifications();
    });

});
function UpdateNotifications() {

    $.ajax({
        type: "GET",
        url: "",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            console.log(response);
            if (response != null && response[0].DateCreated != null) {
                var j = ""; var p = 0; var Q = "";
                j += "  <div class='w3-container' style='width: 100%; height: 350px;' >"
                j += "  <div id='divnotifyheader' class='" + MenuHeader_notifycssclass + "' >" + MenuHeader_notifyHeader + "<button type='button' class='" + MenuHeader_notifycssclass1 + "' data-dismiss='modal' style='color:white;'> &times; </button></div>"
                j += "  <div style=' height: 280px; overflow-y: auto !important; margin-bottom: 30px;margin-top: 20px;' >"
                $.each(response, function (k, v) {
                    p++;
                    if (v.msg != null) {
                        var url='';
                        var res = v.ReffIdencry;
                        var created = created2;
                        var view = view2;
                              
                               
                      //  var strEpay = '@Url.Action("EPayment", "User", new { Id = "aa" })';
                        var strEpay = '/Etrade/User/EPayment?Id=aa';
                        // var strorgreq = '@Url.Action("Org_Registration", "User", new { Id = "aa", Requesttype = "cre", purpose = "view",reqnum="" })';
                        var strorgreq = '/Etrade/User/Org_Registration?Id=' + res + '&Requesttype=' + created + '&purpose=' + view;
                        var res1 = strEpay.replace(/aa/g, res);
                        var url1 = "location.href='" + res1 + "'";

                      //  var strorgreq1 = strorgreq.replace(/aa/g, res);
                       // var strorgreq2 = strorgreq1.replace(/cre/g, created);
                       // var strorgreq3 = strorgreq2.replace(/view/g, view);
                        var url2 = "location.href='" + strorgreq + "'";
                        if (v.ReffIdencry!=null) {
                            if (v.NotificationType == "0") 
                            {
                                url=url2;
                            }
                            else
                            {
                                url=url1;
                            }
                        }
                        if (v.ReadStatus == "1") {
                            j += "<li> <div class='" + notifications_style + "' style='" + textalign + "' onclick=" + url + "><strong style='font-weight: normal;font-size: 1.4rem;line-height: 1.5;'>" + v.msg + "</strong><br><h5>" + v.DateCreated + "</h5></div></li>";
                        }
                        else
                        {
                            j += "<li> <div class='" + notifications_style + "' style='" + textalign + "' onclick=" + url + "><strong style='font-weight: bold;font-size: 1.4rem;line-height: 1.5;'>" + v.msg + "</strong><br><b>" + v.DateCreated + "</b></div></li>";
                        }
                        //  if (p < 6) {

                    }
                    //}
                    //Q += "<a><img src='http://localhost:58449/Content/nty.png' style='height: 10px; width: 10px'><strong>  " + v.Notification.toString() + " </strong> <br /> <div style='text-align:right;font-size:smaller;font-style:italic'> <i style='font-size:smaller'> updated : " + v.LastUpdated.toString() + "</i></div></a>";
                });
                j += "  </div>";
                j += "  </div>";
                $('#myNotifyList').html("");
                //  j += "<li><a data-toggle='modal' data-target='#myModal'><div style='text-align:center'><strong> Show more </strong></div></li>";
                $('#myNotifyList').append(j);
                // $('#idcount').attr("data-count", "");
                //$('#countNotify').html("<span>" + p + "</span>");
                $('#myAllNotifications').html("");
                $('#myAllNotifications').append(Q);
            }
            $("#myLoadingElement").hide();
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}
$(document).ready(function () {
    $("#btnprint").click(function (e) {
        print();
    });

    function print() {
        var aa = '1';
        $.ajax({
            type: "POST",
            url: "",
            data: '{pageIndex: ' + aa + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    var parseJSON = $.parseJSON(response);
                    var printWindow = window.open('', '', 'toolbar=yes,scrollbars=yes,resizable=yes,top=500,left=500,width=800,height=800');
                    printWindow.document.write(parseJSON);
                    printWindow.document.write('<table align="center" style="border:none"><tr><td align="center" style="border:none"><input type="button" value="Print" onClick="window.print()"></td></tr></table>');
                    printWindow.document.close();
                    //  printWindow.print();
                }
                $("#myLoadingElement").hide();
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    }
});

$(document).ready(function () {
    $(".span-icon").click(function (e) {
        $("#myLoadingElement").hide();
        var className = this.className
        //var res = className.replace("glyphicon glyphicon-print ", "");
        var res = className.replace("span-icon ", "");
        Orgprint(res);
    });
    $("#btnprintOrg").click(function (e) {
        $("#myLoadingElement").hide();
        var className1 = this.className
        //var res = className.replace("glyphicon glyphicon-print ", "");
        var res1 = className1.replace("btn button button4 ", "");
        Orgprint(res1);
    });
    function Orgprint(aa) {
        $.ajax({
            type: "POST",
            url: "",
            data: '{OrgReqId: ' + aa + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    var parseJSON = $.parseJSON(response);
                    var printWindow = window.open('', '', 'toolbar=yes,scrollbars=yes,resizable=yes,top=500,left=500,width=800,height=800');
                    printWindow.document.write(parseJSON);
                    printWindow.document.write('<table align="center" style="border:none"><tr><td align="center" style="border:none"><input type="button" value="Print" onClick="window.print()"></td></tr></table>');
                    printWindow.document.close();
                    //  printWindow.print();
                }
                $("#myLoadingElement").hide();
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    }
});



$("#tdorg").on("click", function () {
    $("#myLoadingElement").show();
});
$("#epaytrid").on("click", function () {
    $("#myLoadingElement").show();
});
$(".myclass").click(function (e) {
    $("#myLoadingElement").show();
});
$(".btn").click(function (e) {
    $("#myLoadingElement").show();
});

//$(document).ready(function () {
//    try {
//        $("input[type='text']").each(function () {
//            $(this).attr("autocomplete", "off");
//            $(this).attr("ondrop", "return false");
//            $(this).attr("onpaste", "return false");
//        });
//    }
//    catch (e)
//    { }
//});



$(function () {
    $(document).keydown(function (e) {
        return (e.which || e.keyCode) != 116;
    });
});


var specialKeys = new Array();
specialKeys.push(8); //Backspace
function IsNumeric(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
    return ret;
}

$(document).ready(function () {
    if (parseInt($(window).width())<450) {
        //var height=parseInt($(window).height()-160)+"px";
        //$('#mydiv').attr("style","height:"+height);

        var height="";
        if (parseInt($('#mydiv').height())==640)
        {
            height=parseInt($(window).height()-190)+"px";
        }
        if (parseInt($('#mydiv').height())==460)
        {
            height=parseInt($(window).height()-350)+"px";
        }
        if (parseInt($('#mydiv').height())==660)
        {
            height=parseInt($(window).height()-160)+"px";
        }
        if (parseInt($('#mydiv').height())==700)
        {
            height=parseInt($(window).height()-90)+"px";
        }
        if (parseInt($('#mydiv').height())==720)
        {
            height=parseInt($(window).height()-90)+"px";
        }
        if (parseInt($('#paydiv').height())==570)
        {
            var height1=parseInt($(window).height()-240)+"px";
            $('#paydiv').attr("style","height:"+height1);
        }
        $('#mydiv').attr("style","height:"+height);
    }
});
//window.onload = window.history.forward(0);  //calling function on window onload
