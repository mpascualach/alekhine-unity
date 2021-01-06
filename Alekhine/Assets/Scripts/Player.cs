using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public List<GameObject> pieces;
    public List<GameObject> capturedPieces;

    public List<Move> allMoves;

    public GameObject king;

    public string name;

    public bool isOpponent;
    public bool isWhite;

    public int forward;

    public Player(string name, bool isWhite, bool isOpponent)
    {
        this.isWhite = isWhite;
        this.name = name;
        this.isOpponent = isOpponent;

        pieces = new List<GameObject>();
        capturedPieces = new List<GameObject>();
        allMoves = new List<Move>();

        forward = isWhite ? 1 : -1;
    }

    public void ConsiderMove()
    {
        System.Random rnd = new System.Random();
        int moveIndex = rnd.Next(0, allMoves.Count);

        Move selectedMove = allMoves[moveIndex];

        Piece selectedPiece = selectedMove.piece.GetComponent<Piece>();

        GameHandler.instance.Move(selectedMove.piece, selectedMove.endingPosition);

    }
}
