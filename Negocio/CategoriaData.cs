﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaData
    {

        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetConsulta("select Id,Descripcion from CATEGORIAS");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {

                    Categoria aux = new Categoria();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                datos.CerrarConexion();

            }
        }


    }
}
