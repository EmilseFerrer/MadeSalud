using MadeSalud.BD.Datos;
using MadeSalud.BD.Datos.Entity;
using MadeSalud.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace MadeSalud.Repositorio.Repositorios
{
    public class MedicamentoRepositorio : Repositorio<Medicamento>, IMedicamentoRepositorio
    {
        private readonly AppDbContext context;

        public MedicamentoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<MedicamentoListadoDTO>> SelectLista()
        {
            Console.WriteLine($"CONSULTANDO BD MEDICAMENTOS - {DateTime.Now:HH:mm:ss}");

            return await context.Medicamentos
                  .OrderBy(m => m.Codigo)
                  .Select(m => new MedicamentoListadoDTO
                  {
                      Id = m.Id,
                      Codigo = m.Codigo,
                      Presentacion = m.Presentacion,
                      PrecioUnitario = m.PrecioUnitario,
                      Precio15 = m.Precio15,
                      Precio25 = m.Precio25,
                      PrecioAmaCasa30 = m.PrecioAmaCasa30
                  })
        .ToListAsync();
        }
    }
}

