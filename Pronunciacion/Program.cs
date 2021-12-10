using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Pronunciacion.SistemaExperto;

namespace Pronunciacion
{
    class Program
    {
        static void Main(string[] args)
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

            // REGLAS
            Regla regla1 = new Regla("Liquida");
            regla1.SetPrioridad(1);
            regla1.SetConsecuente(new Clausula("liquida", "corregir"));
            regla1.AddAntecedentes(new Clausula("vibrante", "corregir"));
            regla1.AddAntecedentes(new Clausula("lateral", "corregir"));

            Regla regla2 = new Regla("Vibrante");
            regla2.SetPrioridad(2);
            regla2.SetConsecuente(new Clausula("vibrante", "corregir"));
            regla2.AddAntecedentes(new Clausula("vibranteSimple", "corregir"));
            regla2.AddAntecedentes(new Clausula("vibranteDoble", "corregir"));

            Regla regla3 = new Regla("Lateral");
            regla3.SetPrioridad(3);
            regla3.SetConsecuente(new Clausula("lateral", "corregir"));
            regla3.AddAntecedentes(new Clausula("palabraCorregida", "lateral"));

            Regla regla4 = new Regla("VibranteDoble");
            regla4.SetPrioridad(4);
            regla4.SetConsecuente(new Clausula("vibranteDoble","corregir"));
            regla4.AddAntecedentes(new Clausula("palabraCorregida", "vibranteDoble"));

            Regla regla5 = new Regla("VibranteSimple");
            regla5.SetPrioridad(5);
            regla5.SetConsecuente(new Clausula("vibranteSimple","corregir"));
            regla5.AddAntecedentes(new Clausula("palabraCorregida","vibranteSimple"));

            // Memoria trabajo a ver
            MemoriaTrabajo memoria = new MemoriaTrabajo();
            memoria.AddHecho(new Clausula("palabraCorregida", "vibranteDoble"));
            memoria.AddHecho(new Clausula("palabraCorregida", "vibranteSimple"));
            memoria.AddHecho(new Clausula("palabraCorregida", "lateral"));
            //memoria.AddHecho(new Clausula("palabraCorregida", "vibranteDoble"));
            //memoria.AddHecho(new Clausula("palabraCorregida", "lateral"));
            //memoria.AddHecho(new Clausula("vibrante", "corregir"));
            //memoria.AddHecho(new Clausula("lateral", "corregir"));


            //probando Equals
            Console.WriteLine(memoria.BuscarHecho(new Clausula("palabraCorregida", "vibranteDoble")));

            // Probando si funciona 10-12 17:52
            MotorInferencia motomoto = new MotorInferencia();
            motomoto.AddMemoriaTrabajo(memoria);
            motomoto.AddRegla(regla1);
            motomoto.AddRegla(regla2);
            motomoto.AddRegla(regla3);
            motomoto.AddRegla(regla4);
            motomoto.AddRegla(regla5);

            // A PROBARRRR
            Console.WriteLine(motomoto.ForwardChaining().ToString());


        }
    }
}
