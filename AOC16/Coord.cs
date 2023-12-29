using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC16
{
    internal struct Coord
    {
        public int X;
        public int Y;

        public override bool Equals(object? obj)
        {
            return obj is Coord coord &&
                   X == coord.X &&
                   Y == coord.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Coord a, Coord b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Coord a, Coord b)
        {
            return !a.Equals(b);
        }

        public static Coord operator+(Coord c1, Coord c2)
        {
            return new Coord() { X = c1.X + c2.X, Y = c1.Y + c2.Y };
        }
    }
}
