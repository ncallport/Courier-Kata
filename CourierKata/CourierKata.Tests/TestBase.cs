using CourierKata.Services.Configuration;

namespace CourierKata.Tests
{
    public class TestBase
    {
        protected Settings Settings()
        {
            return new Settings
            {
                SmallDimensionLimit = 10,
                MediumDimensionLimit = 50,
                LargeDimensionLimit = 100,
                SmallWeightLimit = 1,
                MediumWeightLimit = 3,
                LargeWeightLimit = 6,
                XLWeightLimit = 10,
                SmallParcelCost = 3,
                MediumParcelCost = 8,
                LargeParcelCost = 15,
                XLParcelCost = 25,
                OverWeightChargePerKilo = 2
            };
        }
    }
}
