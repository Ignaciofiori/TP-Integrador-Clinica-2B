------------------------------------------------------------
-- CREACIÓN DE BASE DE DATOS
------------------------------------------------------------
IF DB_ID('clinica_db') IS NULL
    CREATE DATABASE clinica_db;
GO
USE clinica_db;
GO


------------------------------------------------------------
-- TABLA OBRA SOCIAL
------------------------------------------------------------
CREATE TABLE ObraSocial (
    id_obra_social INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    porcentaje_cobertura DECIMAL(5,2) DEFAULT 0,
    telefono NVARCHAR(20),
    direccion NVARCHAR(100),
    activo BIT DEFAULT 1
);
GO


------------------------------------------------------------
-- TABLA PACIENTE
------------------------------------------------------------
CREATE TABLE Paciente (
    id_paciente INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    apellido NVARCHAR(50) NOT NULL,
    dni NVARCHAR(15) UNIQUE NOT NULL,
    fecha_nacimiento DATE,
    telefono NVARCHAR(20),
    email NVARCHAR(100),
    direccion NVARCHAR(100),
    id_obra_social INT NULL,
    nro_afiliado NVARCHAR(30),
    activo BIT DEFAULT 1,
    FOREIGN KEY (id_obra_social) REFERENCES ObraSocial(id_obra_social)
);
GO


------------------------------------------------------------
-- TABLA PROFESIONAL
------------------------------------------------------------
CREATE TABLE Profesional (
    id_profesional INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    apellido NVARCHAR(50) NOT NULL,
    dni NVARCHAR(15) UNIQUE NOT NULL,
    fecha_nacimiento DATE,
    telefono NVARCHAR(20),
    email NVARCHAR(100),
    direccion NVARCHAR(100),
    matricula NVARCHAR(30) UNIQUE NOT NULL,
    activo BIT DEFAULT 1
);
GO


------------------------------------------------------------
-- TABLA ESPECIALIDAD
------------------------------------------------------------
CREATE TABLE Especialidad (
    id_especialidad INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    descripcion NVARCHAR(255),
    activo BIT DEFAULT 1
);
GO


------------------------------------------------------------
-- TABLA PROFESIONAL_ESPECIALIDAD
-- (profesional atiende X especialidad a un precio)
------------------------------------------------------------
CREATE TABLE Profesional_Especialidad (
    id_profesional INT NOT NULL,
    id_especialidad INT NOT NULL,
    valor_consulta DECIMAL(10,2) NOT NULL,
    activo BIT DEFAULT 1,
    PRIMARY KEY (id_profesional, id_especialidad),
    FOREIGN KEY (id_profesional) REFERENCES Profesional(id_profesional),
    FOREIGN KEY (id_especialidad) REFERENCES Especialidad(id_especialidad)
);
GO


------------------------------------------------------------
-- TABLA PROFESIONAL_OBRASOCIAL
------------------------------------------------------------
CREATE TABLE Profesional_ObraSocial (
    id_profesional INT NOT NULL,
    id_obra_social INT NOT NULL,
    convenio_activo BIT DEFAULT 1,
    fecha_inicio DATE DEFAULT GETDATE(),
    activo BIT DEFAULT 1,  
    PRIMARY KEY (id_profesional, id_obra_social),
    FOREIGN KEY (id_profesional) REFERENCES Profesional(id_profesional),
    FOREIGN KEY (id_obra_social) REFERENCES ObraSocial(id_obra_social)
);
GO
------------------------------------------------------------
-- TABLA CONSULTORIO
------------------------------------------------------------
CREATE TABLE Consultorio (
    id_consultorio INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50),
    direccion NVARCHAR(100),
    piso NVARCHAR(10),
    numero_sala NVARCHAR(10),
    activo BIT DEFAULT 1
);
GO


