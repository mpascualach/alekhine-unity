using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PieceType {King, Queen, Bishop, Knight, Rook, Pawn};

public abstract class Piece : MonoBehaviour, IPointerClickHandler
{
    public PieceType type;

    public Vector2Int position;
    public bool moved;

    private Vector3 screenPoint;
    private Vector3 offset;

    MoveSelector selector;

    protected Vector2Int[] RookDirections = {new Vector2Int(0,1), new Vector2Int(1, 0), 
        new Vector2Int(0, -1), new Vector2Int(-1, 0)};

    protected Vector2Int[] BishopDirections = {new Vector2Int(1,1), new Vector2Int(1, -1), 
        new Vector2Int(-1, -1), new Vector2Int(-1, 1)};

    protected Vector2Int[] KnightDirections = {new Vector2Int(-1,2), new Vector2Int(1, 2),
        new Vector2Int(2, 1), new Vector2Int(2, -1), new Vector2Int(1,-2),
        new Vector2Int(-1,-2), new Vector2Int(-2, -1), new Vector2Int(-2, -1)};

    public abstract List<Vector2Int> MoveLocations(Vector2Int gridPoint);

    public void OnPointerClick(PointerEventData pointerEventData) {
        GameManager.instance.SelectPiece(gameObject);
    }

}