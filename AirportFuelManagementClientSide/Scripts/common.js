// Code to execute on every page load
$(document).ready(function() {

    var jwtToken = (localStorage.getItem('jwtToken') || null);
    if(jwtToken == null) window.location.href = 'UserLogin.html';
    else{
        $('#navbar').load('navbar.html');
    }
});
