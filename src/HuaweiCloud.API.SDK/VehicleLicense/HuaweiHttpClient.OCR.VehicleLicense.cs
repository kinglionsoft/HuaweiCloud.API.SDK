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
        #region OCR-VehicleLicense

        private string _vehicleLicenseEndpoint;

        protected string VehicleLicenseEndpoint => _vehicleLicenseEndpoint ??= $"https://ocr.{_options.Region}.myhuaweicloud.com/v2/{_options.ProjectId}/ocr/vehicle-license";

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0034.html
        /// </summary>
        /// <param name="url">absolute url of the image</param>
        /// <returns></returns>
        public Task<OcrVehicleLicenseResponse> OcrVehicleLicenseByUrlAsync(string url, bool front, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            return OcrVehicleLicenseAsync(new OcrVehicleLicenseRequest {Url = url, Front = front}, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0034.html
        /// </summary>
        /// <param name="file">absolute path of the image</param>
        /// <returns></returns>
        public Task<OcrVehicleLicenseResponse> OcrVehicleLicenseByFileAsync(string file, bool front, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException(nameof(file));

            Utils.EnsureImageFormat(file);

            var image = Utils.GetBase64OfFile(file);

            return OcrVehicleLicenseAsync(new OcrVehicleLicenseRequest { Image = image, Front = front }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0034.html
        /// </summary>
        /// <param name="fileStream">stream of the image</param>
        /// <returns></returns>
        public async Task<OcrVehicleLicenseResponse> OcrVehicleLicenseByStreamAsync(Stream fileStream, bool front, CancellationToken cancellation = default)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            var image = await Utils.GetBase64OfStreamAsync(fileStream);

            return await OcrVehicleLicenseAsync(new OcrVehicleLicenseRequest { Image = image, Front = front }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0034.html
        /// </summary>
        /// <param name="data">bytes of the image</param>
        /// <returns></returns>
        public async Task<OcrVehicleLicenseResponse> OcrVehicleLicenseByBytesAsync(byte[] data, bool front, CancellationToken cancellation = default)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var image = Convert.ToBase64String(data);

            return await OcrVehicleLicenseAsync(new OcrVehicleLicenseRequest { Image = image, Front = front }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0034.html
        /// </summary>
        /// <param name="base64">Base64 of the image</param>
        /// <returns></returns>
        public async Task<OcrVehicleLicenseResponse> OcrVehicleLicenseByBase64Async(string base64, bool front, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(base64)) throw new ArgumentNullException(nameof(base64));

            return await OcrVehicleLicenseAsync(new OcrVehicleLicenseRequest { Image = base64, Front = front }, cancellation);
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0034.html
        /// </summary>
        /// <returns></returns>
        public virtual async Task<OcrVehicleLicenseResponse> OcrVehicleLicenseAsync(OcrVehicleLicenseRequest request, CancellationToken cancellation)
        {
            var result = await PostAsync<OcrVehicleLicenseResponse>(VehicleLicenseEndpoint, request, cancellation);
            return result;
        }

        #endregion
    }
}