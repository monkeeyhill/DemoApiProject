/**
 * @author thuanlv
 * 17/11/2018
 */
(function ($) {
	"use strict";
	$('.column100').on('mouseover', function () {
		var table1 = $(this).parent().parent().parent();
		var table2 = $(this).parent().parent();
		var verTable = $(table1).data('vertable') + "";
		var column = $(this).data('column') + "";

		$(table2).find("." + column).addClass('hov-column-' + verTable);
		$(table1).find(".row100.head ." + column).addClass('hov-column-head-' + verTable);
	});

	$('.column100').on('mouseout', function () {
		var table1 = $(this).parent().parent().parent();
		var table2 = $(this).parent().parent();
		var verTable = $(table1).data('vertable') + "";
		var column = $(this).data('column') + "";

		$(table2).find("." + column).removeClass('hov-column-' + verTable);
		$(table1).find(".row100.head ." + column).removeClass('hov-column-head-' + verTable);
	});

	// open modal
	$("#btn-add").click(function () {
		$('#form__register').addClass('open')
	});

	// close modal
	$(".btn-close").click(function () {
		$('#form__register').removeClass('open')
	});
	// close modal
	$("#btn-cancel").click(function () {
		$('#form__register').removeClass('open')
	});

	// validate form
	$("form[name='form__register']").validate({
		// Specify validation rules
		rules: {
			firstname: "required",
			lastname: "required",
			email: {
				required: true,
				email: true
			},
			password: {
				required: true,
				minlength: 5
			},
			phone: "required"
		},
		messages: {
			firstname: "Please enter your firstname",
			lastname: "Please enter your lastname",
			password: {
				required: "Please provide a password",
				minlength: "Your password must be at least 5 characters long"
			},
			email: "Please enter a valid email address",
			phone: "Please enter a valid phone"
		},

		// use highlight and unhighlight
		highlight: function (element, errorClass, validClass) {
			$(element.form).find("input[name=" + element.name + "]").addClass("input-error");
		},
		unhighlight: function (element, errorClass, validClass) {
			$(element.form).find("input[name=" + element.name + "]").removeClass("input-error");
		},
		submitHandler: function (form) {
			form.submit();
		}
	});

})(jQuery);