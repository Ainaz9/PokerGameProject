using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameRSF
{
    public class LocalisationManager
    {
        public string CurrentLocalisation { get; private set; }
        public void SetLocalisation(string locale)
        {
            CurrentLocalisation = locale;
            LocalisationChanged?.Invoke();
        }
        public event Action LocalisationChanged;
    }
}
