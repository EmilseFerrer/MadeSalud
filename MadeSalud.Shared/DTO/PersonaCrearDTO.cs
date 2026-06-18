using MadeSalud.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.Shared.DTO
{
    public class PersonaCrearDTO
    {



        [MaxLength(20, ErrorMessage = "Maximo {1} caracteres")]
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; } = "";

        [MaxLength(20, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string Apellido { get; set; } = "";

        [MaxLength(8, ErrorMessage = "Máximo {1} caracteres")]
        [MinLength(7, ErrorMessage = "Mínimo {1} caracteres")]
        [Required(ErrorMessage = "El DNI es obligatorio")]
        //[RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe contener solo números y tener 7 u 8 dígitos.")]
        public string DNI { get; set; }= string.Empty;

        [MaxLength(10, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        public string Telefono { get; set; } = "";

        [MaxLength(30, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "La Dirección es obligatoria")]
        public string Direccion { get; set; } = "";

        [MaxLength(1, ErrorMessage = "Máximo {1} caracter")]
        [Required(ErrorMessage = "El Sexo es obligatorio.")]
        [RegularExpression("^[FMO]$", ErrorMessage = "El sexo debe ser 'F' (Femenino)," +
                                        "'M' (Masculino),'O' (Otro).")]
        public string Sexo { get; set; } = "";


        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El ROL es obligatorio")]
        public RolEnum? Rol { get; set; }

        public string? MatriculaProfesional { get; set; }

        public string? ObraSocial { get; set; }

        public string? MotivoConsulta { get; set; }

    }
}
