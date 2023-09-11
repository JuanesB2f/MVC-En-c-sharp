CREATE TABLE IF NOT EXISTS empleados (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cedula VARCHAR(15) NOT NULL,
    nombre_empleado VARCHAR(50) NOT NULL,
    apellido_empleado VARCHAR(50) NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    marca VARCHAR(50) NOT NULL,
    precio DOUBLE NOT NULL,
    FechaRegistro DATETIME
);