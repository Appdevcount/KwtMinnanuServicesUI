/*!

 =========================================================
 * Bootstrap Wizard - v1.1.1
 =========================================================
 
 */


searchVisible = 0;
transparent = true;



var TIRCNumberConfig = {
    //required: true
};
var ReferenceNumberConfig = {
    //required: true
};
var CivilIdConfig = {
    //required: true
    CivilIdValidator:'on'
};
var SecurityCodeConfig = {
    required: true
};
var SecurityCodeConfigFalse = {
    required: true
};
var MobileConfig = {
    required: true,
    minlength: 8,
    maxlength: 8,
    //digits: true,
    KuwaitMobileNum: 'on'
};
var EmailConfig = {
    required: true,
    minlength: 2,
    email: true
};

//var messagesConfig;

//function SecurityRequireCheck(TIRCNumberValue, ReferenceNumberValue) {

//    console.log("SecurityRequireCheck - "+TIRCNumberValue +"----"+ ReferenceNumberValue);
//    if (TIRCNumberValue === "" && ReferenceNumberValue === "") {
//        messagesConfig = {
//            TIRCNumber: "Please enter your TIRCNumber",//No use
//            SecurityCode: "Please enter your TIRCNumber (or) Reference Number first !",//Displayed
//            Mobile: {
//                required: "Please enter your Mobile Number",
//                minlength: "Please enter your Mobile Number"
//            },
//            Email: {
//                required: "Please enter a valid email address",
//                minlength: "Please enter a valid email address"
//            },
//            ReferenceNumber: { //No use
//                required: "Please enter a Reference Number"
//            }
//        };
//        SecurityCodeConfig = {
//            required: true
//        };
//    }
//    else if (TIRCNumberValue === "" && (ReferenceNumberValue.includes("TDR") || ReferenceNumberValue.includes("tdr"))) {
//        messagesConfig = {
//            TIRCNumber: "Please enter your TIRCNumber",//No use
//            SecurityCode: "Please enter your SecurityCode",//No use
//            Mobile: {
//                required: "Please enter a valid 8 digit Mobile Number",
//                minlength: "Please enter a valid 8 digit Mobile Number",
//                maxlength: "Please enter a valid 8 digit Mobile Number"
//            },
//            Email: {
//                required: "Please enter a valid email address",
//                minlength: "Please enter a valid email address"
//            },
//            ReferenceNumber: { //No use
//                required: "Please enter a Reference Number"
//            }
//        };

//    }
//    else if (TIRCNumberValue !== ""   && (ReferenceNumberValue.includes("TDR") || ReferenceNumberValue.includes("tdr") || ReferenceNumberValue !== "")) {
//        messagesConfig = {
//            TIRCNumber: "Please enter your TIRCNumber",//No use
//            SecurityCode: "Please enter your SecurityCode",//Displayed
//            Mobile: {
//                required: "Please enter your Mobile Number",
//                minlength: "Please enter your Mobile Number"
//            },
//            Email: {
//                required: "Please enter a valid email address",
//                minlength: "Please enter a valid email address"
//            },
//            ReferenceNumber: { //No use
//                required: "Please enter a Reference Number"
//            }
//        };
//        SecurityCodeConfig = {
//            required: true
//        };
//    }
//    else if (TIRCNumberValue === "" && (!ReferenceNumberValue.includes("TDR") || !ReferenceNumberValue.includes("tdr") || ReferenceNumberValue !== "")) {
//        messagesConfig = {
//            TIRCNumber: "Please enter your TIRCNumber",//No use
//            SecurityCode: "Please enter your SecurityCode",//Displayed
//            Mobile: {
//                required: "Please enter your Mobile Number",
//                minlength: "Please enter your Mobile Number"
//            },
//            Email: {
//                required: "Please enter a valid email address",
//                minlength: "Please enter a valid email address"
//            },
//            ReferenceNumber: { //No use
//                required: "Please enter a Reference Number"
//            }
//        };
//        SecurityCodeConfig = {
//            required: true
//        };
//    }
//    else if (TIRCNumberValue !== "" && (ReferenceNumberValue.includes("TDR") || ReferenceNumberValue.includes("tdr") )) {
//        messagesConfig = {
//            TIRCNumber: "Please enter your TIRCNumber",//No use
//            SecurityCode: "Please enter your SecurityCode",//Displayed
//            Mobile: {
//                required: "Please enter your Mobile Number",
//                minlength: "Please enter your Mobile Number"
//            },
//            Email: {
//                required: "Please enter a valid email address",
//                minlength: "Please enter a valid email address"
//            },
//            ReferenceNumber: { //No use
//                required: "Please enter a Reference Number"
//            }
//        };
//        SecurityCodeConfig = {
//            required: true
//        };
//    }
//    FormValidator();
//}


