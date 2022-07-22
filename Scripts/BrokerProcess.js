
//$(function () {

//    jQuery.validator.addMethod('checkdaterange', function (value, element, params) {

//        //console.log(value);
//        var splitD = value.split("/");// dd/MM/yyyy
//        //console.log(splitD);

//        var dateFormatted = splitD[0] + "-" + splitD[1] + "-" + splitD[2];
//        //console.log(dateFormatted);

//        var varDate = new Date(dateFormatted); //dd-mm-YYYY
//        //console.log(varDate);
//        var today = new Date();
//        //console.log(today);

//        if (varDate) {
//            if (varDate >= today) {
//                //console.log('date true');
//                return true;
//            }
//            else {
//                //console.log('date less');
//                return false;
//            }
//        }
//        else {

//            //console.log('datefalse');
//            return false;
//        }



//    }, '');

//    jQuery.validator.unobtrusive.adapters.add('checkdaterange', function (options) {
//        options.rules['checkdaterange'] = {};
//        options.messages['checkdaterange'] = options.message;
//    });

//}(jQuery));




$(function () {

    function process(date) {
        var parts = date.split("/");
        return new Date(parts[2], parts[1] - 1, parts[0]);
    }
    var Sessionserviceid = $('#sessionDiv').attr('data-id');

    if (Sessionserviceid == '2' || Sessionserviceid == '7' || Sessionserviceid == '17') {

        jQuery.validator.addMethod('checkdaterange', function (value, element, params) {

            //console.log(value);
            //console.log(element);
            //console.log(element.id);
            var Sessionserviceid = $('#sessionDiv').attr('data-id');

            //if (Sessionserviceid == '12' )
            //{
            //    return true;
            //}

            //if (Sessionserviceid != '2' || Sessionserviceid != '7') {
            //    return true;
            //}
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = dd + '/' + mm + '/' + yyyy;



            var dts = document.getElementById(element.id);
            //console.log(dts);

            var splitD = value.split("/");// dd/MM/yyyy
            //console.log(splitD);

            var dateFormatted = splitD[1] + "-" + splitD[0] + "-" + splitD[2];
            //console.log(dateFormatted);

            //  var varDate = new Date(dateFormatted); //dd-mm-YYYY
            var varDate = process(value); //dd-mm-YYYY

            var s = process(today);
            //console.log(varDate);
            //var today = new Date();
            ////console.log(today);

            //var date = today.getDate();
            //var month = today.getMonth(); //Be careful! January is 0 not 1
            //var year = today.getFullYear();

            //var dateString = (month + 1)+ "-" + (date) + "-" + year;;// (date) + "-" + (month + 1) + "-" + year;


            //today = new Date(dateString);


            // alert(today);

            // alert(varDate);
            //console.log(today);
            //console.log(varDate >= today);


            if (varDate) {

                if (varDate >= process(today)) {
                    //console.log('date true');
                    return true;
                }
                else {
                    //console.log('date less');
                    return false;
                }
            }
            else {

                //console.log('datefalse');
                return false;
            }


        }, '');

        jQuery.validator.unobtrusive.adapters.add('checkdaterange', function (options) {
            options.rules['checkdaterange'] = {};
            options.messages['checkdaterange'] = options.message;
        });


    }
}(jQuery));

$().ready(function () {

  

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
})