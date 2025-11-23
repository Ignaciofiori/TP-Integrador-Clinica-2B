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

USE clinica_db;
GO

------------------------------------------------------------
-- OBRAS SOCIALES
------------------------------------------------------------
INSERT INTO ObraSocial (nombre, porcentaje_cobertura, telefono, direccion, activo)
VALUES
('OSDE', 70, '0810-555-6733', 'Av. Libertador 1001', 1),
('IOMA', 50, '0810-222-4662', 'Calle Salud 520', 1),
('Swiss Medical', 80, '0810-345-7890', 'Av. Médica 900', 1),
('Galeno', 65, '0800-777-4253', 'Av. Córdoba 1351', 1);
GO

------------------------------------------------------------
-- PACIENTES
------------------------------------------------------------
INSERT INTO Paciente 
(nombre, apellido, dni, fecha_nacimiento, telefono, email, direccion, id_obra_social, nro_afiliado, activo)
VALUES
('Laura', 'González', '32145678', '1980-05-12', '1134567890', 'laura@example.com', 'Av. Siempre Viva 123', 1, 'OSDE-20001', 1),
('Carlos', 'Ramírez', '28987541', '1975-10-02', '1156782349', 'carlos@example.com', 'Calle Sol 456', 2, 'IOMA-45521', 1),
('Mariana', 'Torres', '33456789', '1988-03-22', '1167892345', 'mariana@example.com', 'Calle Luna 789', 3, 'SM-89012', 1),
('Julián', 'Pérez', '40123456', '1999-11-15', '1176543210', 'julian@example.com', 'Mitre 234', NULL, NULL, 1),
('Valeria', 'Suárez', '39234567', '1995-07-30', '1187654321', 'valeria@example.com', 'Rivadavia 1000', 4, 'GAL-11223', 1);
GO

------------------------------------------------------------
-- PROFESIONALES
------------------------------------------------------------
INSERT INTO Profesional 
(nombre, apellido, dni, matricula, telefono, email, activo)
VALUES
('Pablo', 'Fernández', '25789456', 'MAT-1001', '1143216789', 'pablo.fernandez@clinic.com', 1),
('Sofía', 'Martínez', '27845123', 'MAT-1002', '1187654321', 'sofia.martinez@clinic.com', 1),
('Lucas', 'Pereyra', '30123456', 'MAT-1003', '1178945612', 'lucas.pereyra@clinic.com', 1);
GO

------------------------------------------------------------
-- ESPECIALIDADES
------------------------------------------------------------
INSERT INTO Especialidad (nombre, descripcion, activo)
VALUES
('Clínica Médica', 'Atención general de salud del adulto', 1),
('Cardiología', 'Sistema cardiovascular', 1),
('Pediatría', 'Salud infantil', 1),
('Dermatología', 'Enfermedades de la piel', 1);
GO

------------------------------------------------------------
-- PROFESIONAL x ESPECIALIDAD
------------------------------------------------------------
INSERT INTO Profesional_Especialidad (id_profesional, id_especialidad, valor_consulta, activo)
VALUES
(1, 1, 8000, 1),   -- Pablo - Clínica Médica
(1, 4, 9000, 1),   -- Pablo - Dermatología
(2, 2, 12000, 1),  -- Sofía - Cardiología
(3, 3, 7000, 1);   -- Lucas - Pediatría
GO

------------------------------------------------------------
-- PROFESIONAL x OBRA SOCIAL
------------------------------------------------------------
INSERT INTO Profesional_ObraSocial (id_profesional, id_obra_social, convenio_activo, fecha_inicio, activo)
VALUES
(1, 1, 1, GETDATE(), 1),   -- Pablo - OSDE
(1, 3, 1, GETDATE(), 1),   -- Pablo - Swiss
(2, 2, 1, GETDATE(), 1),   -- Sofía - IOMA
(3, 1, 1, GETDATE(), 1),   -- Lucas - OSDE
(3, 4, 1, GETDATE(), 1);   -- Lucas - Galeno
GO

------------------------------------------------------------
-- CONSULTORIOS
------------------------------------------------------------
INSERT INTO Consultorio (nombre, direccion, piso, numero_sala, activo)
VALUES
('Consultorio Central', 'Av. Salud 1500', '1', '101', 1),
('Consultorio Norte', 'Av. Libertad 2500', '2', '205', 1);
GO

------------------------------------------------------------
-- HORARIOS DE ATENCIÓN (ahora de 30 o 60 minutos)
------------------------------------------------------------
-- *** PROFESIONAL 1: Dr. Pablo ***
-- Clínica Médica (Lunes)
INSERT INTO HorarioAtencion VALUES (1, 1, 1, 'Lunes', '08:00', '08:30', 1);
INSERT INTO HorarioAtencion VALUES (1, 1, 1, 'Lunes', '08:30', '09:00', 1);
INSERT INTO HorarioAtencion VALUES (1, 1, 1, 'Lunes', '09:00', '10:00', 1);
INSERT INTO HorarioAtencion VALUES (1, 1, 1, 'Lunes', '10:00', '10:30', 1);
INSERT INTO HorarioAtencion VALUES (1, 1, 1, 'Lunes', '10:30', '11:00', 1);

