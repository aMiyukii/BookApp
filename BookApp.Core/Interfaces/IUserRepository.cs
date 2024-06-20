namespace BookApp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> LoginAsync(string emailAddress, string password);
        Task<int> GetUserIdByEmailAsync(string emailAddress);
    }
}