using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Models.DTO;

namespace Tucson.Models.Entities
{
    public class Reservation
    {
        public Reservation() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }

        [Required]  
        public DateTime Date { get; set; }

        [Required]  
        public int ClientId { get; set; }

        [Required]
        public string TypeOfTable { get; set; }

        public Reservation(PendingReservation reservation)
        {
            Date = reservation.Date;
            ClientId = reservation.ClientId;
            TypeOfTable = reservation.TypeOfTable;
        }

        public Reservation(ReservationDTO reservationDTO)
        {
            Date = reservationDTO.Date;
            ClientId = reservationDTO.ClientId;
        }

    }
}
