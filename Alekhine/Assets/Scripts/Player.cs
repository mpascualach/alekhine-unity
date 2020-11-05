using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public List<GameObject> pieces;
    public List<GameObject> capturedPieces;

    public string name;
    public int forward;

    public bool inCheck;

    public Player(string name, bool isWhite)
    {
        this.name = name;
        pieces = new List<GameObject>();
        capturedPieces = new List<GameObject>();

        this.forward = isWhite ? 1 : -1;
    }
}
