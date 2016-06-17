$(document).ready(function () {
    $("#submit").on("click", function () {
        $(".progress").removeAttr("hidden");
    });

    var progressNotifier = $.connection.progressHub;

    // client-side sendMessage function that will be called from the server-side
    progressNotifier.client.sendMessage = function (message, count) {
        // update progress
        UpdateProgress(message, count);
    };

    // establish the connection to the server and start server-side operation
    $.connection.hub.start().done(function () {
        // call the method CallLongOperation defined in the Hub
        progressNotifier.server.getCountAndMessage();
    });
});

function UpdateProgress(message, count) {
    var result = $("#result");
    result.html(message);

    // update progress bar
    $(".progress-bar").css("width", count + "%");
};