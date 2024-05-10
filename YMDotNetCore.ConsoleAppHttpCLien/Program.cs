// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using YMDotNetCore.ConsoleAppHttpClientExample;

HttpClientExample httpclientexample = new HttpClientExample();
await httpclientexample.RunAsync();