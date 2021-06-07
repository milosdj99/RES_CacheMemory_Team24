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
using System.Diagnostics.CodeAnalysis;

namespace CacheMemory
{
    [ExcludeFromCodeCoverage]
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

           
            Historicall hs = new Historicall(log);

            Reader.Reader reader = new Reader.Reader(hs);

            hs.Connect();

            int izbor;
             

            while(true)
            {
                Console.WriteLine("Izaberite opciju:");
                Console.WriteLine("1.Rucno generisanje podataka");
                Console.WriteLine("2.Automatsko generisanje podataka");
                Console.WriteLine("3.Iscitavanje podataka pomocu Reader-a");
                Console.WriteLine("4. izlazak iz programa");
                
                bool parse = Int32.TryParse(Console.ReadLine(), out izbor);

                if (parse == false)
                {
                    continue;
                }

                if(izbor == 4)
                {
                    break;
                }
                                        //MANUELNI UNOS
                if (izbor == 1)
                {
                    while (true)
                    {
                        brSlanja++;

                        Console.WriteLine("Uneti novi podatak? (y)");
                        Console.WriteLine("Povratak na glavni meni (n)");

                        if (Console.ReadLine() == "n")
                        {
                            break;
                        }

                        Data manualData = new Data();

                        try
                        {
                            manualData = w.ManualWriteToDumpingBuffer(log);
                        }
                        catch(Exception)
                        {
                            Console.WriteLine("Niste uneli validan datum");
                            continue;
                        }

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

                            hs.Recieve(zaSlanje);
                            hs.WriteToBase();

                        }
                        else if (znak == 1 && (brSlanja % 10) == 0 && deltaJePoslat == false) //prvo salje, pa upisuje novo
                        {

                            hs.Recieve(zaSlanje);
                            hs.WriteToBase();

                            zaSlanje = db.PakujDCD(brTransakcija, db.listCD, log);  //novi podaci
                            znak = 0;
                            deltaJePoslat = false;

                           
                        }
                        else if (znak == 0 && (brSlanja % 10) == 0 && deltaJePoslat == false) // samo se posalju podaci
                        {
                            deltaJePoslat = true;


                            hs.Recieve(zaSlanje);
                            hs.WriteToBase();



                        }
                       
                    }
                }

                // AUTOMATSKO GENERISANJE


                if (izbor == 2)
                {
                    Console.WriteLine("Pritisnuti ESC za povratak na glavni meni");

                    while (true)
                    {

                        brSlanja++;

                        d = w.WriteToDumpingBuffer(brSlanja, log);
                        CollectionDescription c = new CollectionDescription(brSlanja, d);

                        int z = db.UpisiListCD(c, log);  //ako je 1 vreme je za pakovanje
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

                            hs.Recieve(zaSlanje);
                            hs.WriteToBase();

                        }
                        else if (znak == 1 && (brSlanja % 10) == 0 && deltaJePoslat == false) //prvo salje, pa upisuje novo
                        {


                            hs.Recieve(zaSlanje);
                            hs.WriteToBase();

                            zaSlanje = db.PakujDCD(brTransakcija, db.listCD, log);  //novi podaci
                            znak = 0;
                            deltaJePoslat = false;
                        }
                        else if (znak == 0 && (brSlanja % 10) == 0 && deltaJePoslat == false) // samo se posalju podaci
                        {
                            deltaJePoslat = true;


                            hs.Recieve(zaSlanje);
                            hs.WriteToBase();


                        }


                        if (Console.KeyAvailable)
                        {
                            var consoleKey = Console.ReadKey(true);

                            if (consoleKey.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                        }


                        Thread.Sleep(2000);
                    }
                }

                if(izbor == 3)
                {
                    int izborReader;
                    do
                    {
                        Console.WriteLine("Izaberite opciju:");
                        Console.WriteLine("1. Pretraga po Dataset-u");
                        Console.WriteLine("2. Pretraga po Code-u");
                        Console.WriteLine("3. Pretraga po datumu");
                        Console.WriteLine("4. Povratak na glavni meni");

                        bool parseReader = Int32.TryParse(Console.ReadLine(), out izborReader);

                        if (parseReader == false)
                        {
                            continue;
                        }

                        if(izborReader == 4)
                        {
                            break;
                        }

                        if(izborReader == 1)
                        {
                            int dataset;
                            Console.WriteLine("Unesite broj DataSet-a:");

                            parseReader = Int32.TryParse(Console.ReadLine(), out dataset);

                            if (parseReader == false || dataset < 1 || dataset > 5)
                            {
                                Console.WriteLine("Morate uneti broj izmedju 1 i 5, povratak na glavni meni Reader-a");
                                continue;
                            }
                            reader.DataSetSearch(dataset);                           

                        } else if (izborReader == 2)
                        {
                            
                            Console.WriteLine("Unesite Code:");                 // previse je komplikovano bilo implementirati proveru,
                                                                                // ako korisnik ne unese ispravan kod vratice mu se prazna lista
                            string code = Console.ReadLine();
                           
                            reader.CodeSearch(code);

                        } else if(izborReader == 3)
                        {
                            int dan, mesec, godina;
                            Console.WriteLine("Unesite prvi datum:");
                            Console.WriteLine("Dan:");
                            bool danB = Int32.TryParse(Console.ReadLine(), out dan);
                            Console.WriteLine("Mesec:");
                            bool mesecB = Int32.TryParse(Console.ReadLine(), out mesec);
                            Console.WriteLine("Godina:");
                            bool godinaB = Int32.TryParse(Console.ReadLine(), out godina);

                            if(danB== false || mesecB == false || godinaB == false)
                            {
                                Console.WriteLine("Niste dobro uneli datum, povratak na glavni meni reader-a");
                                continue;
                            }
                            DateTime datum1 = new DateTime(godina, mesec, dan);

                            
                            Console.WriteLine("Unesite drugi datum:");
                            Console.WriteLine("Dan:");
                            danB = Int32.TryParse(Console.ReadLine(), out dan);
                            Console.WriteLine("Mesec:");
                            mesecB = Int32.TryParse(Console.ReadLine(), out mesec);
                            Console.WriteLine("Godina:");
                            godinaB = Int32.TryParse(Console.ReadLine(), out godina);

                            if (danB == false || mesecB == false || godinaB == false)
                            {
                                Console.WriteLine("Niste dobro uneli datum, povratak na glavni meni reader-a");
                                continue;
                            }
                            DateTime datum2 = new DateTime(godina, mesec, dan);

                            reader.DateSearch(datum1, datum2);

                        } else
                        {

                        }

                    } while (izborReader < 1 || izborReader > 3);
                }

            }

            hs.Disconnect();
        }
    }
}
