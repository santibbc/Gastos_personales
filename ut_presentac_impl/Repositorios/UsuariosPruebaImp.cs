using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class UsuariosPruebaImp
    {
        private readonly IConexion? iConexion;
        private Usuarios? entidad;
        private UsuariosAplicacion? app;

        public UsuariosPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new UsuariosAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Guardar()
        {
            entidad = new Usuarios
            {
                Nombre = "Juan",
                Apellido = "Pérez",
              //esto se usa para generar un correo unico en cada prueba
                Email = $"juan_{Guid.NewGuid()}@test.com",
                Contrasena = "123456",
                FechaRegistro = DateTime.Now,
                Estado = true
            };

            app!.Guardar(entidad);
            return entidad.IdUsuario > 0;
        }

        public bool Modificar()
        {
            entidad!.Nombre = "Juan Modificado";
            app!.Modificar(entidad);
            return true;
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool Borrar()
        {
        
            entidad!.Estado = false;
            app!.Modificar(entidad);

            app!.Borrar(entidad);
            return true;
        }
    }
}


