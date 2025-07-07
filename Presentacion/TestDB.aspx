<%@ Page Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string cadena = System.Configuration.ConfigurationManager.AppSettings["cadenaConexion"];
            using (System.Data.SqlClient.SqlConnection conexion = new System.Data.SqlClient.SqlConnection(cadena))
            {
                conexion.Open();
                Response.Write("✅ Conexión exitosa a la base de datos.");
            }
        }
        catch (Exception ex)
        {
            Response.Write("❌ Error de conexión: " + ex.Message);
        }
    }
</script>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Prueba de conexión</title>
</head>
<body>
    <h2>Verificación de conexión a la base de datos</h2>
</body>
</html>
