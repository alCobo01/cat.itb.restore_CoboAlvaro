using cat.itb.restore_CoboAlvaro.connections;
using cat.itb.restore_CoboAlvaro.empDAO;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace cat.itb.restore_CoboAlvaro.depDAO
{
    public class MongoDepartmentImpl : DepartmentDAO
    {
        private static readonly IMongoDatabase database = MongoConnection.GetDatabase("itb");
        private static readonly IMongoCollection<Department> collection = database.GetCollection<Department>("departments");

        public void DeleteAll()
        {
            database.DropCollection("departments");
        }

        public void InsertAll(List<Department> deps)
        {
            DeleteAll();

            try
            {
                collection.InsertMany(deps);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCollection departments inserted");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection couldn't be inserted");
            }

            Console.ResetColor();
        }

        public List<Department> SelectAll()
        {
            return collection.AsQueryable<Department>().ToList();
        }

        public Department Select(int depId)
        {
            return collection.AsQueryable<Department>()
                    .Where(d => d._id == depId)
                    .Single();
        }

        public bool Insert(Department dep)
        {
            bool isSuccessful;

            try
            {
                collection.InsertOne(dep);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nDepartments inserted");
                isSuccessful = true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection couldn't be inserted");
                isSuccessful = false;
            }
            Console.ResetColor();

            return isSuccessful;
        }


        public bool Delete(int depId)
        {
            bool isDeleted = false;

            var deleteFilter = Builders<Department>.Filter.Eq("_id", depId);

            var depDeleted = collection.DeleteOne(deleteFilter);
            Console.WriteLine("Department deleted: " + depId);

            if (depDeleted.DeletedCount != 0)
            {
                isDeleted = true;
            }

            return isDeleted;
        }

        public bool Update(Department dep)
        {
            Delete(dep._id);
            Console.WriteLine("Departament updated: " + dep._id);
            return Insert(dep);
        }

    }
}