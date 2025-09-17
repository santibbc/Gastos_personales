using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class MetasAhorroPrueba
    {
        private readonly IConexion? iConexion;
        private List<MetasAhorro>? lista;
        private MetasAhorro? entidad;

        public MetasAhorroPrueba()
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
            this.lista = this.iConexion!.MetasAhorro!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.MetaAhorro();
            this.iConexion!.MetasAhorro!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdMeta > 0;
        }

        public bool Modificar()
        {
            this.entidad!.Estado = "Completada";
            var entry = this.iConexion!.Entry<MetasAhorro>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.MetasAhorro!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

