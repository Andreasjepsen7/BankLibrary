using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BankLibrary;
using Newtonsoft.Json;

namespace BankConsoleApp
{
    class Program
    {
        private static List<BankAccount> accounts = new List<BankAccount>();
        private static string jsonFilePath = @"C:\\Users\\HFGF\\source\\repos\\BankLibrary\\BankLibrary\\Data\\mock_accounts.json";

        static void Main(string[] args)
        {
            LoadAccounts();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Edit Account");
                Console.WriteLine("3. Delete Account");
                Console.WriteLine("4. View Accounts");
                Console.WriteLine("5. Show Total Balance");
                Console.WriteLine("6. Exit");
                Console.Write("Enter option: ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        EditAccount();
                        break;
                    case 3:
                        DeleteAccount();
                        break;
                    case 4:
                        ViewAccounts();
                        break;
                    case 5:
                        ShowTotalBalance();
                        break;
                    case 6:
                        SaveAccounts();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void LoadAccounts()
        {
            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                accounts = JsonConvert.DeserializeObject<List<BankAccount>>(jsonData);
            }
        }

        static void SaveAccounts()
        {
            string jsonData = JsonConvert.SerializeObject(accounts, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonFilePath, jsonData);
        }

        static void CreateAccount()
        {
            Console.Write("Enter account holder's name: ");
            string accountHolder = Console.ReadLine();

            BankAccount newAccount = new BankAccount
            {
                AccountNumber = GenerateAccountNumber(),
                AccountHolder = accountHolder,
                Balance = 0
            };

            accounts.Add(newAccount);
            Console.WriteLine("Account created successfully.");
        }

        static int GenerateAccountNumber()
        {
            // You can implement your logic to generate unique account numbers
            // For simplicity, let's just return a random number for now
            Random random = new Random();
            return random.Next(1000, 9999);
        }

        static void EditAccount()
        {
            Console.Write("Enter account number to edit: ");
            int accountNumber = int.Parse(Console.ReadLine());

            BankAccount accountToEdit = accounts.Find(account => account.AccountNumber == accountNumber);

            if (accountToEdit != null)
            {
                Console.Write("Enter new account holder's name: ");
                accountToEdit.AccountHolder = Console.ReadLine();
                Console.WriteLine("Account edited successfully.");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        static void DeleteAccount()
        {
            Console.Write("Enter account number to delete: ");
            int accountNumber = int.Parse(Console.ReadLine());

            BankAccount accountToDelete = accounts.Find(account => account.AccountNumber == accountNumber);

            if (accountToDelete != null)
            {
                accounts.Remove(accountToDelete);
                Console.WriteLine("Account deleted successfully.");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        static void ViewAccounts()
        {
            Console.WriteLine("Accounts:");
            foreach (BankAccount account in accounts)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber}, Holder: {account.AccountHolder}, Balance: {account.Balance}");
            }
        }

        static void ShowTotalBalance()
        {
            decimal totalBalance = 0;
            foreach (BankAccount account in accounts)
            {
                totalBalance += account.Balance;
            }

            Console.WriteLine($"Total Balance of All Accounts: {totalBalance}");
        }
    }
}
