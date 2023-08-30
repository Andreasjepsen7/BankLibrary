using System;

namespace BankLibrary
{
    [Serializable]
    public class BankAccount
    {
        public int AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public decimal Balance { get; set; }
    }
}
