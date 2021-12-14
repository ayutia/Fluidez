using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pronunciacion
{
    public class Texto
    {
        private string _titulo;
        private int _numeroDePalabras;
        List<string> textoEnLineas;
        List<string> textoEnPalabras;
        Dictionary<string, string> modosArticulatorios;

        public Texto(string rutaTexto)
        {
            textoEnLineas = new List<string>();
            textoEnLineas = File.ReadAllLines(rutaTexto).ToList();
            _titulo = textoEnLineas[0];
            textoEnPalabras = new List<string>();
            SetTextoEnPalabras();
            _numeroDePalabras = textoEnPalabras.Count();
            modosArticulatorios = DeterminarModo(textoEnPalabras);
        }

        public string GetTitulo()
        {
            return _titulo;
        }

        private void SetTextoEnPalabras()
        {
            string[] delimitadores = { " ", ",", ".", ":", ";", "-", "—", "¿", "?", "¡", "!", "\t", "\r\n" };
            foreach (var linea in textoEnLineas.Skip(1))
            {
                string[] palabrasEnLinea = linea.Split(delimitadores, System.StringSplitOptions.RemoveEmptyEntries);
                textoEnPalabras.AddRange(palabrasEnLinea);
            }
        }

        public List<string> GetTextoLineas()
        {
            return textoEnLineas;
        }

        public List<string> GetTextoLineasLimpio()
        {
            List<string> copia = textoEnLineas.ToList();
            copia.RemoveAt(0);
            copia.RemoveAll(vacias => vacias == "\r\n");
            return copia;
        }

        public List<string> GetTextoPalabras()
        {
            return textoEnPalabras;
        }

        public int GetTextoNumeroPalabras()
        {
            return _numeroDePalabras;
        }

        // Se determina el modo de articulacion de cada palabra en el texto.
        // Se utiliza diccionario para evitar dobles ocurrencias.
        private Dictionary<string,string> DeterminarModo(List<string> palabras)
        {
            Dictionary<string, string> _modos = new Dictionary<string, string>();
            foreach(var palabra in palabras)
            {
                if(palabra.Length <= 1)
                {
                    continue;
                }
                int rr = CountVibranteDoble(palabra);
                int r = CountVibranteSimple(palabra);
                int l = CountLaterales(palabra);
                if(rr == r && rr == l && rr == 0)
                {
                    //No se puede clasificar
                    continue;
                }
                if (rr >= r && rr>= l)
                {
                    AgregarModo(_modos,palabra,"vibranteDoble");
                }
                else if (r >= l)
                {
                    AgregarModo(_modos,palabra,"vibranteSimple");
                }
                else
                {
                    AgregarModo(_modos,palabra,"lateral");
                }
            }
            return _modos;
        }

        private int CountVibranteSimple(string palabra)
        {
            int contador = 0;
            string[] arreglo = {"ar","er","ir","or","ur","br","cr","dr","fr","gr","pr","tr"};
            List<string> lexemas = new List<string>(arreglo);
            foreach(var lex in lexemas)
            {
                contador += Regex.Matches(palabra, lex, RegexOptions.IgnoreCase).Count;
            }
            return contador;
        }

        private int CountVibranteDoble(string palabra)
        {
            int contador = 0;
            string[] arreglo = { "rr","lr","sr","nr"};
            List<string> lexemas = new List<string>(arreglo);
            if(palabra.StartsWith("r") || palabra.StartsWith("R"))
            {
                contador = 1;
            }
            foreach (var lex in lexemas)
            {
                contador += Regex.Matches(palabra, lex, RegexOptions.IgnoreCase).Count;
            }
            return contador;
        }

        private int CountLaterales(string palabra)
        {
            int contador = 0;
            string[] arreglo = { "la", "le", "li", "lo", "lu", "al", "el", "il", 
                "ol", "ul", "cl", "fl","gl", "pl", "tl" };
            List<string> lexemas = new List<string>(arreglo);
            if (palabra.StartsWith("l") || palabra.StartsWith("L"))
            {
                contador = 1;
            }
            foreach (var lex in lexemas)
            {
                contador += Regex.Matches(palabra, lex, RegexOptions.IgnoreCase).Count;
            }
            return contador;
        }

        private void AgregarModo(Dictionary<string,string> dic,string palabra, string modo)
        {
            try
            {
                dic.Add(palabra,modo);
            }
            catch
            {
                //no se puede debido a que ya esta
            }
        }

        public List<string> GetLineaLimpia(string linea)
        {
            string[] delimitadores = { " ", ",", ".", ":", ";", "-", "—", "¿", "?", "¡", "!", "\t", "\r\n" };
            string[] palabrasEnLinea = linea.Split(delimitadores, System.StringSplitOptions.RemoveEmptyEntries);
            List<string> _linea = new List<string>(palabrasEnLinea);
            return _linea;
        }

        public string ObtenerModoPalabra(string palabraClave)
        {
            return modosArticulatorios.GetValueOrDefault(palabraClave);
        }

        public Dictionary<string,string> GetModos()
        {
            return modosArticulatorios;
        }

        public List<string> GetPalabrasCorregir(string modo)
        {
            List<string> palabrasCorregir = new List<string>();
            switch (modo)
            {
                case "liquida":
                    foreach (var item in modosArticulatorios)
                    {
                        palabrasCorregir.Add(item.Key);
                    }
                    break;
                case "vibrante":
                    foreach (var item in modosArticulatorios)
                    {
                        if(item.Value == "vibranteSimple"|| item.Value == "vibranteDoble")
                        {
                            palabrasCorregir.Add(item.Key);
                        }
                    }
                    break;
                case "vibranteSimple":
                    foreach (var item in modosArticulatorios)
                    {
                        if (item.Value == "vibranteSimple")
                        {
                            palabrasCorregir.Add(item.Key);
                        }
                    }
                    break;
                case "vibranteDoble":
                    foreach (var item in modosArticulatorios)
                    {
                        if (item.Value == "vibranteDoble")
                        {
                            palabrasCorregir.Add(item.Key);
                        }
                    }
                    break;
                case "lateral":
                    foreach (var item in modosArticulatorios)
                    {
                        if (item.Value == "lateral")
                        {
                            palabrasCorregir.Add(item.Key);
                        }
                    }
                    break;
            }
            
            return palabrasCorregir;
        }
    }
}
