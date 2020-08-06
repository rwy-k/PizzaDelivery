using System;
using System.Collections.Generic;
using System.IO;

namespace InterestigaLaba
{
    class PizzaSystem
    {
        static List<User> users = new List<User>();
        static Map map = new Map();
        static User currentUser = new User();
        static string currentLocation = "Polytechnical Institute";
        public PizzaSystem() {}
        public static void Main(string[] args)

        {
            PizzaSystem pizza = new PizzaSystem();
            string log;
            string pass;
            string place = null;
            Client client = new Client();
            StreamReader fpr = new StreamReader("Pizza.txt");
            string ifp;
            while ((ifp = fpr.ReadLine()) != null)
            {
                string[] a = ifp.Split('-');
                pizza.addUser(a[0], a[1], a[2]);
            }
            fpr.Close();
            string s;
            Console.WriteLine("Do you have an account?");
            while (true)
            {
                s = Console.ReadLine();
                if (!s.Contains("no") && !s.Contains("No") && !s.Contains("NO") && !s.Contains("yes") && !s.Contains("Yes") && !s.Contains("YES"))
                {
                    Console.WriteLine("Ooops...Try again");
                }
                else break;

            }
            Console.WriteLine("Enter your login");
            log = Console.ReadLine();
            Console.WriteLine("Enter your password");
            pass = Console.ReadLine();
            if (s.Contains("no") || s.Contains("No") || s.Contains("NO"))
            {
                Console.WriteLine("Enter your name");
                string name = Console.ReadLine();
                pizza.addUser(name, log, pass);
                currentUser = pizza.findUser(log, pass);
                var fpw = new StreamWriter("Pizza.txt", true);
                fpw.WriteLine(name + "-" + log + "-" + pass);
                fpw.Close();
                Console.WriteLine("Enter your location");
                place = Console.ReadLine();
                client.setPlace(place);
            };
            if (s.Contains("yes") || s.Contains("Yes") || s.Contains("YES"))
            {
                while (pizza.findUser(log, pass) == null)
                {
                    Console.WriteLine("Incorrect login or password. Try again.");
                    Console.WriteLine("Enter your login");
                    log = Console.ReadLine();
                    Console.WriteLine("Enter your password");
                    pass = Console.ReadLine();
                }
                currentUser = pizza.findUser(log, pass);
                Console.WriteLine("Enter your location");
                place = Console.ReadLine();
                client.setPlace(place);
            };

            Console.WriteLine("Short route :");
            int c = 0;
            foreach (Place p in map.getAllPlaces)
            {
                if (p.name == place)
                {
                    foreach (string pl in map.generateShortWay(currentLocation, client.getPlace()))
                    {
                       if (pl != client.getPlace()) Console.Write(pl + " -> ");
                       else Console.WriteLine(pl);
                    }
                }
                else c++;
            }
            if (c == map.getAllPlaces.Count)
            {
                Console.WriteLine("Sorry, we do not deliver pizza to your location");
            }
            else
            {
                Console.WriteLine("Time - " + map.ShortTime);
                currentLocation = client.getPlace();
            }
            Console.WriteLine();
            Console.WriteLine("IF YOU WANT TO MAKE AN ORDER PRESS 'BackSpace' ");
            var o = Console.ReadKey();
            if (o.Key == ConsoleKey.Backspace)
            {
                Console.Clear();
                map = new Map();
                Main(args);
            }
            else Environment.Exit(0);
        }

        PizzaSystem addUser(string name, string login, string password)
        {
            User newUser = new User(name, login, password);
            users.Add(newUser);
            return this;
        }

        User findUser(string login, string password)
        {
            foreach(User u in users)
            {
               if (u.enter(login, password)) return u;
            }
            return null;
        }
       // PizzaSystem save() { return null; }
       // PizzaSystem load() { return null; }
    }
}

