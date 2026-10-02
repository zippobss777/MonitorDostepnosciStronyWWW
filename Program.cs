using System.Diagnostics;
using System.Threading;

namespace MonitorDostepnosciStronyWWW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch.StartNew();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Wprowadź adres url: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string url = Console.ReadLine();
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.Write("\nPodaj co ile sekund sprawdzać stronę: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            int sekundy = int.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n============================================");

            int czasMs = sekundy * 1000;

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "https://" + url;
            }

            while (true)
            {
                using (HttpClient client = new HttpClient())
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    HttpResponseMessage response = client.GetAsync(url).Result;

                    stopwatch.Stop();

                    if (response.IsSuccessStatusCode)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\n[ONLINE] ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"Strona \u001b[4m{url}\u001b[0m działa! Czas odpowiedzi: {stopwatch.ElapsedMilliseconds} ms");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n[OFFLINE] ");
                        Console.ForegroundColor= ConsoleColor.Gray;
                        Console.WriteLine($"Strona \u001b[4m{url}\u001b[0m zwróciła błąd. Kod statusu: {response.StatusCode}");
                    }
                }

                Thread.Sleep(czasMs);
            }
        }
    }
}