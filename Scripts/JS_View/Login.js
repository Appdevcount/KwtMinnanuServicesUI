 function aa()
        {
            window.open('https://10.138.77.110/eTradePP//KuwaitCustomseServices-QuickReferenceV2.1-Arabic(002).pdf','_newtab');
        }


 $(document).ready(function () {
            try {
                $("input[type='text']").each(function(){
                    $(this).attr("autocomplete","off");
                });
            }
            catch (e)
            { }
        });

  $(document).ready(function () {
            var res=false;
            if ( $.trim( $('input[type=text]').val() ) == ""||$.trim( $('input[type=password]').val() ) == "") {
                res=true;
            }
            if (res) {
                $('#loginBtn').prop('disabled', true);
            }
            else
            {
                $('#loginBtn').prop('disabled', false);
            }
            $( "#password" ).keydown(function() {
                var res1=false;
                if ( $.trim( $('input[type=text]').val() ) == ""||$.trim( $('input[type=password]').val() ) == "") {
                    res1=true;
                }
                if (res1) {
                    $('#loginBtn').prop('disabled', true);
                }
                else
                {
                    $('#loginBtn').prop('disabled', false);
                }
            })
            $( "#password" ).keyup(function() {
                var res1=false;
                if ( $.trim( $('input[type=text]').val() ) == ""||$.trim( $('input[type=password]').val() ) == "") {
                    res1=true;
                }
                if (res1) {
                    $('#loginBtn').prop('disabled', true);
                }
                else
                {
                    $('#loginBtn').prop('disabled', false);
                }
            })
            $( "#password" ).click(function() {
                var res2=false;
                if ( $.trim( $('input[type=text]').val() ) == ""||$.trim( $('input[type=password]').val() ) == "") {
                    res2=true;
                }
                if (res2) {
                    $('#loginBtn').prop('disabled', true);
                }
                else
                {
                    $('#loginBtn').prop('disabled', false);
                }
            })
        });




$("#idpopup").click(function () {
    event.preventDefault();
    var usageAgreement = $("#usageAgreement").val();
    var usageAgreementHeader = $("#usageAgreementHeader").val();

    $("#modalMessage").text(usageAgreement);
    $("#modelHeaderTitle").text(usageAgreementHeader);


    //$("#modalDiv").removeClass("animated hinge fast");

    $("#modalDiv").css("display", "block");

});






















 //$(document).ready(function () {
 //           if (parseInt($(window).width())<450) {
 //               var height=parseInt($(window).height()-130)+"px";
 //               $('#mydiv').attr("style","height:"+height);
 //           }
 //           else
 //           {
 //               if (parseInt($('#mydiv').height())==460)
 //               {
 //                   var height=parseInt($(window).height()-420)+"px";
 //                   $('#mydiv').attr("style","height:"+height);
 //               }
 //           }
 //       });


 //var x = document.getElementById("inputgln");
 //       $(document).ready(function () {
 //           getLocation();
 //       });
 //       function getLocation() {
 //           if (navigator.geolocation) {
 //               navigator.geolocation.getCurrentPosition(showPosition);
 //           } 
 //       }

 //       function showPosition(position) {
 //           x.value =position.coords.latitude + 
 //           "," + position.coords.longitude;
 //       }




