-- Crear la base de datos
CREATE DATABASE SistemaPedidos;
GO

USE SistemaPedidos;
GO

-- Tabla de usuarios (clientes y administradores)
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Contraseña NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(20) NOT NULL CHECK (Rol IN ('Cliente', 'Administrador'))
);
GO

-- Tabla de productos
CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL
);
GO

-- Tabla de pedidos
CREATE TABLE Pedidos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Estado NVARCHAR(50) DEFAULT 'Pendiente',
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id) ON DELETE CASCADE
);
GO

-- Tabla de detalles de pedido
CREATE TABLE DetallesPedido (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdPedido INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(Id) ON DELETE CASCADE,
    FOREIGN KEY (IdProducto) REFERENCES Productos(Id) ON DELETE CASCADE
);
GO




-- Insertar un cliente
INSERT INTO Usuarios (Nombre, Email, Contraseña, Rol)
VALUES ('Juan Pérez', 'cliente@example.com', 'cliente123', 'Cliente');

-- Insertar un administrador
INSERT INTO Usuarios (Nombre, Email, Contraseña, Rol)
VALUES ('Ana Gómez', 'admin@example.com', 'admin123', 'Administrador');



INSERT INTO Productos (Nombre, Descripcion, Precio, Stock) VALUES
('Coca-Cola 500ml', 'Bebida gaseosa sabor cola en botella de 500ml', 1.20, 150),
('Pepsi 500ml', 'Bebida gaseosa sabor cola en botella de 500ml', 1.15, 140),
('Fanta Naranja 500ml', 'Bebida gaseosa sabor naranja en botella de 500ml', 1.10, 130),
('Sprite 500ml', 'Bebida gaseosa sabor limón-lima en botella de 500ml', 1.10, 120),
('Jugo de Naranja Natural 1L', 'Jugo natural de naranja sin azúcar añadida', 2.50, 100),
('Jugo de Manzana 1L', 'Jugo natural de manzana', 2.30, 110),
('Agua Mineral 600ml', 'Agua mineral sin gas', 0.90, 200),
('Agua con Gas 600ml', 'Agua mineral con gas', 1.00, 180),
('Gatorade Naranja 500ml', 'Bebida isotónica sabor naranja', 1.80, 90),
('Red Bull 250ml', 'Bebida energética', 2.50, 75);