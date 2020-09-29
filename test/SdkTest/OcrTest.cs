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
    }
}