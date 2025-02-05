using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Dto;

namespace WEBAPI.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Register(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}