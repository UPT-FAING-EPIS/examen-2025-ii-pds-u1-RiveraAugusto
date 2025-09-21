namespace CellphoneInventory.Core.Models
{
    public class Cellphone
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string IMEI { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Storage { get; set; }
        public string Processor { get; set; }
        public string RAM { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsAvailable { get; set; }
    }
}