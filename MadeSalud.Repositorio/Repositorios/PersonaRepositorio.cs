using MadeSalud.BD.Datos;
using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.IRepositorios;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;
using MadeSalud.Shared.ENUM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MadeSalud.Repositorio.Repositorios
{
    public class PersonaRepositorio : Repositorio<Persona>, IPersonaRepositorio
    {
        private readonly AppDbContext context;

        public PersonaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

       

        public async Task<Persona?> SelectByDni(string dni)
        {
            return await context.Set<Persona>().FirstOrDefaultAsync(x => x.DNI == dni);
        }


        public async Task<List<PersonaListadoDTO>> SelectListaPersona()
        {
            var lista = await context.Personas
                            .Select(p => new PersonaListadoDTO
                            {
                                Id = p.Id,
                                DatosPersona = $"{p.Nombre} {p.Apellido} -  DNI: {p.DNI} - {p.Sexo} - " +
                                $" Rol: {p.Rol}",
                            })
                            .ToListAsync();

            return lista;
        }
        public async Task<List<MedicoListadoDTO>> SelectListaMedicos()
        {
            return await context.Medicos
                .Include(m => m.Personas)
                .Select(m => new MedicoListadoDTO
                {
                    Id = m.PersonaId,
                    DatosMedico = $"{m.Personas!.Nombre} {m.Personas.Apellido} - DNI: {m.Personas.DNI} - Matrícula: {m.MatriculaProfesional}"
                })
                .ToListAsync();
        }

        public async Task<List<PacienteListadoDTO>> SelectListaPacientes()
        {
            return await context.Pacientes
                .Include(p => p.Personas)
                .Include(p => p.HistoriaClinicas)
                .Select(p => new PacienteListadoDTO
                {
                    Id = p.PersonaId,
                    DatosPaciente = $"{p.Personas!.Nombre} {p.Personas.Apellido} - DNI: {p.Personas.DNI} - NHC: {p.HistoriaClinicas.OrderBy(h => h.Id).Select(h => h.NHC).FirstOrDefault()}",
                    ObraSocial = p.ObraSocial,
                    MotivoConsulta = p.MotivoConsulta
                })
                .ToListAsync();
        }

        public async Task<List<SecretariaListadoDTO>> SelectListaSecretarias()
        {
            return await context.Secretarias
                .Include(s => s.Personas)
                .Select(s => new SecretariaListadoDTO
                {
                    Id = s.PersonaId,
                    DatosSecre = $"{s.Personas!.Nombre} {s.Personas.Apellido} - DNI: {s.Personas.DNI} - Legajo: {s.NLegajo}"
                })
                .ToListAsync();
        }

    }
}
