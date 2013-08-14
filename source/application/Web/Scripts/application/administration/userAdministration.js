$(function () {
    var //private variables
        charityId = $("#administrators").attr("data-charityid"),
        loginUrl = $("#administrators").attr("data-loginUrl"),
        postUrl = $("#administrators").attr("data-deleteUrl"),
        deleteDialog,

        //private functions
        cannotDeleteUser = function(reason) {
            if (reason) {
                $("<div/>")
                    .html(reason)
                    .attr("title", "Error")
                    .dialog({
                        modal: true,
                        buttons: {
                            Ok: function() {
                                $(this).dialog("close");
                            }
                        }
                    });
            }
        },
        
        getUserName = function(row) {
            return $.trim($(row).find("td[data-userName]").attr("data-userName"));
        },
        getUserId = function(row) {
            return $(row).attr("id");
        },
        deleteUser = function() {

            var deleteButton = this,
                row = $(deleteButton).closest("tr"),
                userName = getUserName(row),
                userId = getUserId(row);

            if (!postUrl) return;

            donorSpace.spinner.start();

            $.ajax({
                url: postUrl,
                type: "POST",
                dataType: "json",
                data: {
                    UserId : userId,
                    UserName: userName,
                    CharityId: charityId
                }
            })
              .always(function () {
                  $(deleteDialog).dialog("close");
                  donorSpace.spinner.stop();
              })
            .done(function(result) {
                if (result && result.length === 0) {

                    if ($(row).hasClass("current")) {
                        window.location = loginUrl;
                    }

                    $(row).fadeOut();
                    $(row).remove();
                    
                } else {
                    var errorMessages = "";
                    for (var i = 0; i < result.length; i++) {
                        errorMessages += result[i].Value.Messages.join() + "\n";
                    }
                    cannotDeleteUser(errorMessages);
                }
            })
            .fail(function(jqXhr, textStatus, errorThrown) {
                cannotDeleteUser(errorThrown);
            });
        },
        showDeleteDialog = function() {

            var deleteButton = this,
                currentRow = $(deleteButton).closest("tr"),
                userName = getUserName(currentRow),
                message = "Are you sure you want to delete " + userName + "?";
            
            if ($(currentRow).hasClass("current")) {
                message += "\n This will result in deleting your account, loose your administrator privileges and log you off";
            }

            deleteDialog =
                $("<div/>")
                    .html(message)
                    .attr("title", "Deletion")
                    .dialog({
                        modal: true,
                        buttons: {
                            Delete: function() {
                                deleteUser.apply(deleteButton);
                            },
                            Cancel: function() {
                                $(this).dialog("close");
                            }
                        }
                    });
        };

    //buttonize links
    $(".button").button();

    //subscribe to delete button click
    $("#administrators .deleteButton").click(showDeleteDialog);
});