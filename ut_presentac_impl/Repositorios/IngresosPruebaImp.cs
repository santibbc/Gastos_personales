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
    public class IngresosPruebaImp
    {
        private readonly IConexion? iConexion;
        private Ingresos? entidad;
        private IngresosAplicacion? app;

        public IngresosPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new IngresosAplicacion(iConexion);
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
            entidad = new Ingresos
            {
                IdUsuario = 1, 
                IdCuenta = 1,  
                Monto = 2500000m,
                Fuente = "Salario",
                Fecha = DateTime.Now.AddMonths(-1) 
            };

            app!.Guardar(entidad);
            return entidad.IdIngreso > 0;
        }

        public bool Modificar()
        {
            entidad!.Monto = 3000000m; 
            entidad.Fecha = DateTime.Now.AddMonths(-1); 
            

            app!.Modificar(entidad);

            var ingreso = iConexion!.Ingresos!.AsNoTracking()
                .FirstOrDefault(i => i.IdIngreso == entidad.IdIngreso);

            return ingreso != null && ingreso.Monto == 3000000m;
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            bool existe = lista.Any(i => i.IdIngreso == entidad!.IdIngreso);
            return lista.Count > 0 && existe;
        }

        public bool Borrar()
        {
           
            entidad!.Fecha = DateTime.Now.AddMonths(-2);

            var entry = iConexion!.Entry<Ingresos>(entidad);
            entry.State = EntityState.Modified;
            iConexion.SaveChanges();

            app!.Borrar(entidad);

            var ingreso = iConexion!.Ingresos!.AsNoTracking()
                .FirstOrDefault(i => i.IdIngreso == entidad.IdIngreso);

            return ingreso == null;
        }
    }
}

