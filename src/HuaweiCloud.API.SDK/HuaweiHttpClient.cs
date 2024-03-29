﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
        private readonly ITokenManager _tokenManager;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public HuaweiHttpClient(HttpClient httpClient,
            IOptions<HuaweiSdkOptions> optionAccessor,
            ILogger<HuaweiHttpClient> logger,
            ITokenManager tokenManager,
            JsonSerializerOptions? jsonSerializerOptions = null)
        {
            _httpClient = httpClient;
            _logger = logger;
            _tokenManager = tokenManager;
            _jsonSerializerOptions = jsonSerializerOptions ?? new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };
            _options = optionAccessor.Value;

            _httpClient.Timeout = TimeSpan.FromMilliseconds(_options.Timeout);
        }

        #region Helpers

        protected virtual async Task<T> PostAsync<T>(string url, object body, CancellationToken cancellation)
        {
            await EnsureTokenAsync();
            using var response = await _httpClient.PostAsJsonAsync(url, body, _jsonSerializerOptions, cancellation);
#if DEBUG
            var responseData = await response.Content.ReadAsStringAsync(cancellation);
            _logger.LogDebug(responseData);
#else
            var responseData = await response.Content.ReadAsByteArrayAsync(cancellation);
#endif

            try
            {
                var apiResult = Deserialize<ApiResult<T>>(responseData);
                if (apiResult!.Success)
                {
                    return apiResult.Result;
                }
                throw new HuaweiException($"url: {url}, error_code: {apiResult.ErrorCode}, error_msg: {apiResult.ErrorMsg}");

            }
            catch (JsonException ex)
            {
#if DEBUG
                var json = responseData;
#else
                var json = System.Text.Encoding.UTF8.GetString(responseData);
#endif
                _logger.LogError("反序列化失败，Error={error}, Json={json}", ex.Message, json);
                throw;
            }
        }

        protected virtual string Serialize(object data) => JsonSerializer.Serialize(data, _jsonSerializerOptions);

        protected virtual T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

        protected virtual T? Deserialize<T>(byte[] json) => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

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

            var url = $"https://iam.{_options.Region}.myhuaweicloud.com/v3/auth/tokens?nocatalog=true";

            var response = await _httpClient.PostAsJsonAsync(url, body, _jsonSerializerOptions, default);
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