CREATE DATABASE ExamenFinal
GO

USE ExamenFinal
GO


CREATE TABLE tb_paises
(idpais CHAR(3) PRIMARY KEY,
nompais VARCHAR(50)
)
GO


INSERT INTO tb_paises VALUES('PER','PERU')
INSERT INTO tb_paises VALUES('CHI','CHILE')
INSERT INTO tb_paises VALUES('ARG','ARGENTINA')
GO


CREATE TABLE tb_consumidor
( dnicont CHAR(8) PRIMARY KEY,
 nomcont VARCHAR(40) NOT NULL,
 apecont VARCHAR(40) NOT NULL,
 emailcont VARCHAR(250) UNIQUE,
 idpais CHAR(3),
 FOREIGN KEY(idpais)
 REFERENCES tb_paises(idpais))
GO


CREATE OR ALTER PROC usp_agrega_consumidor
(@dnicont CHAR(8),
@nomcont VARCHAR(40),
@apecont VARCHAR(40),
@emailcont VARCHAR(250),
 @idpais CHAR(3),
 @mensaje VARCHAR(100) OUTPUT)
AS
BEGIN
IF LEN(@dnicont) <> 8
BEGIN
SET @mensaje='DNI debe tener 8 digitos'RETURN
END
IF ISNUMERIC(@dnicont)=0
 BEGIN
 SET @mensaje='DNI debe ser numerico' RETURN
END
IF @nomcont=''
 BEGIN
 SET @mensaje='Nombre obligatorio'RETURN
END
IF @apecont=''
BEGIN
 SET @mensaje='Apellido obligatorio' RETURN
 END
 
 INSERT INTO tb_consumidor VALUES( @dnicont,@nomcont,@apecont,@emailcont,@idpais)
SET @mensaje='Consumidor registrado correctamente'
END
GO
---
CREATE OR ALTER PROC usp_eliminar_consumidor( @dnicont CHAR(8))
AS
BEGIN
IF EXISTS
( SELECT *FROM tb_consumidor WHERE dnicont=@dnicont)
BEGIN
DELETE FROM tb_consumidor WHERE dnicont=@dnicont
END
ELSE
BEGIN
PRINT 'Consumidor no existe'
END
END
GO
---
CREATE OR ALTER PROC usp_listar_consumidor
AS
BEGIN
SELECT *FROM tb_consumidor
END
GO



--AÑADIR NUEVO DATOS(PRUEBA)
DECLARE @mensaje VARCHAR(100)
EXEC usp_agrega_consumidor'22222222','PEPITO','SANCHEZ','SANCHEZ@gmail.com','CHIL',
@mensaje OUTPUT
SELECT @mensaje

SELECT * FROM tb_consumidor

SELECT * FROM tb_paises