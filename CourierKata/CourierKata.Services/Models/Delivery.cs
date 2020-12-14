using System.Collections.Generic;

namespace CourierKata.Services.Models
{
    public class Delivery
    {
        public List<Parcel> Parcels { get; set; }
        public List<DeliveryDiscount> Discounts { get; set; }
        public decimal ParcelCost { get; set; }
        public decimal ParcelDiscount { get; set; }
        public decimal TotalParcelCost { get; set; }
        public bool AddSpeedyShipping { get; set; }
        public decimal SpeedyShippingCost { get; set; }
        public decimal TotalDeliveryCost { get; set; }
    }

}
