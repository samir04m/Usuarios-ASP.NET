using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usuarios.Areas.Usuario.Models;
using Usuarios.Data;
using Usuarios.Library;

namespace Usuarios.Areas.Usuario.Pages.Account
{
    public class DetailsModel : PageModel
    {
        private LUsuario _user;

        public DetailsModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _user = new LUsuario(userManager, signInManager, roleManager, context);
        }

        public void OnGet(int id)
        {
            var data = _user.getTUsuariosAsync(null, id);
            if (0 < data.Result.Count)
            {
                Input = new InputModel
                {
                    DataUser = data.Result.ToList().Last(),
                };
            }
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public InputModelRegister DataUser { get; set; }
        }
    }
}
