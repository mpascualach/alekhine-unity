using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public enum PieceType {King, Queen, Bishop, Knight, Rook, Pawn};

public abstract class Piece : MonoBehaviour, IPointerClickHandler
{
    public int id;
    public PieceType type;

    public Vector2Int position {
        get
        {
            return _position;
        }
        set
        {
            if (startingPositionSet == false) {
                startingPosition = value;
                startingPositionSet = true;
            }
           if (_position != value) _position = value;
        }
    } 
    private Vector2Int _position;

    private bool startingPositionSet;
    public Vector2Int startingPosition;

    public bool developed;
    public bool moved;

    Animator anim;

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

        anim = GetComponent<Animator>();
    }

    private void OnRelease(XRBaseInteractor arg0)
    {
        DeselectPiece();
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        if (GameHandler.instance.currentPlayer.name == player.name)
        {
           SelectPiece();
        } else if (Board.instance.selectedPiece != null)
        {
            GameObject selectedPiece = Board.instance.selectedPiece;
            var selectedPieceMoves = GameHandler.instance.MovesForPiece(selectedPiece);

            if (selectedPieceMoves.Contains(position))
            {
                GameHandler.instance.Move(Board.instance.selectedPiece, position);
            }
            else
            {
                DeselectPiece();
            }
        }
    }

    public void OnGrab(XRBaseInteractor XRInteractor) {
        SelectPiece();
    }

    public void SelectPiece() {
        if (anim != null) {
            anim.SetBool("selected", true);
        }

        if (Board.instance != null)
        {
            Board.instance.SelectPiece(gameObject);
        }
    }

    private void DeselectPiece()
    {
        Board.instance.DeselectPiece(gameObject);
    }

    public void UndoSelectionEffect()
    {
        if (anim != null)
        {
            anim.SetBool("selected", false);
        }
    }
}