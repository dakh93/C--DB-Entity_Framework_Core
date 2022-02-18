using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public BankAccount(decimal balance, string bankName, string swiftCode)
        {
            this.BankName = bankName;
            this.Balance = balance;
            this.SwiftCode = swiftCode;
        }

        public int BankAccountId { get; set; }
        public string BankName { get; set; }
        public decimal Balance { get; set; }
        public string SwiftCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"-- ID: {this.BankAccountId}");
            sb.AppendLine($"--- Balance: {this.Balance}");
            sb.AppendLine($"--- Bank: {this.BankName}");
            sb.AppendLine($"--- SWIFT: {this.SwiftCode}");

            return sb.ToString().Trim();
        }
    }
}