using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class PagosDeudaPruebaImp
    {
        private readonly IConexion? iConexion;
        private PagosDeuda? entidad;
        private PagosDeudaAplicacion? app;

        public PagosDeudaPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new PagosDeudaAplicacion(iConexion);
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
            entidad = new PagosDeuda
            {
                IdDeuda = 1, 
                Monto = 200000,
                MetodoPago = "Transferencia",
                Fecha = DateTime.Now.AddDays(-2) 
            };

            app!.Guardar(entidad);
            return entidad.IdPago > 0;
        }

        public bool Modificar()
        {
           
            entidad!.Monto = 250000;
            entidad.MetodoPago = "Efectivo";
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
          
            entidad!.Fecha = DateTime.Now.AddMonths(-2);
            app!.Modificar(entidad);

            app!.Borrar(entidad);
            return true;
        }
    }
}
