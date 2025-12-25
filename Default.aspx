<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebBanSach.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <link href="My-Style/sanpham.css" rel="stylesheet" />

    <div class="container mt-4">
        <div class="row">
            <div class="col-12">
                <div class="p-2 mb-4 bg-success bg-opacity-10 border-start border-success border-4 d-flex align-items-center">
                    <i class="icon icon-book text-success fs-4 me-2"></i>
                    <h4 class="mb-0 text-success fw-bold text-uppercase">SÁCH MỚI</h4>
                </div>
            </div>
        </div>

        <div class="row">
            <asp:Repeater runat="server" DataSourceID="dsSach">
                <ItemTemplate>

                    <div class="col-md-4 col-sm-6 mb-4">
                        <div class="product-card border p-3">
                            <div class="text-center mb-3">
                                <img src='/Bia_sach/<%# Eval("AnhBia") %>'
                                    alt='<%# Eval("TenSach") %>'
                                    class="img-fluid"
                                    style="height: 200px; object-fit: contain;">
                            </div>

                            <div class="product-title text-center mb-2">
                                <a href='ChiTiet.aspx?id=<%# Eval("MaSach") %>' class="text-decoration-none text-primary fw-medium">
                                    <%# Eval("TenSach") %>
                                </a>
                            </div>

                            <div class="d-flex justify-content-between align-items-center border-top pt-2">
                                <div class="price-text" style="font-size: 0.9rem;">
                                    Giá bán: <span class="fw-bold" style="color: red"><%# Eval("Dongia", "{0:#,##0}") %> đồng</span>
                                </div>
                                <asp:HyperLink ID="lnkDetail" runat="server"
                                    NavigateUrl='<%# "ChiTietSach.aspx?ms=" + Eval("MaSach") %>'
                                    CssClass="btn btn-success btn-sm px-3"
                                    Style="background-color: #5cb85c; border: none; font-size: 0.8rem;">
                                    Chi tiết
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </div>



    <asp:SqlDataSource ID="dsSach" runat="server"
        ConnectionString="<%$ ConnectionStrings:BookStoreDBConnectionString %>"
        ProviderName="<%$ ConnectionStrings:BookStoreDBConnectionString.ProviderName %>"
        SelectCommand="SELECT TOP 6 * FROM [Sach] ORDER BY [Ngaycapnhat] DESC"></asp:SqlDataSource>
</asp:Content>
