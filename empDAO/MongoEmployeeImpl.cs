using cat.itb.restore_CoboAlvaro.connections;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Runtime.Intrinsics.Arm;

namespace cat.itb.restore_CoboAlvaro.empDAO
{
   public class MongoEmployeeImpl : IEmployeeDAO
   {
        private static readonly IMongoDatabase database = MongoConnection.GetDatabase("itb");
        private static readonly IMongoCollection<Employee> collection = database.GetCollection<Employee>("employees");

        public void DeleteAll()
        {
            database.DropCollection("employees");
        }
       
        public void InsertAll(List<Employee> emps)
        {
            DeleteAll();

            try
            {
                collection.InsertMany(emps);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCollection employees inserted");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Collection couldn't be inserted: {ex.Message}");
            }

            Console.ResetColor();
        }

        public List<Employee> SelectAll()
        {
            return collection.AsQueryable().ToList();
        }


        public Employee Select(int depId)
        {
            return collection.AsQueryable()
                    .Where(d => d._id == depId)
                    .Single();
        }

        public bool Insert(Employee dep)
        {
            bool isSuccessful;
            try
            {
                collection.InsertOne(dep);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n Employees inserted");
                isSuccessful = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Employees couldn't be inserted: {ex.Message}");
                isSuccessful = false;
            }
            Console.ResetColor();
            
            return isSuccessful;
        }
        
      
        public bool Delete(int empID)
        {
            bool isSuccessful = false;

            var deleteFilter = Builders<Employee>.Filter.Eq("_id", empID);
            
            var depDeleted = collection.DeleteOne(deleteFilter);
            Console.WriteLine("Employee deleted: " + empID);

            if (depDeleted.DeletedCount != 0)
            {
                isSuccessful = true;
            }

            return isSuccessful;
        }

        public bool Update(Employee emp)
        { 
            Delete(emp._id);
            Console.WriteLine("Employee updated: " + emp._id);
            return Insert(emp);
        }

   }
}