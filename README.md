Recursos necesarios para que ejecute el programa:
1. Cambiar la ruta de conexion de la base de datos en appsettings.json.
2. Ejecutar el script de la bd para que funcione la apliacion (Esta mas abajo).
3. Cambiar el puerto de conexion segun sea necesario para que funcione el token (Ejemplo: https://localhost:7247/  cambiar 7247).


Funcionamiento de la aplicacion:
1. Primero tienes que ejecutar POST /api/auth/login con el ususario: admin y contraseña: admin, lo cual nos dara un token.
   Ejemplo:
   {
  "userId": 0,
  "userName": "admin",
  "password": "admin"
   }
2. En la parte superior derecha de POST /api/auth/login existe un boton, el cual daremos click, con el nombre de 'Authorization' en el cual ingresaremos ese token
   con la palabra reservada 'Bearer' de la siguiente manera Contraseña: Bearer "Aqui ingresa el tokem sin las comillas"
   Ejemeplo:
   Value: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6IjdmYjEwNmMxLTdhZjctNDQzZi1iNzg1LTQyNzNkMWI3MTk4NSIsImV4cCI6MTcyMTU4NjYwMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI0Ny8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MjQ3LyJ9.yFAoEdP91cOHYMJ9oG50cAxPAN3OSggV34tqFVbKP3I
3. Ya puedes usar los diferentes endpoints de la funcion, estan datos de ejemplo que se puede ver en el script de la bd para probar los endpoints.


Script de la BD
Create database SagatSalesDb
go

use SagatSalesDb
go

-- Crear la tabla User
CREATE TABLE [dbo].[Logins] (
    [UserID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserName] NVARCHAR(50) NOT NULL,
    [Password] NVARCHAR(50) NOT NULL
);

-- Crear la tabla Task
CREATE TABLE [dbo].[Tasks] (
    [TaskID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Title] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX),
    [IsCompleted] BIT NOT NULL DEFAULT 0,
    [UserID] INT NOT NULL
);

-- Crear la tabla Comment
CREATE TABLE [dbo].[Comments] (
    [CommentID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [TaskID] INT NOT NULL,
    [ParentCommentID] INT NULL,
    [CommentText] NVARCHAR(MAX) NOT NULL,
    [IsUpdated] BIT NOT NULL DEFAULT 0
);


-- Insertar datos en la tabla User
INSERT INTO [dbo].[Logins] ([UserName], [Password])
VALUES
('admin', 'admin'),
('user1', 'password1'),
('user2', 'password2');

-- Insertar datos en la tabla Task
INSERT INTO [dbo].[Tasks] ([Title], [Description], [IsCompleted], [UserID])
VALUES
('Task 1', 'Description for Task 1', 0, 1),
('Task 2', 'Description for Task 2', 0, 2),
('Task 3', 'Description for Task 3', 1, 1);

-- Insertar datos en la tabla Comment
INSERT INTO [dbo].[Comments] ([TaskID], [ParentCommentID], [CommentText], [IsUpdated])
VALUES
(1, NULL, 'This is a comment for Task 1', 0),
(1, 1, 'This is a reply to the first comment', 0),
(2, NULL, 'This is a comment for Task 2', 0);

-- Crear la relación entre Task y User
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT FK_Tasks_Logins
FOREIGN KEY ([UserID]) REFERENCES [dbo].[Logins]([UserID]);

-- Crear la relación entre Comment y Task
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT FK_Comments_Tasks
FOREIGN KEY ([TaskID]) REFERENCES [dbo].[Tasks]([TaskID]);

-- Crear la relación entre Comment y Comment (para comentarios anidados)
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT FK_Comments_ParentComment
FOREIGN KEY ([ParentCommentID]) REFERENCES [dbo].[Comments]([CommentID]);
