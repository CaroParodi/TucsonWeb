using Tucson.Models.Entities;

namespace Tucson.Business.ClientValidation.Interfaces
{
    public interface IClientReservationStrategy
    {
        int ClientCode { get; }
        bool ValidateReservation(Reservation reservation);

    }
}
