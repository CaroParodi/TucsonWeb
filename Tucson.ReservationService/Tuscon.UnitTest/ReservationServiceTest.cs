using Azure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Business.ClientValidation;
using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Data.Interfaces;
using Tucson.Data.Repositories;
using Tucson.Models.DTO;
using Tucson.Models.Entities;
using Tucson.Models.Enums;
using Tucson.Services.Services;
using Xunit;

namespace Tuscon.UnitTest
{

    public class ReservationServiceTest
    {
        public Mock<IClientRepository> clientRepositoryMock;
        public Mock<IReservationsRepository> reservationsRepositoryMock;
        public Mock<IPendingReservationsRepository> pendingReservationsRepositoryMock;
        public Mock <IClientReservationContext> reservationContextMock;

        public ReservationService ReservationService;

        public ReservationDTO ReservationDTO = new ReservationDTO() { ClientId = 1, Date = DateTime.Now.AddDays(2), Seats = 1};
        public Client TestClient = new Client() { ClientId = 1, CategoryCode = 101, Name = "Test" };
        public Reservation Reservation = new Reservation() { ReservationId = 1, ClientId = 1, Date = DateTime.Now.AddDays(2), TypeOfTable = ETable.SmallTable.TypeOfTable };
        public PendingReservation PendingReservation = new PendingReservation() { PendingReservationId = 1, CategoryCode = 101, ClientId = 1, DateOfCall = DateTime.Now, TypeOfTable = ETable.SmallTable.TypeOfTable };

        public void Initialize()
        {
            clientRepositoryMock = new Mock<IClientRepository>();
            reservationsRepositoryMock = new Mock<IReservationsRepository>();
            pendingReservationsRepositoryMock  = new Mock<IPendingReservationsRepository>();
            reservationContextMock = new Mock<IClientReservationContext>();

            ReservationService = new ReservationService(reservationsRepositoryMock.Object,
                                                        clientRepositoryMock.Object,
                                                        reservationContextMock.Object,
                                                        pendingReservationsRepositoryMock.Object);
        }
        [Fact]
        public void AddReservation_Ok()
        {
            Initialize();
  
            clientRepositoryMock.Setup(r => r.GetClientById(ReservationDTO.ClientId)).Returns(TestClient);
            reservationContextMock.Setup(r => r.GetValidReservation(It.IsAny<Reservation>(), TestClient.CategoryCode)).Returns(true);
            reservationsRepositoryMock.Setup(r => r.GetReservationByTableAndDate(ETable.SmallTable.TypeOfTable, ReservationDTO.Date)).Returns(new List<Reservation>());
            reservationsRepositoryMock.Setup(r => r.AddReservation(It.IsAny<Reservation>())).Returns(new Reservation() { ReservationId = 1});

            var expectedResponse = new ResponseDTO()
            {
                Success = true,
            };

            var result = ReservationService.AddReservation(ReservationDTO);

            Assert.Equal(expectedResponse.Success, result.Success);
        }

        [Fact]
        public void AddReservation_CategoryError()
        {
            Initialize();

            var expectedResponse = new ResponseDTO()
            {
                Message = "La reserva solicitada no cumple con el tiempo de anticipación requerido para su categoría de cliente.",
                Success = false
            };

            clientRepositoryMock.Setup(r => r.GetClientById(ReservationDTO.ClientId)).Returns(TestClient);
            reservationContextMock.Setup(r => r.GetValidReservation(It.IsAny<Reservation>(), TestClient.CategoryCode)).Returns(false);

            var result = ReservationService.AddReservation(ReservationDTO);

            Assert.Equal(expectedResponse.Success, result.Success);
            Assert.Equal(expectedResponse.Message, result.Message);

        }

        [Fact]
        public void AddReservation_SeatsError()
        {
            Initialize();

            var expectedResponse = new ResponseDTO()
            {
                Message = "No se pudo realizar la reserva. La cantidad de asientos solicitada es mayor a la cantidad de asientos por mesa.",
                Success = false
            };

            ReservationDTO.Seats = 9;

            clientRepositoryMock.Setup(r => r.GetClientById(ReservationDTO.ClientId)).Returns(TestClient);

            var result = ReservationService.AddReservation(ReservationDTO);

            Assert.Equal(expectedResponse.Success, result.Success);
            Assert.Equal(expectedResponse.Message, result.Message);

        }

        [Fact]
        public void CancelReservation_Ok()
        {
            Initialize();

            var expectedResponse = new ResponseDTO()
            {
                Success = true,
                Message = "La reserva se ha cancelado con éxito"
            };

            reservationsRepositoryMock.Setup(r => r.GetReservationById(1)).Returns(Reservation);
            reservationsRepositoryMock.Setup(r => r.CancelReservation(1));
            pendingReservationsRepositoryMock.Setup(r => r.GetPriorityPendingReservationByDate(Reservation.Date, Reservation.TypeOfTable)).Returns(PendingReservation);
            reservationsRepositoryMock.Setup(r => r.AddReservation(new Reservation(PendingReservation))).Returns(new Reservation(PendingReservation));
            pendingReservationsRepositoryMock.Setup(r => r.RemovePendingReservation(PendingReservation));

            var result = ReservationService.CancelReservation(1);

            Assert.Equal(expectedResponse.Success, result.Success);

        }

        [Fact]
        public void CancelReservation_ReservationError()
        {
            Initialize();

            var expectedResponse = new ResponseDTO()
            {
                Success = false,
            };


            var result = ReservationService.CancelReservation(1);

            Assert.Equal(expectedResponse.Success, result.Success);
        }

        [Fact]
        public void GetAllReservations_Ok()
        {
            Initialize();

            var expectedResponse = new ResponseDTO()
            {
                Result = new List<Reservation>(3),
                Success = true,
            };

            reservationsRepositoryMock.Setup(r => r.GetAllReservations()).Returns(new List<Reservation>(3));


            var result = ReservationService.GetAllReservations();

            Assert.Equal(expectedResponse.Success, result.Success);
            Assert.Equal(expectedResponse.Result, result.Result);
        }

        [Fact]
        public void GetAllReservations_NoReservations()
        {
            Initialize();
            

            var expectedResponse = new ResponseDTO()
            {
                Success = true,
                Message = "No hay reservas activas",
            };

            var result = ReservationService.GetAllReservations();

            Assert.Equal(expectedResponse.Success, result.Success);
            Assert.Equal(expectedResponse.Result, result.Result);
            Assert.Equal(expectedResponse.Message, result.Message);
        }

        [Fact]
        public void AddPendingReservations_Ok()
        {
            Initialize();

            var expectedResponse = new ResponseDTO()
            {
                Success = true,
                Message = "La reserva ha quedado en espera hasta que se habilite un lugar disponible."
            };

            clientRepositoryMock.Setup(r => r.GetClientById(ReservationDTO.ClientId)).Returns(TestClient);
            pendingReservationsRepositoryMock.Setup(r => r.AddPendingReservation(It.IsAny<PendingReservation>()));

            var result = ReservationService.AddPendingReservation(ReservationDTO);

            Assert.Equal(expectedResponse.Success, result.Success);
            Assert.Equal(expectedResponse.Message, result.Message);
        }
    }
}

