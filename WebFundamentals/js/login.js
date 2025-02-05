function LoginPost() {
    var email = document.getElementById("txtEmail").value;
    var password = document.getElementById("txtPassword").value;
   
    if (email == 'mr.kyaing7@gmail.com' && password == '303') {
        localStorage.setItem('userEmail', email);
        location.href = "dashboard.html";
    }
    else {
        alert("Invalid User!!");
    }
}