------------------------------------------------------------
-- TABLA HORARIO ATENCIÓN
------------------------------------------------------------
CREATE TABLE HorarioAtencion (
    id_horario INT IDENTITY(1,1) PRIMARY KEY,
    id_profesional INT NOT NULL,
    id_especialidad INT NOT NULL,
    id_consultorio INT NOT NULL,
    dia_semana NVARCHAR(10) NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL,
    activo BIT DEFAULT 1,
    CONSTRAINT CHK_dia_semana CHECK (dia_semana IN 
        ('Lunes','Martes','Miércoles','Jueves','Viernes','Sábado')
    ),
    FOREIGN KEY (id_profesional) REFERENCES Profesional(id_profesional),
    FOREIGN KEY (id_especialidad) REFERENCES Especialidad(id_especialidad),
    FOREIGN KEY (id_consultorio) REFERENCES Consultorio(id_consultorio)
);
GO


------------------------------------------------------------
-- TABLA TURNOS
------------------------------------------------------------
CREATE TABLE Turno (
    id_turno INT IDENTITY(1,1) PRIMARY KEY,
    id_paciente INT NOT NULL,
    id_horario INT NOT NULL,
    id_obra_social INT NULL, -- se guarda la OS del momento del turno
    fecha_turno DATE NOT NULL,
    hora_turno TIME NOT NULL,
    estado NVARCHAR(15) DEFAULT 'pendiente', 
    monto_total DECIMAL(10,2) NULL,
    activo BIT DEFAULT 1,
    CONSTRAINT CHK_estado_turno CHECK 
        (estado IN ('pendiente','confirmado','cancelado','asistido')),
    FOREIGN KEY (id_paciente) REFERENCES Paciente(id_paciente),
    FOREIGN KEY (id_horario) REFERENCES HorarioAtencion(id_horario),
    FOREIGN KEY (id_obra_social) REFERENCES ObraSocial(id_obra_social)
);
GO


------------------------------------------------------------
-- TABLA FACTURA
------------------------------------------------------------
CREATE TABLE Factura (
    id_factura INT IDENTITY(1,1) PRIMARY KEY,
    id_turno INT NOT NULL,
    monto_base DECIMAL(10,2) NOT NULL,
    cobertura_aplicada DECIMAL(5,2) DEFAULT 0,
    descuento_aplicado DECIMAL(5,2) DEFAULT 0,
    monto_total DECIMAL(10,2) NOT NULL,
    fecha_emision DATE DEFAULT GETDATE(),
    activo BIT DEFAULT 1,
    FOREIGN KEY (id_turno) REFERENCES Turno(id_turno)
);
GO


------------------------------------------------------------
-- TABLA DESCUENTO
------------------------------------------------------------
CREATE TABLE Descuento (
    id_descuento INT IDENTITY(1,1) PRIMARY KEY,
    id_obra_social INT NOT NULL,
    edad_min INT NULL,
    edad_max INT NULL,
    porcentaje_descuento DECIMAL(5,2) NOT NULL,
    descripcion NVARCHAR(255),
    activo BIT DEFAULT 1,
    FOREIGN KEY (id_obra_social) REFERENCES ObraSocial(id_obra_social)
);
GO


CREATE VIEW vw_Recaudacion
AS
SELECT 
    f.id_factura,
    f.monto_base,
    f.cobertura_aplicada,
    f.descuento_aplicado,
    f.monto_total,
    f.fecha_emision,

    t.id_turno,
    t.fecha_turno,
    t.hora_turno,
    t.estado AS estado_turno,

    (p.apellido + ', ' + p.nombre) AS paciente,

    ISNULL(os.nombre, 'Particular') AS obra_social,

    (prof.apellido + ', ' + prof.nombre) AS profesional,

    esp.nombre AS especialidad,

    (cons.nombre + ' - Sala ' + cons.numero_sala) AS consultorio,

    YEAR(f.fecha_emision) AS anio_factura,
    MONTH(f.fecha_emision) AS mes_factura,
    DAY(f.fecha_emision) AS dia_factura,

    YEAR(t.fecha_turno) AS anio_turno,
    MONTH(t.fecha_turno) AS mes_turno,
    DAY(t.fecha_turno) AS dia_turno
