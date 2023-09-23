using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WOSRSTest.Server.ViewModels.Authorization;

public class LogoutViewModel
{
    [BindNever]
    public string RequestId { get; set; }
}
