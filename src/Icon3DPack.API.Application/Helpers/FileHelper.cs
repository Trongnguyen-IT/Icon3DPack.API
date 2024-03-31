using Icon3DPack.API.AwsS3.Models.AwsS3;
using Microsoft.Extensions.Options;

namespace Icon3DPack.API.Application.Helpers
{
    public static class FileHelper
    {
        public static string ConvertCloudUrl(string url, string clouldfont)
        {
            return $"{clouldfont}/{url}";
        }
    }
}