//function FormValidator() { //Code for the Validator
//    console.log("FormValidator");
//    var $validator = $('.wizard-card form').validate({
//        //   rules: {
//        //     firstname: {
//        //       required: true,
//        //       minlength: 3
//        //     },
//        //     lastname: {
//        //       required: true,
//        //       minlength: 3
//        //     },
//        //     email: {
//        //       required: true,
//        //       minlength: 3,
//        //     }
//        // }

//        rules: {
//            //TIRCNumber: "required",
//            //SecurityCode: "required",
//            //TIRCNumber: {
//            //    required: true
//            //},
//            //SecurityCode: {
//            //    required: true
//            //},
//            //Mobile: {
//            //    required: true,
//            //    minlength: 2,
//            //    digits: true
//            //},
//            //Email: {
//            //    required: true,
//            //    minlength: 2,
//            //    email:true
//            //},
//            //ReferenceNumber: {
//            //    required: true
//            //}
//            TIRCNumber: TIRCNumberConfig,
//            ReferenceNumber: ReferenceNumberConfig,
//            SecurityCode: SecurityCodeConfig,
//            Mobile: MobileConfig,
//            Email: EmailConfig
//        },
//        messages: messagesConfig
//        //{
//        //    TIRCNumber: "Please enter your TIRCNumber",
//        //    SecurityCode: "Please enter your SecurityCode",
//        //    Mobile: {
//        //        required: "Please enter your Mobile Number",
//        //        minlength: "Please enter your Mobile Number"
//        //    },
//        //    Email: {
//        //        required: "Please enter a valid email address",
//        //        minlength: "Please enter a valid email address"
//        //    },
//        //    ReferenceNumber: {
//        //        required: "Please enter a Reference Number"
//        //    }
//        //}
//        ,
//        errorElement: "em",
//        errorPlacement: function (error, element) {
//            // Add the `help-block` class to the error element
//            error.addClass("help-block");

//            // Add `has-feedback` class to the parent div.form-group
//            // in order to add icons to inputs
//            //element.parents(".col-sm-5").addClass("has-feedback");
//            if (element.prop("type") === "checkbox" | element.prop("type") === "radio") {
//                element.parent().parent().addClass("has-feedback");
//            }
//            else {
//                element.parent().addClass("has-feedback");
//            }

//            //if (element.prop("type") === "checkbox") {
//            //    error.insertAfter(element.parent("label"));
//            //} else {
//            //    error.insertAfter(element);
//            //}
//            if (element.prop("type") === "checkbox" | element.prop("type") === "radio") {
//                error.insertAfter(element.parent("div").parent());
//            }
//            else {
//                error.insertAfter(element);
//            }

//            // Add the span element, if doesn't exists, and apply the icon classes to it.

//            //console.log("error" + $(element).next("span")[0]);
//            if (!element.next("span")[0]) {
//                //element.next("span")[0].remove();
//                //$("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
//                $("<span class='fa  fa-times-circle fa-2x form-control-feedback'></span>").insertAfter(element);
//            }
//            ////if (element.prop("type") === "checkbox" | element.prop("type") === "radio") {
//            ////    if (!element.parent().parent().next("span")[0]) {
//            ////        $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element.parent().parent());
//            ////    }
//            ////}
//            ////else {
//            ////    if (!element.next("span")[0]) {
//            ////        $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
//            ////    }
//            ////}

//        },
//        success: function (label, element) {
//            // //Add the span element, if doesn't exists, and apply the icon classes to it.
//            //console.log($(element));
//            //console.log($(element).next());
//            //console.log("success"+$(element).next("span")[0]);

