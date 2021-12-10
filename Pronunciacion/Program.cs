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
            Texto texto = new Texto(@"..\..\..\data\1.txt");
            foreach (var linea in texto.GetTextoLineas())
            {
                foreach (var limpia in texto.GetLineaLimpia(linea))
                {
                    Console.Write(limpia + " ");
                }
                Console.WriteLine(texto.GetLineaLimpia(linea).Count);
                // Console.WriteLine(linea.Length);
            }
            SSML.TextoHablar("ELLA ME PIDE A GRITO QUE YO LA CHAMBEE","-0.50");
            //SSML.LimpiarSSML();
        }
    }
}
