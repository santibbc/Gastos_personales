using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class TarjetasCreditoPruebaImp
    {
        private readonly IConexion? iConexion;
        private TarjetasCredito? entidad;
        private TarjetasCreditoAplicacion? app;

        public TarjetasCreditoPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new TarjetasCreditoAplicacion(iConexion);
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
            entidad = new TarjetasCredito
            {
                IdUsuario = 1, //aqui podrisamo decir que existe el usuario
                Banco = "Banco Prueba",
                NumeroTarjeta = $"41111111{new Random().Next(1000, 9999)}",
                Cupo = 10000000, // es mayor que 0 entonces se cumple
                FechaCorte = DateTime.Today.AddDays(5),
                FechaPago = DateTime.Today.AddDays(10)
            };

            app!.Guardar(entidad);
            return entidad.IdTarjeta > 0;
        }

        public bool Modificar()
        {
            entidad!.Banco = "Banco Modificado";
            entidad!.Cupo = 500000; 
            entidad!.FechaCorte = DateTime.Today.AddDays(3);
            entidad!.FechaPago = DateTime.Today.AddDays(8);

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
            // se deja el cupo ene 0
            entidad!.Cupo = 0;
            var entry = iConexion!.Entry<TarjetasCredito>(entidad);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            iConexion.SaveChanges();

            app!.Borrar(entidad);
            return true;
        }
    }
}

