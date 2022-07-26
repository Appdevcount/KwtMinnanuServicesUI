! function (e) {
    "use strict";
    $(e).load(function () {
        setTimeout(function () {
            $("header .content .row,footer").addClass("hidden")
        }, 100), setTimeout(function () {
            $(".preloader").addClass("end")
        }, 1800), setTimeout(function () {
            $(".global-overlay").addClass("show")
        }, 1900), setTimeout(function () {
            $("header .content .row,footer").removeClass("hidden")
        }, 2300)
    }), $(".rain").each(function () {
        function t(e) {
            return decodeURIComponent((new RegExp("[?|&]" + e + "=([^&;]+?)(&|#|;|$)").exec(location.search) || [, ""])[1].replace(/\+/g, "%20")) || null
        }
        var o = document.getElementById("rainy");
        o.onload = function () {
            var o = new RainyDay("rain", "rainy", e.innerWidth, e.innerHeight, 1, t("blur") || 15),
                n = t("preset") || 1;
            1 == n ? (o.gravity = o.GRAVITY_NON_LINEAR, o.trail = o.TRAIL_DROPS, o.rain([o.preset(3, 3, .88), o.preset(5, 5, .9), o.preset(6, 2, 1)], 100)) : 2 == n ? (o.gravity = o.GRAVITY_NON_LINEAR, o.trail = o.TRAIL_DROPS, o.VARIABLE_GRAVITY_ANGLE = Math.PI / 8, o.rain([o.preset(0, 2, .5), o.preset(4, 4, 1)], 50)) : 3 == n && (o.gravity = o.GRAVITY_NON_LINEAR, o.trail = o.TRAIL_SMUDGE, o.rain([o.preset(0, 2, .5), o.preset(4, 4, 1)], 50))
        }, o.crossOrigin = "anonymous", o.src = "assets/img/rain.jpg"
    }), $(".bubble").each(function () {
        $(".bubble").pobubble({
            color: "#ffffff",
            ammount: 7,
            min: .1,
            max: .3,
            time: 60,
            vertical: !0,
            horizontal: !0,
            style: "circle"
        })
    }), $(".slider").each(function () {
        $(".slider").phoenix({
            delay: 6e3,
            fullscreen: !0,
            dots: !1,
            keys: !1
        })
    }), $(".zoom").each(function () {
        $(".zoom").kenburnsy({
            fullscreen: !0
        })
    }), $(".countdown").each(function () {
        function t(t) {
            t.each(function () {
                var t = $(this),
                    o = t.text(),
                    n = t.attr("data-value"),
                    a = t.attr("data-old");
                "undefined" == typeof a && t.attr("data-old", o), o != n && (t.attr("data-value", o).attr("data-old", n).addClass("animate"), e.setTimeout(function () {
                    t.removeClass("animate").attr("data-old", o)
                }, 300))
            })
        }
        $(".countdown").countdown({
            day: 1,
            month: 1,
            year: 2018,
            onChange: function () {
                t($(".digits span"))
            }
        })
        }),

        //$(".readmore").on("click", function () {
        //$("header,aside,footer").toggleClass("blured"), $("header,aside,footer").toggleClass("open"), $(".body-wrap").toggleClass("open"), $(".section-close").toggleClass("open"), $(".overlay").toggleClass("active"), $(".body-wrap").mCustomScrollbar("scrollTo", "top", {
        //    scrollEasing: "easeInOutExpo",
        //    timeout: 600
        //})

      

        $(".readmore").on("click", function () {
            //$("header,aside,footer").toggleClass("blured"),
            //    $("header,aside,footer").toggleClass("open"),
            //    $("header,aside,footer").toggleClass("open"),
            //    $(".body-wrap").toggleClass("open"),
            //    $(".section-close").toggleClass("open"),
            //    $(".overlay").toggleClass("active"),

            //$("pro").mCustomScrollbar("scrollTo", "top", { scrollEasing: "easeInOutExpo", timeout: 600 })
            document.getElementById("pro").style.display = 'block';

            var interval = 1, //How much to increase the progressbar per frame
                updatesPerSecond = 1000 / 30, //Set the nr of updates per second (fps)
                progress = $('progress'),
                animator = function () {
                    progress.val(progress.val() + interval);
                    $('#val').text(progress.val());
                    if (progress.val() + interval < progress.attr('max')) {
                        setTimeout(animator, updatesPerSecond);
                    } else {
                        //$('#val').text('�� ����� ������');
                        //document.getElementById("val").style.display = 'block';
                        progress.val(progress.attr('max'));
                    }
                }

            setTimeout(animator, updatesPerSecond);



    }), $('[data-map="collapse"],.btn-map-close').on("click", function () {
        $("#map").toggleClass("open"), $(".body-wrap").toggleClass("open"), $(".loading").addClass("active").addClass("active2"), setTimeout(function () {
            $(".loading").removeClass("active")
        }, 1e3), setTimeout(function () {
            $(".loading").removeClass("active2")
        }, 2e3), $(".btn-map-close").toggleClass("open")
    }), $(".overlay,.section-close").on("click", function () {
        $("header,aside,footer").toggleClass("blured"), $("header,aside,footer").toggleClass("open"), $(".body-wrap").toggleClass("open"), $(".overlay").toggleClass("active"), $(".section-close").toggleClass("open")
    }), $(".card").each(function () {
        $(".card").nivoLightbox({
            effect: "slideDown"
        })
    }), $(".card .box-front,.modal-content").each(function () {
        var e = $(this).attr("data-image-src");
        "undefined" != typeof e && e !== !1 && $(this).css("background-image", "url(" + e + ")")
    }), $(".background .solid").each(function () {
        var e = $(this).attr("data-bg-color");
        "undefined" != typeof e && e !== !1 && $(this).css("background-color", "" + e)
    }), $('[data-toggle="tooltip"]').tooltip({
        animation: !0
    }), $(e).load(function () {
        $(".body-wrap").mCustomScrollbar({
            theme: "minimal",
            setLeft: "0px",
            setRight: "0px",
            setTop: "0px",
            scrollbarPosition: "outside"
        })
    }), $(e).resize(function () {
        $(".body-wrap,#map").css({
            height: $(e).height() + "px"
        }), $(".modal-content").css({
            "max-width": $(e).width() + "px"
        }), $("#background").css({
            width: $(e).width() + "px",
            height: $(e).height() + "px"
        })
    }), $(e).trigger("resize"), $("#background").each(function () {
        $("#background,header .parallax").parallax({
            scalarX: 25,
            scalarY: 15,
            frictionX: .1,
            frictionY: .1,
            calibrateX: !1
        })
    }), $("#contact").each(function () {
        jQuery.validator.addMethod("answercheck", function (e, t) {
            return this.optional(t) || /^\b10\b$/.test(e)
        }, "type the correct answer"), $("#contactform").validate({
            highlight: function (e, t) {
                $(e).fadeOut(function () {
                    $(e).fadeIn()
                })
            },
            rules: {
                name: {
                    required: !0,
                    minlength: 3
                },
                email: {
                    required: !0,
                    email: !0
                },
                phone: {
                    required: !0,
                    digits: !0,
                    minlength: 8
                },
                message: {
                    required: !0,
                    minlength: 5
                },
                answer: {
                    required: !0,
                    answercheck: !0
                }
            },
            messages: {
                answer: {
                    required: "sorry, wrong answer!"
                }
            },
            submitHandler: function (e) {
                $(e).ajaxSubmit({
                    type: "POST",
                    data: $(e).serialize(),
                    url: "assets/inc/contact.php",
                    success: function () {
                        setTimeout(function () {
                            $("#contact .message-contact").addClass("bg-success")
                        }, 300), setTimeout(function () {
                            $("#contact .message-contact").prepend("<div><i class='me-message-1'></i>Thanks!We'll be in touch real soon!</div>")
                        }, 300), setTimeout(function () {
                            $("#contact .message-contact").addClass("open")
                        }, 500), setTimeout(function () {
                            $("#contact input").fadeTo("slow", .15).prop("disabled", !0)
                        }, 2200), setTimeout(function () {
                            $("#contact textarea").fadeTo("slow", .15).prop("disabled", !0)
                        }, 2200), setTimeout(function () {
                            $("#contact button").fadeTo("slow", .15).prop("disabled", !0)
                        }, 2200), setTimeout(function () {
                            $("#contact .message-contact").removeClass("open")
                        }, 5e3), setTimeout(function () {
                            $("#contact .message-contact").removeClass("bg-success")
                        }, 5100), setTimeout(function () {
                            $("#contact .message-contact div").remove()
                        }, 5200)
                    },
                    error: function () {
                        setTimeout(function () {
                            $("#contact .message-contact").addClass("bg-danger")
                        }, 300), setTimeout(function () {
                            $("#contact .message-contact").prepend("<div><i class='me-message-1'></i>Something went wrong!try submitting the form again!</div>")
                        }, 300), setTimeout(function () {
                            $("#contact .message-contact").addClass("open")
                        }, 500), setTimeout(function () {
                            $("#contact .message-contact").removeClass("open")
                        }, 5e3), setTimeout(function () {
                            $("#contact .message-contact").removeClass("bg-danger")
                        }, 5100), setTimeout(function () {
                            $("#contact .message-contact div").remove()
                        }, 5200)
                    }
                })
            }
        })
    }), $("#newsletter").each(function () {
        function e(e) {
            $.ajax({
                type: e.attr("method"),
                url: e.attr("action"),
                data: e.serialize(),
                cache: !1,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                error: function (e) {
                    setTimeout(function () {
                        $(".modal #newsletter").before("<h4 class='text-danger' style='display:none'>Could not connect to server. Please try again later!</h4>")
                    }, 100), setTimeout(function () {
                        $(".modal .text-danger").delay(0).slideDown("slow")
                    }, 300), setTimeout(function () {
                        $(".modal .text-danger").delay(0).slideUp("fast")
                    }, 5e3), setTimeout(function () {
                        $(".modal .text-danger").remove("h4")
                    }, 5200)
                },
                success: function (e) {
                    if ("success" != e.result) {
                        var t = e.msg.substring(4);
                        setTimeout(function () {
                            $(".modal #newsletter").before("<h4 class='text-danger' style='display:none'>Something went wrong!<br>" + t + "</h4>")
                        }, 100), setTimeout(function () {
                            $(".modal .text-danger").delay(0).slideDown("slow")
                        }, 300), setTimeout(function () {
                            $(".modal .text-danger").delay(0).slideUp("fast")
                        }, 5e3), setTimeout(function () {
                            $(".modal .text-danger").remove("h4")
                        }, 5200)
                    } else {
                        var t = e.msg.substring(4);
                        setTimeout(function () {
                            $(".modal #newsletter").before("<h4 class='text-success' style='display:none'>Awesome! We sent you a confirmation email!</h4>")
                        }, 100), setTimeout(function () {
                            $(".modal .text-success").delay(0).slideDown("slow")
                        }, 300), setTimeout(function () {
                            $(".modal .text-success").delay(0).slideUp("fast")
                        }, 5e3), setTimeout(function () {
                            $(".modal .text-success").remove("h4")
                        }, 5200)
                    }
                }
            })
        }
        var t = $("#newsletter");
        $("#newsletter .submit").on("click", function (o) {
            o && o.preventDefault(), e(t)
        })
    })
}(window);