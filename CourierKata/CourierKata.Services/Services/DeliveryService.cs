using System.Collections.Generic;
using CourierKata.Services.Configuration;
using CourierKata.Services.Models;

namespace CourierKata.Services.Services
{
    public class DeliveryService : IDeliveryService
    {

        private readonly Settings _settings;
        public DeliveryService(Settings settings)
        {
            _settings = settings;
        }

        public Delivery CreateDelivery(List<Parcel> parcels, bool addSpeedyShipping = false)
        {
            var delivery = new Delivery { Parcels = parcels, AddSpeedyShipping = addSpeedyShipping};

            delivery.SetParcelCost();
            delivery.SetParcelDiscount(_settings);
            delivery.SetTotalParcelCost();
            delivery.SetSpeedyShippingCost();
            delivery.SetTotalDeliveryCost();

            return delivery;
        }
    }
}
