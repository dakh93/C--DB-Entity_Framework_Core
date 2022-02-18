using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.App.Exceptions;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models.Enums;
using P01_BillsPaymentSystem.Initialize;
using System.Linq;
using System.Text;

using (var context = new BillsPaymentSystemContext())
{
    //Initialize.Seed(context);
    Console.Write("Enter User ID: ");
    var userId = int.Parse(Console.ReadLine());

    try
    {
        PrintUserInfoById(context, userId);

    }
    catch (Exception e)
    {

        Console.WriteLine(e.Message);
    }
}

void PrintUserInfoById(BillsPaymentSystemContext context, int userId)
{
    var user = context.Users
        .Select(u => new
        {
            u.UserId,
            u.FirstName,
            u.LastName,
            BankAccounts = u.PaymentMethods
            .Where(p => p.Type == PaymentMethodType.BankAccount)
            .Select(p => p.BankAccount),
            CreditCards = u.PaymentMethods
            .Where(p => p.Type == PaymentMethodType.CreditCard)
            .Select(p => p.CreditCard)
        })
        .Where(u => u.UserId == userId)
        .FirstOrDefault();
        





    if (user == null)
    {
        throw new Exception(String.Format(ExceptionMessage.UserNotFound, userId));
    }


    var sb = new StringBuilder();
    var name = $"User: {user.FirstName} {user.LastName}";
    sb.AppendLine(name);
    sb.AppendLine("Bank Accounts:");
    foreach (var ba in user.BankAccounts)
    {
        sb.AppendLine(ba.ToString());
    }
    sb.AppendLine("Credit Cards:");
    foreach (var cc in user.CreditCards)
    {
        sb.Append(cc.ToString());
    }

    Console.WriteLine(sb.ToString().Trim());
}

