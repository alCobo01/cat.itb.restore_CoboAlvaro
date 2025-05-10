using cat.itb.restore_CoboAlvaro.connections;
using cat.itb.restore_CoboAlvaro.depDAO;
using Npgsql;

namespace cat.itb.restore_CoboAlvaro.empDAO
{
    public class SQLEmployeeImpl : IEmployeeDAO
    {
        private NpgsqlConnection conn;

        public void DeleteAll()
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM employees", conn);

            try
            {
                cmd.ExecuteNonQuery();

                Console.WriteLine("Employees deleted");
            }
            catch
            {
                Console.WriteLine("Couldn't delete Employees");

            }

            conn.Close();

        }

        public void InsertAll(List<Employee> employees)
        {
            DeleteAll();
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO employees VALUES (@_id, @surname, @job, @managerId, @startdate, @salary, @comission, @depID)", conn);

            foreach (var emp in employees)
            {
                cmd.Parameters.AddWithValue("_id", emp._id);
                cmd.Parameters.AddWithValue("surname", emp.Surname);
                cmd.Parameters.AddWithValue("job", emp.Job);
                cmd.Parameters.AddWithValue("managerId", emp.ManagerID);
                cmd.Parameters.AddWithValue("startdate", emp.StartDate);
                cmd.Parameters.AddWithValue("salary", emp.Salary);
                cmd.Parameters.AddWithValue("comission", emp.Comission);
                cmd.Parameters.AddWithValue("depID", emp.DepartmentID);
                cmd.Prepare();

                try
                {
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Employee with Id {0} and Surname {1} added",
                        emp._id, emp.Surname);
                }
                catch
                {
                    Console.WriteLine("Couldn't add Employee with Id {0}", emp._id);
                }

                cmd.Parameters.Clear();
            }

            conn.Close();
        }

        public List<Employee> SelectAll()
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM employees", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Employee> employeeList = new List<Employee>();

            while (dr.Read())
            {
                Employee emp = new Employee()
                {
                    _id = dr.GetInt32(0),
                    Surname = dr.GetString(1),
                    Job = dr.GetString(2),
                    ManagerID = dr.IsDBNull(3) ? null : dr.GetInt32(3),
                    StartDate = dr.GetDateTime(4),
                    Salary = dr.GetDouble(5),
                    Comission = dr.IsDBNull(6) ? null : dr.GetDouble(6),
                    DepartmentID = dr.GetInt32(7)
                };

                employeeList.Add(emp);
            }

            conn.Close();
            return employeeList;
        }

        public Employee Select(int empID)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM employees WHERE _id =" + empID, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Employee employee = new Employee();

            if (dr.Read())
            {
                employee._id = dr.GetInt32(0);
                employee.Surname = dr.GetString(1);
                employee.Job = dr.GetString(2);
                employee.ManagerID = dr.IsDBNull(3) ? null : dr.GetInt32(3);
                employee.StartDate = dr.GetDateTime(4);
                employee.Salary = dr.GetDouble(5);
                employee.Comission = dr.IsDBNull(6) ? null : dr.GetDouble(6);
                employee.DepartmentID = dr.GetInt32(7);
            }
            else
            {
                employee = null;
            }

            conn.Close();

            return employee;

        }

        public bool Insert(Employee emp)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO employees VALUES (@_id, @surname, @job, @managerId, @startdate, @salary, @comission, @depID)", conn);

            cmd.Parameters.AddWithValue("_id", emp._id);
            cmd.Parameters.AddWithValue("surname", emp.Surname);
            cmd.Parameters.AddWithValue("job", emp.Job);
            cmd.Parameters.AddWithValue("managerId", emp.ManagerID == null ? DBNull.Value : emp.ManagerID);
            cmd.Parameters.AddWithValue("startdate", emp.StartDate);
            cmd.Parameters.AddWithValue("salary", emp.Salary);
            cmd.Parameters.AddWithValue("comission", emp.Comission == null ? DBNull.Value : emp.Comission);
            cmd.Parameters.AddWithValue("depID", emp.DepartmentID);
            cmd.Prepare();

            bool isSuccessful;

            try
            {
                cmd.ExecuteNonQuery();
                isSuccessful = true;
                Console.WriteLine("Employee with Id {0} and Surname {1} added",
                    emp._id, emp.Surname);
            }
            catch
            {
                isSuccessful = false;
                Console.WriteLine("Couldn't add Employee with Id {0}", emp._id);
            }

            conn.Close();

            return isSuccessful;

        }

        public bool Delete(int depId)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();
            bool isSuccessful;

            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM employees WHERE _id =" + depId, conn);

            try
            {
                cmd.ExecuteNonQuery();
                isSuccessful = true;
                Console.WriteLine("Employee with Id {0} deleted", depId);
            }
            catch
            {
                Console.WriteLine("Couldn't delete Employee with Id {0}", depId);
                isSuccessful = false;
            }

            conn.Close();
            return isSuccessful;
        }

        public bool Update(Employee emp)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE employees SET surname = @surname, job = @job, managerId = @managerId, startdate = @startdate, salary = @salary, commission = @comission, depID = @depID WHERE _id = @_id", conn);
            bool isSuccessful;

            cmd.Parameters.AddWithValue("_id", emp._id);
            cmd.Parameters.AddWithValue("surname", emp.Surname);
            cmd.Parameters.AddWithValue("job", emp.Job);
            cmd.Parameters.AddWithValue("managerId", emp.ManagerID == null ? DBNull.Value : emp.ManagerID);
            cmd.Parameters.AddWithValue("startdate", emp.StartDate);
            cmd.Parameters.AddWithValue("salary", emp.Salary);
            cmd.Parameters.AddWithValue("comission", emp.Comission == null ? DBNull.Value : emp.Comission);
            cmd.Parameters.AddWithValue("depID", emp.DepartmentID);

            cmd.Prepare();
            try
            {
                cmd.ExecuteNonQuery();
                isSuccessful = true;
                Console.WriteLine("Employee with ID {0} updated", emp._id);
            }
            catch
            {
                isSuccessful = false;
                Console.WriteLine("Couldn't update Employee {0}", emp.Surname);
            }


            conn.Close();
            return isSuccessful;
        }

    }
}