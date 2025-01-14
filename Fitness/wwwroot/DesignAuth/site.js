//$(document).ready(function () {

//    'use strict';

//    var usernameError = true,
//        emailError = true,
//        passwordError = true,
//        passConfirm = true;

//    // Detect browser for css purpose
//    if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1) {
//        $('.form form label').addClass('fontSwitch');
//    }

//    // Label effect
//    $('input').focus(function () {
//        $(this).siblings('label').addClass('active');
//    });

//    // Form validation
//    $('input').blur(function () {

//        // User Name
//        if ($(this).hasClass('name')) {
//            if ($(this).val().length === 0) {
//                $(this).siblings('span.error').text('Please type your full name').fadeIn().parent('.form-group').addClass('hasError');
//                usernameError = true;
//            } else if ($(this).val().length > 1 && $(this).val().length <= 6) {
//                $(this).siblings('span.error').text('Please type at least 6 characters').fadeIn().parent('.form-group').addClass('hasError');
//                usernameError = true;
//            } else {
//                $(this).siblings('.error').text('').fadeOut().parent('.form-group').removeClass('hasError');
//                usernameError = false;
//            }
//        }

//        // Email
//        if ($(this).hasClass('email')) {
//            if ($(this).val().length == '') {
//                $(this).siblings('span.error').text('Please type your email address').fadeIn().parent('.form-group').addClass('hasError');
//                emailError = true;
//            } else {
//                $(this).siblings('.error').text('').fadeOut().parent('.form-group').removeClass('hasError');
//                emailError = false;
//            }
//        }

//        // Password
//        if ($(this).hasClass('pass')) {
//            if ($(this).val().length < 8) {
//                $(this).siblings('span.error').text('Please type at least 8 characters').fadeIn().parent('.form-group').addClass('hasError');
//                passwordError = true;
//            } else {
//                $(this).siblings('.error').text('').fadeOut().parent('.form-group').removeClass('hasError');
//                passwordError = false;
//            }
//        }



//        // label effect
//        if ($(this).val().length > 0) {
//            $(this).siblings('label').addClass('active');
//        } else {
//            $(this).siblings('label').removeClass('active');
//        }
//    });

//    // form switch
//    $('a.switch').click(function (e) {
//        $(this).toggleClass('active');
//        e.preventDefault();

//        if ($('a.switch').hasClass('active')) {
//            $(this).parents('.form-peice').addClass('switched').siblings('.form-peice').removeClass('switched');
//        } else {
//            $(this).parents('.form-peice').removeClass('switched').siblings('.form-peice').addClass('switched');
//        }
//    });

//    // Form submit
//    $('form.signup-form').submit(function (event) {
//        // منع الإرسال إذا كانت البيانات غير صحيحة
//        if (usernameError || emailError || passwordError || passConfirm === false) {
//            $('.name, .email, .pass, .passConfirm').blur();
//            event.preventDefault(); // منع الإرسال إذا كانت البيانات غير صحيحة
//        } else {
//            // إذا كانت البيانات صحيحة، لا تمنع الإرسال
//            // إرسال النموذج بشكل طبيعي
//            this.submit(); // إرسال النموذج
//        }
//    });

//    // Reload page
//    $('a.profile').on('click', function () {
//        location.reload(true);
//    });

//});


$(document).ready(function () {

    'use strict';

    // Detect browser for css purpose
    if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1) {
        $('.form form label').addClass('fontSwitch');
    }

    // Label effect
    $('input').focus(function () {
        $(this).siblings('label').addClass('active');
    });

    // Remove label effect when input is blurred
    $('input').blur(function () {
        if ($(this).val().length === 0) {
            $(this).siblings('label').removeClass('active');
        }
    });

    // Form switch
    $('a.switch').click(function (e) {
        $(this).toggleClass('active');
        e.preventDefault();

        if ($('a.switch').hasClass('active')) {
            $(this).parents('.form-peice').addClass('switched').siblings('.form-peice').removeClass('switched');
        } else {
            $(this).parents('.form-peice').removeClass('switched').siblings('.form-peice').addClass('switched');
        }
    });

    // Reload page
    $('a.profile').on('click', function () {
        location.reload(true);
    });

});
