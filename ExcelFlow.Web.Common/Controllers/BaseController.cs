using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExcelFlow.Web.Common.Controllers;

[ApiController]
[Authorize]
public abstract class BaseController : ControllerBase
{
    protected int GetUserId()
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return string.IsNullOrEmpty(id) ? 0 : int.Parse(id);
    }

    protected IActionResult Success(object data) => Ok(new { success = true, data });

    protected IActionResult Error(string message) => BadRequest(new { success = false, message });
}
