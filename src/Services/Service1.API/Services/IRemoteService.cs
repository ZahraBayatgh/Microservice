using System.Threading.Tasks;

namespace Service1.API.Services
{
    public interface IRemoteService
    {
        Task<string[]> GetValuesAsync();
    }
}