using System;
using System.Collections.Generic;
using System.Text;

namespace MadeSalud.Shared.DTO
{
    public class MedicamentoListadoDTO

    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Presentacion { get; set; } = "";

        public decimal PrecioUnitario { get; set; }

        public decimal Precio15 { get; set; }

        public decimal Precio25 { get; set; }

        public decimal PrecioAmaCasa30 { get; set; }

        
    }
}
