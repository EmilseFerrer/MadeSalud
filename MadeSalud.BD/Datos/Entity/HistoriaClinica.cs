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
    [Index(nameof(NHC), Name = "NHC_UQ", IsUnique = true)]
    public class HistoriaClinica : EntityBase
    {
        public int NHC { get; set; } 

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

       
        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        public  required DateTime FechaCreacion { get; set; } = DateTime.Now;


    }
}
