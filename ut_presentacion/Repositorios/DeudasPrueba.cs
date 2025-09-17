using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class DeudasPrueba
    {
        private readonly IConexion? iConexion;
        private List<Deudas>? lista;
        private Deudas? entidad;

        public DeudasPrueba()
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
            this.lista = this.iConexion!.Deudas!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Deuda();
            this.iConexion!.Deudas!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdDeuda > 0;
        }

        public bool Modificar()
        {
            this.entidad!.Estado = "Pagada";
            var entry = this.iConexion!.Entry<Deudas>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Deudas!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
