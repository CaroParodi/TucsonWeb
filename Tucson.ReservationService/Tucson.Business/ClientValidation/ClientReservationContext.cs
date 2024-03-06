using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Models.Entities;

namespace Tucson.Business.ClientValidation
{
    public class ClientReservationContext : IClientReservationContext
    {
        private readonly IEnumerable<IClientReservationStrategy> _strategies;

        public ClientReservationContext(IEnumerable<IClientReservationStrategy> strategies)
        {
            _strategies = strategies;
        }
        public bool GetValidReservation(Reservation reservation, int clientCode)
        {
            var instance = _strategies.FirstOrDefault(s => s.ClientCode == clientCode);

            return instance is not null ? instance.ValidateReservation(reservation) : false;
           
        }
    }
}
