// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function isValidDate(dateString) {
    // Check the format using a regular expression
    const regex = /^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])\/(200[0-9]|201[0-9]|202[0-4])$/;
    if (!regex.test(dateString)) {
        return false;
    }

    // Parse the date parts to integers
    const parts = dateString.split('/');
    const month = parseInt(parts[0], 10);
    const day = parseInt(parts[1], 10);
    const year = parseInt(parts[2], 10);

    // Check the valid range for year, month, and day
    if (year < 2000 || year > 2024) {
        return false;
    }

    // Create a Date object and check if it is valid
    const date = new Date(year, month - 1, day);
    if (date.getFullYear() !== year || date.getMonth() + 1 !== month || date.getDate() !== day) {
        return false;
    }

    // Additional range check for the specific dates
    const minDate = new Date(2000, 0, 1); // 01/01/2000
    const maxDate = new Date(2024, 0, 1); // 01/01/2024
    if (date < minDate || date > maxDate) {
        return false;
    }

    return true;
}

