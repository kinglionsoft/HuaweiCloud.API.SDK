using System;
using System.IO;
using System.Threading.Tasks;
using HuaweiCloud.API.SDK.Internal;
using HuaweiCloud.API.SDK.Models;

namespace HuaweiCloud.API.SDK
{
    public partial class HuaweiHttpClient
    {
        #region OCR-IdCard

        private string _idCardEndpoint;

        protected string IdCardEndpoint => _idCardEndpoint ??= $"https://ocr.{_options.Region}.myhuaweicloud.com/v1.0/ocr/id-card";

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0030.html
        /// </summary>
        /// <param name="url">absolute url of the image</param>
        /// <param name="side">front or back</param>
        /// <returns></returns>
        public Task<IdCardOcrResult> IdCardByUrlAsync(string url, string side = "front")
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            if ("front" != side && "back" != side) throw new ArgumentOutOfRangeException(nameof(side), side, "must be front or back");

            return IdCardAsync(new { url, side });
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0030.html
        /// </summary>
        /// <param name="file">absolute path of the image</param>
        /// <param name="side">front or back</param>
        /// <returns></returns>
        public Task<IdCardOcrResult> IdCardByFileAsync(string file, string side = "front")
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException(nameof(file));

            if ("front" != side && "back" != side) throw new ArgumentOutOfRangeException(nameof(side), side, "must be front or back");

            Utils.EnsureImageFormat(file);

            var image = Utils.GetBase64OfFile(file);

            return IdCardAsync(new { image, side });
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0030.html
        /// </summary>
        /// <param name="fileStream">stream of the image</param>
        /// <param name="side">front or back</param>
        /// <returns></returns>
        public async Task<IdCardOcrResult> IdCardByStreamAsync(Stream fileStream, string side = "front")
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            if ("front" != side && "back" != side) throw new ArgumentOutOfRangeException(nameof(side), side, "must be front or back");

            var image = await Utils.GetBase64OfStreamAsync(fileStream);

            return await IdCardAsync(new { image, side });
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0030.html
        /// </summary>
        /// <param name="data">bytes of the image</param>
        /// <param name="side">front or back</param>
        /// <returns></returns>
        public async Task<IdCardOcrResult> IdCardByBytesAsync(byte[] data, string side = "front")
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if ("front" != side && "back" != side) throw new ArgumentOutOfRangeException(nameof(side), side, "must be front or back");
            
            var image = Convert.ToBase64String(data);

            return await IdCardAsync(new { image, side });
        }

        /// <summary>
        /// refer https://support.huaweicloud.com/api-ocr/ocr_03_0030.html
        /// </summary>
        /// <param name="base64">Base64 of the image</param>
        /// <param name="side">front or back</param>
        /// <returns></returns>
        public async Task<IdCardOcrResult> IdCardByBase64Async(string base64, string side = "front")
        {
            if (string.IsNullOrEmpty(base64)) throw new ArgumentNullException(nameof(base64));
            if ("front" != side && "back" != side) throw new ArgumentOutOfRangeException(nameof(side), side, "must be front or back");

            return await IdCardAsync(new { image = base64, side });
        }

        protected virtual async Task<IdCardOcrResult> IdCardAsync(object body)
        {
            var result = await PostAsync<IdCardOcrResult>(IdCardEndpoint, body);
            return result;
        }

        #endregion
    }
}