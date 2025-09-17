CREATE DATABASE [db_gastos_personales];
GO

USE [db_gastos_personales];
GO

CREATE TABLE [Usuarios] (
    [IdUsuario] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(50) NOT NULL,
    [Apellido] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(100) UNIQUE NOT NULL,
    [Contrasena] NVARCHAR(200) NOT NULL,
    [FechaRegistro] DATETIME DEFAULT GETDATE(),
    [Estado] BIT DEFAULT 1
);
GO

CREATE TABLE [CuentasBancarias] (
    [IdCuenta] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [Banco] NVARCHAR(100) NOT NULL,
    [NumeroCuenta] NVARCHAR(30) UNIQUE NOT NULL,
    [TipoCuenta] NVARCHAR(20) NOT NULL,
    [SaldoInicial] DECIMAL(12,2) NOT NULL,
    [Moneda] NVARCHAR(10) NOT NULL
);
GO

CREATE TABLE [CategoriasGasto] (
    [IdCategoria] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(50) NOT NULL,
    [Descripcion] NVARCHAR(200)
);
GO

CREATE TABLE [Gastos] (
    [IdGasto] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [IdCategoria] INT FOREIGN KEY REFERENCES [CategoriasGasto]([IdCategoria]),
    [IdCuenta] INT FOREIGN KEY REFERENCES [CuentasBancarias]([IdCuenta]),
    [Monto] DECIMAL(12,2) NOT NULL,
    [Descripcion] NVARCHAR(200),
    [Fecha] DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Ingresos] (
    [IdIngreso] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [IdCuenta] INT FOREIGN KEY REFERENCES [CuentasBancarias]([IdCuenta]),
    [Monto] DECIMAL(12,2) NOT NULL,
    [Fuente] NVARCHAR(100),
    [Fecha] DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Presupuestos] (
    [IdPresupuesto] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [IdCategoria] INT FOREIGN KEY REFERENCES [CategoriasGasto]([IdCategoria]),
    [MontoAsignado] DECIMAL(12,2) NOT NULL,
    [FechaInicio] DATE NOT NULL,
    [FechaFin] DATE NOT NULL
);
GO

CREATE TABLE [MetasAhorro] (
    [IdMeta] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [Nombre] NVARCHAR(100) NOT NULL,
    [MontoObjetivo] DECIMAL(12,2) NOT NULL,
    [FechaLimite] DATE NOT NULL,
    [Estado] NVARCHAR(20) DEFAULT 'En progreso'
);
GO

CREATE TABLE [TarjetasCredito] (
    [IdTarjeta] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [Banco] NVARCHAR(100) NOT NULL,
    [NumeroTarjeta] NVARCHAR(30) UNIQUE NOT NULL,
    [Cupo] DECIMAL(12,2) NOT NULL,
    [FechaCorte] DATE NOT NULL,
    [FechaPago] DATE NOT NULL
);
GO

CREATE TABLE [MovimientosTarjeta] (
    [IdMovimiento] INT IDENTITY(1,1) PRIMARY KEY,
    [IdTarjeta] INT FOREIGN KEY REFERENCES [TarjetasCredito]([IdTarjeta]),
    [Descripcion] NVARCHAR(200),
    [Monto] DECIMAL(12,2) NOT NULL,
    [Fecha] DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Deudas] (
    [IdDeuda] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [Acreedor] NVARCHAR(100) NOT NULL,
    [Monto] DECIMAL(12,2) NOT NULL,
    [FechaInicio] DATE NOT NULL,
    [FechaVencimiento] DATE NOT NULL,
    [Estado] NVARCHAR(20) DEFAULT 'Pendiente'
);
GO

CREATE TABLE [PagosDeuda] (
    [IdPago] INT IDENTITY(1,1) PRIMARY KEY,
    [IdDeuda] INT FOREIGN KEY REFERENCES [Deudas]([IdDeuda]),
    [Monto] DECIMAL(12,2) NOT NULL,
    [Fecha] DATETIME DEFAULT GETDATE(),
    [MetodoPago] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE [ConfiguracionUsuario] (
    [IdConfig] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [MonedaPreferida] NVARCHAR(10) NOT NULL,
    [Notificaciones] BIT DEFAULT 1,
    [LimiteAlertas] DECIMAL(12,2) NULL
);
GO

CREATE TABLE [ProveedoresServicios] (
    [IdProveedor] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Categoria] NVARCHAR(50) NOT NULL,
    [Contacto] NVARCHAR(100)
);
GO

