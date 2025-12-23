<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebBanSach.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <link href="My-Style/sanpham.css" rel="stylesheet" />

    <div class="container mt-4">
        <div class="row">
            <div class="col-12">
                <div class="p-2 mb-4 bg-success bg-opacity-10 border-start border-success border-4 d-flex align-items-center">
                    <i class="icon icon-book me-2 text-success" style="font-size: 1.5rem;"></i>
                    <h4 class="mb-0 text-success fw-bold text-uppercase">SÁCH MỚI</h4>
                </div>
            </div>
        </div>

        <div class="row">
        </div>
    </div>

    <asp:Repeater runat="server" DataSourceID="dsSach">
        <ItemTemplate>
            <div class="col-md-3 col-sm-6 mb-4">
                <div class="card h-100 shadow-sm text-center">
                    <img src='/Bia_sach/<%# Eval("AnhBia") %>'
                        class="card-img-top p-2"
                        alt='<%# Eval("TenSach") %>'
                        style="height: 200px; object-fit: cover;">

                    <div class="card-body d-flex flex-column">
                        <h6 class="card-title fw-bold text-dark">
                            <%# Eval("TenSach") %>
                        </h6>

                        <p class="card-text text-danger fw-bold mt-auto">
                            Giá bán: <%# Eval("Dongia", "{0:#,##0} đồng") %>
                        </p>

                        <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-success w-100">
                            <i class="bi bi-cart-plus"></i> Chi tiết
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <%--<div class="row">
                <div class="col-md-4 col-sm-6 mb-4">
                    <div class="product-item">
                        <img src='/Bia_sach/<%# Eval("AnhBia") %>'
                            alt='<%# Eval("TenSach") %>'
                            class="product-image" />

                        <div class="product-name">
                            <%# Eval("TenSach") %>
                        </div>

                        <div class="product-footer">
                            <div class="product-price">
                                Giá bán: <strong><%# Eval("Dongia") %></strong>
                            </div>
                            <a href="#" class="btn-detail text-uppercase">Chi tiết</a>
                        </div>
                    </div>
                </div>

            </div>--%>

        </ItemTemplate>
    </asp:Repeater>

    <asp:SqlDataSource ID="dsSach" runat="server"
        ConnectionString="<%$ ConnectionStrings:BookStoreDBConnectionString %>"
        ProviderName="<%$ ConnectionStrings:BookStoreDBConnectionString.ProviderName %>"
        SelectCommand="SELECT TOP 6 * FROM [Sach] ORDER BY [Ngaycapnhat] DESC"></asp:SqlDataSource>
</asp:Content>
