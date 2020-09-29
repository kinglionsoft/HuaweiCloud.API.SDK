using System;
using System.Threading.Tasks;

namespace HuaweiCloud.API.SDK
{
    public interface ITokenManager
    {
        /// <summary>
        /// Cache the requested token. Refresh the token if expired.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> GetTokenAsync(Func<Task<string>> request);
    }
}