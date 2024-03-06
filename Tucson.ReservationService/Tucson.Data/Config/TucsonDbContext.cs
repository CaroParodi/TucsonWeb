using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Models.Entities;

namespace Tucson.Data.Config
{
    public class TucsonDbContext : DbContext
    {
        public TucsonDbContext(DbContextOptions<TucsonDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<PendingReservation> PendingReservations { get; set; }

    }
}
