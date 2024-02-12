// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Linq;
using SistemaGestion;


namespace SistemaGestion
{
    internal class Program
    {
        static void Main(string[] args)
        {   //Test de Productos y Usuarios


            Console.WriteLine("----- Listar Productos -----");
            ListarProductos();

            Console.WriteLine("\n----- Obtener Producto por ID -----");
            int idProducto = 1;
            ObtenerProductoPorID(idProducto);

            Console.WriteLine("\n----- Crear Nuevo Producto -----");
            Producto nuevoProducto = new Producto
            {
                descripcion = "Nuevo Producto",
                costo = 10,
                precioVenta = 20,
                stock = 100,
                idUsuario = 1 
            };
            CrearProducto(nuevoProducto);

            Console.WriteLine("\n----- Modificar Producto -----");
            int idProductoModificar = 1;
            ModificarProducto(idProductoModificar);

            Console.WriteLine("\n----- Eliminar Producto -----");
            int idProductoEliminar = 2;
            EliminarProducto(idProductoEliminar);

            Console.WriteLine("\n----- Listar Productos después de las operaciones -----");
            ListarProductos();

            // Listar usuarios
            Console.WriteLine("Listado de Usuarios:");
            ListarUsuarios();

            // Obtener usuario por ID
            Console.WriteLine("\nObtener Usuario por ID (ID = 1):");
            ObtenerUsuarioPorID(1);

            // Crear usuario
            Console.WriteLine("\nCrear Nuevo Usuario:");
            CrearNuevoUsuario();

            // Modificar usuario
            Console.WriteLine("\nModificar Usuario (ID = 1):");
            ModificarUsuario();

            // Eliminar usuario
            Console.WriteLine("\nEliminar Usuario (ID = 1):");
            EliminarUsuario();

            Console.ReadLine();
        }

        static void ListarProductos()
        {
            var productos = ProductoData.ListarProductos();
            if (productos != null)
            {
                foreach (var producto in productos)
                {
                    MostrarInformacionProducto(producto);
                }
            }
            else
            {
                Console.WriteLine("No se pudieron listar los productos.");
            }
        }

        static void ObtenerProductoPorID(int id)
        {
            var producto = ProductoData.ObtenerProductoPorID(id);
            if (producto != null)
            {
                MostrarInformacionProducto(producto);
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        static void CrearProducto(Producto nuevoProducto)
        {
            if (ProductoData.CrearProducto(nuevoProducto))
            {
                Console.WriteLine("Nuevo producto creado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al crear el nuevo producto.");
            }
        }

        static void ModificarProducto(int idProductoModificar)
        {
            var productoModificado = ProductoData.ObtenerProductoPorID(idProductoModificar);
            if (productoModificado != null)
            {
                productoModificado.descripcion = "Producto Modificado";
                productoModificado.costo = 15;
                productoModificado.precioVenta = 25;
                productoModificado.stock = 50;

                if (ProductoData.ModificarProducto(productoModificado))
                {
                    Console.WriteLine("Producto modificado con éxito.");

                    // Mostrar el producto actualizado después de la modificación
                    ObtenerProductoPorID(idProductoModificar);
                }
                else
                {
                    Console.WriteLine("Error al modificar el producto.");
                }
            }
            else
            {
                Console.WriteLine("Error al obtener el producto para modificar.");
            }
        }

        static void EliminarProducto(int id)
        {
            if (ProductoData.EliminarProducto(id))
            {
                Console.WriteLine("Producto eliminado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al eliminar el producto.");
            }
        }

        static void MostrarInformacionProducto(Producto producto)
        {
            Console.WriteLine($"ID: {producto.id}, Descripción: {producto.descripcion}, " +
                              $"Costo: {producto.costo}, Precio Venta: {producto.precioVenta}, " +
                              $"Stock: {producto.stock}, ID Usuario: {producto.idUsuario}");
        }


        static void ListarUsuarios()
        {
            List<Usuario> usuarios = UsuarioData.ListarUsuarios();

            if (usuarios != null)
            {
                foreach (Usuario usuario in usuarios)
                {
                    Console.WriteLine($"ID: {usuario.id}, Nombre: {usuario.nombre}, Apellido: {usuario.apellido}, Usuario: {usuario.nombreusuario}, Mail: {usuario.mail}");
                }
            }
        }

        static void ObtenerUsuarioPorID(int idUsuario)
        {
            Usuario usuario = UsuarioData.ObtenerUsuarioPorID(idUsuario);

            if (usuario != null)
            {
                Console.WriteLine($"ID: {usuario.id}, Nombre: {usuario.nombre}, Apellido: {usuario.apellido}, Usuario: {usuario.nombreusuario}, Mail: {usuario.mail}");
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
            }
        }

        static void CrearNuevoUsuario()
        {
            Usuario nuevoUsuario = new Usuario
            {
                nombre = "Nuevo",
                apellido = "Usuario",
                nombreusuario = "nuevouser",
                contraseña = "contrasena",
                mail = "nuevo@usuario.com"
            };

            bool resultado = UsuarioData.CrearUsuario(nuevoUsuario);

            if (resultado)
            {
                Console.WriteLine("Nuevo usuario creado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al crear nuevo usuario.");
            }
        }

        static void ModificarUsuario()
        {
            Usuario usuarioModificado = new Usuario
            {
                id = 1, // ID del usuario a modificar
                nombre = "UsuarioModificado",
                apellido = "ApellidoModificado",
                nombreusuario = "usermodificado",
                contraseña = "nuevacontrasena",
                mail = "modificado@usuario.com"
            };

            bool resultado = UsuarioData.ModificarUsuario(usuarioModificado);

            if (resultado)
            {
                Console.WriteLine("Usuario modificado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al modificar usuario.");
            }
        }

        static void EliminarUsuario()
        {
            int idUsuarioAEliminar = 1; // ID del usuario a eliminar

            bool resultado = UsuarioData.EliminarUsuario(idUsuarioAEliminar);

            if (resultado)
            {
                Console.WriteLine("Usuario eliminado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al eliminar usuario.");
            }
        }

    }
}