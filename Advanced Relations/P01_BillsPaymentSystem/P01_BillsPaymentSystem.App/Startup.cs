namespace P01_BillsPaymentSystem.App
{
    using System;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;
    using System.Linq;
    using System.Globalization;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var context = new BillsPaymentSystemContext())
            {
                //context.Database.Migrate();
                //context.Database.EnsureCreated();
                //Seed(context);
                //GetUsersWithAccounts(context);
                PayBills(1, 1);
            }
        }

        private static void PayBills(int userId, decimal amount)
        {
            using (var context = new BillsPaymentSystemContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    return;
                }


                var bankAccounts = context.PaymentMethods
                    .Where(pm => pm.UserId == userId && pm.Type == PaymentMethodType.BankAccount)
                    .Select(pm => pm.BankAccount)
                    .OrderBy(b => b.BankAccountId)
                    .ToList();

                var creditCards = context.PaymentMethods
                    .Where(pm => pm.UserId == userId && pm.Type == PaymentMethodType.CreditCard)
                    .Select(pm => pm.CreditCard)
                    .OrderBy(c => c.CreditCardId)
                    .ToList();


                var bankBalance = bankAccounts.Sum(ba => ba.Balance);
                var moneyToPay = creditCards.Sum(cc => cc.MoneyOwed) + amount;

                if(bankBalance < moneyToPay)
                {
                    Console.WriteLine("Insufficient funds!");
                    return;
                }

                var currentCreditIndex = 0;
                for (int i = 0; i < bankAccounts.Count; i++)
                {
                    for (int j = currentCreditIndex;
                        j < creditCards.Count; j++, currentCreditIndex++)
                    {
                        if (bankAccounts[i].Balance >= creditCards[j].MoneyOwed)
                        {
                            bankAccounts[i].WithdrawBalance(creditCards[j].MoneyOwed);
                            creditCards[j].MoneyOwed = 0;
                        }
                        else
                        {
                            break;
                        }

                        if(bankAccounts[i].Balance >= amount)
                        {
                            bankAccounts[i].WithdrawBalance(amount);
                            amount = 0;
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        private static void GetUsersWithAccounts(BillsPaymentSystemContext context)
        {
            var userId = int.Parse(Console.ReadLine());

            using (context)
            {
                var users = context.Users
                    .Where(u => u.UserId == userId)
                    .Select(u => new
                    {
                        Name = $"{u.FirstName} {u.LastName}",

                        CreditCards = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.CreditCard)
                        .Select(pm => pm.CreditCard),

                        BankAccounts = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.BankAccount)
                        .Select(pm => pm.BankAccount),
                    });


                foreach (var user in users)
                {
                    Console.WriteLine($"User: {user.Name}");

                    Console.WriteLine("Bank Accounts:");
                    foreach (var bankAccount in user.BankAccounts)
                    {
                        Console.WriteLine($"-- ID: {bankAccount.BankAccountId}");
                        Console.WriteLine($"--- Balance: {bankAccount.Balance:f2}");
                        Console.WriteLine($"--- Bank: {bankAccount.BankName}");
                        Console.WriteLine($"--- SWIFT: {bankAccount.SwiftCode}");
                    }

                    Console.WriteLine("Credit Cards:");

                    foreach (var creditCard in user.CreditCards)
                    {
                        Console.WriteLine($"-- ID: {creditCard.CreditCardId}");
                        Console.WriteLine($"--- Limit: {creditCard.Limit:f2}");
                        Console.WriteLine($"--- Money Owed: {creditCard.MoneyOwed:f2}");
                        Console.WriteLine($"--- Limit Left:: {creditCard.LimitLeft:f2}");
                        Console.WriteLine($"--- Expiration Date: {creditCard.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                    }
                }
            }
        }

        private static void Seed(BillsPaymentSystemContext context)
        {
            using (context)
            {
                var user = new User()
                {
                    FirstName = "Pesho",
                    LastName = "Stamatov",
                    Password = "azsympesho",
                    Email = "pesho@abv.bg",
                };

                var creditCards = new CreditCard[]
                {
                  new CreditCard()
                  {
                      ExpirationDate = DateTime.ParseExact("20.05.2020","dd.MM.yyyy",null),
                      Limit = 1000m,
                      MoneyOwed = 5m
                  },
                   new CreditCard()
                  {
                      ExpirationDate = DateTime.ParseExact("20.05.2020","dd.MM.yyyy",null),
                      Limit = 1500m,
                      MoneyOwed = 200m
                  }
                };

                var bankAccount = new BankAccount()
                {
                    Balance = 1500m,
                    BankName = "Swiss Bank",
                    SwiftCode = "SWSSBANK",
                };

                var paymentMethods = new PaymentMethod[]
                {
                   new PaymentMethod()
                   {
                       User = user,
                       Type = PaymentMethodType.BankAccount,
                       BankAccount = bankAccount,
                       //BankAccountId = bankAccount.BankAccountId
                   },
                   new PaymentMethod()
                   {
                       User = user,
                       CreditCard = creditCards[1],
                       //CreditCardId = creditCards[1].CreditCardId,
                       Type = PaymentMethodType.CreditCard,
                   },
                };

                context.Users.Add(user);
                context.CreditCards.AddRange(creditCards);
                context.BankAccounts.Add(bankAccount);
                context.AddRange(paymentMethods);
                context.SaveChanges();
            }
        }
    }
}
