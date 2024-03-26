using _11._Working_with_file_system_and_streams.Services;

var service = new AuctionService();

var auction = service.CreateAuction(new TimeSpan(0,15,0));