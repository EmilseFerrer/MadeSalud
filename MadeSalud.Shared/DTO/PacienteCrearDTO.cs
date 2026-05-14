using MadeSalud.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.Shared.DTO
{
    public class PacienteCrearDTO 
    {
        
        

        [Required(ErrorMessage = "La obra social es obligatoria")]
        [MaxLength(30, ErrorMessage = "La cantidad máxima de caracteres es {1}")]
        public string ObraSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatori")]
        [MaxLength(100, ErrorMessage = "La cantidad máxima de caracteres es {1}")]
        public string MotivoConsulta { get; set; } = string.Empty;
        
        public int PersonaId { get; set; }

        public EnumEstadoRegistro EstadoRegistro { get; set; } = EnumEstadoRegistro.EnGrabacion;


    }
}
