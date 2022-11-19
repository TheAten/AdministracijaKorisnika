using AdministracijaKorisnika.Model;
using System.Collections.Generic;

namespace AdministracijaKorisnika.Interface
{
    public interface IDataAcces
    {
        void CreateUser(User model);
        void DeleteUser(int id);
        void UpdateUser(User model, int id);

        int LoginCheck(User model);
        int RegisterCheck(User model);

        List<User> GetAllUsers();
    }
}
