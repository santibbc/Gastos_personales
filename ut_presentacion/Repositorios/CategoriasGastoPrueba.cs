using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class CategoriasGastoPrueba
    {
        private readonly IConexion? iConexion;
        private List<CategoriasGasto>? lista;
        private CategoriasGasto? entidad;

        public CategoriasGastoPrueba()
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
            this.lista = this.iConexion!.CategoriasGasto!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Categoria();
            this.iConexion!.CategoriasGasto!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdCategoria > 0;
        }

        public bool Modificar()
        {
            this.entidad!.Descripcion = "Modificada";
            var entry = this.iConexion!.Entry<CategoriasGasto>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.CategoriasGasto!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

