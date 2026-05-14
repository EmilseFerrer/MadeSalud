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
    public class SecretariaRepositorio : Repositorio<Secretaria>, ISecretariaRepositorio
    {
        private readonly AppDbContext context;

        public SecretariaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }


        public async Task<Secretaria?> SelectByNLegajo(string cod)
        {
            return await context.Set<Secretaria>().FirstOrDefaultAsync(x => x.NLegajo == cod);
        }


        public async Task<List<SecretariaListadoDTO>> SelectListaSecretaria(int personaId)
        {
            var secretarias = await context.Secretarias
                .Include(p => p.Personas)
                .Where(p => p.PersonaId == personaId)
                .Select(p => new SecretariaListadoDTO
                {
                    Id = p.Id,
                    DatosSecre = $"{p.Personas!.Nombre} {p.Personas.Apellido} - DNI: {p.Personas.DNI} " +
                    $"- N° de Legajo:  {p.NLegajo} "
                })
                .ToListAsync();
            
            return secretarias;
        }

        public async Task<List<SecretariaListadoDTO>> SelectListaSecretarias()
        {
            var secretarias = await (
                from sec in context.Secretarias
                join per in context.Personas
                    on sec.PersonaId equals per.Id
                select new SecretariaListadoDTO
                {
                    Id = per.Id,
                    DatosSecre = per.Nombre + " " + per.Apellido +
                                 " - DNI: " + per.DNI +
                                 " - N° Legajo: " + sec.NLegajo
                }
            ).ToListAsync();

            return secretarias;
        }


    }
}
