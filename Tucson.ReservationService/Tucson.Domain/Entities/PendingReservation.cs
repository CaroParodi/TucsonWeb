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
    public class PendingReservation
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PendingReservationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public string TypeOfTable { get; set; }

        public DateTime DateOfCall { get; set; }

        [Required]
        public int CategoryCode { get; set; }

        public PendingReservation() { }
        public PendingReservation(Reservation reservation)
        {
            Date = reservation.Date;
            ClientId = reservation.ClientId;
            TypeOfTable = reservation.TypeOfTable;
            DateOfCall = DateTime.Now;
        }

        public PendingReservation(ReservationDTO reservation)
        {
            Date = reservation.Date;
            ClientId = reservation.ClientId;
            DateOfCall = DateTime.Now;
        }
    }
}
