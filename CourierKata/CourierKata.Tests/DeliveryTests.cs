using System.Collections.Generic;
using CourierKata.Services.Models;
using CourierKata.Services.Services;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace CourierKata.Tests
{
    public class DeliveryTests : TestBase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IParcelService _parcelService;
        private readonly IDeliveryService _deliveryService;

        public DeliveryTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _parcelService = new ParcelService(Settings());
            _deliveryService = new DeliveryService(Settings());
        }

        [Fact]
        public void GivenTwoParcelsOf1CMDimension_WhenCreateDelivery_ThenCorrectTotalIsReturned()
        {
            var expectedTotalCost = 6m;
            var parcel1 = _parcelService.CreateParcel(1, 1, 1, 1);
            var parcel2 = _parcelService.CreateParcel(1, 1, 1, 1);
            var parcels = new List<Parcel> { parcel1, parcel2 };
            var delivery = _deliveryService.CreateDelivery(parcels);
            delivery.TotalDeliveryCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine(ReturnDeliveryOutput(delivery));
        }

        [Fact]
        public void GivenTwoParcelsOf1CMDimensionAndSpeedyShipping_WhenCreateDelivery_ThenCorrectTotalIsReturned()
        {
            var expectedTotalCost = 12m;
            var parcel1 = _parcelService.CreateParcel(1, 1, 1, 1);
            var parcel2 = _parcelService.CreateParcel(1, 1, 1, 1);
            var parcels = new List<Parcel> { parcel1, parcel2 };
            var delivery = _deliveryService.CreateDelivery(parcels, true);
            delivery.TotalDeliveryCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine(ReturnDeliveryOutput(delivery));
        }

        [Fact]
        public void GivenSixMediumParcelsOfDefinedDimensionsAndWeight_WhenCreateDelivery_ThenCorrectDiscountIsReturned()
        {
            var expectedDiscount = 18;
            var parcel1 = _parcelService.CreateParcel(40, 40, 40, 3);
            var parcel2 = _parcelService.CreateParcel(40, 40, 40, 3);
            var parcel3 = _parcelService.CreateParcel(40, 40, 40, 3);
            var parcel4 = _parcelService.CreateParcel(40, 40, 40, 4);
            var parcel5 = _parcelService.CreateParcel(40, 40, 40, 4);
            var parcel6 = _parcelService.CreateParcel(40, 40, 40, 4);
            var parcels = new List<Parcel> {parcel1, parcel2, parcel3, parcel4, parcel5, parcel6};
            var delivery = _deliveryService.CreateDelivery(parcels);
            delivery.ParcelDiscount.ShouldBe(expectedDiscount);
        }
    }
}
