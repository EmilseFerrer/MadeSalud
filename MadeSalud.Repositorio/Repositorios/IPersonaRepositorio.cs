using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;

namespace MadeSalud.Repositorio.IRepositorios
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        
        Task<Persona?> SelectByDni(string DNI);

        Task<List<PersonaListadoDTO>> SelectListaPersona();
        Task<List<PacienteListadoDTO>> SelectListaPacientes();
        Task<List<MedicoListadoDTO>> SelectListaMedicos();
        Task<List<SecretariaListadoDTO>> SelectListaSecretarias();

    }
}