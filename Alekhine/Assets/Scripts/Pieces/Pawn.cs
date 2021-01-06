using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        int forwardDirection = GameHandler.instance.currentPlayer.forward;

        Vector2Int forward = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection);

        if (GameHandler.instance.SquareIsInBounds(forward) && !GameHandler.instance.PieceAtGrid(forward)) {
            locations.Add(forward);
        }

        Vector2Int doubleForward = new Vector2Int(gridPoint.x, gridPoint.y + (forwardDirection * 2)); ;

        if (!this.developed && !GameHandler.instance.PieceAtGrid(doubleForward)) {
            locations.Add(doubleForward);
        }

        Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection);

        if (GameHandler.instance.PieceAtGrid(forwardRight) && !GameHandler.instance.DoesPieceBelongToCurrentPlayer(GameHandler.instance.PieceAtGrid(forwardRight))) {
            locations.Add(forwardRight);
        }

        Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection);

        if (GameHandler.instance.PieceAtGrid(forwardLeft) && !GameHandler.instance.DoesPieceBelongToCurrentPlayer(GameHandler.instance.PieceAtGrid(forwardLeft))) {
            locations.Add(forwardLeft);
        }

        return locations;
    }
}
