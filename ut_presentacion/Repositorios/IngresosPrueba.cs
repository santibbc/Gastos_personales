using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class IngresosPrueba
    {
        private readonly IConexion? iConexion;
        private List<Ingresos>? lista;
        private Ingresos? entidad;

        public IngresosPrueba()
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
            this.lista = this.iConexion!.Ingresos!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Ingreso();
            this.iConexion!.Ingresos!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdIngreso > 0;
        }

        public bool Modificar()
        {
            this.entidad!.Fuente = "Fuente Modificada";
            var entry = this.iConexion!.Entry<Ingresos>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Ingresos!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
