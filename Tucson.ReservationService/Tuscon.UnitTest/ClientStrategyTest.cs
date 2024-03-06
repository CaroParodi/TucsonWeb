using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Business.ClientValidation;
using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Data.Interfaces;
using Tucson.Models.Entities;
using Tucson.Models.Enums;
using Xunit;

namespace Tuscon.UnitTest
{
    public class ClientStrategytest
    {
        public Mock<IClientReservationStrategy> clientReservationStrategyMock;
        public Mock<IClientReservationContext> reservationContextMock;

        public ClientReservationContext ClientReservationContext;

        public ClassicClientValidationStrategy ClassicClientValidationStrategy = new ClassicClientValidationStrategy();
        public GoldClientValidationStrategy GoldClientValidationStrategy = new GoldClientValidationStrategy();
        public DiamondClientValidationStrategy DiamondClientValidationStrategy = new DiamondClientValidationStrategy();
        public PlatinumClientValidationStrategy PlatinumClientValidationStrategy = new PlatinumClientValidationStrategy();


        public Reservation Reservation = new Reservation() { ReservationId = 1, ClientId = 1, Date = DateTime.Now.AddDays(1), TypeOfTable = ETable.SmallTable.TypeOfTable };

        public void Initialize()
        {
            reservationContextMock = new Mock<IClientReservationContext>();
            clientReservationStrategyMock = new Mock<IClientReservationStrategy>();
            ClientReservationContext = new ClientReservationContext(new List<IClientReservationStrategy> { ClassicClientValidationStrategy, GoldClientValidationStrategy, DiamondClientValidationStrategy, PlatinumClientValidationStrategy });
            clientReservationStrategyMock.Setup(s => s.ValidateReservation(It.IsAny<Reservation>())).Returns(true);
        }

        [Fact]
        public void GetValidReservation_ClassicValidation()
        {
            Initialize();

            var result = ClientReservationContext.GetValidReservation(Reservation, (int)ECategoryCode.Classic);

            Assert.True(result);
        }

        [Fact]
        public void GetValidReservation_GoldValidation()
        {
            Initialize();

            var result = ClientReservationContext.GetValidReservation(Reservation, (int)ECategoryCode.Gold);

            Assert.True(result);
        }

        [Fact]
        public void GetValidReservation_PlatinumValidation()
        {
            Initialize();

            var result = ClientReservationContext.GetValidReservation(Reservation, (int)ECategoryCode.Platinum);

            Assert.True(result);
        }

        [Fact]
        public void GetValidReservation_DiamondValidation()
        {
            Initialize();

            var result = ClientReservationContext.GetValidReservation(Reservation, (int)ECategoryCode.Diamond);

            Assert.True(result);
        }

        [Fact]
        public void GetValidReservation_ErrorValidation()
        {
            Initialize();

            var result = ClientReservationContext.GetValidReservation(Reservation, 666);

            Assert.False(result);
        }


        [Fact]
        public void ValidateReservation_ClassicReservation_Ok()
        {
            Initialize();
            Reservation.Date = DateTime.Now.AddDays((int)ECategoryDate.Classic);
            var result = ClassicClientValidationStrategy.ValidateReservation(Reservation);

            Assert.True(result);
        }

        [Fact]
        public void ValidateReservation_ClassicReservation_Error()
        {
            Initialize();

            Reservation.Date = DateTime.Now.AddDays((int)ECategoryDate.Classic + 1);
            var result = ClassicClientValidationStrategy.ValidateReservation(Reservation);

            Assert.False(result);
        }

        [Fact]
        public void ValidateReservation_GoldReservation_Ok()
        {
            Initialize();
            Reservation.Date = DateTime.Now.AddDays((int)ECategoryDate.Gold);

            var result = GoldClientValidationStrategy.ValidateReservation(Reservation);

            Assert.True(result);
        }

        [Fact]
        public void ValidateReservation_GoldReservation_Error()
        {
            Initialize();

            Reservation.Date = DateTime.Now.AddDays((int)ECategoryDate.Gold + 1);
            var result = GoldClientValidationStrategy.ValidateReservation(Reservation);

            Assert.False(result);
        }

        [Fact]
        public void ValidateReservation_DiamondReservation_Ok()
        {
            Initialize();
            Reservation.Date = DateTime.Now.AddDays((int)ECategoryDate.Diamond);

            var result = DiamondClientValidationStrategy.ValidateReservation(Reservation);

            Assert.True(result);
        }

        [Fact]
        public void ValidateReservation_DiamondReservation_Error()
        {
            Initialize();

            Reservation.Date = DateTime.Now.AddDays((int)ECategoryDate.Diamond + 1);
            var result = DiamondClientValidationStrategy.ValidateReservation(Reservation);

            Assert.False(result);
        }

        [Fact]
        public void ValidateReservation_PlatinumReservation_Ok()
        {
            Initialize();
            Reservation.Date = DateTime.Now.AddDays((int)ECategoryDate.Platinum);

            var result = PlatinumClientValidationStrategy.ValidateReservation(Reservation);

            Assert.True(result);
        }

        [Fact]
        public void ValidateReservation_PlatinumReservation_Error()
        {
            Initialize();

            Reservation.Date = DateTime.Now.AddDays((int)ECategoryDate.Platinum + 1);
            var result = PlatinumClientValidationStrategy.ValidateReservation(Reservation);

            Assert.False(result);
        }
    }
}
