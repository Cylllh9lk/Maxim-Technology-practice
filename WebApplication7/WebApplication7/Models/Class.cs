using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public static class AppConfig
    {
        public static IConfiguration Configuration { get; private set; }

        static AppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
    }

}
