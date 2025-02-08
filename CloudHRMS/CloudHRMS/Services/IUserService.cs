namespace CloudHRMS.Services {
    public interface IUserService {
        Task<string> CreateUserAsync(string userName, string password, string email);
        Task<IList<string>> GetRolesByUserId(string userId);
    }
}
