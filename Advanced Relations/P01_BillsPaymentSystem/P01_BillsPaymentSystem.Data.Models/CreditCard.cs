namespace P01_BillsPaymentSystem.Data.Models
{
    using System;

    public class CreditCard
    {
        public int CreditCardId { get; set; }

        public decimal Limit { get; set; }

        public decimal MoneyOwed { get; set; }

        public decimal LimitLeft => Limit - MoneyOwed;

        public DateTime ExpirationDate { get; set; }

        //Ignore
        public int PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void WithdrawMoneyOwed(decimal amount)
        {
            this.MoneyOwed -= amount;
        }

        public void DepositMoneyOwed(decimal amount)
        {
            this.MoneyOwed += amount;
        }

        public void WithdrawMoneyLimit(decimal amount)
        {
            this.Limit -= amount;
        }

        public void DepositLimit(decimal amount)
        {
            this.Limit += amount;
        }
    }
}

