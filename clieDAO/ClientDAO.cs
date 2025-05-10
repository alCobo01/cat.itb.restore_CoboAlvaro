namespace cat.itb.restore_CoboAlvaro.clieDAO
{
    public interface IClientDAO
    {
        void DeleteAll();
        void InsertAll(List<Client> clients);
        List<Client> SelectAll();
        Client Select(int clieID);
        List<Client> SelectByEmpId(int empID);
        List<Client> SelectByEmpSurname(string empSurname);
        bool Insert(Client client);
        bool Delete(int clieID);
        bool Update(Client client);
    }
}
