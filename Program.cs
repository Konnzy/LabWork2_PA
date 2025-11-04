namespace LabWork2
{
    public class Edge
    {
        public int VertexA { get; set; }
        public int VertexB { get; set; }
    }

    class Program
    {
        public static List<string> Regions = new List<string>
        {
            "Vinnytska", "Volynska", "Dnipropetrovska", "Donetska", "Zhytomyrska", "Zakarpatska", "Zaporizka",
            "Ivano-Frankivska", "Kyivska",
            "Kirovohradska", "Luhanska", "Lvivska", "Mykolaivska", "Odeska", "Poltavska", "Rivnenska", "Sumska",
            "Ternopilska", "Kharkivska",
            "Khersonska", "Khmelnytska", "Cherkaska", "Chernivetska", "Chernihivska", "Avtonomna Respublika Krym",
            "Kyiv", "Sevastopol"
        };

        public static int[,] Adjacencymatrix = new int[27, 27]
        {
            { 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0 },
            { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0 },
            { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 }
        };

        static void Main(string[] args)
        {
            var solver = new Backtracking(Adjacencymatrix);
            var result = solver.Solve(1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Backtracking:");
            Console.ResetColor();
            for (int i = 1; i <= Regions.Count; i++)
            {
                int color = result[i];
            
                Console.ForegroundColor = color switch
                {
                    1 => ConsoleColor.Red,
                    2 => ConsoleColor.Green,
                    3 => ConsoleColor.Blue,
                    4 => ConsoleColor.Yellow,
                    _ => ConsoleColor.Gray
                };
            
                Console.Write($"{Regions[i - 1]} - {color}");
                Console.ResetColor();
            
                if (i < Regions.Count)
                    Console.Write(", ");
            }
            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nIterations: {solver.Iterations}");
            Console.WriteLine($"Dead ends: {solver.DeadEnds}");
            Console.WriteLine($"Nodes: {solver.TotalNodes}");
            Console.WriteLine($"Max nodes in memory: {solver.MaxNodesInMemory}");
            Console.ResetColor();
            
            Console.WriteLine();
            var HillSolver = new HillClimbing(Adjacencymatrix);
            var HillResult = HillSolver.Solve();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Hill Climbing:");
            Console.ResetColor();
            for (int i = 1; i <= Regions.Count; i++)
            {
                int color = HillResult[i];

                Console.ForegroundColor = color switch
                {
                    1 => ConsoleColor.Red,
                    2 => ConsoleColor.Green,
                    3 => ConsoleColor.Blue,
                    4 => ConsoleColor.Yellow,
                    _ => ConsoleColor.Gray
                };

                Console.Write($"{Regions[i - 1]} - {color}");
                Console.ResetColor();

                if (i < Regions.Count)
                    Console.Write(", ");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nIterations: {HillSolver.Iterations}");
            Console.WriteLine($"Dead ends: {HillSolver.DeadEnds}");
            Console.WriteLine($"Nodes: {HillSolver.TotalNodes}");
            Console.WriteLine($"Max nodes in memory: {HillSolver.MaxNodesInMemory}");
            Console.ResetColor();
        }
    }
}