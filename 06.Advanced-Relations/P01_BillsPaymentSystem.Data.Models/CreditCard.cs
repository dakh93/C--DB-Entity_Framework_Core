
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        public CreditCard(decimal limit, decimal moneyOwed, DateTime expirationDate)
        {
            this.Limit = limit;
            this.MoneyOwed = moneyOwed;
            this.ExpirationDate = expirationDate;
        }
        public int CreditCardId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Limit { get; set; }
        public decimal MoneyOwed { get; set; }
        public decimal LimitLeft  => this.Limit - this.MoneyOwed;

        public PaymentMethod PaymentMethod { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"-- ID: {this.CreditCardId}");
            sb.AppendLine($"--- Limit: {this.Limit}");
            sb.AppendLine($"--- Money Owed: {this.MoneyOwed}");
            sb.AppendLine($"--- Limit Left: {this.LimitLeft}");
            sb.AppendLine($"--- Expiration Date: {this.ExpirationDate}");

            return sb.ToString().Trim();
        }
    }
}
