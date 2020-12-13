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
            delivery.TotalParcelCost = parcelCost;
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

        public static Delivery SetTotalDeliveryCost(this Delivery delivery)
        {
            delivery.TotalDeliveryCost = delivery.TotalParcelCost + delivery.SpeedyShippingCost;
            return delivery;
        }
    }
}
