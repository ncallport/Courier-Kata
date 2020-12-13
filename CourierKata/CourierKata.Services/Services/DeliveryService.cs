using System.Collections.Generic;
using CourierKata.Services.Models;

namespace CourierKata.Services.Services
{
    public class DeliveryService : IDeliveryService
    {
        public Delivery CreateDelivery(List<Parcel> parcels, bool addSpeedyShipping = false)
        {
            var delivery = new Delivery { Parcels = parcels, AddSpeedyShipping = addSpeedyShipping};

            delivery.SetParcelCost();
            delivery.SetSpeedyShippingCost();
            delivery.SetTotalDeliveryCost();

            return delivery;
        }
    }
}
