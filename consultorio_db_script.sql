------------------------------------------------------------
-- CREACIÓN DE BASE DE DATOS
------------------------------------------------------------
IF DB_ID('consultorio_db') IS NULL
    CREATE DATABASE consultorio_db;
GO
USE consultorio_db;
GO

------------------------------------------------------------
-- TABLA PERSONA
------------------------------------------------------------
CREATE TABLE Persona (
    id_persona INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    apellido NVARCHAR(50) NOT NULL,
    dni NVARCHAR(15) UNIQUE NOT NULL,
    fecha_nacimiento DATE,
    telefono NVARCHAR(20),
    email NVARCHAR(100),
    direccion NVARCHAR(100),
    activo BIT DEFAULT 1
);
GO

------------------------------------------------------------
-- TABLA ESPECIALIDAD
------------------------------------------------------------
CREATE TABLE Especialidad (
    id_especialidad INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    descripcion NVARCHAR(255)
);
GO

------------------------------------------------------------
-- TABLA PROFESIONAL (subtipo de Persona)
------------------------------------------------------------
CREATE TABLE Profesional (
    id_profesional INT PRIMARY KEY,
    matricula NVARCHAR(30) UNIQUE NOT NULL,
    FOREIGN KEY (id_profesional) REFERENCES Persona(id_persona)
);
GO

------------------------------------------------------------
-- TABLA OBRA SOCIAL
------------------------------------------------------------
CREATE TABLE ObraSocial (
    id_obra_social INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    porcentaje_cobertura DECIMAL(5,2) DEFAULT 0,
    telefono NVARCHAR(20),
    direccion NVARCHAR(100)
);
GO

------------------------------------------------------------
-- TABLA PACIENTE (subtipo de Persona)
------------------------------------------------------------
CREATE TABLE Paciente (
    id_paciente INT PRIMARY KEY,
    id_obra_social INT,
    nro_afiliado NVARCHAR(30),
    FOREIGN KEY (id_paciente) REFERENCES Persona(id_persona),
    FOREIGN KEY (id_obra_social) REFERENCES ObraSocial(id_obra_social)
);
GO

------------------------------------------------------------
-- TABLA DESCUENTO (por obra social y condiciones)
------------------------------------------------------------
CREATE TABLE Descuento (
    id_descuento INT IDENTITY(1,1) PRIMARY KEY,
    id_obra_social INT NOT NULL,
    edad_min INT,
    edad_max INT,
    porcentaje_descuento DECIMAL(5,2),
    descripcion NVARCHAR(100),
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
    numero_sala NVARCHAR(10)
);
GO

------------------------------------------------------------
-- TABLA PROFESIONAL_ESPECIALIDAD (valor por especialidad)
------------------------------------------------------------
CREATE TABLE Profesional_Especialidad (
    id_profesional INT NOT NULL,
    id_especialidad INT NOT NULL,
    valor_consulta DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (id_profesional, id_especialidad),
    FOREIGN KEY (id_profesional) REFERENCES Profesional(id_profesional),
    FOREIGN KEY (id_especialidad) REFERENCES Especialidad(id_especialidad)
);
GO

------------------------------------------------------------
-- *** NUEVA TABLA ***
-- RELACIÓN MUCHOS A MUCHOS ENTRE PROFESIONAL Y OBRA SOCIAL
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
-- TABLA HORARIO DE ATENCIÓN
------------------------------------------------------------
CREATE TABLE HorarioAtencion (
    id_horario INT IDENTITY(1,1) PRIMARY KEY,
    id_profesional INT NOT NULL,
    id_consultorio INT NOT NULL,
    dia_semana NVARCHAR(10) NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL,
    FOREIGN KEY (id_profesional) REFERENCES Profesional(id_profesional),
    FOREIGN KEY (id_consultorio) REFERENCES Consultorio(id_consultorio),
    CONSTRAINT CHK_dia_semana CHECK (dia_semana IN ('Lunes','Martes','Miércoles','Jueves','Viernes','Sábado')),
    UNIQUE (id_profesional, id_consultorio, dia_semana, hora_inicio, hora_fin)
);
GO

------------------------------------------------------------
-- TABLA TURNO
------------------------------------------------------------
CREATE TABLE Turno (
    id_turno INT IDENTITY(1,1) PRIMARY KEY,
    id_paciente INT NOT NULL,
    id_horario INT NOT NULL,
    id_especialidad INT NOT NULL,
    id_obra_social INT,
    fecha_turno DATE NOT NULL,
    hora_turno TIME NOT NULL,
    estado NVARCHAR(15) DEFAULT 'pendiente',
    monto_total DECIMAL(10,2),
    FOREIGN KEY (id_paciente) REFERENCES Paciente(id_paciente),
    FOREIGN KEY (id_horario) REFERENCES HorarioAtencion(id_horario),
    FOREIGN KEY (id_especialidad) REFERENCES Especialidad(id_especialidad),
    FOREIGN KEY (id_obra_social) REFERENCES ObraSocial(id_obra_social),
    CONSTRAINT CHK_estado_turno CHECK (estado IN ('pendiente','confirmado','cancelado','asistido'))
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
    FOREIGN KEY (id_turno) REFERENCES Turno(id_turno)
);
GO

------------------------------------------------------------
-- FIN DE SCRIPT
------------------------------------------------------------


