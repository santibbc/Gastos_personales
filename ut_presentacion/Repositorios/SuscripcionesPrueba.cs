using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class SuscripcionesPrueba
    {
        private readonly IConexion? iConexion;
        private List<Suscripciones>? lista;
        private Suscripciones? entidad;

        public SuscripcionesPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Suscripciones!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Suscripcion();
            this.iConexion!.Suscripciones!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdSuscripcion > 0;
        }

        public bool Modificar()
        {
            this.entidad!.MontoMensual += 5000;
            var entry = this.iConexion!.Entry<Suscripciones>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Suscripciones!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

