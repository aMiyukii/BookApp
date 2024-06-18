using System.Threading.Tasks;

namespace BookApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> LoginAsync(string emailAddress, string password);
    }
}