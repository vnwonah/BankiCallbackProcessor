using System;
using Microsoft.Azure.Documents;

namespace BankiCallbackProcessor.Entities
{
    public class Transaction
    {
        public DateTime CreateAt { get; set; }
        public string TransantionId { get; set; }
        public string TransanctionAmount { get; set; }
        public DateTime TransactionTime { get; set; }
        public string PayerName { get; set; }
        public string PayerAmount { get; set; }
        public bool TransactionConfirmed { get; set; }
        public DateTime? ConfirmedAt { get; set; }

    }
}
