using System;
using System.Text;

namespace PasswordGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Password Generator!\n");

            // Get user input for password criteria
            int length = GetIntegerInput("Enter the length of the password: ");
            bool includeLetters = GetYesNoInput("Include letters? (yes/no): ");
            bool includeNumbers = GetYesNoInput("Include numbers? (yes/no): ");
            bool includeSymbols = GetYesNoInput("Include symbols? (yes/no): ");
            bool excludeAmbiguous = GetYesNoInput("Exclude ambiguous characters? (yes/no): ");

            // Generate and display the password
            string password = GeneratePassword(length, includeLetters, includeNumbers, includeSymbols, excludeAmbiguous);
            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Error: No character types selected.");
            }
            else
            {
                Console.WriteLine($"Your generated password: {password}");
            }

            Console.WriteLine("\nThank you for using my Password Generator!");
        }

        static int GetIntegerInput(string prompt)
        {
            int input;
            while (true)
            {
                Console.Write(prompt);
                try
                {
                    input = int.Parse(Console.ReadLine());
                    if (input >= 8) // Check if input is at least 8 chars long
                    {
                        return input;
                    }
                    else
                    {
                        Console.WriteLine("Password length must be 8 characters or more.\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid positive integer.\n");
                }
            }
        }

        static bool GetYesNoInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine()?.ToLower();
                if (input == "yes" || input == "y")
                {
                    return true;
                }
                else if (input == "no" || input == "n")
                {
                    return false;
                }
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.\n");
            }
        }

        static string GeneratePassword(int length, bool includeLetters, bool includeNumbers, bool includeSymbols, bool excludeAmbiguous)
        {
            const string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string symbols = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            // Define character pool based on user criteria
            string pool = "";
            if (includeLetters) pool += letters;
            if (includeNumbers) pool += numbers;
            if (includeSymbols) pool += symbols;

            if (pool == "") return string.Empty;
            if (excludeAmbiguous)
            {
                pool = pool.Replace("l", "").Replace("I", "").Replace("1", "").Replace("O", "").Replace("0", ""); // Remove ambiguous characters
            }

            // Generate password
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, pool.Length);
                password.Append(pool[index]);
            }

            return password.ToString();
        }
    }
}
