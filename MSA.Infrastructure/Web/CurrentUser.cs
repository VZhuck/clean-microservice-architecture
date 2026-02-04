using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MSA.Application.Interfaces;

namespace MSA.Infrastructure.Web;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
