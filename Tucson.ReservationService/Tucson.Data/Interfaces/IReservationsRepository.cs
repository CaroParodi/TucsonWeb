using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Models.Entities;

namespace Tucson.Data.Interfaces
{
    public interface IReservationsRepository
    {
        public List<Reservation> GetAllReservations();
        public List<Reservation> GetReservationByTableAndDate(string table, DateTime date);
        public Reservation AddReservation(Reservation reservation);
        public void CancelReservation(int reservationId);
        public Reservation GetReservationById(int reservationId);

    }
}
