using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public List<GameObject> pieces;
    public List<GameObject> capturedPieces;

    public GameObject king;

    public string name;
    public bool isWhite;

    public int forward;

    public Material checkMaterial;
    public Material defaultMaterialWhite;
    public Material defaultMaterialBlack;
    public Material defaultMaterial;

    public bool inCheck
    {
        get
        {
            return _inCheck;
        }
        set
        {
            if (_inCheck == value) return;
            _inCheck = value;
            king.GetComponent<King>().inCheck = value;
            // trigger check animation;
        }
    }

    private bool _inCheck = false;

    public Player(string name, bool isWhite)
    {
        this.isWhite = isWhite;
        this.name = name;
        pieces = new List<GameObject>();
        capturedPieces = new List<GameObject>();

        this.forward = isWhite ? 1 : -1;

        defaultMaterial = isWhite ? defaultMaterialWhite : defaultMaterialBlack;
    }
}
