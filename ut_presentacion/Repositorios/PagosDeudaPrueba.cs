using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class PagosDeudaPrueba
    {
        private readonly IConexion? iConexion;
        private List<PagosDeuda>? lista;
        private PagosDeuda? entidad;

        public PagosDeudaPrueba()
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
            this.lista = this.iConexion!.PagosDeuda!.ToList();
            return lista.Count >= 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.PagoDeuda();
            this.iConexion!.PagosDeuda!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.IdPago > 0;
        }

        public bool Modificar()
        {
            this.entidad!.MetodoPago = "Efectivo";
            var entry = this.iConexion!.Entry<PagosDeuda>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.PagosDeuda!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

