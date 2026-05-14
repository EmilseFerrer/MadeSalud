using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    public class ConsultaMedica : EntityBase
    {
        public int HistoriaClinicaId { get; set; }
        public HistoriaClinica? HistoriaClinica { get; set; }

        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }

        public int SecretariaId { get; set; }
        public Secretaria? Secretaria { get; set; }

       
        
        [Required]
        public DateTime FechaConsulta { get; set; }= DateTime.Now;

        [Required(ErrorMessage = "El diagnóstico es obligatorio")]
        [MaxLength(100, ErrorMessage = "El diagnóstico no puede exceder los {1} caracteres.")]
        public string Diagnostico { get; set; } = string.Empty;

        [Range(30, 250)]
        public int FrecuenciaCardiaca { get; set; }

        [RegularExpression(@"^\d{2,3}/\d{2,3}$")]
        [MaxLength(7)]
        public string PresionArterial { get; set; } = "120/80";
        
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 999.99)]
        public decimal PesoCorporal { get; set; }

        public ICollection<Receta> Recetas { get; set; } = new List<Receta>();

    }
}
