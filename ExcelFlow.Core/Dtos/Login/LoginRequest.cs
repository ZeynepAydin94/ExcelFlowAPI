using System;

namespace ExcelFlow.Core.Dtos.Login;

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
