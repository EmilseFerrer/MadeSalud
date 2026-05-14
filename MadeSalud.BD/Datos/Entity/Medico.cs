
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    [Index(nameof(MatriculaProfesional), Name = "MP_UQ", IsUnique = true)]
    public class Medico : EntityBase
    {
        
        [Required(ErrorMessage = "El código de matrícula es obligatorio")]
        [MaxLength(10, ErrorMessage = "La cantidad máxima de caracteres es {1}")]
        public required string MatriculaProfesional { get; set; }

        public int PersonaId { get; set; }
        public Persona? Personas { get; set; }
    }
}
