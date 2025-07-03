<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormLogin.aspx.cs" Inherits="Presentacion.FormLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
    <h2 class="text-center">login de Usuario</h2>
    <div class="card p-4 shadow-lg">
        <div class="mb-3">
            <label for="txtEmail" class="form-label">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" required></asp:TextBox>
        </div>
        <div class="mb-3">
            <label for="txtPassword" class="form-label">Contraseña:</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" required></asp:TextBox>
        </div>
        <div class="text-center">
            <asp:Button runat="server" Text="Iniciar"  CssClass="btn btn-primary" ID="ButtonIniciar" OnClick="ButtonIniciar_Click"/>
        </div>
        <div>

        </div>
    </div>
</div>
</asp:Content>
