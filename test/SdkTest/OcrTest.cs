using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SdkTest
{
    public class OcrTest : TestBase
    {
        public OcrTest(ITestOutputHelper output) : base(output)
        {
        }

        #region IdCard

        [Theory]
        [InlineData(@"data\idcard-1.jpg")]
        public async Task IdCardByFile(string file)
        {
            var result = await HuaweiHttpClient.IdCardByFileAsync(file);

            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\idcard-1.jpg")]
        public async Task IdCardByStream(string file)
        {
            await using var fs = File.OpenRead(file);
            var result = await HuaweiHttpClient.IdCardByStreamAsync(fs);

            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\idcard-1.jpg")]
        public async Task IdCardByData(string file)
        {
            var bytes = await File.ReadAllBytesAsync(file);
            var result = await HuaweiHttpClient.IdCardByBytesAsync(bytes);

            WriteJson(result);
        }


        [Theory]
        [InlineData("https://ai.bdstatic.com/file/3C8C5B451BB4445697730217EC8648E3")]
        public async Task IdCardByUrl(string url)
        {
            var result = await HuaweiHttpClient.IdCardByUrlAsync(url);

            WriteJson(result);
        }

        #endregion

        #region DriverLicense

        [Theory]
        [InlineData("https://ai.bdstatic.com/file/079EFF6805BB459696EA65933FA1B51C")]
        public async Task OcrDriverLicenseByUrl(string url)
        {
            var result = await HuaweiHttpClient.OcrDriverLicenseByUrlAsync(url, true);

            WriteJson(result);
        }

        #endregion

        #region VehicleLicense

        [Theory]
        [InlineData("https://ai.bdstatic.com/file/545B7A5A048B47E285B883E983EE32A0")]
        public async Task OcrVehicleLicenseByUrl(string url)
        {
            var result = await HuaweiHttpClient.OcrVehicleLicenseByUrlAsync(url, true);

            WriteJson(result);
        }

        #endregion

        #region Table

        [Theory]
        [InlineData("https://support.huaweicloud.com/api-ocr/zh-cn_image_0282767866.png")]
        public async Task OcrTableByUrl(string url)
        {
            var result = await HuaweiHttpClient.OcrTableByUrlAsync(url);

            WriteJson(result);
        }

        #endregion

        #region PlateNumber

        [Theory]
        [InlineData(@"data\PlateNumber-1.jpg")]
        public async Task PlateNumberByFile(string file)
        {
            var result = await HuaweiHttpClient.PlateNumberByFileAsync(file);

            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\PlateNumber-1.jpg")]
        public async Task PlateNumberByStream(string file)
        {
            await using var fs = File.OpenRead(file);
            var result = await HuaweiHttpClient.PlateNumberByStreamAsync(fs);

            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\PlateNumber-1.jpg")]
        public async Task PlateNumberByData(string file)
        {
            var bytes = await File.ReadAllBytesAsync(file);
            var result = await HuaweiHttpClient.PlateNumberByBytesAsync(bytes);

            WriteJson(result);
        }


        [Theory]
        [InlineData("https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fwww.guohuanjiancai.com%2Fgccxlpm%2Fimages%2F2934349b033b5bb561e1b2e231d3d539b600bcfc.jpg&refer=http%3A%2F%2Fwww.guohuanjiancai.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=jpeg?sec=1637831611&t=d498bc38a5062c8fea8892ca51a484c5")]
        public async Task PlateNumberByUrl(string url)
        {
            var result = await HuaweiHttpClient.PlateNumberByUrlAsync(url);

            WriteJson(result);
        }

        #endregion
    }
}