using CourierKata.Services.Models;
using CourierKata.Services.Services;
using Shouldly;
using Xunit;

namespace CourierKata.Tests
{
    public class ParcelTests
    {



        [Fact]
        public void GivenAParcelOf1CMDimensions_WhenCreateParcel_ThenCorrectCostIsReturned()
        {
            var expectedCost = 3m;
            var parcelService = new ParcelService();
            var parcel = parcelService.CreateParcel(1, 1, 1);
            parcel.Type.ShouldBe(ParcelType.Small);
            parcel.Cost.ShouldBe(expectedCost);
        }

        [Theory]
        [InlineData(1, 1, 1, 3, ParcelType.Small)]
        [InlineData(40, 40, 40, 8, ParcelType.Medium)]
        [InlineData(60, 60, 60, 15, ParcelType.Large)]
        [InlineData(120, 120, 120, 25, ParcelType.XL)]
        [InlineData(1, 40, 1, 8, ParcelType.Medium)]
        [InlineData(1, 40, 60, 15, ParcelType.Large)]
        [InlineData(40, 60, 120, 25, ParcelType.XL)]
        public void GivenAParcelOfDefinedDimensions_WhenCreateParcel_ThenCorrectCostIsReturned(uint width, uint height,
            uint length, decimal expectedCost, ParcelType expectedParcelType)
        {
            var parcelService = new ParcelService();
            var parcel = parcelService.CreateParcel(width, height, length);
            parcel.Type.ShouldBe(expectedParcelType);
            parcel.Cost.ShouldBe(expectedCost);
        }

    }


}
