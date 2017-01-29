using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace GpCore.Tests.Integrated.Common
{
    public class AppSettingsFixture
    {
        [Fact]
        public void GetGpConnectionString()
        {
            Assert.Equal("Data Source=localhost\\sqlexpress;Database=GP_Test;Integrated Security=True;",
                ConfigManager.AppSettings.GetConnectionString("gp"));
        }
    }
}
