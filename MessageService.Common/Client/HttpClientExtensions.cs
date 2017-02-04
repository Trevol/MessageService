using System.Net.Http;
using System.Threading.Tasks;

namespace MessageService.Common.Client
{
    public static class HttpClientExtensions
    {
        /*public static async Task<T> GetAsyncEx<T>(this HttpClient client, string path)
        {
            var responce = await client.GetAsync(path);
            responce.EnsureSuccessStatusCode();
            return await responce.Content.ReadAsAsync<T>();            
        }*/

        public static T Get<T>(this HttpClient client, string path)
        {
            var task = client.GetAsync(path);
            task.Wait();
            task.Result.EnsureSuccessStatusCode();
            var readAsAsync = task.Result.Content.ReadAsAsync<T>();
            readAsAsync.Wait();
            return readAsAsync.Result;
        }

        public static TOut Post<TIn, TOut>(this HttpClient client, TIn value, string path)
        {
            var postTask = client.PostAsJsonAsync(path, value);
            postTask.Wait();
            postTask.Result.EnsureSuccessStatusCode();
            var readAsAsync = postTask.Result.Content.ReadAsAsync<TOut>();
            readAsAsync.Wait();
            return readAsAsync.Result;
        }
    }
}