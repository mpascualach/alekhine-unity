using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> knightMoves = new List<Vector2Int>();

        foreach(var move in KnightDirections) {
            Vector2Int knightMove = new Vector2Int(gridPoint.x + move.x, gridPoint.y + move.y);
            knightMoves.Add(knightMove);
        }

        return knightMoves;
    }
}
