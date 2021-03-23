using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ReversiMvcApp.Controllers
{
    public class ApiController : Controller
    {
        private readonly string baseUrl = "https://localhost:5001/api/";

        public async Task<IEnumerable<T>> GetList<T>(string path)
        {
            IEnumerable<T> result = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                var task = client.GetAsync(path);
                task.Wait();

                var taskResult = task.Result;
                if (taskResult.IsSuccessStatusCode)
                {
                    var readResult = taskResult.Content.ReadAsStringAsync();
                    readResult.Wait();

                    result = JsonConvert.DeserializeObject<IEnumerable<T>>(readResult.Result);
                }
                else
                {
                    result = Enumerable.Empty<T>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }

            return result;
        }

        public async Task<T> GetAsync<T>(string path)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                var result = client.GetAsync(path).Result;
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsAsync<T>();
                }
                throw new TaskCanceledException();
            }
        }

        public async Task<T> PostAsync<T>(T data, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                    var result = client.PostAsJsonAsync(path, data).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsAsync<T>();
                    }
                    throw new TaskCanceledException();
            }
        }

        public async Task<T> PutAsync<T>(T data, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                var result = client.PutAsJsonAsync(path, data).Result;
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsAsync<T>();
                }
                throw new TaskCanceledException();
            }
        }
    }
}
