using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.IRepositorios;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;



namespace MadeSalud.Server.Controllers
{
    [Authorize(Roles = "Secretaria,Medico")]
    [ApiController]
    [Route("api/Medicamento")]
    public class MedicamentoController : ControllerBase
    {
        private readonly IMedicamentoRepositorio repositorio;

        public MedicamentoController(IMedicamentoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [AllowAnonymous]
        [HttpGet("Lista")]
        [OutputCache(Duration = 60)]
        public async Task<ActionResult<List<MedicamentoListadoDTO>>> GetLista()
        {
            var lista = await repositorio.SelectLista();
            return Ok(lista);
        }

        [Authorize(Roles = "Secretaria")]
        [HttpPost]
        public async Task<ActionResult<int>> Post(MedicamentoCrearDTO dto)
        {
            try
            {
                var medicamento = new Medicamento
                {
                    Codigo = dto.Codigo,
                   
                    Presentacion = dto.Presentacion,
                    PrecioUnitario = dto.PrecioUnitario,

                    Precio15 = dto.PrecioUnitario * 0.85m,
                    Precio25 = dto.PrecioUnitario * 0.75m,
                    PrecioAmaCasa30 = dto.PrecioUnitario * 0.70m,

                   
                };

                var id = await repositorio.Insert(medicamento);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear medicamento: {e.InnerException?.Message ?? e.Message}");
            }
        }
        [Authorize(Roles = "Secretaria")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, MedicamentoCrearDTO dto)
        {
            var medicamento = await repositorio.SelectById(id);

            if (medicamento == null)
                return NotFound("No se encontró el medicamento.");

            medicamento.Codigo = dto.Codigo;
            medicamento.Presentacion = dto.Presentacion;
            medicamento.PrecioUnitario = dto.PrecioUnitario;
            medicamento.Precio15 = dto.PrecioUnitario * 0.85m;
            medicamento.Precio25 = dto.PrecioUnitario * 0.75m;
            medicamento.PrecioAmaCasa30 = dto.PrecioUnitario * 0.70m;

            var resultado = await repositorio.Update(id, medicamento);

            if (!resultado)
                return BadRequest("No se pudo modificar el medicamento.");

            return Ok("Medicamento modificado correctamente.");
        }

        [Authorize(Roles = "Secretaria")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await repositorio.Delete(id);

            if (!resultado)
                return NotFound("No se encontró el medicamento.");

            return Ok("Medicamento eliminado correctamente.");
        }
    }
}
