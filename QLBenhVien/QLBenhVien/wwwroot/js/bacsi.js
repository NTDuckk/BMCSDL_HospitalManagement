document.addEventListener("DOMContentLoaded", function () {
    const mainForm = document.getElementById("mainForm");
    const actionButtons = document.querySelectorAll(".action-btn");
    const btnNextPatient = document.getElementById("btnLayBenhNhanKe");
    const xacNhanSection = document.getElementById("xacNhanSection");
    const btnHuy = document.getElementById("btnHuy");
    const currentActionInput = document.getElementById("currentAction");

    const sectionMap = {
        kham: "khamContent",
        xetnghiem: "xetnghiemContent",
        ketoa: "ketoaContent"
    };

    let currentAction = "";

    function disableSidebar() {
        document.querySelectorAll(".sidebar-menu a").forEach(link => {
            link.classList.add("disabled");
            link.style.pointerEvents = "none";
            link.style.opacity = "0.4";
            link.title = "Đang xử lý. Vui lòng hoàn tất.";
        });
    }

    function enableSidebar() {
        document.querySelectorAll(".sidebar-menu a").forEach(link => {
            link.classList.remove("disabled");
            link.style.pointerEvents = "auto";
            link.style.opacity = "1";
            link.title = "";
        });
    }

    window.hoanTatXuLy = function () {
        actionButtons.forEach(btn => btn.classList.add("d-none"));
        xacNhanSection.classList.add("d-none");
        if (btnNextPatient) btnNextPatient.classList.remove("d-none");
    };

    if (mainForm && actionButtons.length) {
        actionButtons.forEach(btn => {
            btn.addEventListener("click", () => {
                const action = btn.dataset.action;
                currentAction = action;

                Object.values(sectionMap).forEach(id => document.getElementById(id)?.classList.add("d-none"));
                document.getElementById(sectionMap[action])?.classList.remove("d-none");

                xacNhanSection.classList.remove("d-none");
                actionButtons.forEach(b => b.disabled = (b !== btn));

                const postUrl = btn.getAttribute("data-post-url");
                mainForm.setAttribute("action", postUrl);
                currentActionInput.value = action;

                disableSidebar();
            });
        });

        btnHuy.addEventListener("click", () => {
            Object.values(sectionMap).forEach(id => document.getElementById(id)?.classList.add("d-none"));
            xacNhanSection.classList.add("d-none");
            currentActionInput.value = "";
            currentAction = "";
            actionButtons.forEach(b => b.disabled = false);
            mainForm.setAttribute("action", "");
            enableSidebar();
        });

        mainForm.addEventListener("submit", function (e) {
            const postUrl = mainForm.getAttribute("action");

            if (currentAction === "xetnghiem") {
                e.preventDefault();

                const maDichVuEl = mainForm.querySelector("[name='MaDichVu']");
                const ghiChuEl = mainForm.querySelector("[name='GhiChuXet']");

                const maDichVu = maDichVuEl?.value;
                const ghiChu = ghiChuEl?.value;

                const formData = new FormData();
                formData.append("MaDichVu", maDichVu);
                formData.append("GhiChuXet", ghiChu);

                fetch(postUrl, {
                    method: "POST",
                    body: formData,
                    credentials: "same-origin"
                })
                    .then(res => res.json())
                    .then(result => {
                        alert(result.message);

                        if (result.success) {
                            if (maDichVuEl && maDichVu) {
                                const selectedOption = maDichVuEl.querySelector(`option[value="${maDichVu}"]`);
                                if (selectedOption) selectedOption.remove();
                                maDichVuEl.selectedIndex = 0;
                            }
                            if (ghiChuEl) ghiChuEl.value = "";

                            btnNextPatient?.removeAttribute("disabled");

                            document.querySelectorAll(".action-btn").forEach(b => {
                                const action = b.dataset.action;
                                if (action === "kham" || action === "ketoa") {
                                    b.classList.add("d-none");
                                } else {
                                    b.disabled = false;
                                }
                            });

                            enableSidebar();
                        }

                    })
                    .catch(err => {
                        alert("Lỗi khi gửi yêu cầu xét nghiệm.");
                        console.error(err);
                    });

                return false;
            } else if (currentAction === "kham") {
                e.preventDefault();
                const trieuChung = mainForm.querySelector("[name='TrieuChung']")?.value;
                const chanDoan = mainForm.querySelector("[name='ChanDoanCuoiCung']")?.value;

                const formData = new FormData();
                formData.append("TrieuChung", trieuChung);
                formData.append("ChanDoanCuoiCung", chanDoan);
             

                fetch(postUrl, {
                    method: "POST",
                    body: formData,
                    credentials: "same-origin"
                })
                    .then(res => res.json())
                    .then(result => {
                        alert(result.message);

                        if (result.success) {
                            const xetBtn = document.querySelector("button[data-action='xetnghiem']");
                            xetBtn?.setAttribute("disabled", "true");
                            xetBtn?.classList.add("disabled");

                            const ketoaBtn = document.querySelector("button[data-action='ketoa']");
                            ketoaBtn?.classList.remove("d-none");
                            ketoaBtn.disabled = false;

                            Object.values(sectionMap).forEach(id => document.getElementById(id)?.classList.add("d-none"));
                            xacNhanSection.classList.add("d-none");

                            enableSidebar();

                            btnNextPatient?.removeAttribute("disabled");
                        }

                    })
                    .catch(err => {
                        alert("Lỗi khi gửi yêu cầu khám bệnh.");
                        console.error(err);
                    });
            } else if (currentAction === "ketoa") {
                e.preventDefault();

                const formData = new FormData();

                for (let i = 1; i <= 10; i++) {
                    const ten = mainForm.querySelector(`[name='TenThuoc_${i}']`)?.value;
                    const sl = mainForm.querySelector(`[name='SoLuong_${i}']`)?.value;
                    const lieu = mainForm.querySelector(`[name='LieuDung_${i}']`)?.value;
                    const ghiChu = mainForm.querySelector(`[name='GhiChu_${i}']`)?.value;

                    if (ten?.trim() && sl?.trim()) {
                        formData.append(`TenThuoc_${i}`, ten);
                        formData.append(`SoLuong_${i}`, sl);
                        formData.append(`LieuDung_${i}`, lieu || "");
                        formData.append(`GhiChu_${i}`, ghiChu || "");
                    }
                }

                fetch(mainForm.getAttribute("action"), {
                    method: "POST",
                    body: formData,
                    credentials: "same-origin"
                })
                    .then(res => res.json())
                    .then(result => {
                        alert(result.message);
                        if (result.success) {
                            // Tat cho di xet nghiem
                            const xetBtn = document.querySelector("button[data-action='xetnghiem']");
                            xetBtn?.setAttribute("disabled", "true");
                            xetBtn?.classList.add("disabled");

                            //  Bat Kham benh
                            const khamBtn = document.querySelector("button[data-action='kham']");
                            khamBtn?.classList.remove("d-none");
                            khamBtn.disabled = false;
                            khamBtn?.classList.remove("disabled");

                            // Bat Ke toa thuoc
                            const ketoaBtn = document.querySelector("button[data-action='ketoa']");
                            ketoaBtn?.classList.remove("d-none");
                            ketoaBtn.disabled = false;
                            ketoaBtn?.classList.remove("disabled");
                        }
                    })
                    .catch(err => {
                        alert("Lỗi khi gửi kê toa thuốc.");
                        console.error(err);
                    });
            }

        });
    }

    btnNextPatient?.addEventListener("click", () => {
        const url = btnNextPatient.getAttribute("data-url");
        if (url) {
            window.location.href = url;
        }
    });
});
