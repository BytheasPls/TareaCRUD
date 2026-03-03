CREATE DATABASE CRUD;
GO

USE CRUD

CREATE TABLE peliculas (
	idPelicula int IDENTITY(1,1) PRIMARY KEY,
	nombrePelicula VARCHAR(100) NOT NULL UNIQUE,
	sinopsis text NOT NULL,
	fechaLanzamiento Date NOT NULL,
);




*LISTAR LAS PELICULAS*/
create procedure sp_ListarPeliculas
as 
begin 
	SELECT * FROM peliculas ORDER BY nombrePelicula ASC
end


/*OBTENER UNA PELICULA EN ESPECIFICO*/
create procedure sp_ObtenerPelicula(
	@idPelicula int
)
as
begin
	SELECT * FROM peliculas
	WHERE idPelicula = @idPelicula
end


/*AGREGAR UNA PELICULA*/
create procedure sp_CrearPelicula(
	@nombrePelicula VARCHAR(50),
	@sinopsis TEXT,
	@fechaLanzamiento DateTime
)
as
begin
	INSERT peliculas (nombrePelicula, sinopsis, fechaLanzamiento) 
	VALUES (@nombrePelicula, @sinopsis, @fechaLanzamiento)
end

/*ACTUALIZAR UNA PELICULA*/
create procedure sp_ActualizarPelicula(
	@idPelicula INT,
	@nombrePelicula VARCHAR(50),
	@sinopsis TEXT,
	@fechaLanzamiento DateTime
)
as
begin
	UPDATE peliculas SET nombrePelicula = @nombrePelicula, sinopsis = @sinopsis, fechaLanzamiento = @fechaLanzamiento 
	WHERE idPelicula = @idPelicula
end

/*ELIMINAR UNA PELICULA*/
create procedure sp_EliminarPelicula(
	@idPelicula INT
)
as 
begin
	DELETE FROM peliculas WHERE idPelicula = @idPelicula
end