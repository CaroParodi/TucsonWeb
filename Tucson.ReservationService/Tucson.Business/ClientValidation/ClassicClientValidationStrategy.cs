using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Models.Entities;
using Tucson.Models.Enums;

namespace Tucson.Business.ClientValidation
{
    public class ClassicClientValidationStrategy : IClientReservationStrategy
    {
        public int ClientCode => (int)ECategoryCode.Classic;
        public bool ValidateReservation(Reservation reservation)
        {

            var maxDayReservation = DateTime.Now.AddDays((int)ECategoryDate.Classic);

            if (reservation.Date <= maxDayReservation)
            {
                return true;
            }
            return false;
        }
    }
}
