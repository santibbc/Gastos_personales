using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ProveedoresServiciosPrueba
    {
        private readonly IConexion? iConexion;
        private List<ProveedoresServicios>? lista;
        private ProveedoresServicios? entidad;

        public ProveedoresServiciosPrueba()
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
            this.lista = this.iConexion!.ProveedoresServicios!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.ProveedorServicio();
            this.iConexion!.ProveedoresServicios!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdProveedor > 0;
        }

        public bool Modificar()
        {
            this.entidad!.Categoria = "Modificada";
            var entry = this.iConexion!.Entry<ProveedoresServicios>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.ProveedoresServicios!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

