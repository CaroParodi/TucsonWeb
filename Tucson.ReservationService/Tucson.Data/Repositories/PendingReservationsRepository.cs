using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Data.Config;
using Tucson.Data.Interfaces;
using Tucson.Models.Entities;

namespace Tucson.Data.Repositories
{
    public class PendingReservationsRepository : IPendingReservationsRepository
    {
        private readonly TucsonDbContext dbContext;
        public PendingReservationsRepository(TucsonDbContext db)
        {
            dbContext = db;
        }
        public void AddPendingReservation(Reservation reservation, int categoryCode)
        {
            PendingReservation pendingReservation = new PendingReservation(reservation);
            pendingReservation.CategoryCode = categoryCode;
            dbContext.Set<PendingReservation>().Add(pendingReservation);
            dbContext.SaveChanges();
        }

        public void AddPendingReservation(PendingReservation pendingReservation)
        {
            dbContext.Set<PendingReservation>().Add(pendingReservation);
            dbContext.SaveChanges();
        }

        public PendingReservation GetPriorityPendingReservationByDate(DateTime date, string table)
        {
            try
            {
                return dbContext.Set<PendingReservation>().Where(pr => pr.Date == date && pr.TypeOfTable == table)
                                                      .OrderByDescending(pr => pr.CategoryCode).ThenBy(pr => pr.DateOfCall)
                                                      .FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public void RemovePendingReservation(PendingReservation pendingReservation)
        {
            dbContext.Set<PendingReservation>().Remove(pendingReservation);
            dbContext.SaveChanges();
        }
    }
}
