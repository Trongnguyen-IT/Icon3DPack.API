using Icon3DPack.API.Core.Common;
using Microsoft.AspNetCore.Identity;

namespace Icon3DPack.API.DataAccess.Identity;

public class ApplicationUser : IdentityUser, IAuditedEntity
{
    public string? ImageUrl { get; set; }
    public string? FullName { get; set; }
    public bool ReceiveEmailNotification { get; set; }
    public string? CreatedBy { get ; set ; }
    public DateTime CreatedTime { get ; set ; }
    public string? ModifiedBy { get ; set ; }
    public DateTime? ModifiedTime { get ; set ; }

    //[NotMapped]
    //public string? FullImageUrl
    //{
    //    get
    //    {
    //        string _environment = "Development";
    //        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json")
    //            .AddJsonFile($"appsettings.{_environment}.json",optional:true,reloadOnChange:true)
    //            .Build();

    //        return $"{configuration.GetSection("AwsS3Configuration:CloudFront").Value}/{ImageUrl}";
    //    }
    //}
}
