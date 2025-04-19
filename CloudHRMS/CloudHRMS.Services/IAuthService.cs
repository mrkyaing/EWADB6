using CloudHRMS.Domain.Models.ViewModels;

namespace CloudHRMS.Services {
    public interface IAuthService {
        Task<(int, string)> AuthLogin(AuthViewModel authViewModel);
    }
}
