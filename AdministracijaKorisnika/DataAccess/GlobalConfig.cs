using System.Configuration;
using AdministracijaKorisnika.Interface;

namespace AdministracijaKorisnika.DataAccess
{
    public static class GlobalConfig
    {
        public static IDataAcces dataAcces { get; private set; }

        public static void InitializeConnection()
        {
            SqlConnetcor sql = new SqlConnetcor();
            dataAcces = sql;
        }

        public static string CnnString(string name) =>
            ConfigurationManager.ConnectionStrings[name].ConnectionString;
    }
}
