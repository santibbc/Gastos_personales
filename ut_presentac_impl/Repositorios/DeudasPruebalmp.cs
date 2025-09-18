using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class DeudasPruebaImp
    {
        private readonly IConexion? iConexion;
        private Deudas? entidad;
        private DeudasAplicacion? app;

        public DeudasPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new DeudasAplicacion(iConexion);
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
            entidad = new Deudas
            {
                IdUsuario = 1,
                Acreedor = "Acreedor Prueba",
                Monto = 222.0m,
                FechaInicio = DateTime.Now,
                FechaVencimiento = DateTime.Now,
                Estado = "Estado Prueba"
            };

            app!.Guardar(entidad);
            return entidad.IdUsuario > 0;
        }

        public bool Modificar()
        {
            entidad!.Acreedor = "Acreedor Modificado";
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

            entidad!.Estado = "Estado prueba 1";
            app!.Modificar(entidad);

            app!.Borrar(entidad);
            return true;
        }
    }
}