//            if (!$(element).next("span")[0]) {
//                //element.next("span")[0].remove();
//                //console.log($(element));
//                //$("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
//                $("<span class='fa  fa-check-circle fa-2x form-control-feedback'></span>").insertAfter($(element));
//                //console.log($(element).next());
//                //console.log($(element).next().next());
//                //$("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
//            }
//            //if (element.prop("type") === "checkbox" | element.prop("type") === "radio") {
//            //    if (!$(element.parent().parent()).next("span")[0]) {
//            //        $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(elementparent().parent()));
//            //    }
//            //}
//            //else {
//            //    if (!$(element).next("span")[0]) {
//            //        $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
//            //    }
//            //}
//        },
//        highlight: function (element, errorClass, validClass) {
//            //$(element).parents(".IP").addClass("has-error").removeClass("has-success");
//            ////$(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
//            ////$( element ).next( "span" ).removeClass( "glyphicon-ok" );
//            $(element).parents(".IP").addClass("has-error").removeClass("has-success");
//            $(element).next("span").addClass("fa-times-circle").removeClass("fa-check-circle");
//        },
//        unhighlight: function (element, errorClass, validClass) {
//            //$(element).parents(".IP").addClass("has-success").removeClass("has-error");
//            ////$(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
//            ////$( element ).next( "span" ).addClass( "glyphicon-ok" );
//            $(element).parents(".IP").addClass("has-success").removeClass("has-error");
//            $(element).next("span").addClass("fa-check-circle").removeClass("fa-times-circle");
//        }
//    });

//    var $valid = $('.wizard-card form').valid();
//    if (!$valid) {
//        $validator.focusInvalid();
//        return false;
//    }
//}

