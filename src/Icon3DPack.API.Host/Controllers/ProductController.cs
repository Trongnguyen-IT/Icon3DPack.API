using Amazon.S3.Model;
using Amazon.S3;
using AutoMapper;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Application.Services.Impl;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess;
using Icon3DPack.API.Host.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Icon3DPack.API.AwsS3.Services;
using Icon3DPack.API.Application.Models.File;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace Icon3DPack.API.Host.Controllers
{
    public class ProductController : BaseAPIController<Product, ProductRequestModel, ProductResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly IStorageService _storageService;
        private readonly IAmazonS3 _s3Client;

        public ProductController(IProductService productService,
            IMapper mapper,
            ILogger<ProductController> logger,
            IAmazonS3 s3Client,
            IStorageService storageService) : base(productService, mapper)
        {
            _mapper = mapper;
            _productService = productService;
            _logger = logger;
            _storageService = storageService;
            _s3Client = s3Client;
        }

        public override async Task<IActionResult> GetAll()
        {
            var paginationResult = await _productService.GetAllPagingAsync();

            var result = new PaginationResult<ProductResponseModel>(_mapper.Map<IReadOnlyList<ProductResponseModel>>(paginationResult.Items),
                paginationResult.PageNumber,
                paginationResult.PageSize,
                paginationResult.TotalCount);

            return Ok(ApiResult<PaginationResult<ProductResponseModel>>.Success(result));
        }

        [HttpPost("product-filter")]
        public async Task<IActionResult> ProductFilter([FromQuery] Dictionary<string, string> filter, int? pageNumber = 1, int? pageSize = 200)
        {
            string name = filter.GetValueOrDefault("name");
            string category = filter.GetValueOrDefault("categoryId");
            string sortOrder = filter.GetValueOrDefault("sortOrder");

            var paginationResult = await _productService.ProductFilter(filter, pageNumber: pageNumber ?? 1, pageSize: pageSize ?? 200);

            var result = new PaginationResult<ProductResponseModel>(_mapper.Map<IReadOnlyList<ProductResponseModel>>(paginationResult.Items),
                paginationResult.PageNumber,
                paginationResult.PageSize,
                paginationResult.TotalCount);

            return Ok(ApiResult<PaginationResult<ProductResponseModel>>.Success(result));
        }

        [HttpPost("{productId}/download-file")]
        [Authorize]
        public async Task<IActionResult> DownloadFile(Guid productId, FileDownloadRequest fileRequest)
        {
            MemoryStream ms = null;
            try
            {
                var bucketExists = await _s3Client.DoesS3BucketExistAsync(fileRequest.BucketName);
                if (!bucketExists) throw new Exception($"Bucket {fileRequest.BucketName} does not exist.");

                using (GetObjectResponse response = await _s3Client.GetObjectAsync(fileRequest.BucketName, fileRequest.Key))
                {
                    //Stream stream = response.ResponseStream;

                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }

                    if (ms is null || ms.ToArray().Length < 1)
                        throw new FileNotFoundException(string.Format("The document '{0}' is not found", fileRequest.Key));

                    await _productService.DownloadFileAsync(productId);

                    // Return the file for download
                    return File(ms.ToArray(), response.Headers.ContentType, fileRequest.Key);
                }
            }
            catch (AmazonS3Exception ex)
            {
                // Handle exception
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
        }
    }
}
