using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Pronunciacion.SistemaExperto;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;


namespace Pronunciacion
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Para empezar, presione enter");
            Console.ReadLine();
            MemoriaTrabajo memoria = new MemoriaTrabajo();
            LecturaInicial lecturaInicial = new LecturaInicial();
            List<string> correcciones = new List<string>();
            correcciones = await lecturaInicial.IniciarEvaluacion();
            correcciones = correcciones.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            foreach (var hecho in correcciones)
            {
                memoria.AddHecho(new Clausula("palabraCorregida", hecho));
            }
            MotorInferencia motomoto = new MotorInferencia();
            motomoto.AddMemoriaTrabajo(memoria);
            string correccion = "";
            correccion = motomoto.ForwardChaining().GetVariable();
            Console.WriteLine(correccion);
            // Lectura lecturaNormal = new Lectura(correccion);
            correcciones.Clear();
            //correciones = await lecturaNormal.IniciarLectura();


            //Parte Luego de Leer
            Console.WriteLine("¿Desea Continuar? Si\tNo");
            var opcionContinuar = Console.ReadLine();
            string rutaTexto = "";
            //correccion sera de ejemplo aca
            // string correccion = "vibranteSimple"; el de ejemplo
            Lectura lectura;
            switch (opcionContinuar)
            {
                case "Si": case "si": case "s":case "SI":
                    //Segunda lectura
                    Console.WriteLine("");
                    Console.WriteLine("Indice el texto que desea leer");
                    Console.WriteLine("1) Lagrimas de Luz");
                    Console.WriteLine("2) Los cinco pollitos");
                    Console.WriteLine("3) Kuyen y el arbol de la amistad");
                    Console.WriteLine("");
                    string opcionTexto = Console.ReadLine();
                    switch (opcionTexto)
                    {
                        case "1":
                            //rutaTexto = @"..\..\..\data\1.txt";
                            rutaTexto = "1.txt";
                            break;
                        case "2":
                            //rutaTexto = @"..\..\..\data\2.txt";
                            rutaTexto = "2.txt";
                            break;
                        case "3":
                            //rutaTexto = @"..\..\..\data\3.txt";
                            rutaTexto = "3.txt";
                            break;
                        case "4":
                            //rutaTexto = @"..\..\..\data\inicial.txt";
                            rutaTexto = "inicial.txt";
                            break;
                    }
                    lectura = new Lectura(correccion, rutaTexto);
                    //Foreach para probar una teoria
                    foreach(var item in lectura.GetTexto().GetPalabrasCorregir(correccion))
                    {
                        Console.WriteLine(item);
                    }
                    // De ejemplo el foreach
                    correcciones = await lectura.IniciarLectura();

                    break;

            }
        }

    }
}
