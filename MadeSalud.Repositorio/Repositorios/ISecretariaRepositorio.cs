using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;

namespace MadeSalud.Repositorio.IRepositorios
{
    public interface ISecretariaRepositorio : IRepositorio<Secretaria>
    {
        Task<Secretaria?> SelectByNLegajo(string cod);
        
        Task<List<SecretariaListadoDTO>> SelectListaSecretaria(int personaId);

        Task<List<SecretariaListadoDTO>> SelectListaSecretarias();

    }
}