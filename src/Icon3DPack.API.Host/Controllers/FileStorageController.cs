using Amazon.S3.Model;
using Amazon.S3;
using Icon3DPack.API.AwsS3.Models.AwsS3;
using Icon3DPack.API.AwsS3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities.Collections;
using Icon3DPack.API.Application.Models;

namespace Icon3DPack.API.Host.Controllers
{
    //[Authorize]
    public class FileStorageController : ApiController
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IStorageService _storageService;

        public FileStorageController(IAmazonS3 s3Client, IStorageService storageService)
        {
            _s3Client = s3Client;
            _storageService = storageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, string bucketName, string? prefix)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");

            var fileExt = Path.GetExtension(file.FileName);
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{Guid.NewGuid()}{fileExt}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);

            await _s3Client.PutObjectAsync(request);

            return Ok(ApiResult<string>.Success(request.Key));

            //For private file
            //return Ok(ApiResult<S3ResponseDto>.Success(new S3ResponseDto()
            //{
            //    FileKey = request.Key.ToString(),
            //    PresignedUrl = _s3Client.GetPreSignedURL(new GetPreSignedUrlRequest()
            //    {
            //        BucketName = bucketName,
            //        Key = request.Key,
            //        Expires = DateTime.UtcNow.AddMinutes(5)
            //    }),
            //}));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllFilesAsync(string bucketName, string? prefix)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
            var request = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = prefix
            };
            var result = await _s3Client.ListObjectsV2Async(request);
            var s3Objects = result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };
                return new S3ResponseDto()
                {
                    FileKey = s.Key.ToString(),
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                };
            });

            return Ok(s3Objects);
        }

        [HttpGet("get-by-key")]
        public async Task<IActionResult> GetFileByKeyAsync(string bucketName, string key)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
            var s3Object = await _s3Client.GetObjectAsync(bucketName, key);

            var urlRequest = new GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = s3Object.Key,
                Expires = DateTime.UtcNow.AddMinutes(5)
            };

            var url = new S3ResponseDto()
            {
                FileKey = s3Object.Key.ToString(),
                PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
            };
            //return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
            return Ok(ApiResult<S3ResponseDto>.Success(url));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFileAsync(string bucketName, string key)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist");
            await _s3Client.DeleteObjectAsync(bucketName, key);
            return NoContent();
        }
    }
}
