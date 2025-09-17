using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class AuditoriaPrueba
    {
        private readonly IConexion? iConexion;
        private List<Auditoria>? lista;
        private Auditoria? entidad;

        public AuditoriaPrueba()
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
            this.lista = this.iConexion!.Auditoria!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Auditoria();
            this.iConexion!.Auditoria!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdAuditoria > 0;
        }

        public bool Modificar()
        {
            this.entidad!.Detalle = "Detalle Modificado";
            var entry = this.iConexion!.Entry<Auditoria>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Auditoria!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

