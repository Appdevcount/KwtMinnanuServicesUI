

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
                  window.location.href = '../Request/RequestListForTheUser';
              }, 2000);
          }
      }


    $("#inputAuthorizedPerson").keyup(function () {

        if ($("#inputAuthorizedPerson").val() !== '') {
            if ($("#inputCivilId").val() !== '') {
                $('.btnsubmit').each(function () {
                    $(this).prop('disabled', false);
                });
                //$('#btnsubmit').prop('disabled', false);
            }
            else {
                $('.btnsubmit').each(function () {
                    $(this).prop('disabled', true);
                });
            }
        }
        else {
            $('.btnsubmit').each(function () {
                $(this).prop('disabled', true);
            });
        }
    })
    $("#inputCivilId").keyup(function () {
        if ($("#inputCivilId").val() !== '') {
            if ($("#inputAuthorizedPerson").val() !== '') {
                $('.btnsubmit').each(function () {
                    $(this).prop('disabled', false);
                });
                //$('#btnsubmit').prop('disabled', false);
            }
            else {
                $('.btnsubmit').each(function () {
                    $(this).prop('disabled', true);
                });
            }
        }
        else {
            $('.btnsubmit').each(function () {
                $(this).prop('disabled', true);
            });
        }
    })
    if ($("#inputAuthorizedPerson").val() !== '') {
        if ($("#inputCivilId").val() !== '') {
            $('.btnsubmit').each(function () {
                $(this).prop('disabled', false);
            });
            //$('#btnsubmit').prop('disabled', false);
        }
        else {
            $('.btnsubmit').each(function () {
                $(this).prop('disabled', true);
            });
        }
    }
    else {
        $('.btnsubmit').each(function () {
            $(this).prop('disabled', true);
        });
    }
   

    if (result === "0") {
        $("#mydiv :input").attr("disabled", true);
        //$("#mydiv :input").attr("style", "background-color:#ccc;width:100%;");
        $('.btn').prop('disabled', false);
        //$('.btn').attr("style", "background-color:#BD3B2E;");
    }
        
            
});

