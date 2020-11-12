using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveSelector : MonoBehaviour, IPointerClickHandler
{
    public Material moveLocationMaterial;
    public Material attackLocationMaterial;

    private GameObject movingPiece;

    private List<Vector2Int> moveLocations = new List<Vector2Int>();

    void Start() {
        this.enabled = false;
    }

    public void EnterState(GameObject piece) {
        movingPiece = piece;
        this.enabled = true;

        moveLocations = GameManager.instance.MovesForPiece(movingPiece);

        HighlightSquares(moveLocations, true);
    }

    public void HighlightSquares(List<Vector2Int> locations, bool highlighting) {
        foreach (Vector2Int location in locations)
        {
            GameObject square = gameObject.transform.GetChild(location.y).GetChild(location.x).gameObject;

            Material locationType = highlighting ?
                        GameManager.instance.PieceAtGrid(location) ?
                        attackLocationMaterial :
                        moveLocationMaterial :
                    square.GetComponent<Renderer>().materials[0];

            ManageMaterials(square, locationType);
        }
    }


    public void ManageMaterials(GameObject square, Material target) {
        Material[] materials = square.GetComponent<MeshRenderer>().materials;
        materials[1] = target;
        square.GetComponent<MeshRenderer>().materials = materials;
    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        foreach(GameObject element in pointerEventData.hovered) {
            if (element.tag == "Square") {
                int squareIndex = element.transform.GetSiblingIndex();
                int rowIndex = element.transform.parent.transform.GetSiblingIndex();

                Vector2Int coordinate = new Vector2Int(squareIndex, rowIndex);

                ExecuteMove(element, coordinate);
            }
        }
    }

    public void ExecuteMove(GameObject element, Vector2Int gridPoint) {
        if (moveLocations.Contains(gridPoint)) {
            GameManager.instance.Move(movingPiece, element.transform.position, gridPoint);
        }
    }


    public void ExitState() {
        this.enabled = false;

        foreach (Vector2Int location in moveLocations)
        {
            GameObject square = Geometry.FindSquare(location);
            ManageMaterials(square, square.GetComponent<Renderer>().materials[0]);
        }
    }


}
