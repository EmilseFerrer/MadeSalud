using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    public class Receta : EntityBase
    {

        [MaxLength(100)]
        public required string Dosis { get; set; }

        [MaxLength(100)]
        public required string Frecuencia { get; set; }

        public int ConsultaMedicaId { get; set; }
        public  ConsultaMedica? ConsultaMedica { get; set; }

        public int MedicamentoId { get; set; }
        public  Medicamento? Medicamento { get; set; }
    }
}