-- Dermatología (Miércoles)
INSERT INTO HorarioAtencion VALUES (1, 4, 1, 'Miércoles', '15:00', '15:30', 1);
INSERT INTO HorarioAtencion VALUES (1, 4, 1, 'Miércoles', '15:30', '16:00', 1);
INSERT INTO HorarioAtencion VALUES (1, 4, 1, 'Miércoles', '16:00', '17:00', 1);
INSERT INTO HorarioAtencion VALUES (1, 4, 1, 'Miércoles', '17:00', '17:30', 1);

-- *** PROFESIONAL 2: Dra. Sofía ***
-- Cardiología (Martes)
INSERT INTO HorarioAtencion VALUES (2, 2, 2, 'Martes', '09:00', '09:30', 1);
INSERT INTO HorarioAtencion VALUES (2, 2, 2, 'Martes', '09:30', '10:00', 1);
INSERT INTO HorarioAtencion VALUES (2, 2, 2, 'Martes', '10:00', '11:00', 1);
INSERT INTO HorarioAtencion VALUES (2, 2, 2, 'Martes', '11:00', '11:30', 1);
INSERT INTO HorarioAtencion VALUES (2, 2, 2, 'Martes', '11:30', '12:00', 1);

-- *** PROFESIONAL 3: Dr. Lucas ***
-- Pediatría (Jueves)
INSERT INTO HorarioAtencion VALUES (3, 3, 1, 'Jueves', '08:00', '08:30', 1);
INSERT INTO HorarioAtencion VALUES (3, 3, 1, 'Jueves', '08:30', '09:00', 1);
INSERT INTO HorarioAtencion VALUES (3, 3, 1, 'Jueves', '09:00', '10:00', 1);
INSERT INTO HorarioAtencion VALUES (3, 3, 1, 'Jueves', '10:00', '10:30', 1);
INSERT INTO HorarioAtencion VALUES (3, 3, 1, 'Jueves', '10:30', '11:00', 1);

-- Sábado (Pediatría)
INSERT INTO HorarioAtencion VALUES (3, 3, 1, 'Sábado', '09:00', '09:30', 1);
INSERT INTO HorarioAtencion VALUES (3, 3, 1, 'Sábado', '09:30', '10:00', 1);
INSERT INTO HorarioAtencion VALUES (3, 3, 1, 'Sábado', '10:00', '10:30', 1);
GO

------------------------------------------------------------
-- TURNOS
------------------------------------------------------------
INSERT INTO Turno (id_paciente, id_horario, fecha_turno, hora_turno, estado, monto_total, id_obra_social, activo)
VALUES
(1, 1, '2025-11-07', '08:00', 'confirmado', 8000, 1, 1),
(2, 8, '2025-11-12', '09:30', 'pendiente', NULL, 2, 1),
(3, 12, '2025-11-15', '10:00', 'asistido', 12000, 3, 1),
(4, 15, '2025-11-16', '08:00', 'pendiente', NULL, NULL, 1),
(5, 18, '2025-11-20', '09:00', 'cancelado', NULL, 4, 1);
GO

------------------------------------------------------------
-- FACTURA
------------------------------------------------------------
INSERT INTO Factura (id_turno, monto_base, cobertura_aplicada, descuento_aplicado, monto_total, fecha_emision, activo)
VALUES
(3, 12000, 80, 0, 2400, GETDATE(), 1);
GO


------------------------------------------------------------
-- DESCUENTOS PARA OSDE (id 1)
------------------------------------------------------------
INSERT INTO Descuento (id_obra_social, edad_min, edad_max, porcentaje_descuento, descripcion, activo)
VALUES 
(1, 0, 12, 15, 'Descuento para menores de 12 años (OSDE)', 1),
(1, 60, 120, 20, 'Descuento para adultos mayores (OSDE)', 1),
(1, NULL, NULL, 5, 'Descuento general OSDE', 1);


------------------------------------------------------------
-- DESCUENTOS PARA IOMA (id 2)
------------------------------------------------------------
INSERT INTO Descuento (id_obra_social, edad_min, edad_max, porcentaje_descuento, descripcion, activo)
VALUES 
(2, 0, 18, 10, 'Descuento para menores de 18 (IOMA)', 1),
(2, 65, 120, 15, 'Descuento para jubilados (IOMA)', 1),
(2, NULL, NULL, 7, 'Descuento general IOMA', 1);


------------------------------------------------------------
-- DESCUENTOS PARA SWISS MEDICAL (id 3)
------------------------------------------------------------
INSERT INTO Descuento (id_obra_social, edad_min, edad_max, porcentaje_descuento, descripcion, activo)
VALUES 
(3, 0, 10, 20, 'Beneficio pediátrico Swiss Medical', 1),
(3, 55, 120, 18, 'Descuento mayores de 55 (Swiss Medical)', 1),
(3, NULL, NULL, 8, 'Descuento general Swiss Medical', 1);
------------------------------------------------------------
-- FIN DEL SCRIPT
------------------------------------------------------------
