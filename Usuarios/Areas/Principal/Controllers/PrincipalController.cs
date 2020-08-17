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
        private UserManager<IdentityUser> _userManager;

        public PrincipalController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //[Authorize(Roles = "Admin, User")]
        //[Authorize(Policy = "Authorization")]
        public async Task<IActionResult> Principal()
        {
            if (_signInManager.IsSignedIn(User))
            {
                //var user = HttpContext.Session.GetString("User");
                //var age = HttpContext.Session.GetInt32("Age");
                //var dataUser = JsonConvert.DeserializeObject<TUsers>(user);
                //var user = User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Name)).Value;

                var id = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(id);
                var roles = await _userManager.GetRolesAsync(user);
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");

            }
        }
    }
}