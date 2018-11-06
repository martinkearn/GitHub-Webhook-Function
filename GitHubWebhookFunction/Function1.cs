
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GitHubWebhookFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a GitHub webhook request.");

            // get payload
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic payload = JsonConvert.DeserializeObject(requestBody);

            // get commit details required to call API
            var commitId = payload.after;
            var owner = payload.head_commit.author.username;
            var repo = payload.repository.name;
            var fileAdded = payload.head_commit.added[0];

            // respond
            return (ActionResult)new OkObjectResult($"We got data for {commitId} which added {fileAdded} by {owner} on {repo}");
        }
    }
}
