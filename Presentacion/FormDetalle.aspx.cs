using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class FormDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idParam = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idParam) && int.TryParse(idParam, out int id))
                {
                    ArticuloData data = new ArticuloData();
                    Articulo art = data.BuscarPorId(id);

                    if (art != null)
                    {
                        lblNombre.Text = art.Nombre;
                        lblCodigo.Text = art.Codigo;
                        lblMarca.Text = art.Marca?.Descripcion;
                        lblCategoria.Text = art.Categoria?.Descripcion;
                        lblPrecio.Text = art.Precio.ToString("N2");
                        lblDescripcion.Text = art.Descripcion;
                        imgProducto.ImageUrl = art.ImagenUrl;
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}