using CourierKata.Services.Configuration;
using CourierKata.Services.Models;

namespace CourierKata.Services.Services
{
    public static class ParcelExtensions
    {
        public static Parcel SetTypeByDimension(this Parcel parcel, Settings settings)
        {
            if (parcel.WeightInKilograms > settings.HeavyWeightLimit)
                parcel.Type = ParcelType.Heavy;

            else if (parcel.HeightInCentimeters < settings.SmallDimensionLimit &&
                     parcel.WidthInCentimeters < settings.SmallDimensionLimit &&
                     parcel.LengthInCentimeters < settings.SmallDimensionLimit)
                parcel.Type = ParcelType.Small;

            else if (parcel.HeightInCentimeters < settings.MediumDimensionLimit &&
                     parcel.WidthInCentimeters < settings.MediumDimensionLimit &&
                     parcel.LengthInCentimeters < settings.MediumDimensionLimit)
                parcel.Type = ParcelType.Medium;

            else if (parcel.HeightInCentimeters < settings.LargeDimensionLimit &&
                     parcel.WidthInCentimeters < settings.LargeDimensionLimit &&
                     parcel.LengthInCentimeters < settings.LargeDimensionLimit)
                parcel.Type = ParcelType.Large;

            else
                parcel.Type = ParcelType.XL;

            return parcel;
        }

        public static Parcel SetCostByType(this Parcel parcel, Settings settings)
        {
            var baseCost = 0m;
            var extraWeightCost = 0m;

            if (parcel.WeightInKilograms > settings.HeavyWeightLimit)
            {
                baseCost = settings.HeavyParcelCost;
                extraWeightCost = (parcel.WeightInKilograms - settings.HeavyWeightLimit) *
                                  settings.HeavyOverWeightChargePerKilo;
            }
            else
            {

                switch (parcel.Type)
                {
                    case ParcelType.Small:
                        baseCost = settings.SmallParcelCost;
                        if (parcel.WeightInKilograms > settings.SmallWeightLimit)
                            extraWeightCost = (parcel.WeightInKilograms - settings.SmallWeightLimit) *
                                              settings.OverWeightChargePerKilo;
                        break;
                    case ParcelType.Medium:
                        baseCost = settings.MediumParcelCost;
                        if (parcel.WeightInKilograms > settings.MediumWeightLimit)
                            extraWeightCost = (parcel.WeightInKilograms - settings.MediumWeightLimit) *
                                              settings.OverWeightChargePerKilo;
                        break;
                    case ParcelType.Large:
                        baseCost = settings.LargeParcelCost;
                        if (parcel.WeightInKilograms > settings.LargeWeightLimit)
                            extraWeightCost = (parcel.WeightInKilograms - settings.LargeWeightLimit) *
                                              settings.OverWeightChargePerKilo;
                        break;
                    case ParcelType.XL:
                        baseCost = settings.XLParcelCost;
                        if (parcel.WeightInKilograms > settings.XLWeightLimit)
                            extraWeightCost = (parcel.WeightInKilograms - settings.XLWeightLimit) *
                                              settings.OverWeightChargePerKilo;
                        break;

                }
            }

            parcel.BaseCost = baseCost;
            parcel.ExtraWeightCost = extraWeightCost;
            parcel.TotalCost = baseCost + extraWeightCost;
            return parcel;
        }
    }
}
