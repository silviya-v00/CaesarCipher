using System;
using System.Diagnostics;

namespace CaesarCipher
{
    class Cipher
    {
        static void Main(string[] args)
        {
            string messageStr = "If you'd like to encript message - type \"1\", for decription - type \"2\", to leave - type \"3\". Press ENTER.";
            Console.WriteLine(messageStr);
            ExecuteOperation(messageStr);
        }

        private delegate void OperationMethod();

        private static void ExecuteOperation(string message)
        {
            int value;
            string operationNumber = Console.ReadLine();
            if (!string.IsNullOrEmpty(operationNumber))
            {
                if (int.TryParse(operationNumber, out value))
                {
                    if (value == 1)
                        Encript();
                    else if (value == 2)
                        Decript();
                    else if (value == 3)
                        Console.WriteLine("\nType any key to leave...");
                    else
                    {
                        Console.WriteLine("\nInvalid option, please try again!\n" + message);
                        ExecuteOperation(message);
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid option, please try again!\n" + message);
                    ExecuteOperation(message);
                }
            }
            else
            {
                Console.WriteLine("Please choose from options!\n" + message);
                ExecuteOperation(message);
            }
        }

        private static string Algorithm(string message, OperationMethod operationType)
        {
            int letterNum, value;
            string endMessage = "";
            Console.Write("Shift: ");
            string shift = Console.ReadLine();
            if (!string.IsNullOrEmpty(shift))
            {
                if (int.TryParse(shift, out value))
                {
                    if (operationType == Decript)
                    {
                        value = 26 - value;
                    }
                    foreach (char ch in message)
                    {
                        if (char.IsLetter(ch))
                        {
                            if (char.IsUpper(ch))
                                letterNum = ((ch + value - 'A') % 26) + 'A';
                            else
                                letterNum = ((ch + value - 'a') % 26) + 'a';
                            endMessage += Convert.ToChar(letterNum).ToString();
                        }
                        else
                            endMessage += ch;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid option, please try again!");
                    operationType();
                }
            }
            else
            {
                Console.WriteLine("Please choose shift number!");
                operationType();
            }
            return endMessage;
        }

        private static void Encript()
        {
            string encripted = "";
            Console.Write("Type message you want to encript: ");
            string message = Console.ReadLine();
            encripted = Algorithm(message, Encript);
            Console.WriteLine("Cipher text: " + encripted);
        }

        private static void Decript()
        {
            string decripted = "";
            Console.Write("Type message you want to decript: ");
            string message = Console.ReadLine();
            decripted = Algorithm(message, Decript);
            Console.WriteLine("Plain text: " + decripted);
        }
    }
}
