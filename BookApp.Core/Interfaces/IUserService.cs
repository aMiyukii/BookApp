namespace BookApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> LoginAsync(string emailAddress, string password);
        Task<int> GetUserIdAsync(string emailAddress);
    }
}

