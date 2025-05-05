using Npgsql;

namespace cat.itb.restore_CoboAlvaro.connections
{
    public class SQLConnection
    {
        private string HOST = "postgresql-alvarobbdd.alwaysdata.net"; // Ubicació de la BD.
        private string DB = "alvarobbdd_itb"; // Nom de la BD.
        private string USER = "alvarobbdd";
        private string PASSWORD = "itb2025@";

        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(
                "Host=" + HOST + ";" + "Username=" + USER + ";" +
                "Password=" + PASSWORD + ";" + "Database=" + DB + ";"
            );
            conn.Open();
            return conn;
        }
    }
}