﻿using System.Security.Claims;
using System.Security.Principal;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace WOSRSTest.Server.Logic;

public static class UserHelpers
{
    public static string GetUserId(this IPrincipal principal)
    {
        var claimsIdentity = (ClaimsIdentity)principal.Identity;
        var claim = claimsIdentity.FindFirst(Claims.Subject);

        return claim.Value;
    }
}
