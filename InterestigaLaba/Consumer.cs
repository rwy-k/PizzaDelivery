using System;

namespace InterestigaLaba
{
    class Client : User
    {
        string place;
        public Client()
        {
            place = "Polytechnical Institute";
        }
        public string getPlace()
        {
            return place;
        }
        public Client setPlace(string place)
        {
            this.place = place;
            return this;
        }

    }
}
