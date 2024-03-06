using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Models.Entities;
using Tucson.Models.Enums;

namespace Tucson.Business.ClientValidation
{
    public class PlatinumClientValidationStrategy : IClientReservationStrategy
    {
        public int ClientCode => (int)ECategoryCode.Platinum;
        public bool ValidateReservation(Reservation reservation)
        {
            var maxDayReservation = DateTime.Now.AddDays((int)ECategoryDate.Platinum);

            if (reservation.Date <= maxDayReservation)
            {
                return true;
            }
            return false;
        }
    }
}
