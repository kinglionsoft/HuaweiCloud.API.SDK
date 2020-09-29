using System;
using System.Threading.Tasks;

namespace HuaweiCloud.API.SDK.Internal
{
    internal class SimpleTokenManager: ITokenManager
    {
        private DateTime _expireAt;

        private string _token;

        public async Task<string> GetTokenAsync(Func<Task<string>> request)
        {
            /*
             * Only for single instance. Otherwise a distribute cache (e.g. Redis) is recommended.
             */

            if (IsExpired())
            {
                _token = await request();
                _expireAt = DateTime.Now.AddHours(24);
            }

            return _token;
        }

        private bool IsExpired()
        {
            return string.IsNullOrEmpty(_token)
                   || DateTime.Now.AddSeconds(200) >= _expireAt;
        }
    }
}