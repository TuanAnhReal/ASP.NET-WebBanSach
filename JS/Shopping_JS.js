$(document).ready(function () {
    // 1. Xử lý nút Cập nhật số lượng [cite: 218]
    $('.btn-update').click(function () {
        let id = $(this).data('id');
        let sl = $(this).closest('tr').find('.txt-soluong').val();

        $.ajax({
            type: "POST",
            url: "GioHang.aspx/CapNhatSoLuong",
            data: JSON.stringify({ maSach: id, mới: parseInt(sl) }),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.d !== "error") {
                    $('#row-' + id + ' .thanhtien').text(res.d.thanhTien + " đồng");
                    $('.tong-tien').text(res.d.tongTien + " đồng");
                    $("[id$='cart-count']").text(res.d.tongSL);
                    alert("Đã cập nhật số lượng thành công!");
                }
            }
        });
    });

    // 2. Xử lý nút Xóa sản phẩm [cite: 219]
    $('.btn-delete').click(function () {
        if (!confirm("Bạn muốn xóa sách này khỏi giỏ?")) return;
        let id = $(this).data('id');

        $.ajax({
            type: "POST",
            url: "GioHang.aspx/XoaSanPham",
            data: JSON.stringify({ maSach: id }),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.d !== "error") {
                    $('#row-' + id).fadeOut(300, function () { $(this).remove(); });
                    $('.tong-tien').text(res.d.tongTien + " đồng");
                    $("[id$='cart-count']").text(res.d.tongSL);
                }
            }
        });
    });

    // 3. Xử lý nút Trả toàn bộ [cite: 220]
    $('#btn-delete-all').click(function () {
        if (!confirm("Bạn muốn xóa sạch giỏ hàng?")) return;
        $.ajax({
            type: "POST",
            url: "GioHang.aspx/XoaToanBo",
            contentType: "application/json; charset=utf-8",
            success: function () {
                location.reload(); // Tải lại trang để hiện giỏ hàng trống
            }
        });
    });
});