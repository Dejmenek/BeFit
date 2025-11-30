using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeFit.Areas.Dashboard.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