CREATE TABLE [Suscripciones] (
    [IdSuscripcion] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [IdProveedor] INT FOREIGN KEY REFERENCES [ProveedoresServicios]([IdProveedor]),
    [MontoMensual] DECIMAL(12,2) NOT NULL,
    [FechaInicio] DATE NOT NULL,
    [FechaRenovacion] DATE NOT NULL
);
GO

CREATE TABLE [Auditoria] (
    [IdAuditoria] INT IDENTITY(1,1) PRIMARY KEY,
    [IdUsuario] INT FOREIGN KEY REFERENCES [Usuarios]([IdUsuario]),
    [EntidadAfectada] NVARCHAR(50) NOT NULL,
    [Operacion] NVARCHAR(20) NOT NULL,
    [Fecha] DATETIME DEFAULT GETDATE(),
    [Detalle] NVARCHAR(500)
);
GO




-- datos insertados de [Usuarios]
INSERT INTO [Usuarios] ([Nombre],[Apellido],[Email],[Contrasena]) VALUES
('Carlos','Pérez','carlos.perez@email.com','claveCarlos123'),
('Laura','González','laura.gonzalez@email.com','passwordLaura456'),
('Andrés','López','andres.lopez@email.com','andresPass789'),
('María','Ramírez','maria.ramirez@email.com','mariaSecure2024'),
('Julián','Torres','julian.torres@email.com','torresKey999');
GO

-- datos insertados de [CuentasBancarias]
INSERT INTO [CuentasBancarias] ([IdUsuario],[Banco],[NumeroCuenta],[TipoCuenta],[SaldoInicial],[Moneda]) VALUES
(1,'Bancolombia','1234567890','Ahorros',1500000,'COP'),
(2,'Davivienda','2233445566','Corriente',2500000,'COP'),
(3,'BBVA','9988776655','Ahorros',800000,'USD'),
(4,'Banco de Bogotá','5566778899','Corriente',1200000,'COP'),
(5,'Banco Popular','6677889900','Ahorros',600000,'COP');
GO

-- datos insertados de [CategoriasGasto]
INSERT INTO [CategoriasGasto] ([Nombre],[Descripcion]) VALUES
('Alimentación','Compras de comida y supermercado'),
('Transporte','Gastos de movilidad: bus, gasolina, etc.'),
('Educación','Pagos de matrícula, libros, cursos'),
('Ocio','Cine, conciertos, entretenimiento'),
('Servicios','Agua, luz, internet, teléfono');
GO

-- datos insertados de [Gastos]
INSERT INTO [Gastos] ([IdUsuario],[IdCategoria],[IdCuenta],[Monto],[Descripcion]) VALUES
(1,1,1,120000,'Supermercado Éxito'),
(2,2,2,60000,'Taxi y gasolina'),
(3,3,3,450000,'Curso en línea'),
(4,4,4,80000,'Cine con amigos'),
(5,5,5,150000,'Factura de internet');
GO

-- datos insertados de [Ingresos]
INSERT INTO [Ingresos] ([IdUsuario],[IdCuenta],[Monto],[Fuente]) VALUES
(1,1,2000000,'Salario mensual'),
(2,2,1800000,'Salario mensual'),
(3,3,2500000,'Venta de servicios'),
(4,4,3000000,'Negocio propio'),
(5,5,1200000,'Beca estudiantil');
GO

-- datos insertados de [Presupuestos]
INSERT INTO [Presupuestos] ([IdUsuario],[IdCategoria],[MontoAsignado],[FechaInicio],[FechaFin]) VALUES
(1,1,500000,'2025-01-01','2025-12-31'),
(2,2,300000,'2025-01-01','2025-12-31'),
(3,3,800000,'2025-01-01','2025-12-31'),
(4,4,400000,'2025-01-01','2025-12-31'),
(5,5,600000,'2025-01-01','2025-12-31');
GO

-- datos insertados de [MetasAhorro]
INSERT INTO [MetasAhorro] ([IdUsuario],[Nombre],[MontoObjetivo],[FechaLimite]) VALUES
(1,'Viaje a Cartagena',3000000,'2025-12-15'),
(2,'Comprar portátil',4000000,'2025-09-01'),
(3,'Fondo de emergencia',5000000,'2025-11-30'),
(4,'Curso de inglés',2500000,'2025-08-20'),
(5,'Moto nueva',7000000,'2025-10-10');
GO

