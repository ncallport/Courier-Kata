using CourierKata.Services.Models;
using CourierKata.Tests;

namespace CourierKata.Services.Services
{
    public class ParcelService : IParcelService
    {
        public Parcel CreateParcel(uint width, uint height, uint length)
        {
            var parcel = new Parcel();
            parcel.WidthInCentimeters = width;
            parcel.HeightInCentimeters = height;
            parcel.LengthInCentimeters = length;

            if (parcel.HeightInCentimeters < 10 &&
                parcel.WidthInCentimeters < 10 &&
                parcel.LengthInCentimeters < 10)
            {
                parcel.Type = ParcelType.Small;
                parcel.Cost = 3;
            }

            else if (parcel.HeightInCentimeters < 50 &&
                     parcel.WidthInCentimeters < 50 &&
                     parcel.LengthInCentimeters < 50)
            {
                parcel.Type = ParcelType.Medium;
                parcel.Cost = 8;
            }

            else if (parcel.HeightInCentimeters < 100 &&
                     parcel.WidthInCentimeters < 100 &&
                     parcel.LengthInCentimeters < 100)
            {
                parcel.Type = ParcelType.Large;
                parcel.Cost = 15;
            }

            else
            {
                parcel.Type = ParcelType.XL;
                parcel.Cost = 25;
            }
            return parcel;
        }
    }
}
