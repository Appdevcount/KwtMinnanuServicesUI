 $(document).ready(function () {
            var res=false;
            if ( $.trim( $('input[type=text]').val() ) == "") {
                res=true;
            }
            if (res) {
                $('#btnsubmit').prop('disabled', true);
            }
            else
            {
                $('#btnsubmit').prop('disabled', false);
            }
            $( ".textval" ).keydown(function() {
                var res1=false;
                if ($.trim($('input[type=text]').val()) == "") {
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
                if ($.trim($('input[type=text]').val()) == "") {
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
                if ($.trim($('input[type=text]').val()) == "") {
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