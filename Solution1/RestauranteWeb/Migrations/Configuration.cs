namespace RestauranteWeb.Migrations
{
    using RestauranteWeb.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestauranteWeb.Contexto.Contextos>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RestauranteWeb.Contexto.Contextos context)
        {
            context.Restaurantes.AddOrUpdate(r => r.NomeRestaurante,
                new Restaurante { NomeRestaurante = "Rest1" },
                new Restaurante { NomeRestaurante = "Rest2" },
                new Restaurante { NomeRestaurante = "Rest2" }
            );
            context.SaveChanges();

            context.Pratos.AddOrUpdate(p => p.NomePrato,
                new Prato { NomePrato = "Prato1", PrecoPrato= 10.00 },
                new Prato { NomePrato = "Prato2", PrecoPrato= 20.00 },
                new Prato { NomePrato = "Prato3", PrecoPrato= 30.00 }
                );
            context.SaveChanges();
        }
    }
}
