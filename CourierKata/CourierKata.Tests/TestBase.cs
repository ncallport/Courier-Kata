using System.Linq;
using System.Text;
using CourierKata.Services.Configuration;
using CourierKata.Services.Models;

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
                HeavyWeightLimit = 50,
                SmallParcelCost = 3,
                MediumParcelCost = 8,
                LargeParcelCost = 15,
                XLParcelCost = 25,
                HeavyParcelCost = 50,
                OverWeightChargePerKilo = 2,
                HeavyOverWeightChargePerKilo = 1,
                SmallParcelDiscountNumber = 4,
                MediumParcelDiscountNumber = 3,
                MixedParcelDiscountNumber = 5
            };
        }

        protected string ReturnDeliveryOutput(Delivery delivery)
        {
            var sb = new StringBuilder();
            var parcels = delivery.Parcels;
            var discounts = delivery.Discounts;

            sb.AppendLine("KATA DELIVERY");
            sb.AppendLine("");

            if (parcels != null && parcels.Any())
            {
                sb.AppendLine("PARCELS");
                sb.AppendLine("");
                var i = 0;
                foreach (var parcel in parcels)
                {
                    i += 1;
                    sb.AppendLine(
                        $"Parcel {i} - Type: {parcel.Type}, W: {parcel.WidthInCentimeters}cm, H: {parcel.HeightInCentimeters}cm, L: {parcel.LengthInCentimeters}cm, KG: {parcel.WeightInKilograms}");
                    sb.AppendLine(
                        $"Cost: ${parcel.BaseCost}, Extra: ${parcel.ExtraWeightCost}, Total: ${parcel.TotalCost}. Discount: {parcel.DiscountApplied}");
                    sb.AppendLine("");
                }
                sb.AppendLine("");
            }

            if (discounts != null && discounts.Any())
            {
                sb.AppendLine("DISCOUNTS");
                sb.AppendLine("");
                var i = 1;
                foreach (var discount in discounts)
                {
                    i += 1;
                    sb.AppendLine($"Discount {i} - Type: {discount.Type}, Amount: ${discount.Amount}");
                }
                sb.AppendLine("");
            }

            sb.AppendLine($"Speedy Shipping: {delivery.AddSpeedyShipping}, Amount: ${delivery.SpeedyShippingCost}");
            sb.AppendLine("");
            sb.AppendLine($"TOTAL: ${delivery.TotalDeliveryCost}");
            return sb.ToString();
        }
    }
}
