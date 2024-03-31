using Amazon.S3;
using Icon3DPack.API.AwsS3.Models.AwsS3;
using Icon3DPack.API.AwsS3.Services;
using Icon3DPack.API.AwsS3.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Icon3DPack.API.AwsS3
{
    public static class AwsS3DependencyInjection
    {
        public static void AddAwsS3Configuration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            services.AddScoped<IStorageService, StorageService>();
            services.Configure<AwsS3Configuration>((con) => configuration.GetSection("AwsS3Configuration"));
        }
    }
}
