using CacheMemory.Logger;
using CacheMemory.DumpingBuffer;
using CacheMemory.Reader;
using CacheMemory.Writer;
using CacheMemory.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CacheMemory
{
    class Program
    {
        static void Main(string[] args)
        {

            Log log = new Log();

            int brSlanja = 0;
            int brTransakcija = 0;
            bool deltaJePoslat = true;
            int znak = 0;

            Data d = new Data();
            DumpingBuff db = new DumpingBuff();
            Write w = new Write();
            DeltaCD zaSlanje = new DeltaCD();

            //  RUCNI UNOS PODATAKA  

            while (true)
            {
                brSlanja++;

                Console.WriteLine("Pritisnuti ESC za prelazak na automatsko generisanje... ");
                Data manualData = w.ManualWriteToDumpingBuffer(log);
                CollectionDescription manualCD = new CollectionDescription(brSlanja, manualData);

                int z = db.UpisiListCD(manualCD, log);
                if (z == 1)
                {
                    znak = 1;
                }

                if (znak == 1 && (brSlanja % 10) != 0 && deltaJePoslat == true) //moze da se upise, ali ne i da se posalje
                {
                    zaSlanje = db.PakujDCD(brTransakcija, db.listCD, log);
                    deltaJePoslat = false;
                    znak = 0;

                }
                else if (znak == 1 && (brSlanja % 10) == 0 && deltaJePoslat == true) //moze da se i upise i posalje
                {
                    zaSlanje = db.PakujDCD(brTransakcija, db.listCD, log);
                    znak = 0;

                    // historical prima komponentu zaSlanje
                    //.....

                }
                else if (znak == 1 && (brSlanja % 10) == 0 && deltaJePoslat == false) //prvo salje, pa upisuje novo
                {


                    // historical prima komponentu zaSlanje
                    //.....

                    zaSlanje = db.PakujDCD(brTransakcija, db.listCD, log);  //novi podaci
                    znak = 0;
                    deltaJePoslat = false;
                }
                else if (znak == 0 && (brSlanja % 10) == 0 && deltaJePoslat == false) // samo se posalju podaci
                {
                    deltaJePoslat = true;


                    // historical prima komponentu zaSlanje
                    //.....


                }

                if (Console.KeyAvailable)
                {
                    var consoleKey = Console.ReadKey(true);

                    if (consoleKey.Key == ConsoleKey.Escape)
                    {
                        break;
                       
                    }
                }
            }
            
            // AUTOMATSKO GENERISANJE

            Console.WriteLine("Pritisnuti ESC za pauziranje generisanja... ");

            while(true)
            {
                brSlanja++;

                d = w.WriteToDumpingBuffer(brSlanja, log);
                CollectionDescription c = new CollectionDescription(brSlanja, d);

                int z = db.UpisiListCD(c, log);  //ako je 1 vreme je za pakovanje
                if(z == 1)
                {
                    znak = 1;
                }


                if (znak == 1 && (brSlanja % 10) !=0 && deltaJePoslat == true) //moze da se upise, ali ne i da se posalje
                {
                    zaSlanje = db.PakujDCD(brTransakcija, db.listCD, log);
                    deltaJePoslat = false;
                    znak = 0;
                    
                }
                else if (znak == 1 && (brSlanja % 10) == 0 && deltaJePoslat == true) //moze da se i upise i posalje
                {
                    zaSlanje = db.PakujDCD(brTransakcija, db.listCD, log);
                    znak = 0;

                    // historical prima komponentu zaSlanje
                    //.....

                }
                else if (znak == 1 && (brSlanja % 10) == 0 && deltaJePoslat == false) //prvo salje, pa upisuje novo
                {


                    // historical prima komponentu zaSlanje
                    //.....

                    zaSlanje = db.PakujDCD(brTransakcija, db.listCD, log);  //novi podaci
                    znak = 0;
                    deltaJePoslat = false;
                }
                else if (znak == 0 && (brSlanja % 10) == 0 && deltaJePoslat == false) // samo se posalju podaci
                {
                    deltaJePoslat = true;


                    // historical prima komponentu zaSlanje
                    //.....


                }


                if (Console.KeyAvailable)
                {
                    var consoleKey = Console.ReadKey(true);  
                                                             
                    if (consoleKey.Key == ConsoleKey.Escape)
                    {
                        //break; pa van petlje reader 
                        // ili ovde pozovi reader pa onda nastavlja generisanje kad zavrsi
                    }
                }


                Thread.Sleep(2000);
            }


        }
    }
}
