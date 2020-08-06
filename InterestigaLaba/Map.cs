using System;
using System.Collections.Generic;
using System.IO;

public class Pair
{
    public int First, Second;
    public Pair(int a, int b)
    {
        First = a;
        Second = b;
    }
};

namespace InterestigaLaba
{
    class Map
    {
        List<Path> paths = new List<Path>();
        List<Place> places = new List<Place>();
        List<int> number_of_place = new List<int>();
        static List<List<Pair>> mtx = new List<List<Pair>>();
        int shortTime = 0;

        public Map() //creating a map
        {
            var fmr = new StreamReader("Map.txt");
            string ifm;
            List<string> used = new List<String>();
            int number = 0;
            while ((ifm = fmr.ReadLine()) != null)
            {
                
                string[] a = ifm.Split('-');
                int time = Convert.ToInt32(a[2]);
                Place p1 = null, p2 = null;
                if (!used.Contains(a[0]))
                {
                    p1 = new Place(a[0], number++);
                    used.Add(a[0]);
                    places.Add(p1);
          
                } else
                {
                    for (int i = 0; i < places.Count; i++)
                    {
                        if (places[i].name == a[0])
                        {
                            p1 = new Place(a[0], places[i].mark);
                        }
                    }
                }
                if (!used.Contains(a[1]))
                {
                    p2 = new Place(a[1], number++);
                    used.Add(a[1]);
                    places.Add(p2);
                }
                else
                {
                    for (int i = 0; i < places.Count; i++)
                    {
                        if (places[i].name == a[1])
                        {
                            p2 = new Place(a[1], places[i].mark);
                        }
                    }
                }
                addPath(p1, p2, time); 
            }
            fmr.Close();

            for (int i = 0; i < used.Count; i++)
            {
                mtx.Add(new List<Pair>());
            }
            for (int i = 0; i < paths.Count; i++)
            {
                int v1 = paths[i].point1.mark;
                int v2 = paths[i].point2.mark;
                int cost = paths[i].time;
                mtx[v1].Add(new Pair(v2, cost));
                mtx[v2].Add(new Pair(v1, cost));
            }        
        }

        public Map addPath(Place place1, Place place2, int time)
        {
            Path newPath = new Path(place1, place2, time);
            paths.Add(newPath);
            return this;
        }

        public List<Place> getAllPlaces
        {
            get
            {
                return places;
            } 
        }

        public int ShortTime
        {
            get {
                return shortTime;
            }
        }

        public void Print()
        {
            foreach(Path p in paths){
                p.Print();
            }
        }

        public List<string> generateShortWay(string start, string end) 
        {
            List<string> pth = new List<string>();
            int m1 = 0, m2 = 0;
            foreach(Place p in places)
            {
                if (p.name == start) m1 = p.mark;
                if (p.name == end) m2 = p.mark;
            }
            List<int> path = Algorithm(m1,m2);
            if (path == null)
            {
                Console.WriteLine("Route not found");
                return null;
            }
            foreach(Place p in places)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    if (p.mark == path[i]) pth.Add(p.name);
                }
            }
            int c = 0;
            foreach (Path p in paths)
            {
                if ((p.point1.name == start && p.point2.name == end) || (p.point2.name == start && p.point1.name == end))
                {
                    c++;
                }
            }
            if (c == 0)
            {
                var fmw = new StreamWriter("Map.txt", true);
                fmw.WriteLine(start + "-" + end + "-" + shortTime);
                fmw.Close();
            }
            
            pth.Reverse();
            return pth;
        }

        public List<int> Algorithm(int start, int end) 
        {
            int nPlace = places.Count;
            /*
            for (int i = 0; i < nPlace; i++)
            {
                Console.WriteLine("{0} {1}\n", places[i].name, places[i].mark);
            }
            */
            const int INF = (int)1e9;
            List<int> distance = new List<int>();
            List<bool> used = new List<bool>();
            List<int> p = new List<int>();
            for (int i = 0; i < nPlace; i++)
            {
                distance.Add(INF);
                used.Add(false);
                p.Add(-1);
            }
            distance[start] = 0;
            for (int i = 0; i < nPlace; i++)
            {
                int v = -1;
                for (int j = 0; j < nPlace; j++)
                {
                    if (!used[j] && (v == -1 || distance[j] < distance[v])) {
                        v = j;
                    }
                }
                if (v == -1) break;
                used[v] = true;
                for (int j = 0; j < mtx[v].Count; j++)
                {
                    int to = mtx[v][j].First;
                    int cost = mtx[v][j].Second;
                    if (distance[v] + cost < distance[to])
                    {
                        distance[to] = distance[v] + cost;
                        p[to] = v;
                    }
                }
            }
            if (distance[end] == INF)
            {
                return null;
            }  else
            {
                shortTime = distance[end];
                List<int> path = new List<int>();
                int cur_v = end;
                while (cur_v != -1)
                {
                    path.Add(cur_v);
                    cur_v = p[cur_v];
                }
                /*
                path.Reverse();
                for (int i = 0; i < path.Count; i++)
                {
                    Console.Write(path[i] + " ");
                }
                */
                return path;
            }
        }
    }
}
