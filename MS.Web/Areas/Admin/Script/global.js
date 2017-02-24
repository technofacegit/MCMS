/*global window, $*/
var Global = {};
Global.FormHelper = function (formElement, options, onBeforeSubmit, onSucccess, onError) {
    "use strict";
    var settings = {};
    settings = $.extend({}, settings, options);
    formElement.data('validator', null);
    formElement.validate(settings.validateSettings);
    formElement.submit(function (e) {
        var submitBtn = formElement.find(':submit');
        if (formElement.validate().valid()) {
            submitBtn.find('i').removeClass("fa fa-arrow-circle-right");
            submitBtn.find('i').addClass("fa fa-refresh");
            submitBtn.prop('disabled', true);
            submitBtn.find('span').html('Submiting..');
            $.ajax(formElement.attr("action"), {
                type: "POST",
                data: formElement.serializeArray(),
                beforeSend: function (jqXHR, data) {
                    if (onBeforeSubmit) {
                        onBeforeSubmit(jqXHR, data);
                    }
                },
                success: function (result) {
                    if (onSucccess === null || onSucccess === undefined) {
                        if (result.isSuccess) {
                            window.location.href = result.redirectUrl;
                        } else {
                            if (settings.updateTargetId) {
                                $("#" + settings.updateTargetId).html(result);
                            }
                        }
                    } else {
                        onSucccess(result);
                    }
                },
                error: function (jqXHR, status, error) {
                    if (onError !== null && onError !== undefined) {
                        onError(jqXHR, status, error);
                    }
                },
                complete: function () {
                    submitBtn.find('i').removeClass("fa fa-refresh");
                    submitBtn.find('i').addClass("fa fa-arrow-circle-right");
                    submitBtn.find('span').html('Submit');
                    submitBtn.prop('disabled', false);
                }
            });
        }

        e.preventDefault();
    });

    return formElement;
};

Global.GridHelper = function (gridElement, options) {
    if ($(gridElement).find('thead tr th').length > 1) {
        var settings = {};
        settings = $.extend({}, settings, options);
        $(gridElement).dataTable(settings);
    }
    return $(gridElement);
};

Global.FormValidationReset = function (formElement, validateOption) {
    if ($(formElement).data('validator')) {
        $(formElement).data('validator', null);
    }

    $(formElement).validate(validateOption);

    return $(formElement);
};

Global.DropDownHelper = function (selectElement, options, callBack) {
    var settings = {};
    settings = $.extend({}, settings, options);
    $(selectElement).empty();
    var optionHtml = '';
    if (settings.optionalLabel) {
        optionHtml = '<option value="">' + settings.optionalLabel + '</option>';
    }
    $.get(settings.url, settings.data, function (result) {
        $.each(result, function (index, item) {
            optionHtml += '<option value="{0}">{1}</option>'.format(item[settings.dataValueField], item[settings.dataTextField]);
        });

        $(selectElement).html(optionHtml);
        if (callBack) { callBack(); }
    });
}


