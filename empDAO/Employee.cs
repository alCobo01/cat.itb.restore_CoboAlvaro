using cat.itb.restore_CoboAlvaro.depDAO;

namespace cat.itb.restore_CoboAlvaro.empDAO
{
    public class Employee
    {
        public int _id { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public int ManagerID { get; set; }
        public string StartDate { get; set; }
        public double Salary { get; set; }
        public double Comission { get; set; }
        public int DepartmentID { get; set; }
    }
}
