using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Acessly.Infrastructure
{
    public class AcesslyDbContextFactory : IDesignTimeDbContextFactory<AcesslyDbContext>
    {
        public AcesslyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AcesslyDbContext>();

            optionsBuilder.UseOracle(
                "User Id=rm560967;Password=240406;Data Source=oracle.fiap.com.br:1521/ORCL;"
            );

            return new AcesslyDbContext(optionsBuilder.Options);
        }
    }
}
