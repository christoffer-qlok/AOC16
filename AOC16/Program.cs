namespace AOC16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var map = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                map[i] = lines[i].ToCharArray();
            }

            int part1 = DFS(map, new Node()
            {
                Loc = new Coord() { Y = 0, X = 0 },
                Dir = new Coord() { Y = 0, X = 1 }
            });

            Console.WriteLine($"Part 1 tiles: {part1}");


            // Part 2
            int max = 0;
            for (int y = 0; y < map.Length; y++)
            {
                int cur = DFS(map, new Node()
                {
                    Loc = new Coord() { Y = y, X = 0 },
                    Dir = new Coord() { Y = 0, X = 1 }
                });
                max = Math.Max(max, cur);
                cur = DFS(map, new Node()
                {
                    Loc = new Coord() { Y = y, X = map[0].Length - 1 },
                    Dir = new Coord() { Y = 0, X = -1 }
                });
                max = Math.Max(max, cur);
            }

            for (int x = 0; x < map[0].Length; x++)
            {
                int cur = DFS(map, new Node()
                {
                    Loc = new Coord() { Y = 0, X = x },
                    Dir = new Coord() { Y = 1, X = 0 }
                });
                max = Math.Max(max, cur);
                cur = DFS(map, new Node()
                {
                    Loc = new Coord() { Y = map.Length - 1, X = x },
                    Dir = new Coord() { Y = -1, X = 0 }
                });
                max = Math.Max(max, cur);
            }

            Console.WriteLine($"Part 2 tiles: {max}");
        }

        static int DFS(char[][] map, Node start)
        {
            var fringe = new Stack<Node>();
            var visited = new HashSet<Node>();
            var locs = new HashSet<Coord>();
            fringe.Push(start);
            while (fringe.Count > 0)
            {
                var cur = fringe.Pop();
                if (visited.Contains(cur))
                    continue;

                visited.Add(cur);
                locs.Add(cur.Loc);
                var neighbours = GetNeighbours(map, cur);
                foreach (var n in neighbours)
                {
                    if (visited.Contains(n))
                        continue;

                    fringe.Push(n);
                }
            }
            return locs.Count();
        }

        static Node[] GetNeighbours(char[][] map, Node n)
        {
            List<Node> potential = new List<Node>();
            Coord newDir;
            switch (map[n.Loc.Y][n.Loc.X])
            {
                case '.':
                    potential.Add(new Node() { Loc = n.Loc + n.Dir, Dir = n.Dir });
                    break;
                case '/':
                    newDir = new Coord() { Y = -1 * n.Dir.X, X = -1 * n.Dir.Y };
                    potential.Add(new Node() { Dir = newDir, Loc = n.Loc + newDir });
                    break;
                case '\\':
                    newDir = new Coord() { Y = n.Dir.X, X = n.Dir.Y };
                    potential.Add(new Node() { Dir = newDir, Loc = n.Loc + newDir });
                    break;
                case '|':
                    if (n.Dir.X == 0)
                    {
                        potential.Add(new Node() { Loc = n.Loc + n.Dir, Dir = n.Dir });
                    }
                    else
                    {
                        potential.Add(new Node()
                        {
                            Loc = new Coord()
                            {
                                Y = n.Loc.Y + 1,
                                X = n.Loc.X
                            },
                            Dir = new Coord()
                            {
                                Y = 1,
                                X = 0
                            }
                        });
                        potential.Add(new Node()
                        {
                            Loc = new Coord()
                            {
                                Y = n.Loc.Y - 1,
                                X = n.Loc.X
                            },
                            Dir = new Coord()
                            {
                                Y = -1,
                                X = 0
                            }
                        });
                    }
                    break;
                case '-':
                    if (n.Dir.Y == 0)
                    {
                        potential.Add(new Node() { Loc = n.Loc + n.Dir, Dir = n.Dir });
                    }
                    else
                    {
                        potential.Add(new Node()
                        {
                            Loc = new Coord()
                            {
                                Y = n.Loc.Y,
                                X = n.Loc.X + 1
                            },
                            Dir = new Coord()
                            {
                                Y = 0,
                                X = 1
                            }
                        });
                        potential.Add(new Node()
                        {
                            Loc = new Coord()
                            {
                                Y = n.Loc.Y,
                                X = n.Loc.X - 1
                            },
                            Dir = new Coord()
                            {
                                Y = 0,
                                X = -1
                            }
                        });
                    }
                    break;
                default:
                    throw new Exception($"Bad map char {map[n.Loc.Y][n.Loc.X]}");
            }

            return potential
                .Where(node => node.Loc.X >= 0 && node.Loc.X < map[0].Length && node.Loc.Y >= 0 && node.Loc.Y < map.Length)
                .ToArray();
        }

    }
}
