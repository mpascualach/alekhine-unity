using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public static Board instance;

    public Material defaultMaterialWhite;
    public Material defaultMaterialBlack;
    public Material selectedMaterial;

    public GameObject selectedPiece;

    MoveSelector selector;

    private void Awake() {
        instance = this;
    }

    public GameObject AddPiece(GameObject piece, Player player, int row, int col) {
        GameObject square = transform.GetChild(row).transform.GetChild(col).gameObject;
        Vector3 position = square.transform.position;
        position.y += (float)0.02;

        int orientation = player.isWhite ? 90 : -90;
        GameObject newPiece = Instantiate(piece, position, Quaternion.Euler(-90, orientation, 0), gameObject.transform);

        Square squareScript = square.GetComponent<Square>();
        squareScript.attachedPiece = newPiece;

        return newPiece;
    }

    public void SelectPiece(GameObject piece) {
        CheckForSelectedPiece();

        selector = GetComponent<MoveSelector>();
        selector.EnterState(piece);

        selectedPiece = piece;
    }

    public void CheckForSelectedPiece(){
        if (selectedPiece != null) {
            Piece selectedPieceScript = selectedPiece.GetComponent<Piece>();
            selectedPieceScript.UndoSelectionEffect();
            DeselectPiece(selectedPiece);
        }
    }

    public void MovePiece(GameObject piece, Vector3 position) {
        piece.transform.position = position;

        Piece pieceObject = piece.GetComponent<Piece>();

        if (pieceObject.developed == false) {
            pieceObject.developed = true;
        }

        DeselectPiece(piece);
    }

    public void DeselectPiece(GameObject piece) {
        Piece pieceScript = piece.GetComponent<Piece>();
        pieceScript.UndoSelectionEffect();

        selector = GetComponent<MoveSelector>();

        if (selector.enabled) {
            selector.ExitState();
        }

        selectedPiece = null;
    }
}
