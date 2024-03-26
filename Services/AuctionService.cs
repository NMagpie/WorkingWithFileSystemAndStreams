using _11._Working_with_file_system_and_streams.Auctions;
using _11._Working_with_file_system_and_streams.Logging;

namespace _11._Working_with_file_system_and_streams.Services
{
    public class AuctionService
    {

        private int _auctionIds;

        private readonly List<Auction> _auctions = [];

        public int AuctionIds
        {
            get
            {
                return _auctionIds++;
            }
        }

        public async Task<Auction?> CreateAuction(TimeSpan duration, DateTime? timeStart = null)
        {
            try
            {
                var auction = new Auction(AuctionIds, duration, timeStart);

                _auctions.Add(auction);

                await ALogger.Log(ELogStatus.SUCCESS);

                return auction;
            }
            catch (Exception)
            {
                await ALogger.Log(ELogStatus.FAILURE);
                return null;
            }
        }
    }
}