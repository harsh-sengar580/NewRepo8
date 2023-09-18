using System;

public class BankAccount
{
    public string AccountNumber { get; private set; }
    public decimal Balance { get; protected set; }

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"Deposited {amount:C}. New balance: {Balance:C}");
        }
        else
        {
            Console.WriteLine("Invalid deposit amount.");
        }
    }

    public virtual void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrawn {amount:C}. New balance: {Balance:C}");
        }
        else
        {
            Console.WriteLine("Invalid withdrawal amount or insufficient balance.");
        }
    }
}

public sealed class SavingsAccount : BankAccount
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance)
    {
    }

    public void AddInterest(decimal rate)
    {
        if (rate > 0)
        {
            decimal interest = Balance * rate / 100;
            Balance += interest;
            Console.WriteLine($"Added interest of {interest:C}. New balance: {Balance:C}");
        }
        else
        {
            Console.WriteLine("Invalid interest rate.");
        }
    }

    public override void Withdraw(decimal amount)
    {
        decimal minimumBalance = 100; 

        if (amount > 0 && Balance - amount >= minimumBalance)
        {
            base.Withdraw(amount);
        }
        else
        {
            Console.WriteLine("Invalid withdrawal amount or insufficient balance to maintain the minimum balance.");
        }
    }
}

class Program
{
    static void Main()
    {
        SavingsAccount savingsAccount = new SavingsAccount("SA12345", 1000);

        savingsAccount.Deposit(290);
        savingsAccount.Withdraw(290);

        savingsAccount.AddInterest(5);

        savingsAccount.Withdraw(560);

        Console.WriteLine($"Final balance in the savings account: {savingsAccount.Balance:C}");
    }
}
