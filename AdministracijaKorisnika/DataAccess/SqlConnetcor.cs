using AdministracijaKorisnika.Interface;
using AdministracijaKorisnika.Model;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace AdministracijaKorisnika.DataAccess
{
    public class SqlConnetcor : IDataAcces
    {
        private const string db = "ADMIN_1";

        public void CreateUser(User model)
        {
            try {
                using IDbConnection dbConnection = new SqlConnection(GlobalConfig.CnnString(db));
                var p = new DynamicParameters();
                p.Add("@Username", model.Username);
                p.Add("@Password", model.Password);
                p.Add("@IsAdmin", model.IsAdmin);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                dbConnection.Open();
                dbConnection.Execute("dbo.spCreateUser", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@Id");
                dbConnection.Close();
            }
            catch(SqlException ) { 
                MessageBox.Show("Wrong Input, try Again");                
            }
            
        }

        public void DeleteUser(int id)
        {
            try
            {
                using IDbConnection dbConnection = new SqlConnection(GlobalConfig.CnnString(db));
                dbConnection.Open();
                dbConnection.Execute("dbo.spDeleteUser", new { id = id }, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
            }
            catch { MessageBox.Show("Issue with the database, change code"); }
        }

        public List<User> GetAllUsers()
        {
            List<User> output = new List<User>();



            using IDbConnection dbConnection = new SqlConnection(GlobalConfig.CnnString(db));

            dbConnection.Open();
            output = dbConnection.Query<User>("dbo.spGetAllUsers").ToList();
            dbConnection.Close();

            return output;

        }

        public int LoginCheck(User model)
        {
            int result = 1;
            try
            {
                using IDbConnection dbConnection = new SqlConnection(GlobalConfig.CnnString(db));
                var p = new DynamicParameters();
                p.Add("@Username", model.Username);
                p.Add("@Password", model.Password);

                dbConnection.Open();
                result = (int)dbConnection.ExecuteScalar("dbo.spLoginCheck", p, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
                return result;
            }
            catch (System.Exception)
            {
                MessageBox.Show("Issues with the database check your code");
                return result;
            }
        }

        public int RegisterCheck(User model)
        {
            using IDbConnection dbConnection = new SqlConnection(GlobalConfig.CnnString(db));
            var p = new DynamicParameters();
            p.Add("@Username", model.Username);

            dbConnection.Open();
            int i = (int)dbConnection.ExecuteScalar("dbo.spRegisterCheck", p, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
            return i;
        }

        public void UpdateUser(User model, int id)
        {
            try
            {
                using IDbConnection dbConnection = new SqlConnection(GlobalConfig.CnnString(db));
                var p = new DynamicParameters();
                p.Add("@Username", model.Username);
                p.Add("@Password", model.Password);
                p.Add("@IsAdmin", model.IsAdmin);
                p.Add("@id", model.Id);

                dbConnection.Open();
                dbConnection.Execute("dbo.spUpdateUser", p, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
            }
            catch (SqlException e)
            {

                MessageBox.Show(e.ToString());
            }
        }
    }
}
