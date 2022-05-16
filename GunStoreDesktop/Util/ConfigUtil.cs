using System;
using Microsoft.Extensions.Configuration;

namespace GunStoreDesktop.Util;

public static class ConfigUtil
{
    private const string Properties = "/Properties";
    private const string AppsettingsJson = "appsettings.json";

    public static string getConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory + Properties)
            .AddJsonFile(AppsettingsJson)
            .Build();
        return configuration.GetSection("Database")["ConnectionString"];
    }
}