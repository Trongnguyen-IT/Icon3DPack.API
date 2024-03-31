using Amazon.S3.Model;
using Icon3DPack.API.AwsS3.Models.AwsS3;

namespace Icon3DPack.API.AwsS3.Services
{
    public interface IStorageService
    {
        Task<PutObjectResponse> UploadFileAsync(PutObjectRequest putObjectRequest);
        Task<S3ResponseDto> GetFileByKeyAsync(string bucketName, string key);
    }
}
