using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer.Dto
{
    public class UserDetails
    {
        public string username;
        public string password;
        public string tckno;
        public string adress;

        public UserDetails()
        {
            
        }
        public UserDetails(string _username, string _password, string _tckno, string _adress)
        {
            username = _username;
            password = _password;
            tckno = _tckno;
            adress = _adress;
        }
    }
}
