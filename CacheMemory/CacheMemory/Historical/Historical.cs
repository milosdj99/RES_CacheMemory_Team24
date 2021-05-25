using CacheMemory.DumpingBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace CacheMemory.Historical
{
    class Historical
    {
        public static string con_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        
        public SqlConnection Con { get; set; }

        public List<Description> LD { get; set; }

        public Historical(List<Description> lD)
        {
            LD = lD;
            Con = new SqlConnection(con_string);
        }

        public Historical()
        {
            LD = new List<Description>();
            Con = new SqlConnection(con_string);
        }

        public void Recieve(DeltaCD delta)
        {
            foreach(Data data in delta.Add.DumpingPropertyCollection)   //provera ispravnosti podataka
            {
                if((data.Code=="CODE_ANALOG" || data.Code == "CODE_DIGITAL") && delta.Add.DataSet == 1)
                {
                    delta.Add.DumpingPropertyCollection.Remove(data);
                    Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");

                }
                else if ((data.Code == "CODE_CUSTOM" || data.Code == "CODE_LIMITSET") && delta.Add.DataSet == 2)
                {
                    delta.Add.DumpingPropertyCollection.Remove(data);
                    Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");

                }
                else if ((data.Code == "CODE_SINGLENODE" || data.Code == "CODE_MULTIPLENODE") && delta.Add.DataSet == 3)
                {
                    delta.Add.DumpingPropertyCollection.Remove(data);
                    Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");

                }
                else if ((data.Code == "CODE_CONSUMER" || data.Code == "CODE_SOURCE") && delta.Add.DataSet == 4)
                {
                    delta.Add.DumpingPropertyCollection.Remove(data);
                    Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");

                }
                else if ((data.Code == "CODE_MOTION" || data.Code == "CODE_SENSOR") && delta.Add.DataSet == 5)
                {
                    delta.Add.DumpingPropertyCollection.Remove(data);
                    Console.WriteLine("Podatak je obrisan jer se DataSet i Code nisu poklapali!");

                } else
                {

                }

                if(delta.Add.DumpingPropertyCollection.Count != 0)                  //prepakivanje
                {
                    List<HistoricalProperty> listaHP = new List<HistoricalProperty>();

                    foreach(Data d in delta.Add.DumpingPropertyCollection)
                    {
                        HistoricalValue hv = new HistoricalValue(d.Value.TimeStamp, d.Value.IdGeografskogPodrucja, d.Value.Potrosnja);
                        HistoricalProperty hp = new HistoricalProperty(d.Code, hv);
                        listaHP.Add(hp);
                    }
                    LD.Add(new Description(1, delta.Add.DataSet, listaHP));
                }

                if (delta.Update.DumpingPropertyCollection.Count != 0)                  //prepakivanje
                {
                    List<HistoricalProperty> listaHP = new List<HistoricalProperty>();

                    foreach (Data d in delta.Update.DumpingPropertyCollection)
                    {
                        HistoricalValue hv = new HistoricalValue(d.Value.TimeStamp, d.Value.IdGeografskogPodrucja, d.Value.Potrosnja);
                        HistoricalProperty hp = new HistoricalProperty(d.Code, hv);
                        listaHP.Add(hp);
                    }
                    LD.Add(new Description(2, delta.Add.DataSet, listaHP));
                }

                if (delta.Remove.DumpingPropertyCollection.Count != 0)                  //prepakivanje
                {
                    List<HistoricalProperty> listaHP = new List<HistoricalProperty>();

                    foreach (Data d in delta.Remove.DumpingPropertyCollection)
                    {
                        HistoricalValue hv = new HistoricalValue(d.Value.TimeStamp, d.Value.IdGeografskogPodrucja, d.Value.Potrosnja);
                        HistoricalProperty hp = new HistoricalProperty(d.Code, hv);
                        listaHP.Add(hp);
                    }
                    LD.Add(new Description(3, delta.Add.DataSet, listaHP));
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
            if(newValue > oldValue*1.02 || newValue < oldValue * 0.98)
            {
                return true;
            } else {
                return false;
            }
        }


        public void WriteToBase()
        {

            List<HistoricalProperty> listaAdd = new List<HistoricalProperty>();
            List<HistoricalProperty> listaUpdate = new List<HistoricalProperty>();
            List<HistoricalProperty> listaRemove = new List<HistoricalProperty>();
            int addDataSet = 1;
            int updateDataSet = 1;
            int removeDataSet = 1;

            List<HistoricalProperty> listaIzBaze = new List<HistoricalProperty>();

            
            
            
            foreach (Description d in LD)                           //ucitavanje podataka iz LD-a kako bi smo lakse rukovali podacima
            {
                if(d.Id==1 && d.HistoricalProperties.Count != 0)            // Description sa id=1 sadrzi listu HP-ova za ADD
                {
                    listaAdd = d.HistoricalProperties;
                    addDataSet = d.DataSet;
                }
                else if (d.Id == 2 && d.HistoricalProperties.Count != 0)            // Description sa id=2 sadrzi listu HP-ova za UPDATE
                {
                    listaUpdate = d.HistoricalProperties;
                    updateDataSet = d.DataSet;
                }
                else if (d.Id == 3 && d.HistoricalProperties.Count != 0)            // Description sa id=3 sadrzi listu HP-ova za REMOVE
                {
                    listaRemove = d.HistoricalProperties;
                    removeDataSet = d.DataSet;
                }
            }

            ///////////////////////////////////////////////////////////////// ADD
            
            if (listaAdd.Count != 0)
            {
                string q = "select Potrosnja, IdGeografskogPodrucja, TimeStamp, Code from DataSet" + addDataSet;   //provera da li u bazi vec postoje takvi podaci

                SqlCommand com = new SqlCommand(q, Con);

                if (Con.State != System.Data.ConnectionState.Open)
                {
                    Con.Open();
                }

                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())                   //ucitavamo sve podatke iz tabele sa tim dataset-om
                    {
                        HistoricalValue valueIzBaze = new HistoricalValue((DateTime)reader["TimeStamp"], (int)reader["IdGeografskogPodrucja"], (int)reader["Potrosnja"]);
                        HistoricalProperty propertyIzBaze = new HistoricalProperty((string)reader["Code"], valueIzBaze);

                        foreach (HistoricalProperty hp in listaAdd)      //ukoliko neki HS iz Add liste postoji u bazi, nece se dodati
                        {
                            if (hp.Equals(propertyIzBaze))
                            {
                                listaAdd.Remove(hp);
                            }
                        }
                    }
                }

                foreach (HistoricalProperty h in listaAdd)                           //dodavanje u bazu
                {
                    q = $"insert into DataSet{addDataSet}(Potrosnja,IdGeografskogPodrucja,TimeStamp,Code) " +
                        $"values('{h.Value.Potrosnja}','{h.Value.IdGeografskogPodrucja}','{h.Value.TimeStamp.ToString()}','{h.Code}')";
                    com = new SqlCommand(q, Con);
                    com.ExecuteNonQuery();
                }

                reader.Close();

            }

            /////////////////////////////////////////////////////////////////////  UPDATE


            if (listaUpdate.Count != 0)
            {

                string q = "select Potrosnja, IdGeografskogPodrucja, TimeStamp, Code from DataSet" + updateDataSet;   //provera da li u bazi vec postoje takvi podaci

                SqlCommand com = new SqlCommand(q, Con);

                if (Con.State != System.Data.ConnectionState.Open)
                {
                    Con.Open();
                }

                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())                   //ucitavamo sve podatke iz tabele sa tim dataset-om
                    {
                        HistoricalValue valueIzBaze = new HistoricalValue((DateTime)reader["TimeStamp"], (int)reader["IdGeografskogPodrucja"], (int)reader["Potrosnja"]);
                        HistoricalProperty propertyIzBaze = new HistoricalProperty((string)reader["Code"], valueIzBaze);

                        foreach (HistoricalProperty hp in listaAdd)      //ukoliko neki HS iz Add liste postoji u bazi, nece se dodati
                        {
                            if (hp.Equals(propertyIzBaze))
                            {
                                listaAdd.Remove(hp);
                            }
                        }
                    }
                }










            }

        }

        public List<HistoricalProperty> DataSetSearch(int dataset)
        {
                if(dataset < 1 && dataset > 5)
                {
                    Console.WriteLine("Neuspesno citanje iz baze, traze Dataset ne postoji!");
                }

                List <HistoricalProperty> returnList = new List<HistoricalProperty>();

            
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
                        HistoricalValue valueIzBaze = new HistoricalValue((DateTime)reader["TimeStamp"], (int)reader["IdGeografskogPodrucja"], (int)reader["Potrosnja"]);
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
                string q = $"select * from DataSet{i} where Code={code}";

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
                        HistoricalValue valueIzBaze = new HistoricalValue((DateTime)reader["TimeStamp"], (int)reader["IdGeografskogPodrucja"], (int)reader["Potrosnja"]);
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
                string q = $"select * from DataSet{i} where TimeStamp between {min.ToString()} and {max.ToString()}";

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
                        HistoricalValue valueIzBaze = new HistoricalValue((DateTime)reader["TimeStamp"], (int)reader["IdGeografskogPodrucja"], (int)reader["Potrosnja"]);
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
