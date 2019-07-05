using ProyectoG.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProyectoG.Context
{
    public class GameContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<tbl_Administradores> tbl_Administradores { get; set; }
        public DbSet<tbl_Respuestas> tbl_Respuestas { get; set; }
        public DbSet<tbl_Preguntas> tbl_Preguntas { get; set; }
        public DbSet<tbl_Categorias> tbl_Categorias { get; set; }
        public DbSet<tbl_Amigos> tbl_Amigos { get; set; }
        public DbSet<tbl_Errores> tbl_Errores { get; set; }
        public DbSet<tbl_Jugadores> tbl_Jugadores { get; set; }
        public DbSet<tbl_Niveles> tbl_Niveles { get; set; }
        public DbSet<tbl_Puntuaciones> tbl_Puntuaciones { get; set; }
        public DbSet<tbl_PuntuacionSalones> tbl_PuntuacionSalones { get; set; }
        public DbSet<tbl_Retos> tbl_Retos { get; set; }
        public DbSet<tbl_Salones> tbl_Salones { get; set; }
        public DbSet<tbl_PreguntaSalones> tbl_PreguntaSalones { get; set; }
        public DbSet<tbl_JugadoresSalones> tbl_JugadoresSalones { get; set; }

        public System.Data.Entity.DbSet<ProyectoG.Models.tbl_Instituciones> tbl_Instituciones { get; set; }
    }
}