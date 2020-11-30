using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public Material defaultMaterialWhite;
    public Material defaultMaterialBlack;
    public Material selectedMaterial;

    public GameObject selectedPiece;

    MoveSelector selector;

    public GameObject AddPiece(GameObject piece, Player player, int col, int row)
    {
        Vector3 position = transform.GetChild(row).transform.GetChild(col).transform.position;
        position.y += (float)0.02;


        int orientation = player.isWhite ? 90 : -90;
        GameObject newPiece = Instantiate(piece, position, Quaternion.Euler(-90, orientation, 0), gameObject.transform);
        return newPiece;
    }

    public void SelectPiece(GameObject piece)
    {
        //if (GameManager.instance.DoesPieceBelongToCurrentPlayer(piece))
        //{
        if (selectedPiece) DeselectPiece(selectedPiece);

        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = selectedMaterial;
        selectedPiece = piece;

        selector = GetComponent<MoveSelector>();
        selector.EnterState(piece);

        //}
        //else if (selectedPiece)
        //{
        //    List<Vector2Int> moveLocations = GameManager.instance.MovesForPiece(selectedPiece);
        //    Piece pieceScript = piece.GetComponent<Piece>();

        //    Vector2Int piecePosition = pieceScript.position;

        //    if (moveLocations.Contains(piecePosition))
        //    {
        //        GameObject square = Geometry.FindSquare(piecePosition);
        //        GameManager.instance.Move(selectedPiece, square.transform.position, pieceScript.position);
        //    }
        //}
    }

    public void MovePiece(GameObject piece, Vector3 position)
    {
        piece.transform.position = position;

        Piece pieceObject = piece.GetComponent<Piece>();
        pieceObject.moved = true;

        DeselectPiece(piece);
    }

    public void DeselectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();

        renderers.material = GameManager.instance.currentPlayer.isWhite ? defaultMaterialWhite : defaultMaterialBlack;

        selector = GetComponent<MoveSelector>();
        selector.ExitState();

        selectedPiece = null;
    }
}
