using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class CuentasBancariasPrueba
    {
        private readonly IConexion? iConexion;
        private List<CuentasBancarias>? lista;
        private CuentasBancarias? entidad;

        public CuentasBancariasPrueba()
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
            this.lista = this.iConexion!.CuentasBancarias!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Cuenta();
            this.iConexion!.CuentasBancarias!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdCuenta > 0;
        }

        public bool Modificar()
        {
            this.entidad!.Banco = "BancoModificado";
            var entry = this.iConexion!.Entry<CuentasBancarias>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.CuentasBancarias!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

