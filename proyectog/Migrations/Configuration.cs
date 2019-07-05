namespace ProyectoG.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProyectoG.Context.GameContext>
    {
        //        Para migraciones manuales:
        //En la consola administradora de paquetes
        //1. Enable-Migration -ContextTypeName NOmbredelstorecontext
        //2. Add-Migration Nombredelamigracionquelequeramosdar
        //3. Update-Database
        //4. Se deben volver a crear las vistas para que funcione bien

        //Para migraciones automaticas

        //1.Enable-Migrations -ContextTypeName Nombrecontexto -EnableAutomaticMigrations

            ///             ignorar los cambios iniciales
       /// Add-Migration Initial -IgnoreChanges
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ProyectoG.Context.GameContext";
        }

        protected override void Seed(ProyectoG.Context.GameContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
