<%@ Page Title="Chi tiết sách" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WebBanSach.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="container mt-5 mb-5">
        <asp:Repeater ID="rptChiTiet" runat="server" DataSourceID="dsChiTietSach">
            <ItemTemplate>
                <div class="row mb-4">
                    <div class="col-12">
                        <div class="d-flex align-items-center p-2 bg-success bg-opacity-10 border-start border-success border-4">
                            <i class="icon icon-book text-success fs-4 me-2"></i>
                            <h4 class="mb-0 text-success fw-bold text-uppercase">THÔNG TIN CHI TIẾT QUYỂN SÁCH</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4 text-center">
                        <img src='/Bia_sach/<%# Eval("AnhBia") %>'
                            class="img-fluid border p-2 shadow-sm"
                            alt='<%# Eval("TenSach") %>'
                            style="max-height: 400px; object-fit: contain;" />
                    </div>

                    <div class="col-md-8">
                        <h2 class="text-primary fw-bold mb-3"><%# Eval("TenSach") %></h2>

                        <div class="p-3 bg-light rounded border">
                            <p class="mb-2"><strong>Mã sách:</strong> <%# Eval("MaSach") %></p>
                            <p class="mb-2 text-danger fs-5">
                                <strong>Giá bán:</strong> <%# Eval("Dongia", "{0:#,##0} đồng") %>
                            </p>
                            <p class="mb-2"><strong>Ngày cập nhật:</strong> <%# Eval("Ngaycapnhat", "{0:dd/MM/yyyy}") %></p>

                        </div>

                        <div class="mt-4">
                            <h5 class="border-bottom pb-2 fw-bold text-secondary">MÔ TẢ NỘI DUNG</h5>
                            <p class="text-justify mt-2" style="line-height: 1.6;">
                                <%# Eval("Mota") %>
                            </p>
                        </div>

                        <div class="mt-4 d-flex gap-2">
                            <a href="Default.aspx" class="btn btn-outline-secondary px-4">
                                <i class="fa-solid fa-backward"></i>Quay lại
                            </a>

                            <asp:LinkButton ID="btnAddToCart" runat="server"
                                CssClass="btn btn-success px-4 fw-bold"
                                OnClick="btnAddToCart_Click"
                                CommandArgument='<%# Eval("MaSach") %>'>
                            <i class="fa-solid fa-bag-shopping"></i> THÊM VÀO GIỎ
                            </asp:LinkButton>

                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <%--thông tin sách cùng loại--%>
    <div class="row mt-5 mb-3">
        <div class="col-12">
            <div class="p-2 bg-info bg-opacity-10 border-start border-info border-4 d-flex align-items-center">
                <i class="icon icon-library me-2 text-info" style="font-size: 1.2rem;"></i>
                <h5 class="mb-0 text-info fw-bold text-uppercase">Sách cùng chủ đề</h5>
            </div>
        </div>
    </div>

    <div class="row row-cols-2 row-cols-md-5 g-3">
        <asp:Repeater ID="rptSachCungCD" runat="server" DataSourceID="dsSachCungCD">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 border-0 shadow-sm text-center p-2">
                        <a href='Details.aspx?MaSach=<%# Eval("MaSach") %>' class="text-decoration-none">
                            <img src='/Bia_sach/<%# Eval("AnhBia") %>'
                                class="card-img-top img-fluid mb-2"
                                alt='<%# Eval("TenSach") %>'
                                style="height: 150px; object-fit: contain;">

                            <div class="card-body p-1">
                                <h6 class="card-title text-dark small text-truncate-2" style="height: 2.5rem; overflow: hidden;">
                                    <%# Eval("TenSach") %>
                                </h6>
                                <p class="card-text text-danger fw-bold small">
                                    <%# Eval("Dongia", "{0:#,##0} đ") %>
                                </p>
                            </div>
                        </a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <asp:SqlDataSource ID="dsChiTietSach" runat="server"
        ConnectionString="<%$ ConnectionStrings:BookStoreDBConnStr %>"
        SelectCommand="SELECT * FROM [Sach] WHERE ([MaSach] = @MaSach)">
        <SelectParameters>
            <asp:QueryStringParameter Name="MaSach" QueryStringField="MaSach" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="dsSachCungCD" runat="server"
        ConnectionString="<%$ ConnectionStrings:BookStoreDBConnStr %>"
        SelectCommand="SELECT TOP 5 MaSach, TenSach, AnhBia, Dongia FROM Sach WHERE MaCD = (SELECT MaCD FROM Sach WHERE MaSach = @MaSach) AND MaSach <> @MaSach">
        <SelectParameters>
            <asp:QueryStringParameter Name="MaSach" QueryStringField="MaSach" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
