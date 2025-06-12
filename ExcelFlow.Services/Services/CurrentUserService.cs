// ExcelFlow.Auth.API/Services/CurrentUserService.cs (Yeni bir klasör veya mevcut bir yere ekleyebilirsiniz)

using System.Security.Claims;
using ExcelFlow.Core.Interfaces; // ICurrentUserService için
using Microsoft.AspNetCore.Http; // HttpContextAccessor için

namespace ExcelFlow.Services.Services // Namespace'i projenize göre ayarlayın
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetUserId()
        {
            // HttpContext'in null olabileceğini veya kullanıcının oturum açmamış olabileceğini kontrol edin
            if (_httpContextAccessor.HttpContext?.User == null)
            {
                return null;
            }

            // ClaimTypes.NameIdentifier, genellikle kullanıcı ID'sini tutar.
            // Bu claim'in türü, kimlik doğrulama mekanizmanıza (örneğin JWT) bağlıdır.
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            return null; // Kullanıcı ID'si bulunamadı veya parse edilemedi
        }
    }
}