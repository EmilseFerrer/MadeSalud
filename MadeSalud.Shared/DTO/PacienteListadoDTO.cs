using MadeSalud.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.Shared.DTO
{
    public class PacienteListadoDTO
    {
        
        public int Id { get; set; }
        
        public string DatosPaciente { get; set; } = string.Empty;

        public  string ObraSocial { get; set; }= string.Empty;

        public string MotivoConsulta { get; set; } = string.Empty;



       
    }
}