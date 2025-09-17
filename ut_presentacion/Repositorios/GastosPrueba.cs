using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class GastosPrueba
    {
        private readonly IConexion? iConexion;
        private List<Gastos>? lista;
        private Gastos? entidad;

        public GastosPrueba()
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
            this.lista = this.iConexion!.Gastos!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Gasto();
            this.iConexion!.Gastos!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdGasto > 0;
        }

        public bool Modificar()
        {
            this.entidad!.Descripcion = "Gasto modificado";
            var entry = this.iConexion!.Entry<Gastos>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Gastos!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

