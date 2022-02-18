using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class User
    {

        public User()
        {
            this.PaymentMethods = new List<PaymentMethod>();
        }

       

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();


            var bankAccs = this.PaymentMethods
                .Select(x => x.BankAccount)
                .Where(x => x != null)
                .ToList();

            var creditCards = this.PaymentMethods
                .Select(c => c.CreditCard)
                .Where(c => c != null)
                .ToList();

            var name = $"{this.FirstName} {this.LastName}";
            builder.AppendLine(name);

            //BANK ACCOUNTS
            builder.AppendLine("Bank Accounts:");
            if (!bankAccs.Any())  builder.AppendLine("(No bank accounts !)");
            else
            {
                foreach (var bankAcc in bankAccs)
                {
                    builder.AppendLine(bankAcc.ToString());
                }
            }

            //CREDIT CARDS
            builder.AppendLine("Credit cards:");
            if (creditCards.Count() == 0) builder.AppendLine("(No credit cards !)");
            else
            {
                foreach (var creditCard in creditCards)
                {
                    builder.AppendLine(creditCard.ToString());
                }
            }
            return builder.ToString().Trim();

        }
    }
}
