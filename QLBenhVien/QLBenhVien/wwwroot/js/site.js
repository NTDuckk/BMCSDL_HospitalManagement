document.addEventListener("DOMContentLoaded", function () {
    const logoutLink = document.getElementById("logoutLink");
    if (logoutLink) {
        logoutLink.addEventListener("click", function (e) {
            e.preventDefault();

            const confirmLogout = confirm("Bạn có chắc chắn muốn đăng xuất không?");
            if (confirmLogout) {
               
                localStorage.removeItem("DoctorKey");

                window.location.href = '/Account/Logout';
            }
        });
    }
});