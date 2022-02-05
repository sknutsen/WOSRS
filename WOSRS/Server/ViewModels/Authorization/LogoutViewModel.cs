using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WOSRS.Server.ViewModels.Authorization;

public class LogoutViewModel
{
    [BindNever]
    public string RequestId { get; set; }
}
