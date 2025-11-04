namespace LabWork2;

public class DegreeHeuristic
{
    public int DGR(List<int> allVertices, HashSet<int> coloredVertices, List<Edge> edges)
    {
        var maxDegree = -1;
        var chosenVertex = -1;

        foreach (var currentVertexId in allVertices)
        {
            // Check if vertex is not colored yet
            if (!coloredVertices.Contains(currentVertexId))
            {
                var currentDegree = 0;

                // Calculate the highest degree of vertex
                foreach (var edge in edges)
                {
                    var neighborVertexId = -1;

                    if (edge.VertexA == currentVertexId)
                    {
                        neighborVertexId = edge.VertexB;
                    }
                    else if (edge.VertexB == currentVertexId)
                    {
                        neighborVertexId = edge.VertexA;
                    }

                    if (neighborVertexId != -1 && !coloredVertices.Contains(neighborVertexId))
                    {
                        currentDegree++;
                    }
                }

                // Check if the current vertex has the highest degree
                if (currentDegree > maxDegree)
                {
                    maxDegree = currentDegree;
                    chosenVertex = currentVertexId;
                }
            }
        }

        return chosenVertex;
    }
}