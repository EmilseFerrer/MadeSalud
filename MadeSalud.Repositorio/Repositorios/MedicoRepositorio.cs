using MadeSalud.BD.Datos;
using MadeSalud.BD.Datos.Entity;
using MadeSalud.Repositorio.IRepositorios;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MadeSalud.Repositorio.Repositorios
{
    public class MedicoRepositorio : Repositorio<Medico>, IMedicoRepositorio
    {
        private readonly AppDbContext context;
        public MedicoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }


        public async Task<Medico?> SelectByMatricula(string cod)
        {
            return await context.Set<Medico>().FirstOrDefaultAsync(x => x.MatriculaProfesional == cod);
        }

        public async Task<List<MedicoListadoDTO>> SelectListaMedico(int personaId)
        {
            var medicos = await context.Medicos
                .Include(m => m.Personas)
                .Where(m => m.PersonaId == personaId)
                .Select(m => new MedicoListadoDTO
                {
                    Id = m.PersonaId,
                    DatosMedico = $"{m.Personas!.Nombre} {m.Personas.Apellido} - DNI: {m.Personas.DNI} - N° Matrícula: {m.MatriculaProfesional}"
                })
                .ToListAsync();

            return medicos;
        }

        public async Task<List<MedicoListadoDTO>> SelectListaMedicos()
        {
            var medicos = await context.Medicos
                .Include(m => m.Personas)
                .Select(m => new MedicoListadoDTO
                {
                    Id = m.PersonaId,
                    DatosMedico = $"{m.Personas!.Nombre} {m.Personas.Apellido} - DNI: {m.Personas.DNI} - N° de Matrícula: {m.MatriculaProfesional}"
                })
                .ToListAsync();

            return medicos;
        }



    }
}