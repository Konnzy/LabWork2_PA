namespace LabWork2;

public class Backtracking
{
    private readonly int[,] _adjacencyMatrix;
    public List<int> AllVertices = new();
    public List<Edge> Edges = new();
    public const int ColorsCount = 4;
    public Dictionary<int, int> VertexColor = new();
    public int Iterations { get; private set; }
    public int TotalNodes => VertexColor.Count;
    public int MaxNodesInMemory { get; private set; }
    public int DeadEnds { get; private set; }
    public int StartVertex { get; set; } = 1;
    public Backtracking(int[,] adjacencyMatrix)
    {
        _adjacencyMatrix = adjacencyMatrix;
        var n = adjacencyMatrix.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            AllVertices.Add(i + 1);
            for (var j = i + 1; j < n; j++)
            {
                if (adjacencyMatrix[i, j] == 1)
                {
                    Edges.Add(new Edge { VertexA = i + 1, VertexB = j + 1 });
                }
            }
        }
    }

    // Check if the color is valid for vertex
    private bool IsColorValid(int vertexId, int color)
    {
        foreach (var edge in Edges)
        {
            if (edge.VertexA == vertexId && VertexColor.TryGetValue(edge.VertexB, out int cB) && cB == color)
                return false;
            if (edge.VertexB == vertexId && VertexColor.TryGetValue(edge.VertexA, out int cA) && cA == color)
                return false;
        }
        return true;
    }
    public bool Backtrack()
    {
        Iterations++;
        
        var coloredVertices = new HashSet<int>(VertexColor.Keys);

        var heuristic = new DegreeHeuristic();
        // var heuristic = new MRVHeuristic();

        int vertexId;
        
        if (coloredVertices.Count == 0)
        {
            vertexId = StartVertex;
        }
        else
        {
            vertexId = heuristic.DGR(AllVertices, coloredVertices, Edges);
            // vertexId = heuristic.MRV(AllVertices, coloredVertices, Edges, vertexId);
        }

        if (vertexId == -1)
            return true;

        for (int color = 1; color <= ColorsCount; color++)
        {
            if (!IsColorValid(vertexId, color))
                continue;

            VertexColor[vertexId] = color;
            MaxNodesInMemory = Math.Max(MaxNodesInMemory, VertexColor.Count);

            // Here we can check if the solution is found
            if (Backtrack())
            {
                return true;
            }

            VertexColor.Remove(vertexId);
        }
        
        DeadEnds++;
        return false;
    }

    public Dictionary<int, int> Solve(int startVertex)
    {
        StartVertex = startVertex;
        VertexColor = new Dictionary<int, int>();
        Iterations = 0;
        MaxNodesInMemory = 0;
        DeadEnds = 0;
        
        Backtrack();
        return VertexColor;
    }

}