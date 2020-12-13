namespace CourierKata.Services.Models
{
    public class Parcel
    {
        public uint LengthInCentimeters { get; set; }
        public uint WidthInCentimeters { get; set; }
        public uint HeightInCentimeters { get; set; }
        public ParcelType Type { get; set; }
        public decimal Cost { get; set; }
    }
}
