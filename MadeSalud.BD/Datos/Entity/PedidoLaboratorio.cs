using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    public class PedidoLaboratorio : EntityBase
    {
        public int SecretariaId { get; set; }
        public Secretaria? Secretaria { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

    }
}
