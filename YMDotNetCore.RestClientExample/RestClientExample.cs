using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace YMDotNetCore.RestClientExample
{
    internal  class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7234")) ;
        private readonly string _blogEndpoint = "/api/Blog";
        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(23);
            //await CreateAsync("ym", "test", "test");
            await UpdateAsync(15, "ymt", "testing", "test");

        }
        public async Task ReadAsync()
        {
            RestRequest restRequest = new RestRequest(_blogEndpoint); 
            var response = await _client.GetAsync(restRequest);

            //RestRequest restRequest = new RestRequest(_blogEndpoint, Method.Get);
            //var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
                foreach (var blog in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(blog));
                    Console.WriteLine($"Title=> {blog.BlogTitle}");
                    Console.WriteLine($"Author=> {blog.BlogAuthor}");
                    Console.WriteLine($"Content=> {blog.BlogContent}");
                }

            }
        }

        private async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}",Method.Get);
            var response = await _client.GetAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
                BlogDto blog = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;
                Console.WriteLine(JsonConvert.SerializeObject(blog));
                Console.WriteLine($"Title=> {blog.BlogTitle}");
                Console.WriteLine($"Author=> {blog.BlogAuthor}");
                Console.WriteLine($"Content=> {blog.BlogContent}");
            }
            else
            {
                string message =  response.Content!;
                Console.WriteLine(message);
            }

        }

        private async Task CreateAsync(string title, string content, string author)
        {
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            RestRequest restRequest = new RestRequest(_blogEndpoint, Method.Post);
            restRequest.AddJsonBody(blog);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }
        private async Task UpdateAsync(int id, string title, string content, string author)
        {
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string blogjson = JsonConvert.SerializeObject(blog);
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
            restRequest.AddJsonBody(blog);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }
        private async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message =  response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }
    }
}
