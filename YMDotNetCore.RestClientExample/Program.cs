// See https://aka.ms/new-console-template for more information
using RestSharp;
using YMDotNetCore.RestClientExample;

Console.WriteLine("Hello, World!");
RestClientExample restClientExample = new RestClientExample();
await restClientExample.RunAsync();