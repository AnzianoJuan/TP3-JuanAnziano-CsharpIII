using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class FormListaDiscos : System.Web.UI.Page
    {
        private List<Articulo> listaArticulos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
                cargarFiltros();
            }
        }

        private void cargarGrilla()
        {
            ArticuloData data = new ArticuloData();
            listaArticulos = data.Listar();
            Session["ListaArticulos"] = listaArticulos;
            dgvArticulos.DataSource = listaArticulos;
            dgvArticulos.DataBind();
        }

        private void cargarFiltros()
        {
            MarcaData marcaData = new MarcaData();
            ddlMarca.DataSource = marcaData.listar();
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();

            ddlMarcaFiltro.DataSource = marcaData.listar();
            ddlMarcaFiltro.DataTextField = "Descripcion";
            ddlMarcaFiltro.DataValueField = "Id";
            ddlMarcaFiltro.DataBind();

            CategoriaData catData = new CategoriaData();
            ddlCategoria.DataSource = catData.listar();
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();

            ddlCategoriaFiltro.DataSource = catData.listar();
            ddlCategoriaFiltro.DataTextField = "Descripcion";
            ddlCategoriaFiltro.DataValueField = "Id";
            ddlCategoriaFiltro.DataBind();
        }

        protected void btnBuscarRapido_Click(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.ToLower();
            var lista = (List<Articulo>)Session["ListaArticulos"];
            var filtrada = lista.Where(a =>
                a.Nombre.ToLower().Contains(filtro) ||
                a.Marca.Descripcion.ToLower().Contains(filtro) ||
                a.Categoria.Descripcion.ToLower().Contains(filtro)
            ).ToList();

            dgvArticulos.DataSource = filtrada;
            dgvArticulos.DataBind();
        }

        protected void btnMostrarFormulario_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            pnlFormulario.Visible = true;
            tituloFormulario.InnerText = "Agregar nuevo artículo";
            hfModoVista.Value = "alta";
        }

        protected void dgvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(dgvArticulos.DataKeys[index].Value);
            Articulo seleccionado = ((List<Articulo>)Session["ListaArticulos"]).FirstOrDefault(x => x.Id == id);

            if (e.CommandName == "Editar")
            {
                CargarFormulario(seleccionado);
                hfModoVista.Value = "editar";
                tituloFormulario.InnerText = "Editar artículo";
                pnlFormulario.Visible = true;
            }
            else if (e.CommandName == "Ver")
            {

                e.CommandArgument.ToString();
                Response.Redirect("FormDetalle.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                ArticuloData data = new ArticuloData();
                data.EliminarFisico(id);
                cargarGrilla();
            }
        }

        private void CargarFormulario(Articulo art)
        {
            hfIdArticulo.Value = art.Id.ToString();
            txtCodigo.Text = art.Codigo;
            txtNombre.Text = art.Nombre;
            txtDescripcion.Text = art.Descripcion;
            txtPrecio.Text = art.Precio.ToString("0.00");
            txtImagen.Text = art.ImagenUrl;
            ddlMarca.SelectedValue = art.Marca.Id.ToString();
            ddlCategoria.SelectedValue = art.Categoria.Id.ToString();

            HabilitarCampos();
            btnGuardar.Visible = true;
        }

        private void LimpiarFormulario()
        {
            hfIdArticulo.Value = "";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtImagen.Text = "";
            ddlMarca.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
            HabilitarCampos();
            btnGuardar.Visible = true;
            lblError.Text = "";
        }

        private void DeshabilitarCampos()
        {
            txtCodigo.Enabled = false;
            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
            txtPrecio.Enabled = false;
            txtImagen.Enabled = false;
            ddlMarca.Enabled = false;
            ddlCategoria.Enabled = false;
            btnGuardar.Visible = false;
        }

        private void HabilitarCampos()
        {
            txtCodigo.Enabled = true;
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            txtPrecio.Enabled = true;
            txtImagen.Enabled = true;
            ddlMarca.Enabled = true;
            ddlCategoria.Enabled = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo
                {
                    Codigo = txtCodigo.Text,
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    ImagenUrl = txtImagen.Text,
                    Marca = new Marca { Id = int.Parse(ddlMarca.SelectedValue) },
                    Categoria = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) }
                };

                ArticuloData data = new ArticuloData();

                if (hfModoVista.Value == "editar")
                {
                    nuevo.Id = int.Parse(hfIdArticulo.Value);
                    data.modificar(nuevo);
                }
                else
                {
                    data.agregarArticulo(nuevo);
                }

                pnlFormulario.Visible = false;
                cargarGrilla();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = false;
        }

        protected void txtBuscarRapido_TextChanged(object sender, EventArgs e)
        {
            btnBuscarRapido_Click(sender, e);
        }

        protected void btnBuscarAvanzado_Click(object sender, EventArgs e)
        {
            int? idMarca = string.IsNullOrEmpty(ddlMarcaFiltro.SelectedValue) ? (int?)null : int.Parse(ddlMarcaFiltro.SelectedValue);
            int? idCategoria = string.IsNullOrEmpty(ddlCategoriaFiltro.SelectedValue) ? (int?)null : int.Parse(ddlCategoriaFiltro.SelectedValue);
            decimal? precioMin = string.IsNullOrEmpty(txtPrecio.Text) ? (decimal?)null : decimal.Parse(txtPrecio.Text);
            decimal? precioMax = string.IsNullOrEmpty(txtPrecio.Text) ? (decimal?)null : decimal.Parse(txtPrecio.Text);

            var lista = (List<Articulo>)Session["ListaArticulos"];

            var filtrada = lista.Where(a =>
                (!idMarca.HasValue || a.Marca.Id == idMarca) &&
                (!idCategoria.HasValue || a.Categoria.Id == idCategoria) &&
                (!precioMin.HasValue || a.Precio >= precioMin) &&
                (!precioMax.HasValue || a.Precio <= precioMax)
            ).ToList();

            dgvArticulos.DataSource = filtrada;
            dgvArticulos.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            btnBuscarRapido_Click(sender, e);
        }
    }
}
