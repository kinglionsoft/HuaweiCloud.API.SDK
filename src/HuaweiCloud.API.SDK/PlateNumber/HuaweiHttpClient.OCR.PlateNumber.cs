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
        #region OCR-PlateNumber

        private string _plateNumberEndpoint;

        protected string PlateNumberEndpoint => _plateNumberEndpoint ??= $"https://ocr.{_options.Region}.myhuaweicloud.com/v2/{_options.ProjectId}/ocr/license-plate";

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0040.html
        /// </summary>
        /// <param name="url">absolute url of the image</param>
        /// <returns></returns>
        public Task<PlateNumberOcrResult[]> PlateNumberByUrlAsync(string url, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            return PlateNumberAsync(new { url }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0040.html
        /// </summary>
        /// <param name="file">absolute path of the image</param>
        /// <returns></returns>
        public Task<PlateNumberOcrResult[]> PlateNumberByFileAsync(string file, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException(nameof(file));

            Utils.EnsureImageFormat(file);

            var image = Utils.GetBase64OfFile(file);

            return PlateNumberAsync(new { image }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0040.html
        /// </summary>
        /// <param name="fileStream">stream of the image</param>
        /// <returns></returns>
        public async Task<PlateNumberOcrResult[]> PlateNumberByStreamAsync(Stream fileStream, CancellationToken cancellation = default)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            var image = await Utils.GetBase64OfStreamAsync(fileStream);

            return await PlateNumberAsync(new { image }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0040.html
        /// </summary>
        /// <param name="data">bytes of the image</param>
        /// <returns></returns>
        public async Task<PlateNumberOcrResult[]> PlateNumberByBytesAsync(byte[] data, CancellationToken cancellation = default)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var image = Convert.ToBase64String(data);

            return await PlateNumberAsync(new { image }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0040.html
        /// </summary>
        /// <param name="base64">Base64 of the image</param>
        /// <returns></returns>
        public async Task<PlateNumberOcrResult[]> PlateNumberByBase64Async(string base64, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(base64)) throw new ArgumentNullException(nameof(base64));

            return await PlateNumberAsync(new { image = base64 }, cancellation);
        }

        protected virtual async Task<PlateNumberOcrResult[]> PlateNumberAsync(object body, CancellationToken cancellation)
        {
            var result = await PostAsync<PlateNumberOcrResult[]>(PlateNumberEndpoint, body, cancellation);
            return result;
        }

        #endregion
    }
}