using BookApp.Core.DTO;
using System.Threading.Tasks;

namespace BookApp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> LoginAsync(string emailAddress, string password);
    }
}