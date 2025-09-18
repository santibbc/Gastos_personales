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
    public class MetasAhorroPruebaImp
    {
        private readonly IConexion? iConexion;
        private MetasAhorro? entidad;
        private MetasAhorroAplicacion? app;

        public MetasAhorroPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new MetasAhorroAplicacion(iConexion);
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
            entidad = new MetasAhorro
            {
                IdUsuario = 1, 
                Nombre = "Meta de prueba - Viaje",
                MontoObjetivo = 1500000m,
                FechaLimite = DateTime.Now.AddMonths(6), 
                Estado = "Activa"
            };

            app!.Guardar(entidad);
            return entidad.IdMeta > 0;
        }

        public bool Modificar()
        {
            entidad!.MontoObjetivo = 2000000m; 
            entidad.FechaLimite = DateTime.Now.AddMonths(8); 
            entidad.Nombre = "Meta de prueba modificada";

            app!.Modificar(entidad);

            var meta = iConexion!.MetasAhorro!.AsNoTracking()
                .FirstOrDefault(m => m.IdMeta == entidad.IdMeta);

            return meta != null && meta.Nombre == entidad.Nombre && meta.MontoObjetivo == 2000000m;
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            bool existe = lista.Any(m => m.IdMeta == entidad!.IdMeta);
            return lista.Count > 0 && existe;
        }

        public bool Borrar()
        {
            
            entidad!.Estado = "Finalizada";

            var entry = iConexion!.Entry<MetasAhorro>(entidad);
            entry.State = EntityState.Modified;
            iConexion.SaveChanges();

            app!.Borrar(entidad);

            var meta = iConexion!.MetasAhorro!.AsNoTracking()
                .FirstOrDefault(m => m.IdMeta == entidad.IdMeta);

            return meta == null;
        }
    }
}

