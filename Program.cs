using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace WebWordCounter
{
    public class Program
    {
        public static async Task Main() //@ionene
        {
            string url = null;
            string w = null;
            int n = 0;

            Console.WriteLine("WEB WORD COUNTER\n");
            Console.WriteLine("Programa que cuenta\ncuántas veces aparece\nen una web que se indique\nla palabra que se quiera.\n");

            while(true)
            {
                Console.WriteLine("Escribe la url o exit:");
                url = Console.ReadLine();
                if (String.Equals(url.Trim().ToUpper(),"EXIT")) break;
                if (!url.StartsWith("http")) continue;

                Task<string[]> task1 = Task.Run(() => CreateWordArray(url));

                Console.WriteLine("Escribe la palabra:");
                w = Console.ReadLine();
                
                string[] words = task1.Result;                
                
                await Task.Run(() => {
                    n = GetCountForWord(words, w);
                });
                
                Console.WriteLine("La palabra aparece {0} veces.", n);
            }

            Console.WriteLine("FIN\n");

        }

        private static int GetCountForWord(string[] words, string term)
        {
            var findWord = from word in words
                           where word.ToUpper().Contains(term.ToUpper())
                           select word;
            return findWord.Count();
        }

        static string[] CreateWordArray(string uri)
        {
            string s = new WebClient().DownloadString(uri);
            return s.Split(
                new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '_', '/', '\\', '<', '>', '(', ')', '"', '\'', '&', '»' },
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
 /* 
    Un programa el cual dada una url pedida al usuario mediante el método Console.WriteLine("Escribe la url o exit:") y 
    guardada en el atributo url mediante Console.ReadLine(), busca cuantas veces sale una palabra con los métodos mencionados anteriormente y 
    lo guarda en un atributo llamado n que es un contador el cual irá incrementando conforme encuentra la misma palabra en la página web.
*/

 /* 
    Hay un metodo await (Línea 36) para que no se inicie el metodo antes de que se haya terminado la ejecuión de todo lo anterior,
    El operador await indica al compilador que el método asincrónico no puede continuar pasado ese punto,
    hasta que se complete el proceso asincrónico aguardado.
*/

 /* 
    Instrucciones del uso del programa:

        1- Ejecutar el programa en cuestión (en mi caso con dotnet run)
        2- Introducir una url o salir
            2.1 -> En caso de poner "exit"finaliza la ejecución del programa
        3- Escribir la palabra que quieres saber cuantas veces aparece
        4- Fin de ejecución del programa
    
    Al terminar el programa muestra cuantas veces aparece la palabra en esa url.
*/