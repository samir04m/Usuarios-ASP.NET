using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Usuarios.Models;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Usuarios.Areas.Usuario.Models;
using Usuarios.Library;
using Usuarios.Areas.Principal.Controllers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Usuarios.Data;

namespace Usuarios.Controllers
{
    public class HomeController : Controller
    {
        //IServiceProvider _serviceProvider;
        private SignInManager<IdentityUser> _signInManager;
        private static LoginModel _model = null;
        private LUsuario _usuario;

        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IServiceProvider serviceProvider)
        {
            //_serviceProvider = serviceProvider;
            _signInManager = signInManager;
            _usuario = new LUsuario(userManager, signInManager, roleManager, context);
        }

        public async Task<IActionResult> Index()
        {
            //throw new Exception("This is some exception!!!");
            //await CreateRolesAsync(_serviceProvider);
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(PrincipalController.Principal), "Principal");
            }
            else
            {
                if (_model == null)
                {
                    return View();
                }
                else
                {
                    return View(_model);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _usuario.UserLoginAsync(model);
                if (result.Succeeded)
                {
                    //HttpContext.Session.SetString("User", "Samir");
                    //HttpContext.Session.SetInt32("Age", 22);
                    //var user = new TUsers
                    //{
                    //    Name = "Samir",
                    //    LastName = "Mejia",
                    //};
                    //var data = JsonConvert.SerializeObject(user);
                    //HttpContext.Session.SetString("User", data);

                    model.ErrorMessage = null;
                    model.Input.Email = null;
                    _model = model;
                    return Redirect("/Principal/Principal");
                }
                else if (result.IsLockedOut)
                {
                    model.ErrorMessage = "Cuenta de usuario bloqueada.";
                    _model = model;
                    return Redirect("/");
                }
                else
                {
                    model.ErrorMessage = "Correo o contraseña inválidos.";
                    _model = model;
                    return Redirect("/");
                }
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            ErrorViewModel error = null;
            if (statusCode != null) 
            {
                error = new ErrorViewModel
                {
                    RequestId = Convert.ToString(statusCode),
                    ErrorMessage = "Se produjo un error al procesar su solicitud",

                };
            }
            else
            {
                var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionFeature != null)
                {
                    error = new ErrorViewModel
                    {
                        RequestId = "500",
                        ErrorMessage = exceptionFeature.Error.Message,
                    };
                }
            }
            return View(error);
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] rolesName = { "Admin", "User" };
            foreach (var item in rolesName)
            {
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }

    }
}
