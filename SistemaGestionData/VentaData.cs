using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion
{
    public static class VentaData
    {
        private static string connectionString;

        static VentaData()
        {
            VentaData.connectionString = @"Server=.; Database=coderhouse; Trusted_Connection=True;";
        }

        public static List<Venta> ListarVentas()
        {
            try
            {
                List<Venta> listadoDeVentas = new List<Venta>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM VENTA;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Venta venta = new Venta();
                        venta.id = Convert.ToInt32(reader["Id"]);
                        venta.comentarios = reader["Comentarios"].ToString();
                        venta.idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        listadoDeVentas.Add(venta);
                    }
                }
                return listadoDeVentas;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener listado de ventas: {ex.Message}");
                return null;
            }
        }

        public static Venta ObtenerVentaPorID(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM VENTA WHERE Id = @Id;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        Venta venta = new Venta();
                        venta.id = Convert.ToInt32(reader["Id"]);
                        venta.comentarios = reader["Comentarios"].ToString();
                        venta.idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        return venta;
                    }
                    else
                    {
                        Console.WriteLine("Venta no encontrada.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener venta por ID: {ex.Message}");
                return null;
            }
        }

        public static void CrearVenta(Venta nuevaVenta)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO VENTA (Comentarios, IdUsuario) VALUES (@Comentarios, @IdUsuario);";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Comentarios", nuevaVenta.comentarios);
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", nuevaVenta.idUsuario);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }

                Console.WriteLine("Venta creada con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear venta: {ex.Message}");
            }
        }

        public static void ModificarVenta(Venta ventaModificada)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE VENTA SET Comentarios = @Comentarios, IdUsuario = @IdUsuario WHERE Id = @Id;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Comentarios", ventaModificada.comentarios);
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", ventaModificada.idUsuario);
                    sqlCommand.Parameters.AddWithValue("@Id", ventaModificada.id);

                    connection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Venta modificada con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró la venta para modificar.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar venta: {ex.Message}");
            }
        }

        public static void EliminarVenta(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM VENTA WHERE Id = @Id;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Venta eliminada con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró la venta para eliminar.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar venta: {ex.Message}");
            }
        }
    }
}