FROM Factura f
    INNER JOIN Turno t ON t.id_turno = f.id_turno
    INNER JOIN Paciente p ON p.id_paciente = t.id_paciente
    LEFT JOIN ObraSocial os ON os.id_obra_social = t.id_obra_social
    INNER JOIN HorarioAtencion h ON h.id_horario = t.id_horario
    INNER JOIN Profesional prof ON prof.id_profesional = h.id_profesional
    INNER JOIN Especialidad esp ON esp.id_especialidad = h.id_especialidad
    INNER JOIN Consultorio cons ON cons.id_consultorio = h.id_consultorio
WHERE f.activo = 1;
GO

------------------------------------------------------------
-- TABLA ROL
------------------------------------------------------------

Create Table Rol(
id_rol INT IDENTITY(1,1) PRIMARY KEY,
nombre_rol VARCHAR(30) NOT NULL
);
GO
------------------------------------------------------------
-- TABLA USUARIO
------------------------------------------------------------
Create Table Usuario(
id_usuario INT IDENTITY(1,1) PRIMARY KEY,
username VARCHAR(100) NOT NULL UNIQUE,
password VARCHAR(50) NOT NULL,
nombre VARCHAR(30) NOT NULL,
apellido VARCHAR(50) NOT NULL,
id_rol INT NOT NULL,
activo BIT DEFAULT 1,
FOREIGN KEY (id_rol) REFERENCES Rol(id_rol)
);
GO
------------------------------------------------------------
-- TABLA PROFESIONAL
------------------------------------------------------------
Create Table Usuario_Profesional(
id_usuario INT NOT NULL UNIQUE,
id_profesional INT NOT NULL UNIQUE,
CONSTRAINT PK_UsuarioProfesional PRIMARY KEY (id_usuario, id_profesional),
FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario),
FOREIGN KEY (id_profesional) REFERENCES Profesional(id_profesional)
);
GO

Alter Table Turno
Add nota_profesional VARCHAR(MAX) NULL
GO

---------------------------------------------------------
--INSERTS--
-----------------------------------------------------------
--Nuevo--

INSERT INTO Rol (nombre_rol)
VALUES ('Administrador'), ('Profesional');

INSERT INTO Usuario (username, password, nombre, apellido, id_rol)
VALUES
('admin1', 'admin123', 'Laura', 'Gomez', 1),
('admin2', 'admin123', 'Jorge', 'Paredes', 1),
('admin3', 'admin123', 'Marina', 'Sosa', 1);

INSERT INTO Usuario (username, password, nombre, apellido, id_rol)
VALUES
('pablof', 'clave123', 'Pablo', 'Fernández', 2),
('sofiam', 'clave123', 'Sofía', 'Martínez', 2),
('lucasp', 'clave123', 'Lucas', 'Pereyra', 2),
('caros', 'clave123', 'Carolina', 'Suárez', 2),
('martind', 'clave123', 'Martín', 'Delgado', 2),
('veroi', 'clave123', 'Verónica', 'Iglesias', 2),
('julianm', 'clave123', 'Julián', 'Moreno', 2),
('ivanas', 'clave123', 'Ivana', 'Soria', 2),
('ramirog', 'clave123', 'Ramiro', 'Gauna', 2),
('elenap', 'clave123', 'Elena', 'Paz', 2),
('adrianq', 'clave123', 'Adrián', 'Quiroga', 2),
('martao', 'clave123', 'Marta', 'Ojeda', 2),
('danielb', 'clave123', 'Daniel', 'Barrios', 2),
('claudiar', 'clave123', 'Claudia', 'Rojas', 2),
('estebanl', 'clave123', 'Esteban', 'López', 2),
('nadiag', 'clave123', 'Nadia', 'Giménez', 2),
('tobiasv', 'clave123', 'Tobías', 'Varela', 2),
('flors', 'clave123', 'Florencia', 'Sánchez', 2),
('mauricioh', 'clave123', 'Mauricio', 'Herrera', 2),
('rocion', 'clave123', 'Rocío', 'Núñez', 2);

