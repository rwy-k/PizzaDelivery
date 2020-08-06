using System;

namespace InterestigaLaba
{
    class User
    {
        string name;
        string login;
        string password;
        public User() { }
        public User(string name, string login, string password)
        {
            this.name = name;
            this.login = login;
            this.password = password;
        }
        public Boolean enter(string login, string password)
        {
            if (login == this.login && password == this.password) return true;
            else return false;
        }

    }
}
