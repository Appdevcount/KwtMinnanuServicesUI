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
      $(".textval").click(function () {
          var res2 = false;
          if ($.trim($('input[type=text]').val()) == "") {
              res2 = true;
          }
          if (res2) {
              $('#btnsubmit').prop('disabled', true);
          }
          else {
              $('#btnsubmit').prop('disabled', false);
          }
      });





    

       var category = null;
  $("input[name='optradio']").click(function () {
      debugger;
      if (this.value === '1') {
          //$(".OPTIONB").prop("disabled", true);
          //$(".OPTIONA").prop("disabled", false);
          $("#HousebillSearch").prop("disabled", false);
          $("#deliveryOrderNo").prop("disabled", true);
          $("#securityNo").prop("disabled", true);
      }
      else if (this.value === '2') {
          //$(".OPTIONA").prop("disabled", true);
          //$(".OPTIONB").prop("disabled", false);
          $("#HousebillSearch").prop("disabled", true);
          $("#deliveryOrderNo").prop("disabled", false);
          $("#securityNo").prop("disabled", false);
      }
  });
});