INSERT INTO Usuario_Profesional (id_usuario, id_profesional)
VALUES
(4, 1),
(5, 2),
(6, 3),
(7, 4),
(8, 5),
(9, 6),
(10, 7),
(11, 8),
(12, 9),
(13, 10),
(14, 11),
(15, 12),
(16, 13),
(17, 14),
(18, 15),
(19, 16),
(20, 17),
(21, 18),
(22, 19),
(23, 20);
--Nuevo--

INSERT INTO ObraSocial (nombre, porcentaje_cobertura, telefono, direccion, activo)
VALUES
('OSDE', 70, '0810-555-6733', 'Av. Libertador 1001', 1),
('IOMA', 50, '0810-222-4662', 'Calle Salud 520', 1),
('Swiss Medical', 80, '0810-345-7890', 'Av. Médica 900', 1),
('Galeno', 65, '0800-777-4253', 'Av. Córdoba 1351', 1),
('Medifé', 60, '0810-333-6333', 'Av. Rivadavia 800', 1),
('OMINT', 75, '0810-555-6666', 'Reconquista 845', 1),
('OSPJN', 55, '011-4331-2222', 'Lavalle 1430', 1),
('APSOT', 40, '0810-444-1212', 'Callao 1234', 1),
('OSPEGAP', 70, '0800-222-5555', 'Florida 550', 1),
('Particular', 0, NULL, NULL, 1);
GO

INSERT INTO Especialidad (nombre, descripcion, activo)
VALUES
('Clínica Médica','Atención general',1),
('Cardiología','Sistema cardiovascular',1),
('Pediatría','Salud infantil',1),
('Dermatología','Piel y anexos',1),
('Traumatología','Sistema musculoesquelético',1),
('Ginecología','Salud femenina',1),
('Neurología','Sistema nervioso',1),
('Endocrinología','Hormonas y metabolismo',1),
('Gastroenterología','Sistema digestivo',1),
('Urología','Tracto urinario',1);
GO


INSERT INTO Profesional (nombre, apellido, dni, fecha_nacimiento, telefono, email, direccion, matricula, activo)
VALUES
('Pablo','Fernández','25789456','1975-03-12','1143216789','pablo@clinic.com','Mitre 1200','MAT-1001',1),
('Sofía','Martínez','27845123','1980-07-15','1187654321','sofia@clinic.com','Rivadavia 1320','MAT-1002',1),
('Lucas','Pereyra','30123456','1985-02-09','1178945612','lucas@clinic.com','Belgrano 500','MAT-1003',1),
('Carolina','Suárez','29222333','1990-10-01','1199900011','caro@clinic.com','Sarmiento 300','MAT-1004',1),
('Martín','Delgado','30000555','1981-06-18','1133300022','martin@clinic.com','Callao 710','MAT-1005',1),
('Verónica','Iglesias','28777000','1978-01-12','1144112233','vero@clinic.com','Caseros 890','MAT-1006',1),
('Julián','Moreno','31888999','1992-05-29','1188223344','julian@clinic.com','Uriburu 150','MAT-1007',1),
('Ivana','Soria','30555333','1987-11-23','1155778899','ivana@clinic.com','Larrea 220','MAT-1008',1),
('Ramiro','Gauna','27000999','1979-04-14','1144778899','ramiro@clinic.com','Salguero 430','MAT-1009',1),
('Elena','Paz','26333555','1973-09-05','1199443322','elena@clinic.com','Bulnes 510','MAT-1010',1),
('Adrián','Quiroga','31111666','1984-12-10','1122993344','adrian@clinic.com','Viamonte 900','MAT-1011',1),
('Marta','Ojeda','28222111','1969-08-11','1133445599','marta@clinic.com','Córdoba 1300','MAT-1012',1),
('Daniel','Barrios','29911222','1982-03-15','1144556677','daniel@clinic.com','Lavalle 130','MAT-1013',1),
('Claudia','Rojas','30099888','1976-12-01','1188807766','claudia@clinic.com','Yrigoyen 800','MAT-1014',1),
('Esteban','López','25666111','1974-01-15','1122445599','esteban@clinic.com','Jujuy 440','MAT-1015',1),
('Nadia','Giménez','33000333','1991-11-02','1166552211','nadia@clinic.com','Cultreras 120','MAT-1016',1),
('Tobías','Varela','31444777','1989-07-30','1177665544','tobias@clinic.com','Rosales 820','MAT-1017',1),
('Florencia','Sánchez','29988444','1993-04-28','1133557733','flor@clinic.com','French 670','MAT-1018',1),
('Mauricio','Herrera','27777444','1977-02-22','1144668899','mauri@clinic.com','Anchorena 520','MAT-1019',1),
('Rocío','Núñez','28888777','1992-09-09','1188110022','rocio@clinic.com','Anchorena 900','MAT-1020',1);

