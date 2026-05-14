using MadeSalud.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MadeSalud.Shared.DTO
{
    public class MedicoCrearDTO 
    {
        

        [Required(ErrorMessage = "El código de matrícula es obligatorio")]
        [MaxLength(10, ErrorMessage = "La cantidad máxima de caracteres es {1}")]
        public string MatriculaProfesional { get; set; } = string.Empty;

        public int PersonaId { get; set; }


    }
}