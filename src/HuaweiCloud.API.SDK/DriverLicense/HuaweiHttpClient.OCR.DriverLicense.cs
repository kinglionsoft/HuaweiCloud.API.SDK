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
        #region OCR-DriverLicense

        private string _driverLicenseEndpoint;

        protected string DriverLicenseEndpoint => _driverLicenseEndpoint ??= $"https://ocr.{_options.Region}.myhuaweicloud.com/v2/{_options.ProjectId}/ocr/driver-license";

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0033.html
        /// </summary>
        /// <param name="url">absolute url of the image</param>
        /// <returns></returns>
        public Task<OcrDriverLicenseResponse> OcrDriverLicenseByUrlAsync(string url, bool front, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            return OcrDriverLicenseAsync(new OcrDriverLicenseRequest {Url = url, Front = front}, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0033.html
        /// </summary>
        /// <param name="file">absolute path of the image</param>
        /// <returns></returns>
        public Task<OcrDriverLicenseResponse> OcrDriverLicenseByFileAsync(string file, bool front, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException(nameof(file));

            Utils.EnsureImageFormat(file);

            var image = Utils.GetBase64OfFile(file);

            return OcrDriverLicenseAsync(new OcrDriverLicenseRequest { Image = image, Front = front }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0033.html
        /// </summary>
        /// <param name="fileStream">stream of the image</param>
        /// <returns></returns>
        public async Task<OcrDriverLicenseResponse> OcrDriverLicenseByStreamAsync(Stream fileStream, bool front, CancellationToken cancellation = default)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            var image = await Utils.GetBase64OfStreamAsync(fileStream);

            return await OcrDriverLicenseAsync(new OcrDriverLicenseRequest { Image = image, Front = front }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0033.html
        /// </summary>
        /// <param name="data">bytes of the image</param>
        /// <returns></returns>
        public async Task<OcrDriverLicenseResponse> OcrDriverLicenseByBytesAsync(byte[] data, bool front, CancellationToken cancellation = default)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var image = Convert.ToBase64String(data);

            return await OcrDriverLicenseAsync(new OcrDriverLicenseRequest { Image = image, Front = front }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0033.html
        /// </summary>
        /// <param name="base64">Base64 of the image</param>
        /// <returns></returns>
        public async Task<OcrDriverLicenseResponse> OcrDriverLicenseByBase64Async(string base64, bool front, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(base64)) throw new ArgumentNullException(nameof(base64));

            return await OcrDriverLicenseAsync(new OcrDriverLicenseRequest { Image = base64, Front = front }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0033.html
        /// </summary>
        /// <returns></returns>
        public virtual async Task<OcrDriverLicenseResponse> OcrDriverLicenseAsync(OcrDriverLicenseRequest request, CancellationToken cancellation)
        {
            var result = await PostAsync<OcrDriverLicenseResponse>(DriverLicenseEndpoint, request, cancellation);
            return result;
        }

        #endregion
    }
}