using System.Net.Http;
using System.Threading.Tasks;
using Xtramile.WheatherApp.ServiceContract.Response;

namespace Xtramile.WheatherApp.ServiceContract
{
    public interface IApiManager
    {
        Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod);

        Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod, StringContent content);
    }
}
