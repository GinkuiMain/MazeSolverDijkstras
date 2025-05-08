// ENZO FIGUEIREDO FEITOSA - 08/05/2025 (DD/MM/YY) - 
namespace mazes_project_code;

class Program
{
    static void Main()
    {
        // I hardcoded the path to the input file for testing purposes. I tried to use a relative path, but it didn't work.
        string inputFile = "D:\\maze_solver_assets\\maze_31x31.txt";
        
        try
        {
            var lines = File.ReadAllLines(inputFile);
            // Added this because the errors I got when doing some testcases were very, very annoying.
            lines = Array.FindAll(lines, line => !string.IsNullOrWhiteSpace(line));
            
            int R = lines.Length, C = lines[0].Length;
            var maze = new char[R, C];
            (int r, int c) start = (-1, -1), goal = (-1, -1);

            // Populate our maze and find 'S' and 'G'
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    maze[i, j] = lines[i][j];
                    if (lines[i][j] == 'S') start = (i, j);
                    if (lines[i][j] == 'G') goal = (i, j);
                }
            }

            if (start.r == -1 || goal.r == -1)
            {
                Console.WriteLine("Error: Start or Goal not found in the maze file.");
                return;
            }

            // Print maze dimensions and positions for debug
            Console.WriteLine($"Maze size: {R}x{C}");
            Console.WriteLine($"Start position: ({start.r}, {start.c})");
            Console.WriteLine($"Goal position: ({goal.r}, {goal.c})");

            // Instantiate solver and solve
            var solver = new MazeSolver(maze);
            try
            {
                var res = solver.Solve(start, goal);
                Console.WriteLine($"Steps: {res.Steps}, Coins: {res.Coins}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: File not found at {inputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}