using _11._Working_with_file_system_and_streams.Entities;
using _11._Working_with_file_system_and_streams.Logging;
using _11._Working_with_file_system_and_streams.Services;

try
{
    var service = new AuctionService();

    var auction = await service.CreateAuction(new TimeSpan(0, 15, 0));

    await auction.Start();

    await auction.PlaceBid(new BidDTO(0, 12));

    await auction.PlaceBid(new BidDTO(12, 14));

    await ALogger.ReadLog("D:\\AM\\C#\\11. Working with file system and streams\\bin\\Debug\\net8.0\\Logs\\Log_26_03_2024.txt");
} catch (Exception e)
{
    Console.WriteLine(e);
}