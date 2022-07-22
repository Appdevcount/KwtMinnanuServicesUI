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
           
            if (ViewBagmodelstatus != null) {
                if (ViewBagmodelstatus == "0") {
                    $("#modelemail").modal('show');
                }
            }
            $("#btnHide").click(function () {
                $("#modelemail").modal('hide');
                $("#myLoadingElement").hide();
            });
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
            $('input[type=text]').each(function(){
                if (this.value == "") {
                    res=true;
                } 
            })
            $('input[type=password]').each(function(){
                if (this.value == "") {
                    res=true;
                } 
            })
            if(!($("#Term_conditions").is(":checked"))){
                res=true;
            }
            if (res) {
                $('#btnsubmit').prop('disabled', true);
            }
            else
            {
                $('#btnsubmit').prop('disabled', false);
            }
            $('input[type="checkbox"]').click(function(){
                var res1=false;
                $('input[type=text]').each(function(){
                    if (this.value == "") {
                        res1=true;
                    } 
                })
                $('input[type=password]').each(function(){
                    if (this.value == "") {
                        res1=true;
                    } 
                })
                if($(this).is(":not(:checked)")){
                    res1=true;
                }
                if (res1) {
                    $('#btnsubmit').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit').prop('disabled', false);
                }
            })
            $( ".textval" ).keydown(function() {
                var res1=false;
                $('input[type=text]').each(function(){
                    if (this.value == "") {
                        res1=true;
                    } 
                })
                $('input[type=password]').each(function(){
                    if (this.value == "") {
                        res1=true;
                    } 
                })
                if(!($("#Term_conditions").is(":checked"))){
                    res1=true;
                }
                if (res1) {
                    $('#btnsubmit').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit').prop('disabled', false);
                }
            })
            $( ".textval" ).keyup(function() {
                var res1=false;
                $('input[type=text]').each(function(){
                    if (this.value == "") {
                        res1=true;
                    } 
                })
                $('input[type=password]').each(function(){
                    if (this.value == "") {
                        res1=true;
                    } 
                })
                if(!($("#Term_conditions").is(":checked"))){
                    res1=true;
                }
                if (res1) {
                    $('#btnsubmit').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit').prop('disabled', false);
                }
            })
            $( ".textval" ).click(function() {
                var res2=false;
                $('input[type=text]').each(function(){
                    if (this.value == "") {
                        res2=true;
                    } 
                })
                $('input[type=password]').each(function(){
                    if (this.value == "") {
                        res2=true;
                    } 
                })
                if(!($("#Term_conditions").is(":checked"))){
                    res2=true;
                }
                if (res2) {
                    $('#btnsubmit').prop('disabled', true);
                }
                else
                {
                    $('#btnsubmit').prop('disabled', false);
                }
            })
        });


  var specialKeys = new Array();
        specialKeys.push(8); 
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }

 $(document).ready(function () {
            if (parseInt($(window).width())<450) {
                var height=parseInt($(window).height()-200)+"px";
                $('#mydiv').attr("style","height:"+height);
            }
        });