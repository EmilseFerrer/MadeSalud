using MadeSalud.Shared.ENUM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    [Index(nameof(DNI), Name = "DNI_UQ", IsUnique = true)]
    public class Persona : EntityBase        
    {   

        [MaxLength(20, ErrorMessage = "Maximo {1} caracteres")]
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public required string Nombre { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public required string Apellido { get; set; }

        [MaxLength(8, ErrorMessage = "Máximo {1} caracteres")]
        [MinLength(7, ErrorMessage = "Mínimo {1} caracteres")]
        [Required(ErrorMessage = "El DNI es obligatorio")]
        [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe contener solo números y tener 7 u 8 dígitos.")]
        public required string DNI { get; set; }

        [MaxLength(10, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        public required string Telefono { get; set; }

        [MaxLength(30, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "La Dirección es obligatoria")]
        public required string Direccion { get; set; }

        [MaxLength(1, ErrorMessage = "Máximo {1} caracter")]
        [Required(ErrorMessage = "El Sexo es obligatorio.")]
        [RegularExpression("^[FM]$", ErrorMessage = "El sexo debe ser 'F' (Femenino) o 'M' (Masculino).")]
        public required string Sexo { get; set; }

        
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime FechaNacimiento { get; set; }

        public RolEnum Rol { get; set; } //Paciente=1, Medico=2, Secretaria=3


    }
}
