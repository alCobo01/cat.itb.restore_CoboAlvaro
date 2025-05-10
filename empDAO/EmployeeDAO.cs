namespace cat.itb.restore_CoboAlvaro.empDAO
{
    public interface IEmployeeDAO
    {
        void DeleteAll();
        void InsertAll(List<Employee> emps);
        List<Employee> SelectAll();
        Employee Select(int empId);
        bool Insert(Employee emp);
        bool Delete(int empId);
        bool Update(Employee emp);
    }
}
