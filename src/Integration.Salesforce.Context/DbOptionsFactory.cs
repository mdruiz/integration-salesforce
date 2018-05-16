using Microsoft.Extensions.Configuration;


public static class DbOptionsFactory
{
    static DbOptionsFactory()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.dev.json")
            .Build();
        ConnectionString = config["MongoDB:ConnectionString"];
        DatabaseName = config["MongoDB:Database"];
        
    }

    public static string ConnectionString{ get; }
    public static string DatabaseName { get; }
}