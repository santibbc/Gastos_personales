using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ConfiguracionUsuarioPrueba
    {
        private readonly IConexion? iConexion;
        private List<ConfiguracionUsuario>? lista;
        private ConfiguracionUsuario? entidad;

        public ConfiguracionUsuarioPrueba()
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
            this.lista = this.iConexion!.ConfiguracionUsuario!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.ConfiguracionUsuario();
            this.iConexion!.ConfiguracionUsuario!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdConfig > 0;
        }

        public bool Modificar()
        {
            this.entidad!.Notificaciones = false;
            var entry = this.iConexion!.Entry<ConfiguracionUsuario>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.ConfiguracionUsuario!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

