using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Pronunciacion
{
    public static class SSML
    {
        public static void TextoHablar(string texto)
        {
            string ruta = @"..\..\..\data\ssml.xml";
            File.WriteAllText(ruta,Regex.Replace(File.ReadAllText(ruta),"reemplazar",texto));
        }

        public static void TextoHablar(string texto, string velocidad)
        {
            string ruta = @"..\..\..\data\ssml.xml";
            File.WriteAllText(ruta, Regex.Replace(File.ReadAllText(ruta), "reemplazar", texto));
            File.WriteAllText(ruta, Regex.Replace(File.ReadAllText(ruta), "0.0", velocidad));
        }

        public static void LimpiarSSML()
        {
            string ssml = @"..\..\..\data\ssml.xml";
            string ssml1 = @"..\..\..\data\ssml1.xml";
            File.WriteAllText(ssml,File.ReadAllText(ssml1));
        }

    }
}
