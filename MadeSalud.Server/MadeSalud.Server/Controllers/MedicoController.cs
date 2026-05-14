using MadeSalud.BD.Datos;
using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.IRepositorios;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace MadeSalud.Server.Controllers
{
    [ApiController]
    [Route("api/Medico")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepositorio repositorio;
        private readonly AppDbContext context;

        public MedicoController(IMedicoRepositorio repositorio, AppDbContext context)
        {
            this.repositorio = repositorio;
            this.context = context;
        }

       
            

        [HttpGet] //api/Medico
        public async Task<ActionResult<List<Medico>>> GetList()
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


        [HttpGet("Id/{id:int}")] //api/Medico/Id/5
        public async Task<ActionResult<Medico>> GetById(int id)
        {
            var entidad = await repositorio.SelectById(id);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(entidad);

        }

        [HttpGet("NMatricula/{cod}")] //api/Medico/NMatricula/ABC123
        public async Task<ActionResult<Medico>> GetByMatricula(string cod)
        {
            var entidad = await repositorio.SelectByMatricula(cod);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con el código: {cod}.");
            }

            return Ok(entidad);
        }




        [HttpGet("ListaMedico/{personaId:int}")] //api/Medico/ListaMedico/5
        public async Task<ActionResult<List<MedicoListadoDTO>>> GetListaMedico(int personaId)
        {
            var lista = await repositorio.SelectListaMedico(personaId);
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
        [HttpGet("ListaMedicos")]
        public async Task<ActionResult<List<MedicoListadoDTO>>> GetListaMedicos()
        {
            var lista = await repositorio.SelectListaMedicos();
            return Ok(lista);
        }

        [HttpPost] //api/Medico
        public async Task<ActionResult<int>> Post(MedicoCrearDTO DTO)
        {
            try
            {
                Medico entidad = new Medico
                {
                    MatriculaProfesional= DTO.MatriculaProfesional,
                    
                };
                var id = await repositorio.Insert(entidad);
                return Ok(entidad.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el nuevo registro: {e.Message}");
            }
        }


        [HttpPut("{id:int}")] //api/Medico/5
        public async Task<ActionResult> Put(int id, Medico DTO)
        {
            var resultado = await repositorio.Update(id, DTO);
            if (!resultado)
            {
                return BadRequest("Datos no validos");
            }
            return Ok($"El registro con el id: {id} fue actualizado correctamente.");
        }

        [HttpDelete("{id:int}")] //api/Medico/5
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