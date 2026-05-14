using MadeSalud.BD.Datos;
using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.IRepositorios;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;
using MadeSalud.Shared.ENUM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MadeSalud.Server.Controllers
{
    [Authorize(Roles = "Secretaria")]
    [ApiController]
    [Route("api/Persona")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepositorio repositorio;
        private readonly AppDbContext context;

        public PersonaController(IPersonaRepositorio repositorio, AppDbContext context)
        {
            this.repositorio = repositorio;
            this.context = context;
        }

        [HttpGet] //api/Persona
        public async Task<ActionResult<List<Persona>>> GetList()
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

        [HttpGet("Id/{id:int}")] //api/Persona/Id/5
        public async Task<ActionResult<Persona>> GetById(int id)
        {
            var entidad = await repositorio.SelectById(id);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(entidad);
        }

        [HttpGet("Dni/{dni}")] //api/Persona/Dni/12345678
        public async Task<ActionResult<Persona>> GetByDni(string dni)
        {
            var entidad = await repositorio.SelectByDni(dni);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con el DNI: {dni}.");
            }

            return Ok(entidad);
        }

        [HttpGet("ListaPersona")]
        [AllowAnonymous]
        public async Task<ActionResult<List<PersonaListadoDTO>>> GetListaPersona()
        {
            var lista = await repositorio.SelectListaPersona();

            if (lista == null)
                return NotFound("No se encontro la lista, VERIFICAR.");

            return Ok(lista);
        }

        [HttpGet("ListaPacientes")]
        [AllowAnonymous]
        public async Task<ActionResult<List<PacienteListadoDTO>>> GetListaPacientes()
        {
            var lista = await repositorio.SelectListaPacientes();

            if (lista == null)
                return NotFound("No se encontró la lista de pacientes.");

            return Ok(lista);
        }

        [HttpGet("ListaMedicos")] // api/Persona/ListaMedicos
        [AllowAnonymous]
        public async Task<ActionResult<List<MedicoListadoDTO>>> GetListaMedicos()
        {
            var lista = await repositorio.SelectListaMedicos();

            if (lista == null)
                return NotFound("No se encontró la lista de médicos.");

            return Ok(lista);
        }

        [HttpGet("ListaSecretarias")] // api/Persona/ListaSecretarias
        [AllowAnonymous]
        public async Task<ActionResult<List<SecretariaListadoDTO>>> GetListaSecretarias()
        {
            var lista = await repositorio.SelectListaSecretarias();

            if (lista == null)
                return NotFound("No se encontró la lista de secretarias.");

            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(PersonaCrearDTO DTO)
        {
            if (DTO.Rol == null)
            {
                return BadRequest("Debe seleccionar un rol.");
            }

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                Persona entidad = new Persona
                {
                    Nombre = DTO.Nombre,
                    Apellido = DTO.Apellido,
                    DNI = DTO.DNI,
                    Telefono = DTO.Telefono,
                    Direccion = DTO.Direccion,
                    Sexo = DTO.Sexo,
                    FechaNacimiento = DTO.FechaNacimiento,
                    Rol = DTO.Rol.Value,
                    EstadoRegistro = EnumEstadoRegistro.Activo
                };

                context.Personas.Add(entidad);
                await context.SaveChangesAsync();

                if (DTO.Rol.Value == RolEnum.Secretaria)
                {
                    int cantidadSecretarias = await context.Secretarias.CountAsync();

                    var secretaria = new Secretaria
                    {
                        PersonaId = entidad.Id,
                        NLegajo = $"LEG-{cantidadSecretarias + 1:0000}"
                    };

                    context.Secretarias.Add(secretaria);
                    await context.SaveChangesAsync();
                }
                else if (DTO.Rol.Value == RolEnum.Medico)
                {
                    if (string.IsNullOrWhiteSpace(DTO.MatriculaProfesional))
                    {
                        return BadRequest("Debe ingresar la matrícula profesional del médico.");
                    }

                    var medico = new Medico
                    {
                        PersonaId = entidad.Id,
                        MatriculaProfesional = DTO.MatriculaProfesional
                    };

                    context.Medicos.Add(medico);
                    await context.SaveChangesAsync();
                }
                else if (DTO.Rol.Value == RolEnum.Paciente)
                {
                    var paciente = new Paciente
                    {
                        PersonaId = entidad.Id,
                        ObraSocial = string.IsNullOrWhiteSpace(DTO.ObraSocial) ? "Sin obra social" : DTO.ObraSocial,
                        MotivoConsulta = string.IsNullOrWhiteSpace(DTO.MotivoConsulta) ? "Sin motivo" : DTO.MotivoConsulta
                    };

                    context.Pacientes.Add(paciente);
                    await context.SaveChangesAsync();

                    int ultimoNHC = await context.HistoriasClinicas
                        .Select(h => (int?)h.NHC)
                        .MaxAsync() ?? 0;

                    var historiaClinica = new HistoriaClinica
                    {
                        PacienteId = paciente.Id,
                        NHC = ultimoNHC + 1,
                        FechaCreacion = DateTime.Now
                    };

                    context.HistoriasClinicas.Add(historiaClinica);
                    await context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Rol no válido.");
                }

                await transaction.CommitAsync();

                return Ok(entidad.Id);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Error al crear el nuevo registro: {e.Message}");
            }
        }

        [HttpPut("{id:int}")] //api/Persona/5
        public async Task<ActionResult> Put(int id, PersonaCrearDTO dto)
        {
            // Convertir el DTO a entidad
            var entidad = new Persona
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                DNI = dto.DNI,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Sexo = dto.Sexo,
                FechaNacimiento = dto.FechaNacimiento,
                Rol = dto.Rol.Value
                // Agrega otros campos si existen en la entidad
            };

            var resultado = await repositorio.Update(id, entidad);
            if (!resultado)
            {
                return BadRequest("Datos no válidos");
            }
            return Ok($"El registro con el id: {id} fue actualizado correctamente.");
        }

        [HttpDelete("{id:int}")] //api/Persona/5
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