------------------------------------------------------------
--SCRIPT DE DATOS DE PRUEBA
--------------------------------------------------------------

------------------------------------------------------------
-- PERSONAS (Base para pacientes y profesionales)
------------------------------------------------------------
INSERT INTO Persona (nombre, apellido, dni, fecha_nacimiento, telefono, email, direccion)
VALUES 
('Laura', 'González', '32145678', '1980-05-12', '1134567890', 'laura.gonzalez@example.com', 'Av. Siempre Viva 123'),
('Carlos', 'Ramírez', '28987541', '1975-10-02', '1156782349', 'carlos.ramirez@example.com', 'Calle Sol 456'),
('Mariana', 'Torres', '33456789', '1988-03-22', '1167892345', 'mariana.torres@example.com', 'Calle Luna 789'),
('Julián', 'Pérez', '40123456', '1999-11-15', '1176543210', 'julian.perez@example.com', 'Mitre 234'),
('Valeria', 'Suárez', '39234567', '1995-07-30', '1187654321', 'valeria.suarez@example.com', 'Rivadavia 1000'),
('Tomás', 'Fernández', '37890123', '2001-09-05', '1198765432', 'tomas.fernandez@example.com', 'Belgrano 900');
GO

------------------------------------------------------------
-- PROFESIONALES (subtipo de persona)
-- (Los primeros 3 registros de persona serán médicos)
------------------------------------------------------------
INSERT INTO Profesional (id_profesional, matricula)
VALUES 
(1, 'MAT-1001'),
(2, 'MAT-1002'),
(3, 'MAT-1003');
GO

------------------------------------------------------------
-- PACIENTES (subtipo de persona)
-- (Los últimos 3 registros de persona serán pacientes)
------------------------------------------------------------
INSERT INTO ObraSocial (nombre, porcentaje_cobertura, telefono, direccion) VALUES
('OSDE', 70, '0810-555-6733', 'Av. Libertador 1001'),
('IOMA', 50, '0810-222-4662', 'Calle Salud 520'),
('Swiss Medical', 80, '0810-345-7890', 'Av. Médica 900');
GO

INSERT INTO Paciente (id_paciente, id_obra_social, nro_afiliado)
VALUES
(4, 1, 'OSDE-20001'),
(5, 2, 'IOMA-45521'),
(6, 3, 'SM-89012');
GO

------------------------------------------------------------
-- ESPECIALIDADES
------------------------------------------------------------
INSERT INTO Especialidad (nombre, descripcion)
VALUES
('Clínica Médica', 'Atención general de salud'),
('Cardiología', 'Especialista en sistema cardiovascular'),
('Pediatría', 'Atención de niños y adolescentes'),
('Dermatología', 'Tratamiento de la piel');
GO

------------------------------------------------------------
-- PROFESIONAL x ESPECIALIDAD + VALOR CONSULTA
------------------------------------------------------------
INSERT INTO Profesional_Especialidad (id_profesional, id_especialidad, valor_consulta)
VALUES
(1, 1, 8000),  -- Laura González - Clínica Médica
(1, 4, 9000),  -- Laura González - Dermatología
(2, 2, 12000), -- Carlos Ramírez - Cardiología
(3, 3, 7000);  -- Mariana Torres - Pediatría
GO

------------------------------------------------------------
-- PROFESIONAL x OBRA SOCIAL (NUEVA TABLA)
------------------------------------------------------------
INSERT INTO Profesional_ObraSocial (id_profesional, id_obra_social)
VALUES
(1, 1), (1, 3),    -- Laura trabaja con OSDE y Swiss
(2, 2),            -- Carlos trabaja con IOMA
(3, 1), (3, 2);    -- Mariana trabaja con OSDE y IOMA
GO

------------------------------------------------------------
-- CONSULTORIOS
------------------------------------------------------------
INSERT INTO Consultorio (nombre, direccion, piso, numero_sala)
VALUES
('Consultorio Central', 'Av. Salud 1500', '1', '101'),
('Consultorio Norte', 'Av. Libertad 2500', '2', '205');
GO

------------------------------------------------------------
-- HORARIOS DE ATENCIÓN
------------------------------------------------------------
INSERT INTO HorarioAtencion (id_profesional, id_consultorio, dia_semana, hora_inicio, hora_fin)
VALUES
(1, 1, 'Lunes', '09:00', '13:00'),
(1, 1, 'Miércoles', '15:00', '18:00'),
(2, 2, 'Martes', '10:00', '14:00'),
(3, 1, 'Jueves', '08:00', '12:00');
GO

------------------------------------------------------------
-- TURNOS (ejemplos)
------------------------------------------------------------
INSERT INTO Turno (id_paciente, id_horario, id_especialidad, id_obra_social, fecha_turno, hora_turno, estado, monto_total)
VALUES
(4, 1, 1, 1, '2025-11-07', '09:30', 'confirmado', 8000),
(5, 3, 2, 2, '2025-11-08', '10:15', 'pendiente', NULL),
(6, 4, 3, 3, '2025-11-09', '08:45', 'asistido', 7000);
GO

------------------------------------------------------------
-- FACTURA (solo para el turno "asistido")
------------------------------------------------------------
INSERT INTO Factura (id_turno, monto_base, cobertura_aplicada, descuento_aplicado, monto_total)
VALUES
(3, 7000, 80, 0, 1400);
GO
