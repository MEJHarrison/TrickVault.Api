﻿using System.Security.Claims;

namespace TrickVault.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
    }
}
