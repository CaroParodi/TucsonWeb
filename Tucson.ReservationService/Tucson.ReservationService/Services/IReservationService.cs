using Tucson.Models.DTO;


namespace Tucson.Services.Services
{
    public interface IReservationService
    {
        public ResponseDTO AddReservation(ReservationDTO reservation);
        public ResponseDTO AddPendingReservation(ReservationDTO reservation);
        public ResponseDTO CancelReservation(int reservationId);
        public ResponseDTO GetAllReservations();

    }
}
