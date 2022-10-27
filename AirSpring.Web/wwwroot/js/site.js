// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    } else if (typeof exports === 'object') {
        factory(require('jquery'));
    } else {
        factory(jQuery);
    }
}(function ($) {
    var FormatPhoneNumber = {
        init: function (el, options) {
            this.el = el;
            this.format = options.format;
            this.validFormat = '#-() +';

            if (this.validateFormat()) {
                this.bindEvents();
            }
        },
        validateFormat: function () {
            for (var i = 0; i < this.format.length; i++) {
                if (this.validFormat.indexOf(this.format[i]) < 0) {
                    return false;
                }
            }
            return true;
        },
        bindEvents: function () {
            var self = this;
            self.el.on('keyup', function () {
                $(this).val(self.formatValue($(this).val()));
            });
        },
        formatValue: function (value) {
            var formatedValue = this.format;
            value = value.replace(/[^0-9]+/ig, "");
            for (var i = 0; i < value.length; i++) {
                formatedValue = formatedValue.replace("#", value[i]);
            }
            formatedValue = formatedValue.replace(/[\-\(\) #\+]+$/ig, "");
            return formatedValue;
        }
    };

    $.fn.formatPhoneNumber = function (options) {
        var formatPhoneNumber = Object.create(FormatPhoneNumber);
        formatPhoneNumber.init(this, $.extend({}, { format: '###-###-####' }, options));
    };
}));
