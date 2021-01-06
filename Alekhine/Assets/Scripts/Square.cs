using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour, IPointerClickHandler
{
    private Animator anim;

    public GameObject attachedPiece
    {
        get { return _attachedPiece; }
        set
        {
            if (value == null) {
                _attachedPiece = null;
                attachedPieceScript = null;
            } else if (_attachedPiece != value) {
                _attachedPiece = value;
                attachedPieceScript = value.GetComponent<Piece>();
            }
        }
    }

    public List<Move> moves;

    private GameObject _attachedPiece;
    private Piece attachedPieceScript;

    public Vector2Int position;

    private bool isMove;
    private bool isAttack;

    void Start()
    {
        anim = GetComponent<Animator>();
        moves = new List<Move>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (_attachedPiece != null && attachedPieceScript != null)
        {
            attachedPieceScript.SelectPiece();
        }

        if (isMove || isAttack)
        {
            GameObject selectedPiece = Board.instance.selectedPiece;

            if (selectedPiece)
            {
                Piece selectedPieceScript = selectedPiece.GetComponent<Piece>();
                if (selectedPieceScript.type == PieceType.King &&
                    (position.x == selectedPieceScript.position.x - 2 ||
                    position.x == selectedPieceScript.position.x + 2))
                {
                    Player currentPlayer = GameHandler.instance.currentPlayer;

                    Piece kingsideRook = GameHandler.instance.FindRook(7, currentPlayer);
                    Piece queensideRook = GameHandler.instance.FindRook(0, currentPlayer);

                    Debug.Log("Time for castling");

                    if (kingsideRook && position.x == selectedPieceScript.position.x + 2)
                    {
                        Debug.Log("Time for castling 1");
                        GameHandler.instance.Castle(selectedPiece, kingsideRook.gameObject);
                    }
                    else if (queensideRook && position.x == selectedPieceScript.position.x - 2)
                    {
                        Debug.Log("Time for castling 2");
                        GameHandler.instance.Castle(selectedPiece, queensideRook.gameObject);
                    }
                } else
                {
                    GameHandler.instance.Move(selectedPiece, position);
                }
            }

            if (isMove) UndoMoveEffect();
            else UndoAttackEffect();
        }
    }

    public void TriggerMoveEffect()
    {
        anim.SetBool("isMove", true);
        isMove = true;
    }

    public void UndoMoveEffect()
    {
        anim.SetBool("isMove", false);
        isMove = false;
    }

    public void TriggerAttackEffect()
    {
        anim.SetBool("isAttack", true);
        isAttack = true;
    }

    public void UndoAttackEffect()
    {
        anim.SetBool("isAttack", false);
        isAttack = false;
    }
}
