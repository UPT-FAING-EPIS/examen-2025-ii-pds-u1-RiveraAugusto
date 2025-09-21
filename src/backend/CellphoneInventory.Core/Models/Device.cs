namespace CellphoneInventory.Core.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string IMEI { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public DateTime EntryDate { get; set; }
        public string Observations { get; set; }
    }
}