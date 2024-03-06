using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Models.Entities;

namespace Tucson.Business.ClientValidation.Interfaces
{
    public interface IClientReservationContext
    {
        bool GetValidReservation(Reservation reservation, int clientCode);
    }
}
