using CourierKata.Services.Models;
using CourierKata.Services.Services;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace CourierKata.Tests
{
    public class ParcelExtensionsTests : TestBase
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ParcelExtensionsTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void GivenAParcelOf1CMDimensions_WhenSetTypeByDimension_ThenCorrectTypeIsApplied()
        {
            var expectedType = ParcelType.Small;
            var parcel = new Parcel
            {
                WidthInCentimeters = 1,
                HeightInCentimeters = 1,
                LengthInCentimeters = 1
            };
            parcel.SetTypeByDimension(Settings());
            parcel.Type.ShouldBe(expectedType);
            _testOutputHelper.WriteLine($"Parcel type: {parcel.Type}");
        }

        [Theory]
        [InlineData(1, 1, 1, ParcelType.Small)]
        [InlineData(40, 40, 40, ParcelType.Medium)]
        [InlineData(60, 60, 60, ParcelType.Large)]
        [InlineData(120, 120, 120, ParcelType.XL)]
        [InlineData(1, 40, 1, ParcelType.Medium)]
        [InlineData(1, 40, 60, ParcelType.Large)]
        [InlineData(40, 60, 120, ParcelType.XL)]
        public void GivenAParcelOfDefinedDimensions_WhenSetTypeByDimension_ThenCorrectTypeIsApplied(uint width, uint height, uint length, ParcelType expectedType)
        {
            var parcel = new Parcel
            {
                WidthInCentimeters = width,
                HeightInCentimeters = height,
                LengthInCentimeters = length
            };
            parcel.SetTypeByDimension(Settings());
            parcel.Type.ShouldBe(expectedType);
            _testOutputHelper.WriteLine($"Parcel type: {parcel.Type}");
        }

        [Fact]
        public void GivenAParcelOf1CMDimensions_WhenSetCostByType_ThenCorrectCostIsApplied()
        {
            var expectedCost = 3m;
            var parcel = new Parcel
            {
                WidthInCentimeters = 1,
                HeightInCentimeters = 1,
                LengthInCentimeters = 1
            };
            parcel.SetTypeByDimension(Settings());
            parcel.SetCostByType(Settings());
            parcel.BaseCost.ShouldBe(expectedCost);
            parcel.TotalCost.ShouldBe(expectedCost);
            _testOutputHelper.WriteLine($"Parcel cost: {parcel.BaseCost}");
        }

        [Theory]
        [InlineData(1, 1, 1, 3)]
        [InlineData(40, 40, 40, 8)]
        [InlineData(60, 60, 60, 15)]
        [InlineData(120, 120, 120, 25)]
        [InlineData(1, 40, 1, 8)]
        [InlineData(1, 40, 60, 15)]
        [InlineData(40, 60, 120, 25)]
        public void GivenAParcelOfDefinedDimensions_WhenSetCostByType_ThenCorrectCostIsApplied(uint width, uint height, uint length, decimal expectedCost)
        {

            var parcel = new Parcel
            {
                WidthInCentimeters = width,
                HeightInCentimeters = height,
                LengthInCentimeters = length
            };
            parcel.SetTypeByDimension(Settings());
            parcel.SetCostByType(Settings());
            parcel.BaseCost.ShouldBe(expectedCost);
            parcel.TotalCost.ShouldBe(expectedCost);
            _testOutputHelper.WriteLine($"Parcel cost: {parcel.BaseCost}");
        }

        [Fact]
        public void GivenAParcelOf1CMAnd1KG_WhenSetCostByType_ThenCorrectCostIsApplied()
        {
            var expectedCost = 3m;
            var parcel = new Parcel
            {
                WidthInCentimeters = 1,
                HeightInCentimeters = 1,
                LengthInCentimeters = 1,
                WeightInKilograms = 1
            };
            parcel.SetTypeByDimension(Settings());
            parcel.SetCostByType(Settings());
            parcel.BaseCost.ShouldBe(expectedCost);
            parcel.TotalCost.ShouldBe(expectedCost);
            _testOutputHelper.WriteLine($"Parcel cost: {parcel.BaseCost}");
        }

        [Fact]
        public void GivenAParcelOf1CMAnd2KG_WhenSetCostByType_ThenCorrectCostIsApplied()
        {

            var expectedBaseCost = 3m;
            var expectedExtraWeightCost = 2m;
            var expectedTotalCost = 5m;
            var parcel = new Parcel
            {
                WidthInCentimeters = 1,
                HeightInCentimeters = 1,
                LengthInCentimeters = 1,
                WeightInKilograms = 2
            };
            parcel.SetTypeByDimension(Settings());
            parcel.SetCostByType(Settings());
            parcel.BaseCost.ShouldBe(expectedBaseCost);
            parcel.ExtraWeightCost.ShouldBe(expectedExtraWeightCost);
            parcel.TotalCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine($"Parcel base cost: {parcel.BaseCost}");
            _testOutputHelper.WriteLine($"Parcel extra weight cost: {parcel.ExtraWeightCost}");
            _testOutputHelper.WriteLine($"Parcel total cost: {parcel.TotalCost}");
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 3, 0, 3)] // Small, no extra weight
        [InlineData(1, 1, 1, 2, 3, 2, 5)] // Small, 1kg extra
        [InlineData(40, 40, 40, 3, 8, 0, 8)] // Medium, no extra weight
        [InlineData(40, 40, 40, 5, 8, 4, 12)] // Medium, 2kg extra
        [InlineData(60, 60, 60, 6, 15, 0, 15)] // Large, no extra weight
        [InlineData(60, 60, 60, 10, 15, 8, 23)] // Large, 4kg extra
        [InlineData(120, 120, 120, 10, 25, 0, 25)] // XL, no extra weight
        [InlineData(120, 120, 120, 16, 25, 12, 37)] // XL, 6kg extra
        public void GivenAParcelOfDefinedCMAndDefinedKG_WhenSetCostByType_ThenCorrectCostIsApplied(uint width, uint height, uint length, uint weight, decimal expectedBaseCost, decimal expectedExtraWeightCost, decimal expectedTotalCost)
        {

            var parcel = new Parcel
            {
                WidthInCentimeters = width,
                HeightInCentimeters = height,
                LengthInCentimeters = length,
                WeightInKilograms = weight
            };
            parcel.SetTypeByDimension(Settings());
            parcel.SetCostByType(Settings());
            parcel.BaseCost.ShouldBe(expectedBaseCost);
            parcel.ExtraWeightCost.ShouldBe(expectedExtraWeightCost);
            parcel.TotalCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine($"Parcel base cost: {parcel.BaseCost}");
            _testOutputHelper.WriteLine($"Parcel extra weight cost: {parcel.ExtraWeightCost}");
            _testOutputHelper.WriteLine($"Parcel total cost: {parcel.TotalCost}");
        }

        [Fact]
        public void GivenAParcelOf1CMAnd60KG_WhenSetCostByType_ThenCorrectCostIsApplied()
        {
            var expectedType = ParcelType.Heavy;
            var expectedBaseCost = 50m;
            var expectedExtraWeightCost = 10m;
            var expectedTotalCost = 60m;
            var parcel = new Parcel
            {
                WidthInCentimeters = 1,
                HeightInCentimeters = 1,
                LengthInCentimeters = 1,
                WeightInKilograms = 60
            };
            parcel.SetTypeByDimension(Settings());
            parcel.SetCostByType(Settings());
            parcel.Type.ShouldBe(expectedType);
            parcel.BaseCost.ShouldBe(expectedBaseCost);
            parcel.ExtraWeightCost.ShouldBe(expectedExtraWeightCost);
            parcel.TotalCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine($"Parcel type: {parcel.Type}");
            _testOutputHelper.WriteLine($"Parcel base cost: {parcel.BaseCost}");
            _testOutputHelper.WriteLine($"Parcel extra weight cost: {parcel.ExtraWeightCost}");
            _testOutputHelper.WriteLine($"Parcel total cost: {parcel.TotalCost}");
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 3, 0, 3)] // Small, no extra weight
        [InlineData(1, 1, 1, 2, 3, 2, 5)] // Small, 1kg extra
        [InlineData(40, 40, 40, 3, 8, 0, 8)] // Medium, no extra weight
        [InlineData(40, 40, 40, 5, 8, 4, 12)] // Medium, 2kg extra
        [InlineData(60, 60, 60, 6, 15, 0, 15)] // Large, no extra weight
        [InlineData(60, 60, 60, 10, 15, 8, 23)] // Large, 4kg extra
        [InlineData(120, 120, 120, 10, 25, 0, 25)] // XL, no extra weight
        [InlineData(120, 120, 120, 16, 25, 12, 37)] // XL, 6kg extra
        [InlineData(1, 1, 1, 60, 50, 10, 60)] // Small but Heavy
        public void GivenAParcelOfDefinedCMAndDefinedKGWithHeavy_WhenSetCostByType_ThenCorrectCostIsApplied(uint width, uint height, uint length, uint weight, decimal expectedBaseCost, decimal expectedExtraWeightCost, decimal expectedTotalCost)
        {

            var parcel = new Parcel
            {
                WidthInCentimeters = width,
                HeightInCentimeters = height,
                LengthInCentimeters = length,
                WeightInKilograms = weight
            };
            parcel.SetTypeByDimension(Settings());
            parcel.SetCostByType(Settings());
            parcel.BaseCost.ShouldBe(expectedBaseCost);
            parcel.ExtraWeightCost.ShouldBe(expectedExtraWeightCost);
            parcel.TotalCost.ShouldBe(expectedTotalCost);
            _testOutputHelper.WriteLine($"Parcel base cost: {parcel.BaseCost}");
            _testOutputHelper.WriteLine($"Parcel extra weight cost: {parcel.ExtraWeightCost}");
            _testOutputHelper.WriteLine($"Parcel total cost: {parcel.TotalCost}");
        }

    }
}
