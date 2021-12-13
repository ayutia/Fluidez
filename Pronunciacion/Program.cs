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
            MemoriaTrabajo memoria = new MemoriaTrabajo();
            LecturaInicial lecturaInicial = new LecturaInicial();
            List<string> correciones = new List<string>();
            correciones = await lecturaInicial.IniciarEvaluacion();
            correciones = correciones.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
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
        }

    }
}
