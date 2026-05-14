using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;


namespace MadeSalud.Repositorio.Repositorios
{
    public interface IMedicoRepositorio : IRepositorio<Medico>
    {
        Task<Medico?> SelectByMatricula(string cod);
        Task<List<MedicoListadoDTO>> SelectListaMedicos(); 
        Task<List<MedicoListadoDTO>> SelectListaMedico(int personaId);

    }
}