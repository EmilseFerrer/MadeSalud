using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace MadeSalud.Shared.DTO
{
    public class MedicamentoCrearDTO
    {
        [Required(ErrorMessage = "El código es obligatorio")]
        [MaxLength(20)]
        public string Codigo { get; set; } = "";

        [Required]
        [MaxLength(80)]
        public string Presentacion { get; set; } = "";

        public decimal PrecioUnitario { get; set; }

       
    }
}