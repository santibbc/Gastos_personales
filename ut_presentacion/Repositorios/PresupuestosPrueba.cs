using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class PresupuestosPrueba
    {
        private readonly IConexion? iConexion;
        private List<Presupuestos>? lista;
        private Presupuestos? entidad;

        public PresupuestosPrueba()
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
            this.lista = this.iConexion!.Presupuestos!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Presupuesto();
            this.iConexion!.Presupuestos!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdPresupuesto > 0;
        }

        public bool Modificar()
        {
            this.entidad!.MontoAsignado += 100000;
            var entry = this.iConexion!.Entry<Presupuestos>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Presupuestos!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

