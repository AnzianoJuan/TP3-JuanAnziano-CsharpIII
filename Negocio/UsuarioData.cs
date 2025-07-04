using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioData
    {
        public bool Login(Usuario trainee)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetConsulta("SELECT id, admin, email, pass FROM CATALOGO_WEB_DB.dbo.USERS WHERE email = @email AND pass = @pass");
                accesoDatos.SetearParametro("@email", trainee.Email);
                accesoDatos.SetearParametro("@pass", trainee.Pass);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    trainee.Id = (int)accesoDatos.Lector["id"];
                    trainee.Admin = (bool)accesoDatos.Lector["admin"];
                    // Si necesitás los otros campos también:
                     trainee.Email = accesoDatos.Lector["email"].ToString();
                     trainee.Pass = accesoDatos.Lector["pass"].ToString();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }





    }


}

