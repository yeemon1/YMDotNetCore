using Refit;
using YMDotNetCore.ConsoleApp.RefitExmample;

var service = RestService.For<IBlogApi>("https://localhost:7111");
var lst = await service.Get();
foreach(var item in lst)
{
    Console.WriteLine(item.BlogTitle);
    Console.WriteLine(item.BlogAuthor);
    Console.WriteLine(item.BlogContent);
}