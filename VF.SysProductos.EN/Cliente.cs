using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.SysProductos.EN
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del cliente es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string Apellido { get; set; }

        [StringLength(50, ErrorMessage = "El correo electrónico no puede tener más de 50 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Email { get; set; }

        [StringLength(10, ErrorMessage = "El teléfono debe tener 10 caracteres.")]
        public string Telefono { get; set; }

        [StringLength(9, ErrorMessage = "El DUI debe tener 9 caracteres.")]
        public string DUI { get; set; }
    }
}
