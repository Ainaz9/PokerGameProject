using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameProject
{
    public class AuthContainer
    {
        public AuthContainer() { }
        public bool IsAuthenticated { get; private set; }
        public event Action Authenticated;
        public string Token { get; private set; }
        public void SetAuthenticated(string token)
        {
            Token = token;
            IsAuthenticated = true;
            Authenticated?.Invoke();
        }



    }
}