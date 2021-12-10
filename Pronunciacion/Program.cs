using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pronunciacion
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(File.Exists(@"..\..\..\data\1.txt"));
            string palabra = "israel";
            int contador = 0;
            string[] arreglo = { "rr", "lr", "sr", "nr" };
            List<string> lexemas = new List<string>(arreglo);
            if (palabra.StartsWith("r") || palabra.StartsWith("R"))
            {
                contador = 1;
            }
            foreach (var lex in lexemas)
            {
                contador += Regex.Matches(palabra, lex, RegexOptions.IgnoreCase).Count;
            }
            Console.WriteLine(contador);
        }
    }
}
