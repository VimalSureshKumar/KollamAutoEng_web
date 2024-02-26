// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function toggleMenu() {
    var menuOptions = document.getElementById("menuOptions");
    menuOptions.style.display = (menuOptions.style.display === "block") ? "none" : "block";
}

// Handling click events for the login and register links
document.getElementById("registerLink").addEventListener("click", function (event) {
    event.preventDefault(); // Prevent default link behavior
    // Redirect to the Register page
    window.location.href = "/Identity/Account/Register";
});

document.getElementById("loginLink").addEventListener("click", function (event) {
    event.preventDefault(); // Prevent default link behavior
    // Redirect to the Login page
    window.location.href = "/Identity/Account/Login";
});