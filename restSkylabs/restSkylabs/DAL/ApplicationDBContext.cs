using restSkylabs.Model;
using Microsoft.EntityFrameworkCore;

namespace restSkylabs.DAL
{
    public class ApplicationDBContext : DbContext
    {

        public DbSet<Records> Records { get; set; }

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

    }
}
