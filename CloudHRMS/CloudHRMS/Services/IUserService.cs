namespace CloudHRMS.Services {
    public interface IUserService {
        //asynchronous programming apporach(parellel programming )
        //Thread 
        Task<string> CreateUserAsync(string userName, string password, string email);
        Task<IList<string>> GetRolesByUserIdAync(string userId);
    }
}
