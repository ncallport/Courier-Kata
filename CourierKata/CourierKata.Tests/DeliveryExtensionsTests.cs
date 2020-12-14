using System.Collections.Generic;
using CourierKata.Services.Models;
using CourierKata.Services.Services;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace CourierKata.Tests
{
    public class DeliveryExtensionsTests : TestBase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IParcelService _parcelService;

        public DeliveryExtensionsTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _parcelService = new ParcelService(Settings());
        }

        [Fact]
        public void GivenTwoParcelsOf1CMDimension_WhenSetParcelCost_ThenCorrectTotalIsReturned()
        {
            var expectedTotalCost = 6m;
            var parcel1 = _parcelService.CreateParcel(1, 1, 1, 1);
            var parcel2 = _parcelService.CreateParcel(1, 1, 1, 1);
            var parcels = new List<Parcel> { parcel1, parcel2 };
            var delivery = new Delivery { Parcels = parcels, AddSpeedyShipping = false };
            delivery.SetParcelCost();
            delivery.ParcelCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine(ReturnDeliveryOutput(delivery));
        }

        [Fact]
        // USING THIS DATA
        //[InlineData(1, 1, 1, 1, 3, 0, 3)] // Small, no extra weight
        //[InlineData(1, 1, 1, 2, 3, 2, 5)] // Small, 1kg extra
        //[InlineData(40, 40, 40, 3, 8, 0, 8)] // Medium, no extra weight
        //[InlineData(40, 40, 40, 5, 8, 4, 12)] // Medium, 2kg extra
        //[InlineData(60, 60, 60, 6, 15, 0, 15)] // Large, no extra weight
        //[InlineData(60, 60, 60, 10, 15, 8, 23)] // Large, 4kg extra
        //[InlineData(120, 120, 120, 10, 25, 0, 25)] // XL, no extra weight
        //[InlineData(120, 120, 120, 16, 25, 12, 37)] // XL, 6kg extra
        //[InlineData(1, 1, 1, 60, 50, 10, 60)] // Small but Heavy
        public void GivenNineParcelsOfVariousDimensions_WhenSetParcelCost_ThenCorrectTotalIsReturned()
        {
            var expectedTotalCost = 188m;
            var parcel1 = _parcelService.CreateParcel(1, 1, 1, 1);
            var parcel2 = _parcelService.CreateParcel(1, 1, 1, 2);
            var parcel3 = _parcelService.CreateParcel(40, 40, 40, 3);
            var parcel4 = _parcelService.CreateParcel(40, 40, 40, 5);
            var parcel5 = _parcelService.CreateParcel(60, 60, 60, 6);
            var parcel6 = _parcelService.CreateParcel(60, 60, 60, 10);
            var parcel7 = _parcelService.CreateParcel(120, 120, 120, 10);
            var parcel8 = _parcelService.CreateParcel(120, 120, 120, 16);
            var parcel9 = _parcelService.CreateParcel(1, 1, 1, 60);

            var parcels = new List<Parcel> { parcel1, parcel2, parcel3, parcel4, parcel5, parcel6, parcel7, parcel8, parcel9 };
            var delivery = new Delivery { Parcels = parcels, AddSpeedyShipping = false };
            delivery.SetParcelCost();
            delivery.ParcelCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine(ReturnDeliveryOutput(delivery));
        }

        [Fact]
        // USING THIS DATA
        //[InlineData(1, 1, 1, 1, 3, 0, 3)] // Small, no extra weight
        //[InlineData(1, 1, 1, 2, 3, 2, 5)] // Small, 1kg extra
        //[InlineData(40, 40, 40, 3, 8, 0, 8)] // Medium, no extra weight
        //[InlineData(40, 40, 40, 5, 8, 4, 12)] // Medium, 2kg extra
        //[InlineData(60, 60, 60, 6, 15, 0, 15)] // Large, no extra weight
        //[InlineData(60, 60, 60, 10, 15, 8, 23)] // Large, 4kg extra
        //[InlineData(120, 120, 120, 10, 25, 0, 25)] // XL, no extra weight
        //[InlineData(120, 120, 120, 16, 25, 12, 37)] // XL, 6kg extra
        //[InlineData(1, 1, 1, 60, 50, 10, 60)] // Small but Heavy
        public void GivenNineParcelsOfVariousDimensions_WhenSetParcelDiscount_ThenCorrectTotalIsReturned()
        {
            var expectedTotalDiscount = 15m;
            var parcel1 = _parcelService.CreateParcel(1, 1, 1, 1);
            var parcel2 = _parcelService.CreateParcel(1, 1, 1, 2);
            var parcel3 = _parcelService.CreateParcel(40, 40, 40, 3);
            var parcel4 = _parcelService.CreateParcel(40, 40, 40, 5);
            var parcel5 = _parcelService.CreateParcel(60, 60, 60, 6);
            var parcel6 = _parcelService.CreateParcel(60, 60, 60, 10);
            var parcel7 = _parcelService.CreateParcel(120, 120, 120, 10);
            var parcel8 = _parcelService.CreateParcel(120, 120, 120, 16);
            var parcel9 = _parcelService.CreateParcel(1, 1, 1, 60);

            var parcels = new List<Parcel> { parcel1, parcel2, parcel3, parcel4, parcel5, parcel6, parcel7, parcel8, parcel9 };
            var delivery = new Delivery { Parcels = parcels, AddSpeedyShipping = false };
            delivery.SetParcelDiscount(Settings());
            delivery.ParcelDiscount.ShouldBe(expectedTotalDiscount);
            _testOutputHelper.WriteLine(ReturnDeliveryOutput(delivery));
        }

        [Fact]
        public void GivenParcelCostAndParcelDiscount_WhenSetTotalParcelCost_ThenTotalParcelCostIsSet()
        {
            var expectedTotalParcelCost = 12m;
            var delivery = new Delivery { ParcelCost = 20, ParcelDiscount = 8 };
            delivery.SetTotalParcelCost();
            delivery.TotalParcelCost.ShouldBe(expectedTotalParcelCost);
            _testOutputHelper.WriteLine(ReturnDeliveryOutput(delivery));
        }

        [Fact]
        public void GivenSpeedyShippingCost_WhenSetSpeedyShippingCost_ThenShippingCostIsSet()
        {
            var expectedSpeedyShippingCost = 12m;
            var delivery = new Delivery { TotalParcelCost = 12, AddSpeedyShipping = true };
            delivery.SetSpeedyShippingCost();
            delivery.SpeedyShippingCost.ShouldBe(expectedSpeedyShippingCost);
            _testOutputHelper.WriteLine(ReturnDeliveryOutput(delivery));
        }

        [Fact]
        public void GivenTotalParcelCostAndSpeedyShippingCost_WhenSetTotalDeliveryCost_ThenTotalDeliveryCostIsSet()
        {
            var expectedTotalDeliveryCost = 20m;
            var delivery = new Delivery { TotalParcelCost = 10, SpeedyShippingCost = 10 };
            delivery.SetTotalDeliveryCost();
            delivery.TotalDeliveryCost = expectedTotalDeliveryCost;
            _testOutputHelper.WriteLine(ReturnDeliveryOutput(delivery));
        }
    }
}
