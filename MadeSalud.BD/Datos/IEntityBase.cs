using MadeSalud.Shared.ENUM;

namespace MadeSalud.BD.Datos
{
    public interface IEntityBase
    {
        public int Id { get; set; }
        public EnumEstadoRegistro EstadoRegistro { get; set; }
        public string Observacion { get; set; }
    }
}
