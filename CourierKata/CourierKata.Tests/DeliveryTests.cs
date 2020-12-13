using System.Collections.Generic;
using CourierKata.Services.Models;
using CourierKata.Services.Services;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace CourierKata.Tests
{
    public class DeliveryTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IParcelService _parcelService;
        private readonly IDeliveryService _deliveryService;

        public DeliveryTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _parcelService = new ParcelService();
            _deliveryService = new DeliveryService();
        }

        [Fact]
        public void GivenTwoParcelsOf1CMDimension_WhenCreateDelivery_ThenCorrectTotalIsReturned()
        {
            var expectedTotalCost = 6m;
            var parcel1 = _parcelService.CreateParcel(1, 1, 1);
            var parcel2 = _parcelService.CreateParcel(1, 1, 1);
            var parcels = new List<Parcel> { parcel1, parcel2 };
            var delivery = _deliveryService.CreateDelivery(parcels);
            delivery.TotalDeliveryCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine($"Delivery cost: {delivery.TotalDeliveryCost}");
        }

        [Fact]
        public void GiveTwoParcelsOf1CMDimensionAndSpeedyShipping_WhenCreateDelivery_ThenCorrectTotalIsReturned()
        {
            var expectedTotalCost = 12m;
            var parcel1 = _parcelService.CreateParcel(1, 1, 1);
            var parcel2 = _parcelService.CreateParcel(1, 1, 1);
            var parcels = new List<Parcel> { parcel1, parcel2 };
            var delivery = _deliveryService.CreateDelivery(parcels, true);
            delivery.TotalDeliveryCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine($"Delivery cost: {delivery.TotalDeliveryCost}");
        }
    }
}
