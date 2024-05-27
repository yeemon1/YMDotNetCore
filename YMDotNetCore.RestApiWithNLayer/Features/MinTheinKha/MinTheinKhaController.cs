using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace YMDotNetCore.RestApiWithNLayer.Features.MinTheinKha
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinTheinKhaController : ControllerBase
    {
        private async Task<LatHtaukBayDin> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonStr); 
            return model;
        }
       
        [HttpGet("question")]
        public async Task<IActionResult> Questions()
        {
            var model = await GetDataAsync();
            return Ok(model.questions);
        }
        [HttpGet]
        public async Task<IActionResult> NumberList()
        {
            var model = await GetDataAsync();
            return Ok(model.numberList);
        }
        [HttpGet("{questionNo}/{no}")]
        public async Task<IActionResult> Answers(int questionNo,int no)
        {
            var model = await GetDataAsync();
            return Ok(model.answers.FirstOrDefault(x => x.questionNo == questionNo && x.answerNo == no));
        }
       
    }

    public class LatHtaukBayDin
    {
        public Question[] questions { get; set; }
        public Answer[] answers { get; set; }
        public string[] numberList { get; set; }
    }

    public class Question
    {
        public int questionNo { get; set; }
        public string questionName { get; set; }
    }

    public class Answer
    {
        public int questionNo { get; set; }
        public int answerNo { get; set; }
        public string answerResult { get; set; }
    }


}
