using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;


namespace MadeSalud.Repositorio.Repositorios
{
    public interface IMedicamentoRepositorio : IRepositorio<Medicamento>
    {
        Task<List<MedicamentoListadoDTO>> SelectLista();
        

    }
}