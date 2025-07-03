<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentacion.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
    
    <h2 class="mb-4">Catálogo de Productos</h2>
    <asp:Repeater ID="repRepetidor" runat="server">
        <ItemTemplate>
            <div class="card mb-3" style="max-width: 540px;">
                <div class="row g-0">
                    <div class="col-md-4">
                        <img src='<%# Eval("ImagenUrl") %>' class="img-fluid rounded-start" onerror="this.src='img/error.jpg';" />
                    </div>
                    <div class="col-md-8">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text"><%# Eval("Descripcion") %></p>
                            <p class="card-text"><small class="text-muted">Marca: <%# Eval("Marca.Descripcion") %> | Categoría: <%# Eval("Categoria.Descripcion") %></small></p>
                            <strong>Precio: $<%# Eval("Precio", "{0:N2}") %></strong>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>


</asp:Content>
