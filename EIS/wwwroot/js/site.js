// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');

    //Normal click
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        event.preventDefault();
        document.getElementById("myButton").disabled = true;
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal({ backdrop: "static" });
            PlaceHolderElement.find('.modal').modal('show');
        })
    });

    //Double click disabled
    $('button[data-toggle="ajax-modal"]').dblclick(function (event) {
        event.preventDefault();
        alert('Double click disabled...');
    });

    //Save popup windows and close
    PlaceHolderElement.on('click', 'button[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
            setTimeout(() => { window.location.reload(true); }, 1000);
            document.getElementById("myButton").disabled = false;
        })
    });

    //Close popup windows
    PlaceHolderElement.on('click', 'button[data-dismiss="modal"]', function (event) {
        PlaceHolderElement.find('.modal').modal('hide');
        document.getElementById("myButton").disabled = false;
    });
})