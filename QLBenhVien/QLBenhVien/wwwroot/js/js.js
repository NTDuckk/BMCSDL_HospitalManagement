//document.addEventListener("DOMContentLoaded", function () {
//    const modalElement = document.getElementById("doctorKeyModal");
//    if (modalElement) {
//        const modal = new bootstrap.Modal(modalElement, {
//            backdrop: 'static',
//            keyboard: false
//        });

//        const alreadyEnteredKey = localStorage.getItem("DoctorKey");

//        if (!alreadyEnteredKey) {
//            modal.show();
//        }

//        const openBtn = document.getElementById("openDoctorKeyModal");
//        if (openBtn) {
//            openBtn.addEventListener("click", function (e) {
//                e.preventDefault();
//                modal.show();
//            });
//        }

//        const confirmBtn = document.getElementById("confirmDoctorKey");
//        if (confirmBtn) {
//            confirmBtn.addEventListener("click", function () {
//                const key = document.getElementById("doctorKeyInput").value;
//                localStorage.setItem("DoctorKey", key);
//                modal.hide();
//            });
//        }
//    }
//});