INSERT INTO Paciente 
(nombre, apellido, dni, fecha_nacimiento, telefono, email, direccion, id_obra_social, nro_afiliado, activo)
VALUES
('Lucía', 'Fernández', '30111222', '1990-03-12', '1122334455', 'lucia@mail.com', 'Rivadavia 1200', 1, 'OSDE-1001', 1),
('Marcos', 'Díaz', '29555111', '1988-05-20', '1133445566', 'marcos@mail.com', 'Mitre 450', 2, 'IOMA-5001', 1),
('Ana', 'Pérez', '32111444', '1995-07-02', '1144556677', 'ana@mail.com', 'Belgrano 890', 3, 'SWISS-2233', 1),
('Juan', 'Lopez', '28999111', '1980-01-11', '1155667788', 'juan@mail.com', 'Alsina 332', 4, 'GAL-3321', 1),
('Sofía', 'Molina', '33777222', '2000-02-22', '1166778899', 'sofia@mail.com', 'Lima 230', 5, 'MED-1122', 1),
('Diego', 'Ramírez', '30000001', '1984-10-30', '1133221100', 'diego@mail.com', 'Corrientes 900', 6, 'OM-2211', 1),
('Carla', 'Santos', '29999888', '1992-06-10', '1122113344', 'carla@mail.com', 'San Juan 250', 7, 'OSPJN-6182', 1),
('Pedro', 'Ruiz', '27777333', '1978-08-22', '1177992233', 'pedro@mail.com', 'Laprida 100', 8, 'APS-1129', 1),
('Valentina', 'Silva', '33333111', '1999-03-30', '1155337799', 'valen@mail.com', 'Chile 550', 9, 'OSP-5599', 1),
('Nicolás', 'Martínez', '31122888', '1993-04-12', '1166110033', 'nico@mail.com', 'Rawson 800', 1, 'OSDE-2002', 1),
('Brenda', 'Cruz', '32222111', '1997-05-22', '1155001122', 'brenda@mail.com', 'Urquiza 440', 3, 'SWISS-3455', 1),
('Elena', 'Farias', '28888111', '1970-11-01', '1133117788', 'elena@mail.com', 'Pueyrredón 1020', 4, 'GAL-4455', 1),
('Mario', 'Giménez', '25555999', '1968-06-05', '1166445577', 'mario@mail.com', 'Independencia 900', 2, 'IOMA-9922', 1),
('Julieta', 'Bravo', '34444999', '2002-09-21', '1177441100', 'julieta@mail.com', 'La Rioja 710', 5, 'MED-5566', 1),
('Tomás', 'Vega', '30066555', '1994-01-15', '1177112211', 'tomas@mail.com', 'Azcuénaga 300', 6, 'OM-3311', 1),
('Rocío', 'Gallo', '31222111', '1996-12-19', '1155223300', 'rocio@mail.com', 'Sarmiento 1500', 7, 'OSPJN-9991', 1),
('Alan', 'Maidana', '29911001', '1991-03-03', '1144882299', 'alan@mail.com', 'Defensa 1020', 8, 'APS-6655', 1),
('Laura', 'Soto', '28888222', '1983-04-17', '1122331199', 'laura@mail.com', 'Tacuarí 230', 9, 'OSPE-1100', 1),
('Gabriel', 'Torres', '26666111', '1975-07-29', '1166998800', 'gabriel@mail.com', 'Moreno 180', 1, 'OSDE-9910', 1),
('Milagros', 'Rueda', '33322999', '2001-08-02', '1177886655', 'mila@mail.com', 'Alberdi 1200', 3, 'SWISS-0098', 1);

