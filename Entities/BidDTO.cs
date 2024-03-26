namespace _11._Working_with_file_system_and_streams.Entities
{
    public class BidDTO(float price, int lot)
    {
        public int AuctionId { get; set; }

        public float Price { get; set; } = price;

        public DateTime BidTime { get; set; } = DateTime.Now;

        public int Lot { get; set; } = lot;
    }
}
