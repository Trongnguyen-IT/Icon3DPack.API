using Microsoft.AspNetCore.Identity;

namespace Icon3DPack.API.DataAccess.Identity;

public class ApplicationUser : IdentityUser {
    public string? ImageUrl { get; set; }
    public string? FullName { get; set; }
    public bool ReceiveEmailNotification { get; set; }

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
