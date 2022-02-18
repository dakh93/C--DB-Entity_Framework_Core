
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Models.Enums;

namespace P01_BillsPaymentSystem.Initialize
{
    public class PaymentMethodInitializer 
    {
        public static List<PaymentMethod> GetPaymentMethods()
        {
            var paymentMethods = new List<PaymentMethod>()
            {
                new PaymentMethod{ Type = PaymentMethodType.BankAccount, UserId = 1, BankAccountId = 4},
                new PaymentMethod{ Type =  PaymentMethodType.CreditCard, UserId = 2, CreditCardId = 1},
                new PaymentMethod{ Type = PaymentMethodType.CreditCard, UserId = 3, CreditCardId = 3},
                new PaymentMethod{ Type =  PaymentMethodType.BankAccount, UserId = 4, BankAccountId = 2},
                new PaymentMethod{ Type =  PaymentMethodType.CreditCard, UserId = 1, CreditCardId = 2},
                new PaymentMethod{ Type =  PaymentMethodType.BankAccount, UserId = 5, BankAccountId = 5}
            };

            return paymentMethods;
        }
    }
}
