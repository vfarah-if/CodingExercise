using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Exercise.Domain.Tests
{
    public static class TestHelper
    {
        public static IConfigurationRoot GetAppSettings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }
    }
}
