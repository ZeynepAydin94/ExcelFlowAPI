
using System;

namespace ExcelFlow.Core.Interfaces
{
    public interface ICurrentUserService
    {
        int? GetUserId(); // int? çünkü kullanıcı oturum açmamış olabilir
    }
}