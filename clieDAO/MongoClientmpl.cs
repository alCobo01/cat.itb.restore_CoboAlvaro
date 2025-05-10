using cat.itb.restore_CoboAlvaro.connections;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace cat.itb.restore_CoboAlvaro.clieDAO
{
   public class MongoClientImpl : IClientDAO
   {
        private static readonly IMongoDatabase database = MongoConnection.GetDatabase("itb");
        private static readonly IMongoCollection<Client> collection = database.GetCollection<Client>("clients");

        public void DeleteAll()
        {
            database.DropCollection("clients");
        }
       
        public void InsertAll(List<Client> clients)
        {
            DeleteAll();

            try
            {
                collection.InsertMany(clients);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCollection clients inserted");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Collection couldn't be inserted {ex.Message}");
            }

            Console.ResetColor();
        }

        public List<Client> SelectAll()
        {
            return collection.AsQueryable().ToList();
        }

        public Client Select(int clientID)
        {
            return collection.AsQueryable()
                    .Where(d => d._id == clientID)
                    .Single();
        }

        public bool Insert(Client client)
        {
            bool isSuccessful;

            try
            {
                collection.InsertOne(client);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n Clients inserted");
                isSuccessful = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Clients couldn't be inserted: {ex.Message}");
                isSuccessful = false;
            }

            Console.ResetColor();
            
            return isSuccessful;
        }
      
        public bool Delete(int clientID)
        {
            bool isSuccessful = false;

            var deleteFilter = Builders<Client>.Filter.Eq("_id", clientID);
            
            var depDeleted = collection.DeleteOne(deleteFilter);
            Console.WriteLine("Employee deleted: " + clientID);

            if (depDeleted.DeletedCount != 0)
            {
                isSuccessful = true;
            }

            return isSuccessful;
        }

        public bool Update(Client client)
        { 
            Delete(client._id);
            Console.WriteLine("Employee updated: " + client._id);
            return Insert(client);
        }

        public List<Client> SelectByEmpId(int empID)
        {
            return collection.AsQueryable()
                    .Where(d => d.EmployeeId == empID)
                    .ToList();
        }

        public List<Client> SelectByEmpSurname(string empSurname)
        {
            return collection.Aggregate()
                .Lookup("employees", "EmployeeId", "_id", "employee")
                .Unwind("employee")
                .Match(Builders<BsonDocument>.Filter.Eq("employee.Surname", empSurname))
                .Project(new BsonDocument
                {
                    { "_id", "$_id" },
                    { "Name", "$Name" },
                    { "Address", "$Address" },
                    { "City", "$City" },
                    { "State", "$State" },
                    { "Zipcode", "$Zipcode" },
                    { "Area", "$Area" },
                    { "Phone", "$Phone" },
                    { "EmployeeId", "$EmployeeId" },
                    { "Credit", "$Credit" },
                    { "Comments", "$Comments" }
                })
                .As<Client>()
                .ToList();
        }
    }
}