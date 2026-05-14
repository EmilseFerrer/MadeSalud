using MadeSalud.BD.Datos;
using MadeSalud.BD.Datos.Entity;

namespace MadeSalud.Repositorio.Repositorios
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        Task<bool> Existe(int id);
        Task<int> Insert(E entidad);
        Task<List<E>> Select();
        Task<E?> SelectById(int id);
        Task<bool> Update(int id, E entidad);
        Task<bool> Delete(int id);
    }
}