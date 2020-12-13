using System.Collections.Generic;
using CourierKata.Services.Models;

namespace CourierKata.Services.Services
{
    public interface IDeliveryService
    {
        Delivery CreateDelivery(List<Parcel> parcels, bool addSpeedyShipping = false);
    }
}
