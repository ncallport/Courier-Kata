namespace CourierKata.Services.Configuration
{
    public class Settings
    {
        public int SmallDimensionLimit { get; set; }
        public int MediumDimensionLimit { get; set; }
        public int LargeDimensionLimit { get; set; }
        public int SmallWeightLimit { get; set; }
        public int MediumWeightLimit { get; set; }
        public int LargeWeightLimit { get; set; }
        public int XLWeightLimit { get; set; }
        public decimal SmallParcelCost { get; set; }
        public decimal MediumParcelCost { get; set; }
        public decimal LargeParcelCost { get; set; }
        public decimal XLParcelCost { get; set; }
        public decimal OverWeightChargePerKilo { get; set; }
    }
}
