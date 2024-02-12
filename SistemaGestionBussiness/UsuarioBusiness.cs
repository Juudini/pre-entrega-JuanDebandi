using SistemaGestion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness
{
    public class UsuarioBusiness
    {
        public static List<Usuario> ListarUsuarios()
        {
            try
            {
                List<Usuario> listadoDeUsuarios = new List<Usuario>();
                using (SqlConnection connection = new SqlConnection(UsuarioData.connectionString))
                {
                    string query = "SELECT * FROM USUARIO;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.id = Convert.ToInt32(reader["Id"]);
                        usuario.nombre = reader["Nombre"].ToString();
                        usuario.apellido = reader["Apellido"].ToString();
                        usuario.nombreusuario = reader["NombreUsuario"].ToString();
                        usuario.contraseña = reader["Contraseña"].ToString();
                        usuario.mail = reader["Mail"].ToString();
                        listadoDeUsuarios.Add(usuario);
                    }
                }
                return listadoDeUsuarios;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuarios: {ex.Message}");
                return null;
            }
        }

        public static Usuario ObtenerUsuarioPorID(int idUsuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(UsuarioData.connectionString))
                {
                    string query = "SELECT * FROM USUARIO WHERE Id = @IdUsuario;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.id = Convert.ToInt32(reader["Id"]);
                        usuario.nombre = reader["Nombre"].ToString();
                        usuario.apellido = reader["Apellido"].ToString();
                        usuario.nombreusuario = reader["NombreUsuario"].ToString();
                        usuario.contraseña = reader["Contraseña"].ToString();
                        usuario.mail = reader["Mail"].ToString();
                        return usuario;
                    }

                    return null; // No se encontró un usuario con ese ID.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuario por ID: {ex.Message}");
                return null;
            }
        }

        public static bool CrearUsuario(Usuario nuevoUsuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(UsuarioData.connectionString))
                {
                    string query = "INSERT INTO USUARIO (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                                   "VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail);";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Nombre", nuevoUsuario.nombre);
                    sqlCommand.Parameters.AddWithValue("@Apellido", nuevoUsuario.apellido);
                    sqlCommand.Parameters.AddWithValue("@NombreUsuario", nuevoUsuario.nombreusuario);
                    sqlCommand.Parameters.AddWithValue("@Contraseña", nuevoUsuario.contraseña);
                    sqlCommand.Parameters.AddWithValue("@Mail", nuevoUsuario.mail);
                    connection.Open();

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear nuevo usuario: {ex.Message}");
                return false;
            }
        }

        public static bool ModificarUsuario(Usuario usuarioModificado)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(UsuarioData.connectionString))
                {
                    string query = "UPDATE USUARIO " +
                                   "SET Nombre = @Nombre, " +
                                   "    Apellido = @Apellido, " +
                                   "    NombreUsuario = @NombreUsuario, " +
                                   "    Contraseña = @Contraseña, " +
                                   "    Mail = @Mail " +
                                   "WHERE Id = @IdUsuario;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@Nombre", usuarioModificado.nombre);
                    sqlCommand.Parameters.AddWithValue("@Apellido", usuarioModificado.apellido);
                    sqlCommand.Parameters.AddWithValue("@NombreUsuario", usuarioModificado.nombreusuario);
                    sqlCommand.Parameters.AddWithValue("@Contraseña", usuarioModificado.contraseña);
                    sqlCommand.Parameters.AddWithValue("@Mail", usuarioModificado.mail);
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", usuarioModificado.id);
                    connection.Open();

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar usuario: {ex.Message}");
                return false;
            }
        }

        public static bool EliminarUsuario(int idUsuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(UsuarioData.connectionString))
                {
                    string query = "DELETE FROM USUARIO WHERE Id = @IdUsuario;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    connection.Open();

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
                return false;
            }
        }



    }
}
