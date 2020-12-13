
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public enum PieceType {King, Queen, Bishop, Knight, Rook, Pawn};

public abstract class Piece : MonoBehaviour, IPointerClickHandler
{
    public PieceType type;

    public Vector2Int position;
    public bool moved;

    public Player player;

    protected Vector2Int[] RookDirections = {new Vector2Int(0,1), new Vector2Int(1, 0),
        new Vector2Int(0, -1), new Vector2Int(-1, 0)};

    protected Vector2Int[] BishopDirections = {new Vector2Int(1,1), new Vector2Int(1, -1),
        new Vector2Int(-1, -1), new Vector2Int(-1, 1)};

    protected Vector2Int[] KnightDirections = {new Vector2Int(-1,2), new Vector2Int(1, 2),
        new Vector2Int(2, 1), new Vector2Int(2, -1), new Vector2Int(1,-2),
        new Vector2Int(-1,-2), new Vector2Int(-2, -1), new Vector2Int(-2, -1)};

    public abstract List<Vector2Int> MoveLocations(Vector2Int gridPoint);

    private void Start()
    {
        GetComponent<XRGrabInteractable>().onSelectEntered.AddListener(OnGrab);
        GetComponent<XRGrabInteractable>().onSelectExited.AddListener(OnRelease);
    }

    private void OnRelease(XRBaseInteractor arg0)
    {
        DeselectPiece();
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        Debug.Log(position);
        SelectPiece();
        // only own pieces
        // disable other controller
    }

    public void OnGrab(XRBaseInteractor XRInteractor) {
        SelectPiece();
    }

    private void SelectPiece() {
        GameHandler.instance.SelectPiece(gameObject);
    }

    private void DeselectPiece()
    {
        GameHandler.instance.DeselectPiece(gameObject);
        // check if move is legal
    }
}