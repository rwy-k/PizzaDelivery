using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestigaLaba
{
    class Path
    {
        public Place point1;
        public Place point2;
        public int time;
        public Path() { }
        public Path(Place point1, Place point2, int time)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.time = time;
        }
        public int getTime(int m1, int m2)
        {
            if (point1.mark == m1 && point2.mark == m2) return time;
            else return 1000000;
        }
        public bool getPath(int m1, int m2)
        {
            if (point1.mark == m1 || point2.mark == m2) return true;
            else return false;
        }
        public Place getPoint(int i)
        {
            if (i == 1) return point1;
            else
            if (i == 2) return point2;
            else return null;
        }
        public void Print()
        {
            point1.Print();
            Console.Write(" - ");
            point2.Print();
            Console.Write(" - ");
            Console.WriteLine(time);
        }

        public bool ComparePath(Path path1)
        {
            if (this.time < path1.time) return true;
            else return false;
        }
        
    }
    class Place
    {
        public string name;
        public int mark;
        public Place(string name, int mark)
        {
            this.name = name;
            this.mark = mark;
        }
        public void Print()
        {
            Console.Write(name);
        }
    }
}
