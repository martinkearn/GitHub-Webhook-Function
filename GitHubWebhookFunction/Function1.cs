
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace GitHubWebhookFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // get payload
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic webhookPayload = JsonConvert.DeserializeObject(requestBody);

            // get commit details required to call API
            var commitId = webhookPayload.after;
            var owner = webhookPayload.head_commit.author.username;
            var repo = webhookPayload.repository.name;



            return (ActionResult)new OkObjectResult($"We got data for {commitId} by {owner} on {repo}");
        }
    }
}
