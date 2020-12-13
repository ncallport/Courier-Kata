using CourierKata.Services.Configuration;
using CourierKata.Services.Models;
using CourierKata.Tests;

namespace CourierKata.Services.Services
{
    public class ParcelService : IParcelService
    {
        private readonly Settings _settings;
        public ParcelService(Settings settings)
        {
            _settings = settings;
        }

        public Parcel CreateParcel(uint width, uint height, uint length, uint weight)
        {
            var parcel = new Parcel
            {
                WidthInCentimeters = width,
                HeightInCentimeters = height,
                LengthInCentimeters = length,
                WeightInKilograms = weight
            };

            parcel.SetTypeByDimension(_settings);
            parcel.SetCostByType(_settings);

            return parcel;
        }
    }
}
