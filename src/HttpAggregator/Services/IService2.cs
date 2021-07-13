using System.Threading.Tasks;

namespace HttpAggregator.Services
{
    public interface IService2
    {
        Task<string[]> GetValuesAsync();
    }
}