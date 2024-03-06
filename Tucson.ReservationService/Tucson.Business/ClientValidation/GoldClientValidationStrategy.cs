using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Models.Entities;
using Tucson.Models.Enums;

namespace Tucson.Business.ClientValidation
{
    public class GoldClientValidationStrategy : IClientReservationStrategy
    {
        public int ClientCode => (int)ECategoryCode.Gold;
        public bool ValidateReservation(Reservation reservation)
        {
            var dayToReservation = DateTime.Now.AddDays((int)ECategoryDate.Gold);

            if (reservation.Date < dayToReservation)
            {
                return true;
            }
            return false;
        }
    }
}
