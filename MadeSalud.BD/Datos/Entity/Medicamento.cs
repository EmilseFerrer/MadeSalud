using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    [Index(nameof(Codigo), Name = "CODMED_UQ", IsUnique = true)]
    public class Medicamento : EntityBase
    {
        [Required]
        [MaxLength(20)]
        public string Codigo { get; set; } = "";

        

        [Required]
        [MaxLength(80)]
        public string Presentacion { get; set; } = "";

        public decimal PrecioUnitario { get; set; }

        public decimal Precio15 { get; set; }

        public decimal Precio25 { get; set; }

        public decimal PrecioAmaCasa30 { get; set; }

       

    }
}
