using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Oracle.EntityFrameworkCore;

namespace ConformiTrain.Persistence
{
    public class ConformiTrainDbContextFactory : IDesignTimeDbContextFactory<ConformiTrainDbContext>
    {
        public ConformiTrainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConformiTrainDbContext>();

            var connectionString = "User Id=RM558444;Password=160405;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SID=ORCL)))";

            optionsBuilder.UseOracle(connectionString);

            return new ConformiTrainDbContext(optionsBuilder.Options);
        }
    }
}

