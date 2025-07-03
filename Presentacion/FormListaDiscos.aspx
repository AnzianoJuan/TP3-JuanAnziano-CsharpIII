<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormListaDiscos.aspx.cs" Inherits="Presentacion.FormListaDiscos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <h2 class="mb-4">Gestión de Artículos</h2>

    <!-- Búsqueda rápida y botones superiores -->
    <div class="row mb-3">
        <div class="col-md-3">
            <asp:TextBox ID="txtFiltro" CssClass="form-control" runat="server" placeholder="Buscar por nombre, marca o categoría..." />
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnBuscarRapido" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" OnClick="btnBuscarRapido_Click" />
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnMostrarFormulario" runat="server" Text="Agregar nuevo artículo" CssClass="btn btn-success w-100" OnClick="btnMostrarFormulario_Click" />
        </div>
    </div>

    <!-- Grilla -->
    <asp:GridView ID="dgvArticulos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
        DataKeyNames="Id" OnRowCommand="dgvArticulos_RowCommand">
        <Columns>
            <asp:BoundField DataField="Codigo" HeaderText="Código" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Marca.Descripcion" HeaderText="Marca" />
            <asp:BoundField DataField="Categoria.Descripcion" HeaderText="Categoría" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Ver" CommandName="Ver" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-outline-info btn-sm me-1" />
                    <asp:Button runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-outline-warning btn-sm me-1" />
                    <asp:Button runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-outline-danger btn-sm" OnClientClick="return confirm('¿Eliminar artículo definitivamente?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!-- Formulario de alta/edición/visualización -->
    <asp:Panel ID="pnlFormulario" runat="server" Visible="false" CssClass="mt-5 border p-4 rounded bg-light">
        <h4 id="tituloFormulario" runat="server"></h4>

        <asp:Label ID="lblError" runat="server" ForeColor="Red" CssClass="mb-3 d-block"></asp:Label>

        <div class="row mb-3">
            <div class="col-md-6">
                <label>Código</label>
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <label>Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-12">
                <label>Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label>Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" />
            </div>
            <div class="col-md-6">
                <label>Categoria</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label>Precio</label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <label>URL Imagen</label>
                <asp:TextBox ID="txtImagen" runat="server" CssClass="form-control" />
            </div>
        </div>

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary ms-2" OnClick="btnCancelar_Click" />
        <asp:HiddenField ID="hfIdArticulo" runat="server" />
        <asp:HiddenField ID="hfModoVista" runat="server" />
    </asp:Panel>

</asp:Content>
