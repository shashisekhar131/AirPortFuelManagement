$(document).ready(function () {
    $('#userRegistrationForm').submit(function (event) {
        event.preventDefault(); // Prevent form submission


// Create data object to be sent to the server
     var user = {
    Email: $('#email').val(),
    Password: $('#password').val(),
    Name: $('#name').val()
    };
    $.ajax({
        url: 'https://localhost:7053/api/User',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(user),
        success: function (response) {
            if(response){
                window.location.href='../Views/UserLogin.html';
            }
            console.log('Registration successful:', response);
        },
        error: function (xhr, status, error) {
            console.log('Registration failed:', error);
        }
    });
});
});