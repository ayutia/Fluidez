using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Pronunciacion
{
    public static class SSML
    {
        public static void TextoHablar(string texto)
        {
            string ruta = @"..\..\..\data\ssml.xml";
            File.WriteAllText(ruta,Regex.Replace(File.ReadAllText(ruta),"reemplazar",texto));
        }

        public static async Task TextoHablarAsync(string texto)
        {
            string ruta = @"..\..\..\data\ssml.xml";
            await File.WriteAllTextAsync(ruta, Regex.Replace(File.ReadAllText(ruta), "reemplazar", texto));
        }

        public static void TextoHablar(string texto, string velocidad)
        {
            string ruta = @"..\..\..\data\ssml.xml";
            File.WriteAllText(ruta, Regex.Replace(File.ReadAllText(ruta), "reemplazar", texto));
            File.WriteAllText(ruta, Regex.Replace(File.ReadAllText(ruta), "0.0", velocidad));
        }
        public static async Task Corregir(string texto, string velocidad)
        {
            string ruta = @"..\..\..\data\ssml.xml";
            await File.WriteAllTextAsync(ruta, Regex.Replace(File.ReadAllText(ruta), "reemplazar", texto));
            await File.WriteAllTextAsync(ruta, Regex.Replace(File.ReadAllText(ruta), "0.0", velocidad));
        }

        public static void LimpiarSSML()
        {
            string ssml = @"..\..\..\data\ssml.xml";
            string ssml1 = @"..\..\..\data\ssml1.xml";
            File.WriteAllText(ssml,File.ReadAllText(ssml1));
        }
        public static async Task LimpiarSSMLAsync()
        {
            string ssml = @"..\..\..\data\ssml.xml";
            string ssml1 = @"..\..\..\data\ssml1.xml";
            await File.WriteAllTextAsync(ssml, File.ReadAllText(ssml1));
        }

    }
}
