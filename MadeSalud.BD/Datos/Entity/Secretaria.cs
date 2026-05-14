using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    public class Secretaria: EntityBase
    {
        [Required, MaxLength(20)]
        public string NLegajo { get; set; }= string.Empty;

        public int PersonaId { get; set; }
        public Persona? Personas { get; set; }
    }
}
