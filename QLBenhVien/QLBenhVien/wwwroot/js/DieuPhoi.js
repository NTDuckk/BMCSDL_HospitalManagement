document.addEventListener('DOMContentLoaded', function () {
    const table = document.getElementById('table-dieuphoi');
    if (!table) return;

    const rows = table.querySelectorAll('.row-dieuphoi');

    rows.forEach(row => {
        const dropdown = row.querySelector('.phong-kham-dropdown');
        const maKhoa = row.dataset.makhoa || "";
        const maDichVu = parseInt(row.dataset.madichvu, 10);
        const selectedValue = row.dataset.selected;
        const maPhongKhamDaCo = row.dataset.maphongkham;

        row.addEventListener('click', function () {
            rows.forEach(r => r.classList.remove('selected-dieuphoi'));
            row.classList.add('selected-dieuphoi');
        });

        if (dropdown && dropdown.options.length <= 1 && maDichVu && !maPhongKhamDaCo) {
            fetch(`/DieuPhoi/GetPhongKham?maKhoa=${encodeURIComponent(maKhoa)}&maDichVu=${maDichVu}`)
                .then(res => res.ok ? res.json() : Promise.reject("Lỗi tải"))
                .then(data => {
                    console.log("Dữ liệu trả về từ fetch:", data);

                    if (!Array.isArray(data)) {
                        dropdown.innerHTML = '<option value="">Không có phòng khám phù hợp</option>';
                        return;
                    }

                    dropdown.innerHTML = '<option value="">-- Chọn phòng khám --</option>';
                    data.forEach(p => {
                        const opt = document.createElement('option');
                        opt.value = p.maPhongKham;
                        opt.textContent = `${p.maPhongKham} - ${p.tenPhongKham}`;
                        if (selectedValue && selectedValue === p.maPhongKham) {
                            opt.selected = true;
                        }
                        dropdown.appendChild(opt);
                    });
                })
                .catch(err => {
                    console.error("Lỗi lấy phòng khám:", err);
                    dropdown.innerHTML = '<option>Lỗi tải phòng</option>';
                });
        }

        if (dropdown) {
            dropdown.addEventListener('change', function () {
                const selectedText = dropdown.options[dropdown.selectedIndex].text;
                this.title = selectedText;
            });
        }
    });


    const btn = document.getElementById('btnDieuPhoi');
    if (btn) {
        btn.addEventListener('click', function (e) {
            const selectedRow = document.querySelector('.row-dieuphoi.selected-dieuphoi');
            if (!selectedRow) {
                alert("Vui lòng chọn một dòng để điều phối.");
                e.preventDefault();
                return;
            }

            const stt = selectedRow.dataset.id;
            const dropdown = selectedRow.querySelector('select.phong-kham-dropdown');
            const maPhongKham = dropdown?.value;

            if (!maPhongKham) {
                alert("Vui lòng chọn phòng khám.");
                e.preventDefault();
                return;
            }

            const confirmResult = confirm(`Xác nhận điều phối STT ${stt} vào phòng khám ${maPhongKham}?`);
            if (!confirmResult) {
                e.preventDefault();
                return;
            }

            document.getElementById('selectedStt').value = stt;
            document.getElementById('selectedPhongKham').value = maPhongKham;
        });
    }
});
