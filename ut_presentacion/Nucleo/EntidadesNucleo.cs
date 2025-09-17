using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class EntidadesNucleo
    {
        public static Usuarios Usuario()
        {
            return new Usuarios
            {
                Nombre = "Usuario" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                Apellido = "Prueba",
                Email = "usuario" + DateTime.Now.ToString("HHmmss") + "@mail.com",
                Contrasena = "12345",
                FechaRegistro = DateTime.Now,
                Estado = true
            };
        }

        public static CuentasBancarias Cuenta()
        {
            return new CuentasBancarias
            {
                IdUsuario = 1,
                Banco = "BancoPrueba",
                NumeroCuenta = "123456" + DateTime.Now.ToString("ss"),
                TipoCuenta = "Ahorros",
                SaldoInicial = 1000000,
                Moneda = "COP"
            };
        }

        public static CategoriasGasto Categoria()
        {
            return new CategoriasGasto
            {
                Nombre = "Alimentación " + DateTime.Now.ToString("HHmmss"),
                Descripcion = "Gastos en comida"
            };
        }

        public static Gastos Gasto()
        {
            return new Gastos
            {
                IdUsuario = 1,
                IdCategoria = 1,
                IdCuenta = 1,
                Monto = 45000,
                Descripcion = "Compra prueba " + DateTime.Now.ToString("HHmmss"),
                Fecha = DateTime.Now
            };
        }

        public static Ingresos Ingreso()
        {
            return new Ingresos
            {
                IdUsuario = 1,
                IdCuenta = 1,
                Monto = 2000000,
                Fuente = "Salario",
                Fecha = DateTime.Now
            };
        }

        public static Presupuestos Presupuesto()
        {
            return new Presupuestos
            {
                IdUsuario = 1,
                IdCategoria = 1,
                MontoAsignado = 1000000,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddMonths(1)
            };
        }

        public static MetasAhorro MetaAhorro()
        {
            return new MetasAhorro
            {
                IdUsuario = 1,
                Nombre = "Viaje " + DateTime.Now.ToString("yyyy"),
                MontoObjetivo = 5000000,
                FechaLimite = DateTime.Now.AddMonths(12),
                Estado = "En Progreso"
            };
        }

        public static TarjetasCredito Tarjeta()
        {
            return new TarjetasCredito
            {
                IdUsuario = 1,
                Banco = "Bancolombia",
                NumeroTarjeta = "111122223333" + DateTime.Now.ToString("ss"),
                Cupo = 3000000,
                FechaCorte = DateTime.Now.AddDays(30),
                FechaPago = DateTime.Now.AddDays(40)
            };
        }

        public static MovimientosTarjeta MovimientoTarjeta()
        {
            return new MovimientosTarjeta
            {
                IdTarjeta = 1,
                Descripcion = "Compra prueba",
                Monto = 150000,
                Fecha = DateTime.Now
            };
        }

        public static Deudas Deuda()
        {
            return new Deudas
            {
                IdUsuario = 1,
                Acreedor = "Banco de Prueba",
                Monto = 500000,
                FechaInicio = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddMonths(6),
                Estado = "Activa"
            };
        }

        public static PagosDeuda PagoDeuda()
        {
            return new PagosDeuda
            {
                IdDeuda = 1,
                Monto = 200000,
                Fecha = DateTime.Now,
                MetodoPago = "Transferencia"
            };
        }

        public static ConfiguracionUsuario ConfiguracionUsuario()
        {
            return new ConfiguracionUsuario
            {
                IdUsuario = 1,
                MonedaPreferida = "COP",
                Notificaciones = true,
                LimiteAlertas = 500000
            };
        }

        public static ProveedoresServicios ProveedorServicio()
        {
            return new ProveedoresServicios
            {
                Nombre = "Netflix",
                Categoria = "Entretenimiento",
                Contacto = "soporte@netflix.com"
            };
        }

        public static Suscripciones Suscripcion()
        {
            return new Suscripciones
            {
                IdUsuario = 1,
                IdProveedor = 1,
                MontoMensual = 35000,
                FechaInicio = DateTime.Now,
                FechaRenovacion = DateTime.Now.AddMonths(1)
            };
        }

        public static Auditoria Auditoria()
        {
            return new Auditoria
            {
                IdUsuario = 1,
                EntidadAfectada = "Usuarios",
                Operacion = "Insert",
                Fecha = DateTime.Now,
                Detalle = "Se creó un usuario de prueba"
            };
        }
    }
}


