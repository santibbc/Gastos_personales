using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;
using System;
using System.Linq;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class GastosPruebaImp
    {
        private readonly IConexion? iConexion;
        private Gastos? entidad;
        private GastosAplicacion? app;

        public GastosPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new GastosAplicacion(iConexion);
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
            entidad = new Gastos
            {
                IdUsuario = 1,      // debe existir en tu BD
                IdCategoria = 1,    // debe existir en tu BD
                IdCuenta = 1,       // debe existir en tu BD
                Monto = 120000m,
                Descripcion = "Compra supermercado",
                Fecha = DateTime.Now.AddDays(-5)
            };

            app!.Guardar(entidad);
            return entidad.IdGasto > 0;
        }

        public bool Modificar()
        {
            entidad!.Monto = 150000m;
            entidad.Descripcion = "Compra supermercado (ajustada)";

            app!.Modificar(entidad);

            var gasto = iConexion!.Gastos!.AsNoTracking()
                .FirstOrDefault(g => g.IdGasto == entidad.IdGasto);

            return gasto != null && gasto.Monto == 150000m;
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            bool existe = lista.Any(g => g.IdGasto == entidad!.IdGasto);
            return lista.Count > 0 && existe;
        }

        public bool Borrar()
        {
            app!.Borrar(entidad);

            var gasto = iConexion!.Gastos!.AsNoTracking()
                .FirstOrDefault(g => g.IdGasto == entidad!.IdGasto);

            return gasto == null;
        }
    }
}

