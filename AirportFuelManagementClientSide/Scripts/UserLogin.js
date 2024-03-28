$(document).ready(function() {

    $('#loginForm').submit(function(event) {
      event.preventDefault(); 

      var UserEmail = $('#userEmail').val();
      var UserPassword = $('#password').val(); 

      function handleSuccess(response) {         
        localStorage.setItem('jwtToken', response.token);
        window.location.href="../Views/AirportsList.html";
      }

    function handleError(xhr, status, error) {
      if (xhr.status === 401) {
        $('#toastContainer .toast').toast('show');
      } else {
        alert("An error occurred while processing your request. Please try again later.");
      }    
        }

    makePostRequest('/Login/AuthenticateUser',{UserEmail,UserPassword}, handleSuccess, handleError);

      
    });
  });
