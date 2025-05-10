using Newtonsoft.Json;

namespace cat.itb.restore_CoboAlvaro.clieDAO
{
    public class FileClientImpl : IClientDAO
    {
        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void InsertAll(List<Client> clients)
        {
            FileInfo file = new FileInfo("../../../clients.json");
            StreamWriter sw = file.CreateText();
            try
            {
                foreach (var client in clients)
                    sw.WriteLine(JsonConvert.SerializeObject(client));

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nSuccesful inserts in file clients.json");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInserts in clients.json couldn't be executed");
            }
            sw.Close();
            Console.ResetColor();
        }

        public List<Client> SelectAll()
        {
            FileInfo file = new FileInfo("../../../clients.json");
            StreamReader sr = file.OpenText();

            string client;
            List<Client> list = new List<Client>();

            while ((client = sr.ReadLine()) != null)
                list.Add(JsonConvert.DeserializeObject<Client>(client));

            sr.Close();

            return list;
        }

        public Client Select(int clieID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Client client)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int clieID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Client client)
        {
            throw new NotImplementedException();
        }

        public List<Client> SelectByEmpId(int empID)
        {
            throw new NotImplementedException();
        }

        public List<Client> SelectByEmpSurname(string empSurname)
        {
            throw new NotImplementedException();
        }
    }
}
