using MadeSalud.BD.Datos.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadeSalud.BD.Datos
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<MiUsuario>(options)
    {
        public required DbSet<Persona> Personas { get; set; }

        public required DbSet<Paciente> Pacientes { get; set; }

        public required DbSet<Medico> Medicos { get; set; }

        public required DbSet<Secretaria> Secretarias { get; set; }

        public required DbSet<Turno> Turnos { get; set; }

        public required DbSet<HistoriaClinica> HistoriasClinicas { get; set; }

        public required DbSet<ConsultaMedica> ConsultasMedicas { get; set; }

        public required DbSet<Medicamento> Medicamentos { get; set; }

        public required DbSet<PedidoLaboratorio> PedidosLaboratorio { get; set; }

        public required DbSet<DetallePedidoLaboratorio> DetallesPedidosLaboratorio { get; set; }

        public required DbSet<Receta> Recetas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Medico)
                .WithMany()
                .HasForeignKey(t => t.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Paciente)
                .WithMany()
                .HasForeignKey(t => t.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }





    }
}
