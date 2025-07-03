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
                accesoDatos.SetConsulta("select admin,email,pass from CATALOGO_WEB_DB.dbo.USERS WHERE email = @email and pass = @pass");
                accesoDatos.SetearParametro("@email", trainee.Email);
                accesoDatos.SetearParametro("@pass", trainee.Pass);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    trainee.Id = (int)accesoDatos.Lector["id"];
                    trainee.Admin = (bool)accesoDatos.Lector["admin"];

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
