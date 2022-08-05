using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankiCallbackProcessor.Models
{
    public class WebhookRequestModel
    {
        [Required]
        [JsonProperty("transaction_id")]
        public string TransantionId { get; set; }

        [Required]
        [JsonProperty("transaction_amount")]
        public float TransanctionAmount { get; set; }

        [Required]
        [JsonProperty("transaction_time")]
        public DateTime TransactionTime { get; set;  }

        [Required]
        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [Required]
        [JsonProperty("customer_email")]
        public string CustomerEmail { get; set; }


    }
}
