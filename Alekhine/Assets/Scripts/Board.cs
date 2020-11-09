using UnityEngine;

public class Board : MonoBehaviour {

    public Material defaultMaterialWhite;
    public Material defaultMaterialBlack;
    public Material selectedMaterial;

    public GameObject selectedPiece;

    MoveSelector selector;

    public GameObject AddPiece(GameObject piece, int col, int row) {
        Vector3 position = transform.GetChild(row).transform.GetChild(col).transform.position;

        GameObject newPiece = Instantiate(piece, position, Quaternion.identity, gameObject.transform);
        return newPiece;
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

        if (selectedPiece)
        {
            DeselectPiece(selectedPiece);
        }

        selector = GetComponent<MoveSelector>();
        selector.EnterState(piece);
        selectedPiece = piece;
    }

    public void DeselectPiece(GameObject piece) {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();

        renderers.material = GameManager.instance.currentPlayer.name == "white" ? defaultMaterialWhite : defaultMaterialBlack;

        selector = GetComponent<MoveSelector>();
        selector.ExitState();

        selectedPiece = null;
    }
}
