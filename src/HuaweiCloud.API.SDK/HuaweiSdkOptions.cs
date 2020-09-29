namespace HuaweiCloud.API.SDK
{
    public sealed class HuaweiSdkOptions
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Domain { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Region { get; set; }

        /// <summary>
        /// Request timeout, in ms.
        /// </summary>
        public int Timeout { get; set; } = 10_000;

        /// <summary>
        /// Retry durations in ms, <see cref="Microsoft.Extensions.Http.Polly"/>
        /// </summary>
        public int[] RetryDurations { get; set; }

        public void CloneFrom(HuaweiSdkOptions options)
        {
            UserName = options.UserName;
            Password = options.Password;
            Domain = options.Domain;
            ProjectId = options.ProjectId;
            ProjectName = options.ProjectName;
            Region = options.Region;
            Timeout = options.Timeout;
            RetryDurations = options.RetryDurations;
        }
    }
}