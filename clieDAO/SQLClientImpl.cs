using cat.itb.restore_CoboAlvaro.connections;
using cat.itb.restore_CoboAlvaro.empDAO;
using Npgsql;

namespace cat.itb.restore_CoboAlvaro.clieDAO
{
    public class SQLClientImpl : IClientDAO
    {
        private NpgsqlConnection conn;

        public void DeleteAll()
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM clients", conn);

            try
            {
                cmd.ExecuteNonQuery();

                Console.WriteLine("Clients deleted");
            }
            catch
            {
                Console.WriteLine("Couldn't delete Clients");

            }

            conn.Close();

        }

        public void InsertAll(List<Client> clients)
        {
            DeleteAll();
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO clients VALUES (@_id, @name, @address, @city, @state, @zipcode, @area, @phone, @employeeID, @credit, @comments)", conn);

            foreach (var clie in clients)
            {
                cmd.Parameters.AddWithValue("_id", clie._id);
                cmd.Parameters.AddWithValue("name", clie.Name);
                cmd.Parameters.AddWithValue("address", clie.Address);
                cmd.Parameters.AddWithValue("city", clie.City);
                cmd.Parameters.AddWithValue("state", clie.State);
                cmd.Parameters.AddWithValue("zipcode", clie.Zipcode);
                cmd.Parameters.AddWithValue("area", clie.Area);
                cmd.Parameters.AddWithValue("phone", clie.Phone);
                cmd.Parameters.AddWithValue("employeeID", clie.EmployeeId);
                cmd.Parameters.AddWithValue("credit", clie.Credit);
                cmd.Parameters.AddWithValue("comments", clie.Comments);
                cmd.Prepare();

                try
                {
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Client with Id {0} and Name {1} added",
                        clie._id, clie.Name);
                }
                catch
                {
                    Console.WriteLine("Couldn't add client with Id {0}", clie._id);
                }

                cmd.Parameters.Clear();

            }
            conn.Close();
        } 

        public List<Client> SelectAll()
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM clients", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Client> clientList = new List<Client>();

            while (dr.Read())
            {
                Client client = new Client()
                {
                    _id = dr.GetInt32(0),
                    Name = dr.GetString(1),
                    Address = dr.GetString(2),
                    City = dr.GetString(3),
                    State = dr.GetString(4),
                    Zipcode = dr.GetString(5),
                    Area = dr.GetInt32(6),
                    Phone = dr.GetString(7),
                    EmployeeId = dr.GetInt32(8),
                    Credit = dr.GetDouble(9),
                    Comments = dr.IsDBNull(10) ? null : dr.GetString(10)
                };

                clientList.Add(client);
            }

            conn.Close();
            return clientList;
        }

        public Client Select(int clientId)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM clients WHERE _id =" + clientId, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Client client = new Client();

            if (dr.Read())
            {
                client._id = dr.GetInt32(0);
                client.Name = dr.GetString(1);
                client.Address = dr.GetString(2);
                client.City = dr.GetString(3);
                client.State = dr.GetString(4);
                client.Zipcode = dr.GetString(5);
                client.Area = dr.GetInt32(6);
                client.Phone = dr.GetString(7);
                client.EmployeeId = dr.GetInt32(8);
                client.Credit = dr.GetDouble(9);
                client.Comments = dr.IsDBNull(10) ? null : dr.GetString(10);
            }
            else
            {
                client = null;
            }

            conn.Close();

            return client;

        }

        public bool Insert(Client clie)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO clients VALUES (@_id, @name, @address, @city, @state, @zipcode, @area, @phone, @employeeID, @credit, @comments)", conn);

            cmd.Parameters.AddWithValue("_id", clie._id);
            cmd.Parameters.AddWithValue("name", clie.Name);
            cmd.Parameters.AddWithValue("address", clie.Address);
            cmd.Parameters.AddWithValue("city", clie.City);
            cmd.Parameters.AddWithValue("state", clie.State);
            cmd.Parameters.AddWithValue("zipcode", clie.Zipcode);
            cmd.Parameters.AddWithValue("area", clie.Area);
            cmd.Parameters.AddWithValue("phone", clie.Phone);
            cmd.Parameters.AddWithValue("employeeID", clie.EmployeeId);
            cmd.Parameters.AddWithValue("credit", clie.Credit);
            cmd.Parameters.AddWithValue("comments", clie.Comments == null ? DBNull.Value : clie.Comments);
            cmd.Prepare();

            bool isSuccessful;

            try
            {
                cmd.ExecuteNonQuery();
                isSuccessful = true;
                Console.WriteLine("Client with Id {0} and Name {1} added",
                    clie._id, clie.Name);
            }
            catch
            {
                isSuccessful = false;
                Console.WriteLine("Couldn't add client with Id {0}", clie._id);
            }

            conn.Close();

            return isSuccessful;

        }

        public bool Delete(int clienID)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();
            bool isSuccessful;

            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM clients WHERE _id =" + clienID, conn);

            try
            {
                cmd.ExecuteNonQuery();
                isSuccessful = true;
                Console.WriteLine("Client with Id {0} deleted", clienID);
            }
            catch
            {
                Console.WriteLine("Couldn't delete client with Id {0}", clienID);
                isSuccessful = false;
            }

            conn.Close();
            return isSuccessful;
        }

        public bool Update(Client client)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE clients SET name = @name, address = @address, city = @city, st = @state, zipcode = @zipcode, area = @area, phone = @phone, empID = @employeeID, credit = @credit, comments = @comments WHERE _id = @_id", conn);
            bool isSuccessful;

            cmd.Parameters.AddWithValue("_id", client._id);
            cmd.Parameters.AddWithValue("name", client.Name);
            cmd.Parameters.AddWithValue("address", client.Address);
            cmd.Parameters.AddWithValue("city", client.City);
            cmd.Parameters.AddWithValue("state", client.State);
            cmd.Parameters.AddWithValue("zipcode", client.Zipcode);
            cmd.Parameters.AddWithValue("area", client.Area);
            cmd.Parameters.AddWithValue("phone", client.Phone);
            cmd.Parameters.AddWithValue("employeeID", client.EmployeeId);
            cmd.Parameters.AddWithValue("credit", client.Credit);
            cmd.Parameters.AddWithValue("comments", client.Comments == null ? DBNull.Value : client.Comments);
            cmd.Prepare();
            try
            {
                cmd.ExecuteNonQuery();
                isSuccessful = true;
                Console.WriteLine("Client with ID {0} updated", client._id);
            }
            catch
            {
                isSuccessful = false;
                Console.WriteLine("Couldn't update client {0}", client.Name);
            }


            conn.Close();
            return isSuccessful;
        }

        public List<Client> SelectByEmpId(int empId)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM clients WHERE empID =" + empId, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Client> clientList = new List<Client>();
            while (dr.Read())
            {
                Client client = new Client()
                {
                    _id = dr.GetInt32(0),
                    Name = dr.GetString(1),
                    Address = dr.GetString(2),
                    City = dr.GetString(3),
                    State = dr.GetString(4),
                    Zipcode = dr.GetString(5),
                    Area = dr.GetInt32(6),
                    Phone = dr.GetString(7),
                    EmployeeId = dr.GetInt32(8),
                    Credit = dr.GetDouble(9),
                    Comments = dr.IsDBNull(10) ? null : dr.GetString(10)
                };
                clientList.Add(client);
            }

            conn.Close();

            return clientList;
        }

        public List<Client> SelectByEmpSurname(string empSurname)
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM clients c JOIN employees e ON c.empid = e._id WHERE e.surname = @surname", conn);
            cmd.Parameters.AddWithValue("surname", empSurname);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Client> clientList = new List<Client>();
            while (dr.Read())
            {
                Client client = new Client()
                {
                    _id = dr.GetInt32(0),
                    Name = dr.GetString(1),
                    Address = dr.GetString(2),
                    City = dr.GetString(3),
                    State = dr.GetString(4),
                    Zipcode = dr.GetString(5),
                    Area = dr.GetInt32(6),
                    Phone = dr.GetString(7),
                    EmployeeId = dr.GetInt32(8),
                    Credit = dr.GetDouble(9),
                    Comments = dr.IsDBNull(10) ? null : dr.GetString(10)
                };
                clientList.Add(client);
            }

            conn.Close();

            return clientList;
        }
    }
}