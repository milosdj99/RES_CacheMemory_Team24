using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheMemory.Logger;

namespace CacheMemory.DumpingBuffer
{
    class DumpingBuff
    {
        public List<CollectionDescription> listCD { get; set; }

        public DumpingBuff() {
            listCD = new List<CollectionDescription>();
        }
        public DumpingBuff(CollectionDescription cd)
        {
            listCD.Add(cd);
        }

        public bool ProveriCode(CollectionDescription cd)
        {
            foreach (Data data in cd.DumpingPropertyCollection)
            {
                foreach (CollectionDescription c in listCD)
                {
                    foreach (Data d in c.DumpingPropertyCollection)
                    {
                        if (data.Code == d.Code)     //isti Code 
                        {
                            d.Value = data.Value;   //azuriramo vr
                            return true;
                        }

                    }
                }
            }
            return false;
        }

        public bool ProveriDataSet(CollectionDescription cd)
        {
            foreach (CollectionDescription c in listCD)
            {
                if (cd.DataSet == c.DataSet)    //isti dataset
                {
                    c.DumpingPropertyCollection.AddRange(cd.DumpingPropertyCollection); //dodamo el u okviru istog data seta
                    return true;
                    //podaci su spremni za pakovanje u DeltaCD
                }
            }
            return false;
        }

        public int UpisiListCD(CollectionDescription cd, Log log)
        {
            bool p = ProveriCode(cd);
            bool k;
            if (p == false) //ne postoji sa istim kodom
            {
                k = ProveriDataSet(cd);
                if (k == false) // ne postoji ni sa istim dataset-om
                {
                    listCD.Add(cd);
                    return 0;
                }
                else  //postoje 2 sa istim dataSetom
                {
                    return 1; //znak da je vreme za pakovanje u delta cd
                }

            }

            log.LogMsg("Podaci uneti u ListCD");

            return 0;
            
        }

        public DeltaCD PakujDCD(int brtr, List<CollectionDescription> listCD, Log log)
        {
            DeltaCD dcd = new DeltaCD();

            dcd.Add = new List<CollectionDescription>();
            dcd.Update = new List<CollectionDescription>();
            dcd.Remove = new List<CollectionDescription>();

                dcd.TransactionId = brtr;

                foreach (CollectionDescription cd in listCD)
                {
                    if ((cd.Id % 5) == 0)
                    {
                        dcd.Update.Add(cd);
                    }
                    else if ((cd.Id % 7) == 0)
                    {
                        dcd.Remove.Add(cd);
                    }
                    else
                    {
                        dcd.Add.Add(cd);
                    }
                }

                brtr++;

            listCD.Clear();
            listCD.TrimExcess();

            log.LogMsg("Podaci uneti u DeltaCD, listCD osloboljen");

                return dcd;

         }
        

    }
}
