using Microsoft.EntityFrameworkCore;
using TicketsAPI.Core.Entities;

namespace TicketsAPI.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
