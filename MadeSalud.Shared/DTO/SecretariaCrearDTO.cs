using MadeSalud.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MadeSalud.Shared.DTO
{
    public class SecretariaCrearDTO 
    {
        public string NLegajo { get; set; }

        public int PersonaId { get; set; }
    }
}