$(document).ready(function () {

    /*  Activate the tooltips      */
    $('[rel="tooltip"]').tooltip();



    //Code for the Validator
    var $validator = $('.wizard-card form').validate({
        //   rules: {
        //     firstname: {
        //       required: true,
        //       minlength: 3
        //     },
        //     lastname: {
        //       required: true,
        //       minlength: 3
        //     },
        //     email: {
        //       required: true,
        //       minlength: 3,
        //     }
        // }

        rules: {
            //TIRCNumber: "required",
            SecurityCode: SecurityCodeConfig,
            //TIRCNumber: {
            //    required: true
            //},
            //SecurityCode: {
            //    required: true
            //},
            //Mobile: {
            //    required: true,
            //    minlength: 2,
            //    digits: true
            //},
            //Email: {
            //    required: true,
            //    minlength: 2,
            //    email:true
            //},
            //ReferenceNumber: {
            //    required: true
            //}
            //TIRCNumber: TIRCNumberConfig,
            ReferenceNumber: ReferenceNumberConfig,
            //SecurityCode: (TIRCNumberValue === "" && (ReferenceNumberValue.includes("TDR") || ReferenceNumberValue.includes("tdr"))) ? SecurityCodeConfig : SecurityCodeConfigTrue,
            //SecurityCode: SecurityCodeConfig,
            Mobile: MobileConfig,
            Email: EmailConfig
        },
        messages: messageConfig
        //{
        //    TIRCNumber: "Please enter your TIRCNumber",
        //    //SecurityCode: "Please enter your TIRCNumber (or) Reference Number first !",
        //    Mobile: {
        //        required: "Please enter your Mobile Number",
        //        minlength: "Please enter a valid Mobile number of 8 digits ",
        //        maxlength: "Please enter a valid Mobile number of 8 digits "
        //        //KuwaitMobileNum: "Please enter a valid Mobile number of 8 digits "
        //    },
        //    Email: {
        //        required: "Please enter a valid email address",
        //        minlength: "Please enter a valid email address"
        //    },
        //    ReferenceNumber: {
        //        required: "Please enter a Reference Number"
        //    }
        //}
        ,
        errorElement: "em",
        errorPlacement: function (error, element) {
            // Add the `help-block` class to the error element
            error.addClass("help-block");

            // Add `has-feedback` class to the parent div.form-group
            // in order to add icons to inputs
            //element.parents(".col-sm-5").addClass("has-feedback");
            if (element.prop("type") === "checkbox" | element.prop("type") === "radio") {
                element.parent().parent().addClass("has-feedback");
            }
            else {
                element.parent().addClass("has-feedback");
            }

            //if (element.prop("type") === "checkbox") {
            //    error.insertAfter(element.parent("label"));
            //} else {
            //    error.insertAfter(element);
            //}
            if (element.prop("type") === "checkbox" | element.prop("type") === "radio") {
                error.insertAfter(element.parent("div").parent());
            }
            else {
                error.insertAfter(element);
            }

            // Add the span element, if doesn't exists, and apply the icon classes to it.

            //console.log("error" + $(element).next("span")[0]);
            if (!element.next("span")[0]) {
                //element.next("span")[0].remove();
                //$("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
                $("<span class='fa  fa-times-circle fa-2x form-control-feedback'></span>").insertAfter(element);
            }
            ////if (element.prop("type") === "checkbox" | element.prop("type") === "radio") {
            ////    if (!element.parent().parent().next("span")[0]) {
            ////        $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element.parent().parent());
            ////    }
            ////}
            ////else {
            ////    if (!element.next("span")[0]) {
            ////        $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
            ////    }
            ////}

        },
        success: function (label, element) {
            // //Add the span element, if doesn't exists, and apply the icon classes to it.
            //console.log($(element));
            //console.log($(element).next());
            //console.log("success"+$(element).next("span")[0]);

            if (!$(element).next("span")[0]) {
                //element.next("span")[0].remove();
                //console.log($(element));
                //$("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
                $("<span class='fa  fa-check-circle fa-2x form-control-feedback'></span>").insertAfter($(element));
                //console.log($(element).next());
                //console.log($(element).next().next());
                //$("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
            }
            //if (element.prop("type") === "checkbox" | element.prop("type") === "radio") {
            //    if (!$(element.parent().parent()).next("span")[0]) {
            //        $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(elementparent().parent()));
            //    }
            //}
            //else {
            //    if (!$(element).next("span")[0]) {
            //        $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
            //    }
            //}
        },
        highlight: function (element, errorClass, validClass) {
            //$(element).parents(".IP").addClass("has-error").removeClass("has-success");
            ////$(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
            ////$( element ).next( "span" ).removeClass( "glyphicon-ok" );
            $(element).parents(".IP").addClass("has-error").removeClass("has-success");
            $(element).next("span").addClass("fa-times-circle").removeClass("fa-check-circle");
        },
        unhighlight: function (element, errorClass, validClass) {
            //$(element).parents(".IP").addClass("has-success").removeClass("has-error");
            ////$(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
            ////$( element ).next( "span" ).addClass( "glyphicon-ok" );
            $(element).parents(".IP").addClass("has-success").removeClass("has-error");
            $(element).next("span").addClass("fa-check-circle").removeClass("fa-times-circle");
        }
    });


    $.validator.addMethod(
        "KuwaitMobileNum",
        function (value, element) {//, requiredValue) {
            //return value === requiredValue;
            //alert(value + requiredValue);
            //var KuwaitMobileNum = /^[4-9]\d{7}$/; 
            var KuwaitMobileNum = /^[569]\d{7}$/; // Must start with 4 or 5,6,7, 8, 9 . Must be digits after the first integer
            //var MobileREGEX = new RegExp(KuwaitMobileNum);
            //alert(MobileREGEX.test(value));
            return KuwaitMobileNum.test(value);
        },
        MobileMinLengthmsg
        //"Please enter a valid Mobile number of 8 digits "
    );
    $.validator.addMethod(
        "CivilIdValidator",
        function (value, element) {//, requiredValue) {
            return fnCIVILIDValidate(value);
        },
        CivilIdmsg
        //"Please enter a valid Mobile number of 8 digits "
    );

    // Wizard Initialization
    $('.wizard-card').bootstrapWizard({
        'tabClass': 'nav nav-pills',
        'nextSelector': '.btn-next',
        'previousSelector': '.btn-previous',

        onNext: function (tab, navigation, index) {
            console.log(SessionChecker());
            if (!SessionChecker()) {
                window.location.reload();
            }

            //SecurityRequireCheck();

            //console.log("INDEX =" + index);
            var TIRCNumberValue = $("#TIRCNumber").val(); var ReferenceNumberValue = $("#ReferenceNumber").val();

            //var SecurityCodeMsg1 = "Please enter above References ";// "Please enter your TIRCNumber (or) Reference Number first !";
            //var SecurityCodeMsg2 = "Please enter your SecurityCode";
            ////var SecurityCodeMsg3 = "Please enter your TIRCNumber and Security Code";
            //var TIRCNumberMsg1 = "Please enter your TIRCNumber ";
            ////console.log(TIRCNumberValue + " =" + ReferenceNumberValue);


            //COMMENTED IN VERSION 2
            //$("#TIRCNumber").rules("remove");
            //$("#SecurityCode").rules("remove");

            console.log("GCSKnetAnonymousUser");
            console.log(GCSKnetAnonymousUser);

          

            if (GCSKnetAnonymousUser == "True") {//version 2 condition
                //$("#TIRCNumber").rules("remove");
                //$("#SecurityCode").rules("remove");

                console.log("indep");
                console.log(ReferenceNumberValue);
                console.log(RefNumRequired);

                $("#ReferenceNumber").rules("remove");

                //var CivilIdPassed = false;
                if (!ReferenceNumberValue) {
                    if (ReferenceNumberValue.indexOf("/") < 0) {
                        //CivilIdPassed = true;
                        $("#ReferenceNumber").rules("add", {
                            CivilIdValidator: 'on',
                            messages: {
                                CivilIdValidator: CivilIdmsg
                            }
                        });
                    }
                    else {
                        //if (!ReferenceNumberValue || (!ReferenceNumberValue.indexOf("TDR") >= 0 && !ReferenceNumberValue.indexOf("tdr") >= 0)) {
                        $("#ReferenceNumber").rules("add", {
                            required: true,
                            messages: {
                                required: RefNumRequired
                            }
                        });
                        //}
                    }
                }
                else {
                    console.log('no reference nuber passed');
                }
            }
            else {//version 1 condition
                console.log("epdi");
                //adde below 2 conditon in version 2
                $("#Mobile").rules("remove");//DUPLICATE REMOVAL ONLY
                $("#Email").rules("remove");//DUPLICATE REMOVAL ONLY

                if (TIRCNumberValue === "" && ReferenceNumberValue === "") {
                    //var $valid = $('.wizard-card form').valid();
                    //console.log("$validator0 ");
                    //$("#SecurityCode").rules("remove");
                    $("#SecurityCode").rules("add", {
                        required: true,
                        messages: {
                            required: SecurityCodeMsg1
                        }
                    });

                }
                else if (TIRCNumberValue === "" && (ReferenceNumberValue.indexOf("TDR") >= 0 || ReferenceNumberValue.indexOf("tdr") >= 0)) {
                    //var $valid0 = $('.wizard-card form').valid();
                    //console.log("$validator1 ");
                    $("#SecurityCode").rules("remove");//DUPLICATE REMOVAL ONLY
                    $("#Email").rules("remove");//DUPLICATE REMOVAL ONLY



                }
                else if (TIRCNumberValue !== "" && (ReferenceNumberValue.indexOf("TDR") >= 0 || ReferenceNumberValue.indexOf("tdr") >= 0 || ReferenceNumberValue !== "")) {
                    //var $valid = $('.wizard-card form').valid();
                    //console.log("$validator2 ");
                    //$("#SecurityCode").rules("remove");
                    $("#SecurityCode").rules("add", {
                        required: true,
                        messages: {
                            required: SecurityCodeMsg2
                        }
                    });
                }
                else if (TIRCNumberValue === "" && (!ReferenceNumberValue.indexOf("TDR") >= 0 || !ReferenceNumberValue.indexOf("tdr") >= 0 || ReferenceNumberValue !== "")) {
                    //var $valid = $('.wizard-card form').valid();
                    //console.log("$validator3 ");
                    //$("#SecurityCode").rules("remove");
                    $("#SecurityCode").rules("add", {
                        required: true,
                        messages: {
                            required: SecurityCodeMsg2
                        }
                    });

                    $("#TIRCNumber").rules("add", {
                        required: true,
                        messages: {
                            required: TIRCNumberMsg1
                        }
                    });
                }
                else if (TIRCNumberValue !== "" && (ReferenceNumberValue.indexOf("TDR") >= 0 || ReferenceNumberValue.indexOf("tdr") >= 0)) {
                    //var $valid = $('.wizard-card form').valid();
                    //console.log("$validator4 ");
                    //$("#SecurityCode").rules("remove");
                    $("#SecurityCode").rules("add", {
                        required: true,
                        messages: {
                            required: SecurityCodeMsg2
                        }
                    });
                }
                else if (TIRCNumberValue !== "" && ReferenceNumberValue === "") {
                    //var $valid = $('.wizard-card form').valid();
                    //console.log("$validator5 ");
                    //$("#SecurityCode").rules("remove");
                    $("#SecurityCode").rules("add", {
                        required: true,
                        messages: {
                            required: SecurityCodeMsg2
                        }
                    });
                }

            }

            var $valid = $('.wizard-card form').valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }

            //console.log("INDEX =" + index);
            if (index === 1) {
                //PaymentVerifierOnStart("FROM TEST");
                var Proceed = ReceiptValidatorCall();
                console.log("aduthu" + Proceed);
                //return Proceed;
                return Proceed;
            }
            event.preventDefault();//To avoid postback on buttonclick ,.. If we use input type=button then no issues
            //ReceiptValidatorCall();
        },

        onInit: function (tab, navigation, index) {

            console.log(tab);
            console.log(navigation);
            console.log(index);
            //check number of tabs and fill the entire row
            var $total = navigation.find('li').length;
            $width = 100 / $total;
            var $wizard = navigation.closest('.wizard-card');

            $display_width = $(document).width();

            if ($display_width < 600 && $total > 3) {
                $width = 50;
            }

            navigation.find('li').css('width', $width + '%');
            $first_li = navigation.find('li:first-child a').html();
            $moving_div = $('<div class="moving-tab">' + $first_li + '</div>');

            //version 2 test
            //$last_li = navigation.find('li:last-child a').html();
            //$moving_div = $('<div class="moving-tab">' + $last_li + '</div>');

            $('.wizard-card .wizard-navigation').append($moving_div);
            refreshAnimation($wizard, index);
            $('.moving-tab').css('transition', 'transform 0s');

            //$("#testidforversion2 a:last").tab('show');
        },

        onTabClick: function (tab, navigation, index) {

            console.log(SessionChecker());
            if (!SessionChecker()) {
                window.location.reload();
            }
            //alert("TEST");
            return false;
            // var $valid = $('.wizard-card form').valid();

            if (!$valid) {
                return false;
            } else {
                return true;
            }

        },

        onTabShow: function (tab, navigation, index) {
            var $total = navigation.find('li').length;
            var $current = index + 1;

            //version 2 test
            //var $current = index - 1;

            var $wizard = navigation.closest('.wizard-card');

            // If it's the last tab then hide the last button and show the finish instead
            if ($current >= $total) {
                $($wizard).find('.btn-next').hide();
                $($wizard).find('.btn-finish').show();
            } else {
                $($wizard).find('.btn-next').show();
                $($wizard).find('.btn-finish').hide();
            }

            button_text = navigation.find('li:nth-child(' + $current + ') a').html();

            setTimeout(function () {
                $('.moving-tab').text(button_text);
            }, 150);

            var checkbox = $('.footer-checkbox');

            if (!index == 0) {
                $(checkbox).css({
                    'opacity': '0',
                    'visibility': 'hidden',
                    'position': 'absolute'
                });
            } else {
                $(checkbox).css({
                    'opacity': '1',
                    'visibility': 'visible'
                });
            }

            refreshAnimation($wizard, index);
        }
    });


    // Prepare the preview for profile picture
    $("#wizard-picture").change(function () {
        readURL(this);
    });

    $('[data-toggle="wizard-radio"]').click(function () {
        wizard = $(this).closest('.wizard-card');
        wizard.find('[data-toggle="wizard-radio"]').removeClass('active');
        $(this).addClass('active');
        $(wizard).find('[type="radio"]').removeAttr('checked');
        $(this).find('[type="radio"]').attr('checked', 'true');
    });

    $('[data-toggle="wizard-checkbox"]').click(function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
            $(this).find('[type="checkbox"]').removeAttr('checked');
        } else {
            $(this).addClass('active');
            $(this).find('[type="checkbox"]').attr('checked', 'true');
        }
    });

    $('.set-full-height').css('height', 'auto');

});



//Function to show image before upload

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#wizardPicturePreview').attr('src', e.target.result).fadeIn('slow');
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$(window).resize(function () {
    $('.wizard-card').each(function () {
        $wizard = $(this);
        index = $wizard.bootstrapWizard('currentIndex');
        refreshAnimation($wizard, index);

        $('.moving-tab').css({
            'transition': 'transform 0s'
        });
    });
});

function refreshAnimation($wizard, index) {
    total_steps = $wizard.find('li').length;
    move_distance = $wizard.width() / total_steps;
    step_width = move_distance;
    move_distance *= index;

    $wizard.find('.moving-tab').css('width', step_width);
    $('.moving-tab').css({
        'transform': 'translate3d(' + move_distance + 'px, 0, 0)',
        'transition': 'all 0.3s ease-out'

    });
}

function debounce(func, wait, immediate) {
    var timeout;
    return function () {
        var context = this, args = arguments;
        clearTimeout(timeout);
        timeout = setTimeout(function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        }, wait);
        if (immediate && !timeout) func.apply(context, args);
    };
};
