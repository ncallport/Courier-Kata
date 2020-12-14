namespace CourierKata.Services.Models
{
    public class Parcel
    {
        public uint LengthInCentimeters { get; set; }
        public uint WidthInCentimeters { get; set; }
        public uint HeightInCentimeters { get; set; }
        public uint WeightInKilograms { get; set; }
        public decimal BaseCost { get; set; }
        public decimal ExtraWeightCost { get; set; }
        public ParcelType Type { get; set; }
        public decimal TotalCost { get; set; }
        public bool DiscountApplied { get; set; } = false;
    }
}
