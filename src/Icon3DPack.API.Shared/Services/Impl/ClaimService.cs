﻿using Icon3DPack.API.Shared.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Icon3DPack.API.Shared.Services.Impl
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return GetClaim(ClaimTypes.NameIdentifier);
        }

        public string GetClaim(string key)
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value;
        }
    }
}
