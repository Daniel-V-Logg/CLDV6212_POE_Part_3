using Microsoft.EntityFrameworkCore;
using ST10310998_CLDV6212_POE__Part_1.Models;

namespace ST10310998_CLDV6212_POE__Part_1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
