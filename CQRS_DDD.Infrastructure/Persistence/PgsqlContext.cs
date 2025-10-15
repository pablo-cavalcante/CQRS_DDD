using Microsoft.EntityFrameworkCore;
using CQRS_DDD.Domain.Entities;

#pragma warning disable
namespace CQRS_DDD.Infrastructure.Persistence
{
    public class PgsqlContext: DbContext
    {
        public PgsqlContext(DbContextOptions<PgsqlContext> options) : base(options) {  }

        public DbSet<FrutasEntity> frutas { get; set; }

        public DbSet<ComunicadoLojaEntity> ciLojas { get; set; }
    }
}
