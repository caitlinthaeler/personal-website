using System.Threading.Tasks;

namespace data_api.Services;
public interface IDataService<T> where T : class
{
    Task<List<T>> GetAllDataAsync();
    Task<T> GetDataByIdAsync(string id);
    Task CreateDataAsync(T data);
    Task UpdateDataAsync(string id, T data);
    Task DeleteDataAsync(string id);
}