INSERT INTO Consultorio (nombre, direccion, piso, numero_sala, activo)
VALUES
('Consultorio Central', 'Av. Salud 1500', '1', '101', 1),
('Consultorio Norte', 'Av. Libertad 2500', '2', '205', 1),
('Consultorio Sur', 'Av. Paz 3300', '3', '310', 1),
('Consultorio Este', 'Av. Italia 4500', '1', '115', 1),
('Consultorio Oeste', 'Av. España 5400', 'PB', '12', 1);

INSERT INTO Profesional_Especialidad VALUES
(1,1,8000,1),(1,4,9000,1),
(2,2,12000,1),(2,6,11000,1),
(3,3,7000,1),(3,9,8500,1),
(4,4,9500,1),(4,1,8200,1),
(5,5,10000,1),(5,2,12000,1),
(6,6,11000,1),(6,10,9000,1),
(7,7,13000,1),(7,3,7500,1),
(8,8,14000,1),(8,4,9000,1),
(9,9,12000,1),(9,1,7500,1),
(10,10,15000,1),(10,5,10000,1),
(11,1,8000,1),(11,7,12000,1),
(12,2,12000,1),(12,4,9000,1),
(13,3,7000,1),(13,8,14000,1),
(14,6,11000,1),(14,9,12000,1),
(15,10,15000,1),(15,2,11000,1),
(16,1,8500,1),(16,6,11500,1),
(17,2,13000,1),(17,5,9000,1),
(18,3,8000,1),(18,4,9500,1),
(19,4,9000,1),(19,7,13000,1),
(20,9,12500,1),(20,10,14000,1);

INSERT INTO Profesional_ObraSocial VALUES
(1,1,1,'2022-01-01',1),(1,3,1,'2022-02-02',1),(1,5,1,'2022-03-03',1),
(2,2,1,'2022-01-01',1),(2,4,1,'2022-01-05',1),
(3,1,1,'2022-01-01',1),(3,3,1,'2022-01-10',1),(3,6,1,'2022-01-15',1),
(4,7,1,'2022-01-01',1),(4,1,1,'2022-01-20',1),
(5,2,1,'2022-02-01',1),(5,9,1,'2022-02-10',1),
(6,3,1,'2022-03-01',1),(6,10,1,'2022-03-15',1),
(7,4,1,'2022-04-01',1),(7,1,1,'2022-04-15',1),
(8,8,1,'2022-05-01',1),(8,5,1,'2022-05-10',1),
(9,1,1,'2022-06-01',1),(9,3,1,'2022-06-10',1),
(10,2,1,'2022-07-01',1),(10,6,1,'2022-07-10',1),
(11,1,1,'2022-08-01',1),(11,9,1,'2022-08-15',1),
(12,5,1,'2022-09-01',1),(12,2,1,'2022-09-10',1),
(13,3,1,'2022-10-01',1),(13,7,1,'2022-10-15',1),
(14,10,1,'2022-11-01',1),(14,4,1,'2022-11-10',1),
(15,1,1,'2022-12-01',1),(15,2,1,'2022-12-10',1),
(16,6,1,'2023-01-01',1),(16,3,1,'2023-01-15',1),
(17,2,1,'2023-02-01',1),(17,8,1,'2023-02-20',1),
(18,4,1,'2023-03-01',1),(18,1,1,'2023-03-20',1),
(19,7,1,'2023-04-01',1),(19,5,1,'2023-04-15',1),
(20,9,1,'2023-05-01',1),(20,1,1,'2023-05-20',1);

