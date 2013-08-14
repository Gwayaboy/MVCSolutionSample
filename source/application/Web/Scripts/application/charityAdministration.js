$(function () {

    //internal functions
    var toggleButtonsDependingOnSelectedAuthenticationType =
            function() {
                $("#btnAuthSettings").toggle($(this).val() === "OffPremise");
                $("#btnManageDonors").toggle($(this).val() === "OnPremise");
            },
        webServiceUrlIsEmpty = function() {
            return $.trim($("#WebServiceUrl").val()) === "";
        },
        
        applyValidation = function() {
            var isEmpty = webServiceUrlIsEmpty();
            
            $("#WebServiceUrl").toggleClass("input-validation-error", isEmpty);
            $("#webServiceSettings span.field-validation-error").toggle(isEmpty);
        },
        saveWebServiceUrl = function () {
            if (webServiceUrlIsEmpty()) {
                applyValidation();
                return;
            }
        
            donorSpace.spinner.start();

            $.ajax({
                url: $("#postUrl").val(),
                type: "POST",
                dataType: "json",
                data: { Id: $("#Id").val(), WebServiceUrl: $("#WebServiceUrl").val() }
            })
            .always(function() {
                donorSpace.spinner.stop();
            })
            .done(function (result) {
                if (result || result.IsSuccessFull) {
                    $("#webServiceSettings").dialog("close");
                }
            })
            .fail(function (jqXhr, textStatus, errorThrown) {
                $("#WebServiceUrl").addClass("input-validation-error");
                $("#webServiceSettings span.field-validation-error")
                    .text(textStatus + ": " + errorThrown + ","+jqXhr)
                    .show();
            });

        },
        openDialogWhenOffPremiseAuthentication = function() {
            if ($("input[type=radio][name=AuthenticationType]:checked").val() == "OffPremise") {
                $("#webServiceSettings").dialog({
                    modal: true,
                    height: 210,
                    width: 800,
                    buttons: {
                        Save: saveWebServiceUrl,
                        Cancel: function() {
                            $(this).dialog("close");
                        }
                    }
                });
            }
        };


    //buttonize links
    $(".button").button();

    //susbcribe to radio button group clicks
    $("input[type=radio][name=AuthenticationType]").click(toggleButtonsDependingOnSelectedAuthenticationType);

    //susbcribe to authentication setting button
    $("#btnAuthSettings").click(openDialogWhenOffPremiseAuthentication);

    $("#WebServiceUrl").keyup(applyValidation);
});