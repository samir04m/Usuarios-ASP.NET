using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Usuarios.Areas.Usuario.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo apellido es obligatorio.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo nid es obligatorio.")]
        public string NID { get; set; }

        [Required(ErrorMessage = "El campo telefono es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "El formato telefono ingresado no es válido.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El campo email es obligatorio.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo password es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Seleccione un role.")]
        public string Role { get; set; }

    }
}
