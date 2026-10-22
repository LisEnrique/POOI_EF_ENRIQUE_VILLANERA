using POOI_EF_ENRIQUE_VILLANERA.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


namespace POOI_EF_ENRIQUE_VILLANERA.Controllers
{
    public class ConsumidorController : Controller
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;

        private IEnumerable<Consumidor> listarConsumidores()
        {
            List<Consumidor> listaConsumidores =
                new List<Consumidor>();

            SqlConnection sqlConnection =
                new SqlConnection(cadenaConexion);

            sqlConnection.Open();

            SqlCommand sqlCommand =
                new SqlCommand(
                    "select * from tb_consumidor",
                    sqlConnection);

            SqlDataReader sqlDataReader =
                sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                listaConsumidores.Add(
                    new Consumidor(
                        sqlDataReader.GetString(0),
                        sqlDataReader.GetString(1),
                        sqlDataReader.GetString(2),
                        sqlDataReader.GetString(3),
                        sqlDataReader.GetString(4)
                    )
                );
            }

            sqlDataReader.Close();
            sqlConnection.Close();

            return listaConsumidores;
        }


        public ActionResult Index()
        {
            return View(listarConsumidores());
        }

        string guardarConsumidor(Consumidor consumidor)
        {
            if (string.IsNullOrWhiteSpace(consumidor.idpais))
            {
                return "Pais obligatorio";
            }
            if (string.IsNullOrWhiteSpace(consumidor.dnicont))
            {
                return "DNI obligatorio";
            }
            if (!consumidor.dnicont.All(char.IsDigit))
            {
                return "DNI debe ser numerico";
            }
            if (consumidor.dnicont.Length != 8)
            {
                return "DNI debe tener 8 digitos";
            }
            if (string.IsNullOrWhiteSpace(consumidor.nomcont))
            {
                return "Nombre obligatorio";
            }

            if (string.IsNullOrWhiteSpace(consumidor.apecont))
            {
                return "Apellido obligatorio";
            }
            SqlConnection sqlConnection =new SqlConnection(cadenaConexion);

            sqlConnection.Open();

            SqlCommand sqlCommand =
                new SqlCommand(
                "usp_agrega_consumidor",
                sqlConnection);

            sqlCommand.CommandType =
                System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue(
                "@dnicont", consumidor.dnicont);

            sqlCommand.Parameters.AddWithValue(
                "@nomcont", consumidor.nomcont);

            sqlCommand.Parameters.AddWithValue(
                "@apecont", consumidor.apecont);

            sqlCommand.Parameters.AddWithValue(
                "@emailcont", consumidor.emailcont);

            sqlCommand.Parameters.AddWithValue(
                "@idpais", consumidor.idpais);

            SqlParameter mensaje =
                new SqlParameter(
                "@mensaje",
                System.Data.SqlDbType.VarChar,
                100);

            mensaje.Direction =
                System.Data.ParameterDirection.Output;

            sqlCommand.Parameters.Add(mensaje);

            sqlCommand.ExecuteNonQuery();

            string respuesta =
                mensaje.Value.ToString();

            sqlConnection.Close();

            return respuesta;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Consumidor consumidor)
        {
            string mensaje =
                guardarConsumidor(consumidor);

            ViewBag.mensaje = mensaje;

            return View(consumidor);
        }

        Consumidor recuperarConsumidorPorId(string id)
        {
            Consumidor consumidor = new Consumidor();

            SqlConnection sqlConnection =
                new SqlConnection(cadenaConexion);

            sqlConnection.Open();

            SqlCommand sqlCommand =
                new SqlCommand(
                "select * from tb_consumidor where dnicont=@dnicont",
                sqlConnection);

            sqlCommand.Parameters.AddWithValue(
                "@dnicont", id);

            SqlDataReader sqlDataReader =
                sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                consumidor = new Consumidor(
                    sqlDataReader.GetString(0),
                    sqlDataReader.GetString(1),
                    sqlDataReader.GetString(2),
                    sqlDataReader.GetString(3),
                    sqlDataReader.GetString(4)
                );
            }

            sqlDataReader.Close();
            sqlConnection.Close();

            return consumidor;
        }
        string eliminarConsumidorPorId(string dnicont)
        {
            SqlConnection sqlConnection =
                new SqlConnection(cadenaConexion);

            sqlConnection.Open();

            SqlCommand sqlCommand =
                new SqlCommand(
                "usp_eliminar_consumidor",
                sqlConnection);

            sqlCommand.CommandType =
                System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue(
                "@dnicont", dnicont);

            int cantidad =
                sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return cantidad > 0 ?
                   "Registro eliminado" :
                   "Error al eliminar";
        }
        public ActionResult Delete(string id)
        {
            Consumidor consumidorExistente =
                recuperarConsumidorPorId(id);

            return View(consumidorExistente);
        }

        [HttpPost]
        public ActionResult Delete(Consumidor consumidor)
        {
            string mensaje =
                eliminarConsumidorPorId(consumidor.dnicont);

            TempData["mensaje"] = mensaje;

            return RedirectToAction("Index");
        }

        string actualizarConsumidor(Consumidor consumidor)
        {
            SqlConnection sqlConnection =
                new SqlConnection(cadenaConexion);

            sqlConnection.Open();

            SqlCommand sqlCommand =
                new SqlCommand(
                "update tb_consumidor " +
                "set nomcont=@nomcont, " +
                "apecont=@apecont, " +
                "emailcont=@emailcont, " +
                "idpais=@idpais " +
                "where dnicont=@dnicont",
                sqlConnection);

            sqlCommand.Parameters.AddWithValue(
                "@dnicont", consumidor.dnicont);

            sqlCommand.Parameters.AddWithValue(
                "@nomcont", consumidor.nomcont);

            sqlCommand.Parameters.AddWithValue(
                "@apecont", consumidor.apecont);

            sqlCommand.Parameters.AddWithValue(
                "@emailcont", consumidor.emailcont);

            sqlCommand.Parameters.AddWithValue(
                "@idpais", consumidor.idpais);

            int cantidad =
                sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return cantidad > 0 ?
                "Registro actualizado" :
                "Error al actualizar";
        }
        public ActionResult Edit(string id)
        {
            Consumidor consumidorExistente =recuperarConsumidorPorId(id);

            return View(consumidorExistente);
        }

        [HttpPost]
        public ActionResult Edit(Consumidor consumidor)
        {
            string mensaje = actualizarConsumidor(consumidor);
           ViewBag.mensaje = mensaje;

            return View(consumidor);
        }
        public ActionResult Details(string id)
        {
            Consumidor consumidorExistente = recuperarConsumidorPorId(id);

            return View(consumidorExistente);
        }



    }

}
