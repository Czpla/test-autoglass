namespace App.Infra.Data.Context
{
    using Base;
    using Microsoft.EntityFrameworkCore;

    public class PostgresContext : ContextBase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

            var d = Environment.GetEnvironmentVariables();

            optionsBuilder.UseNpgsql(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
