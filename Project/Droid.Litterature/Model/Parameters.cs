
using System;
using Microsoft.Extensions.Configuration;

namespace Droid.Litterature
{
    public static class Parameters
    {
        public static IConfiguration Config;

        static Parameters()
        {
            Config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
        }
    }
}
