using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace Presentacion
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArticuloData data = new ArticuloData();
                List<Articulo> lista = data.Listar();
                repRepetidor.DataSource = lista;
                repRepetidor.DataBind();
            }
        }
    }
}