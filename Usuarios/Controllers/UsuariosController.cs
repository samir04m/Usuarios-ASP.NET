using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Usuarios.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index(int id, String name, int age)
        {
            // https://localhost:44371/Usuarios/Index?name=Samir&age=22

            ViewData["id"] = id;
            String datos = name + "  " + age;
            return View("Index", datos);
        }
    }
}