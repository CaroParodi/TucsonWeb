using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Models.Entities;
using Tucson.Models.Enums;

namespace Tucson.Business.ClientValidation
{
    public class DiamondClientValidationStrategy : IClientReservationStrategy
    {
        public int ClientCode => (int)ECategoryCode.Diamond;
        public bool ValidateReservation(Reservation reservation)
        {
            var maxDayReservation = DateTime.Now.AddDays((int)ECategoryDate.Diamond);

            if (reservation.Date <= maxDayReservation)
            {
                return true;
            }
            return false;
        }
    }
}
