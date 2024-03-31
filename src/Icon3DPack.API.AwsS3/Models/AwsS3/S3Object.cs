namespace Icon3DPack.API.AwsS3.Models.AwsS3
{
    public class S3Object
    {
        public string Name { get; set; } = null!;
        public MemoryStream InputStream { get; set; } = null!;
    }
}
