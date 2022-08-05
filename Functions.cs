/*
 * 
 * 
 * Banki CallBack Processor
 * 
 * Http Trigger writes request to queue and returns response
 * Queue writes to DB
 * 
 * DB Write triggers verification call, updates transaction
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */



using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BankiCallbackProcessor.Models;
using System;
using System.Collections.Generic;
using BankiCallbackProcessor.Entities;
using Microsoft.Azure.Documents;

namespace BankiCallbackProcessor
{
    public static class Functions
    {
        
        [FunctionName("ProcessTransactionEntry")]
        public static void ProcessTransactionEntry(
            [CosmosDBTrigger(databaseName: "banki-cosmos-db",
            collectionName: "transactions",
            ConnectionStringSetting = "BankiCosmosDbConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]
            IReadOnlyList<Document> transactions,
            ILogger log)
        {
            log.LogInformation($"Processing Transaction with Transaction Id: {(transactions[0]).Id} from DB");
            // query provider for transaction details


            // update transaction
        } 


        [FunctionName("TransactionsQueue")]
        public static void  TransactionsQueue(
            [QueueTrigger("banki-callback-queue")] Transaction queueTxn,
             [CosmosDB(
                databaseName: "banki-cosmos-db",
                collectionName: "transactions",
                ConnectionStringSetting = "BankiCosmosDbConnectionString")] out Transaction dbTxn,
            ILogger log)
        {
            log.LogInformation($"Processing Transaction with Transaction Id: {queueTxn.TransantionId} from queue");
            dbTxn = queueTxn;
        }

        [FunctionName("Webhook")]
        public static async Task<IActionResult> Webhook(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            [Queue("banki-callback-queue")] IAsyncCollector<WebhookRequestModel> transactionsQueue,
            ILogger log)
        {

            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var transaction = JsonConvert.DeserializeObject<WebhookRequestModel>(requestBody);
                await transactionsQueue.AddAsync(transaction);
                return new OkObjectResult(new { transaction_id = transaction.TransantionId});

            } 
            catch(Exception e)
            {
                // log exceptions
                return new BadRequestResult();
            }
        }
    }
}
