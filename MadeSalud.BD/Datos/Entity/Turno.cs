using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    public class Turno: EntityBase
    {
            [Required(ErrorMessage = "La fecha y hora del turno es obligatoria")]
            public DateTime FechayHora { get; set; } = DateTime.Now;

            public int PacienteId { get; set; }

            public int MedicoId { get; set; }

            public Paciente? Paciente { get; set; }

            public Medico? Medico { get; set; }

            public List<ConsultaMedica> ConsultasMedicas { get; set; } = new List<ConsultaMedica>();
    }
}
