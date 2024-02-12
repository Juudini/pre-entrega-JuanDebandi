using SistemaGestion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness
{
    public class ProductoBussiness
    {
        public static List<Producto> ListarProductos()
        {
            try
            {
                List<Producto> listadoDeProductos = new List<Producto>();
                using (SqlConnection connection = new SqlConnection(ProductoData.connectionString))
                {
                    string query = "SELECT * FROM PRODUCTO;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.id = Convert.ToInt32(reader["Id"]);
                        producto.descripcion = reader["Descripciones"].ToString();
                        producto.costo = Convert.ToInt32(reader["Costo"]);
                        producto.precioVenta = Convert.ToInt32(reader["PrecioVenta"]);
                        producto.stock = Convert.ToInt32(reader["Stock"]);
                        producto.idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        listadoDeProductos.Add(producto);
                    }
                }
                return listadoDeProductos;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener productos: {ex.Message}");
                return null;
            }
        }

        public static Producto ObtenerProductoPorID(int idProducto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ProductoData.connectionString))
                {
                    string query = "SELECT * FROM PRODUCTO WHERE Id = @IdProducto;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@IdProducto", idProducto);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.id = Convert.ToInt32(reader["Id"]);
                        producto.descripcion = reader["Descripciones"].ToString();
                        producto.costo = Convert.ToInt32(reader["Costo"]);
                        producto.precioVenta = Convert.ToInt32(reader["PrecioVenta"]);
                        producto.stock = Convert.ToInt32(reader["Stock"]);
                        producto.idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        return producto;
                    }

                    return null; // No se encontró un producto con ese ID.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener producto por ID: {ex.Message}");
                return null;
            }
        }

        public static bool CrearProducto(Producto nuevoProducto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ProductoData.connectionString))
                {
                    string query = "INSERT INTO PRODUCTO (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " +
                                   "VALUES (@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario);";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Descripciones", nuevoProducto.descripcion);
                    sqlCommand.Parameters.AddWithValue("@Costo", nuevoProducto.costo);
                    sqlCommand.Parameters.AddWithValue("@PrecioVenta", nuevoProducto.precioVenta);
                    sqlCommand.Parameters.AddWithValue("@Stock", nuevoProducto.stock);
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", nuevoProducto.idUsuario);
                    connection.Open();

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear nuevo producto: {ex.Message}");
                return false;
            }
        }

        public static bool ModificarProducto(Producto productoModificado)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ProductoData.connectionString))
                {
                    string query = "UPDATE PRODUCTO " +
                                   "SET Descripciones = @Descripciones, " +
                                   "    Costo = @Costo, " +
                                   "    PrecioVenta = @PrecioVenta, " +
                                   "    Stock = @Stock, " +
                                   "    IdUsuario = @IdUsuario " +
                                   "WHERE Id = @IdProducto;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Descripciones", productoModificado.descripcion);
                    sqlCommand.Parameters.AddWithValue("@Costo", productoModificado.costo);
                    sqlCommand.Parameters.AddWithValue("@PrecioVenta", productoModificado.precioVenta);
                    sqlCommand.Parameters.AddWithValue("@Stock", productoModificado.stock);
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", productoModificado.idUsuario);
                    sqlCommand.Parameters.AddWithValue("@IdProducto", productoModificado.id);
                    connection.Open();

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar producto: {ex.Message}");
                return false;
            }
        }

        public static bool EliminarProducto(int idProducto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ProductoData.connectionString))
                {
                    string query = "DELETE FROM PRODUCTO WHERE Id = @IdProducto;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@IdProducto", idProducto);
                    connection.Open();

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar producto: {ex.Message}");
                return false;
            }
        }
    }
}

