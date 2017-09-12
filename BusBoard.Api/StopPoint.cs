namespace BusBoard.Api
{
    public class StopPoint
    {
        public string NaptanId { get; set; }
        public string CommonName { get; set; }
        public decimal Distance { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
    }
}
