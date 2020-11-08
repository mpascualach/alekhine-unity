using UnityEngine;

public class Board : MonoBehaviour {

    public Material defaultMaterialWhite;
    public Material defaultMaterialBlack;
    public Material selectedMaterial;

    public GameObject selectedPiece;

    public GameObject[,] rows;

    public GameObject AddPiece(GameObject piece, int col, int row) {
        //Vector2Int gridPoint = Geometry.GridPoint(col, row);

        //GameObject newPiece = Instantiate(piece, Geometry.PointFromGrid(gridPoint), Quaternion.identity, gameObject.transform);
        return piece;
        //return newPiece;
    }

    public void RemovePiece(GameObject piece) {
        Destroy(piece);
    }

    public void MovePiece(GameObject piece, Vector2Int gridPoint) {
        piece.transform.position = Geometry.PointFromGrid(gridPoint);

        Piece pieceObject = piece.GetComponent<Piece>();
        pieceObject.moved = true;

        DeselectPiece(piece);
    }

    public void SelectPiece(GameObject piece) {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = selectedMaterial;

        TileSelector selector = GetComponent<TileSelector>();
        selector.ExitState(piece);
    }

    public void DeselectPiece(GameObject piece) {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();

        renderers.material = GameManager.instance.currentPlayer.name == "white" ? defaultMaterialWhite :defaultMaterialBlack;

        selectedPiece = null;
    }
}
