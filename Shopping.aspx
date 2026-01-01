<%@ Page Title="Giỏ hàng" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="Shopping.aspx.cs" Inherits="WebBanSach.Shopping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container mt-5">
        <div class="row mb-4">
            <div class="col-12">

                <div class="p-2 mb-4 bg-success bg-opacity-10 border-start border-success border-4 d-flex align-items-center">
                    <i class="fa-solid fa-bag-shopping text-success fs-4 me-2"></i>
                    <h4 class="mb-0 text-success fw-bold text-uppercase">Thông tin hàng hóa trong giỏ hàng</h4>
                </div>
            </div>
        </div>

        <div class="table-responsive">
            <table class="table table-bordered align-middle">
                <thead class="table-light">
                    <tr class="text-center">
                        <th>Hình sản phẩm</th>
                        <th>Tên sản phẩm</th>
                        <th>Số lượng</th>
                        <th>Đơn giá</th>
                        <th>Thành tiền</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptGioHang" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center" style="width: 150px;">
                                    <img src='/Bia_sach/<%# Eval("AnhBia") %>' class="img-fluid" style="max-height: 80px;" />
                                </td>
                                <td class="fw-bold text-primary"><%# Eval("TenSach") %></td>
                                <td class="text-center" style="width: 100px;">
                                    <%# Eval("SoLuong") %>
                                </td>
                                <td class="text-end text-danger"><%# Eval("DonGia", "{0:#,##0}") %> đồng</td>
                                <td class="text-end fw-bold text-danger"><%# Eval("ThanhTien", "{0:#,##0}") %> đồng</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr class="bg-light">
                        <td colspan="4" class="text-end fw-bold fs-5">Tổng cộng:</td>
                        <td class="text-end fw-bold fs-5 text-danger">
                            <asp:Literal ID="ltrTongTien" runat="server"></asp:Literal>
                            đồng
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>

        <div class="d-flex justify-content-between mt-4 mb-5">
            <a href="Default.aspx" class="btn btn-outline-success px-4">Tiếp tục mua hàng</a>
            <div class="d-flex gap-2">
                <%-- Các nút chức năng sẽ xử lý sau --%>
                <button class="btn btn-secondary disabled">Cập nhật giỏ hàng</button>
                <button class="btn btn-danger disabled">Xóa giỏ hàng</button>
            </div>
        </div>
    </div>
</asp:Content>
