using Amazon.S3;
using Amazon.S3.Model;
using Icon3DPack.API.AwsS3.Models.AwsS3;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Icon3DPack.API.AwsS3.Services.Impl
{
    public class StorageService : IStorageService
    {
        private readonly IAmazonS3 _s3Client;

        public StorageService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }
        public async Task<PutObjectResponse> UploadFileAsync(PutObjectRequest  putObjectRequest)
        {
           return  await _s3Client.PutObjectAsync(putObjectRequest);
        }

        public async Task<S3ResponseDto> GetFileByKeyAsync(string bucketName, string key)
        {
            try
            {
                var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
                if (!bucketExists) throw new Exception($"Bucket {bucketName} does not exist.");
                var s3Object = await _s3Client.GetObjectAsync(bucketName, key);

                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = s3Object.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };

                var url = new S3ResponseDto()
                {
                    FileKey = s3Object.Key.ToString(),
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                };
                //return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
                return url;
            }
            catch (Exception ex)
            {
                throw new Exception(JsonConvert.SerializeObject(ex));
            }
        }
    }
}
