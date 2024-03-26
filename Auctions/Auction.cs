using _11._Working_with_file_system_and_streams.Entities;
using _11._Working_with_file_system_and_streams.Logging;
using System.Timers;

namespace _11._Working_with_file_system_and_streams.Auctions
{
    public class Auction(int id, TimeSpan auctionDuration, DateTime? timeStart = null)
    {
        public int Id { get; set; } = id;

        private DateTime? _timeStart = timeStart;

        public DateTime? TimeStart
        {
            get => _timeStart;

            set
            {
                _timeStart = value;
                _timeEnd = _timeStart?.Add(_auctionDuration);
            }
        }

        private TimeSpan _auctionDuration = auctionDuration;

        public TimeSpan AuctionDuration
        {
            get => _auctionDuration;
            set
            {
                _auctionDuration = value;
                _timeEnd = _timeStart?.Add(_auctionDuration);
            }
        }

        private DateTime? _timeEnd;

        public DateTime? TimeEnd
        {
            get => _timeEnd;
        }

        public EAcutionStatus AuctionStatus { get; set; } = EAcutionStatus.PENDING;

        public List<BidDTO> Bids { get; } = [];

        public void Start()
        {
            try
            {
                TimeStart = DateTime.Now;
                AuctionStatus = EAcutionStatus.STARTED;

                var checkForTime = new System.Timers.Timer(AuctionDuration.Milliseconds);

                checkForTime.Elapsed += (sender, e) => Stop(sender, e, checkForTime);
                checkForTime.Enabled = true;

                ALogger.Log(ELogStatus.SUCCESS);
            }
            catch (Exception)
            {
                ALogger.Log(ELogStatus.FAILURE);
            }
        }

        public void Stop(object? sender, ElapsedEventArgs e, System.Timers.Timer timer)
        {
            try
            {
                AuctionStatus = EAcutionStatus.FINISHED;
                timer.Stop();

                ALogger.Log(ELogStatus.SUCCESS);
            }
            catch (Exception)
            {
                ALogger.Log(ELogStatus.FAILURE);
            }
        }

        public void PlaceBid(BidDTO bid)
        {
            try
            {
                if (AuctionStatus != EAcutionStatus.STARTED)
                    return;

                if (_timeEnd < DateTime.Now)
                    return;

                Bids.Add(bid);

                ALogger.Log(ELogStatus.SUCCESS);
            }
            catch (Exception)
            {
                ALogger.Log(ELogStatus.FAILURE);
            }
        }
    }
}