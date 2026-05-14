using MadeSalud.BD.Datos;
using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.IRepositorios;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;
using MadeSalud.Shared.ENUM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MadeSalud.Server.Controllers
{
    [ApiController]
    [Route("api/Paciente")]
    public class PacienteController : ControllerBase
    {

        private readonly IPacienteRepositorio repositorio;
        private readonly AppDbContext context;
        public PacienteController(IPacienteRepositorio repositorio, AppDbContext context)

        {
            this.repositorio = repositorio;
            this.context = context;
        }

        [HttpGet] //api/Paciente
        public async Task<ActionResult<List<Paciente>>> GetList()
        {
            var lista = await repositorio.Select();
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen items en la lista en este momento");
            }

            return Ok(lista);
        }


        [HttpGet("Id/{id:int}")] //api/Paciente/Id/5
        public async Task<ActionResult<Paciente>> GetById(int id)
        {
            var entidad = await repositorio.SelectById(id);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(entidad);
        }




        [HttpGet("ListaPaciente/{personaId:int}")] //api/Paciente/ListaPaciente/5
        public async Task<ActionResult<List<PacienteListadoDTO>>> GetListaPaciente(int personaId)
        {
            var lista = await repositorio.SelectListaPaciente(personaId);
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen items en la lista en este momento");
            }
            return Ok(lista);
        }

        [HttpGet("ListaPacientes")]
        public async Task<ActionResult<List<PacienteListadoDTO>>> GetListaPacientes()
        {
            var lista = await repositorio.SelectListaPacientes();
            return Ok(lista);
        }

        [HttpPost] //api/Paciente
        public async Task<ActionResult<int>> Post(PacienteCrearDTO DTO)
        {
            try
            {
                Paciente entidad = new Paciente
                {
                    PersonaId = DTO.PersonaId,
                    ObraSocial = DTO.ObraSocial,
                    MotivoConsulta = DTO.MotivoConsulta,
                    EstadoRegistro = EnumEstadoRegistro.Activo  


                };
                var id = await repositorio.Insert(entidad);
                return Ok(entidad.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el nuevo registro: {e.Message}");
            }
        }


        [HttpPut("{id:int}")] //api/Paciente/5
        public async Task<ActionResult> Put(int id, Paciente DTO)
        {
            var resultado = await repositorio.Update(id, DTO);
            if (!resultado)
            {
                return BadRequest("Datos no validos");
            }
            return Ok($"El registro con el id: {id} fue actualizado correctamente.");
        }

        [HttpDelete("{id:int}")] //api/Paciente/5
        public async Task<ActionResult> Delete(int id)
        {

            var resultado = await repositorio.Delete(id);
            if (!resultado)
            {
                return BadRequest("Datos no validos");
            }
            return Ok($"El registro con el id: {id} fue eliminado correctamente.");
        }
    }
}
