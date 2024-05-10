using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace YMDotNetCore.ConsoleAppHttpClientExample
{
    public class HttpClientExample
    {
        private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7234") };
        private readonly string _blogEndpoint = "/api/Blog";
        public  async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(23);
            //await CreateAsync("ym", "test", "test");
            await UpdateAsync(15,"ymt", "testing", "test");

        }
        public  async Task ReadAsync()
        {
            var response = await _client.GetAsync(_blogEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();               
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

        private async Task EditAsync (int id)
        {
            var response = await _client.GetAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogDto blog = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;
                Console.WriteLine(JsonConvert.SerializeObject(blog));
                Console.WriteLine($"Title=> {blog.BlogTitle}");
                Console.WriteLine($"Author=> {blog.BlogAuthor}");
                Console.WriteLine($"Content=> {blog.BlogContent}");
            }
            else
            {
                string message= await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }

        }

        private async Task CreateAsync(string title,string content,string author)
        {
            BlogDto blog = new BlogDto()
            {
                BlogTitle =title,
                BlogAuthor = author,
                BlogContent = content
            };

            string blogjson = JsonConvert.SerializeObject(blog);
            HttpContent httpcontent = new StringContent(blogjson,Encoding.UTF8,Application.Json);
            var response = await _client.PostAsync(_blogEndpoint,httpcontent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }

        }
        private async Task UpdateAsync(int  id,string title, string content, string author)
        {
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string blogjson = JsonConvert.SerializeObject(blog);
            HttpContent httpcontent = new StringContent(blogjson, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync($"{_blogEndpoint}/{id}", httpcontent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }

        }
        private async Task DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }

        }
    }
}
