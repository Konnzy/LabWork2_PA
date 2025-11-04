namespace LabWork2;

public class HillClimbing
{
    private readonly int[,] _adjacencyMatrix;
    private readonly Random _rand = new();
    public List<int> AllVertices = new();
    public List<Edge> Edges = new();
    public Dictionary<int, int> VertexColor = new();
    public const int ColorsCount = 4;
    public int Iterations { get; private set; }
    public int TotalNodes => Iterations;
    public int MaxNodesInMemory { get; private set; }
    public int DeadEnds { get; private set; }
    public int StartVertex { get; set; } = 1;

    public HillClimbing(int[,] adjacencyMatrix)
    {
        _adjacencyMatrix = adjacencyMatrix;
        int n = adjacencyMatrix.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            AllVertices.Add(i + 1);
            for (int j = i + 1; j < n; j++)
            {
                if (adjacencyMatrix[i, j] == 1)
                {
                    Edges.Add(new Edge { VertexA = i + 1, VertexB = j + 1 });
                }
            }
        }
    }

    // For calculating num of conflicts
    private int Conflicts()
    {
        int c = 0;
        foreach (var e in Edges)
        {
            if (VertexColor.TryGetValue(e.VertexA, out var ca) && VertexColor.TryGetValue(e.VertexB, out var cb) &&
                ca == cb)
                c++;
        }

        return c;
    }

    // For calculating num of conflicts at vertex
    private int ConflictsAtVertex(int v)
    {
        int cnt = 0;
        if (!VertexColor.TryGetValue(v, out int cv))
            return 0;

        foreach (var e in Edges)
        {
            int u = (e.VertexA == v) ? e.VertexB : (e.VertexB == v) ? e.VertexA : -1;
            if (u == -1)
                continue;

            if (VertexColor.TryGetValue(u, out int cu) && cu == cv)
                cnt++;
        }

        return cnt;
    }

    // For calculating num of conflicts at vertex if the color is changed
    private int ConflictsAtVertexIf(int v, int color)
    {
        int cnt = 0;
        foreach (var e in Edges)
        {
            int u = (e.VertexA == v) ? e.VertexB : (e.VertexB == v) ? e.VertexA : -1;
            if (u == -1)
                continue;

            // if the neighbor is colored with the same color, the vertex cannot be colored with this color
            if (VertexColor.TryGetValue(u, out int cu) && cu == color)
                cnt++;
        }

        return cnt;
    }

    // For initializing random state
    private void InitRandomState(int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : _rand;

        VertexColor.Clear();
        foreach (var v in AllVertices)
            VertexColor[v] = 1 + rng.Next(ColorsCount);
    }

    // For selecting the best move
    private bool SelectBestMove(out int bestVertex, out int bestColor, out int newConflicts, bool allowSideways,
        int currentConflicts)
    {
        bestVertex = -1;
        bestColor = -1;
        newConflicts = -1;
        int bestConflicts = int.MaxValue;

        foreach (var v in AllVertices)
        {
            int currColor = VertexColor[v];
            int baseDelta = ConflictsAtVertex(v);

            for (var color = 1; color <= ColorsCount; color++)
            {
                if (color == currColor)
                    continue;
                int delta = ConflictsAtVertexIf(v, color);
                int conflicts = currentConflicts - baseDelta + delta;

                bool acceptable = allowSideways ? (conflicts <= currentConflicts) : (conflicts < currentConflicts);
                if (!acceptable)
                    continue;

                // If the move is better, update best move
                if (conflicts < bestConflicts || (conflicts == bestConflicts && RandomTie()))
                {
                    bestConflicts = conflicts;
                    bestVertex = v;
                    bestColor = color;
                    newConflicts = conflicts;
                }
            }
        }


        return bestVertex != -1;
    }

    private void ApplyMove(int v, int color)
    {
        VertexColor[v] = color;
        Iterations++;
        MaxNodesInMemory = Math.Max(MaxNodesInMemory, VertexColor.Count);
    }

    // For hill climbing
    private bool HillClimb(int maxRestarts = 100, int maxSideways = 100, int maxSteps = 200000)
    {
        Iterations = 0;
        DeadEnds = 0;
        MaxNodesInMemory = AllVertices.Count;
        for (int restart = 0; restart <= maxRestarts; restart++)
        {
            InitRandomState();
            int conflicts = Conflicts();
            if (conflicts == 0)
                return true;

            int sideways = maxSideways;
            for (int step = 0; step < maxSteps; step++)
            {
                // If there are no more moves to make, return false
                if (!SelectBestMove(out int v, out int color, out int conflictsAtVertex, sideways > 0, currentConflicts: conflicts))
                {
                    DeadEnds++;
                    break;
                }

                // If the move is better, apply it
                if (conflictsAtVertex < conflicts)
                {
                    ApplyMove(v, color);
                    conflicts = conflictsAtVertex;
                    if (conflicts == 0)
                        return true;
                    continue;
                }

                // If the move is worse, try to apply it sideways
                if (conflictsAtVertex == conflicts && sideways > 0)
                {
                    ApplyMove(v, color);
                    conflicts = conflictsAtVertex;
                    sideways--;
                    continue;
                }

                DeadEnds++;
                break;
            }
        }

        return false;
    }

    public Dictionary<int, int> Solve()
    {
        HillClimb();
        return VertexColor;
    }

    private bool RandomTie() => _rand.Next(2) == 0;
}