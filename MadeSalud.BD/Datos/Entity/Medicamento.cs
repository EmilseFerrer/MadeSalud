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
        [Range(0, 999999)]
        public int Codigo { get; set; }

        [MaxLength(120)]
        public required string NombreFormula { get; set; }

    }
}
