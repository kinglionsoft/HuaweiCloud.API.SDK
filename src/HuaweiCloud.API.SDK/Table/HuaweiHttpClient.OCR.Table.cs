using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HuaweiCloud.API.SDK.Internal;
using HuaweiCloud.API.SDK.Models;

namespace HuaweiCloud.API.SDK
{
    public partial class HuaweiHttpClient
    {
        #region OCR-General-Table

        private string _tableEndpoint;

        protected string TableEndpoint => _tableEndpoint ??= $"https://ocr.{_options.Region}.myhuaweicloud.com/v2/{_options.ProjectId}/ocr/id-card";

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0031.html
        /// </summary>
        /// <param name="url">absolute url of the image</param>
        /// <returns></returns>
        public Task<OcrTableResponse> OcrTableByUrlAsync(string url, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            return OcrTableAsync(new OcrTableRequest
            {
                Url = url
            }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0031.html
        /// </summary>
        /// <param name="file">absolute path of the image</param>
        /// <returns></returns>
        public Task<OcrTableResponse> OcrTableByFileAsync(string file, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException(nameof(file));

            Utils.EnsureImageFormat(file);

            var image = Utils.GetBase64OfFile(file);

            return OcrTableAsync(new OcrTableRequest { Image = image }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0031.html
        /// </summary>
        /// <param name="fileStream">stream of the image</param>
        /// <returns></returns>
        public async Task<OcrTableResponse> OcrTableByStreamAsync(Stream fileStream, CancellationToken cancellation = default)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            var image = await Utils.GetBase64OfStreamAsync(fileStream);

            return await OcrTableAsync(new OcrTableRequest { Image = image }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0031.html
        /// </summary>
        /// <param name="data">bytes of the image</param>
        /// <returns></returns>
        public async Task<OcrTableResponse> OcrTableByBytesAsync(byte[] data, CancellationToken cancellation = default)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var image = Convert.ToBase64String(data);

            return await OcrTableAsync(new OcrTableRequest { Image = image }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0031.html
        /// </summary>
        /// <param name="base64">Base64 of the image</param>
        /// <returns></returns>
        public async Task<OcrTableResponse> OcrTableByBase64Async(string base64, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(base64)) throw new ArgumentNullException(nameof(base64));

            return await OcrTableAsync(new OcrTableRequest { Image = base64 }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0031.html
        /// </summary>
        /// <returns></returns>
        public virtual async Task<OcrTableResponse> OcrTableAsync(OcrTableRequest request, CancellationToken cancellation)
        {
            var result = await PostAsync<OcrTableResponse>(TableEndpoint, request, cancellation);
            return result;
        }

        #endregion
    }
}