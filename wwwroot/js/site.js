document.addEventListener("DOMContentLoaded", function () {
    // Form validation function
    document.getElementById('contactForm').onsubmit = function () {
        return validateForm();
    };

    function validateForm() {
        // Clear previous error messages
        document.getElementById('validationSummary').innerText = '';
        document.getElementById('fullNameError').innerText = '';
        document.getElementById('emailError').innerText = '';
        document.getElementById('messageError').innerText = '';

        let isValid = true;

        // Validate Full Name
        const fullName = document.getElementById('fullName').value;
        if (!/^[A-ZÀ-ÿ][a-zÀ-ÿ]*([-'\s][A-ZÀ-ÿ][a-zÀ-ÿ]*)*$/.test(fullName)) {
            document.getElementById('fullNameError').innerText = 'Please enter your full name, starting with a capital letter.';
            isValid = false;
        }

        // Validate Email
        const email = document.getElementById('email').value;
        if (!/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email)) {
            document.getElementById('emailError').innerText = 'Please enter a valid email address.';
            isValid = false;
        }

        // Validate Message
        const message = document.getElementById('message').value;

        // Check if the message exceeds 20 characters
        if (message.length > 35) {
            document.getElementById('messageError').innerText = 'Message cannot exceed 35 characters.';
            isValid = false;
        } else if (!/^[A-Za-z\s]+$/.test(message)) {
            // Check if the message contains only letters and spaces
            document.getElementById('messageError').innerText = 'Message can only contain letters and spaces.';
            isValid = false;
        }

        // If not valid, display summary error message
        if (!isValid) {
            document.getElementById('validationSummary').innerText = 'Please correct the errors above.';
        }

        return isValid; // Prevent form submission if not valid
    }
});
