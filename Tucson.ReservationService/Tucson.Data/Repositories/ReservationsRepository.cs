using Microsoft.EntityFrameworkCore;
using Tucson.Data.Config;
using Tucson.Data.Interfaces;
using Tucson.Models.Entities;

namespace Tucson.Data.Repositories
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly TucsonDbContext dbContext;
        public ReservationsRepository(TucsonDbContext db)
        {
            dbContext = db;
        }

        public List<Reservation> GetAllReservations()
        {
            return dbContext.Set<Reservation>().ToList();
        }


        public Reservation AddReservation(Reservation reservation)
        {
            dbContext.Set<Reservation>().Add(reservation);
            dbContext.SaveChanges();
            return reservation;
        }

        public void CancelReservation(int reservationId)
        {
            Reservation reservation = dbContext.Set<Reservation>().Find(reservationId);
            dbContext.Set<Reservation>().Remove(reservation);
            dbContext.SaveChanges();
        }

        public List<Reservation> GetReservationByTableAndDate(string table, DateTime date)
        {
            return dbContext.Set<Reservation>().Where(r => r.TypeOfTable == table && r.Date.Day == date.Day).ToList();
        }

        public Reservation GetReservationById(int reservationId)
        {
            return dbContext.Set<Reservation>().Find(reservationId);
        }
    }
}
