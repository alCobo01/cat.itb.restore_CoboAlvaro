namespace cat.itb.restore_CoboAlvaro.depDAO
{
    public interface IDepartmentDAO
    {
        void DeleteAll();
        void InsertAll(List<Department> deps);
        List<Department> SelectAll();
        Department Select(int depId);
        bool Insert(Department dep);
        bool Delete(int depId);
        bool Update(Department dep);

    }
}