using System.Collections.Generic;
using CourierKata.Services.Models;

namespace CourierKata.Services.Services
{
    public class DeliveryService : IDeliveryService
    {
        public Delivery CreateDelivery(List<Parcel> parcels, bool addSpeedyShipping = false)
        {
            var delivery = new Delivery { Parcels = parcels, AddSpeedyShipping = addSpeedyShipping};

            var totalParcelCost = 0m;
            foreach (var parcel in delivery.Parcels)
            {
                totalParcelCost += parcel.TotalCost;
            }
            delivery.TotalParcelCost = totalParcelCost;

            var speedyShippingCost = 0m;
            if (delivery.AddSpeedyShipping)
            {
                delivery.AddSpeedyShipping = true;
                speedyShippingCost = delivery.TotalParcelCost;
            }

            delivery.SpeedyShippingCost = speedyShippingCost;
            delivery.TotalDeliveryCost = delivery.TotalParcelCost + delivery.SpeedyShippingCost;
            return delivery;
        }
    }
}
