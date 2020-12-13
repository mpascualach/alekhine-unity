using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();

        foreach (Vector2Int dir in BishopDirections)
        {
            for (int i = 1; i < 8; i++)
            {
                Vector2Int nextGridPoint = new Vector2Int(position.x + i * dir.x, position.y + i * dir.y);
                locations.Add(nextGridPoint);
                if (GameHandler.instance.PieceAtGrid(nextGridPoint))
                {
                    Piece otherPiece = GameHandler.instance.PieceAtGrid(nextGridPoint).GetComponent<Piece>();
                    if (otherPiece.type == PieceType.King && otherPiece.player.name != player.name)
                    {
                        otherPiece.player.inCheck = true;
                    }
                    break;
                }
            }
        }

        foreach (Vector2Int dir in RookDirections)
        {
            for (int i = 1; i < 8; i++)
            {
                Vector2Int nextGridPoint = new Vector2Int(position.x + i * dir.x, position.y + i * dir.y);
                locations.Add(nextGridPoint);
                if (GameHandler.instance.PieceAtGrid(nextGridPoint))
                {
                    Piece otherPiece = GameHandler.instance.PieceAtGrid(nextGridPoint).GetComponent<Piece>();
                    if (otherPiece.type == PieceType.King && otherPiece.player.isWhite != player.isWhite)
                    {
                        otherPiece.player.inCheck = true;
                    }
                    break;
                }
            }
        }

        return locations;
    }
}
