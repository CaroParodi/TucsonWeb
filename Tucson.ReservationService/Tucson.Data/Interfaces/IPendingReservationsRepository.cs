using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Models.Entities;

namespace Tucson.Data.Interfaces
{
    public interface IPendingReservationsRepository
    {
        public void AddPendingReservation(Reservation reservation, int categoryCode);
        public void AddPendingReservation(PendingReservation pendingReservation);
        public void RemovePendingReservation(PendingReservation pendingReservation);
        public PendingReservation GetPriorityPendingReservationByDate(DateTime date, string table);

    }
}
