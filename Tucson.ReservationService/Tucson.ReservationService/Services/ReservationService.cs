using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Data.Interfaces;
using Tucson.Models.DTO;
using Tucson.Models.Entities;
using Tucson.Models.Enums;

namespace Tucson.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IClientReservationContext _clientReservationContext;
        private readonly IClientRepository _clientRepository;
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IPendingReservationsRepository _pendingReservationsRepository;

        public ReservationService(
                                  IReservationsRepository reservationsRepository,
                                  IClientRepository clientRepository,
                                  IClientReservationContext clientReservationContext,
                                  IPendingReservationsRepository pendingReservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
            _clientRepository = clientRepository;
            _clientReservationContext = clientReservationContext;
            _pendingReservationsRepository = pendingReservationsRepository;
        }

        public ResponseDTO GetAllReservations()
        {
            var response = new ResponseDTO();

            try
            {
                response.Result = _reservationsRepository.GetAllReservations();
                if (response.Result == null)
                {
                    response.Message = "No hay reservas activas";
                }
                response.Success = true;
                return response;
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ResponseDTO AddReservation(ReservationDTO reservation)
        {
            var response = new ResponseDTO();

            try
            {
                Client client = _clientRepository.GetClientById(reservation.ClientId);
                if (client != null)
                {
                    var table = ValidateTable(reservation.Seats);
                    if (table != null)
                    {
                        Reservation reservationToSave = new Reservation(reservation);
                        reservationToSave.TypeOfTable = table.TypeOfTable;
                        if (_clientReservationContext.GetValidReservation(reservationToSave, client.CategoryCode))
                        {

                            if (_reservationsRepository.GetReservationByTableAndDate(reservationToSave.TypeOfTable, reservation.Date).Count() < table.Availability)
                            {
                                var newReservation = _reservationsRepository.AddReservation(reservationToSave);
                                response.Message = "La reserva se ha generado con éxito, su número de reserva es: " + newReservation.ReservationId;
                                response.Success = true;
                                return response;
                            }
                            _pendingReservationsRepository.AddPendingReservation(reservationToSave, client.CategoryCode);
                            response.Success = false;
                            response.Message = "La reserva no pudo ser creada debido a que no existen mesas disponibles para esta fecha";
                            return response;

                        }
                        response.Success = false;
                        response.Message = "La reserva solicitada no cumple con el tiempo de anticipación requerido para su categoría de cliente.";
                        return response;
                    }


                    response.Success = false;
                    response.Message = "No se pudo realizar la reserva. La cantidad de asientos solicitada es mayor a la cantidad de asientos por mesa.";
                    return response;

                }

                response.Success = false;
                response.Message = "El cliente ingresado no existe";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public ResponseDTO AddPendingReservation(ReservationDTO reservation)
        {
            var response = new ResponseDTO();

            try
            {
                Client client = _clientRepository.GetClientById(reservation.ClientId);
                var table = ValidateTable(reservation.Seats);

                PendingReservation pendingReservation = new PendingReservation(reservation);
                pendingReservation.TypeOfTable = table.TypeOfTable;
                pendingReservation.CategoryCode = client.CategoryCode;

                _pendingReservationsRepository.AddPendingReservation(pendingReservation);

                response.Success = true;
                response.Message = "La reserva ha quedado en espera hasta que se habilite un lugar disponible.";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

        }


        public ResponseDTO CancelReservation(int reservationId)
        {
            var response = new ResponseDTO();

            try
            {
                Reservation reservation = _reservationsRepository.GetReservationById(reservationId);
                _reservationsRepository.CancelReservation(reservationId);

                PendingReservation priorityReservation = _pendingReservationsRepository.GetPriorityPendingReservationByDate(reservation.Date, reservation.TypeOfTable);
                if (priorityReservation != null)
                {
                    _reservationsRepository.AddReservation(new Reservation(priorityReservation));
                    _pendingReservationsRepository.RemovePendingReservation(priorityReservation);
                }

                response.Success = true;
                response.Message = "La reserva se ha cancelado con éxito";
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }


        private ETable ValidateTable(int seats)
        {

            switch (seats)
            {
                case var s when s <= 2:
                    return ETable.SmallTable;
                case var s when s <= 4:
                    return ETable.MediumTable;
                case var s when s <= 6:
                    return ETable.LargeTable;
            }
            return null;
        }

    }
}
