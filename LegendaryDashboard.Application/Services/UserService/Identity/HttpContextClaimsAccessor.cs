﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Application.Services.UserService.Identity
{
    public sealed class HttpContextClaimsAccessor : IClaimsAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public HttpContextClaimsAccessor(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;
        public async Task<IEnumerable<Claim>> GetCurrentClaims(CancellationToken cancellationToken) => _contextAccessor.HttpContext.User.Claims;
    }
}