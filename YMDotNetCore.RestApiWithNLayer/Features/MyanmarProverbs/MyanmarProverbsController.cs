using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace YMDotNetCore.RestApiWithNLayer.Features.MyanmarProverbs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarProverbsController : ControllerBase
    {

        private  async Task<Tbl_Mmproverbs> GetDataFromApi()
        {
            HttpClient client = new HttpClient();
            var response = await  client.GetAsync("https://raw.githubusercontent.com/sannlynnhtun-coding/Myanmar-Proverbs/main/MyanmarProverbs.json");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
               // string Str = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<Tbl_Mmproverbs>(jsonStr);
                return model;
            }
            return null;
        }

        public async Task<IActionResult> Get()
        {
            var model = await GetDataFromApi();
            return Ok(model.Tbl_MMProverbsTitle);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var model = await GetDataFromApi();
            
            return Ok(model.Tbl_MMProverbsTitle);
        }

        public class Tbl_Mmproverbs
        {
            public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
            public Tbl_MmproverbsDetail[] Tbl_MMProverbs { get; set; }
        }

        public class Tbl_Mmproverbstitle
        {
            public int TitleId { get; set; }
            public string TitleName { get; set; }
        }

        public class Tbl_MmproverbsDetail
        {
            public int TitleId { get; set; }
            public int ProverbId { get; set; }
            public string ProverbName { get; set; }
            public string ProverbDesp { get; set; }
        }

    }
}
