using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaGerenciamento;

public class Utils
{
    public static void GoOn(string question = null)
    {
        Console.WriteLine();
        Console.WriteLine(question == null ? "Aperte qualquer tecla para continuar..." : $"{question}");
        Console.ReadLine();
        Console.Clear();
    }

    public static bool ReadYesOrNo(string question)
    {
        Console.WriteLine();
        Console.WriteLine($"{question}? (s/n)");

        while (true)
        {
            string input = Console.ReadLine()?.ToLower();

            if (Regex.IsMatch(input, "^(s(im)?|n(ao|ão)|n(ao)?o?)$")) return input.StartsWith("s");

            Console.WriteLine("Entrada inválida.");
            Console.WriteLine($"{question} ? (s / n)");
            Console.WriteLine();
        }
    }

    public static int ReadOption(int min, int max)
    {
        bool valid = false;
        int number = 0;

        while (!valid)
        {
            Console.WriteLine();
            Console.WriteLine("Digite uma opção: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out number))
            {
                if (number >= min && number <= max) valid = true;
                else
                {
                    Console.WriteLine($"Digite um número entre {min} e {max}");
                }
            }
            else Console.WriteLine("Digite um número válido");
        }

        return number;
    }
}