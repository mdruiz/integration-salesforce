using Microsoft.Extensions.Configuration;


public static class DbOptionsFactory
{
    static DbOptionsFactory()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        ConnectionString = config["MongoDB:ConnectionString"];
        DatabaseName = config["MongoDB:Database"];


        // DbContextOptions = new DbContextOptionsBuilder<MyDbContext>()
        //     .UseSqlServer(connectionString)
        //     .Options;

        
    }

    public static string ConnectionString{ get; }
    public static string DatabaseName { get; }
}