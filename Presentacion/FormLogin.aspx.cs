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
            try
            {
                Usuario trainee = new Usuario();
                trainee.Email = txtEmail.Text;
                trainee.Pass = txtPassword.Text;

                UsuarioData data = new UsuarioData();

                if (data.Login(trainee))
                {
                    // Guardar el usuario completo en la sesión
                    Session.Add("trainee", trainee);
                    // Guardar adicionalmente si es admin o no (opcional)
                    Session["esAdmin"] = trainee.Admin;

                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    Session.Add("error", "Email o contraseña incorrectos");
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