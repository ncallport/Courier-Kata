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

    public class ParcelService : IParcelService
    {
        public Parcel CreateParcel(uint width, uint height, uint length)
        {
            var parcel = new Parcel();
            parcel.WidthInCentimeters = width;
            parcel.HeightInCentimeters = height;
            parcel.LengthInCentimeters = length;

            if (parcel.HeightInCentimeters < 10 &&
                parcel.WidthInCentimeters < 10 &&
                parcel.LengthInCentimeters < 10)
            {
                parcel.Type = ParcelType.Small;
                parcel.Cost = 3;
            }

            else if (parcel.HeightInCentimeters < 50 &&
                     parcel.WidthInCentimeters < 50 &&
                     parcel.LengthInCentimeters < 50)
            {
                parcel.Type = ParcelType.Medium;
                parcel.Cost = 8;
            }

            else if (parcel.HeightInCentimeters < 100 &&
                     parcel.WidthInCentimeters < 100 &&
                     parcel.LengthInCentimeters < 100)
            {
                parcel.Type = ParcelType.Large;
                parcel.Cost = 15;
            }

            else
            {
                parcel.Type = ParcelType.XL;
                parcel.Cost = 25;
            }
            return parcel;
        }
    }

    public class Parcel
    {
        public uint LengthInCentimeters { get; set; }
        public uint WidthInCentimeters { get; set; }
        public uint HeightInCentimeters { get; set; }
        public ParcelType Type { get; set; }
        public decimal Cost { get; set; }
    }

    public enum ParcelType
    {
        Small,
        Medium,
        Large,
        XL
    }
}
