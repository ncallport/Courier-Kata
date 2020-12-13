using CourierKata.Services.Models;

namespace CourierKata.Tests
{
    public interface IParcelService
    {
        Parcel CreateParcel(uint width, uint height, uint length, uint weight);
    }
}
