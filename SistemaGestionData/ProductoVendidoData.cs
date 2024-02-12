using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion
{
    public static class ProductoVendidoData
    {
        private static string connectionString;

        static ProductoVendidoData()
        {
            ProductoVendidoData.connectionString = @"Server=.; Database=coderhouse; Trusted_Connection=True;";
        }

        public static List<ProductoVendido> ListarProductosVendidos()
        {
            try
            {
                List<ProductoVendido> listadoDeProductosVendidos = new List<ProductoVendido>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM PRODUCTOVENDIDO;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductoVendido productoVendido = new ProductoVendido();
                        productoVendido.id = Convert.ToInt32(reader["Id"]);
                        productoVendido.stock = Convert.ToInt32(reader["Stock"]);
                        productoVendido.idProducto = Convert.ToInt32(reader["IdProducto"]);
                        productoVendido.idVenta = Convert.ToInt32(reader["IdVenta"]);
                        listadoDeProductosVendidos.Add(productoVendido);
                    }
                }
                return listadoDeProductosVendidos;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener productos vendidos: {ex.Message}");
                return null;
            }
        }

        public static ProductoVendido ObtenerProductoVendidoPorID(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM PRODUCTOVENDIDO WHERE Id = @Id;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        ProductoVendido productoVendido = new ProductoVendido();
                        productoVendido.id = Convert.ToInt32(reader["Id"]);
                        productoVendido.stock = Convert.ToInt32(reader["Stock"]);
                        productoVendido.idProducto = Convert.ToInt32(reader["IdProducto"]);
                        productoVendido.idVenta = Convert.ToInt32(reader["IdVenta"]);
                        return productoVendido;
                    }
                    else
                    {
                        Console.WriteLine("Producto vendido no encontrado.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener producto vendido por ID: {ex.Message}");
                return null;
            }
        }

        public static void CrearProductoVendido(ProductoVendido nuevoProductoVendido)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO PRODUCTOVENDIDO (Stock, IdProducto, IdVenta) VALUES (@Stock, @IdProducto, @IdVenta);";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Stock", nuevoProductoVendido.stock);
                    sqlCommand.Parameters.AddWithValue("@IdProducto", nuevoProductoVendido.idProducto);
                    sqlCommand.Parameters.AddWithValue("@IdVenta", nuevoProductoVendido.idVenta);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }

                Console.WriteLine("Producto vendido creado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear producto vendido: {ex.Message}");
            }
        }

        public static void ModificarProductoVendido(ProductoVendido productoModificado)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE PRODUCTOVENDIDO SET Stock = @Stock, IdProducto = @IdProducto, IdVenta = @IdVenta WHERE Id = @Id;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Stock", productoModificado.stock);
                    sqlCommand.Parameters.AddWithValue("@IdProducto", productoModificado.idProducto);
                    sqlCommand.Parameters.AddWithValue("@IdVenta", productoModificado.idVenta);
                    sqlCommand.Parameters.AddWithValue("@Id", productoModificado.id);

                    connection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Producto vendido modificado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el producto vendido para modificar.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar producto vendido: {ex.Message}");
            }
        }

        public static void EliminarProductoVendido(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM PRODUCTOVENDIDO WHERE Id = @Id;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Producto vendido eliminado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el producto vendido para eliminar.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar producto vendido: {ex.Message}");
            }
        }
    }
}
