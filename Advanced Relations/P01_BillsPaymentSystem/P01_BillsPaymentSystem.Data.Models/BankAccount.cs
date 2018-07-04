namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public decimal Balance { get; set; }

        public string BankName { get; set; }

        public string SwiftCode { get; set; }

        //Ignore
        public int PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void WithdrawBalance(decimal amount)
        {
            this.Balance -= amount;
        }

        public void DepositBalance(decimal amount)
        {
            this.Balance += amount;
        }
    }
}
