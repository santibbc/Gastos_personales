using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class ProveedoresServiciosPruebaImp
    {
        private readonly IConexion? iConexion;
        private ProveedoresServicios? entidad;
        private ProveedoresServiciosAplicacion? app;

        public ProveedoresServiciosPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new ProveedoresServiciosAplicacion(iConexion);
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
            entidad = new ProveedoresServicios
            {
                Nombre = "Crunchyroll",
                Categoria = "Streaming",
                Contacto = "soporte@crunchyroll.com"
            };

            app!.Guardar(entidad);
            return entidad.IdProveedor > 0;
        }

        public bool Modificar()
        {
            entidad!.Nombre = "Proveedor de streaming - Modificado";
            entidad!.Categoria = "Streaming";
            entidad!.Contacto = "soporte@crunchyroll.com";

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
          
            entidad!.Nombre = "Proveedor Eliminable";
            var entry = iConexion!.Entry<ProveedoresServicios>(entidad);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            iConexion.SaveChanges();

            app!.Borrar(entidad);
            return true;
        }
    }
}

