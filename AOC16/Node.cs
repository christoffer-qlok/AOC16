using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC16
{
    internal class Node
    {
        public Coord Loc { get; set; }
        public Coord Dir { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Node node &&
                   Loc == node.Loc &&
                   Dir == node.Dir;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Loc, Dir);
        }
    }
}
