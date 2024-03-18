namespace App.Infra.Data.Context
{
    using Base;
    using Microsoft.EntityFrameworkCore;

    public class InMemoryContext : ContextBase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemory");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
