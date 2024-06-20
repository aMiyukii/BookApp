namespace BookApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> LoginAsync(string emailAddress, string password);
        Task<int> GetUserIdAsync(string emailAddress);
        Task<bool> CreateUserAsync(string name, string emailAddress, string password);
        Task<bool> UserExistsAsync(string emailAddress);
    }

}

