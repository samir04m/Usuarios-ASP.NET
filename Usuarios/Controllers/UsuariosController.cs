using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Usuarios.Controllers
{
    //[Route("/users")]
    public class UsuariosController : Controller
    {
        //[HttpGet]
        //[Route("/users/home")]
        //[Route("/users/profile/{name}")]
        public IActionResult Index(int data)
        {
            var url = Url.RouteUrl("User", new {age=22, name="Samir" });
            return Redirect(url);
        }

        [HttpGet("[controller]/[action]", Name = "User")]
        public IActionResult Metodo(int code)
        {
            var data = $"Codigo de estado {code}";
            return View("Index", data);
        }
    }
}