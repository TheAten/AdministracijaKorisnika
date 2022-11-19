using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracijaKorisnika.Helpers
{
    public class Mediators
    {
        static readonly Mediators instance = new Mediators();

        public static Mediators Instance
        {
            get
            {
                return instance;    
            }
        }

        private Mediators()
        {

        }

        private static Dictionary<string, Action<object>> subs = new Dictionary<string, Action<object>>();

        public void Register(string message, Action<object> action)
        {
            subs.Add(message, action);
        }

        public void Notify(string message, Object param)
        {
            foreach (var item in subs)
            {
                if (item.Key.Equals(message))
                {
                    Action<object> method = (Action<object>)item.Value;
                    method.Invoke(param);
                }
            }
        }
    }
}
