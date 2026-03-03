using CRUDwithPatrones.Models;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CRUDwithPatrones.Data;
using Microsoft.AspNetCore.Mvc;

namespace CRUDwithPatrones.Data
{
    public class peliculasDatos
    {
        public List<PeliculasModel> Listar()
        {
            var plista = new List<PeliculasModel>();
            using (SqlConnection conexion = SingletonDBcnx.Instance.ObtenerInstancia())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarPeliculas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader()) {
                    while (dr.Read()) {
                        plista.Add(new PeliculasModel { 
                            IdPelicula = Convert.ToInt32(dr["idPelicula"]),
                            NombrePelicula = dr["nombrePelicula"].ToString(),
                            Sinopsis = dr["sinopsis"].ToString(),
                            FechaLanzamiento = DateOnly.FromDateTime(Convert.ToDateTime(dr["fechaLanzamiento"])),
                        });
                    }
                }
            }
            return plista;
        }



        public PeliculasModel ObtenerPelicula(int idPelicula)
        {
            var oPeliculas = new PeliculasModel();
            using (SqlConnection conexion = SingletonDBcnx.Instance.ObtenerInstancia())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerPelicula", conexion);
                cmd.Parameters.AddWithValue("idPelicula", idPelicula);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oPeliculas.IdPelicula = Convert.ToInt32(dr["idPelicula"]);
                        oPeliculas.NombrePelicula = dr["nombrePelicula"].ToString();
                        oPeliculas.Sinopsis = dr["sinopsis"].ToString();
                        oPeliculas.FechaLanzamiento = DateOnly.FromDateTime(Convert.ToDateTime(dr["fechaLanzamiento"]));
                    }
                }
            }
            return oPeliculas;
        }




        public bool GuardarPelicula(PeliculasModel oPeliculas)
        {
            bool respt;
            try
            {
                using (SqlConnection conexion = SingletonDBcnx.Instance.ObtenerInstancia())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_CrearPelicula", conexion);

                    cmd.Parameters.AddWithValue("nombrePelicula", oPeliculas.NombrePelicula);
                    cmd.Parameters.AddWithValue("sinopsis", oPeliculas.Sinopsis);
                    cmd.Parameters.AddWithValue("fechaLanzamiento", oPeliculas.FechaLanzamiento.ToDateTime(TimeOnly.MinValue));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    respt = true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                respt = false;
            }

            return respt;
        }



        public bool EditarPelicula(PeliculasModel oPeliculas)
        {
            bool respt;
            try
            {
                using (SqlConnection conexion = SingletonDBcnx.Instance.ObtenerInstancia())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarPelicula", conexion);
                    cmd.Parameters.AddWithValue("idPelicula", oPeliculas.IdPelicula);
                    cmd.Parameters.AddWithValue("nombrePelicula", oPeliculas.NombrePelicula);
                    cmd.Parameters.AddWithValue("sinopsis", oPeliculas.Sinopsis);
                    cmd.Parameters.AddWithValue("fechaLanzamiento", oPeliculas.FechaLanzamiento.ToDateTime(TimeOnly.MinValue));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    respt = true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                respt = false;
            }

            return respt;
        }



        public bool EliminarPelicula(int idPelicula)
        {
            bool respt;
            try
            {
                using (SqlConnection conexion = SingletonDBcnx.Instance.ObtenerInstancia())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarPelicula", conexion);
                    cmd.Parameters.AddWithValue("idPelicula", idPelicula);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    respt = true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                respt = false;
            }

            return respt;
        }
    }
}
