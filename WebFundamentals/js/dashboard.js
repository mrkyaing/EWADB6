document.addEventListener('DOMContentLoaded', function() {
    var email = localStorage.getItem('userEmail');
    if (email) {
        document.getElementById('welcomeMessage').textContent = 'Welcome, ' + email;
    } else {
        document.getElementById('welcomeMessage').textContent = 'Welcome, Guest';
    }
});