document.querySelector(".log-out-btn").addEventListener('click', function() {
    if (localStorage.getItem("jwtToken")) {
        localStorage.clear(); 
    }
    window.location.href ="../Views/UserLogin.html";
});
