
window.donorSpace = window.donorSpace || {};
donorSpace.spinner = function () {

    var spinnerSelector = ".spinner",
        adjustSpinnerPanel = function () {
            $(spinnerSelector)
                .parent()
                .height($(window).height() - 2)
                .width($(window).width() - 2);
            return donorSpace.spinner;
        };
    var visible = false;

    var showSpinnerWhenPostingWithNoValidationErrors = function() {

        var hasNoValidationErrors = $(this).validate().numberOfInvalids() === 0;

        if (hasNoValidationErrors) {
            donorSpace.spinner.start();
        }
        return true;
    };

    return {
        setSelector: function (selector) {
            spinnerSelector = selector || ".spinner";
            return this;
        },

        adjust: adjustSpinnerPanel,
        start: function () {
            adjustSpinnerPanel();
            $(spinnerSelector).parent().removeClass("invisible");
            visible = true;
        },


        stop: function () {
            $(spinnerSelector).parent().addClass("invisible");
            visible = false;
        },

        registerOnAjaxStartAndStop: function () {
            $(document)
                .ajaxStart(this.start)
                .ajaxStop(this.stop);

            return this;
        },

        deregisterOnAjaxStartAndStop: function () {
            $(document).off('ajaxStart');
            $(document).off('ajaxStop');
        },
        
        isVisible: function () {
            return visible;
        },
        
        showWhenPosting : function(formSelector){
            $(formSelector).submit(showSpinnerWhenPostingWithNoValidationErrors);
        }
    };
}();


