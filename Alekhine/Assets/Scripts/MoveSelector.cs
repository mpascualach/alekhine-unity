using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveSelector : MonoBehaviour
{
    public Material moveLocationMaterial;
    public Material attackLocationMaterial;

    private GameObject movingPiece;

    private List<Vector2Int> moveLocations = new List<Vector2Int>();

    public void EnterState(GameObject piece) {
        this.enabled = true;

        movingPiece = piece;
        moveLocations = GameHandler.instance.MovesForPiece(movingPiece);

        HighlightSquares(moveLocations);
    }

    public void HighlightSquares(List<Vector2Int> locations) {
        foreach (Vector2Int location in locations)
        {
            GameObject square = gameObject.transform.GetChild(location.y).GetChild(location.x).gameObject;
            Square squareScript = square.GetComponent<Square>();

            if (GameHandler.instance.PieceAtGrid(location))
            {
                squareScript.TriggerAttackEffect();
            } else
            {
                squareScript.TriggerMoveEffect();
            }
        }
    }


    public void ManageMaterials(GameObject square, Material target) {
        Material[] materials = square.GetComponent<MeshRenderer>().materials;
        materials[1] = target;
        square.GetComponent<MeshRenderer>().materials = materials;
    }

    public void ExitState() {
        this.enabled = false;

        foreach (Vector2Int location in moveLocations)
        {
            GameObject square = gameObject.transform.GetChild(location.y).GetChild(location.x).gameObject;
            Square squareScript = square.GetComponent<Square>();

            if (GameHandler.instance.PieceAtGrid(location))
            {
                squareScript.UndoAttackEffect();
            }
            else
            {
                squareScript.UndoMoveEffect();
            }
        }
    }

}
