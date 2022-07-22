  $(document).ready(function () {
        if (ViewBagmodelstatus != null) {
            if (ViewBagmodelstatus == "2") {
                $("#MSGModal").modal('show');
            }
            if (ViewBagmodelstatus == "3") {
                $("#MSGModal").modal('show');
            }
        }
        $(".btnhide").click(function () {
            $("#MSGModal").modal('hide');
            $("#myLoadingElement").hide();
        });
    });
 $(document).ready(function () {
        var res=false;
        $('input[type=text]').each(function(){
            if (this.value == "") {
                res=true;
            }
        })
        if (res) {
            $('#btnsubmit').prop('disabled', true);
        }
        else
        {
            $('#btnsubmit').prop('disabled', false);
        }
        $( ".textval" ).keydown(function() {
            var res1=false;
            $('input[type=text]').each(function(){
                if (this.value == "") {
                    res1=true;
                }
            })
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
            if (res2) {
                $('#btnsubmit').prop('disabled', true);
            }
            else
            {
                $('#btnsubmit').prop('disabled', false);
            }
        })
    });