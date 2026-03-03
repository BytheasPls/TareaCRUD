using Microsoft.AspNetCore.Mvc;
using CRUDwithPatrones.Data;
using CRUDwithPatrones.Models;

namespace CRUDwithPatrones.Controllers
{
    public class MantController : Controller
    {
        peliculasDatos _PeliculasDatos = new peliculasDatos();
        public IActionResult Listar()
        {
            var plista = _PeliculasDatos.Listar();
            return View(plista);
        }

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(PeliculasModel oPeliculas)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = _PeliculasDatos.GuardarPelicula(oPeliculas);
            if(respuesta) 
                return RedirectToAction("Listar"); 
            else 
                return View();
        }


        peliculasDatos _peliculaDatos = new peliculasDatos();

        public IActionResult Editar(int idPelicula)
        {
            var oPelicula = _peliculaDatos.ObtenerPelicula(idPelicula);
            return View(oPelicula);
        }


        [HttpPost]
        public IActionResult Editar(PeliculasModel oModelo)
        {
            bool respuesta = _peliculaDatos.EditarPelicula(oModelo);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int idPelicula)
        {
            var respuesta = _peliculaDatos.EliminarPelicula(idPelicula);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
