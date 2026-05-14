using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    public class Paciente : EntityBase
    {
        [Required(ErrorMessage = "La obra social es obligatoria")]
        [MaxLength(30, ErrorMessage = "La cantidad máxima de caracteres es {1}")]
        public required string ObraSocial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatori")]
        [MaxLength(100, ErrorMessage = "La cantidad máxima de caracteres es {1}")]
        public required string MotivoConsulta { get; set; } 
        
        public int PersonaId { get; set; }
        public Persona? Personas { get; set; }

        public List<HistoriaClinica> HistoriaClinicas { get; set; } = new();
    }
}
