using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Usuarios.Areas.Principal.Controllers
{
    [Area("Principal")]
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
        [Authorize(Policy = "Authorization")]
        public IActionResult Principal()
        {
            //if (_signInManager.IsSignedIn(User))
            //{
            return View();
            //}
            //else
            //{
            //    return RedirectToAction(nameof(HomeController.Index), "Home");

            //}
        }
    }
}