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
    [Route("api/Secretaria")]
    public class SecretariaController : ControllerBase
    {
        
        private readonly ISecretariaRepositorio repositorio;
        private readonly AppDbContext context;
        public SecretariaController(ISecretariaRepositorio repositorio, AppDbContext context)

        { 
            this.repositorio = repositorio;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Secretaria>>> GetList()
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


        [HttpGet("Id/{id:int}")]
        public async Task<ActionResult<Paciente>> GetById(int id)
        {
            var entidad = await repositorio.SelectById(id);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(entidad);
        }

        [HttpGet("NHC/{cod}")]
        public async Task<ActionResult<Secretaria>> GetByNLegajo(string cod)
        {
            var entidad = await repositorio.SelectByNLegajo(cod);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con el código: {cod}.");
            }

            return Ok(entidad);
        }




        [HttpGet("ListaSecretaria/{personaId:int}")]
        public async Task<ActionResult<List<SecretariaListadoDTO>>> GetListaSecretaria(int personaId)
        {
            var lista = await repositorio.SelectListaSecretaria(personaId);
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

        [HttpGet("ListaSecretarias")]
        public async Task<ActionResult<List<SecretariaListadoDTO>>> GetListaSecretarias()
        {
            var lista = await repositorio.SelectListaSecretarias();
            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(SecretariaCrearDTO DTO)
        {
            try
            {
                Secretaria entidad = new Secretaria
                {
                    PersonaId = DTO.PersonaId,
                    NLegajo = DTO.NLegajo
                };

                await repositorio.Insert(entidad);
                return Ok(entidad.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el nuevo registro: {e.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Secretaria DTO)
        {
            var resultado = await repositorio.Update(id, DTO);
            if (!resultado)
            {
                return BadRequest("Datos no validos");
            }
            return Ok($"El registro con el id: {id} fue actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]
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
