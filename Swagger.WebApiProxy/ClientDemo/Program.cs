using SwaggerServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo
{
    class Program
    {
        static async void StartDemo()
        {
            FlugWebProxy proxy = new FlugWebProxy(new Uri("http://localhost:8082"));

            // Mit der nächsten Zeile könnte man ein OAuth2-Token festlegen
            // proxy.AuthToken = "1234";

            var flug = new Flug
            {
                ablugOrt = "Graz",
                zielOrt = "Kognito",
                datum = DateTime.Now,
                flugNr = "4711"
            };

            await proxy.ApiFlugPost(flug);

            var fluege = await proxy.ApiFlugGet();

            foreach (var f in fluege)
            {
                Console.WriteLine($"{f.id}: {f.ablugOrt} - {f.zielOrt} am {f.datum}");
            }

            Console.WriteLine("Fertig!");

        }

        static void Main(string[] args)
        {
            StartDemo();
            
            Console.ReadLine();
        }
    }
}
