// ENZO FIGUEIREDO FEITOSA - 08/05/2025 (DD/MM/YY) - 
namespace mazes_project_code;
using System;
using System.Collections.Generic;

// Mr. Valdis! Just like before, I split the code into two separate files-- To keep things more... Organised.
// And, obviously, since it would be far, far better for you to read... I hope.
// Also, feel free to follow me on Github! 
// https://github.com/GinkuiMain

public class MazeSolver
{
    public struct Result // I was going to put this in another file- But it's too small, unlike my node.
    {
        public int Steps, Coins;
    }

    private readonly char[,] maze; // This will be our 2D arrangement of the maze
    private readonly int rows, cols; // Auto-explanatory.

    // Now, the directions. This one was.. Kinda (by kinda, I mean a lot) tricky to write- Used a few (many, actually) pieces of paper to even think of it
    private static readonly int[] dR = {-1, -1, -1, 0, 0, 1, 1, 1}; // Direction rows
    private static readonly int[] dC = {-1, 0, 1, -1, 1, -1, 0, 1}; // Direction columns
    //  N, NE, E, SE, S, SW, W, NW

    struct Node : IComparable<Node>
    {
        public int Row, Col, Steps, Coins;
        public Node(int r, int c, int s, int coins) // Defying our node and its characteristics (number of row, column, how many steps so far, coin inside...)
        {
            Row = r; Col = c; Steps = s; Coins = coins;
        }

        // Compare by steps first, then coins
        public int CompareTo(Node o)
        {
            // 1) Compare by steps
            int c = Steps.CompareTo(o.Steps);
            if (c != 0) return c;

            // 2) Then by coins
            c = Coins.CompareTo(o.Coins);
            if (c != 0) return c;

            // 3) Tie‑break on row
            c = Row.CompareTo(o.Row);
            if (c != 0) return c;

            // 4) Finally on column
            return Col.CompareTo(o.Col);
        } // THIS SINGLE PART WAS HELL ON EARTH. (compare to) -- TOOK ME ALMOST 1 HOUR 😭
    }


    public MazeSolver(char[,] maze) // Our constructor
    {
        this.maze = maze;
        rows = maze.GetLength(0); // Self-explanatory.
        cols = maze.GetLength(1); // As well.
    }

    public Result Solve((int r, int c) start, (int r, int c) goal)
    {
        var best_path = new Tuple<int, int>[rows, cols]; // Create a 2D array to store the best (fewest steps, fewest coins) we've seen for each cell
        for (int i = 0; i < rows; i++)        // Initialize all cells with "infinite" values: max steps and max coins
        {                                   // So any real path will be considered better when first encountered
            for (int j = 0; j < cols; j++)
            {
                best_path[i, j] = Tuple.Create(int.MaxValue, int.MaxValue);
            }
        }

        // Priority queue (pq), sorted by steps and coints
        var pq = new SortedSet<Node>();

        best_path[start.r, start.c] = Tuple.Create(0, 0);
        pq.Add(new Node(start.r, start.c, 0, 0));

        
        
        while (pq.Count > 0)
        {
            var cur = pq.Min;
            pq.Remove(cur);

            if (cur.Row == goal.r && cur.Col == goal.c)
                return new Result { Steps = cur.Steps, Coins = cur.Coins}; // Ayyy we found it!

            var rec = best_path[cur.Row, cur.Col]; // Get the best path for the current cell
            if (cur.Steps > rec.Item1 || cur.Coins > rec.Item2) continue; // Keep going (skips worse path)

            // Try all 8 directions
            for (int d = 0; d < 8; d++) // Short warning- I had to use GPT on the next section (just for this loop part) because when I tried to use the previous version I had made, I kept getting errors whenever I tried to use the 31x31 maze.
            {                           // Although, I made SURE to read everything, step by step, and comment on my own (to prove that I understood it.)
                int nr = cur.Row + dR[d], nc = cur.Col + dC[d]; // Next row and colum 

                // Bound checker
                if (nr < 0 || nr >= rows || nc < 0 || nc >= cols) continue;
                char ch = maze[nr, nc];

                // Skip walls
                if (ch == 'X') continue;

                // Compute next step and coin sum
                int ns = cur.Steps + 1; // increment step
                int ncns = cur.Coins + (char.IsDigit(ch) ? ch - '0' : 0); // add coin if digit
                var old = best_path[nr, nc];

                // Update if found better path
                if (ns < old.Item1 || (ns == old.Item1 && ncns < old.Item2))
                {
                    best_path[nr, nc] = Tuple.Create(ns, ncns);
                    pq.Add(new Node(nr, nc, ns, ncns));
                }
            }
        }
        // If we reach here, no path was found
        throw new InvalidOperationException("No path found");

    }
}