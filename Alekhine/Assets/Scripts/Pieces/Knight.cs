using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        List<Vector2Int> diagonals = new List<Vector2Int>(BishopDirections);
        List<Vector2Int> straightLines = new List<Vector2Int>(RookDirections);

        for (int i = 0; i < diagonals.Count; i++) {
            for (int j = i; j < straightLines.Count; j++)
            {
                Vector2Int direction = diagonals[i] + straightLines[j];
                Debug.Log("This direction: " + direction);
                locations.Add(direction);
            }
        }

        return locations;
    }
}
