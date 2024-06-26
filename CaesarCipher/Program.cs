using System;
using System.Diagnostics;
using System.Linq;

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
                    string alphabet = "";
                    string bg = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЬЮЯ";
                    string eng = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    char firstInputEl = new string(message.SkipWhile(c => !Char.IsLetter(c)).ToArray()).ToLower().FirstOrDefault();

                    if (bg.ToLower().Contains(firstInputEl))
                        alphabet = bg;
                    else if (eng.ToLower().Contains(firstInputEl))
                        alphabet = eng;
                    else
                    {
                        Console.WriteLine("\nInvalid character!");
                        operationType();
                    }

                    if (operationType == Decript)
                        value = alphabet.Length - value;

                    foreach (char ch in message)
                    {
                        if (char.IsLetter(ch))
                        {
                            alphabet = char.IsUpper(ch) ? alphabet.ToUpper() : alphabet.ToLower();
                            letterNum = (alphabet.IndexOf(ch) + value) % alphabet.Length;
                            endMessage += alphabet[letterNum].ToString();
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
                Console.WriteLine("\nPlease choose shift number!");
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

