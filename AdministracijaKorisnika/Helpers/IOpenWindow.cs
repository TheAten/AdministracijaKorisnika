using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracijaKorisnika.Helpers
{
    public interface IOpenWindow
    {
        Action OpenView { get; set; }
        Action OpenNew { get; set; }
    }
}
