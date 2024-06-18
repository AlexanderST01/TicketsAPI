using Microsoft.EntityFrameworkCore;
using TicketsAPI.Models;
using TicketsAPI.Models.Ticket;

namespace TicketsAPI.DAL
{

    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Personas> Personas { get; set; }
    }
}
