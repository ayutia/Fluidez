using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace Pronunciacion
{
    public class Lectura
    {
        Texto _texto;
        string _modo = "";
        string _libro = "";
        List<string> palabrasCorregir = new List<string>();
        List<string> correcciones = new List<string>();

        public Lectura(string modo)
        {
            Console.WriteLine("Indice el texto que desea leer");
            Console.WriteLine("1) Lagrimas de Luz");
            Console.WriteLine("2) Los cinco pollitos");
            Console.WriteLine("3) Kuyen y el arbol de la amistad");
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    _libro = @"..\..\..\data\1.txt";
                    break;
                case "2":
                    _libro = @"..\..\..\data\2.txt";
                    break;
                case "3":
                    _libro = @"..\..\..\data\3.txt";
                    break;
            }
            _texto = new Texto(_libro);
            _modo = modo;
            palabrasCorregir = SetPalabrasCorregir();
        }

        public List<string> SetPalabrasCorregir()
        {
            List<string> palabras = new List<string>();
            switch (_modo)
            {
                case "liquida":
                    foreach (var item in _texto.GetModos())
                    {
                        palabras.Add(item.Key.ToLower());
                    }
                    break;

                case "vibrante":
                    foreach (var item in _texto.GetModos())
                    {
                        if (item.Value == "vibranteSimple" || item.Value == "vibranteDoble")
                        {
                            palabras.Add(item.Key.ToLower());
                        }
                    }
                    break;

                case "vibranteDoble":
                    foreach (var item in _texto.GetModos())
                    {
                        if (item.Value == "vibranteDoble")
                        {
                            palabras.Add(item.Key.ToLower());
                        }
                    }
                    break;

                case "vibranteSimple":
                    foreach (var item in _texto.GetModos())
                    {
                        if (item.Value == "vibranteSimple")
                        {
                            palabras.Add(item.Key.ToLower()) ;
                        }
                    }
                    break;

                case "lateral":
                    foreach (var item in _texto.GetModos())
                    {
                        if (item.Value == "lateral")
                        {
                            palabras.Add(item.Key);
                        }
                    }
                    break;
            }
            return palabras;
        }

        public async Task<List<string>> IniciarLectura()
        {
            foreach (var lineas in _texto.GetTextoLineasLimpio())
            {
                await SSML.LimpiarSSMLAsync();
                await SSML.TextoHablarAsync(lineas);
                await LeerLineasAsync();
                // Aca ya empezar lo chido, evaluar la lectura
                await SSML.LimpiarSSMLAsync();
                await SSML.TextoHablarAsync("Ahora te toca a ti.");
                await LeerLineasAsync();
                // cada palabra de la linea
                await SSML.LimpiarSSMLAsync();
                foreach (var palabra in _texto.GetLineaLimpia(lineas))
                {
                    Console.Write(palabra + " ");
                    var decision = await VerCorregir(palabra, palabrasCorregir);
                    if (decision)
                    {
                        Console.WriteLine("entre");
                        await SSML.Corregir(palabra, "-50");
                        await LeerLineasAsync();
                        await SSML.LimpiarSSMLAsync();
                    }
                    else
                    {
                        var resultado = await SpeechSingleShotRecognitionAsync();
                        if (palabra.ToLower() != resultado)
                        {
                            await SSML.Corregir(palabra, "-50");
                            await LeerLineasAsync();
                            correcciones.Add(_texto.ObtenerModoPalabra(palabra));
                            await SSML.LimpiarSSMLAsync();
                        }
                    }
                    
                }
                Console.WriteLine();
            }

            return correcciones;
        }

        public static async Task<bool> VerCorregir(string palabra, List<string> conflictivas)
        {
            return conflictivas.Contains(palabra);
        }

        public static async Task<string> SpeechSingleShotRecognitionAsync()
        {
            string resultadoLimpio = null;
            var config = SpeechConfig.FromSubscription("2e5647a940d94ce78f41c046c24ddf4b", "brazilsouth");
            using (var recognizer = new SpeechRecognizer(config, "es-CL"))
            {
                var result = await recognizer.RecognizeOnceAsync();

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    //Console.WriteLine($"RECOGNIZED: Text={result.Text}");
                    resultadoLimpio = result.Text.ToLower();
                    resultadoLimpio = resultadoLimpio.Remove(resultadoLimpio.Length - 1);
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
                return resultadoLimpio;
            }
        }

        // Sirve para leer lineas normal, leer palabra corregida, leer más lento.
        public static async Task LeerLineasAsync()
        {
            var config = SpeechConfig.FromSubscription("2e5647a940d94ce78f41c046c24ddf4b", "brazilsouth");
            using var sintetizador = new SpeechSynthesizer(config);
            var ssml = File.ReadAllText(@"..\..\..\data\ssml.xml");
            await sintetizador.SpeakSsmlAsync(ssml);
        }

    }
}
