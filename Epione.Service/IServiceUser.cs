using ServicePattern;
using Epione.Domain;
using System.Threading.Tasks;

namespace Epione.Service
{
    public interface IServiceUser : IService<user>
    {

         Task<user> getUserByIdAsync(int userId);
         Task<user> loginUserAsync(string userName, string password);
         Task<user> getUserByEmailAsync(string email);
         Task<bool> RegisterUser(user user);
    }
}