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
            if (Session["trainee"] == null)
                Response.Redirect("FormLogin.aspx");


            if (!IsPostBack)
            {
                ArticuloData data = new ArticuloData();
                List<Articulo> lista = data.Listar();
                repRepetidor.DataSource = lista;
                repRepetidor.DataBind();
            }
        }

        protected void repRepetidor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("FormDetalle.aspx?id=" + id);
            }
        }

        protected void txtBuscarRapido_TextChanged(object sender, EventArgs e)
        {

        }
    }
}