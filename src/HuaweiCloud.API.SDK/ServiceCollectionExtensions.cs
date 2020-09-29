using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using HuaweiCloud.API.SDK.Internal;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace HuaweiCloud.API.SDK
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHuaweiSdk(this IServiceCollection services, Action<HuaweiSdkOptions> configure)
        {
            Debug.Assert(configure != null);

            var options = new HuaweiSdkOptions();

            configure(options);

            services.Configure<HuaweiSdkOptions>(opt => opt.CloneFrom(options));

            var httpClientBuilder = services.AddHttpClient<HuaweiHttpClient>();

            if (options.DisableServerSslValidation)
            {
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                });
            }

            if (options.RetryDurations?.Length > 0)
            {
                httpClientBuilder.AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(options.RetryDurations.Select(x => TimeSpan.FromMilliseconds(x))));
            }

            services.AddSingleton<IJsonSerializer, JsonSerializer>()
                .AddSingleton<ITokenManager, SimpleTokenManager>()
                ;

            return services;
        }
    }
}
