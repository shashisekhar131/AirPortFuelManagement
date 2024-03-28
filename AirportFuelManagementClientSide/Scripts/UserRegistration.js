$(document).ready(function () {
    $('#userRegistrationForm').submit(function (event) {
        event.preventDefault(); // Prevent form submission


// Create data object to be sent to the server
     var user = {
    Email: $('#email').val(),
    Password: $('#password').val(),
    Name: $('#name').val()
    };

    function handleSuccess(response) {
        if(response){
            window.location.href='../Views/UserLogin.html';
        }
        console.log('Registration successful:', response);
      }

    function handleError(xhr, status, error) {
        console.log('Registration failed:', error);
    }

    makePostRequest('/User',user, handleSuccess, handleError);

});
});