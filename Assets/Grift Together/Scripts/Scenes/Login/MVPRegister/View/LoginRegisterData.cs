using UnityEngine;

namespace GriftTogether {
    public class LoginRegisterData {
    
        public string Login { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        public LoginRegisterData() { }

        public LoginRegisterData(string login, string userName, string password) { 
            Login = login;
            UserName = userName;
            Password = password;
        }

    }
}
