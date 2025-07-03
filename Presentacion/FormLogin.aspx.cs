using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;


namespace Presentacion
{
    public partial class FormLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonIniciar_Click(object sender, EventArgs e)
        {
            Usuario trainee = new Usuario();
            UsuarioData data = new UsuarioData();

            try
            {
                trainee.Email = txtEmail.Text;
                trainee.Pass = txtPassword.Text;
                if (data.Login(trainee))
                {
                    Session.Add("trainee", trainee);
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    Session.Add("error", "User o pass incorrecto");
                    Response.Redirect("Error.aspx", false);
                }


            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }
    }
}