using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.Shared.DTO
{
    public class PersonaListadoDTO 
    {
        public int Id { get; set; }
        //public string Nombre { get; set; } = string.Empty;
        //public string Apellido { get; set; } = string.Empty;
        //public string DNI { get; set; } = string.Empty;
        //public string Sexo { get; set; } = string.Empty;
        //public string Rol { get; set; } = string.Empty;
        public string DatosPersona { get; set; } = string.Empty; // Nombre, Apellido, DNI, Sexo, Rol

        public string? MatriculaProfesional { get; set; }   // solo médicos
        public string? NLegajo { get; set; }          // solo secretarias
        public int? NHC { get; set; }    // solo pacientes
    }
}
