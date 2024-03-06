using Microsoft.AspNetCore.Mvc;
using Moq;
using Tucson.Models.DTO;
using Tucson.Models.Entities;
using Tucson.Models.Enums;
using Tucson.Services.Services;
using Tucson.WebApi.Controllers;
using Xunit;

namespace Tuscon.UnitTest
{
    public class ReservationControllerTest
    {
        #region ListReservations
        [Fact]
        public void ListReservations_ReturnListOK()
        {
            // Arrange
            var mockReservationService = new Mock<IReservationService>();
            var result = new ResponseDTO()
            {
                Result = new List<Reservation>()
                {
                    new Reservation { ReservationId = 1, ClientId = 1, Date = DateTime.Now, TypeOfTable = ETable.SmallTable.TypeOfTable },
                    new Reservation { ReservationId = 2, ClientId = 2, Date = DateTime.Now, TypeOfTable = ETable.MediumTable.TypeOfTable }

                },
                Success = true
            };

            mockReservationService.Setup(service => service.GetAllReservations()).Returns(result);

            var controller = new ReservationController(mockReservationService.Object);

            // Act
            var resultado = controller.ListReservations() as OkObjectResult;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(200, resultado.StatusCode);
            Assert.Equal(result.Result, resultado.Value);
        }

        [Fact]
        public void ListReservations_ReturnError()
        {
            // Arrange
            var mockReservationService = new Mock<IReservationService>();
            mockReservationService.Setup(service => service.GetAllReservations()).Throws(new Exception("Ocurrió un error"));

            var controller = new ReservationController(mockReservationService.Object);

            // Act
            var resultado = controller.ListReservations() as BadRequestObjectResult;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(400, resultado.StatusCode);
            Assert.Equal("Ocurrió un error", resultado.Value);
        }
        #endregion

        #region Reserve
        [Fact]
        public void Reserve_ReservationOk()
        {
            // Arrange
            var mockReservationService = new Mock<IReservationService>();
            var reservation = new ReservationDTO();
            var successMessage = new ResponseDTO { Success = true, Message = "Reserva exitosa" };
            mockReservationService.Setup(service => service.AddReservation(reservation)).Returns(successMessage);

            var controller = new ReservationController(mockReservationService.Object);

            // Act
            var resultado = controller.AddReservation(reservation) as OkObjectResult;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(200, resultado.StatusCode);
            Assert.Equal("Reserva exitosa", resultado.Value);
        }

        [Fact]
        public void Reserve_ModelStateNotValid()
        {
            // Arrange
            var mockReservationService = new Mock<IReservationService>();
            var reservation = new ReservationDTO(); // Crear una instancia de ReservationDTO inválida
            var controller = new ReservationController(mockReservationService.Object);
            controller.ModelState.AddModelError("Error", "ModelState inválido");

            // Act
            var resultado = controller.AddReservation(reservation) as BadRequestResult;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(400, resultado.StatusCode);
        }

        [Fact]
        public void Reserve_BadRequest()
        {
            // Arrange
            var mockReservationService = new Mock<IReservationService>();
            var reservation = new ReservationDTO(); // Crear una instancia de ReservationDTO válida
            mockReservationService.Setup(service => service.AddReservation(reservation)).Throws(new Exception("Ocurrió un error"));

            var controller = new ReservationController(mockReservationService.Object);

            // Act
            var resultado = controller.AddReservation(reservation) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(400, resultado.StatusCode);
            Assert.Equal("Ocurrió un error", resultado.Value);
        }
        #endregion
    }
}