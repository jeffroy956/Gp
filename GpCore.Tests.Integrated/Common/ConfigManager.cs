using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Tests.Integrated.Common
{
    public class ConfigManager
    {
        private static IConfigurationRoot _appSettings;
        public static IConfigurationRoot AppSettings
        {
            get
            {
                if (_appSettings == null)
                {
                    var builder = new ConfigurationBuilder();
                    _appSettings = builder.AddJsonFile("appSettings.json").Build();
                }
                return _appSettings;
            }
        }

    }
}