-- datos insertados de [TarjetasCredito]
INSERT INTO [TarjetasCredito] ([IdUsuario],[Banco],[NumeroTarjeta],[Cupo],[FechaCorte],[FechaPago]) VALUES
(1,'Bancolombia','4111111111111111',5000000,'2025-09-20','2025-09-30'),
(2,'Davivienda','4222222222222222',4000000,'2025-09-15','2025-09-25'),
(3,'BBVA','4333333333333333',3500000,'2025-09-10','2025-09-20'),
(4,'Banco de Bogotá','4444444444444444',6000000,'2025-09-05','2025-09-15'),
(5,'Banco Popular','4555555555555555',4500000,'2025-09-12','2025-09-22');
GO

-- datos insertados de [MovimientosTarjeta]
INSERT INTO [MovimientosTarjeta] ([IdTarjeta],[Descripcion],[Monto]) VALUES
(1,'Compra supermercado',150000),
(2,'Gasolina carro',80000),
(3,'Pago Netflix',45000),
(4,'Compra ropa',200000),
(5,'Cena restaurante',120000);
GO

-- datos insertados de [Deudas]
INSERT INTO [Deudas] ([IdUsuario],[Acreedor],[Monto],[FechaInicio],[FechaVencimiento]) VALUES
(1,'Banco Caja Social',2000000,'2025-01-10','2025-07-10'),
(2,'Coopcentral',1500000,'2025-02-01','2025-08-01'),
(3,'Davivienda',1000000,'2025-03-15','2025-09-15'),
(4,'Banco de Bogotá',500000,'2025-04-05','2025-10-05'),
(5,'BBVA',800000,'2025-05-20','2025-11-20');
GO

-- datos insertados de [PagosDeuda]
INSERT INTO [PagosDeuda] ([IdDeuda],[Monto],[MetodoPago]) VALUES
(1,500000,'Transferencia'),
(2,300000,'Efectivo'),
(3,250000,'PSE'),
(4,100000,'Efectivo'),
(5,200000,'Transferencia');
GO

-- datos insertados de [ConfiguracionUsuario]
INSERT INTO [ConfiguracionUsuario] ([IdUsuario],[MonedaPreferida],[Notificaciones],[LimiteAlertas]) VALUES
(1,'COP',1,1000000),
(2,'COP',0,500000),
(3,'USD',1,2000000),
(4,'COP',1,NULL),
(5,'COP',1,750000);
GO

-- datos insertados de [ProveedoresServicios]
INSERT INTO [ProveedoresServicios] ([Nombre],[Categoria],[Contacto]) VALUES
('Netflix','Streaming','contact@netflix.com'),
('Spotify','Música','support@spotify.com'),
('Claro','Telefonía','servicio@claro.com'),
('Movistar','Internet','ayuda@movistar.com'),
('HBO Max','Streaming','soporte@hbomax.com');
GO

-- datos insertados de [Suscripciones]
INSERT INTO [Suscripciones] ([IdUsuario],[IdProveedor],[MontoMensual],[FechaInicio],[FechaRenovacion]) VALUES
(1,1,26000,'2025-01-01','2025-02-01'),
(2,2,15000,'2025-01-10','2025-02-10'),
(3,3,90000,'2025-01-20','2025-02-20'),
(4,4,120000,'2025-02-01','2025-03-01'),
(5,5,30000,'2025-02-05','2025-03-05');
GO

-- datos insertados de [Auditoria] 
INSERT INTO [Auditoria] ([IdUsuario],[EntidadAfectada],[Operacion],[Detalle]) VALUES
(1,'Usuarios','INSERT','Se creó usuario Carlos Pérez'),
(2,'CuentasBancarias','INSERT','Cuenta Davivienda agregada'),
(3,'Gastos','INSERT','Gasto en educación registrado'),
(4,'Ingresos','INSERT','Ingreso de negocio propio registrado'),
(5,'Deudas','INSERT','Se registró deuda en BBVA');
GO


SELECT * FROM [Usuarios];
SELECT * FROM [CuentasBancarias];
SELECT * FROM [CategoriasGasto];
SELECT * FROM [Gastos];
SELECT * FROM [Ingresos];
SELECT * FROM [Presupuestos];
SELECT * FROM [MetasAhorro];
SELECT * FROM [TarjetasCredito];
SELECT * FROM [MovimientosTarjeta];
SELECT * FROM [Deudas];
SELECT * FROM [PagosDeuda];
SELECT * FROM [ConfiguracionUsuario];
SELECT * FROM [ProveedoresServicios];
SELECT * FROM [Suscripciones];
SELECT * FROM [Auditoria];
GO
