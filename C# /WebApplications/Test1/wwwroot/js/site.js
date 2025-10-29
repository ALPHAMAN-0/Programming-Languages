// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function() {
    // Initialize all modals
    var modals = document.querySelectorAll('.modal');
    modals.forEach(function(modal) {
        new bootstrap.Modal(modal);
    });
});

$(document).ready(function () {
    // Initialize modal
    $('#logoModal').modal({
        keyboard: true,
        backdrop: 'static'
    });

    // Handle logo click
    $('.logo-link').on('click', function (e) {
        e.preventDefault();
        $('#logoModal').modal('show');
    });

    // Handle close button
    $('.modal .close').on('click', function () {
        $('#logoModal').modal('hide');
    });
});
