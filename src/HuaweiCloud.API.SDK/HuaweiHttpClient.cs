using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HuaweiCloud.API.SDK.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HuaweiCloud.API.SDK
{
    public partial class HuaweiHttpClient
    {
        private const string TokenHeaderName = "X-Auth-Token";

        private readonly HttpClient _httpClient;
        private readonly HuaweiSdkOptions _options;
        private readonly ILogger _logger;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ITokenManager _tokenManager;

        public HuaweiHttpClient(HttpClient httpClient,
            IOptions<HuaweiSdkOptions> optionAccessor,
            ILogger<HuaweiHttpClient> logger,
            IJsonSerializer jsonSerializer,
            ITokenManager tokenManager)
        {
            _httpClient = httpClient;
            _logger = logger;
            _jsonSerializer = jsonSerializer;
            _tokenManager = tokenManager;
            _options = optionAccessor.Value;

            _httpClient.Timeout = TimeSpan.FromMilliseconds(_options.Timeout);
        }

        #region Helpers

        protected virtual async Task<T> PostAsync<T>(string url, object body, CancellationToken cancellation)
        {
            var json = _jsonSerializer.Serialize(body);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            await EnsureTokenAsync();
            using var response = await _httpClient.PostAsync(url, content, cancellation);
            var responseData = await response.Content.ReadAsByteArrayAsync();
            var apiResult = _jsonSerializer.Deserialize<ApiResult<T>>(new ReadOnlySpan<byte>(responseData));
            if (apiResult.Success)
            {
                return apiResult.Result;
            }
            throw new HuaweiException($"url: {url}, error_code: {apiResult.ErrorCode}, error_msg: {apiResult.ErrorMsg}");
        }

        #endregion

        #region IAM

        internal async Task EnsureTokenAsync()
        {
            var token = await _tokenManager.GetTokenAsync(GetTokenAsync);
            _httpClient.DefaultRequestHeaders.Remove(TokenHeaderName);
            _httpClient.DefaultRequestHeaders.Add(TokenHeaderName, token);
        }

        internal async Task<string> GetTokenAsync()
        {
            /*
             * https://support.huaweicloud.com/api-ocr/ocr_03_0005.html
             */
            var body = new
            {
                auth = new
                {
                    identity = new
                    {
                        methods = new[]
                        {
                            "password"
                        },
                        password = new
                        {
                            user = new
                            {
                                name = _options.UserName,
                                password = _options.Password,
                                domain = new
                                {
                                    name = _options.Domain
                                }
                            }
                        }
                    },
                    scope = new
                    {
                        project = new
                        {
                            id = _options.ProjectId,
                            name = _options.ProjectName
                        }
                    }
                }
            };
            var json = _jsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://iam.{_options.Region}.myhuaweicloud.com/v3/auth/tokens?nocatalog=true";

            var response = await _httpClient.PostAsync(url, content);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                var token = response.Headers.GetValues("X-Subject-Token").First();
                return token;
            }
            throw new HuaweiException($"Request token failed. StatusCode: {(int)response.StatusCode} see https://support.huaweicloud.com/api-iam/iam_30_0001.html");
        }

        #endregion
    }
}