using System;
using HuaweiCloud.API.SDK;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace SdkTest
{
    public class TestBase
    {
        protected readonly ITestOutputHelper Output;
        protected readonly IJsonSerializer JsonSerializer;
        protected readonly HuaweiCloud.API.SDK.HuaweiHttpClient HuaweiHttpClient;

        public TestBase(ITestOutputHelper output)
        {
            Output = output;
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            var sp = new ServiceCollection()
                .AddHuaweiSdk(configuration.GetSection("Huawei").Bind)
                .BuildServiceProvider();

            HuaweiHttpClient = sp.GetRequiredService<HuaweiHttpClient>();
            JsonSerializer = sp.GetRequiredService<IJsonSerializer>();
        }

        protected void WriteJson(object data) => Output.WriteLine(JsonSerializer.Serialize(data));
    }
}
