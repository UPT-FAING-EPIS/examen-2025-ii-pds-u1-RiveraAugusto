namespace CellphoneInventory.Core.Models
{
    public class Movement
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public string MovementType { get; set; } // Entry, Exit, Transfer, Decommission
        public string SourceLocation { get; set; }
        public string DestinationLocation { get; set; }
        public DateTime MovementDate { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Notes { get; set; }
    }
}