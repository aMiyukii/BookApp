namespace BookApp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> LoginAsync(string emailaddress, string password);
        Task<int> GetUserIdByEmailAsync(string emailaddress);
        Task<bool> CreateUserAsync(string name, string emailaddress, string password);
        Task<bool> UserExistsAsync(string emailaddress);
    }
}