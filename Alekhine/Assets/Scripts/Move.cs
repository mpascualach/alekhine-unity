using System;
using UnityEngine;

public class Move
{
    public GameObject piece;
    public Vector2Int startingPoint;
    public Vector2Int endingPosition;

    bool capture;

    public Move(GameObject piece, Vector2Int endingPosition, GameObject pieceToCapture = null)
    {
        this.piece = piece;

        Piece pieceInfo = piece.GetComponent<Piece>();
        startingPoint = pieceInfo.position;

        this.endingPosition = endingPosition;
    }
}