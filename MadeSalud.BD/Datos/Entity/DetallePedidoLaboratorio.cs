using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeSalud.BD.Datos.Entity
{
    public class DetallePedidoLaboratorio : EntityBase
    {
        [Range(0, 9999)]
        public int Cantidad { get; set; }

        public int MedicamentoId { get; set; }
        public Medicamento? Medicamento { get; set; }

        public int PedidoLaboratorioId { get; set; }
        public PedidoLaboratorio? PedidoLaboratorio { get; set; }

    }
}
