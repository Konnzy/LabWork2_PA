namespace LabWork2;

public class MRVHeuristic
{
    public int MRV(List<int> allVertices, HashSet<int> coloredVertices, List<Edge> edges,
        Dictionary<int, int> vertexColor)
    {
        int best = -1;
        int minRemaining = int.MaxValue;
        // find the vertex with the fewest remaining colors
        foreach (var vertex in allVertices)
        {
            // skip colored vertices
            if (coloredVertices.Contains(vertex))
                continue;

            int allowed = 0;
            // check if vertex can be colored with any color
            for (int color = 1; color <= 4; color++)
            {
                bool valid = true;
                foreach (var e in edges)
                {
                    int u = (e.VertexA == vertex) ? e.VertexB : (e.VertexB == vertex) ? e.VertexA : -1;
                    if (u == -1)
                        continue;
                    // if the neighbor is colored with the same color, the vertex cannot be colored with this color
                    if (vertexColor.TryGetValue(u, out int cu) && cu == color)
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid) allowed++;
            }
            // if the vertex has the fewest remaining colors, it is the best
            if (allowed < minRemaining)
            {
                minRemaining = allowed;
                best = vertex;
            }
            else if (allowed == minRemaining)
            {
                // if the vertex has the same number of remaining colors,
                int degBest = edges.Count(e => e.VertexA == best || e.VertexB == best);
                int degCurr = edges.Count(e => e.VertexA == vertex || e.VertexB == vertex);
                if (degCurr > degBest)
                    best = vertex;
            }
        }

        return best;
    }
}