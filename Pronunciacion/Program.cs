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
            // PRUEBAS TEXTO
            //Texto texto = new Texto(@"..\..\..\data\1.txt");
            //foreach (var linea in texto.GetTextoLineas())
            //{
            //    foreach (var limpia in texto.GetLineaLimpia(linea))
            //    {
            //        Console.Write(limpia + " ");
            //    }
            //    Console.WriteLine(texto.GetLineaLimpia(linea).Count);
            // Console.WriteLine(linea.Length);
            // }
            // SSML.TextoHablar("ELLA ME PIDE A GRITO QUE YO LA CHAMBEE","-0.50");
            //SSML.LimpiarSSML();

            // Memoria trabajo a ver
            MemoriaTrabajo memoria = new MemoriaTrabajo();
            //memoria.AddHecho(new Clausula("palabraCorregida", "vibranteDoble"));
            //memoria.AddHecho(new Clausula("palabraCorregida", "vibranteSimple"));
            //memoria.AddHecho(new Clausula("palabraCorregida", "lateral"));
            //memoria.AddHecho(new Clausula("palabraCorregida", "vibranteDoble"));
            //memoria.AddHecho(new Clausula("palabraCorregida", "lateral"));
            //memoria.AddHecho(new Clausula("vibrante", "corregir"));
            //memoria.AddHecho(new Clausula("lateral", "corregir"));


            //probando Equals
            //Console.WriteLine(memoria.BuscarHecho(new Clausula("palabraCorregida", "vibranteDoble")));

            // Probando si funciona 10-12 17:52
             //MotorInferencia motomoto = new MotorInferencia();
             //motomoto.AddMemoriaTrabajo(memoria);

            // A PROBARRRR
            //Console.WriteLine(motomoto.ForwardChaining().ToString());
            //foreach(var linea in motomoto.GetRazonamiento())
            //{
            //    Console.WriteLine(linea);
            //}
            
            LecturaInicial lecturaInicial = new LecturaInicial();
            List<string> correciones = new List<string>();
            correciones = await lecturaInicial.IniciarEvaluacion();
            //Console.WriteLine(correciones.ToString());
            //List<string> corregidoSinDuplicados = correciones.Distinct().ToList();
            //foreach (var correccion in correciones)
            //{
            //    Console.WriteLine(correccion);
            //}
            correciones = correciones.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            //Console.WriteLine("ahora sin duplicados");

            foreach (var hecho in correciones)
            {
                memoria.AddHecho(new Clausula("palabraCorregida", hecho));
            }
            MotorInferencia motomoto = new MotorInferencia();
            motomoto.AddMemoriaTrabajo(memoria);
            string correccion = "";
            correccion = motomoto.ForwardChaining().ToString();
            Console.WriteLine(correccion);

            Lectura lecturaNormal = new Lectura(correccion);
            correciones.Clear();
            correciones = await lecturaNormal.IniciarLectura();
            //Experto experto = new Experto();
            //List<string> correciones = new List<string>();
            //correciones.Add("vibranteSimple");
            //experto.InicializarMemoria(correciones);
            //Console.WriteLine(experto.GetInferencia());
            //await LeerLineasAsync();
            //await SynthesizeAudioAsync();



        }

    }
}
