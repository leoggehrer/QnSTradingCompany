//@QnSCodeCopy
//MdStart
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Modules.SessionStorage
{
    public interface IProtectedBrowserStorage
    {
        ValueTask DeleteAsync(string key);
        ValueTask<T> GetAsync<T>(string key);
        ValueTask<T> GetAsync<T>(string purpose, string key);
        ValueTask SetAsync(string key, object value);
        ValueTask SetAsync(string purpose, string key, object value);
    }
}
//MdEnd
