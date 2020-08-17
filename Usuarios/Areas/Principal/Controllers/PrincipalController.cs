using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Usuarios.Areas.Usuario.Models;
using Usuarios.Controllers;

namespace Usuarios.Areas.Principal.Controllers
{
    [Area("Principal")]
    [Authorize]
    public class PrincipalController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;

        public PrincipalController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
        }

        //[Authorize(Roles = "Admin, User")]
        //[Authorize(Policy = "Authorization")]
        public IActionResult Principal()
        {
            if (_signInManager.IsSignedIn(User))
            {
                //var user = HttpContext.Session.GetString("User");
                //var age = HttpContext.Session.GetInt32("Age");
                //var dataUser = JsonConvert.DeserializeObject<TUsers>(user);
                var user = User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Name)).Value;
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");

            }
        }
    }
}