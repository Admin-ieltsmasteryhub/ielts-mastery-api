using IeltsApp.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Net;
using System.Text;
using System;
using Newtonsoft.Json;
using static IeltsApp.DataAccess.Models.EssayCheckerDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IeltsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeController : ControllerBase
    {
        private readonly IOpenAIProxy _openAIProxy;

        public PracticeController(IOpenAIProxy openAIProxy)
        {
            _openAIProxy = openAIProxy;
        }
        // GET: api/<PracticeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PracticeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PracticeController>
        [Route("/essaychecker")]
        [HttpPost]
        public async Task<IActionResult> SendMessageToOpenAI([FromBody] string message)
        {
            var retur = await Evaluate(message);
            var response = JsonConvert.DeserializeObject<EssayChecker>(retur);

            return Ok(response);
        }

        // PUT api/<PracticeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PracticeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        static async Task<string> Evaluate(string message)
        {
            try
            {
                string url = "https://generativelanguage.googleapis.com/v1beta2/models/text-bison-001:generateText";
                string prompt = "Please evaluate the following essay based on IELTS standards. Consider the essay's adherence to the prompt, coherence and cohesion in its organization, appropriate and varied lexical resource usage, accurate grammatical range and structure, and effective argumentation and perspective. Evaluate the introduction's clarity and conclusion's summarization. Assess the essay's length and paragraph structure, incorporation of cohesive devices, and relevancy of ideas supported by relevant examples. The holistic assessment across these criteria will contribute to the overall band score, which ranges from 1 to 9. Please provide specific feedback on each criterion to aid the writer's understanding and improvement. Your insights will be highly valuable in enhancing their writing skills and provide 1: Band Score 2: Five bullet point in which essay can be improved and written in the following format:- Band Score = value,Tell me the drawbacks in the essay if any = value .Essay:";
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("x-goog-api-key", "AIzaSyCrofdxbquVgChuVuHdi204PQwrK6mVQQA");

                    string jsonData = $"{{\"prompt\": {{\"text\": \"{prompt + message}\"}}}}";
                    var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(url, httpContent))
                    {
                        response.EnsureSuccessStatusCode();

                        string responseBody = await response.Content.ReadAsStringAsync();
                        // Process the response body here as needed
                        return(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                return($"Error: {ex.Message}");
            }
        }

    }

}