INSERT INTO HorarioAtencion (id_profesional,id_especialidad,id_consultorio,dia_semana,hora_inicio,hora_fin,activo)
VALUES
(1,1,1,'Lunes','08:00','09:00',1),(1,4,1,'Martes','10:00','11:00',1),
(2,2,2,'Miércoles','09:00','10:00',1),(2,6,2,'Viernes','14:00','15:00',1),
(3,3,3,'Jueves','08:00','09:00',1),(3,9,1,'Martes','16:00','17:00',1),
(4,4,1,'Sábado','10:00','11:00',1),(4,1,5,'Lunes','15:00','16:00',1),
(5,5,4,'Martes','09:00','10:00',1),(5,2,1,'Miércoles','11:00','12:00',1),
(6,6,2,'Jueves','13:00','14:00',1),(6,10,3,'Viernes','09:00','10:00',1),
(7,7,5,'Lunes','10:00','11:00',1),(7,3,2,'Martes','12:00','13:00',1),
(8,8,1,'Miércoles','08:00','09:00',1),(8,4,4,'Jueves','16:00','17:00',1),
(9,9,3,'Viernes','10:00','11:00',1),(9,1,5,'Martes','08:00','09:00',1),
(10,10,2,'Sábado','09:00','10:00',1),(10,5,4,'Lunes','11:00','12:00',1),
(11,1,1,'Martes','14:00','15:00',1),(11,7,3,'Miércoles','15:00','16:00',1),
(12,2,2,'Jueves','11:00','12:00',1),(12,4,5,'Viernes','08:00','09:00',1),
(13,3,4,'Lunes','09:00','10:00',1),(13,8,5,'Martes','10:00','11:00',1),
(14,6,3,'Miércoles','10:00','11:00',1),(14,9,2,'Jueves','14:00','15:00',1),
(15,10,4,'Viernes','09:00','10:00',1),(15,2,1,'Sábado','11:00','12:00',1),
(16,1,5,'Lunes','13:00','14:00',1),(16,6,4,'Martes','15:00','16:00',1),
(17,2,3,'Miércoles','16:00','17:00',1),(17,5,4,'Jueves','09:00','10:00',1),
(18,3,1,'Viernes','13:00','14:00',1),(18,4,5,'Sábado','10:00','11:00',1),
(19,4,2,'Lunes','08:00','09:00',1),(19,7,3,'Martes','14:00','15:00',1),
(20,9,4,'Miércoles','17:00','18:00',1),(20,10,5,'Jueves','11:00','12:00',1);

DECLARE @hoy DATE = GETDATE();

