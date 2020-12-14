using System.Collections.Generic;
using System.Linq;
using CourierKata.Services.Configuration;
using CourierKata.Services.Models;

namespace CourierKata.Services.Services
{
    public static class DeliveryExtensions
    {
        public static Delivery SetParcelCost(this Delivery delivery)
        {
            var parcelCost = 0m;
            foreach (var parcel in delivery.Parcels)
            {
                parcelCost += parcel.TotalCost;
            }
            delivery.ParcelCost = parcelCost;
            return delivery;
        }

        public static Delivery SetSpeedyShippingCost(this Delivery delivery)
        {
            var speedyShippingCost = 0m;
            if (delivery.AddSpeedyShipping)
            {
                delivery.AddSpeedyShipping = true;
                speedyShippingCost = delivery.TotalParcelCost;
            }
            delivery.SpeedyShippingCost = speedyShippingCost;
            return delivery;
        }

        public static Delivery SetTotalParcelCost(this Delivery delivery)
        {
            delivery.TotalParcelCost = delivery.ParcelCost - delivery.ParcelDiscount;
            return delivery;
        }

        public static Delivery SetTotalDeliveryCost(this Delivery delivery)
        {
            delivery.TotalDeliveryCost = delivery.TotalParcelCost + delivery.SpeedyShippingCost;
            return delivery;
        }

        public static Delivery SetParcelDiscount(this Delivery delivery, Settings settings)
        {
            var discounts = new List<DeliveryDiscount>();
            var smallParcels = delivery.Parcels.Where(x => x.Type == ParcelType.Small).OrderBy(x => x.TotalCost).ToList();
            var mediumParcels = delivery.Parcels.Where(p => p.Type == ParcelType.Medium).OrderBy(p => p.TotalCost).ToList();
            int i;

            // Do Small parcels discount
            var smallParcelsDiscount = 0m;
            if (smallParcels.Any())
            {
                i = 0;
                foreach (var smallParcel in smallParcels)
                {
                    i += 1;
                    if (i % settings.SmallParcelDiscountNumber == 0)
                    {
                        smallParcel.DiscountApplied = true;
                        smallParcelsDiscount += smallParcel.TotalCost;
                    }
                }
                discounts.Add(new DeliveryDiscount { Amount = smallParcelsDiscount, Type = DiscountType.SmallParcelDiscount });
            }

            // Do Medium parcels discount
            var mediumParcelsDiscount = 0m;
            if (mediumParcels.Any())
            {
                i = 0;
                foreach (var mediumParcel in mediumParcels)
                {
                    i += 1;
                    if (i % settings.MediumParcelDiscountNumber == 0)
                    {
                        mediumParcel.DiscountApplied = true;
                        mediumParcelsDiscount += mediumParcel.TotalCost;
                    }
                }
                discounts.Add(new DeliveryDiscount { Amount = mediumParcelsDiscount, Type = DiscountType.MediumParcelDiscount });
            }

            // Do Mixed parcels discount
            var mixedParcelsDiscount = 0m;
            var remainingParcels = delivery.Parcels.Where(p => p.DiscountApplied == false).OrderBy(p => p.TotalCost).ToList();
            if (remainingParcels.Any())
            {
                i = 0;
                foreach (var remainingParcel in remainingParcels)
                {
                    i += 1;
                    if (i % settings.MixedParcelDiscountNumber == 0)
                    {
                        remainingParcel.DiscountApplied = true;
                        mixedParcelsDiscount += remainingParcel.TotalCost;
                    }
                }
                discounts.Add(new DeliveryDiscount { Amount = mixedParcelsDiscount, Type = DiscountType.MixedParcelDiscount });
            }
            
            var parcelDiscount = 0m;
            if (discounts.Any())
            {
                delivery.Discounts = discounts;
                parcelDiscount = discounts.Sum(p => p.Amount);
            }

            delivery.ParcelDiscount = parcelDiscount;
            return delivery;
        }
    }
}
