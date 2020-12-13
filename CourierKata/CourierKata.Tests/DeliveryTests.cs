using System.Collections.Generic;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace CourierKata.Tests
{
    public class DeliveryTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IParcelService _parcelService;

        public DeliveryTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _parcelService = new ParcelService();
        }

        [Fact]
        public void GivenTwoParcelsOf1CMDimension_WhenCreateDelivery_ThenCorrectTotalIsReturned()
        {
            var expectedTotalCost = 6m;
            var parcel1 = _parcelService.CreateParcel(1, 1, 1);
            var parcel2 = _parcelService.CreateParcel(1, 1, 1);
            var parcels = new List<Parcel> { parcel1, parcel2 };

            var deliveryService = new DeliveryService();


            var delivery = deliveryService.CreateDelivery(parcels);
            delivery.TotalCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine($"Delivery cost: {delivery.TotalCost}");
        }
    }

    public class DeliveryService
    {
        public Delivery CreateDelivery(List<Parcel> parcels)
        {
            var delivery = new Delivery {Parcels = parcels};
            var totalCost = 0m;
            foreach (var parcel in delivery.Parcels)
            {
                totalCost += parcel.Cost;
            }
            delivery.TotalCost = totalCost;
            return delivery;
        }
    }

    public class Delivery
    {
        public List<Parcel> Parcels { get; set; }
        public decimal TotalCost { get; set; }
    }
}