-- TURNOS PASADOS (asistidos / confirmados / cancelados)
INSERT INTO Turno (id_paciente,id_horario,id_obra_social,fecha_turno,hora_turno,estado,monto_total,activo)
VALUES
(1,1,1,DATEADD(DAY,-10,@hoy),'08:00','asistido',8000,1),
(2,2,2,DATEADD(DAY,-20,@hoy),'09:00','asistido',12000,1),
(3,3,3,DATEADD(DAY,-5,@hoy),'10:00','confirmado',7000,1),
(4,4,4,DATEADD(DAY,-3,@hoy),'11:00','asistido',9000,1),
(5,5,5,DATEADD(DAY,-15,@hoy),'09:00','cancelado',NULL,1),
(6,6,6,DATEADD(DAY,-8,@hoy),'14:00','asistido',11000,1),
(7,7,7,DATEADD(DAY,-12,@hoy),'12:00','confirmado',7500,1),
(8,8,8,DATEADD(DAY,-6,@hoy),'08:00','asistido',14000,1),
(9,9,9,DATEADD(DAY,-9,@hoy),'10:00','asistido',12000,1),
(10,10,10,DATEADD(DAY,-11,@hoy),'09:00','asistido',15000,1),
(11,11,1,DATEADD(DAY,-13,@hoy),'14:00','confirmado',8000,1),
(12,12,2,DATEADD(DAY,-4,@hoy),'08:00','asistido',12000,1),
(13,13,3,DATEADD(DAY,-7,@hoy),'09:00','asistido',7000,1),
(14,14,4,DATEADD(DAY,-14,@hoy),'10:00','asistido',11000,1),
(15,15,5,DATEADD(DAY,-16,@hoy),'11:00','asistido',10000,1),
(16,16,6,DATEADD(DAY,-17,@hoy),'13:00','asistido',8500,1),
(17,17,7,DATEADD(DAY,-18,@hoy),'16:00','confirmado',13000,1),
(18,18,8,DATEADD(DAY,-19,@hoy),'14:00','asistido',8000,1),
(19,19,9,DATEADD(DAY,-21,@hoy),'08:00','asistido',9000,1),
(20,20,10,DATEADD(DAY,-22,@hoy),'11:00','confirmado',14000,1);

-- TURNOS FUTUROS (pendientes) CON MONTOS
INSERT INTO Turno (id_paciente,id_horario,id_obra_social,fecha_turno,hora_turno,estado,monto_total,activo)
VALUES
(1,1,1,DATEADD(DAY,2,@hoy),'08:00','pendiente',8000,1),
(2,2,2,DATEADD(DAY,5,@hoy),'09:00','pendiente',12000,1),
(3,3,3,DATEADD(DAY,3,@hoy),'10:00','pendiente',7000,1),
(4,4,4,DATEADD(DAY,6,@hoy),'11:00','pendiente',9000,1),
(5,5,5,DATEADD(DAY,7,@hoy),'09:00','pendiente',10000,1),
(6,6,6,DATEADD(DAY,10,@hoy),'14:00','pendiente',11000,1),
(7,7,7,DATEADD(DAY,12,@hoy),'12:00','pendiente',7500,1),
(8,8,8,DATEADD(DAY,8,@hoy),'08:00','pendiente',14000,1),
(9,9,9,DATEADD(DAY,9,@hoy),'10:00','pendiente',12000,1),
(10,10,10,DATEADD(DAY,11,@hoy),'09:00','pendiente',15000,1),
(11,11,1,DATEADD(DAY,14,@hoy),'14:00','pendiente',8000,1),
(12,12,2,DATEADD(DAY,13,@hoy),'08:00','pendiente',12000,1),
(13,13,3,DATEADD(DAY,16,@hoy),'09:00','pendiente',7000,1),
(14,14,4,DATEADD(DAY,18,@hoy),'10:00','pendiente',11000,1),
(15,15,5,DATEADD(DAY,20,@hoy),'11:00','pendiente',10000,1),
(16,16,6,DATEADD(DAY,22,@hoy),'13:00','pendiente',8500,1),
(17,17,7,DATEADD(DAY,25,@hoy),'16:00','pendiente',13000,1),
(18,18,8,DATEADD(DAY,30,@hoy),'14:00','pendiente',8000,1),
(19,19,9,DATEADD(DAY,35,@hoy),'08:00','pendiente',9000,1),
(20,20,10,DATEADD(DAY,40,@hoy),'11:00','pendiente',14000,1);


INSERT INTO Factura (id_turno,monto_base,cobertura_aplicada,descuento_aplicado,monto_total,fecha_emision,activo)
SELECT 
    id_turno,
    monto_total AS monto_base,
    0 AS cobertura_aplicada,
    0 AS descuento_aplicado,
    monto_total,
    DATEADD(DAY,-1,GETDATE()),
    1
FROM Turno
WHERE estado IN ('asistido','confirmado') AND monto_total IS NOT NULL;


