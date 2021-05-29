using CacheMemory.DumpingBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using CacheMemory.Logger;

namespace CacheMemory.Historical
{
    class Historicall
    {
        
        public static string con_string = @"Data Source=(LocalDB)\MSSQLLocalDB; " +
                @"AttachDbFilename=|DataDirectory|Database1.mdf;
                Integrated Security=True;
                Connect Timeout=30";
       
        public SqlConnection Con { get; set; }

        public List<Description> LD { get; set; }

        Log  Logger { get; set; }

        
        public Historicall(Log log)
        {
            LD = new List<Description>();
            Con = new SqlConnection(con_string);
            Logger = log;

        }

        public List<CollectionDescription> CheckData(List<CollectionDescription> list)
        {


            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].DumpingPropertyCollection.Count; j++)
                {
                    Data data = list[i].DumpingPropertyCollection[j];

                    if ((data.Code == "CODE_ANALOG" || data.Code == "CODE_DIGITAL") && list[i].DataSet != 1)
                    {
                        Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        Logger.LogMsg("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        list[i].DumpingPropertyCollection.Remove(data);
                    }
                    else if ((data.Code == "CODE_CUSTOM" || data.Code == "CODE_LIMITSET") && list[i].DataSet != 2)
                    {
                        Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        Logger.LogMsg("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        list[i].DumpingPropertyCollection.Remove(data);

                    }
                    else if ((data.Code == "CODE_SINGLENODE" || data.Code == "CODE_MULTIPLENODE") && list[i].DataSet != 3)
                    {
                        Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        Logger.LogMsg("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        list[i].DumpingPropertyCollection.Remove(data);

                    }
                    else if ((data.Code == "CODE_CONSUMER" || data.Code == "CODE_SOURCE") && list[i].DataSet != 4)
                    {
                        Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        Logger.LogMsg("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        list[i].DumpingPropertyCollection.Remove(data);

                    }
                    else if ((data.Code == "CODE_MOTION" || data.Code == "CODE_SENSOR") && list[i].DataSet != 5)
                    {
                        Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        Logger.LogMsg("Podatak je obrisan jer se DataSet i Code nisu poklapali!");
                        list[i].DumpingPropertyCollection.Remove(data);

                    }
                    else
                    {

                    }
                }


            }
            return list;
        }

        public void Recieve(DeltaCD delta)
        {
           
             List<CollectionDescription> Add = CheckData(delta.Add);
                       
             List<CollectionDescription> Update = CheckData(delta.Update);
                       
             List<CollectionDescription> Remove = CheckData(delta.Remove);
            


            if (Add.Count != 0)                  //prepakivanje
            {
                foreach (CollectionDescription cd in Add) {
                    List<HistoricalProperty> listaHP = new List<HistoricalProperty>();

                    foreach (Data d in cd.DumpingPropertyCollection)
                    {
                        HistoricalValue hv = new HistoricalValue(d.Value.TimeStamp, d.Value.IdGeografskogPodrucja, d.Value.Potrosnja);
                        HistoricalProperty hp = new HistoricalProperty(d.Code, hv);
                        listaHP.Add(hp);
                    }
                    LD.Add(new Description(1, cd.Id, cd.DataSet, listaHP));
                }
            }

            if (Update.Count != 0)                  //prepakivanje
            {
                foreach (CollectionDescription cd in Update)
                {
                    List<HistoricalProperty> listaHP = new List<HistoricalProperty>();

                    foreach (Data d in cd.DumpingPropertyCollection)
                    {
                        HistoricalValue hv = new HistoricalValue(d.Value.TimeStamp, d.Value.IdGeografskogPodrucja, d.Value.Potrosnja);
                        HistoricalProperty hp = new HistoricalProperty(d.Code, hv);
                        listaHP.Add(hp);
                    }
                    LD.Add(new Description(2, cd.Id, cd.DataSet, listaHP));
                }
            }

            if (Remove.Count != 0)                  //prepakivanje
            {
                foreach (CollectionDescription cd in Remove)
                {
                    List<HistoricalProperty> listaHP = new List<HistoricalProperty>();

                    foreach (Data d in cd.DumpingPropertyCollection)
                    {
                        HistoricalValue hv = new HistoricalValue(d.Value.TimeStamp, d.Value.IdGeografskogPodrucja, d.Value.Potrosnja);
                        HistoricalProperty hp = new HistoricalProperty(d.Code, hv);
                        listaHP.Add(hp);
                    }
                    LD.Add(new Description(3, cd.Id, cd.DataSet, listaHP));
                }
            }


        }



        public void Connect()
        {
            try
            {
                Con.Open();
                Console.WriteLine("Uspesno konektovanje sa bazom podataka.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Bezuspesno konektovanje sa bazom podataka.");
                Console.WriteLine(e.Message);
            }

        }

        public void Disconnect()
        {
            try
            {
                Con.Close();
                Console.WriteLine("Uspesno diskonektovanje sa bazom podataka.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Bezuspesno diskonektovanje sa bazom podataka.");
                Console.WriteLine(e.Message);
            }
        }

        public bool CheckDeadBand(int oldValue, int newValue)           //provera da li podatak treba da se upise u bazu ili ne
        {
            if (newValue > oldValue * 1.02 || newValue < oldValue * 0.98)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void WriteToBase()
        {

            foreach (Description d in LD)                           //ucitavanje podataka iz LD-a kako bi smo lakse rukovali podacima
            {
                if (d.HistoricalProperties.Count != 0)
                {
                    if (d.Tip == 1)
                    {
                        Add(d);
                    }
                    else if (d.Tip == 2)
                    {
                        Update(d);
                    }
                    else if (d.Tip == 3)
                    {
                        Remove(d);
                    }
                }
            }
        }

       
        public void Add(Description d) {

            
               string q = "select Id from DataSet" + d.DataSet;   //provera da li u bazi vec postoje takvi podaci

                SqlCommand com = new SqlCommand(q, Con);

                if (Con.State != System.Data.ConnectionState.Open)
                {
                    Con.Open();
                }

                SqlDataReader reader = com.ExecuteReader();

                bool postoji = false;

                if (reader.HasRows)
                {
                    while (reader.Read())                   
                    {
                        if((int)reader["Id"] == d.Id)
                        {
                            postoji = true;
                            break;
                        }
                    }
                }

            reader.Close();

            if (postoji== false)
            {
                string format = "yyyy-MM-dd HH:mm:ss";
                foreach (HistoricalProperty h in d.HistoricalProperties)                           //dodavanje u bazu
                {
                    q = $"insert into DataSet{d.DataSet}(Id, Potrosnja, IdGeografskojPodrucja, TimeStamp,Code) " +
                        $"values({d.Id},{h.Value.Potrosnja},{h.Value.IdGeografskogPodrucja},'{h.Value.TimeStamp.ToString(format)}','{h.Code}')";

                    com = new SqlCommand(q, Con);
                    com.ExecuteNonQuery();
                    Logger.LogMsg("Podatak dodat u bazu");
                }
            }
                reader.Close();

            
        }
        

        public void Update(Description d)
        {

            foreach (HistoricalProperty hp in d.HistoricalProperties) {

                string q = $"select Code, Potrosnja from DataSet{d.DataSet} where Id={d.Id}";   //provera da li u bazi postoje takvi podaci

                SqlCommand com = new SqlCommand(q, Con);

                if (Con.State != System.Data.ConnectionState.Open)
                {
                    Con.Open();
                }

                SqlDataReader reader = com.ExecuteReader();

                List<int> potrosnje = new List<int>();

                if (reader.HasRows)
                {
                    while (reader.Read())                   //ucitavamo potrosnje u listu
                    {
                        int potrosnja = (int)reader["Potrosnja"];
                        potrosnje.Add(potrosnja);
                    }
                }

                reader.Close();

                foreach (int p in potrosnje)                    //izbacujemo potrosnje koje ne treba menjati
                {
                    if (CheckDeadBand(p, hp.Value.Potrosnja)==false)
                    {
                        potrosnje.Remove(p);
                    }
                }

                if (potrosnje.Count == 0)
                {
                    q = $"update DataSet{d.DataSet} set Potrosnja={hp.Value.Potrosnja} where Id='{d.Id}' and Code='CODE_DIGITAL'";    //pravimo query

                }
                else if (potrosnje.Count == 1)
                {
                    q = $"update DataSet{d.DataSet} set Potrosnja={hp.Value.Potrosnja} where Id='{d.Id}' and Code='CODE_DIGITAL' and Potrosnja={potrosnje[0]}";    //pravimo query

                }
                else
                {

                    q = $"update DataSet{d.DataSet} set Potrosnja={hp.Value.Potrosnja} where Id='{d.Id}' and (Code='CODE_DIGITAL' or Potrosnja in (";    //pravimo query

                    foreach (int p in potrosnje)
                    {
                        q += $"{p},";

                    }

                    q = q.Remove(q.Length - 1, 1);        //brisemo zarez

                    q += ")";
                }
                com = new SqlCommand(q, Con);
                com.ExecuteNonQuery();
                Logger.LogMsg("Podatak u bazi izmenjen");

                reader.Close();

            }

            
        }

        public void Remove(Description d)
        {

            foreach (HistoricalProperty hp in d.HistoricalProperties)
            {

                string q = $"delete from DataSet{d.DataSet} where Id={d.Id}";   

                SqlCommand com = new SqlCommand(q, Con);

                if (Con.State != System.Data.ConnectionState.Open)
                {
                    Con.Open();
                }
                
                com.ExecuteNonQuery();
                Logger.LogMsg("Podatak/ci izbrisan/i iz baze");

            }


        }



        public List<HistoricalProperty> DataSetSearch(int dataset)
        {
            if (dataset < 1 && dataset > 5)
            {
                Console.WriteLine("Neuspesno citanje iz baze, trazeni Dataset ne postoji!");
            }

            List<HistoricalProperty> returnList = new List<HistoricalProperty>();


            string q = "select * from DataSet" + dataset;

            SqlCommand com = new SqlCommand(q, Con);

            if (Con.State != System.Data.ConnectionState.Open)
            {
                Con.Open();
            }

            SqlDataReader reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HistoricalValue valueIzBaze = new HistoricalValue((DateTime)reader["TimeStamp"], (int)reader["IdGeografskojPodrucja"], (int)reader["Potrosnja"]);
                    HistoricalProperty propertyIzBaze = new HistoricalProperty((string)reader["Code"], valueIzBaze);

                    returnList.Add(propertyIzBaze);

                }
            }

            reader.Close();

            return returnList;

        }



        public List<HistoricalProperty> CodeSearch(string code)
        {

            List<HistoricalProperty> returnList = new List<HistoricalProperty>();

            for (int i = 1; i <= 5; i++)
            {
                string q = $"select * from DataSet{i} where Code='{code}'";

                SqlCommand com = new SqlCommand(q, Con);

                if (Con.State != System.Data.ConnectionState.Open)
                {
                    Con.Open();
                }

                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HistoricalValue valueIzBaze = new HistoricalValue((DateTime)reader["TimeStamp"], (int)reader["IdGeografskojPodrucja"], (int)reader["Potrosnja"]);
                        HistoricalProperty propertyIzBaze = new HistoricalProperty((string)reader["Code"], valueIzBaze);

                        returnList.Add(propertyIzBaze);

                    }
                }

                reader.Close();


            }

            return returnList;
        }


        public List<HistoricalProperty> DateSearch(DateTime min, DateTime max)
        {

            List<HistoricalProperty> returnList = new List<HistoricalProperty>();

            for (int i = 1; i <= 5; i++)
            {
                

                string q = $"select * from DataSet{i} where TimeStamp between '{min.Year}/{min.Month}/{min.Day}'" +
                    $" and '{max.Year}/{max.Month}/{max.Day}'";

                SqlCommand com = new SqlCommand(q, Con);

                if (Con.State != System.Data.ConnectionState.Open)
                {
                    Con.Open();
                }

                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HistoricalValue valueIzBaze = new HistoricalValue((DateTime)reader["TimeStamp"], (int)reader["IdGeografskojPodrucja"], (int)reader["Potrosnja"]);
                        HistoricalProperty propertyIzBaze = new HistoricalProperty((string)reader["Code"], valueIzBaze);

                        returnList.Add(propertyIzBaze);

                    }
                }

                reader.Close();


            }

            return returnList;
        }

    }

}

