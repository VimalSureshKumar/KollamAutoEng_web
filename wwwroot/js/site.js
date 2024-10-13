// Wait for the DOM to fully load
document.addEventListener("DOMContentLoaded", function () {

    // Attach the form validation function to the contact form's submit event
    document.getElementById('contactForm').onsubmit = function () {
        return validateForm(); // Call the validation function
    };

    // Form validation function
    function validateForm() {
        // Clear previous error messages
        document.getElementById('validationSummary').innerText = ''; // Reset summary message
        document.getElementById('fullNameError').innerText = ''; // Reset full name error
        document.getElementById('emailError').innerText = ''; // Reset email error
        document.getElementById('messageError').innerText = ''; // Reset message error

        let isValid = true; // Track overall validity of the form

        // Validate Full Name
        const fullName = document.getElementById('fullName').value; // Get full name input
        // Regular expression to validate full name format
        if (!/^[A-ZÀ-ÿ][a-zÀ-ÿ]*([-'\s][A-ZÀ-ÿ][a-zÀ-ÿ]*)*$/.test(fullName)) {
            // If invalid, set error message
            document.getElementById('fullNameError').innerText = 'Please enter your full name, starting with a capital letter.';
            isValid = false; // Mark form as invalid
        }

        // Validate Email
        const email = document.getElementById('email').value; // Get email input
        // Regular expression to validate email format
        if (!/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email)) {
            // If invalid, set error message
            document.getElementById('emailError').innerText = 'Please enter a valid email address.';
            isValid = false; // Mark form as invalid
        }

        // Validate Message
        const message = document.getElementById('message').value; // Get message input

        // Check if the message exceeds 35 characters
        if (message.length > 35) {
            // If too long, set error message
            document.getElementById('messageError').innerText = 'Message cannot exceed 35 characters.';
            isValid = false; // Mark form as invalid
        } else if (!/^[A-Za-z\s]+$/.test(message)) {
            // Check if the message contains only letters and spaces
            document.getElementById('messageError').innerText = 'Message can only contain letters and spaces.';
            isValid = false; // Mark form as invalid
        }

        // If there are any validation errors, display a summary error message
        if (!isValid) {
            document.getElementById('validationSummary').innerText = 'Please correct the errors above.';
        }

        return isValid; // Return the validity status to prevent form submission if not valid
    }
});
