using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YMDotNetCore.RestApiWithNLayer.Features.MinTheinKha;

namespace YMDotNetCore.RestApiWithNLayer.Features.Dictionary
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private async Task<Dictionary> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("MyanmarProverbs.json");
            var model = JsonConvert.DeserializeObject<Dictionary>(jsonStr);
            return model;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var result = await GetDataAsync();
            return Ok(result.Tbl_MMProverbs);
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> Getproverbstitle(string title)
        {
            var result = await GetDataAsync();
            var subtitle = result.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == title);
            var id = subtitle.TitleId;
            var detailsubtitle = result.Tbl_MMProverbs.Where(x => x.TitleId == id);
            var titlelist = new List<string>();
            foreach (var item in detailsubtitle) 
            {
                titlelist.Add(item.ProverbName);
            }
            return Ok(titlelist);
        }
        
        [HttpGet("Title/{titlename}")]
        public async Task<IActionResult> GetproverbDetail(string titlename)
        {
            var result = await GetDataAsync();
            var  title = result.Tbl_MMProverbs.FirstOrDefault(x => x.ProverbName == titlename);
            return Ok(title);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Tbl_Mmproverbstitle model)
        {
            var result = await GetDataAsync();
            var title = result.Tbl_MMProverbsTitle.Append(model);
            return Ok(title);
        }

    }
   
    public class Dictionary
    {
        public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
        public Tbl_Mmproverbs[] Tbl_MMProverbs { get; set; }
    }

    public class Tbl_Mmproverbstitle
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class Tbl_Mmproverbs
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
        public string ProverbDesp { get; set; }
    }

}
