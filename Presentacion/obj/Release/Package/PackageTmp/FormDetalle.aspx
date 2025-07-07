<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormDetalle.aspx.cs" Inherits="Presentacion.FormDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Detalle del Producto</h2>

    <div class="row">
        <div class="col-md-4">
            <asp:Image ID="imgProducto" runat="server" CssClass="img-fluid rounded shadow" ImageUrl="" onerror="this.src='img/error.jpg';" />
        </div>
        <div class="col-md-8">
            <h3>
                <asp:Label ID="lblNombre" runat="server" /></h3>
            <p>
                <strong>Código:</strong>
                <asp:Label ID="lblCodigo" runat="server" />
            </p>
            <p>
                <strong>Marca:</strong>
                <asp:Label ID="lblMarca" runat="server" />
            </p>
            <p>
                <strong>Categoría:</strong>
                <asp:Label ID="lblCategoria" runat="server" />
            </p>
            <p><strong>Precio:</strong> $<asp:Label ID="lblPrecio" runat="server" /></p>
            <p><strong>Descripción:</strong></p>
            <p>
                <asp:Label ID="lblDescripcion" runat="server" />
            </p>
            <asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="Default.aspx" CssClass="btn btn-secondary mt-3">Volver al catálogo</asp:HyperLink>
        </div>
    </div>


</asp:Content>
