using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tucson.Models.DTO
{
    public class ReservationDTO
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int Seats { get; set; }
    }
}
