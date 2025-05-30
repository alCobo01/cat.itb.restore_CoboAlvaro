﻿using Newtonsoft.Json;

namespace cat.itb.restore_CoboAlvaro.empDAO
{
    public class FileEmployeeImpl : IEmployeeDAO
    {
        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void InsertAll(List<Employee> deps)
        {
            FileInfo file = new FileInfo("../../../employees.json");
            StreamWriter sw = file.CreateText();
            try
            {
                foreach (var dep in deps)
                    sw.WriteLine(JsonConvert.SerializeObject(dep));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nSuccesful inserts in file employees.json");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInserts in employees.json couldn't be executed");
            }
            sw.Close();
            Console.ResetColor();
        }

        public List<Employee> SelectAll()
        {
            FileInfo file = new FileInfo("../../../employees.json");
            StreamReader sr = file.OpenText();

            string emp;
            List<Employee> list = new List<Employee>();

            while ((emp = sr.ReadLine()) != null)
                list.Add(JsonConvert.DeserializeObject<Employee>(emp));

            sr.Close();

            return list;
        }

        public Employee Select(int empID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Employee emp)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int empID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Employee emp)
        {
            throw new NotImplementedException();
        }
    }
}
