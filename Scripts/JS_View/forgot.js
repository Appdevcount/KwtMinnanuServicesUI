  $(document).ready(function () {
          
            if (ViewBagmodelstatus == "2") {
                $("#MSGModel").modal('show');
            }
            if (ViewBagmodelstatus == "3") {
                $("#MSGModel1").modal('show');
            }
            $(".btnhide").click(function () {
                $("#MSGModel").modal('hide');
                $("#myLoadingElement").hide();
            });
        });

  $("#divEmp").submit(function(e) {
            $("#myLoadingElement").show();
        });
        $(".btn").click(function(e) {
            $("#myLoadingElement").show();
        });
        $(".myclass").click(function(e) {
            $("#myLoadingElement").show();
        });

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
            var res1=false;
            if ( $.trim( $('#inputfgt').val() ) == ""||$.trim( $('#inputCaptcha').val() ) == "") {
                res=true;
            }
            if ( $.trim( $('#inputcode').val() ) == ""||$.trim( $('#inputpwd').val() ) == ""||$.trim( $('#inputcpwd').val() ) == "") {
                res1=true;
            }
            if (res) {
                $('#btnsubmit').prop('disabled', true);
            }
            else
            {
                $('#btnsubmit').prop('disabled', false);
            }
            if (res1) {
                $('#btnsubmit1').prop('disabled', true);
            }
            else
            {
                $('#btnsubmit1').prop('disabled', false);
            }
            $( ".textval" ).keydown(function() {
                var res=false;
                var res1=false;
                if ( $.trim( $('#inputfgt').val() ) == ""||$.trim( $('#inputCaptcha').val() ) == "") {
                    res=true;
                }
                if ( $.trim( $('#inputcode').val() ) == ""||$.trim( $('#inputpwd').val() ) == ""||$.trim( $('#inputcpwd').val() ) == "") {
                    res1=true;
                }
                if (res) {
                    $('#btnsubmit').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit').prop('disabled', false);
                }
                if (res1) {
                    $('#btnsubmit1').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit1').prop('disabled', false);
                }
            })
            $( ".textval" ).keyup(function() {
                var res=false;
                var res1=false;
                if ( $.trim( $('#inputfgt').val() ) == ""||$.trim( $('#inputCaptcha').val() ) == "") {
                    res=true;
                }
                if ( $.trim( $('#inputcode').val() ) == ""||$.trim( $('#inputpwd').val() ) == ""||$.trim( $('#inputcpwd').val() ) == "") {
                    res1=true;
                }
                if (res) {
                    $('#btnsubmit').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit').prop('disabled', false);
                }
                if (res1) {
                    $('#btnsubmit1').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit1').prop('disabled', false);
                }
            })
            $( ".textval" ).click(function() {
                var res=false;
                var res1=false;
                if ( $.trim( $('#inputfgt').val() ) == ""||$.trim( $('#inputCaptcha').val() ) == "") {
                    res=true;
                }
                if ( $.trim( $('#inputcode').val() ) == ""||$.trim( $('#inputpwd').val() ) == ""||$.trim( $('#inputcpwd').val() ) == "") {
                    res1=true;
                }
                if (res) {
                    $('#btnsubmit').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit').prop('disabled', false);
                }
                if (res1) {
                    $('#btnsubmit1').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit1').prop('disabled', false);
                }
            })
        });



 $(document).ready(function () {
            if (parseInt($(window).width())<450) {
                var height=parseInt($(window).height()-200)+"px";
                $('#mydiv').attr("style","height:"+height);
            }
        });