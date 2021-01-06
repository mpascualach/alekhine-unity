using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveLogger : MonoBehaviour
{
    public static MoveLogger instance;
    public Text Logs;

    private int increment = 1;

    void Awake()
    {
        instance = this;
    }

    public void LogMove(GameObject pieceObject, Vector2Int destination)
    {
        string move = (char)(97 + destination[0]) + (destination[1] + 1).ToString();
        Piece piece = pieceObject.GetComponent<Piece>();

        string ending = piece.player.isWhite ? "   " : "\n";

        if (!piece.moved)
        {
            Debug.Log(piece.position);
            GameHandler.instance.Move(pieceObject, destination);
        }
        Logs.text += $"{(piece.player.isWhite ? increment + "." : "")} {move} {ending}";
        if (!piece.player.isWhite) {
            increment++;
        }
    }
}
